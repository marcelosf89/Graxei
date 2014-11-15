using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.ContratosDeDados.TinyTypes;
using Graxei.Transversais.Idiomas;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Infraestutura.PaginacaoChain
{
    public class MaisQueMaximoElementos : IPaginacaoChain
    {
        public MaisQueMaximoElementos(ListaTotalElementos listaTotalElementos, ListaElementoAtual listaElementoAtual, int maximoElementosPaginacao)
        {
            _listaTotalElementos = listaTotalElementos;
            _listaElementoAtual = listaElementoAtual;
            _maximoElementosPaginacao = maximoElementosPaginacao;
        }

        public MvcHtmlString Get()
        {
            return null;
        }

        public IPaginacaoChain ProximoElemento { get; private set; }

        public void SetProximoElemento(IPaginacaoChain paginacaoChain)
        {
            ProximoElemento = paginacaoChain;
        }

        private ListaTotalElementos _listaTotalElementos;
        private ListaElementoAtual _listaElementoAtual;
        private int _maximoElementosPaginacao;
    }
}