using Graxei.Persistencia.Contrato.PesquisaProduto;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.PesquisaProduto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graxei.Persistencia.Implementacao.Teste
{
    [TestClass]
    public class TestePesquisaProdutoFactory
    {
        [TestMethod]
        public void QuandoNaoHaLojaNoCriterioDeveRetornarPesquisaProdutoSimples()
        {
            // Arrange
            string criterio = "prod qualquer";

            // Act
            PesquisaProdutoFactory factory = new PesquisaProdutoFactory();
            IPesquisaProdutoRepositorio resultado = factory.Get(criterio);

            // Assert
            Assert.IsInstanceOfType(resultado, typeof(PesquisaProdutoSimples));
        }

        [TestMethod]
        public void QuandoHaLojaNoCriterioDeveRetornarPesquisaProdutoLoja()
        {
            // Arrange
            string criterio = "abc prod qualquer loja: birico";

            // Act
            PesquisaProdutoFactory factory = new PesquisaProdutoFactory();
            IPesquisaProdutoRepositorio resultado = factory.Get(criterio);

            // Assert
            Assert.IsInstanceOfType(resultado, typeof(PesquisaProdutoLoja));
        }
    }
}
