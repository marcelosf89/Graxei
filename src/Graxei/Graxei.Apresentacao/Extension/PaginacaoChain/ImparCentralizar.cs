using Graxei.Transversais.ContratosDeDados.TinyTypes;
using Graxei.Apresentacao.Extension.PaginacaoChain.LinkBuilderStrategy;

namespace Graxei.Apresentacao.Extension.PaginacaoChain
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
            bool ficarNoCentro = _elementoAtualLista.Valor <= (_totalPaginas - _meioLista) && _elementoAtualLista.Valor > _meioLista;
            return (impar && ficarNoCentro);
        }

        public override long GetPrimeiraPaginaGrupoAtual()
        {
            return _elementoAtualLista.Valor - _meioLista;
        }

        public override long GetUltimaPaginaGrupoAtual()
        {
            return _elementoAtualLista.Valor + _meioLista;
        }

        public override long GetElementoParaSubstituir()
        {
            return _meioLista + 1;
        }

        private int _meioLista;
    }
}