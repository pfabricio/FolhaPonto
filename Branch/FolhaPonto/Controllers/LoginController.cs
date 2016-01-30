using System.Web.Mvc;
using System.Web.Security;
using FolhaPonto.Extensions;
using FolhaPonto.Models;
using FolhaPonto.Negocio.Controller;

namespace FolhaPonto.Controllers
{
    public class LoginController : Controller
    {
        private readonly FuncionarioCo _db;
        public LoginController()
        {
            _db = new FuncionarioCo();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel funcionario, string ReturnUrl)
        {
            if (!ModelState.IsValid) return View(funcionario);
            var isValidarUser = _db.IsAutenticado(funcionario.Login, funcionario.Senha);
            if (isValidarUser)
            {
                FormsAuthentication.SetAuthCookie(funcionario.Login, false);

                if (Url.IsLocalUrl(ReturnUrl))
                    return Redirect(ReturnUrl);
                else
                    return RedirectToAction("Index", "Home");
            }
            else
            {
                this.EnviarMensagem("Verifique o seu Login ou a sua senha.","2");
                return RedirectToAction("Index", "Login", new {info = "notAuth"});
            }
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
                _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
