using Graxei.Aplicacao.Contrato.TransformacaoDados;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Transversais.ContratosDeDados;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Graxei.Aplicacao.Contrato.Teste.Transformacao
{
    [TestClass]
    public class LojaTransformacaoTeste
    {
        [TestMethod]
        public void QuandoLojaContratoTemIdIgualAZero_DeveTransformarLojaContratoEmLojaModelo()
        {
            // Arrange
            LojaContrato lojaContrato = new LojaContrato();
            string nomeEsperado = "Loja 1";
            lojaContrato.Nome = nomeEsperado;
            Mock<IServicoLojas> mockServicoLojas = new Mock<IServicoLojas>();

            // Act
            LojasTransformacao lojasTransformacao = new LojasTransformacao(mockServicoLojas.Object);
            Loja loja = lojasTransformacao.Transformar(lojaContrato);

            // Assert
            Assert.IsNotNull(loja, "Loja deveria ser uma instância");
            Assert.AreEqual(0, loja.Id, "Loja deveria ser transiente");
            Assert.AreEqual(nomeEsperado, loja.Nome, string.Format("Loja deveria ter o nome {0}", nomeEsperado));
        }

        [TestMethod]
        public void QuandoLojaContratoTemIdMaiorQueZero_DeveTransformarLojaContratoEmLojaModelo()
        {
            // Arrange
            string nomeEsperado = "Loja 1";
            long idEsperado = 3;
            LojaContrato lojaContrato = new LojaContrato();
            lojaContrato.Nome = nomeEsperado;
            lojaContrato.Id = idEsperado;
            Mock<Loja> mockLoja = new Mock<Loja>();
            mockLoja.SetupProperty(p => p.Id, idEsperado);
            mockLoja.SetupProperty(p => p.Nome, nomeEsperado);
            Mock<IServicoLojas> mockServicoLojas = new Mock<IServicoLojas>();
            mockServicoLojas.Setup(p => p.GetPorId(It.IsAny<long>())).Returns(mockLoja.Object);

            // Act
            LojasTransformacao lojasTransformacao = new LojasTransformacao(mockServicoLojas.Object);
            Loja loja = lojasTransformacao.Transformar(lojaContrato);

            // Assert
            Assert.IsNotNull(loja, "Loja deveria ser uma instância");
            Assert.AreEqual(idEsperado, loja.Id, string.Format("Loja deveria ter o Id {0}", idEsperado));
            Assert.AreEqual(nomeEsperado, loja.Nome, string.Format("Loja deveria ter o nome {0}", nomeEsperado));
        }
    }
}
