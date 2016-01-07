using Graxei.Transversais.ContratosDeDados.Listas;

namespace Graxei.Aplicacao.Contrato.Consultas
{
    public interface IConsultasProdutoVendedor
    {
        ListaPesquisaContrato Get(string texto);

        ListaPesquisaContrato Get(string txtSearch, string pais, string cidade, int page, string ip);

        long GetQuantidadeProduto();

        long GetQuantidadeProduto(long lojaId);
    }
}