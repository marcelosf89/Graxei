using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto.Visitor;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.SqlNativo;
using Graxei.Persistencia.Implementacao.NHibernate;
using Graxei.Transversais.ContratosDeDados;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Graxei.Persistencia.Implementacao.Teste
{
    [TestClass]
    public class TesteProdutoVendedorRepositorio
    {
        private Mock<IVisitorCriacaoFuncao> _visitorCriacaoFuncao;

        private Mock<IMudancaProdutoVendedorFuncaoFactory> _mudancaFactory;

        private Mock<IMudancaProdutoVendedorFuncao> _mockFuncao;

        private Mock<IProdutoVendedorNativo> _mockProdutoVendedorNativo;

        [TestInitialize]
        public void SetUp()
        {
            _visitorCriacaoFuncao = new Mock<IVisitorCriacaoFuncao>();
            _mudancaFactory = new Mock<IMudancaProdutoVendedorFuncaoFactory>();
            _mockFuncao = new Mock<IMudancaProdutoVendedorFuncao>();
            _mockFuncao.Setup(p => p.Aceitar(It.IsAny<IVisitorCriacaoFuncao>()));
            _mockProdutoVendedorNativo = new Mock<IProdutoVendedorNativo>();
        }

        [TestMethod]
        public void DeveRetornarSemAcaoQuandoAtualizarListaRecebeListaNula()
        {
            // Act
            ProdutoVendedorRepositorio produtoVendedorRepositorio = new ProdutoVendedorRepositorio(_visitorCriacaoFuncao.Object, _mudancaFactory.Object, _mockProdutoVendedorNativo.Object);
            produtoVendedorRepositorio.AtualizarLista(null);

            // Assert
            _mudancaFactory.Verify(p => p.GetComBaseEm(It.IsAny<IList<ProdutoLojaPrecoContrato>>()), Times.Never);

        }

        [TestMethod]
        public void DeveRetornarSemAcaoQuandoAtualizarListaRecebeListaVazia()
        {
            // Act
            ProdutoVendedorRepositorio produtoVendedorRepositorio = new ProdutoVendedorRepositorio(_visitorCriacaoFuncao.Object, _mudancaFactory.Object, _mockProdutoVendedorNativo.Object);
            produtoVendedorRepositorio.AtualizarLista(new List<ProdutoLojaPrecoContrato>());

            // Assert
            _mudancaFactory.Verify(p => p.GetComBaseEm(It.IsAny<IList<ProdutoLojaPrecoContrato>>()), Times.Never);

        }

        [TestMethod]
        public void DeveVerificarAcaoFeitaQuandoAtualizarListaRecebeUmaListaNaoVazia()
        {
            // Arrange
            ProdutoLojaPrecoContrato produtoLojaPrecoContrato = new ProdutoLojaPrecoContrato();
            List<ProdutoLojaPrecoContrato> lista = new List<ProdutoLojaPrecoContrato>();
            lista.Add(produtoLojaPrecoContrato);

            IList<IMudancaProdutoVendedorFuncao> listaFuncoes = new List<IMudancaProdutoVendedorFuncao>();
            listaFuncoes.Add(_mockFuncao.Object);
            _mockProdutoVendedorNativo.Setup(p => p.Get(It.IsAny<string>())).Returns(lista);
            _mudancaFactory.Setup(p => p.GetComBaseEm(It.IsAny<IList<ProdutoLojaPrecoContrato>>())).Returns(listaFuncoes);
            _visitorCriacaoFuncao.Setup(p => p.GetResultado()).Returns("sql");

            // Act
            ProdutoVendedorRepositorio produtoVendedorRepositorio = new ProdutoVendedorRepositorio(_visitorCriacaoFuncao.Object, _mudancaFactory.Object, _mockProdutoVendedorNativo.Object);
            IList<ProdutoLojaPrecoContrato> real = produtoVendedorRepositorio.AtualizarLista(lista);
            
            // Assert
            Assert.AreEqual(lista.Count, real.Count);
        }


        [TestMethod]
        public void QuandoAtualizarListaRecebeUmaListaNaoVaziaMasNenhumElementoEValido()
        {
            // Arrange
            ProdutoLojaPrecoContrato produtoLojaPrecoContrato = new ProdutoLojaPrecoContrato();
            List<ProdutoLojaPrecoContrato> lista = new List<ProdutoLojaPrecoContrato>();
            lista.Add(produtoLojaPrecoContrato);

            IList<IMudancaProdutoVendedorFuncao> listaFuncoes = new List<IMudancaProdutoVendedorFuncao>();
            listaFuncoes.Add(_mockFuncao.Object);
            _mudancaFactory.Setup(p => p.GetComBaseEm(It.IsAny<IList<ProdutoLojaPrecoContrato>>())).Returns(listaFuncoes);
            _visitorCriacaoFuncao.Setup(p => p.GetResultado()).Returns(string.Empty);

            // Act
            ProdutoVendedorRepositorio produtoVendedorRepositorio = new ProdutoVendedorRepositorio(_visitorCriacaoFuncao.Object, _mudancaFactory.Object, _mockProdutoVendedorNativo.Object);
            produtoVendedorRepositorio.AtualizarLista(lista);

            // Assert
            _visitorCriacaoFuncao.Verify(p => p.GetResultado(), Times.Once);
        }

 
    }
}
