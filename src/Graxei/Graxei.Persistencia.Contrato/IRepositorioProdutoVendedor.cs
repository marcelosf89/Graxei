using System.Collections.Generic;
using Graxei.Modelo;
using Graxei.Transversais.ContratosDeDados;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioProdutoVendedor : IRepositorioIrrestrito<ProdutoVendedor>
    {
        IList<ProdutoVendedor> GetPorDescricao(string descricao);
        
        ProdutoVendedor GetPorDescricaoAndLoja(string descricao, string nomeLoja);
        
        ProdutoVendedor GetPorDescricaoAndLoja(string descricao, Loja loja);

        IList<ProdutoLojaPrecoContrato> AtualizarLista(IList<ProdutoLojaPrecoContrato> produtoLojaPrecoContratos);

        void ExcluirDe(Loja loja);

        long GetQuantidadeProduto(Usuario usuario);

        long GetQuantidadeProduto(long lojaId);
    }

}
