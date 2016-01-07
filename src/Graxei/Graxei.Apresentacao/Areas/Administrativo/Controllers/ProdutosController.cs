using Graxei.Modelo;
using System.Collections.Generic;
using System.Web.Mvc;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.ContratosDeDados.TinyTypes;
using System;
using Graxei.Transversais.Comum.Excecoes;
using Graxei.Transversais.ContratosDeDados.Listas;
using Graxei.Aplicacao.Contrato.Transacionais;
using Graxei.Transversais.Idiomas;
using Graxei.Apresentacao.Infrastructure.ActionResults;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Graxei.Apresentacao.Areas.Administrativo.Controllers
{
    public class ProdutosController : Controller
    {

        public ProdutosController(IConsultasLojas consultasLojas, IConsultasListaLojas consultasListaLojas, IConsultasProdutos consultasProdutos, IConsultaListaProdutosLoja consultaListaProdutosLoja, IGerenciamentoProdutos gerenciamentoProdutos)
        {
            _consultasLojas = consultasLojas;
            _consultasListaLojas = consultasListaLojas;
            _consultasProdutos = consultasProdutos;
            _consultaListaProdutosLoja = consultaListaProdutosLoja;
            _gerenciamentoProdutos = gerenciamentoProdutos;
        }

        public ActionResult ListarLojas(int numeroPagina = 1, int tamanho = 10)
        {
            if (numeroPagina < 0)
            {
                numeroPagina = 0;
            }
            
            ListaLojas listaLojas = _consultasListaLojas.Get(numeroPagina, tamanho);
            listaLojas = new ListaLojas(listaLojas.Lista, new TotalElementosLista(130), new PaginaAtualLista(numeroPagina));
            return View(listaLojas);
        }

        public ActionResult Listar(long idLoja)
        {
            long endereco = this._consultasLojas.GetIdDoUnicoEndereco(idLoja);
            return View(new PesquisaProdutoContrato { IdLoja = idLoja, IdUnicoEndereco = endereco });
        }

        [HttpPost]
        public ActionResult PesquisarDoEndereco(PesquisaProdutoContrato model)
        {
            try
            {
                if (model.PaginaAtualLista == 0)
                {
                    model.PaginaAtualLista = 1;
                }
                ListaProdutosLoja produtos = _consultaListaProdutosLoja.Get(model, 10);
                return View(produtos);
            }
            catch (ProdutoForaDoLimiteException e)
            {
                IList<Produto> produtos = e.List;
                return View(produtos);
            }
        }

        [HttpPost]
        public JsonNetResult PesquisarJson(PesquisaProdutoContrato model)
        {
            try
            {
                if (model.PaginaAtualLista == 0)
                {
                    model.PaginaAtualLista = 1;
                }
                ListaProdutosLoja produtos = _consultaListaProdutosLoja.Get(model, 10);
                return JsonNetResult.GetWithDefaultFormatting(produtos);
            }
            catch (ProdutoForaDoLimiteException e)
            {
                IList<Produto> produtos = e.List;
                return JsonNetResult.GetWithDefaultFormatting(produtos);
            }
        }

        [HttpPost]
        public ActionResult Pesquisar(PesquisaProdutoContrato model)
        {
            string json = JsonConvert.SerializeObject(model, new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            return PartialView("Pesquisar", json);
            //try
            //{
            //    if (model.PaginaAtualLista == 0)
            //    {
            //        model.PaginaAtualLista = 1;
            //    }
            //    ListaProdutosLoja produtos = _consultaListaProdutosLoja.Get(model, 10);
            //    return JsonNetResult.GetWithDefaultFormatting(produtos);
            //}
            //catch (ProdutoForaDoLimiteException e)
            //{
            //    IList<Produto> produtos = e.List;
            //    return JsonNetResult.GetWithDefaultFormatting(produtos);
            //}
        }

        [HttpPost]
        public JsonResult Salvar(IList<ProdutoLojaPrecoContrato> itens)
        {
            IList<ProdutoLojaPrecoContrato> saida = null;
            try
            {
                saida =_gerenciamentoProdutos.SalvarLista(itens);
            }
            catch (Exception)
            {
                return Json(new { Sucesso = false, Mensagem = Erros.ListaNaoAtualizada }); ;
            }

            return Json(new { Sucesso = true, Mensagem = Sucesso.ListaAtualizada, ProdutosIncluidos = saida });
        }

        private readonly IConsultasLojas _consultasLojas;
        private readonly IConsultasListaLojas _consultasListaLojas;
        private readonly IConsultasProdutos _consultasProdutos;
        private readonly IGerenciamentoProdutos _gerenciamentoProdutos;
        private IConsultaListaProdutosLoja _consultaListaProdutosLoja;
    }
}