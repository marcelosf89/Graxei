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
        [TestMethod]
        public void VisitorFuncaoDeveRetornarUmaChamadaAFuncaoCriarProdutoVendedor()
        {
            //  Arrange
            string expected = "SELECT criar_produto_vendedor(array[row(1, 2, 'NovaDescricao', 110.25, 1, '2010-11-12 13:39:36:123')::produto_criacao])";
            Usuario usuario = new Usuario(){Id = 1};
            CriarProdutoVendedor criarProdutoVendedor = new CriarProdutoVendedor(1, "NovaDescricao", 110.25, 2, usuario);
            DateTime data = new DateTime(2010, 11, 12, 13, 39, 36, 123);

            // Act
            VisitorFuncoesComVetorDeTipos visitor = new VisitorFuncoesComVetorDeTipos();
            visitor.DataSistema = new DataSistemaTeste(data);
            string actual = visitor.Visit(criarProdutoVendedor);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void VisitorFuncaoDeveRetornarUmaChamadaAFuncaoAlterarProdutoVendedor()
        {
            //  Arrange
            string expected = "SELECT alterar_produto_vendedor(array[row(1, 'DescricaoModificada', 15.33, 25, '2006-05-22 10:47:19:596')::produto_modificacao])";
            Usuario usuario = new Usuario() { Id = 25 };
            AlterarProdutoVendedor alterarProdutoVendedor = new AlterarProdutoVendedor(1, "DescricaoModificada", 15.33, usuario);
            DateTime data = new DateTime(2006, 5, 22, 10, 47, 19, 596);

            // Act
            VisitorFuncoesComVetorDeTipos visitor = new VisitorFuncoesComVetorDeTipos();
            visitor.DataSistema = new DataSistemaTeste(data);
            string actual = visitor.Visit(alterarProdutoVendedor);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void VisitorFuncaoDeveRetornarUmaChamadaAFuncaoExcluirProdutoVendedor()
        {
            //  Arrange
            string expected = "SELECT excluir_produto_vendedor(array[row(1, 10, '1997-09-21 22:11:48:819')::produto_exclusao])";
            Usuario usuario = new Usuario() { Id = 10 };
            ExcluirProdutoVendedor excluirProdutoVendedor = new ExcluirProdutoVendedor(1, usuario);
            DateTime data = new DateTime(1997, 9, 21, 22, 11, 48, 819);

            // Act
            VisitorFuncoesComVetorDeTipos visitor = new VisitorFuncoesComVetorDeTipos();
            visitor.DataSistema = new DataSistemaTeste(data);
            string actual = visitor.Visit(excluirProdutoVendedor);

            // Assert
            Assert.AreEqual(expected, actual);
        }

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
