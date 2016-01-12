using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.SqlNativo;
using Graxei.Transversais.ContratosDeDados;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHibernate;
using System.Collections.Generic;
using System.Data;

namespace Graxei.Persistencia.Implementacao.Teste
{
    [TestClass]
    public class TesteProdutoVendedorNativo
    {
        private Mock<IDbCommand> _mockDbCommand;

        [TestMethod]
        public void DeveRetornarUmaListaNulaQuandoSQLForVazio()
        {
            // Act
            ProdutoVendedorNativo produtoVendedorNativo = new ProdutoVendedorNativo();
            int contador = produtoVendedorNativo.Get(string.Empty).Count;

            // Assert
            Assert.AreEqual(0, contador);
        }

        [TestMethod]
        public void DeveRetornarUmaListaQuandoSQLNaoForVazio()
        {
            // Arrange
            long id = 1; long idProduto = 20;
            double esperado = (id * 4) + (idProduto / 2);
            ProdutoLojaPrecoContrato produtoLoja =
                new ProdutoLojaPrecoContrato { IdMeuProduto = id, Id = idProduto};
            IDataReader dataReader = GetDataReader(id, idProduto);
            _mockDbCommand = new Mock<IDbCommand>();
            _mockDbCommand.Setup(p => p.ExecuteReader()).Returns(dataReader);
            
            // Act
            ProdutoVendedorNativo produtoVendedorNativo = new ProdutoVendedorNativo();
            produtoVendedorNativo.SetSessaoAtual(SetupMocks().Object);
            IList<ProdutoLojaPrecoContrato> listaReal = produtoVendedorNativo.Get("sql");
            ProdutoLojaPrecoContrato produtoLojaReal = listaReal[0];
            double real = (produtoLojaReal.IdMeuProduto * 4) + (produtoLojaReal.Id / 2);

            // Assert
            Assert.AreEqual(esperado, real);
        }

        private IDataReader GetDataReader(long id, long idProduto)
        {
            DataTable dataTable = this.GetDatable();
            DataRow dataRow = dataTable.NewRow();
            dataRow["id_produto_vendedor"] = id;
            dataRow["id_produto"] = idProduto;
            
            dataTable.Rows.Add(dataRow);
            IDataReader dataReader = dataTable.CreateDataReader();
            return dataReader;
        }

        private DataTable GetDatable(){
            var dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("id_produto_vendedor", typeof(long)));
            dataTable.Columns.Add(new DataColumn("id_produto", typeof(long)));
            return dataTable;
        }

        private Mock<ISession> SetupMocks()
        {
            Mock<IDbConnection> mockDbConnection = new Mock<IDbConnection>();
            Mock<ISession> mockSessao = new Mock<ISession>();
            mockSessao.Setup(p => p.CreateSQLQuery(It.IsAny<string>()));
            mockSessao.Setup(p => p.Connection).Returns(mockDbConnection.Object);
            mockDbConnection.Setup(p => p.CreateCommand()).Returns(_mockDbCommand.Object);
            return mockSessao;
        }
    }
}
