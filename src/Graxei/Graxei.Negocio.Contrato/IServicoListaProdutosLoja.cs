using Graxei.Transversais.ContratosDeDados.Listas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoListaProdutosLoja
    {
        ListaProdutosLoja Get(string criterio, long idLoja, int pagina, int tamanhoPagina, int totalElementos);
    }
}
