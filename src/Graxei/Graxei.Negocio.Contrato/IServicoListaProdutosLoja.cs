using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.ContratosDeDados.Listas;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoListaProdutosLoja
    {
        ListaProdutosLoja Get(PesquisaProdutoContrato pesquisaProdutoContrato, int tamanhoPagina);
    }
}
