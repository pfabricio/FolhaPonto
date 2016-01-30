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
    public class UnidadeController : Controller
    {
        private readonly UnidadeCo _db;
        public UnidadeController()
        {
            _db = new UnidadeCo();
        }
        public ActionResult Index(int? page)
        {
            var lst = _db.Listar().Select(x => new UnidadeViewModel
            {
                Id = x.Id,
                NomeUnidade = x.NomeUnidade
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
        public ActionResult Create([Bind(Include = "Id,NomeUnidade")] UnidadeViewModel unidade)
        {
            if (!ModelState.IsValid)
                return View(unidade);

            _db.Add(SetUnidade(unidade));
            _db.Commit();
            this.EnviarMensagem("Unidade foi cadastrada com sucesso.");
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            return View(GetUnidade(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NomeUnidade")] UnidadeViewModel unidade)
        {
            if (!ModelState.IsValid)
                return View(unidade);

            _db.Update(SetUnidade(unidade));
            _db.Commit();
            this.EnviarMensagem("Unidade foi alterada com sucesso.");
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            return View(GetUnidade(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            var unidade = _db.Find(x => x.Id == id);
            if (unidade.Funcionarios.Count == 0)
            {
                _db.Excluir(id);
                _db.Commit();
                this.EnviarMensagem("Unidade foi excluída com sucesso.");
            }
            else
                this.EnviarMensagem("Não foi possivel excluir. Porque existe funcionário com esta Unidade", "2");

            return RedirectToAction("Index");
        }

        #region Metodos Privados
        private UnidadeViewModel GetUnidade(int id)
        {
            var unidadade = _db.Find(x => x.Id == id);
            return new UnidadeViewModel
            {
                Id = unidadade.Id,
                NomeUnidade = unidadade.NomeUnidade
            };
        }
        private UnidadeMo SetUnidade(UnidadeViewModel unidade)
        {
            var unidadeMo = _db.Find(x => x.Id == unidade.Id) ?? new UnidadeMo();
            unidadeMo.NomeUnidade = unidade.NomeUnidade;
            return unidadeMo;
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
