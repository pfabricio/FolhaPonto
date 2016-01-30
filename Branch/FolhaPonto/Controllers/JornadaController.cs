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
    public class JornadaController : Controller
    {
        private readonly JornadaCo _db;
        public JornadaController()
        {
            _db = new JornadaCo();
        }
        
        public ActionResult Index(int? page)
        {
            var lst = _db.Listar().Select(x => new JornadaListViewModel()
            {
                Id = x.Id,
                Cargo = x.Cargo.NomeCargo,
                HoraEntrada = x.HoraEntrada.ToString(@"hh\:mm"),
                HoraSaida = x.HoraSaida.ToString(@"hh\:mm"),
                TempoIntervalo = x.TempoIntervalo,
                TempoMeta = x.TempoMeta
            }).ToList();

            var pageSize = Extencoes.NumPage;
            var pageNumber = (page ?? 1);

            return View(lst.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Create()
        {
            var cargoCo = new CargoCo();
            ViewBag.Cargo = cargoCo.Listar().Select(x => new
            {
                x.Id,
                Nome = x.NomeCargo
            }).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CargoId,HoraEntrada,HoraSaida,TempoIntervalo,TempoMeta")] JornadaViewModel jornada)
        {
            var cargoCo = new CargoCo();
            ViewBag.Cargo = cargoCo.Listar().Select(x => new
            {
                x.Id,
                Nome = x.NomeCargo
            }).ToList();

            if (!ModelState.IsValid)
                return View(jornada);

            _db.Add(SetJornada(jornada));
            _db.Commit();
            this.EnviarMensagem("Jornada foi cadastrada com sucesso.");
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var cargoCo = new CargoCo();
            ViewBag.Cargo = cargoCo.Listar().Select(x => new
            {
                x.Id,
                Nome = x.NomeCargo
            }).ToList();

            var jornada = GetJornada(id);

            return View(jornada);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CargoId,HoraEntrada,HoraSaida,TempoIntervalo,TempoMeta")] JornadaViewModel jornada)
        {
            var cargoCo = new CargoCo();
            ViewBag.Cargo = cargoCo.Listar().Select(x => new
            {
                x.Id,
                Nome = x.NomeCargo
            }).ToList();

            if (!ModelState.IsValid)
                return View(jornada);

            _db.Update(SetJornada(jornada));
            _db.Commit();
            this.EnviarMensagem("Jornada foi editada com sucesso.");
            return RedirectToAction("Index");
        }
        
        public ActionResult Delete(int id)
        {
            var jornadaMo = _db.Find(x => x.Id == id);
            return View(new JornadaListViewModel
            {
                Id = jornadaMo.Id,
                Cargo = jornadaMo.Cargo.NomeCargo,
                HoraEntrada = jornadaMo.HoraEntrada.ToString(@"hh\:mm"),
                HoraSaida = jornadaMo.HoraSaida.ToString(@"hh\:mm"),
                TempoIntervalo = jornadaMo.TempoIntervalo,
                TempoMeta = jornadaMo.TempoMeta
            });
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id, FormCollection collection)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            var jornada = _db.Find(x => x.Id == id);
            if (jornada.Funcionarios.Count == 0)
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
        private JornadaViewModel GetJornada(int id)
        {
            var jornada = _db.Find(x => x.Id == id);
            return new JornadaViewModel
            {
                Id = jornada.Id,
                CargoId = jornada.Cargo.Id,
                HoraEntrada = jornada.HoraEntrada,
                HoraSaida = jornada.HoraSaida,
                TempoIntervalo = jornada.TempoIntervalo,
                TempoMeta = jornada.TempoMeta
            };
        }
        private JornadaMo SetJornada(JornadaViewModel jornada)
        {
            var jornadaMo = _db.Find(x => x.Id == jornada.Id) ?? new JornadaMo();
            jornadaMo.CargoId = jornada.CargoId;
            jornadaMo.HoraEntrada = jornada.HoraEntrada;
            jornadaMo.HoraSaida = jornada.HoraSaida;
            jornadaMo.TempoIntervalo = jornada.TempoIntervalo;
            jornadaMo.TempoMeta = jornada.TempoMeta;
            return jornadaMo;
        }
        #endregion

        #region Metodos Ajax
        public ActionResult GetJornada(string cargoid)
        {
            var jornCo = new JornadaCo();
            var lst =
                jornCo.Listar(x => x.Cargo.Id == int.Parse(cargoid))
                    .Select(x => new SelectListItem {Value = Convert.ToString(x.Id), Text = x.Descricao})
                    .ToList();
            return Json(lst, JsonRequestBehavior.AllowGet);
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
