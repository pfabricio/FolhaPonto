using System.Linq;
using System.Web.Mvc;
using FolhaPonto.Extensions;
using FolhaPonto.Models;
using FolhaPonto.Negocio.Controller;
using FolhaPonto.Negocio.Model;
using PagedList;

namespace FolhaPonto.Controllers
{
    [Authorize]
    public class FuncionarioController : Controller
    {
        private readonly FuncionarioCo _db;
        public FuncionarioController()
        {
            _db = new FuncionarioCo();
        }

        public ActionResult Index(int? page)
        {
            var lst = _db.Listar().Select(x => new FuncionarioListViewModel
            {
                Id = x.Id,
                Cargo = x.Cargo.NomeCargo,
                Perfil = x.Perfil.NomePergil,
                Unidade = x.Unidade.NomeUnidade,
                Nome = x.Nome
            }).ToList();

            var pageSize = Extencoes.NumPage;
            var pageNumber = (page ?? 1);

            return View(lst.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            PopulaCombo();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FuncionarioViewModel funcionario)
        {
            PopulaCombo();
            if (!ModelState.IsValid)
                return View(funcionario);

            _db.Add(SetFuncionarioCad(funcionario));
            _db.Commit();
            this.EnviarMensagem("Funcionário cadastrado com sucesso.");

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var funcionario = GetFuncionario(id);
            PopulaCombo();
            return View(funcionario);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FuncionarioEditViewModel funcionario)
        {
            PopulaCombo();

            if (!ModelState.IsValid)
                return View(funcionario);

            _db.Update(SetFuncionarioEdit(funcionario));
            _db.Commit();
            this.EnviarMensagem("Funcionário alterado com sucesso.");
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var funcionario = _db.Listar().Select(x => new FuncionarioListViewModel
            {
                Id = x.Id,
                Cargo = x.Cargo.NomeCargo,
                Perfil = x.Perfil.NomePergil,
                Unidade = x.Unidade.NomeUnidade,
                Nome = x.Nome,
                Email = x.Email,
                Endereco = x.Endereco,
                Telefone = x.Telefone
            }).SingleOrDefault();

            return View(funcionario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            var funcionarioMo = _db.Find(x => x.Id == id);
            if (funcionarioMo != null && funcionarioMo.Frequencias.Count > 0)
            {
                var freqCo = new FrequenciaCo();
                freqCo.Excluir(x => x.Funcionario.Id == id);
                freqCo.Commit();
            }

            _db.Excluir(id);
            _db.Commit();
            this.EnviarMensagem("Funcionário foi excluído com sucesso.");

            return RedirectToAction("Index");
        }

        #region Medotod Privados
        private FuncionarioEditViewModel GetFuncionario(int id)
        {
            var funcionaro = _db.Find(x => x.Id == id);
            return new FuncionarioEditViewModel
            {
                Id = funcionaro.Id,
                CargoId = funcionaro.Cargo.Id,
                PerfilId = funcionaro.Perfil.Id,
                JornadaId = funcionaro.Jornada.Id,
                UnidadeId = funcionaro.Unidade.Id,

                Nome = funcionaro.Nome,
                Email = funcionaro.Email,
                Endereco = funcionaro.Endereco,
                Telefone = funcionaro.Telefone
            };
        }
        private FuncionarioMo SetFuncionarioCad(FuncionarioViewModel funcionario)
        {
            var funcionarioMo = _db.Find(x => x.Id == funcionario.Id) ?? new FuncionarioMo();

            funcionarioMo.CargoId = funcionario.CargoId;
            funcionarioMo.PerfilId = funcionario.PerfilId;
            funcionarioMo.UnidadeId = funcionario.UnidadeId;
            funcionarioMo.JornadaId = funcionario.JornadaId;

            funcionarioMo.Nome = funcionario.Nome;
            funcionarioMo.Email = funcionario.Email;
            funcionarioMo.Endereco = funcionario.Endereco;
            funcionarioMo.Telefone = funcionario.Telefone;
            funcionarioMo.Login = funcionario.Login;
            funcionarioMo.Senha = funcionario.Senha;
            
            return funcionarioMo;
        }
        private FuncionarioMo SetFuncionarioEdit(FuncionarioEditViewModel funcionario)
        {
            var funcionarioMo = _db.Find(x => x.Id == funcionario.Id) ?? new FuncionarioMo();

            funcionarioMo.CargoId = funcionario.CargoId;
            funcionarioMo.PerfilId = funcionario.PerfilId;
            funcionarioMo.UnidadeId = funcionario.UnidadeId;
            funcionarioMo.JornadaId = funcionario.JornadaId;

            funcionarioMo.Nome = funcionario.Nome;
            funcionarioMo.Email = funcionario.Email;
            funcionarioMo.Endereco = funcionario.Endereco;
            funcionarioMo.Telefone = funcionario.Telefone;

            return funcionarioMo;
        }
        private void PopulaCombo()
        {
            var perfilCo = new PerfilCo();
            var unidadeCo = new UnidadeCo();
            var cargoCo = new CargoCo();
            //var jornadaCo = new JornadaCo();

            ViewBag.Perfil = perfilCo.Listar().Select(x => new { x.Id, Nome = x.NomePergil });
            ViewBag.Unidade = unidadeCo.Listar().Select(x => new { x.Id, Nome = x.NomeUnidade });
            ViewBag.Cargo = cargoCo.Listar().Select(x => new { x.Id, Nome = x.NomeCargo });
            //ViewBag.Jornada = jornadaCo.Listar().Select(x => new { x.Id, Nome = x.Descricao });
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if(disposing)
                _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
