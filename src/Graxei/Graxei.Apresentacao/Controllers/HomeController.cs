using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Aplicacao.Contrato.Operacoes;
using Graxei.Aplicacao.Implementacao.Operacoes;
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
        [AllowAnonymous]
        public ActionResult Index(string q)
        {
            if (q != null)
            {
                ViewBag.q = q;
            }
            return View("");
        }

        [AjaxGenericException]
        [AllowAnonymous]
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
            ViewBag.TempoBusca = string.Format("{0} segundos", stopWatch.Elapsed.ToString(@"ss\,fff"));
            ViewBag.newq = q;
            return View(pesquisarModel);
        }

        [AllowAnonymous]
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
            ViewBag.TempoBusca = string.Format("{0} segundos", stopWatch.Elapsed.ToString(@"ss\,fff"));
            return View("Pesquisar", listaPesquisaContrato.Lista);
        }

        private string GetQuerySearch(string q, string lojaNome)
        {
            if (!String.IsNullOrEmpty(lojaNome) && !q.ToLower().Contains("loja:" + lojaNome))
            {
                int idxOfLoja = q.ToLower().IndexOf("loja:");
                if (idxOfLoja >= 0)
                {
                    int nIdxOf = q.Substring(idxOfLoja).IndexOf(' ');

                    string loja = q.Substring(idxOfLoja + 5);
                    if (nIdxOf > 0)
                        loja = q.Substring(idxOfLoja + 5, nIdxOf - 5);

                    q = q.Replace("loja:" + lojaNome, " loja:" + loja);
                    lojaNome = loja;
                }
                else
                {
                    q = q + " loja:" + lojaNome;
                }
            }
            return q;
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
            IpRegiaoModel ipRegiaoModel = new IpRegiaoModel()
            {
                Cidade = cidade,
                Pais = pais,
                EstadoCodigo = Convert.ToInt32(regiao)
            };
            _cacheComum.IpRegiaoModel = ipRegiaoModel;
            return;
        }

        [AllowAnonymous]
        public ActionResult VerLoja()
        {
            return RedirectToAction("VerLoja", "Loja", new { id = 0 });
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

        [AllowAnonymous]
        public ActionResult IndexLoja(String lojaNome, String q)
        {
            Loja loja = _consultasLojas.GetPorUrl(lojaNome);

            if (loja == null)
            {
                return View("Error404");
            }

            ViewBag.loja = loja;

            if (!String.IsNullOrEmpty(q))
            {
                q = GetQuerySearch(q, lojaNome);
                ViewBag.q = q;
            }
            return View("Index");

        }

        [AllowAnonymous]
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

        [AllowAnonymous]
        public ActionResult Error404()
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
