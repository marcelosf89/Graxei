using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;
using Graxei.Apresentacao.Extension;
using Graxei.Transversais.ContratosDeDados.TinyTypes;
using Graxei.Apresentacao.Extension.PaginacaoChain.LinkBuilderStrategy;

namespace Graxei.Apresentacao.Extension.PaginacaoChain
{
    public class UltimoGrupoElementos : AbstractPaginacao
    {
        public UltimoGrupoElementos(AjaxHelper ajaxHelper, TotalElementosLista listaTotalElementos, PaginaAtualLista listaElementoAtual, int maximoElementosPaginacao, ILinkBuilderStrategy linkBuilderStrategy)
            : base(listaTotalElementos, listaElementoAtual, maximoElementosPaginacao, linkBuilderStrategy)
        {
        }

        public override bool RegraAtende()
        {
            int meioLista = _quantidadeMaximaLinksPaginacaoPorVez / 2;
            long inicioUltimoGrupo = (_totalPaginas - _quantidadeMaximaLinksPaginacaoPorVez) + 1;
            return _elementoAtualLista.Valor > (inicioUltimoGrupo + meioLista);
        }

        public override long GetPrimeiraPaginaGrupoAtual()
        {
            return (_totalPaginas - _quantidadeMaximaLinksPaginacaoPorVez) + 1;
        }

        public override long GetUltimaPaginaGrupoAtual()
        {
            return _totalPaginas;
        }

        public override long GetElementoParaSubstituir()
        {
            return _elementoAtualLista.Valor - (_totalPaginas - _quantidadeMaximaLinksPaginacaoPorVez);
        }
    }
}