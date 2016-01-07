using Graxei.Transversais.ContratosDeDados.TinyTypes;
using Graxei.Transversais.Comum.Autenticacao;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graxei.Transversais.Teste
{
    [TestClass]
    public class TinyTypesTest
    {
        [TestMethod]
        public void ListaElementoAtual_Equals_FalseQuandoObjetoComparadoNull()
        {
            // Act
            PaginaAtualLista listaElementoAtualPrincipal = new PaginaAtualLista(1);

            // Assert
            Assert.IsFalse(listaElementoAtualPrincipal.Equals(null));
        }

        [TestMethod]
        public void ListaElementoAtual_Equals_FalseQuandoObjetoComparadoNaoForDoTipoListaElementoAtual()
        {
            // Act
            PaginaAtualLista listaElementoAtualPrincipal = new PaginaAtualLista(1);

            // Assert
            Assert.IsFalse(listaElementoAtualPrincipal.Equals(new GerenciadorAutenticacaoSessaoHttp()));
        }

        [TestMethod]
        public void ListaElementoAtual_Equals_FalseQuandoValoresDeAtualForemDiferentes()
        {
            // Act
            PaginaAtualLista listaElementoAtualPrincipal = new PaginaAtualLista(1);
            PaginaAtualLista listaElementoAtualComparar = new PaginaAtualLista(2);

            // Assert
            Assert.AreNotEqual(listaElementoAtualPrincipal, listaElementoAtualComparar);
        }

        [TestMethod]
        public void ListaElementoAtual_Equals_TrueQuandoValoresDeAtualForemIguais()
        {
            // Act
            PaginaAtualLista listaElementoAtualPrincipal = new PaginaAtualLista(99);
            PaginaAtualLista listaElementoAtualComparar = new PaginaAtualLista(99);

            // Assert
            Assert.AreEqual(listaElementoAtualPrincipal, listaElementoAtualComparar);
        }

        [TestMethod]
        public void ListaTotalElementos_Equals_FalseQuandoObjetoComparadoNull()
        {
            // Act
            TotalElementosLista listaTotalElementosPrincipal = new TotalElementosLista(1);

            // Assert
            Assert.IsFalse(listaTotalElementosPrincipal.Equals(null));
        }

        [TestMethod]
        public void ListaTotalElementos_Equals_FalseQuandoObjetoComparadoNaoForDoTipoListaTotalElementos()
        {
            // Act
            TotalElementosLista listaTotalElementosPrincipal = new TotalElementosLista(1);

            // Assert
            Assert.IsFalse(listaTotalElementosPrincipal.Equals(new GerenciadorAutenticacaoSessaoHttp()));
        }

        [TestMethod]
        public void ListaTotalElementos_Equals_FalseQuandoValoresDeTotalForemDiferentes()
        {
            // Act
            TotalElementosLista listaTotalElementosPrincipal = new TotalElementosLista(1);
            TotalElementosLista listaTotalElementosComparar = new TotalElementosLista(2);

            // Assert
            Assert.AreNotEqual(listaTotalElementosPrincipal, listaTotalElementosComparar);
        }

        [TestMethod]
        public void ListaTotalElementos_Equals_TrueQuandoValoresDeTotalForemIguais()
        {
            // Act
            TotalElementosLista listaTotalElementosPrincipal = new TotalElementosLista(99);
            TotalElementosLista listaTotalElementosComparar = new TotalElementosLista(99);

            // Assert
            Assert.AreEqual(listaTotalElementosPrincipal, listaTotalElementosComparar);
        }
    }
}