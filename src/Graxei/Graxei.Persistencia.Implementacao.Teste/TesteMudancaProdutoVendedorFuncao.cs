using Graxei.Modelo;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.Comum.Autenticacao.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Graxei.Persistencia.Implementacao.Teste
{
    [TestClass]
    public class TesteMudancaProdutoVendedorFuncao
    {
        private Mock<IGerenciadorAutenticacao> _gerenciadorAutenticacao;

        [TestInitialize]
        public void SetUp()
        {
            _gerenciadorAutenticacao = new Mock<IGerenciadorAutenticacao>();
            Usuario usuario = new Usuario { Id = 1 };
            _gerenciadorAutenticacao.Setup(p => p.Get()).Returns(usuario);
        }
        
        [TestMethod]
        public void QuandoProdutoPrecoContratoForIncluirDeveRetornarCriarProdutoVendedor()
        {
            // Arrange
            ProdutoLojaPrecoContrato produtoLoja = new ProdutoLojaPrecoContrato{ Id = 10, MinhaDescricao = "Teste de Produto", IdEndereco = 100, Preco = 190 };
            
            // Act
            CriarProdutoVendedor criarProdutoVendedor = (CriarProdutoVendedor)new MudancaoProdutoVendedorFuncaoFactory(_gerenciadorAutenticacao.Object).GetComBaseEm(produtoLoja);

            // Assert
            Assert.IsTrue(Igual(criarProdutoVendedor, produtoLoja));
        }

        [TestMethod]
        public void QuandoProdutoPrecoContratoForAlterarDeveRetornarAlterarProdutoVendedor()
        {
            // Arrange
            ProdutoLojaPrecoContrato produtoLoja = new ProdutoLojaPrecoContrato { MinhaDescricao = "Teste de Produto", IdMeuProduto = 55, Preco = 190 };

            // Act
            AlterarProdutoVendedor alterarProdutoVendedor = (AlterarProdutoVendedor)new MudancaoProdutoVendedorFuncaoFactory(_gerenciadorAutenticacao.Object).GetComBaseEm(produtoLoja);

            // Assert
            Assert.IsTrue(Igual(alterarProdutoVendedor, produtoLoja));
        }

        [TestMethod]
        public void QuandoProdutoPrecoContratoForExcluirDeveRetornarExcluirProdutoVendedor()
        {
            // Arrange
            ProdutoLojaPrecoContrato produtoLoja = new ProdutoLojaPrecoContrato { IdMeuProduto = 55 };

            // Act
            ExcluirProdutoVendedor excluirProdutoVendedor = (ExcluirProdutoVendedor)new MudancaoProdutoVendedorFuncaoFactory(_gerenciadorAutenticacao.Object).GetComBaseEm(produtoLoja);

            // Assert
            Assert.IsTrue(Igual(excluirProdutoVendedor, produtoLoja));
        }

        [TestMethod]
        public void QuandoPegarListaPassandoArgumentoNuloDeveRetornarListaVazia()
        {
            // Act
            IList<IMudancaProdutoVendedorFuncao> resultado = new MudancaoProdutoVendedorFuncaoFactory(_gerenciadorAutenticacao.Object).GetComBaseEm(null as IList<ProdutoLojaPrecoContrato>);

            // Assert
            Assert.AreEqual(0, resultado.Count);
        }

        [TestMethod]
        public void QuandoPegarListaComUmProdutoDeveRetornarListaComUmProduto()
        {
            // Act
            ProdutoLojaPrecoContrato pl = new ProdutoLojaPrecoContrato { IdMeuProduto = 1, Preco = 10 };
            List<ProdutoLojaPrecoContrato> lista = new List<ProdutoLojaPrecoContrato>();
            lista.Add(pl);
            IList<IMudancaProdutoVendedorFuncao> resultado = new MudancaoProdutoVendedorFuncaoFactory(_gerenciadorAutenticacao.Object).GetComBaseEm(lista);

            // Assert
            Assert.AreEqual(1, resultado.Count);
            Assert.IsInstanceOfType(resultado[0], typeof(AlterarProdutoVendedor));
        }

        [TestMethod]
        public void QuandoPegarListaComTresProdutoSendoUmInvalidoDeveRetornarListaComDoisProdutos()
        {
            // Arrange
            ProdutoLojaPrecoContrato pl = new ProdutoLojaPrecoContrato { Id = 1, Preco = 10 };
            List<ProdutoLojaPrecoContrato> lista = new List<ProdutoLojaPrecoContrato>();
            lista.Add(pl);
            pl = new ProdutoLojaPrecoContrato { IdMeuProduto = 15, Preco = 33 };
            lista.Add(pl);
            pl = new ProdutoLojaPrecoContrato { IdMeuProduto = 10, Preco = 15 };
            lista.Add(pl);

            // Act
            IList<IMudancaProdutoVendedorFuncao> resultado = new MudancaoProdutoVendedorFuncaoFactory(_gerenciadorAutenticacao.Object).GetComBaseEm(lista);

            // Assert
            Assert.AreEqual(2, resultado.Count);
            Assert.IsInstanceOfType(resultado[1], typeof(AlterarProdutoVendedor));
        }

        [TestMethod]
        public void QuandoPegarListaComTresProdutoValidosDeveRetornarListaComTresProdutos()
        {
            // Arrange
            ProdutoLojaPrecoContrato pl = new ProdutoLojaPrecoContrato { IdMeuProduto = 1 };
            List<ProdutoLojaPrecoContrato> lista = new List<ProdutoLojaPrecoContrato>();
            lista.Add(pl);
            pl = new ProdutoLojaPrecoContrato { IdMeuProduto = 15, Preco = 33 };
            lista.Add(pl);
            pl = new ProdutoLojaPrecoContrato { IdMeuProduto = 10, Preco = 15 };
            lista.Add(pl);

            // Act
            IList<IMudancaProdutoVendedorFuncao> resultado = new MudancaoProdutoVendedorFuncaoFactory(_gerenciadorAutenticacao.Object).GetComBaseEm(lista);

            // Assert
            Assert.AreEqual(3, resultado.Count);
            Assert.IsInstanceOfType(resultado[0], typeof(ExcluirProdutoVendedor));
        }

        private bool Igual(CriarProdutoVendedor criarProdutoVendedor, ProdutoLojaPrecoContrato produtoLojaPrecoContrato)
        {
            bool resultado = true;
            resultado &= criarProdutoVendedor.IdProduto == produtoLojaPrecoContrato.Id;
            resultado &= criarProdutoVendedor.DescricaoProdutoVendedor == produtoLojaPrecoContrato.MinhaDescricao;
            resultado &= criarProdutoVendedor.IdEndereco == produtoLojaPrecoContrato.IdEndereco;
            resultado &= criarProdutoVendedor.Preco == produtoLojaPrecoContrato.Preco;
            resultado &= criarProdutoVendedor.Usuario.Id == 1;
            return resultado;
        }

        private bool Igual(AlterarProdutoVendedor alterarProdutoVendedor, ProdutoLojaPrecoContrato produtoLojaPrecoContrato)
        {
            bool resultado = true;
            resultado &= alterarProdutoVendedor.DescricaoProdutoVendedor == produtoLojaPrecoContrato.MinhaDescricao;
            resultado &= alterarProdutoVendedor.IdProdutoVendedor == produtoLojaPrecoContrato.IdMeuProduto;
            resultado &= alterarProdutoVendedor.Preco == produtoLojaPrecoContrato.Preco;
            resultado &= alterarProdutoVendedor.Usuario.Id == 1;
            return resultado;
        }

        private bool Igual(ExcluirProdutoVendedor excluirProdutoVendedor, ProdutoLojaPrecoContrato produtoLojaPrecoContrato)
        {
            bool resultado = true;
            resultado &= excluirProdutoVendedor.IdProdutoVendedor == produtoLojaPrecoContrato.IdMeuProduto;
            resultado &= excluirProdutoVendedor.Usuario.Id == 1;
            return resultado;
        }
    }
}
