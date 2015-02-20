using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto.Visitor;
using Graxei.Persistencia.Implementacao.NHibernate;
using Graxei.Transversais.ContratosDeDados;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Implementacao.Teste
{
    [TestClass]
    public class TesteProdutoVendedorRepositorio
    {
        private Mock<IVisitorCriacaoFuncao> _visitorCriacaoFuncao;

        private Mock<IMudancaProdutoVendedorFuncaoFactory> _mudancaFactory;

        private Mock<IDbCommand> _mockDbCommand;

        private Mock<IMudancaProdutoVendedorFuncao> _mockFuncao;

        [TestInitialize]
        public void SetUp()
        {
            _visitorCriacaoFuncao = new Mock<IVisitorCriacaoFuncao>();
            _mudancaFactory = new Mock<IMudancaProdutoVendedorFuncaoFactory>();
            _mockDbCommand = new Mock<IDbCommand>();
            _mockFuncao = new Mock<IMudancaProdutoVendedorFuncao>();
            _mockFuncao.Setup(p => p.Aceitar(It.IsAny<IVisitorCriacaoFuncao>()));
        }

        [TestMethod]
        public void DeveRetornarSemAcaoQuandoAtualizarListaRecebeListaNula()
        {
            // Act
            ProdutoVendedorRepositorio produtoVendedorRepositorio = new ProdutoVendedorRepositorio(_visitorCriacaoFuncao.Object, _mudancaFactory.Object);
            produtoVendedorRepositorio.AtualizarLista(null);

            // Assert
            _mudancaFactory.Verify(p => p.GetComBaseEm(It.IsAny<IList<ProdutoLojaPrecoContrato>>()), Times.Never);

        }

        [TestMethod]
        public void DeveRetornarSemAcaoQuandoAtualizarListaRecebeListaVazia()
        {
            // Act
            ProdutoVendedorRepositorio produtoVendedorRepositorio = new ProdutoVendedorRepositorio(_visitorCriacaoFuncao.Object, _mudancaFactory.Object);
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

            Mock<ISession> sessao = SetupMocks();
            IList<IMudancaProdutoVendedorFuncao> listaFuncoes = new List<IMudancaProdutoVendedorFuncao>();
            listaFuncoes.Add(_mockFuncao.Object);
            _mudancaFactory.Setup(p => p.GetComBaseEm(It.IsAny<IList<ProdutoLojaPrecoContrato>>())).Returns(listaFuncoes);
            _visitorCriacaoFuncao.Setup(p => p.GetResultado()).Returns("sql");

            // Act
            ProdutoVendedorRepositorio produtoVendedorRepositorio = new ProdutoVendedorRepositorio(_visitorCriacaoFuncao.Object, _mudancaFactory.Object);
            produtoVendedorRepositorio.SetSessaoAtual(sessao.Object);
            produtoVendedorRepositorio.AtualizarLista(lista);
            
            // Assert
            _mockDbCommand.Verify(p => p.ExecuteReader(), Times.Once);
        }


        [TestMethod]
        public void QuandoAtualizarListaRecebeUmaListaNaoVaziaMasNenhumElementoEValido()
        {
            // Arrange
            ProdutoLojaPrecoContrato produtoLojaPrecoContrato = new ProdutoLojaPrecoContrato();
            List<ProdutoLojaPrecoContrato> lista = new List<ProdutoLojaPrecoContrato>();
            lista.Add(produtoLojaPrecoContrato);

            Mock<ISession> sessao = SetupMocks();
            IList<IMudancaProdutoVendedorFuncao> listaFuncoes = new List<IMudancaProdutoVendedorFuncao>();
            listaFuncoes.Add(_mockFuncao.Object);
            _mudancaFactory.Setup(p => p.GetComBaseEm(It.IsAny<IList<ProdutoLojaPrecoContrato>>())).Returns(listaFuncoes);
            _visitorCriacaoFuncao.Setup(p => p.GetResultado()).Returns(string.Empty);

            // Act
            ProdutoVendedorRepositorio produtoVendedorRepositorio = new ProdutoVendedorRepositorio(_visitorCriacaoFuncao.Object, _mudancaFactory.Object);
            produtoVendedorRepositorio.SetSessaoAtual(sessao.Object);
            produtoVendedorRepositorio.AtualizarLista(lista);

            // Assert
            _visitorCriacaoFuncao.Verify(p => p.GetResultado(), Times.Once);
            _mockDbCommand.Verify(p => p.ExecuteReader(), Times.Never);
        }
        private Mock<ISession> SetupMocks()
        {
            Mock<IDbConnection> mockDbConnection = new Mock<IDbConnection>();
            Mock<IDataReader> mockDataReader = new Mock<IDataReader>();
            Mock<ISession> mockSessao = new Mock<ISession>();
            mockSessao.Setup(p => p.CreateSQLQuery(It.IsAny<string>()));
            mockSessao.Setup(p => p.Connection).Returns(mockDbConnection.Object);
            mockDbConnection.Setup(p => p.CreateCommand()).Returns(_mockDbCommand.Object);
            _mockDbCommand.Setup(p => p.ExecuteReader()).Returns(mockDataReader.Object);
            return mockSessao;
        }
    }
}
