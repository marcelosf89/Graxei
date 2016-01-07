using Graxei.Persistencia.Contrato;
using Graxei.Persistencia.Implementacao.Teste;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.ContratosDeDados.Listas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Graxei.Negocio.Implementacao.Teste
{
    [TestClass]
    public class ListaProdutosLojaTeste
    {
        [TestMethod]
        public void SomenteUmEndereco_RecuperarLista()
        {
            // Arrange
            ListaProdutosLoja expectedListaProdutosLoja = RepositorioCommon.Construir(RepositorioCommon.GetDoisElementos(), 1, 1);
            Mock<IRepositorioListaProdutosLoja> mockRepositorioListaProdutosLoja = new Mock<IRepositorioListaProdutosLoja>();
            mockRepositorioListaProdutosLoja.Setup(p => p.GetSomenteUmEndereco(It.IsAny<PesquisaProdutoContrato>(), It.IsAny<int>())).Returns(expectedListaProdutosLoja);
            PesquisaProdutoContrato pesquisaProdutoContrato = new PesquisaProdutoContrato { DescricaoProduto = "criterio" };
            // Act
            ListaProdutosLoja actualListaProdutosLoja = new ServicoListaProdutosLojaUmEndereco(mockRepositorioListaProdutosLoja.Object).Get(pesquisaProdutoContrato, It.IsAny<int>());

            // Assert
            Assert.AreEqual(expectedListaProdutosLoja, actualListaProdutosLoja);
        }

    }
}
