using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
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
    public class Bairros
    {
        private Mock<IRepositorioBairros> mockRepositorioBairros;

        [TestInitialize]
        public void SetUp()
        {
            mockRepositorioBairros = new Mock<IRepositorioBairros>();
        }

        [TestMethod]
        public void Get_BairrosPorCidade_RecuperarLista()
        {
            // Arrange
            List<Bairro> bairros = PreencherLista();
            List<string> expectedBairros = bairros.Select(p => p.Nome).ToList();
            mockRepositorioBairros.Setup(p => p.Get(It.IsAny<Cidade>())).Returns(bairros);

            // Act
            ServicoBairros servicoBairros = new ServicoBairros(mockRepositorioBairros.Object);
            IList<Bairro> actualBairros = servicoBairros.Get(It.IsAny<Cidade>());
            List<string> actualListaBairros = actualBairros.Select(p => p.Nome).ToList();

            // Assert
            Assert.IsTrue(expectedBairros.SequenceEqual(actualListaBairros));

        }

        [TestMethod]
        public void Get_BairrosPorIdCidade_RecuperarLista()
        {
            // Arrange
            List<Bairro> bairros = PreencherLista();
            List<string> expectedBairros = bairros.Select(p => p.Nome).ToList();
            mockRepositorioBairros.Setup(p => p.Get(It.IsAny<long>())).Returns(bairros);

            // Act
            ServicoBairros servicoBairros = new ServicoBairros(mockRepositorioBairros.Object);
            IList<Bairro> actualBairros = servicoBairros.GetPorCidade(It.IsAny<long>());
            List<string> actualListaBairros = actualBairros.Select(p => p.Nome).ToList();

            // Assert
            Assert.IsTrue(expectedBairros.SequenceEqual(actualListaBairros));

        }

        [TestMethod]
        public void Get_BairrosPorNomeCidade_RecuperarLista()
        {
            // Arrange
            List<Bairro> bairros = PreencherLista();
            List<string> expectedBairros = bairros.Select(p => p.Nome).ToList();
            mockRepositorioBairros.Setup(p => p.GetPorCidade(It.IsAny<string>(), It.IsAny<long>())).Returns(bairros);

            // Act
            ServicoBairros servicoBairros = new ServicoBairros(mockRepositorioBairros.Object);
            IList<Bairro> actualBairros = servicoBairros.GetPorCidade(It.IsAny<string>(), It.IsAny<long>());
            List<string> actualListaBairros = actualBairros.Select(p => p.Nome).ToList();

            // Assert
            Assert.IsTrue(expectedBairros.SequenceEqual(actualListaBairros));

        }

        [TestMethod]
        public void Get_BairroPorNomeBairroNomeCidadeIdEstado_RecuperarUm()
        {
            // Arrange
            string nomeBairro = "Bairro 1";
            Bairro bairro = new Bairro();
            bairro.Nome = nomeBairro;
            mockRepositorioBairros.Setup(p => p.Get(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>())).Returns(bairro);

            // Act
            ServicoBairros servicoBairros = new ServicoBairros(mockRepositorioBairros.Object);
            Bairro actualBairro = servicoBairros.Get(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>());

            // Assert
            Assert.AreEqual(nomeBairro, actualBairro.Nome);

        }

        [TestMethod]
        public void Get_BairroPorNomeBairroNomeCidadeObjetoEstado_RecuperarUm()
        {
            // Arrange
            string nomeBairro = "Bairro 1";
            Bairro bairro = new Bairro();
            bairro.Nome = nomeBairro;
            mockRepositorioBairros.Setup(p => p.Get(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Estado>())).Returns(bairro);

            // Act
            ServicoBairros servicoBairros = new ServicoBairros(mockRepositorioBairros.Object);
            Bairro actualBairro = servicoBairros.Get(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Estado>());

            // Assert
            Assert.AreEqual(nomeBairro, actualBairro.Nome);

        }

        [TestMethod]
        public void Get_BairroPorNomeBairroIdCidade_RecuperarUm()
        {
            // Arrange
            string nomeBairro = "Bairro 1";
            Bairro bairro = new Bairro();
            bairro.Nome = nomeBairro;
            mockRepositorioBairros.Setup(p => p.Get(It.IsAny<string>(), It.IsAny<long>())).Returns(bairro);

            // Act
            ServicoBairros servicoBairros = new ServicoBairros(mockRepositorioBairros.Object);
            Bairro actualBairro = servicoBairros.Get(It.IsAny<string>(), It.IsAny<long>());

            // Assert
            Assert.AreEqual(nomeBairro, actualBairro.Nome);

        }

        [TestMethod]
        public void Get_BairroPorNomeBairroObjetoCidade_RecuperarUm()
        {
            // Arrange
            string nomeBairro = "Bairro 1";
            Bairro bairro = new Bairro();
            bairro.Nome = nomeBairro;
            mockRepositorioBairros.Setup(p => p.Get(It.IsAny<string>(), It.IsAny<Cidade>())).Returns(bairro);

            // Act
            ServicoBairros servicoBairros = new ServicoBairros(mockRepositorioBairros.Object);
            Bairro actualBairro = servicoBairros.Get(It.IsAny<string>(), It.IsAny<Cidade>());

            // Assert
            Assert.AreEqual(nomeBairro, actualBairro.Nome);

        }

        private static List<Bairro> PreencherLista()
        {
            string nomeBairro1 = "Bairro 1";
            string nomeBairro2 = "Bairro 2";
            List<Bairro> bairros = new List<Bairro>();
            Bairro bairro = new Bairro();
            bairro.Nome = nomeBairro1;
            bairros.Add(bairro);
            bairro = new Bairro();
            bairro.Nome = nomeBairro2;
            bairros.Add(bairro);
            return bairros;
        }
    }
}
