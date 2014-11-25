using Graxei.Modelo;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioProdutos : IRepositorioEntidades<Produto>
    {
        Produto GetPorDescricao(string descricao);

        long GetMax(string texto);
        System.Collections.Generic.IList<Produto> Get(string texto, long page);
    }
}
