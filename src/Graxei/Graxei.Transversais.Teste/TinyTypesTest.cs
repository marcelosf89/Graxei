using Graxei.Transversais.ContratosDeDados.Listas;
using Graxei.Transversais.ContratosDeDados.TinyTypes;
using Graxei.Transversais.Utilidades.Autenticacao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Transversais.Teste
{
    [TestClass]
    public class TinyTypesTest
    {
        [TestMethod]
        public void ListaElementoAtual_Equals_FalseQuandoObjetoComparadoNull()
        {
            // Act
            ListaElementoAtual listaElementoAtualPrincipal = new ListaElementoAtual(1);

            // Assert
            Assert.IsFalse(listaElementoAtualPrincipal.Equals(null));
        }

        [TestMethod]
        public void ListaElementoAtual_Equals_FalseQuandoObjetoComparadoNaoForDoTipoListaElementoAtual()
        {
            // Act
            ListaElementoAtual listaElementoAtualPrincipal = new ListaElementoAtual(1);

            // Assert
            Assert.IsFalse(listaElementoAtualPrincipal.Equals(new GerenciadorAutenticacaoSessaoHttp()));
        }

        [TestMethod]
        public void ListaElementoAtual_Equals_FalseQuandoValoresDeAtualForemDiferentes()
        {
            // Act
            ListaElementoAtual listaElementoAtualPrincipal = new ListaElementoAtual(1);
            ListaElementoAtual listaElementoAtualComparar = new ListaElementoAtual(2);

            // Assert
            Assert.AreNotEqual(listaElementoAtualPrincipal, listaElementoAtualComparar);
        }

        [TestMethod]
        public void ListaElementoAtual_Equals_TrueQuandoValoresDeAtualForemIguais()
        {
            // Act
            ListaElementoAtual listaElementoAtualPrincipal = new ListaElementoAtual(99);
            ListaElementoAtual listaElementoAtualComparar = new ListaElementoAtual(99);

            // Assert
            Assert.AreEqual(listaElementoAtualPrincipal, listaElementoAtualComparar);
        }

        [TestMethod]
        public void ListaTotalElementos_Equals_FalseQuandoObjetoComparadoNull()
        {
            // Act
            ListaTotalElementos listaTotalElementosPrincipal = new ListaTotalElementos(1);

            // Assert
            Assert.IsFalse(listaTotalElementosPrincipal.Equals(null));
        }

        [TestMethod]
        public void ListaTotalElementos_Equals_FalseQuandoObjetoComparadoNaoForDoTipoListaTotalElementos()
        {
            // Act
            ListaTotalElementos listaTotalElementosPrincipal = new ListaTotalElementos(1);

            // Assert
            Assert.IsFalse(listaTotalElementosPrincipal.Equals(new GerenciadorAutenticacaoSessaoHttp()));
        }

        [TestMethod]
        public void ListaTotalElementos_Equals_FalseQuandoValoresDeTotalForemDiferentes()
        {
            // Act
            ListaTotalElementos listaTotalElementosPrincipal = new ListaTotalElementos(1);
            ListaTotalElementos listaTotalElementosComparar = new ListaTotalElementos(2);

            // Assert
            Assert.AreNotEqual(listaTotalElementosPrincipal, listaTotalElementosComparar);
        }

        [TestMethod]
        public void ListaTotalElementos_Equals_TrueQuandoValoresDeTotalForemIguais()
        {
            // Act
            ListaTotalElementos listaTotalElementosPrincipal = new ListaTotalElementos(99);
            ListaTotalElementos listaTotalElementosComparar = new ListaTotalElementos(99);

            // Assert
            Assert.AreEqual(listaTotalElementosPrincipal, listaTotalElementosComparar);
        }
    }
}