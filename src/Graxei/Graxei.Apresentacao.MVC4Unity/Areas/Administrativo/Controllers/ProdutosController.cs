using Graxei.Modelo;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Graxei.Apresentacao.MVC4Unity.Models;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.ContratosDeDados.TinyTypes;
using System;
using Graxei.Transversais.Utilidades.Excecoes;
using Graxei.Transversais.ContratosDeDados.Listas;
using Graxei.Apresentacao.MVC4Unity.Areas.Teste.Models;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Controllers
{
    public class ProdutosController : Controller
    {

        public ProdutosController(IConsultasLojas consultasLojas, IConsultasListaLojas consultasListaLojas, IConsultasProdutos consultasProdutos, IConsultaListaProdutosLoja consultaListaProdutosLoja)
        {
            _consultasLojas = consultasLojas;
            _consultasListaLojas = consultasListaLojas;
            _consultasProdutos = consultasProdutos;
            _consultaListaProdutosLoja = consultaListaProdutosLoja;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListarLojas(int numeroPagina = 1, int tamanho = 10)
        {
            if (numeroPagina < 0)
            {
                numeroPagina = 0;
            }
                

            ListaLojas listaLojas = _consultasListaLojas.Get(numeroPagina, tamanho);
            listaLojas = new ListaLojas(listaLojas.Lista, new ListaTotalElementos(130), new ListaElementoAtual(numeroPagina));
            return View(listaLojas);
        }

        public ActionResult Listar(long idLoja)
        {
            return View(new PesquisaProdutoLojaModel { IdLoja = idLoja });
        }

        [HttpPost]
        public ActionResult Pesquisar(PesquisaProdutoLojaModel model)
        {
            try
            {
                ListaProdutosLoja produtos = _consultaListaProdutosLoja.Get(model.DescricaoProduto, model.IdLoja, 1, 10, 0);
                return View(produtos);
            }
            catch (ProdutoForaDoLimiteException e)
            {
                IList<Produto> produtos = e.List;
                return View(produtos);
            }
        }

        public ActionResult LinkPesquisar(string criterio, long idLoja, int pagina, int totalPaginas, int totalElementos)
        {
            try
            {
                ListaProdutosLoja produtos = _consultaListaProdutosLoja.Get(criterio, idLoja, 1, 10, 0);
                return View(produtos);
            }
            catch (ProdutoForaDoLimiteException e)
            {
                IList<Produto> produtos = e.List;
                return View(produtos);
            }
        }
        /*
        public ActionResult Novo()
        {
            return View();
        }

        public ActionResult Copiar()
        {
            return View();
        }

        public ActionResult Autocomplete(string term)
        {
            IList<Fabricante> fabs = null;
            if (Session[Constantes.Fabricantes] == null)
            {
                Session[Constantes.Fabricantes] = _servicoFabricantes.TodosNomes();
            }
            string[] nomes = ((IList<string>)Session[Constantes.Fabricantes]).ToArray();
            return Json(nomes, JsonRequestBehavior.AllowGet);
        }
  */
        private readonly IConsultasLojas _consultasLojas;
        private readonly IConsultasListaLojas _consultasListaLojas;
        private readonly IConsultasProdutos _consultasProdutos;
        private IConsultaListaProdutosLoja _consultaListaProdutosLoja;
    }
}