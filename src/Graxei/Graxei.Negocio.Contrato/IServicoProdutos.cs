using FAST.Modelo;
using Graxei.Modelo;

namespace Graxei.Negocio.Contrato
{ 
    public interface IServicoProdutos : IServicoEntidades<Produto>
    {
        void PreSalvar(Produto produto);
        void PreAtualizar(Produto produto);

        long GetMax(string texto);
        System.Collections.Generic.IList<Produto> Get(string texto, long p);
    }
}
