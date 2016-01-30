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
    public class PerfilController : Controller
    {
        private readonly PerfilCo _db;
        public PerfilController()
        {
            _db = new PerfilCo();
        }

        public ActionResult Index(int? page)
        {
            var lst = _db.Listar().Select(x => new PerfilViewModel
            {
                Id = x.Id,
                NomePerfil = x.NomePergil,
                Peso = x.Peso
            }).ToList();

            var pageSize = Extencoes.NumPage;
            var pageNumber = (page ?? 1);

            return View(lst.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NomePerfil,Peso")] PerfilViewModel perfil)
        {
            if (!ModelState.IsValid)
                return View(perfil);

            _db.Add(SetPerfil(perfil));
            _db.Commit();

            this.EnviarMensagem("Perfil cadastrado com sucesso.");

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            return View(GetPerfil(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NomePerfil,Peso")] PerfilViewModel perfil)
        {
            if (!ModelState.IsValid)
                return View(perfil);

            this.EnviarMensagem("Perfil alterado com sucesso.");

            _db.Update(SetPerfil(perfil));
            _db.Commit();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            return View(GetPerfil(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            var perfil = _db.Find(x => x.Id == id);
            if (perfil.Funcionarios.Count == 0)
            {
                _db.Excluir(id);
                _db.Commit();
                this.EnviarMensagem("Perfil foi excluído com sucesso.");
            }
            else
                this.EnviarMensagem("Não foi possivel excluir. Porque existe funcionário com este perfil", "2");

            return RedirectToAction("Index");
        }

        #region Medotod Privados
        private PerfilViewModel GetPerfil(int id)
        {
            var perfil = _db.Find(x => x.Id == id);
            return  new PerfilViewModel
            {
                Id = perfil.Id,
                NomePerfil = perfil.NomePergil,
                Peso = perfil.Peso
            };
        }

        private PerfilMo SetPerfil(PerfilViewModel perfil)
        {
            var perfilMo = _db.Find(x => x.Id == perfil.Id) ?? new PerfilMo();
            perfilMo.NomePergil = perfil.NomePerfil;
            perfilMo.Peso = perfil.Peso;
            return perfilMo;
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
