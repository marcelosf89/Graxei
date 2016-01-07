using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.ContratosDeDados.Listas;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioListaProdutosLoja
    {
        ListaProdutosLoja GetSomenteUmEndereco(PesquisaProdutoContrato pesquisaProdutoContrato, int tamanhoPagina);
    }
}
