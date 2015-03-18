using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Negocio.Implementacao.Factories;
using Graxei.Transversais.Utilidades.Excecoes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Negocio.Implementacao.Teste.Factories
{
    [TestClass]
    public class TesteBuilders
    {
        [TestInitialize]
        public void SetUp()
        {
            _mockServicoBairros = new Mock<IServicoBairros>();
            _mockServicoCidades = new Mock<IServicoCidades>();
            _mockServicoEstados = new Mock<IServicoEstados>();
            _bairroBuilder = new BairrosBuilder(_mockServicoBairros.Object, _mockServicoCidades.Object, _mockServicoEstados.Object);
        }

        [TestCleanup]
        public void Clear()
        {
            _mockServicoBairros = null;
            _mockServicoCidades = null;
            _mockServicoEstados = null;
            _bairroBuilder = null;
        }

        [TestMethod]
        [ExpectedException(typeof(ObjetoConstrucaoException))]
        public void QuandoNomeForVazioNoBuilderDeBairros_DeveDispararExcecao()
        {
            new BairrosBuilder(null, null, null).Validar();
        }

        [TestMethod]
        [ExpectedException(typeof(ObjetoConstrucaoException))]
        public void QuandoNomeDaCidadeForVazioNoBuilderDeBairros_DeveDispararExcecao()
        {
            _bairroBuilder.SetNome("Bairro").Validar();
        }

        [TestMethod]
        public void QuandoNomeDoBairroEDaCidadeForemInformadosNoBuilderDeBairros_ValidacaoDevePassar()
        {
            _bairroBuilder.SetNome("Bairro").SetCidade("Cidade").Validar();
        }

        [TestMethod]
        public void QuandoServicoDeBairrosLocalizarBairrosNoBuilderDeBairros_DeveRetornarEsseBairro()
        {
            // Arrange
            Bairro esperado = GetBairroPadrao(null);
            _mockServicoBairros.Setup(p => p.Get(_bairro, _cidade, _estado)).Returns(esperado);

            // Act
            Bairro real = _bairroBuilder.SetNome(_bairro).SetCidade(_cidade).SetIdEstado(_estado).Build();

            // Assert
            Assert.AreEqual(esperado.Nome, real.Nome);
            _mockServicoCidades.Verify(p => p.Get(It.IsAny<string>(), It.IsAny<long>()), Times.Never);
        }

        [TestMethod]
        public void QuandoServicoDeBairrosNaoLocalizarBairrosEServicosCidadesLocalizarCidadeNoBuilderDeBairros_DeveRetornarEsseBairro()
        {
            // Arrange
            Cidade cidade = new Cidade { Nome = _cidade };
            Bairro esperado = GetBairroPadrao(cidade);
            _mockServicoBairros.Setup(p => p.Get(_bairro, _cidade, _estado)).Returns(null as Bairro);
            _mockServicoCidades.Setup(p => p.Get(_cidade, _estado)).Returns(cidade);

            // Act
            Bairro real = _bairroBuilder.SetNome(_bairro).SetCidade(_cidade).SetIdEstado(_estado).Build();

            // Assert
            Assert.AreEqual(esperado.Cidade.Nome, real.Cidade.Nome);
            _mockServicoCidades.Verify(p => p.Get(It.IsAny<string>(), It.IsAny<long>()), Times.Once);
        }


        [TestMethod]
        [ExpectedException(typeof(ObjetoNaoEncontradoException))]
        public void QuandoNadaForLocalizadoIncluindoEstadoNoBuilderDeBairros_DeveDispararExcecao()
        {
            // Arrange
            _mockServicoBairros.Setup(p => p.Get(_bairro, _cidade, _estado)).Returns(null as Bairro);
            _mockServicoCidades.Setup(p => p.Get(_cidade, _estado)).Returns(null as Cidade);
            _mockServicoEstados.Setup(p => p.GetPorId(_estado)).Returns(null as Estado);

            // Act
            Bairro real = _bairroBuilder.SetNome(_bairro).SetCidade(_cidade).SetIdEstado(_estado).Build();

            // Assertion made by ExpectedException
        }

        [TestMethod]
        public void QuandoNemBairroNemCidadeForemEncontradosNoBuilderDeBairros_DeveRetornarNovoBairro()
        {
            // Arrange
            Estado estado = new Estado { Id = _estado };
            Cidade cidade = new Cidade { Nome = _cidade, Estado = estado };
            Bairro esperado = GetBairroPadrao(cidade);
            _mockServicoBairros.Setup(p => p.Get(_bairro, _cidade, _estado)).Returns(null as Bairro);
            _mockServicoCidades.Setup(p => p.Get(_cidade, _estado)).Returns(null as Cidade);
            _mockServicoEstados.Setup(p => p.GetPorId(_estado)).Returns(estado);

            // Act
            Bairro real = _bairroBuilder.SetNome(_bairro).SetCidade(_cidade).SetIdEstado(_estado).Build();

            // Assert
            Assert.AreEqual(esperado.Cidade.Estado.Id, real.Cidade.Estado.Id);
            _mockServicoEstados.Verify(p => p.GetPorId(_estado), Times.Once);
        }

        private Mock<IServicoBairros> _mockServicoBairros;

        private Mock<IServicoCidades> _mockServicoCidades;

        private Mock<IServicoEstados> _mockServicoEstados;

        private BairrosBuilder _bairroBuilder;

        private const string _bairro = "Bairro 1";

        private const string _cidade = "Cidade 1";

        private const long _estado = 9;

        private Bairro GetBairroPadrao(Cidade cidade)
        {
            Bairro retorno = new Bairro { Nome = _bairro };
            retorno.Cidade = cidade;
            return retorno;
        }
    }
}
