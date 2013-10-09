using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioProdutoVendedor : IRepositorioIrrestrito<ProdutoVendedor>
    {
        IList<ProdutoVendedor> GetPorDescricao(string descricao);
        ProdutoVendedor GetPorDescricaoAndLoja(string descricao, string nomeLoja);
        ProdutoVendedor GetPorDescricaoAndLoja(string descricao, Loja loja);
    }

}
