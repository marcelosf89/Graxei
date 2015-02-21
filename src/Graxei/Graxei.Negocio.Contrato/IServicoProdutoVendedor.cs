using Graxei.Modelo;
using Graxei.Transversais.ContratosDeDados;
using System.Collections.Generic;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoProdutoVendedor : IEntidadesIrrestrito<ProdutoVendedor>
    {
        IList<PesquisaContrato> Get(string texto);

        IList<PesquisaContrato> Get(string texto, string pais, string cidade, int page);

        IList<ProdutoLojaPrecoContrato> AtualizarLista(IList<ProdutoLojaPrecoContrato> produtoLojaPrecoContrato);

        long GetMax(string texto, string pais, string cidade, int page);

        long GetQuantidadeProduto();

        long GetQuantidadeProduto(long lojaId);
    }
}
