using Graxei.Transversais.ContratosDeDados;
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
        ListaProdutosLoja GetSomenteUmEndereco(PesquisaProdutoContrato pesquisaProdutoContrato, int tamanhoPagina);
    }
}
