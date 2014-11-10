using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioProdutoVendedor : IRepositorioIrrestrito<ProdutoVendedor>
    {
        IList<ProdutoVendedor> GetPorDescricao(string descricao);
        IList<ProdutoVendedor> GetPorDescricaoPesquisa(string descricao, string pais, string cidade, int page);        
        ProdutoVendedor GetPorDescricaoAndLoja(string descricao, string nomeLoja);
        ProdutoVendedor GetPorDescricaoAndLoja(string descricao, Loja loja);
        void ExcluirDe(Loja loja);

        long GetMaxPorDescricaoPesquisa(string texto, string pais, string cidade, int page);
    }

}
