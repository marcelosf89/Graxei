using Graxei.Transversais.ContratosDeDados.Listas;
using Graxei.Transversais.ContratosDeDados.TinyTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Implementacao.Teste
{
    public class RepositorioCommon
    {
        public static List<ListaProdutosLojaContrato> GetDoisElementos()
        {
            List<ListaProdutosLojaContrato> retorno =
                new List<ListaProdutosLojaContrato> { new ListaProdutosLojaContrato { Id = 1, Descricao = "Produto 2", Preco = 101 } };
            retorno.AddRange(GetUmElemento());
            return retorno;
        }

        public static List<ListaProdutosLojaContrato> GetUmElemento()
        {
            return new List<ListaProdutosLojaContrato> { new ListaProdutosLojaContrato { Id = 1, Descricao = "Produto 1", Preco = 99 } };
        }

        public static ListaProdutosLoja Construir(List<ListaProdutosLojaContrato> lista, int total, int atual)
        {
            ListaTotalElementos listaTotalElementos = new ListaTotalElementos(total);
            ListaElementoAtual listaElementoAtual = new ListaElementoAtual(atual);
            return new ListaProdutosLoja(lista, listaTotalElementos, listaElementoAtual);
        }
    }
}
