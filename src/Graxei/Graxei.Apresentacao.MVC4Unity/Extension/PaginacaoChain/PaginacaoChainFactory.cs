using System.Web.Mvc;
using System.Web.Routing;
using Graxei.Transversais.ContratosDeDados.Interfaces;
using Graxei.Transversais.ContratosDeDados.TinyTypes;

namespace Graxei.Apresentacao.MVC4Unity.Extension.PaginacaoChain
{
    public class PaginacaoChainFactory
    {

        public PaginacaoChainFactory(AjaxHelper ajaxHelper, TotalElementosLista listaTotalElementos, PaginaAtualLista listaElementoAtual, int maximoElementosPaginacao, string controller, string action)
        {
            _listaTotalElementos = listaTotalElementos;
            _listaElementoAtual = listaElementoAtual;
            _maximoElementosPaginacao = maximoElementosPaginacao;
            _controller = controller;
            _action = action;
            _ajaxHelper = ajaxHelper;
        }

        public IPaginacaoChain ConstruirCadeiaDePaginacao()
        {
            IPaginacaoChain primeiroElemento = new ImparMenosQueMaximoElementos(_ajaxHelper, _listaTotalElementos, _listaElementoAtual, _maximoElementosPaginacao, _controller, _action);
            IPaginacaoChain segundoElemento = new UltimoGrupoElementos(_ajaxHelper, _listaTotalElementos, _listaElementoAtual, _maximoElementosPaginacao, _controller, _action);
            primeiroElemento.SetProximoElemento(segundoElemento);
            IPaginacaoChain terceiroElemento = new ImparCentralizar(_ajaxHelper, _listaTotalElementos, _listaElementoAtual, _maximoElementosPaginacao, _controller, _action);
            segundoElemento.SetProximoElemento(terceiroElemento);
            return primeiroElemento;
        }

        private TotalElementosLista _listaTotalElementos;

        private PaginaAtualLista _listaElementoAtual;

        private int _maximoElementosPaginacao = 5;

        private string _controller;

        private string _action;

        private AjaxHelper _ajaxHelper;
    }
}