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
        public void DeveTrazerEnumeradorInserirQuandoIdMeuProdutoForZeroEIdProdutoForMaiorQueZeroEPrecoForMaiorQueZero()
        {
            // Arrange
            OperacaoProdutoLoja esperado = OperacaoProdutoLoja.Incluir;
            ProdutoLojaPrecoContrato produtoLojaPrecoContrato = new ProdutoLojaPrecoContrato { IdProduto = 1, IdMeuProduto = 0 , Preco = 10 };

            // Act
            OperacaoProdutoLoja real = produtoLojaPrecoContrato.OperacaoNoContrato;

            //
            Assert.AreEqual(esperado, real);
        }

        [TestMethod]
        public void DeveTrazerEnumeradorExcluirQuandoPrecoForMenorQueZero()
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
        public void DeveTrazerEnumeradorExcluirQuandoPrecoForIgualAZero()
        {
            // Arrange
            OperacaoProdutoLoja esperado = OperacaoProdutoLoja.Excluir;
            ProdutoLojaPrecoContrato produtoLojaPrecoContrato = new ProdutoLojaPrecoContrato { Preco = 0 };

            // Act
            OperacaoProdutoLoja real = produtoLojaPrecoContrato.OperacaoNoContrato;

            //
            Assert.AreEqual(esperado, real);
        }

        [TestMethod]
        public void DeveTrazerEnumeradorAlterarQuandoPrecoForMaiorQueZeroEIdMeuProdutoForMaiorQueZero()
        {
            // Arrange
            OperacaoProdutoLoja esperado = OperacaoProdutoLoja.Alterar;
            ProdutoLojaPrecoContrato produtoLojaPrecoContrato = new ProdutoLojaPrecoContrato { Preco = 2, IdMeuProduto = 10 };

            // Act
            OperacaoProdutoLoja real = produtoLojaPrecoContrato.OperacaoNoContrato;

            //
            Assert.AreEqual(esperado, real);
        }
    }
}
