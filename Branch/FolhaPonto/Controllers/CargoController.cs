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
    public class CargoController : Controller
    {
        private readonly CargoCo _db;
        public CargoController()
        {
            _db = new CargoCo();
        }
        public ActionResult Index(int? page)
        {
            var lst = _db.Listar().Select(x => new CargoViewModel
            {
                Id = x.Id,
                NomeCargo = x.NomeCargo
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
        public ActionResult Create([Bind(Include = "Id,NomeCargo")] CargoViewModel cargo)
        {
            if (!ModelState.IsValid)
                return View(cargo);

            _db.Add(SetCargo(cargo));           
            _db.Commit();
            this.EnviarMensagem("Cargo foi cadastrado com sucesso.");
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            return View(GetCargo(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NomeCargo")] CargoViewModel cargo)
        {
            if (!ModelState.IsValid)
                return View(cargo);

            _db.Update(SetCargo(cargo));
            _db.Commit();
            this.EnviarMensagem("Cargo foi alterado com sucesso.");
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            return View(GetCargo(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            var cargo = _db.Find(x => x.Id == id);

            if (cargo.Funcionarios.Count > 0)
            {
                this.EnviarMensagem("Não foi possivel excluir. Porque existe funcionário com este cargo.", "2");
                return RedirectToAction("Index");
            }

            if (cargo.Jornadas.Count > 0)
            {
                this.EnviarMensagem("Não foi possivel excluir. Porque existe jornada com este cargo.", "2");
                return RedirectToAction("Index");
            }

            _db.Excluir(id);
            _db.Commit();
            this.EnviarMensagem("Cargo foi excluído com sucesso.");

            return RedirectToAction("Index");
        }

        #region Medotod Privados
        private CargoViewModel GetCargo(int id)
        {
            var cargo = _db.Find(x => x.Id == id);
            return new CargoViewModel
            {
                Id = cargo.Id,
                NomeCargo = cargo.NomeCargo
            };
        }

        private CargoMo SetCargo(CargoViewModel cargo)
        {
            var cargoMo = _db.Find(x => x.Id == cargo.Id) ?? new CargoMo();
            cargoMo.NomeCargo = cargo.NomeCargo;
            return cargoMo;
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
