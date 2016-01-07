using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.SqlResolver;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.ContratosDeDados.Listas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHibernate;
using NHibernate.Transform;
using System.Collections.Generic;

namespace Graxei.Persistencia.Implementacao.Teste.Postgres
{
    [TestClass]
    public class SqlResolvers
    {
        private Mock<ISession> _mockSession = new Mock<ISession>();

        [TestInitialize]
        public void SetUp()
        {
            _mockSession = new Mock<ISession>();
        }

        [TestMethod]
        public void DeveTrazerListaCompletaParaListaProdutosCompletoResolver()
        {
            // Arrange
            List<ListaProdutosLojaContrato> expectedLista = RepositorioCommon.GetDoisElementos();
            SetupMockSessionQueryOverListar(expectedLista);
            PesquisaProdutoContrato pesquisaProdutoContrato = new PesquisaProdutoContrato { IdLoja = It.IsAny<long>(), DescricaoProduto = "criterio" };

            // Act
            ListaProdutosLojaCompletoResolver listaProdutosLojaCompletoResolver = new ListaProdutosLojaCompletoResolver(pesquisaProdutoContrato);
            listaProdutosLojaCompletoResolver.SessaoAtual = _mockSession.Object;
            IList<ListaProdutosLojaContrato> actualLista = listaProdutosLojaCompletoResolver.Get(It.IsAny<int>(), It.IsAny<int>());

            // Assert
            Assert.AreEqual(expectedLista, actualLista);
        }

        [TestMethod]
        public void DeveTrazerListaCompletaParaListaSomenteMeusProdutosResolver()
        {
            // Arrange
            List<ListaProdutosLojaContrato> expectedLista = RepositorioCommon.GetDoisElementos();
            SetupMockSessionQueryOverListar(expectedLista);
            PesquisaProdutoContrato pesquisaProdutoContrato = new PesquisaProdutoContrato { IdLoja = It.IsAny<long>(), DescricaoProduto = "criterio" };

            // Act
            ListaProdutosLojaMeusProdutosResolver listaProdutosLojaMeusProdutosResolver = new ListaProdutosLojaMeusProdutosResolver(pesquisaProdutoContrato);
            listaProdutosLojaMeusProdutosResolver.SessaoAtual = _mockSession.Object;
            IList<ListaProdutosLojaContrato> actualLista = listaProdutosLojaMeusProdutosResolver.Get(It.IsAny<int>(), It.IsAny<int>());

            // Assert
            Assert.AreEqual(expectedLista, actualLista);
        }

        [TestMethod]
        public void DeveTrazerContagemDeElementosParaListaProdutosCompletoResolver()
        {
            // Arrange
            long expectedTotalElementos = 9;
            SetupMockSessionQueryOverTotal(expectedTotalElementos);
            PesquisaProdutoContrato pesquisaProdutoContrato = new PesquisaProdutoContrato{ IdLoja = It.IsAny<long>(), DescricaoProduto = "criterio"};

            // Act
            ListaProdutosLojaCompletoResolver listaProdutosLojaCompletoResolver = new ListaProdutosLojaCompletoResolver(pesquisaProdutoContrato);
            listaProdutosLojaCompletoResolver.SessaoAtual = _mockSession.Object;
            long actualTotalElementos = listaProdutosLojaCompletoResolver.GetConsultaDeContagem();

            // Assert
            Assert.AreEqual(expectedTotalElementos, actualTotalElementos);
        }

        [TestMethod]
        public void DeveTrazerContagemDeElementosParaListaMeusProdutosResolver()
        {
            // Arrange
            long expectedTotalElementos = 12;
            SetupMockSessionQueryOverTotal(expectedTotalElementos);
            PesquisaProdutoContrato pesquisaProdutoContrato = new PesquisaProdutoContrato { IdLoja = It.IsAny<long>(), DescricaoProduto = "criterio" };

            // Act
            ListaProdutosLojaMeusProdutosResolver listaProdutosLojaMeusProdutosResolver = new ListaProdutosLojaMeusProdutosResolver(pesquisaProdutoContrato);
            listaProdutosLojaMeusProdutosResolver.SessaoAtual = _mockSession.Object;
            long actualTotalElementos = listaProdutosLojaMeusProdutosResolver.GetConsultaDeContagem();

            // Assert
            Assert.AreEqual(expectedTotalElementos, actualTotalElementos);
        }

        private void SetupMockSessionQueryOverListar(IList<ListaProdutosLojaContrato> lista)
        {
            _mockSession.Setup(p => p.CreateSQLQuery(It.IsAny<string>())
                                      .SetResultTransformer(Transformers.AliasToBean(typeof(ListaProdutosLojaContrato)))
                                      .SetParameter<long>("id", It.IsAny<long>())
                                      .SetParameter<string>("descricao", It.IsAny<string>())
                                      .SetFirstResult(It.IsAny<int>())
                                      .SetMaxResults(It.IsAny<int>())
                                      .List<ListaProdutosLojaContrato>()).Returns(lista);
        }

        private void SetupMockSessionQueryOverTotal(long expectedTotalElementos)
        {
            _mockSession.Setup(p => p.CreateSQLQuery(It.IsAny<string>())
                                    .SetParameter<long>("id", It.IsAny<long>())
                                    .SetParameter<string>("descricao", It.IsAny<string>())
                                    .UniqueResult<long>()).Returns(expectedTotalElementos);
        }

    }
}
