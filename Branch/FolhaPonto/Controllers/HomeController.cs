using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using FolhaPonto.Models;
using FolhaPonto.Negocio.Controller;

namespace FolhaPonto.Controllers
{
    public enum MyEnum
    {
        Caixa = 100,
        Gerente = 200
    }
    public enum TipoEntrada
    {
        Entrada = 1,
        EntradaIntervalo = 2,
        SaidaIntervalo = 3,
        Saida = 4
    }

    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated) return View();

            var login = User.Identity.Name;
            var funcCo = new FuncionarioCo();
            var menuCo = new MenuCo();

            var func = funcCo.Find(x => x.Login.Equals(login));
            ViewBag.Menu = menuCo.GetMenus(func.Perfil.Peso).Select(x => new MenuViewModel
            {
                Id = x.Id,
                Action = x.Action,
                Controller = x.Controller,
                Peso = x.Peso,
                Imagem = "~/Content/imagem/menu/" + x.Imagem
            }).ToList();
            return View();
        }
    }
}