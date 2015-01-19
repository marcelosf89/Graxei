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
        ListaProdutosLoja GetSomenteUmEndereco(string criterio, bool meusProdutos, long idLoja, int pagina, int tamanhoPagina, long totalElementos);
        ListaProdutosLoja GetSomenteUmEndereco(string criterio, bool meusProdutos, long idLoja, int pagina, int tamanhoPagina);
    }
}
