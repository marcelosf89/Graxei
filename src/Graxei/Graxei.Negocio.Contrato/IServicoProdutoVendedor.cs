using Graxei.Modelo;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.ContratosDeDados.Listas;
using System.Collections.Generic;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoProdutoVendedor : IEntidadesIrrestrito<ProdutoVendedor>
    {
        ListaPesquisaContrato Get(string texto);

        ListaPesquisaContrato Get(string texto, int pagina);

        IList<ProdutoLojaPrecoContrato> AtualizarLista(IList<ProdutoLojaPrecoContrato> produtoLojaPrecoContrato);

        ListaPesquisaContrato GetUltimaPagina(string criterio);

        long GetQuantidadeProduto();

        long GetQuantidadeProduto(long lojaId);
    }
}
