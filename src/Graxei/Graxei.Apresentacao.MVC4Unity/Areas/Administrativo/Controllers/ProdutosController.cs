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

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Controllers
{
    public class ProdutosController : Controller
    {

        public ProdutosController(IConsultasLojas consultasLojas, IConsultasListaLojas consultasListaLojas, IConsultasProdutos consultasProdutos)
        {
            _consultasLojas = consultasLojas;
            _consultasListaLojas = consultasListaLojas;
            _consultasProdutos = consultasProdutos;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListarLojas(int numeroPagina = 0, int tamanho = 5)
        {
            if (numeroPagina < 0)
                numeroPagina = 0;

            ListaLojas listaLojas = _consultasListaLojas.Get(numeroPagina, tamanho);
            listaLojas = new ListaLojas(listaLojas.Lista, new ListaTotalElementos(130), new ListaElementoAtual(numeroPagina));
            return View(listaLojas);
        }

        public ActionResult ProdutosListar(long idLoja)
        {

            return View();
        }

        public ActionResult Pesquisar(String txtProduto, bool meusProdutos, long paginaSelecionada = 0)
        {
            PesquisarModel pm = (PesquisarModel)TempData["txtProduto"];
            if (pm == null || !String.IsNullOrEmpty(txtProduto))
            {
                pm = new PesquisarModel();
                pm.Texto = txtProduto;
            }

            try
            {
                IList<Produto> produtos = _consultasProdutos.Get(pm.Texto, paginaSelecionada);
                pm.PaginaSelecionada = paginaSelecionada;
                if (produtos.Count < 10)
                {
                    TempData["NumeroMaximoPagina_P"] = paginaSelecionada;
                    pm.NumeroMaximoPagina = paginaSelecionada;
                }

                return View(produtos);
            }
            catch (ProdutoForaDoLimiteException e)
            {
                pm.NumeroMaximoPagina = e.Max;
                pm.PaginaSelecionada = e.Max;
                IList<Produto> produtos = e.List;
                return View(produtos);
            }
            finally
            {
                ViewBag.PesquisarModel = pm;
                TempData["txtProduto"] = pm;
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
    }
}