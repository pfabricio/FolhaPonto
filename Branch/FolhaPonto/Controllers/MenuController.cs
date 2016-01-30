using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using FolhaPonto.Extensions;
using FolhaPonto.Models;
using FolhaPonto.Negocio.Controller;
using FolhaPonto.Negocio.Model;
using PagedList;

namespace FolhaPonto.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        private readonly MenuCo _db;
        public MenuController()
        {
            _db = new MenuCo();
        }
        public ActionResult Index(int? page, int? peso)
        {
            var perfilCo = new PerfilCo();
            ViewBag.Peso = perfilCo.Listar().Select(x => new SelectListItem { Value = Convert.ToString(x.Peso), Text = x.NomePergil }).ToList();

            var pageSize = Extencoes.NumPage;
            var pageNumber = (page ?? 1);

            List<MenuViewModel> lst;
            if (peso != null)
            {
                lst = _db.Listar(x=>x.Peso == Convert.ToInt32(peso)).Select(x => new MenuViewModel
                {
                    Id = x.Id,
                    Action = x.Action,
                    Controller = x.Controller,
                    Peso = x.Peso
                }).ToList();

                ViewBag.ItemSelecionado = peso;

                return View(lst.ToPagedList(pageNumber, pageSize));
            }

            lst = _db.Listar().Select(x => new MenuViewModel
            {
                Id = x.Id,
                Action = x.Action,
                Controller = x.Controller,
                Peso = x.Peso
            }).ToList();

            return View(lst.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Create()
        {
            PopularCombo();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Action,Controller,Peso,Imagem")] MenuViewModel menu)
        {
            PopularCombo();

            if (!ModelState.IsValid)
                return View(menu);

            _db.Add(SetMenu(menu));
            _db.Commit();
            this.EnviarMensagem("Menu foi cadastrado com sucesso.");

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            return View(GetMenu(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            _db.Excluir(id);
            _db.Commit();

            this.EnviarMensagem("O menu foi escluído com sucesso.");
            return RedirectToAction("Index");

            return View();
        }

        #region Medotod Privados
        private MenuViewModel GetMenu(int id)
        {
            var menu = _db.Find(x => x.Id == id);
            return new MenuViewModel()
            {
                Id = menu.Id,
                Action = menu.Action,
                Controller = menu.Controller,
                Imagem = menu.Imagem,
                Peso = menu.Peso
            };
        }
        private MenuMo SetMenu(MenuViewModel menu)
        {
            var menuMo = _db.Find(x => x.Id == menu.Id) ?? new MenuMo();
            menuMo.Action = menu.Action;
            menuMo.Controller = menu.Controller;
            menuMo.Imagem = menu.Imagem;
            menuMo.Peso = menu.Peso;
            return menuMo;
        }
        private void PopularCombo()
        {
            var perfilCo = new PerfilCo();
            ViewBag.Peso = perfilCo.Listar().Select(x => new { Id = x.Peso, Nome = x.NomePergil }).ToList();
            ViewBag.Controller = GetControllerNames().Select(x => new { Id = x, Nome = x }).ToList();
            ViewBag.Imagem = GetImagemNome().Select(x => new {Id = x, Nome = x}).ToList();
        }
        public List<string> GetImagemNome()
        {
            var files = Directory.GetFiles(Server.MapPath("~/Content/imagem/menu"), "*.png");
            return (from item in files select item.Split('\\') into sDir let tam = sDir.Length select sDir[tam - 1]).ToList();
        }
        #endregion

        #region Metodos do Combo Controller e Action
        private IEnumerable<Type> GetSubClasses<T>()
        {
            return Assembly.GetCallingAssembly().GetTypes().Where(
                type => type.IsSubclassOf(typeof(T))).ToList();
        }
        public List<string> GetControllerNames()
        {
            var lst = GetSubClasses<Controller>();
            return lst.Select(controller => controller.Name.Replace("Controller", string.Empty)).ToList();
        }
        public List<string> GetActionNames(string controllerName)
        {
            if (!controllerName.EndsWith("Controller"))
            {
                controllerName += "Controller";
            }
            var lstAction = new List<string>();
            var controllers =
                Assembly.GetExecutingAssembly()
                    .GetExportedTypes()
                    .Where(t => typeof(ControllerBase).IsAssignableFrom(t) && t.Name == controllerName)
                    .Select(t => t);

            foreach (var actions in controllers.Select(controller => controller.GetMethods()
                .Where(
                    t =>
                        t.DeclaringType != null && (t.Name != "Dispose" && !t.IsSpecialName &&
                                                    t.DeclaringType.IsSubclassOf(typeof(ControllerBase)) && t.IsPublic && !t.IsStatic))
                .ToList()))
            {
                lstAction.AddRange(actions.Select(action => action.Name));
            }

            return lstAction.Distinct().ToList();
        }
        #endregion

        #region Metodos Ajax
        public ActionResult ListaActions(string controllerNome)
        {
            var lst = GetActionNames(controllerNome).Select(x => new SelectListItem {Text = x, Value = x}).ToList();
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