using Graxei.Transversais.ContratosDeDados;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Transversais.Teste
{
    [TestClass]
    public class TesteProdutoLojaPrecoContrato
    {
        [TestMethod]
        public void DeveTrazerInserirQuandoIdMeuProdutoForZeroEIdProdutoPrecoIdEnderecoForMaiorQueZero()
        {
            // Arrange
            OperacaoProdutoLoja esperado = OperacaoProdutoLoja.Incluir;
            ProdutoLojaPrecoContrato produtoLojaPrecoContrato = new ProdutoLojaPrecoContrato { IdProduto = 1, IdEndereco = 10, IdMeuProduto = 0 , Preco = 10 };

            // Act
            OperacaoProdutoLoja real = produtoLojaPrecoContrato.OperacaoNoContrato;

            //
            Assert.AreEqual(esperado, real);
        }

        [TestMethod]
        public void DeveTrazerExcluirQuandoPrecoMenorQueZero()
        {
            // Arrange
            OperacaoProdutoLoja esperado = OperacaoProdutoLoja.Excluir;
            ProdutoLojaPrecoContrato produtoLojaPrecoContrato = new ProdutoLojaPrecoContrato { Preco = -1 };

            // Act
            OperacaoProdutoLoja real = produtoLojaPrecoContrato.OperacaoNoContrato;

            //
            Assert.AreEqual(esperado, real);
        }

        [TestMethod]
        public void DeveTrazerExcluirQuandoPrecoIgualAZero()
        {
            // Arrange
            OperacaoProdutoLoja esperado = OperacaoProdutoLoja.Excluir;
            ProdutoLojaPrecoContrato produtoLojaPrecoContrato = new ProdutoLojaPrecoContrato { Preco = 0 };

            // Act
            OperacaoProdutoLoja real = produtoLojaPrecoContrato.OperacaoNoContrato;

            // Assert
            Assert.AreEqual(esperado, real);
        }

        [TestMethod]
        public void DeveTrazerAlterarQuandoPrecoMaiorQueZeroEIdMeuProdutoMaiorQueZero()
        {
            // Arrange
            OperacaoProdutoLoja esperado = OperacaoProdutoLoja.Alterar;
            ProdutoLojaPrecoContrato produtoLojaPrecoContrato = new ProdutoLojaPrecoContrato { Preco = 2, IdMeuProduto = 10 };

            // Act
            OperacaoProdutoLoja real = produtoLojaPrecoContrato.OperacaoNoContrato;

            // Assert
            Assert.AreEqual(esperado, real);
        }

        [TestMethod]
        public void DeveTrazerInvalidoQuandoPrecoMaiorQueZeroIdProdutoIgualZeroIdMeuProdutoIgualZero()
        {
            // Arrange
            OperacaoProdutoLoja esperado = OperacaoProdutoLoja.Invalido;
            ProdutoLojaPrecoContrato produtoLojaPrecoContrato = new ProdutoLojaPrecoContrato { Preco = 1, IdMeuProduto = 0, IdProduto = 0 };

            // Act
            OperacaoProdutoLoja real = produtoLojaPrecoContrato.OperacaoNoContrato;
            
            // Assert
            Assert.AreEqual(esperado, real);
        }

        [TestMethod]
        public void DeveTrazerInvalidoQuandoPrecoMaiorQueZeroIdMeuProdutoIgualZeroIdProdutoIgualZeroIdEnderecoMaiorQueZero()
        {
            // Arrange
            OperacaoProdutoLoja esperado = OperacaoProdutoLoja.Invalido;
            ProdutoLojaPrecoContrato produtoLojaPrecoContrato = new ProdutoLojaPrecoContrato { Preco = 1, IdMeuProduto = 0, IdProduto = 0, IdEndereco = 15 };

            // Act
            OperacaoProdutoLoja real = produtoLojaPrecoContrato.OperacaoNoContrato;

            // Assert
            Assert.AreEqual(esperado, real);
        }

        [TestMethod]
        public void DeveTrazerInvalidoQuandoPrecoMaiorQueZeroIdMeuProdutoIgualZeroIdEnderecoIgualZeroIdProdutoMaiorQueZero()
        {
            // Arrange
            OperacaoProdutoLoja esperado = OperacaoProdutoLoja.Invalido;
            ProdutoLojaPrecoContrato produtoLojaPrecoContrato = new ProdutoLojaPrecoContrato { Preco = 1, IdMeuProduto = 0, IdProduto = 16, IdEndereco = 0 };

            // Act
            OperacaoProdutoLoja real = produtoLojaPrecoContrato.OperacaoNoContrato;

            // Assert
            Assert.AreEqual(esperado, real);
        }
    }
}
