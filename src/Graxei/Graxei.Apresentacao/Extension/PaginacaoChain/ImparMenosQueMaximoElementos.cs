﻿using System.Web.Mvc;
using Graxei.Transversais.ContratosDeDados.TinyTypes;
using Graxei.Apresentacao.Extension.PaginacaoChain.LinkBuilderStrategy;

namespace Graxei.Apresentacao.Extension.PaginacaoChain
{
    public class ImparMenosQueMaximoElementos : AbstractPaginacao
    {
        public ImparMenosQueMaximoElementos(AjaxHelper ajaxHelper, TotalElementosLista listaTotalElementos, PaginaAtualLista listaElementoAtual, int maximoElementosPaginacao, ILinkBuilderStrategy linkBuilderStrategy)
            : base(listaTotalElementos, listaElementoAtual, maximoElementosPaginacao, linkBuilderStrategy)
        {
        }

        public override bool RegraAtende()
        {
            bool impar = (_quantidadeMaximaLinksPaginacaoPorVez % 2 != 0);
            int meioLista = _quantidadeMaximaLinksPaginacaoPorVez / 2 + 1;
            bool menosQueMaximoElementos = _elementoAtualLista.Valor < _quantidadeMaximaLinksPaginacaoPorVez && _elementoAtualLista.Valor <= meioLista;
            return impar && menosQueMaximoElementos;
        }

        public override long GetPrimeiraPaginaGrupoAtual()
        {
            return 1;
        }

        public override long GetUltimaPaginaGrupoAtual()
        {
            return _quantidadeMaximaLinksPaginacaoPorVez > _totalPaginas
                    ? _totalPaginas
                    : _quantidadeMaximaLinksPaginacaoPorVez;
        }

        public override long GetElementoParaSubstituir()
        {
            return _elementoAtualLista.Valor;
        }

    }
}