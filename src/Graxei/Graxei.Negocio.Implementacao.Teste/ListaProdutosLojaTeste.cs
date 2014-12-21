using Graxei.Persistencia.Contrato;
using Graxei.Persistencia.Implementacao.Teste;
using Graxei.Transversais.ContratosDeDados.Listas;
using Graxei.Transversais.ContratosDeDados.TinyTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            mockRepositorioListaProdutosLoja.Setup(p => p.GetSomenteUmEndereco(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(expectedListaProdutosLoja);

            // Act
            ListaProdutosLoja actualListaProdutosLoja = new ServicoListaProdutosLojaUmEndereco(mockRepositorioListaProdutosLoja.Object).Get("criterio", It.IsAny<long>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>());

            // Assert
            Assert.AreEqual(expectedListaProdutosLoja, actualListaProdutosLoja);
        }

    }
}
