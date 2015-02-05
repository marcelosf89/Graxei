using Graxei.Transversais.ContratosDeDados.Listas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Aplicacao.Contrato.Consultas
{
    public interface IConsultaListaProdutosLoja
    {
        ListaProdutosLoja Get(string criterio, bool meusProdutos, long idLoja, int pagina, int tamanhoPagina, long totalElementos);
    }
}
