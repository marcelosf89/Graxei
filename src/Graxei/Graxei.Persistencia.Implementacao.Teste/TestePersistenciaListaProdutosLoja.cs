using Graxei.Modelo;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto.Visitor;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.SqlResolver.Factory;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.SqlResolver.Interface;
using Graxei.Transversais.ContratosDeDados;
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
    public class TestePersistenciaListaProdutosLoja
    {
        private Mock<ISession> _mockSession = new Mock<ISession>();

        private Mock<IListaProdutosLojaSqlResolverFactory> _mockSqlResolverFactory;

        private Mock<IListaProdutosLojaSqlResolver> _mockSqlResolver;

        private Mock<IVisitorCriacaoFuncao> _mockVisitorCriacaoFuncao;

        private ListaProdutosLojaRepositorio _listaProdutosLojaRepositorio;
        
        [TestInitialize]
        public void SetUp()
        {
            _mockSession = new Mock<ISession>();
            _mockSqlResolver = new Mock<IListaProdutosLojaSqlResolver>();
            _mockSqlResolverFactory = new Mock<IListaProdutosLojaSqlResolverFactory>();
            _mockSqlResolverFactory.Setup(p => p.Get(It.IsAny<PesquisaProdutoContrato>())).Returns(_mockSqlResolver.Object);
            _mockVisitorCriacaoFuncao = new Mock<IVisitorCriacaoFuncao>();
            _listaProdutosLojaRepositorio = new ListaProdutosLojaRepositorio(_mockSqlResolverFactory.Object, _mockVisitorCriacaoFuncao.Object);
            _listaProdutosLojaRepositorio.SetSessaoAtual(_mockSession.Object);
        }

        [TestMethod]
        public void Get_TotalElementosPassadosIgualAZeroETotalListaMaiorQueZero_Listar()
        {
            // Arrange
            List<ListaProdutosLojaContrato> lista = RepositorioCommon.GetUmElemento();
            int expectedTotalElementos = 100;
            int expectedElementoAtual = 5;
            ListaProdutosLoja expectedListaProdutosLoja = RepositorioCommon.Construir(lista, expectedTotalElementos, expectedElementoAtual);
            SetupMockSessionQueryOverTotal(expectedTotalElementos);
            SetupMockSessionQueryOverListar(lista);
            _mockSqlResolver.Setup(p => p.GetConsultaDeContagem()).Returns(expectedTotalElementos);
            PesquisaProdutoContrato pesquisaProdutoContrato = new PesquisaProdutoContrato { DescricaoProduto = "criterio", MeusProdutos = It.IsAny<bool>(), PaginaAtualLista = expectedElementoAtual };

            // Act
            ListaProdutosLoja actualListaProdutosLoja = _listaProdutosLojaRepositorio.GetSomenteUmEndereco(pesquisaProdutoContrato, 0);

            // Assert
            _mockSqlResolver.Verify(p => p.GetConsultaDeContagem(), Times.Once);
            Assert.AreEqual(expectedListaProdutosLoja, actualListaProdutosLoja);
        }

        [TestMethod]
        public void Get_TotalElementosContadosIgualAZero_RetornarListaVaziaETotalEAtualIguaisAZero()
        {
            // Arrange
            ListaProdutosLoja expectedListaProdutosLoja = new ListaProdutosLoja(new List<ListaProdutosLojaContrato>(), new TotalElementosLista(0), new PaginaAtualLista(0));
            SetupMockSessionQueryOverTotal(0);
            PesquisaProdutoContrato pesquisaProdutoContrato = new PesquisaProdutoContrato { DescricaoProduto = "criterio", IdLoja = It.IsAny<long>(), MeusProdutos = It.IsAny<bool>(), TotalElementosLista = 0 };

            // Act
            ListaProdutosLoja actualListaProdutosLoja = _listaProdutosLojaRepositorio.GetSomenteUmEndereco(pesquisaProdutoContrato, 1);

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
            ListaProdutosLoja expectedListaProdutosLoja = new ListaProdutosLoja(new List<ListaProdutosLojaContrato>(), new TotalElementosLista(0), new PaginaAtualLista(0));
            ListaProdutosLojaRepositorio listaProdutosLojaRepositorio = new ListaProdutosLojaRepositorio(_mockSqlResolverFactory.Object, _mockVisitorCriacaoFuncao.Object);
            SetupMockSessionQueryOverListar(new List<ListaProdutosLojaContrato>());
            PesquisaProdutoContrato pesquisaProdutoContrato = new PesquisaProdutoContrato() { DescricaoProduto = "criterio", TotalElementosLista = 12 };
           
            // Act
            ListaProdutosLoja actualListaProdutosLoja = _listaProdutosLojaRepositorio.GetSomenteUmEndereco(pesquisaProdutoContrato, 1);

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
            ListaProdutosLoja expectedListaProdutosLoja = new ListaProdutosLoja(new List<ListaProdutosLojaContrato>(), new TotalElementosLista(0), new PaginaAtualLista(0));
            SetupMockSessionQueryOverListar(new List<ListaProdutosLojaContrato>());
            PesquisaProdutoContrato pesquisaProdutoContrato = new PesquisaProdutoContrato { DescricaoProduto = "criterio", MeusProdutos = It.IsAny<bool>(), PaginaAtualLista = 0 };


            // Act
            ListaProdutosLoja actualListaProdutosLoja = _listaProdutosLojaRepositorio.GetSomenteUmEndereco(pesquisaProdutoContrato, 0);

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
            SetupMockSessionQueryOverListar(lista);
            PesquisaProdutoContrato pesquisaProdutoContrato = new PesquisaProdutoContrato { DescricaoProduto = "criterio", MeusProdutos = It.IsAny<bool>(), PaginaAtualLista = expectedElementoAtual, TotalElementosLista = expectedTotalElementos };

            // Act
            ListaProdutosLoja actualListaProdutosLoja = _listaProdutosLojaRepositorio.GetSomenteUmEndereco(pesquisaProdutoContrato, 0);

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
            _mockSqlResolver.Setup(p => p.Get(It.IsAny<int>(), It.IsAny<int>())).Returns(lista);
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
