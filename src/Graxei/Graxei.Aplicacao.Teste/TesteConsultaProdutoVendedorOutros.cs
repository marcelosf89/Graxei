using Graxei.Aplicacao.Implementacao.Consultas;
using Graxei.Negocio.Contrato;
using Graxei.Transversais.ContratosDeDados.Listas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Graxei.Aplicacao.Teste
{
    [TestClass]
    public class TesteConsultaProdutoVendedorOutros
    {
        [TestInitialize]
        public void SetUp()
        {
            _mockServicoProdutoVendedor = new Mock<IServicoProdutoVendedor>();
            _consultasProdutoVendedor = new ConsultasProdutoVendedor(_mockServicoProdutoVendedor.Object, null, null);
        }

        [TestMethod]
        public void DeveRetornarResultadoDoGet()
        {
            // Arrange 
            ListaPesquisaContrato esperado = TesteConsultaProdutoVendedorGet.GetComum();
            _mockServicoProdutoVendedor.Setup(p => p.Get(_texto)).Returns(esperado);

            // Act
            ListaPesquisaContrato real = _consultasProdutoVendedor.Get(_texto);

            // Assert
            Assert.IsTrue(TesteConsultaProdutoVendedorGet.AssertListas(esperado, real));
        }

        [TestMethod]
        public void DeveRetornarUmParaQuantidadeProdutoDoServico()
        {
            // Arrange
            long esperado = 1;
            _mockServicoProdutoVendedor.Setup(p => p.GetQuantidadeProduto()).Returns(esperado);

            // Act
            long real = _consultasProdutoVendedor.GetQuantidadeProduto();

            // Assert
            Assert.AreEqual(esperado, real);
        }

        [TestMethod]
        public void DeveRetornarTresParaQuantidadeProdutoDoServicoQuandoLojaForIdentificada()
        {
            // Arrange
            long esperado = 3;
            _mockServicoProdutoVendedor.Setup(p => p.GetQuantidadeProduto(5)).Returns(esperado);

            // Act
            long real = _consultasProdutoVendedor.GetQuantidadeProduto(5);

            // Assert
            Assert.AreEqual(esperado, real);
        }

        private Mock<IServicoProdutoVendedor> _mockServicoProdutoVendedor;

        private ConsultasProdutoVendedor _consultasProdutoVendedor;

        private string _texto = "consulta";
    }
}
