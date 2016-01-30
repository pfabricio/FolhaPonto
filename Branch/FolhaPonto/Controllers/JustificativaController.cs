using System;
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
    public class JustificativaController : Controller
    {
        internal readonly JustificavaCo Db;
        public JustificativaController()
        {
            Db = new JustificavaCo();
        }

        public ActionResult Index(int? page)
        {
            var lst = Db.Listar().Select(x => new JustificativaListViewModel
            {
                Id = x.Id,
                FuncionarioNome = x.Funcionario.Nome,
                Data = x.Data.ToString("dd/MM/yyyy"),
                Inicio = x.Inicio.ToString(@"hh\:mm"),
                Fim = x.Fim.ToString(@"hh\:mm"),
                Texto = x.Texto.Left(50)
            }).ToList();

            var lista = lst.OrderByDescending(x => DateTime.Parse(x.Data)).ToList();

            var pageSize = Extencoes.NumPage;
            var pageNumber = (page ?? 1);

            return View(lista.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Detail(int id)
        {
            var jus = Db.Find(x => x.Id == id);
            return View(new JustificativaListViewModel
            {
                Id = jus.Id,
                FuncionarioNome = jus.Funcionario.Nome,
                Data = jus.Data.ToString("dd/MM/yyyy"),
                Inicio = jus.Inicio.ToString(@"hh\:mm"),
                Fim = jus.Fim.ToString(@"hh\:mm"),
                Texto = jus.Texto
            });
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FuncionarioNome,FuncionarioId,Data,Inicio,Fim,Texto")] JustificativaViewModel justificativa)
        {
            if(!ModelState.IsValid) return View(justificativa);

            Db.Add(SetJustificativa(justificativa));
            Db.Commit();
            this.EnviarMensagem("sua justificativa foi cadastrada com sucesso.");
            return RedirectToAction("Index");
        }

        #region Metodos Privados
        private JustificativaMo SetJustificativa(JustificativaViewModel justificativa)
        {
            var justMo = Db.Find(x => x.Id == justificativa.Id) ?? new JustificativaMo();

            justMo.FuncionarioId = justificativa.FuncionarioId;
            justMo.Data = justificativa.Data;
            justMo.Inicio = justificativa.Inicio;
            justMo.Fim = justificativa.Fim;
            justMo.Texto = justificativa.Texto;

            return justMo;
        }
        #endregion

        #region Metodos Ajax
        public ActionResult GetFuncionarios(string term)
        {
            var funcCo = new FuncionarioCo();
            var lst =
                funcCo.Listar(x => x.Nome.ToUpper().StartsWith(term.ToUpper()))
                    .Select(x => new {id = x.Id, value = x.Nome, label = x.Nome}).Take(10)
                    .ToList();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Db.Dispose();
            base.Dispose(disposing);
        }
    }
}
