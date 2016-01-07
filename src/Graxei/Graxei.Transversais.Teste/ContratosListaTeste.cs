using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.ContratosDeDados.Listas;
using Graxei.Transversais.ContratosDeDados.TinyTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Graxei.Transversais.Teste
{
    [TestClass]
    public class ContratosListaTeste
    {
        [TestMethod]
        public void ListaProdutosLoja_ThatListaNaoEListaProdutosLoja_RetornaFalso()
        {
            // Arrange/Act
            ListaProdutosLoja thisListaProdutosLoja = new ListaProdutosLoja(new List<ListaProdutosLojaContrato>(), null, null);
            ListaProdutosLoja thatListaProdutosLoja = new ListaProdutosLoja(null, null, null);

            // Assert
            Assert.AreNotEqual(thisListaProdutosLoja, new LojaContrato());
        }

        [TestMethod]
        public void ListaProdutosLoja_ThatENulo_RetornaFalso()
        {
            // Arrange/Act
            ListaProdutosLoja thisListaProdutosLoja = new ListaProdutosLoja(new List<ListaProdutosLojaContrato>(), null, null);
            ListaProdutosLoja thatListaProdutosLoja = new ListaProdutosLoja(null, null, null);

            // Assert
            Assert.AreNotEqual(thisListaProdutosLoja, null);
        }

        [TestMethod]
        public void ListaProdutosLoja_ThatListaProdutoComMesmoNumeroDeElementosMasDiferencaEntreEles_RetornaFalso()
        {
            // Arrange
            ListaProdutosLojaContrato listaProdutosLojaContrato1 = new ListaProdutosLojaContrato { Id = 1, DescricaoOriginal = "Produto 1", Preco = 99 };
            ListaProdutosLojaContrato listaProdutosLojaContrato2 = new ListaProdutosLojaContrato { Id = 2, DescricaoOriginal = "Produto X", Preco = 99 };
            List<ListaProdutosLojaContrato> lista1 = new List<ListaProdutosLojaContrato>();
            lista1.Add(listaProdutosLojaContrato1);
            List<ListaProdutosLojaContrato> lista2 = new List<ListaProdutosLojaContrato>();
            lista2.Add(listaProdutosLojaContrato2);

            ListaProdutosLoja thisListaProdutosLoja = new ListaProdutosLoja(lista1, null, null);
            ListaProdutosLoja thatListaProdutosLoja = new ListaProdutosLoja(lista2, null, null);

            // Assert
            Assert.AreNotEqual(thisListaProdutosLoja, thatListaProdutosLoja);
        }

        [TestMethod]
        public void ListaProdutosLoja_ThatListaProdutoComMaisElementos_RetornaFalso()
        {
            // Arrange
            ListaProdutosLojaContrato listaProdutosLojaContrato1 = new ListaProdutosLojaContrato { Id = 1, DescricaoOriginal = "Produto 1", Preco = 99 };
            ListaProdutosLojaContrato listaProdutosLojaContrato2 = new ListaProdutosLojaContrato { Id = 2, DescricaoOriginal = "Produto X", Preco = 99 };
            ListaProdutosLojaContrato listaProdutosLojaContrato3 = new ListaProdutosLojaContrato { Id = 3, DescricaoOriginal = "Produto N", Preco = 101 };
            List<ListaProdutosLojaContrato> lista1 = new List<ListaProdutosLojaContrato>();
            lista1.Add(listaProdutosLojaContrato1);
            List<ListaProdutosLojaContrato> lista2 = new List<ListaProdutosLojaContrato>();
            lista2.Add(listaProdutosLojaContrato2);
            lista2.Add(listaProdutosLojaContrato3);

            ListaProdutosLoja thisListaProdutosLoja = new ListaProdutosLoja(lista1, null, null);
            ListaProdutosLoja thatListaProdutosLoja = new ListaProdutosLoja(lista2, null, null);

            // Assert
            Assert.AreNotEqual(thisListaProdutosLoja, thatListaProdutosLoja);
        }

        [TestMethod]
        public void ListaProdutosLoja_ListaTotalElementosDiferente_RetornaFalso()
        {
            // Arrange/Act
            ListaProdutosLoja thisListaProdutosLoja = new ListaProdutosLoja(null, new TotalElementosLista(1), null);
            ListaProdutosLoja thatListaProdutosLoja = new ListaProdutosLoja(null, new TotalElementosLista(2), null);

            // Assert
            Assert.AreNotEqual(thisListaProdutosLoja, thatListaProdutosLoja);
            Assert.AreNotEqual(thisListaProdutosLoja.Total, thatListaProdutosLoja.Total);
            
        }

        [TestMethod]
        public void ListaProdutosLoja_ListaElementoAtualDiferente_RetornaFalso()
        {
            // Arrange/Act
            ListaProdutosLoja thisListaProdutosLoja = new ListaProdutosLoja(null, new TotalElementosLista(9), new PaginaAtualLista(5));
            ListaProdutosLoja thatListaProdutosLoja = new ListaProdutosLoja(null, new TotalElementosLista(9), new PaginaAtualLista(7));

            // Assert
            Assert.AreNotEqual(thisListaProdutosLoja, thatListaProdutosLoja);
            Assert.AreNotEqual(thisListaProdutosLoja.Atual, thatListaProdutosLoja.Atual);
        }

        [TestMethod]
        public void ListaProdutosLoja_TudoIgual_RetornaVerdadeiro()
        {
            // Arrange
            ListaProdutosLojaContrato listaProdutosLojaContrato1 = new ListaProdutosLojaContrato { Id = 1, DescricaoOriginal = "Produto 1", Preco = 99 };
            ListaProdutosLojaContrato listaProdutosLojaContrato2 = new ListaProdutosLojaContrato { Id = 1, DescricaoOriginal = "Produto 1", Preco = 99 };
            List<ListaProdutosLojaContrato> lista1 = new List<ListaProdutosLojaContrato>();
            lista1.Add(listaProdutosLojaContrato1);
            List<ListaProdutosLojaContrato> lista2 = new List<ListaProdutosLojaContrato>();
            lista2.Add(listaProdutosLojaContrato2);

            // Act
            ListaProdutosLoja thisListaProdutosLoja = new ListaProdutosLoja(lista1, new TotalElementosLista(9), new PaginaAtualLista(5));
            ListaProdutosLoja thatListaProdutosLoja = new ListaProdutosLoja(lista2, new TotalElementosLista(9), new PaginaAtualLista(5));

            // Assert
            Assert.AreEqual(thisListaProdutosLoja, thatListaProdutosLoja);
        }
    }
}
