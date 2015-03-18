using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;
using Graxei.Apresentacao.MVC4Unity.Extension;
using Graxei.Transversais.ContratosDeDados.TinyTypes;
using Graxei.Apresentacao.MVC4Unity.Extension.PaginacaoChain.LinkBuilderStrategy;

namespace Graxei.Apresentacao.MVC4Unity.Extension.PaginacaoChain
{
    public class ImparCentralizar : AbstractPaginacao
    {
        public ImparCentralizar(TotalElementosLista listaTotalElementos, PaginaAtualLista listaElementoAtual, int maximoElementosPaginacao, ILinkBuilderStrategy linkBuilderStrategy)
            : base(listaTotalElementos, listaElementoAtual, maximoElementosPaginacao, linkBuilderStrategy)
        {
            _meioLista = (_quantidadeMaximaLinksPaginacaoPorVez / 2);
        }

        public override bool RegraAtende()
        {
            bool impar = (_quantidadeMaximaLinksPaginacaoPorVez%2 != 0);
            bool ficarNoCentro = _elementoAtualLista.Atual <= (_totalPaginas - _meioLista) && _elementoAtualLista.Atual > _meioLista;
            return (impar && ficarNoCentro);
        }

        public override long GetPrimeiraPaginaGrupoAtual()
        {
            return _elementoAtualLista.Atual - _meioLista;
        }

        public override long GetUltimaPaginaGrupoAtual()
        {
            return _elementoAtualLista.Atual + _meioLista;
        }

        public override long GetElementoParaSubstituir()
        {
            return _meioLista + 1;
        }

        private int _meioLista;
    }
}