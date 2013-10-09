using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioAtributos : IRepositorioEntidades<Atributo>
    {
        IList<Atributo> Todos(string descricaoProduto, string nomeLoja);
        IList<Atributo> Todos(ProdutoVendedor produtoVendedor);
    }
}
