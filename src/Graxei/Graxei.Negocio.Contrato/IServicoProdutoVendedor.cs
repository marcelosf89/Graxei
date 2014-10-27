using Graxei.Modelo;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoProdutoVendedor : IEntidadesIrrestrito<ProdutoVendedor>
    {
        System.Collections.Generic.IList<ProdutoVendedor> Get(string texto);
    }
}
