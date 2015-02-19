using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto.Visitor;
using Graxei.Persistencia.Implementacao.NHibernate;
using Graxei.Transversais.ContratosDeDados;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHibernate;
using System;
using System.Collections.Generic;
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

        [TestInitialize]
        public void SetUp()
        {
            _visitorCriacaoFuncao = new Mock<IVisitorCriacaoFuncao>();
            _mudancaFactory = new Mock<IMudancaProdutoVendedorFuncaoFactory>();
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

            Mock<IMudancaProdutoVendedorFuncao> mockFuncao = new Mock<IMudancaProdutoVendedorFuncao>();
            mockFuncao.Setup(p => p.Aceitar(It.IsAny<IVisitorCriacaoFuncao>()));

            IList<IMudancaProdutoVendedorFuncao> listaFuncoes = new List<IMudancaProdutoVendedorFuncao>();
            listaFuncoes.Add(mockFuncao.Object);
            _mudancaFactory.Setup(p => p.GetComBaseEm(It.IsAny<IList<ProdutoLojaPrecoContrato>>())).Returns(listaFuncoes);

            // Act
            Mock<ISession> sessao = new Mock<ISession>();
            sessao.Setup(p => p.CreateSQLQuery(It.IsAny<string>()));
            ProdutoVendedorRepositorio produtoVendedorRepositorio = new ProdutoVendedorRepositorio(_visitorCriacaoFuncao.Object, _mudancaFactory.Object);
            produtoVendedorRepositorio.SetSessaoAtual(sessao.Object);
            produtoVendedorRepositorio.AtualizarLista(lista);
            

            // Assert
            _visitorCriacaoFuncao.Verify(p => p. GetResultado(), Times.Once);
        }
    }
}
