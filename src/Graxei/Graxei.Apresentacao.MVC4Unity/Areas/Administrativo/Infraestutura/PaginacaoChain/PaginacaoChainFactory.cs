using Graxei.Transversais.ContratosDeDados.Interfaces;
using Graxei.Transversais.ContratosDeDados.TinyTypes;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Infraestutura.PaginacaoChain
{
    public class PaginacaoChainFactory
    {

        public PaginacaoChainFactory(ListaTotalElementos listaTotalElementos, ListaElementoAtual listaElementoAtual, int maximoElementosPaginacao)
        {
            _listaTotalElementos = listaTotalElementos;
            _listaElementoAtual = listaElementoAtual;
            _maximoElementosPaginacao = maximoElementosPaginacao;
        }

        public IPaginacaoChain Get()
        {
            ////IPaginacaoChain primeiroElemento = new PrimeiroItem(_listaTotalElementos);
            ////IPaginacaoChain segundoElemento =
            ////primeiroElemento
            return null;
        }

        private ListaTotalElementos _listaTotalElementos;

        private ListaElementoAtual _listaElementoAtual;

        private int _maximoElementosPaginacao = 5;

    }
}