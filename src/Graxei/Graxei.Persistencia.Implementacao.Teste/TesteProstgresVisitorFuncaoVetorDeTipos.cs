using Graxei.Modelo;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto.Visitor;
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
        private string _esperadoCriar = "SELECT criar_produto_vendedor(array[row(1, 2, 'NovaDescricao', 110.25, 1, '2010-11-12 13:39:36:123')::produto_criacao]) UNION ";

        private string _esperadoAlterar = "SELECT alterar_produto_vendedor(array[row(1, 'DescricaoModificada', 15.33, 25, '2006-05-22 10:47:19:596')::produto_modificacao]) UNION ";

        private string _esperadoExcluir = "SELECT excluir_produto_vendedor(array[row(1, 10, '1997-09-21 22:11:48:819')::produto_exclusao])";

        [TestMethod]
        public void QuandoHaSomenteUmCriarProdutoRetornarConsultaApropriada()
        {   
            //  Arrange
            string esperado = RemoverClausulaUnionDe(_esperadoCriar);
            CriarProdutoVendedor criarProdutoVendedor = GetCriarPadrao();
            DateTime data = new DateTime(2010, 11, 12, 13, 39, 36, 123);

            // Act
            VisitorFuncoesComVetorDeTipos visitor = new VisitorFuncoesComVetorDeTipos();
            visitor.DataSistema = new DataSistemaTeste(data);
            string actual = visitor.Visit(criarProdutoVendedor);
            string actualResultado = visitor.GetResultado();

            // Assert
            Assert.AreEqual(esperado, actualResultado);

        }

        [TestMethod]
        public void QuandoHaDoisCriarProdutoRetornarConsultaApropriada()
        {
            //  Arrange
            string esperado = "SELECT criar_produto_vendedor(array[row(1, 2, 'NovaDescricao', 110.25, 1, '2010-11-12 13:39:36:123')::produto_criacao, row(2, 1, 'NovaDescricao2', 110.30, 1, '2010-11-12 13:39:36:123')::produto_criacao])";
            CriarProdutoVendedor criarProdutoVendedor = GetCriarPadrao();
            CriarProdutoVendedor criarProdutoVendedor2 = new CriarProdutoVendedor(2, "NovaDescricao2", 110.30, 1, GetUsuario(1));
            DateTime data = new DateTime(2010, 11, 12, 13, 39, 36, 123);

            // Act
            VisitorFuncoesComVetorDeTipos visitor = new VisitorFuncoesComVetorDeTipos();
            visitor.DataSistema = new DataSistemaTeste(data);
            visitor.Visit(criarProdutoVendedor);
            visitor.Visit(criarProdutoVendedor2);
            string actualResultado = visitor.GetResultado();

            // Assert
            Assert.AreEqual(esperado, actualResultado);
        }

        [TestMethod]
        public void QuandoHaSomentUmAlterarProdutoRetornarConsultaApropriada()
        {
            //  Arrange
            string esperado = RemoverClausulaUnionDe(_esperadoAlterar);
            AlterarProdutoVendedor alterarProdutoVendedor = GetAlterarPadrao();
            DateTime data = new DateTime(2006, 5, 22, 10, 47, 19, 596);
            
            // Act
            VisitorFuncoesComVetorDeTipos visitor = new VisitorFuncoesComVetorDeTipos();
            visitor.DataSistema = new DataSistemaTeste(data);
            visitor.Visit(alterarProdutoVendedor);
            string actualResultado = visitor.GetResultado();

            // Assert
            Assert.AreEqual(esperado, actualResultado);
        }

        [TestMethod]
        public void QuandoHaDoisAlterarProdutoRetornarConsultaApropriada()
        {
            //  Arrange
            string esperado = "SELECT alterar_produto_vendedor(array[row(1, 'DescricaoModificada', 15.33, 25, '2006-05-22 10:47:19:596')::produto_modificacao, row(2, 'DescricaoModificada2', 155.44, 25, '2006-05-22 10:47:19:596')::produto_modificacao])";
            AlterarProdutoVendedor alterarProdutoVendedor = GetAlterarPadrao();
            AlterarProdutoVendedor alterarProdutoVendedor2 = new AlterarProdutoVendedor(2, "DescricaoModificada2", 155.44, GetUsuario(25));
            DateTime data = new DateTime(2006, 5, 22, 10, 47, 19, 596);

            // Act
            VisitorFuncoesComVetorDeTipos visitor = new VisitorFuncoesComVetorDeTipos();
            visitor.DataSistema = new DataSistemaTeste(data);
            visitor.Visit(alterarProdutoVendedor);
            visitor.Visit(alterarProdutoVendedor2);
            string actualResultado = visitor.GetResultado();

            // Assert
            Assert.AreEqual(esperado, actualResultado);
        }

        [TestMethod]
        public void QuandoHaSomentUmExcluirProdutoRetornarConsultaApropriada()
        {
            ExcluirProdutoVendedor excluirProdutoVendedor = new ExcluirProdutoVendedor(1, GetUsuario(10));
            DateTime data = new DateTime(1997, 9, 21, 22, 11, 48, 819);

            // Act
            VisitorFuncoesComVetorDeTipos visitor = new VisitorFuncoesComVetorDeTipos();
            visitor.DataSistema = new DataSistemaTeste(data);
            visitor.Visit(excluirProdutoVendedor);
            string actualResultado = visitor.GetResultado();

            // Assert
            Assert.AreEqual(_esperadoExcluir, actualResultado);
        }

        [TestMethod]
        public void QuandoHaDoisExcluirProdutoRetornarConsultaApropriada()
        {
            //  Arrange
            string esperado = "SELECT excluir_produto_vendedor(array[row(1, 10, '1997-09-21 22:11:48:819')::produto_exclusao, row(2, 10, '1997-09-21 22:11:48:819')::produto_exclusao])";
            ExcluirProdutoVendedor excluirProdutoVendedor = GetExcluirPadrao();
            ExcluirProdutoVendedor excluirProdutoVendedor2 = new ExcluirProdutoVendedor(2, GetUsuario(10));
            DateTime data = new DateTime(1997, 9, 21, 22, 11, 48, 819);

            // Act
            VisitorFuncoesComVetorDeTipos visitor = new VisitorFuncoesComVetorDeTipos();
            visitor.DataSistema = new DataSistemaTeste(data);
            visitor.Visit(excluirProdutoVendedor);
            visitor.Visit(excluirProdutoVendedor2);
            string actualResultado = visitor.GetResultado();

            // Assert
            Assert.AreEqual(esperado, actualResultado);
        }

        [TestMethod]
        public void QuandoHaUmCriarEUmAlterarProdutoRetornarConsultaApropriada()
        {
            //  Arrange
            string esperadoAlterar = "SELECT alterar_produto_vendedor(array[row(1, 'DescricaoModificada', 15.33, 25, '2010-11-12 13:39:36:123')::produto_modificacao])";
            string esperado = _esperadoCriar + esperadoAlterar;

            CriarProdutoVendedor criarProdutoVendedor = GetCriarPadrao();
            AlterarProdutoVendedor alterarProdutoVendedor = GetAlterarPadrao();
            DateTime data = new DateTime(2010, 11, 12, 13, 39, 36, 123);

            // Act
            VisitorFuncoesComVetorDeTipos visitor = new VisitorFuncoesComVetorDeTipos();
            visitor.DataSistema = new DataSistemaTeste(data);
            visitor.Visit(criarProdutoVendedor);
            visitor.Visit(alterarProdutoVendedor);
            string actualResultado = visitor.GetResultado();

            // Assert
            Assert.AreEqual(esperado, actualResultado);
        }

        [TestMethod]
        public void QuandoHaUmCriarEUmExcluirProdutoRetornarConsultaApropriada()
        {
            //  Arrange
            string esperadoExcluir = "SELECT excluir_produto_vendedor(array[row(1, 10, '2010-11-12 13:39:36:123')::produto_exclusao])";
            string esperado = _esperadoCriar + esperadoExcluir;

            CriarProdutoVendedor criarProdutoVendedor = GetCriarPadrao();
            ExcluirProdutoVendedor excluirProdutoVendedor = GetExcluirPadrao();
            DateTime data = new DateTime(2010, 11, 12, 13, 39, 36, 123);

            // Act
            VisitorFuncoesComVetorDeTipos visitor = new VisitorFuncoesComVetorDeTipos();
            visitor.DataSistema = new DataSistemaTeste(data);
            visitor.Visit(criarProdutoVendedor);
            visitor.Visit(excluirProdutoVendedor);
            string actualResultado = visitor.GetResultado();

            // Assert
            Assert.AreEqual(esperado, actualResultado);
        }

        [TestMethod]
        public void QuandoHaUmAlterarEUmExcluirProdutoRetornarConsultaApropriada()
        {
            //  Arrange
            string esperadoExcluir = "SELECT excluir_produto_vendedor(array[row(1, 10, '2006-05-22 10:47:19:596')::produto_exclusao])";
            string esperado = _esperadoAlterar + esperadoExcluir;

            AlterarProdutoVendedor alterarProdutoVendedor = GetAlterarPadrao();
            ExcluirProdutoVendedor excluirProdutoVendedor = GetExcluirPadrao();
            DateTime data = new DateTime(2006, 5, 22, 10, 47, 19, 596);

            // Act
            VisitorFuncoesComVetorDeTipos visitor = new VisitorFuncoesComVetorDeTipos();
            visitor.DataSistema = new DataSistemaTeste(data);
            visitor.Visit(alterarProdutoVendedor);
            visitor.Visit(excluirProdutoVendedor);
            string actualResultado = visitor.GetResultado();

            // Assert
            Assert.AreEqual(esperado, actualResultado);
        }

        [TestMethod]
        public void QuandoHaUmCriarUmAlterarEUmExcluirProdutoRetornarConsultaApropriada()
        {
            //  Arrange
            string esperadoAlterar = "SELECT alterar_produto_vendedor(array[row(1, 'DescricaoModificada', 15.33, 25, '2010-11-12 13:39:36:123')::produto_modificacao]) UNION ";
            string esperadoExcluir = "SELECT excluir_produto_vendedor(array[row(1, 10, '2010-11-12 13:39:36:123')::produto_exclusao])";
            string esperado = _esperadoCriar + esperadoAlterar + esperadoExcluir;

            CriarProdutoVendedor criarProdutoVendedor = GetCriarPadrao();
            AlterarProdutoVendedor alterarProdutoVendedor = GetAlterarPadrao();
            ExcluirProdutoVendedor excluirProdutoVendedor = GetExcluirPadrao();
            DateTime data = new DateTime(2010, 11, 12, 13, 39, 36, 123);

            // Act
            VisitorFuncoesComVetorDeTipos visitor = new VisitorFuncoesComVetorDeTipos();
            visitor.DataSistema = new DataSistemaTeste(data);
            visitor.Visit(criarProdutoVendedor);
            visitor.Visit(alterarProdutoVendedor);
            visitor.Visit(excluirProdutoVendedor);
            string actualResultado = visitor.GetResultado();

            // Assert
            Assert.AreEqual(esperado, actualResultado);
        }

        private ExcluirProdutoVendedor GetExcluirPadrao()
        {
            ExcluirProdutoVendedor excluirProdutoVendedor = new ExcluirProdutoVendedor(1, GetUsuario(10));
            return excluirProdutoVendedor;
        }

        private CriarProdutoVendedor GetCriarPadrao()
        {
            CriarProdutoVendedor criarProdutoVendedor = new CriarProdutoVendedor(1, "NovaDescricao", 110.25, 2, GetUsuario(1));
            return criarProdutoVendedor;
        }

        private AlterarProdutoVendedor GetAlterarPadrao()
        {
            AlterarProdutoVendedor alterarProdutoVendedor = new AlterarProdutoVendedor(1, "DescricaoModificada", 15.33, GetUsuario(25));
            return alterarProdutoVendedor;
        }
        
        private string RemoverClausulaUnionDe(string str)
        {
            return str.Remove(str.Length - 7);
        }

        private Usuario GetUsuario(int id)
        {
            return new Usuario() { Id = id };
        }

        //private VisitorFuncoesComVetorDeTipos GetVisitor(DateTime data, params IMudancaProdutoVendedorFuncao[] produtos)
        //{
        //    VisitorFuncoesComVetorDeTipos visitor = new VisitorFuncoesComVetorDeTipos();
        //    visitor.DataSistema = new DataSistemaTeste(data);
        //    for (int i = 0; i < produtos.Length; i++)
        //    {
        //        visitor.Visit(produtos[i]);
        //    }

        //    return visitor;
        //}

        private class DataSistemaTeste : IDataSistema
        {
            private DateTime _data;
            public DataSistemaTeste(DateTime data)
            {
                _data = data;
            }

            public DateTime Agora
            {
                get
                {
                    return _data;
                }
            }
        }
    }
}
