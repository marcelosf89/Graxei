using System.Web.Mvc;
using Graxei.Transversais.ContratosDeDados.TinyTypes;

namespace Graxei.Apresentacao.MVC4Unity.Extension.PaginacaoChain
{
    public class ImparMenosQueMaximoElementos : AbstractPaginacao
    {
        public ImparMenosQueMaximoElementos(AjaxHelper ajaxHelper, TotalElementosLista listaTotalElementos, PaginaAtualLista listaElementoAtual, int maximoElementosPaginacao, string controller, string action) : base(ajaxHelper, listaTotalElementos, listaElementoAtual, maximoElementosPaginacao, controller, action)
        {
        }

        public override bool RegraAtende()
        {
            bool impar = (_quantidadeMaximaLinksPaginacaoPorVez % 2 != 0);
            int meioLista = _quantidadeMaximaLinksPaginacaoPorVez / 2 + 1;
            bool menosQueMaximoElementos = _elementoAtualLista.Atual < _quantidadeMaximaLinksPaginacaoPorVez && _elementoAtualLista.Atual <= meioLista;
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
            return _elementoAtualLista.Atual - 1;
        }

    }
}