using System.Collections.Generic;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Transversais.ContratosDeDados;

namespace Graxei.Aplicacao.Contrato.Transacionais
{
    public interface IGerenciamentoProdutos :ITransacional
    {
        void Salvar(ProdutoVendedor produtoVendedor);
        void Excluir(ProdutoVendedor produtoVendedor);
        IList<ProdutoLojaPrecoContrato> SalvarLista(IList<ProdutoLojaPrecoContrato> produtoLojaPrecoContrato);
        IServicoProdutoVendedor Servico { get; }
    }
}
