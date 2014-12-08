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
    public class Cidades
    {
        private Mock<IRepositorioCidades> mockRepositorioCidades;

        [TestInitialize]
        public void SetUp()
        {
            mockRepositorioCidades = new Mock<IRepositorioCidades>();
        }

        [TestMethod]
        public void Get_CidadePorIdEstado_RecuperarUm()
        {
            // Arrange
            string nomeCidade = "Cidade 1";
            Cidade cidade = new Cidade();
            cidade.Nome = nomeCidade;
            mockRepositorioCidades.Setup(p => p.Get(It.IsAny<string>(), It.IsAny<long>())).Returns(cidade);

            // Act
            ServicoCidades servicoCidades = new ServicoCidades(mockRepositorioCidades.Object);
            Cidade actualCidade = servicoCidades.Get(It.IsAny<string>(), It.IsAny<long>());

            // Assert
            Assert.AreEqual(nomeCidade, actualCidade.Nome);

        }

        [TestMethod]
        public void Get_CidadePorObjetoEstado_RecuperarUm()
        {
            // Arrange
            string nomeCidade = "Cidade 1";
            Cidade cidade = new Cidade();
            cidade.Nome = nomeCidade;
            mockRepositorioCidades.Setup(p => p.Get(It.IsAny<string>(), It.IsAny<Estado>())).Returns(cidade);

            // Act
            ServicoCidades servicoCidades = new ServicoCidades(mockRepositorioCidades.Object);
            Cidade actualCidade = servicoCidades.Get(It.IsAny<string>(), It.IsAny<Estado>());

            // Assert
            Assert.AreEqual(nomeCidade, actualCidade.Nome);

        }

        [TestMethod]
        public void Get_CidadesPorObjetoEstado_RecuperarLista()
        {
            // Arrange
            List<Cidade> cidades = PreencherLista();
            List<string> expectedCidades = cidades.Select(p => p.Nome).ToList();
            mockRepositorioCidades.Setup(p => p.Get(It.IsAny<Estado>())).Returns(cidades);

            // Act
            ServicoCidades servicoCidades = new ServicoCidades(mockRepositorioCidades.Object);
            IList<Cidade> actualCidades = servicoCidades.Get(It.IsAny<Estado>());
            List<string> actualListaBairros = actualCidades.Select(p => p.Nome).ToList();

            // Assert
            Assert.IsTrue(expectedCidades.SequenceEqual(actualListaBairros));

        }

        [TestMethod]
        public void Get_CidadesPorIdEstado_RecuperarLista()
        {
            // Arrange
            List<Cidade> cidades = PreencherLista();
            List<string> expectedCidades = cidades.Select(p => p.Nome).ToList();
            mockRepositorioCidades.Setup(p => p.GetPorEstado(It.IsAny<long>())).Returns(cidades);

            // Act
            ServicoCidades servicoCidades = new ServicoCidades(mockRepositorioCidades.Object);
            IList<Cidade> actualCidades = servicoCidades.GetPorEstado(It.IsAny<long>());
            List<string> actualListaBairros = actualCidades.Select(p => p.Nome).ToList();

            // Assert
            Assert.IsTrue(expectedCidades.SequenceEqual(actualListaBairros));

        }

        [TestMethod]
        public void Get_CidadesPorSiglaEstado_RecuperarLista()
        {
            // Arrange
            List<Cidade> cidades = PreencherLista();
            List<string> expectedCidades = cidades.Select(p => p.Nome).ToList();
            mockRepositorioCidades.Setup(p => p.GetPorSiglaEstado(It.IsAny<string>())).Returns(cidades);

            // Act
            ServicoCidades servicoCidades = new ServicoCidades(mockRepositorioCidades.Object);
            IList<Cidade> actualCidades = servicoCidades.GetPorSiglaEstado(It.IsAny<string>());
            List<string> actualListaBairros = actualCidades.Select(p => p.Nome).ToList();

            // Assert
            Assert.IsTrue(expectedCidades.SequenceEqual(actualListaBairros));

        }

        [TestMethod]
        public void Get_CidadesPorNomeEstado_RecuperarLista()
        {
            // Arrange
            List<Cidade> cidades = PreencherLista();
            List<string> expectedCidades = cidades.Select(p => p.Nome).ToList();
            mockRepositorioCidades.Setup(p => p.GetPorNomeEstado(It.IsAny<string>())).Returns(cidades);

            // Act
            ServicoCidades servicoCidades = new ServicoCidades(mockRepositorioCidades.Object);
            IList<Cidade> actualCidades = servicoCidades.GetPorNomeEstado(It.IsAny<string>());
            List<string> actualListaBairros = actualCidades.Select(p => p.Nome).ToList();

            // Assert
            Assert.IsTrue(expectedCidades.SequenceEqual(actualListaBairros));

        }

        private static List<Cidade> PreencherLista()
        {
            string nomeCidade1 = "Cidade 1";
            string nomeCidade2 = "Cidade 2";
            List<Cidade> cidades = new List<Cidade>();
            Cidade cidade = new Cidade();
            cidade.Nome = nomeCidade1;
            cidades.Add(cidade);
            cidade = new Cidade();
            cidade.Nome = nomeCidade2;
            cidades.Add(cidade);
            return cidades;
        }

    }
}
