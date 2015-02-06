using Graxei.Aplicacao.Implementacao.Consultas;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Implementacao.Teste;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.ContratosDeDados.Listas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Aplicacao.Contrato.Teste
{
    [TestClass]
    public class ConsultaListaProdutosLojaTeste
    {
        [TestMethod]
        public void SomenteUmEndereco_RecuperarLista()
        {
            // Arrange
            ListaProdutosLoja expectedListaProdutosLoja = RepositorioCommon.Construir(RepositorioCommon.GetDoisElementos(), 1, 1);
            Mock<IServicoListaProdutosLoja> mockRepositorioListaProdutosLoja = new Mock<IServicoListaProdutosLoja>();
            mockRepositorioListaProdutosLoja.Setup(p => p.Get(It.IsAny<PesquisaProdutoContrato>(), It.IsAny<int>())).Returns(expectedListaProdutosLoja);

            // Act
            ListaProdutosLoja actualListaProdutosLoja = new ConsultaListaProdutosLoja(mockRepositorioListaProdutosLoja.Object).Get(It.IsAny<PesquisaProdutoContrato>(), It.IsAny<int>());

            // Assert
            Assert.AreEqual(expectedListaProdutosLoja, actualListaProdutosLoja);
        }
    }
}
