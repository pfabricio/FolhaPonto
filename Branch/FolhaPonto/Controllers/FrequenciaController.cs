using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using FolhaPonto.Extensions;
using FolhaPonto.Models;
using FolhaPonto.Negocio.Controller;
using FolhaPonto.Negocio.Model;
using PagedList;

namespace FolhaPonto.Controllers
{
    [Authorize]
    public class FrequenciaController : Controller
    {
        internal readonly FrequenciaCo Db;
        public FrequenciaController()
        {
            Db = new FrequenciaCo();
        }

        public ActionResult Index(int? page)
        {

            var lst = Db.Listar().Select(x => new FrequenciaListViewModel
            {
                Id = x.Id,
                FuncionarioNome = x.Funcionario.Nome,
                Data = x.Data.ToString("dd/MM/yyyy"),
                Entrada = x.Entrada.ToString(@"hh\:mm"),
                EntradaIntervalo = x.EntradaIntervalo.ToString(@"hh\:mm"),
                SaidaIntervalo = x.SaidaIntervalo.ToString(@"hh\:mm"),
                Saida = x.Saida.ToString(@"hh\:mm")
            }).ToList();

            var pageSize = Extencoes.NumPage;
            var pageNumber = (page ?? 1);

            return View(lst.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Entrada(string login)
        {
            var freqId = SetFrequencia(login, TipoEntrada.Entrada);
            return freqId == 0 ? RedirectToAction("Index", "Home") : RedirectToAction("ExibeInfo", new { id = freqId, tipo = (int)TipoEntrada.Entrada });
        }

        public ActionResult Intervalo(string login)
        {
            var freqId = SetFrequencia(login, TipoEntrada.EntradaIntervalo);
            return freqId == 0 ? RedirectToAction("Index", "Home") : RedirectToAction("ExibeInfo", new { id = freqId, tipo = (int)TipoEntrada.EntradaIntervalo });
        }

        public ActionResult VoltarIntervalo(string login)
        {
            var freqId = SetFrequencia(login, TipoEntrada.SaidaIntervalo);
            return freqId == 0 ? RedirectToAction("Index", "Home") : RedirectToAction("ExibeInfo", new { id = freqId, tipo = (int)TipoEntrada.SaidaIntervalo });
        }

        public ActionResult Saida(string login)
        {
            var freqId = SetFrequencia(login, TipoEntrada.Saida);
            return freqId == 0 ? RedirectToAction("Index", "Home") : RedirectToAction("ExibeInfo", new { id = freqId, tipo = (int)TipoEntrada.Saida });
        }

        public ActionResult ExibeInfo(int id, int tipo)
        {
            var lst = Db.Listar(x => x.Id == id).Select(x => new FrequenciaListViewModel
            {
                Data = x.Data.ToString("dd/MM/yyyy"),
                Entrada = x.Entrada.ToString(@"hh\:mm"),
                EntradaIntervalo = x.EntradaIntervalo.ToString(@"hh\:mm"),
                SaidaIntervalo = x.SaidaIntervalo.ToString(@"hh\:mm"),
                Saida = x.Saida.ToString(@"hh\:mm")
            }).ToList();

            ViewBag.Tipo = tipo;

            return View(lst.SingleOrDefault());
        }

        public ActionResult RelatorioMetas()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExebirRelatorios([Bind(Include = "Funcionario,Funcionarioid,Data1,Data2")] RelatorioMetasViewModel relatorio)
        {
            var funcCo = new FuncionarioCo();

            var func = funcCo.Find(x => x.Id == relatorio.Funcionarioid);

            ViewBag.TempoMeta = func.Jornada.TempoMeta;

            if (!ModelState.IsValid) return RedirectToAction("RelatorioMetas");

            var lst =
                Db.Listar(
                    x =>
                        x.Data >= DateTime.Parse(relatorio.Data1) && x.Data <= DateTime.Parse(relatorio.Data2) &&
                        x.Funcionario.Id == relatorio.Funcionarioid)
                        .Select(x => new FrequenciaListViewModel
                        {
                            Id = x.Id,
                            FuncionarioNome = x.Funcionario.Nome,
                            Data = x.Data.ToString("dd/MM/yyyy"),
                            Entrada = x.Entrada.ToString(@"hh\:mm"),
                            EntradaIntervalo = x.EntradaIntervalo.ToString(@"hh\:mm"),
                            SaidaIntervalo = x.SaidaIntervalo.ToString(@"hh\:mm"),
                            Saida = x.Saida.ToString(@"hh\:mm"),
                            HoraTrabalhada = x.HoraTrabalhada
                        }).ToList();

            ViewBag.Total =
                lst.GroupBy(x => x.FuncionarioNome)
                    .Select(
                        x =>
                            new FrequenciaListViewModel
                            {
                                FuncionarioNome = x.Key,
                                HoraTrabalhada = x.Sum(y => y.HoraTrabalhada)
                            }).SingleOrDefault();

            return View(lst);
        }

        #region Metodos Privados
        private FrequenciaMo GetFrequencia(string login, TipoEntrada tipo)
        {
            var funcC0 = new FuncionarioCo();
            var func = funcC0.Find(x => x.Login.Equals(login));
            var hoje = DateTime.Parse(DateTime.Now.ToShortDateString());
            var hora = DateTime.Now;

            var freqMo = Db.Find(x => x.Data == hoje && x.Funcionario.Id == func.Id) ?? new FrequenciaMo();

            freqMo.Data = hoje;
            freqMo.FuncionarioId = func.Id;

            switch (tipo)
            {
                case TipoEntrada.Entrada:
                    {
                        freqMo.Entrada = new TimeSpan(hora.Hour, hora.Minute, 0);
                        freqMo.IsEntrada = true;
                        break;
                    }
                case TipoEntrada.EntradaIntervalo:
                    {
                        freqMo.EntradaIntervalo = new TimeSpan(hora.Hour, hora.Minute, 0);
                        freqMo.IsSaidaIntervalo = true;
                        break;
                    }
                case TipoEntrada.SaidaIntervalo:
                    {
                        freqMo.SaidaIntervalo = new TimeSpan(hora.Hour, hora.Minute, 0);
                        freqMo.IsVoltaIntervalo = true;
                        break;
                    }
                case TipoEntrada.Saida:
                    {
                        //freqMo.Saida = new TimeSpan(hora.Hour, hora.Minute, 0);
                        freqMo.IsSaida = true;

                        var horaTrabalhada = (freqMo.Saida - freqMo.SaidaIntervalo) +
                                             (freqMo.EntradaIntervalo - freqMo.Entrada);

                        freqMo.HoraTrabalhada = Convert.ToDecimal(horaTrabalhada.TotalHours);
                        break;
                    }
            }

            return freqMo;
        }

        private bool FrequenciaValida(string login, TipoEntrada tipo)
        {
            var hoje = DateTime.Parse(DateTime.Now.ToShortDateString());

            var freq = new FrequenciaMo();

            switch (tipo)
            {
                case TipoEntrada.Entrada:
                    {
                        freq = Db.Find(x => x.Funcionario.Login.Equals(login) && x.Data == hoje && x.IsEntrada);
                        break;
                    }
                case TipoEntrada.EntradaIntervalo:
                    {
                        freq = Db.Find(x => x.Funcionario.Login.Equals(login) && x.Data == hoje && x.IsSaidaIntervalo);
                        break;
                    }
                case TipoEntrada.SaidaIntervalo:
                    {
                        freq = Db.Find(x => x.Funcionario.Login.Equals(login) && x.Data == hoje && x.IsVoltaIntervalo);
                        break;
                    }
                case TipoEntrada.Saida:
                    {
                        freq = Db.Find(x => x.Funcionario.Login.Equals(login) && x.Data == hoje && x.IsSaida);
                        break;
                    }
            }

            return freq == null;
        }

        private int SetFrequencia(string login, TipoEntrada tipo)
        {
            if (!FrequenciaValida(login, tipo))
            {
                this.EnviarMensagem("Já foi realizada a frequência de entrada", "2");
                return 0;
            }

            var freq = GetFrequencia(login, tipo);

            //Db.AddOrUpdate(freq);
            //Db.Commit();

            return freq.Id;
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
