using Graxei.Modelo;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre;
using Graxei.Transversais.ContratosDeDados.Listas;
using Graxei.Transversais.ContratosDeDados.TinyTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Implementacao.Teste
{
    [TestClass]
    public class PersistenciaListaProdutosLojaTeste
    {
        private Mock<ISession> _mockSession = new Mock<ISession>();

        [TestInitialize]
        public void SetUp()
        {
            _mockSession = new Mock<ISession>();
        }

        [TestMethod]
        public void Get_TotalElementosPassadosIgualAZeroETotalListaMaiorQueZero_Listar()
        {
            // Arrange
            List<ListaProdutosLojaContrato> lista = RepositorioCommon.GetUmElemento();
            int expectedTotalElementos = 100;
            int expectedElementoAtual = 5;
            ListaProdutosLoja expectedListaProdutosLoja = RepositorioCommon.Construir(lista, expectedTotalElementos, expectedElementoAtual);
            ListaProdutosLojaRepositorio listaProdutosLojaRepositorio = new ListaProdutosLojaRepositorio();
            SetupMockSessionQueryOverTotal(expectedTotalElementos);
            SetupMockSessionQueryOverListar(lista);
            listaProdutosLojaRepositorio.SetSessaoAtual(_mockSession.Object);

            // Act
            ListaProdutosLoja actualListaProdutosLoja = listaProdutosLojaRepositorio.GetSomenteUmEndereco("criterio", It.IsAny<int>(), expectedElementoAtual, 0);

            // Assert
            Assert.AreEqual(expectedListaProdutosLoja, actualListaProdutosLoja);
        }

        [TestMethod]
        public void Get_TotalElementosContadosIgualAZero_RetornarListaVaziaETotalEAtualIguaisAZero()
        {
            // Arrange
            ListaProdutosLoja expectedListaProdutosLoja = new ListaProdutosLoja(new List<ListaProdutosLojaContrato>(), new ListaTotalElementos(0), new ListaElementoAtual(0));
            ListaProdutosLojaRepositorio listaProdutosLojaRepositorio = new ListaProdutosLojaRepositorio();
            SetupMockSessionQueryOverTotal(0);
            listaProdutosLojaRepositorio.SetSessaoAtual(_mockSession.Object);

            // Act
            ListaProdutosLoja actualListaProdutosLoja = listaProdutosLojaRepositorio.GetSomenteUmEndereco("criterio", It.IsAny<long>(), 1, 0);

            // Assert
            _mockSession.Verify(p => p.CreateSQLQuery(It.IsAny<string>())
                                    .SetResultTransformer(Transformers.AliasToBean(typeof(ListaProdutosLojaContrato)))
                                    .SetParameter<int>("id", It.IsAny<int>())
                                    .SetFirstResult(It.IsAny<int>())
                                    .SetMaxResults(It.IsAny<int>())
                                    .List<ListaProdutosLojaContrato>(), Times.Never);
            Assert.AreEqual(expectedListaProdutosLoja, actualListaProdutosLoja);
        }

        [TestMethod]
        public void Get_ListaVindaDoRepositorioEVaziaETotalPassadoMaiorQueZero_RetornarListaVaziaETotalEAtualIguaisAZero()
        {
            // Arrange
            ListaProdutosLoja expectedListaProdutosLoja = new ListaProdutosLoja(new List<ListaProdutosLojaContrato>(), new ListaTotalElementos(0), new ListaElementoAtual(0));
            ListaProdutosLojaRepositorio listaProdutosLojaRepositorio = new ListaProdutosLojaRepositorio();
            listaProdutosLojaRepositorio.SetSessaoAtual(_mockSession.Object);
            SetupMockSessionQueryOverListar(new List<ListaProdutosLojaContrato>());

            // Act
            ListaProdutosLoja actualListaProdutosLoja = listaProdutosLojaRepositorio.GetSomenteUmEndereco("criterio", It.IsAny<int>(), 0, 0, 12);

            // Assert
            _mockSession.Verify((p => p.QueryOver<ProdutoVendedor>()
                                    .JoinQueryOver<Endereco>(q => q.Endereco)
                                    .JoinQueryOver<Loja>(r => r.Loja).Where(s => s.Id == It.IsAny<long>()).RowCount()), Times.Never);
            Assert.AreEqual(expectedListaProdutosLoja, actualListaProdutosLoja);
        }

        [TestMethod]
        public void Get_ListaVindaDoRepositorioNaoEVaziaETotalPassadoMaiorQueZero_RetornarListaETotalEAtualIguaisAZero()
        {
            // Arrange
            ListaProdutosLoja expectedListaProdutosLoja = new ListaProdutosLoja(new List<ListaProdutosLojaContrato>(), new ListaTotalElementos(0), new ListaElementoAtual(0));
            ListaProdutosLojaRepositorio listaProdutosLojaRepositorio = new ListaProdutosLojaRepositorio();
            listaProdutosLojaRepositorio.SetSessaoAtual(_mockSession.Object);
            SetupMockSessionQueryOverListar(new List<ListaProdutosLojaContrato>());

            // Act
            ListaProdutosLoja actualListaProdutosLoja = listaProdutosLojaRepositorio.GetSomenteUmEndereco("criterio", It.IsAny<long>(), 0, 0, 12);

            // Assert
            _mockSession.Verify((p => p.QueryOver<ProdutoVendedor>()
                                    .JoinQueryOver<Endereco>(q => q.Endereco)
                                    .JoinQueryOver<Loja>(r => r.Loja).Where(s => s.Id == It.IsAny<long>()).RowCount()), Times.Never);
            Assert.AreEqual(expectedListaProdutosLoja, actualListaProdutosLoja);
        }


        [TestMethod]
        public void Get_ListaVindaDoRepositorioNaoEVaziaETotalPassadoMaiorQueZero_Listar()
        {
            // Arrange
            List<ListaProdutosLojaContrato> lista = RepositorioCommon.GetDoisElementos();
            int expectedTotalElementos = 88;
            int expectedElementoAtual = 5;
            ListaProdutosLoja expectedListaProdutosLoja = RepositorioCommon.Construir(lista, expectedTotalElementos, expectedElementoAtual);
            ListaProdutosLojaRepositorio listaProdutosLojaRepositorio = new ListaProdutosLojaRepositorio();
            listaProdutosLojaRepositorio.SetSessaoAtual(_mockSession.Object);
            SetupMockSessionQueryOverListar(lista);

            // Act
            ListaProdutosLoja actualListaProdutosLoja = listaProdutosLojaRepositorio.GetSomenteUmEndereco("criterio", It.IsAny<long>(), expectedElementoAtual, 0, expectedTotalElementos);

            // Assert
            _mockSession.Verify((p => p.QueryOver<ProdutoVendedor>()
                                    .JoinQueryOver<Endereco>(q => q.Endereco)
                                    .JoinQueryOver<Loja>(r => r.Loja).Where(s => s.Id == It.IsAny<long>()).RowCount()), Times.Never);
            Assert.AreEqual(expectedListaProdutosLoja, actualListaProdutosLoja);
        }

        private void SetupMockSessionQueryOverTotal(int expectedTotalElementos)
        {
            _mockSession.Setup(p => p.QueryOver<ProdutoVendedor>()
                                     .Where(Restrictions.InsensitiveLike(Projections.Property<ProdutoVendedor>(q => q.Descricao),
                                                                            It.IsAny<string>(), MatchMode.Anywhere))
                                        .JoinQueryOver<Endereco>(q => q.Endereco)
                                        .JoinQueryOver<Loja>(r => r.Loja)
                                        .Where(s => s.Id == It.IsAny<long>())
                                        .RowCount()).Returns(expectedTotalElementos);
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

    }
}
