using Graxei.Modelo;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto.Visitor;
using Graxei.Persistencia.Implementacao.Teste.Helper;
using Graxei.Transversais.Utilidades.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Implementacao.Teste
{
    [TestClass]
    public class TesteProstgresVisitorFuncaoVetorDeTipos
    {
        private string _esperadoCriar = "SELECT * FROM criar_produto_vendedor(array[row(1, 2, 'NovaDescricao', 110.25, 1, '2010-11-12 13:39:36.123')::produto_criacao{0}]) UNION ";

        private string _esperadoAlterar = "SELECT alterar_produto_vendedor(array[row(1, 'DescricaoModificada', 15.33, 25, '2010-11-12 13:39:36.123')::produto_modificacao{0}]) id_produto_vendedor, null UNION ";

        private string _esperadoExcluir = "SELECT excluir_produto_vendedor(array[row(1, 10, '2010-11-12 13:39:36.123')::produto_exclusao{0}]) id_produto_vendedor, null";

        [TestMethod]
        public void QuandoHaSomenteUmCriarProdutoRetornarConsultaApropriada()
        {   
            //  Arrange
            string esperado = PostgresVisitorHelper.VisitorConsultaFinal(PostgresVisitorHelper.GetLimpo(_esperadoCriar));
            CriarProdutoVendedor criarProdutoVendedor = PostgresVisitorHelper.GetCriarPadrao();

            // Act
            VisitorFuncoesComVetorDeTipos visitor = PostgresVisitorHelper.GetVisitorAndVisit(criarProdutoVendedor);
            string actualResultado = visitor.GetResultado();

            // Assert
            Assert.AreEqual(esperado, actualResultado);
        }

        [TestMethod]
        public void QuandoHaDoisCriarProdutoRetornarConsultaApropriada()
        {
            //  Arrange
            string esperado = string.Format(_esperadoCriar, ", row(2, 1, 'NovaDescricao2', 110.30, 1, '2010-11-12 13:39:36.123')::produto_criacao");
            esperado = PostgresVisitorHelper.VisitorConsultaFinal(PostgresVisitorHelper.GetLimpo(esperado));
            CriarProdutoVendedor criarProdutoVendedor = PostgresVisitorHelper.GetCriarPadrao();
            CriarProdutoVendedor criarProdutoVendedor2 = new CriarProdutoVendedor(2, "NovaDescricao2", 110.30, 1, PostgresVisitorHelper.GetUsuario(1));

            // Act
            VisitorFuncoesComVetorDeTipos visitor = PostgresVisitorHelper.GetVisitorAndVisit(criarProdutoVendedor, criarProdutoVendedor2);
            string actualResultado = visitor.GetResultado();

            // Assert
            Assert.AreEqual(esperado, actualResultado);
        }

        [TestMethod]
        public void QuandoHaSomentUmAlterarProdutoRetornarConsultaApropriada()
        {
            //  Arrange
            string esperado = PostgresVisitorHelper.VisitorConsultaFinal(PostgresVisitorHelper.GetLimpo(_esperadoAlterar));
            AlterarProdutoVendedor alterarProdutoVendedor = PostgresVisitorHelper.GetAlterarPadrao();
            
            // Act
            VisitorFuncoesComVetorDeTipos visitor = PostgresVisitorHelper.GetVisitorAndVisit(alterarProdutoVendedor);
            string actualResultado = visitor.GetResultado();

            // Assert
            Assert.AreEqual(esperado, actualResultado);
        }

        [TestMethod]
        public void QuandoHaDoisAlterarProdutoRetornarConsultaApropriada()
        {
            //  Arrange
            string esperado = string.Format(_esperadoAlterar, ", row(2, 'DescricaoModificada2', 155.44, 25, '2010-11-12 13:39:36.123')::produto_modificacao");
            esperado = PostgresVisitorHelper.VisitorConsultaFinal(PostgresVisitorHelper.GetLimpo(esperado));
            AlterarProdutoVendedor alterarProdutoVendedor = PostgresVisitorHelper.GetAlterarPadrao();
            AlterarProdutoVendedor alterarProdutoVendedor2 = new AlterarProdutoVendedor(2, "DescricaoModificada2", 155.44, PostgresVisitorHelper.GetUsuario(25));
            
            // Act
            VisitorFuncoesComVetorDeTipos visitor = PostgresVisitorHelper.GetVisitorAndVisit(alterarProdutoVendedor, alterarProdutoVendedor2);
            string actualResultado = visitor.GetResultado();

            // Assert
            Assert.AreEqual(esperado, actualResultado);
        }

        [TestMethod]
        public void QuandoHaSomentUmExcluirProdutoRetornarConsultaApropriada()
        {
            // Arrange
            string esperado = PostgresVisitorHelper.VisitorConsultaFinal(string.Format(_esperadoExcluir, string.Empty));
            ExcluirProdutoVendedor excluirProdutoVendedor = new ExcluirProdutoVendedor(1, PostgresVisitorHelper.GetUsuario(10));

            // Act
            VisitorFuncoesComVetorDeTipos visitor = PostgresVisitorHelper.GetVisitorAndVisit(excluirProdutoVendedor);
            string actualResultado = visitor.GetResultado();

            // Assert
            Assert.AreEqual(esperado, actualResultado);
        }

        [TestMethod]
        public void QuandoHaDoisExcluirProdutoRetornarConsultaApropriada()
        {
            //  Arrange
            string esperado = PostgresVisitorHelper.VisitorConsultaFinal(string.Format(_esperadoExcluir, ", row(2, 10, '2010-11-12 13:39:36.123')::produto_exclusao"));
            ExcluirProdutoVendedor excluirProdutoVendedor = PostgresVisitorHelper.GetExcluirPadrao();
            ExcluirProdutoVendedor excluirProdutoVendedor2 = new ExcluirProdutoVendedor(2, PostgresVisitorHelper.GetUsuario(10));

            // Act
            VisitorFuncoesComVetorDeTipos visitor = PostgresVisitorHelper.GetVisitorAndVisit(excluirProdutoVendedor, excluirProdutoVendedor2);
            string actualResultado = visitor.GetResultado();

            // Assert
            Assert.AreEqual(esperado, actualResultado);
        }

        [TestMethod]
        public void QuandoHaUmCriarEUmAlterarProdutoRetornarConsultaApropriada()
        {
            //  Arrange
            string esperado = PostgresVisitorHelper.VisitorConsultaFinal(string.Format(_esperadoCriar, string.Empty) + PostgresVisitorHelper.GetLimpo(_esperadoAlterar));

            CriarProdutoVendedor criarProdutoVendedor = PostgresVisitorHelper.GetCriarPadrao();
            AlterarProdutoVendedor alterarProdutoVendedor = PostgresVisitorHelper.GetAlterarPadrao();

            // Act
            VisitorFuncoesComVetorDeTipos visitor = PostgresVisitorHelper.GetVisitorAndVisit(criarProdutoVendedor, alterarProdutoVendedor);
            string actualResultado = visitor.GetResultado();

            // Assert
            Assert.AreEqual(esperado, actualResultado);
        }

        [TestMethod]
        public void QuandoHaUmCriarEUmExcluirProdutoRetornarConsultaApropriada()
        {
            //  Arrange
            string esperado = PostgresVisitorHelper.VisitorConsultaFinal(string.Format(_esperadoCriar, string.Empty) + string.Format(_esperadoExcluir, string.Empty));

            CriarProdutoVendedor criarProdutoVendedor = PostgresVisitorHelper.GetCriarPadrao();
            ExcluirProdutoVendedor excluirProdutoVendedor = PostgresVisitorHelper.GetExcluirPadrao();

            // Act
            VisitorFuncoesComVetorDeTipos visitor = PostgresVisitorHelper.GetVisitorAndVisit(criarProdutoVendedor, excluirProdutoVendedor);
            string actualResultado = visitor.GetResultado();

            // Assert
            Assert.AreEqual(esperado, actualResultado);
        }

        [TestMethod]
        public void QuandoHaUmAlterarEUmExcluirProdutoRetornarConsultaApropriada()
        {
            //  Arrange
            string esperado = PostgresVisitorHelper.VisitorConsultaFinal(string.Format(_esperadoAlterar, string.Empty) + string.Format(_esperadoExcluir, string.Empty));

            AlterarProdutoVendedor alterarProdutoVendedor = PostgresVisitorHelper.GetAlterarPadrao();
            ExcluirProdutoVendedor excluirProdutoVendedor = PostgresVisitorHelper.GetExcluirPadrao();

            // Act
            VisitorFuncoesComVetorDeTipos visitor = PostgresVisitorHelper.GetVisitorAndVisit(alterarProdutoVendedor, excluirProdutoVendedor);
            string actualResultado = visitor.GetResultado();

            // Assert
            Assert.AreEqual(esperado, actualResultado);
        }

        [TestMethod]
        public void QuandoHaUmCriarUmAlterarEUmExcluirProdutoRetornarConsultaApropriada()
        {
            //  Arrange
            string esperado = PostgresVisitorHelper.VisitorConsultaFinal(string.Format(_esperadoCriar, string.Empty) + string.Format(_esperadoAlterar, string.Empty) + string.Format(_esperadoExcluir,  string.Empty));

            CriarProdutoVendedor criarProdutoVendedor = PostgresVisitorHelper.GetCriarPadrao();
            AlterarProdutoVendedor alterarProdutoVendedor = PostgresVisitorHelper.GetAlterarPadrao();
            ExcluirProdutoVendedor excluirProdutoVendedor = PostgresVisitorHelper.GetExcluirPadrao();

            // Act
            VisitorFuncoesComVetorDeTipos visitor = PostgresVisitorHelper.GetVisitorAndVisit(criarProdutoVendedor, alterarProdutoVendedor, excluirProdutoVendedor);
            string actualResultado = visitor.GetResultado();

            // Assert
            Assert.AreEqual(esperado, actualResultado);
        }

        [TestMethod]
        public void QuandoHaDoisCriarUmAlterarEUmExcluirProdutoRetornarConsultaApropriada()
        {
            //  Arrange
            string esperadoCriarDoTeste = string.Format(_esperadoCriar, ", row(2, 1, 'NovaDescricao2x', 110.30, 1, '2010-11-12 13:39:36.123')::produto_criacao");
            string esperado = esperadoCriarDoTeste + string.Format(_esperadoAlterar, string.Empty) + string.Format(_esperadoExcluir, string.Empty);
            esperado = PostgresVisitorHelper.VisitorConsultaFinal(esperado);
            CriarProdutoVendedor criarProdutoVendedor = PostgresVisitorHelper.GetCriarPadrao();
            CriarProdutoVendedor criarProdutoVendedor2 = new CriarProdutoVendedor(2, "NovaDescricao2x", 110.30, 1, PostgresVisitorHelper.GetUsuario(1));
            AlterarProdutoVendedor alterarProdutoVendedor = PostgresVisitorHelper.GetAlterarPadrao();
            ExcluirProdutoVendedor excluirProdutoVendedor = PostgresVisitorHelper.GetExcluirPadrao();

            // Act
            VisitorFuncoesComVetorDeTipos visitor = PostgresVisitorHelper.GetVisitorAndVisit(criarProdutoVendedor, criarProdutoVendedor2, alterarProdutoVendedor, excluirProdutoVendedor);
            string actualResultado = visitor.GetResultado();

            // Assert
            Assert.AreEqual(esperado, actualResultado);
        }
    }
}
