using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.ContratosDeDados.Listas;
using System.Collections.Generic;

namespace Graxei.Persistencia.Contrato.PesquisaProduto
{
    public interface IPesquisaProdutoRepositorio
    {
        IList<PesquisaContrato> Get(int pagina);
        
        ListaPesquisaContrato GetUltimaPagina(int tamanhoPagina);
    }
}
