using System.Web.Mvc;
using System.Web.Routing;
using Graxei.Transversais.ContratosDeDados.Interfaces;
using Graxei.Transversais.ContratosDeDados.TinyTypes;
using Graxei.Apresentacao.Extension.PaginacaoChain.LinkBuilderStrategy;

namespace Graxei.Apresentacao.Extension.PaginacaoChain
{
    public class PaginacaoChainFactory
    {

        public PaginacaoChainFactory(AjaxHelper ajaxHelper, TotalElementosLista listaTotalElementos, PaginaAtualLista listaElementoAtual, int maximoElementosPaginacao, string controller, string action, ILinkBuilderStrategy linkBuilderStrategy)
        {
            _listaTotalElementos = listaTotalElementos;
            _listaElementoAtual = listaElementoAtual;
            _maximoElementosPaginacao = maximoElementosPaginacao;
            _controller = controller;
            _action = action;
            _ajaxHelper = ajaxHelper;
            _linkBuilderStrategy = linkBuilderStrategy;
        }

        public IPaginacaoChain ConstruirCadeiaDePaginacao()
        {
            IPaginacaoChain primeiroElemento = new ImparMenosQueMaximoElementos(_ajaxHelper, _listaTotalElementos, _listaElementoAtual, _maximoElementosPaginacao, _linkBuilderStrategy);
            IPaginacaoChain segundoElemento = new UltimoGrupoElementos(_ajaxHelper, _listaTotalElementos, _listaElementoAtual, _maximoElementosPaginacao, _linkBuilderStrategy);
            primeiroElemento.SetProximoElemento(segundoElemento);
            IPaginacaoChain terceiroElemento = new ImparCentralizar(_listaTotalElementos, _listaElementoAtual, _maximoElementosPaginacao, _linkBuilderStrategy);
            segundoElemento.SetProximoElemento(terceiroElemento);
            return primeiroElemento;
        }

        private TotalElementosLista _listaTotalElementos;

        private PaginaAtualLista _listaElementoAtual;

        private int _maximoElementosPaginacao = 5;

        private string _controller;

        private string _action;

        private AjaxHelper _ajaxHelper;

        private ILinkBuilderStrategy _linkBuilderStrategy;
    }
}