using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Apresentacao.MVC4Unity.Models;
using Graxei.Modelo;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Utilidades.Excecoes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;

namespace Graxei.Apresentacao.MVC4Unity.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IConsultasProdutoVendedor consultaVendedor, IConsultasPlanos consultasPlanos, IConsultasLojas consultasLojas)
        {
            _iConsultasProdutoVendedor = consultaVendedor;
            _consultasPlanos = consultasPlanos;
            _consultasLojas = consultasLojas;
        }
        //
        // GET: /Home/
        [AllowAnonymous]
        public ActionResult Index(string q)
        {
            if (q != null)
            {
                ViewBag.q = q;
            }
            return View("");
        }

        [AllowAnonymous]
        public ActionResult Pesquisar(string q)
        {
            DateTime dtIni = DateTime.Now;
            IList<PesquisaContrato> list;
            IpRegiaoModel ir = (IpRegiaoModel)Session["IpRegiaoModel"];
            if (ir == null)
                list = _iConsultasProdutoVendedor.Get(q, "", "", 0);
            else
                list = _iConsultasProdutoVendedor.Get(q, ir.Pais, ir.Cidade, 0);

            PesquisarModel pm = new PesquisarModel();
            pm.Texto = q;
            pm.PaginaSelecionada = 0;

            if (list.Count < 10)
                pm.NumeroMaximoPagina = 0;
            else
                pm.NumeroMaximoPagina = null;

            TempData["txtSearch"] = ViewBag.PesquisarModel = pm;

            TimeSpan tf = DateTime.Now - dtIni;
            ViewBag.TempoBusca = tf.Seconds + "," + tf.Milliseconds;
            ViewBag.newq = q;
            return View(list);
        }

        [AllowAnonymous]
        public ActionResult PesquisarPagina(string paginaSelecionada)
        {
            DateTime dtIni = DateTime.Now;

            PesquisarModel pm = (PesquisarModel)TempData["txtSearch"];
            pm.PaginaSelecionada = Convert.ToInt32(paginaSelecionada);

            IList<PesquisaContrato> list;
            try
            {
                IpRegiaoModel ir = (IpRegiaoModel)Session["IpRegiaoModel"];
                if (ir == null)
                    list = _iConsultasProdutoVendedor.Get(pm.Texto, "", "", Convert.ToInt32(pm.PaginaSelecionada));
                else
                    list = _iConsultasProdutoVendedor.Get(pm.Texto, ir.Pais, ir.Cidade, Convert.ToInt32(pm.PaginaSelecionada));

                if (list.Count < 10)
                    pm.NumeroMaximoPagina = pm.PaginaSelecionada;
            }
            catch (ForaDoLimiteException fl)
            {
                list = fl.List;
                pm.NumeroMaximoPagina = pm.PaginaSelecionada = fl.Max;
            }

            TempData["txtSearch"] = ViewBag.PesquisarModel = pm;


            TimeSpan tf = DateTime.Now - dtIni;
            ViewBag.TempoBusca = tf.Seconds + "," + tf.Milliseconds;
            return View("Pesquisar", list);
        }

        [AllowAnonymous]
        public ActionResult Planos()
        {

            IList<Plano> lp = _consultasPlanos.GetPlanosAtivos();
            return View(lp);
        }

        [HttpPost]
        [AllowAnonymous]
        public void SetRegionIP(string pais, string cidade, string regiao)
        {
            IpRegiaoModel ir = new IpRegiaoModel()
            {
                Cidade = cidade,
                Pais = pais,
                EstadoCodigo = Convert.ToInt32(regiao)
            };
            Session["IpRegiaoModel"] = ir;
            return;
        }

        [AllowAnonymous]
        public ActionResult VerLoja()
        {
            return RedirectToAction("VerLoja", "Loja", new { id = 0 });
        }

        [HttpPost]
        [AllowAnonymous]
        public void SetFiltro(string filtro, string valor)
        {

        }

        [AllowAnonymous]
        public ActionResult _Index()
        {
            return PartialView("_Index");
        }

        [AllowAnonymous]
        public ActionResult _Sobre()
        {
            return PartialView("_Sobre");
        }

        [AllowAnonymous]
        public ActionResult Contato()
        {
            return View("Contato", new ContatoModel());
        }

        [AllowAnonymous]
        public ActionResult Enviar(ContatoModel contatoModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("Contato", contatoModel);
            }
            string _mailserver = ConfigurationManager.AppSettings["mailserver"];
            string _mailuser = ConfigurationManager.AppSettings["mailuser"];
            string _mailpassword = ConfigurationManager.AppSettings["mailpassword"];
            string _contatonome = ConfigurationManager.AppSettings["contatonome"];
            string _contatoendereco = ConfigurationManager.AppSettings["contatoendereco"];

            try
            {
                FAST.Utils.SendEmail mail = new FAST.Utils.SendEmail();
                mail.Subject = contatoModel.Assunto;
                mail.Body = contatoModel.Mensagem;
                mail.Host = _mailserver;
                mail.Port = 25;
                mail.AddTo(_contatoendereco, _contatonome); ;
                mail.SetFrom(contatoModel.Email, contatoModel.Nome);
                mail.SetCredentials(_mailuser, _mailpassword);
                mail.Send();
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Falha ao enviar o e-mail.");
                return PartialView("Contato", contatoModel);
            }
            ModelState.Clear();
            ViewBag.OperacaoSucesso = Sucesso.EmailEnviado;
            return PartialView("Contato", new ContatoModel());
        }


        [AllowAnonymous]
        public ActionResult ContatoAnuncioModal()
        {
            return View("ContatoAnuncioModal");
        }

        [AllowAnonymous]
        public ActionResult ContatoAnuncio()
        {
            ContatoModel cm = new ContatoModel();
            cm.Assunto = "-- Anuncie Aqui ---";
            return View("ContatoAnuncio", cm);
        }
        [AllowAnonymous]
        public ActionResult EnviarContatoAnuncio(ContatoModel contatoModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("ContatoAnuncio", contatoModel);
            }
            string _mailserver = ConfigurationManager.AppSettings["mailserver"];
            string _mailuser = ConfigurationManager.AppSettings["mailuser"];
            string _mailpassword = ConfigurationManager.AppSettings["mailpassword"];
            string _contatonome = ConfigurationManager.AppSettings["contatonome"];
            string _contatoendereco = ConfigurationManager.AppSettings["contatoendereco"];

            try
            {
                FAST.Utils.SendEmail mail = new FAST.Utils.SendEmail();
                mail.Subject = contatoModel.Assunto;
                mail.Body = contatoModel.Mensagem;
                mail.Host = _mailserver;
                mail.Port = 25;
                mail.AddTo(_contatoendereco, _contatonome); ;
                mail.SetFrom(contatoModel.Email, contatoModel.Nome);
                mail.SetCredentials(_mailuser, _mailpassword);
                mail.Send();
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Falha ao enviar o e-mail.");
                return PartialView("ContatoAnuncio", contatoModel);
            }
            ModelState.Clear();
            ViewBag.OperacaoSucesso = Sucesso.EmailEnviado;
            return PartialView("ContatoAnuncio", new ContatoModel());
        }

        [AllowAnonymous]
        public ActionResult IndexLoja(String lojaNome)
        {
            Loja loja = _consultasLojas.GetPorUrl(lojaNome);

            if (loja == null)
                return View("Error404");

            ViewBag.loja = loja;
            return View("Index");
        }

        [AllowAnonymous]
        public ActionResult Error404()
        {
            return View();
        }

        IConsultasProdutoVendedor _iConsultasProdutoVendedor;
        IConsultasPlanos _consultasPlanos;
        IConsultasLojas _consultasLojas;
    }
}
