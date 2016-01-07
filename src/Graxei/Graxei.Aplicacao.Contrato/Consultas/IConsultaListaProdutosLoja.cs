using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.ContratosDeDados.Listas;

namespace Graxei.Aplicacao.Contrato.Consultas
{
    public interface IConsultaListaProdutosLoja
    {
        ListaProdutosLoja Get(PesquisaProdutoContrato pesquisaProdutoContrato, int tamanhoPagina);
    }
}
