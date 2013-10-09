using FAST.Modelo;
using Graxei.Modelo;

namespace Graxei.Negocio.Contrato
{ 
    public interface IServicoProdutos : IServicoEntidades<Produto>
    {
        void PreSalvar(Produto produto);
        void PreAtualizar(Produto produto);
    }
}
