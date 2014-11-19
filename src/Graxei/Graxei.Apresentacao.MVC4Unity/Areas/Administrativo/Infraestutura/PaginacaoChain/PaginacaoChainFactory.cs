using System.Web.Mvc;
using System.Web.Routing;
using Graxei.Transversais.ContratosDeDados.Interfaces;
using Graxei.Transversais.ContratosDeDados.TinyTypes;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Infraestutura.PaginacaoChain
{
    public class PaginacaoChainFactory
    {

        public PaginacaoChainFactory(AjaxHelper ajaxHelper, ListaTotalElementos listaTotalElementos, ListaElementoAtual listaElementoAtual, int maximoElementosPaginacao, RouteValueDictionary rota)
        {
            _listaTotalElementos = listaTotalElementos;
            _listaElementoAtual = listaElementoAtual;
            _maximoElementosPaginacao = maximoElementosPaginacao;
            _rota = rota;
            _ajaxHelper = ajaxHelper;
        }

        public IPaginacaoChain ConstruirCadeiaDePaginacao()
        {
            IPaginacaoChain primeiroElemento = new ImparMenosQueMaximoElementos(_ajaxHelper, _listaTotalElementos, _listaElementoAtual, _maximoElementosPaginacao, _rota);
            IPaginacaoChain segundoElemento = new UltimoGrupoElementos(_ajaxHelper, _listaTotalElementos, _listaElementoAtual, _maximoElementosPaginacao, _rota);
            primeiroElemento.SetProximoElemento(segundoElemento);
            IPaginacaoChain terceiroElemento = new ElementosPaginacaoImpar(_ajaxHelper, _listaTotalElementos, _listaElementoAtual, _maximoElementosPaginacao, _rota);
            segundoElemento.SetProximoElemento(terceiroElemento);
            return primeiroElemento;
        }

        private ListaTotalElementos _listaTotalElementos;

        private ListaElementoAtual _listaElementoAtual;

        private int _maximoElementosPaginacao = 5;

        private RouteValueDictionary _rota;

        private AjaxHelper _ajaxHelper;
    }
}