using Graxei.Modelo;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.ContratosDeDados.Listas;
using System.Collections.Generic;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoProdutoVendedor : IEntidadesIrrestrito<ProdutoVendedor>
    {
        ListaPesquisaContrato Get(string texto);

        ListaPesquisaContrato Get(string texto, string pais, string cidade, int page);

        IList<ProdutoLojaPrecoContrato> AtualizarLista(IList<ProdutoLojaPrecoContrato> produtoLojaPrecoContrato);

        ListaPesquisaContrato GetUltimaPagina(string texto, string pais, string cidade);

        long GetQuantidadeProduto();

        long GetQuantidadeProduto(long lojaId);
    }
}
