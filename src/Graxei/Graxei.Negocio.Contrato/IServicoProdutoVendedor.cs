using Graxei.Modelo;
using Graxei.Transversais.ContratosDeDados;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoProdutoVendedor : IEntidadesIrrestrito<ProdutoVendedor>
    {
        System.Collections.Generic.IList<PesquisaContrato> Get(string texto);

        System.Collections.Generic.IList<PesquisaContrato> Get(string texto, string pais, string cidade, int page);

        long GetMax(string texto, string pais, string cidade, int page);

        long GetQuantidadeProduto();

        long GetQuantidadeProduto(long lojaId);
    }
}
