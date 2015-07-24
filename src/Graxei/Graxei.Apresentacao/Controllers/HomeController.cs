using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Aplicacao.Contrato.Operacoes;
using Graxei.Apresentacao.Infrastructure.Cache;
using Graxei.Apresentacao.Infrastructure.Filters;
using Graxei.Apresentacao.Models;
using Graxei.Modelo;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Comum.Excecoes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;
using Graxei.Transversais.Comum.SectionGroups;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Converters;
using Graxei.Transversais.ContratosDeDados.Api.PesquisaProdutos;
using System.Text;
using System.Diagnostics;
using Graxei.Transversais.ContratosDeDados.Listas;

namespace Graxei.Apresentacao.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public HomeController(IConsultasProdutoVendedor consultaVendedor, IConsultasPlanos consultasPlanos, IConsultasLojas consultasLojas, IGerenciamentoMensageria gerenciamentoMensageria, ICacheComum cacheComum, IConsultaFabricantes appConsultasFabricantes)
        {
            _appConsultasFabricantes = appConsultasFabricantes;
            _consultasProdutoVendedor = consultaVendedor;
            _consultasPlanos = consultasPlanos;
            _consultasLojas = consultasLojas;
            _gerenciamentoMensageria = gerenciamentoMensageria;
            _cacheComum = cacheComum;
        }

        //
        // GET: /Home/
        public ActionResult Index(string q)
        {
            if (q != null)
            {
                ViewBag.q = q;
            }
            return View("");
        }

        [AjaxGenericException]
        public ActionResult Pesquisar(string q, string lojaNome)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();

            IpRegiaoModel ipRegiaoModel = _cacheComum.IpRegiaoModel;
            ListaPesquisaContrato listaPesquisaContrato = _consultasProdutoVendedor.Get(q, ipRegiaoModel.Pais, ipRegiaoModel.Cidade, 0, ControllerContext.HttpContext.Request.UserHostAddress);

            PesquisarModel pesquisarModel = new PesquisarModel
            {
                Texto = q,
                PaginaSelecionada = 0
            };

            if (listaPesquisaContrato.Lista.Count < 10)
            {
                pesquisarModel.NumeroMaximoPagina = 0;
            }
            else
            {
                pesquisarModel.NumeroMaximoPagina = null;
            }

            TempData["txtSearch"] = ViewBag.PesquisarModel = pesquisarModel;
            pesquisarModel.PesquisaContrato = listaPesquisaContrato.Lista;

            stopWatch.Stop();
            ViewBag.TempoBusca = string.Format("{0} segundos", stopWatch.Elapsed.ToString(@"s\,fff"));
            ViewBag.newq = q;
            return View(pesquisarModel);
        }

        public ActionResult PesquisarPagina(string paginaSelecionada)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();

            PesquisarModel pesquisarModel = (PesquisarModel)TempData["txtSearch"];
            pesquisarModel.PaginaSelecionada = Convert.ToInt32(paginaSelecionada);

            IpRegiaoModel ipRegiaoModel = _cacheComum.IpRegiaoModel;
            ListaPesquisaContrato listaPesquisaContrato = _consultasProdutoVendedor.Get(pesquisarModel.Texto, ipRegiaoModel.Pais, ipRegiaoModel.Cidade, Convert.ToInt32(pesquisarModel.PaginaSelecionada), ControllerContext.HttpContext.Request.UserHostAddress);

            if (listaPesquisaContrato.Lista.Count < 10)
            {
                pesquisarModel.NumeroMaximoPagina = pesquisarModel.PaginaSelecionada;
            } else if (listaPesquisaContrato.Total.Valor > 0)
            {
                pesquisarModel.NumeroMaximoPagina = pesquisarModel.PaginaSelecionada = listaPesquisaContrato.Total.Valor;
            }

            TempData["txtSearch"] = ViewBag.PesquisarModel = pesquisarModel;
            stopWatch.Stop();
            ViewBag.TempoBusca = string.Format("{0} segundos", stopWatch.Elapsed.ToString(@"s\,fff"));
            return View("Pesquisar", listaPesquisaContrato.Lista);
        }

        public ActionResult Planos()
        {
            IList<Plano> lp = _consultasPlanos.GetPlanosAtivos();
            return View(lp);
        }

        [HttpPost]
        public void SetRegionIP(string pais, string cidade, string regiao)
        {
            IpRegiaoModel ipRegiaoModel = new IpRegiaoModel()
            {
                Cidade = cidade,
                Pais = pais,
                EstadoCodigo = Convert.ToInt32(regiao)
            };
            _cacheComum.IpRegiaoModel = ipRegiaoModel;
            return;
        }

        public ActionResult VerLoja()
        {
            return RedirectToAction("VerLoja", "Loja", new { id = 0 });
        }

        public ActionResult _Index()
        {
            return PartialView("_Index");
        }

        public ActionResult _Sobre()
        {
            return PartialView("_Sobre");
        }

        public ActionResult Contato()
        {
            return View("Contato", new ContatoModel());
        }

        public ActionResult Enviar(ContatoModel contatoModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("Contato", contatoModel);
            }

            try
            {
                Mensagem mensagem = new Mensagem(contatoModel.Assunto, contatoModel.Mensagem, contatoModel.Nome, contatoModel.Email);
                ConfiguracaoMail configuracaoMail = new ConfiguracaoMail(ConfigurationManager.AppSettings["contaservidormail"], ConfigurationManager.AppSettings["senhaservidormail"], ConfigurationManager.AppSettings["enderecoservidormail"], int.Parse(ConfigurationManager.AppSettings["portaservidormail"]), ConfigurationManager.AppSettings["contatonome"], ConfigurationManager.AppSettings["contatoendereco"], bool.Parse(ConfigurationManager.AppSettings["habilitarSSL"]));
                _gerenciamentoMensageria.Enviar(mensagem, configuracaoMail);
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

        public ActionResult ContatoAnuncioModal()
        {
            return View("ContatoAnuncioModal");
        }

        public ActionResult ContatoAnuncio()
        {
            ContatoModel cm = new ContatoModel();
            cm.Assunto = "-- Anuncie Aqui ---";
            return View("ContatoAnuncio", cm);
        }

        public ActionResult EnviarContatoAnuncio(ContatoModel contatoModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("ContatoAnuncio", contatoModel);
            }

            try
            {
                Mensagem mensagem = new Mensagem(contatoModel.Assunto, contatoModel.Mensagem, contatoModel.Nome, contatoModel.Email);
                ConfiguracaoMail configuracaoMail = new ConfiguracaoMail(ConfigurationManager.AppSettings["contaservidormail"], ConfigurationManager.AppSettings["senhaservidormail"], ConfigurationManager.AppSettings["enderecoservidormail"], int.Parse(ConfigurationManager.AppSettings["portaservidormail"]), ConfigurationManager.AppSettings["contatonome"], ConfigurationManager.AppSettings["contatoendereco"], bool.Parse(ConfigurationManager.AppSettings["habilitarSSL"]));
                _gerenciamentoMensageria.Enviar(mensagem, configuracaoMail);
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

        public FileResult GetThumbnail(int idProduto = 0)
        {
            if (idProduto != 0)
            {
                String caminhoImagem = ConfigurationManager.AppSettings["imagesPath"];
                byte[] file = _appConsultasFabricantes.GetThumbnail(idProduto, caminhoImagem);
                if (file != null)
                {
                    return File(file, "image/jpeg");
                }
            }

            return null;
        }

        public ActionResult Error404()
        {
            return View();
        }

        public ActionResult ModalEnderecoAngular()
        {
            return View();
        }

        private IConsultaFabricantes _appConsultasFabricantes;
        private IConsultasProdutoVendedor _consultasProdutoVendedor;
        private IConsultasPlanos _consultasPlanos;
        private IConsultasLojas _consultasLojas;
        private IGerenciamentoMensageria _gerenciamentoMensageria;
        private ICacheComum _cacheComum;

        public PesquisarModel pesquisaModel { get; set; }
    }
}
