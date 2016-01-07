using Graxei.Modelo;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoAtributos : IServicoEntidades<Atributo>
    {
        void PreSalvar(ProdutoVendedor produtoVendedor);
        void PreAtualizar(ProdutoVendedor produtoVendedor);
    }
}
