using Graxei.Transversais.ContratosDeDados.Listas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioListaProdutosLoja
    {
        ListaProdutosLoja GetSomenteUmEndereco(int idLoja, int pagina, int tamanhoPagina, int totalElementos);
        ListaProdutosLoja GetSomenteUmEndereco(int idLoja, int pagina, int tamanhoPagina);
    }
}
