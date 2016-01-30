using System.Web.Mvc;
using FolhaPonto.Extensions;
using FolhaPonto.Models;
using FolhaPonto.Negocio.Controller;
using FolhaPonto.Negocio.Model;

namespace FolhaPonto.Controllers
{
    [Authorize]
    public class ContaController : Controller
    {
        internal readonly FuncionarioCo Db;
        public ContaController()
        {
            Db = new FuncionarioCo();
        }
        public ActionResult Conta(string nome)
        {
            var funcMo = Db.Find(x => x.Login.Equals(nome));
            return View(GetConta(funcMo));
        }
        public ActionResult TrocarSenha(int id)
        {
            return View(new TrocaSenhaViewModel {Id = id});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TrocarSenha([Bind(Include = "Id,Login,SenhaAntiga,SenhaNova,ConfirmSenha")] TrocaSenhaViewModel troca)
        {
            if (!ModelState.IsValid) return View(troca);

            if (!Db.IsAutenticado(troca.Login, troca.SenhaAntiga))
            {
                this.EnviarMensagem("Login ou senha inválidos","2");
                return View(troca);
            }

            var funcMo = Db.Find(c => c.Id == troca.Id);

            funcMo.Senha = troca.SenhaNova;
            Db.Update(funcMo);
            Db.Commit();

            this.EnviarMensagem("A Senha foi alterada com sucesso.");
            return View(troca);
        }

        #region Metodos Privados
        public ContaViewModel GetConta(FuncionarioMo funcMo)
        {
            return new ContaViewModel
            {
                Id = funcMo.Id,
                Login = funcMo.Login,
                Nome = funcMo.Nome
            };
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if(disposing)
                Db.Dispose();
            base.Dispose(disposing);
        }
    }
}
