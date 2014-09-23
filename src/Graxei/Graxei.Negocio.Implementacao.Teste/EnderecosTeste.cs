using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Negocio.Implementacao;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Utilidades.Excecoes;
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
    public class EnderecosTeste
    {
        [TestInitialize]
        public void Iniciar()
        {
            _mockRepositorioEnderecos = new Mock<IRepositorioEnderecos>();
            _mockServicoLogradouros = new Mock<IServicoLogradouros>();
            _mockServicoBairros = new Mock<IServicoBairros>();
            _mockServicoCidades = new Mock<IServicoCidades>();
            _mockServicoEstados = new Mock<IServicoEstados>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeveRetornarFalhaNoValidadorNoPreSalvar_QuandoEnderecoForNulo()
        {
            // Arrange
            Endereco endereco = null;
            
            // Act
            ServicoEnderecos servicoEnderecos = new ServicoEnderecos(_mockRepositorioEnderecos.Object);
            servicoEnderecos.PreSalvar(endereco);

            // Assert feito através de disparo de exceção
        }

        [TestMethod]
        [ExpectedException(typeof(OperacaoEntidadeException))]
        public void DeveDispararExcecaoNoPreSalvar_QuandoLojaDoEnderecoForNula()
        {
            // Arrange
            Endereco endereco = new Endereco();

            // Act
            ServicoEnderecos servicoEnderecos = new ServicoEnderecos(_mockRepositorioEnderecos.Object);
            servicoEnderecos.PreSalvar(endereco);

            // Assert feito através de disparo de exceção
        }

        [TestMethod]
        [ExpectedException(typeof(OperacaoEntidadeException))]
        public void DeveDispararExcecaoNoPreSalvar_QuandoLojaDoEnderecoForTransiente()
        {
            // Arrange
            Endereco endereco = new Endereco();
            endereco.Loja = new Loja();

            // Act
            ServicoEnderecos servicoEnderecos = new ServicoEnderecos(_mockRepositorioEnderecos.Object);
            servicoEnderecos.PreSalvar(endereco);

            // Assert feito através de disparo de exceção
        }

        [TestMethod]
        [ExpectedException(typeof(OperacaoEntidadeException))]
        public void DeveDispararExcecaoNoPreSalvar_QuandoLogradouroDoEnderecoForVazio()
        {
            // Arrange
            Endereco endereco = new Endereco();
            Mock<Loja> mockLoja = new Mock<Loja>();
            mockLoja.SetupGet(p => p.Id).Returns(1);
            endereco.Loja = new Loja();

            // Act
            ServicoEnderecos servicoEnderecos = new ServicoEnderecos(_mockRepositorioEnderecos.Object);
            servicoEnderecos.PreSalvar(endereco);

            // Assert feito através de disparo de exceção
        }

        [TestMethod]
        [ExpectedException(typeof(OperacaoEntidadeException))]
        public void DeveDispararExcecaoNoPreSalvar_QuandoNumeroDoEnderecoForNulo()
        {
            // Arrange
            Endereco endereco = new Endereco();
            Mock<Loja> mockLoja = new Mock<Loja>();
            mockLoja.SetupGet(p => p.Id).Returns(1);
            endereco.Loja = mockLoja.Object;
            endereco.Logradouro = "Rua x";

            // Act
            ServicoEnderecos servicoEnderecos = new ServicoEnderecos(_mockRepositorioEnderecos.Object);
            servicoEnderecos.PreSalvar(endereco);

            // Assert feito através de disparo de exceção
        }

        [TestMethod]
        [ExpectedException(typeof(OperacaoEntidadeException))]
        public void DeveDispararExcecaoNoPreSalvar_QuandoBairroDoEnderecoForNulo()
        {
            // Arrange
            Endereco endereco = new Endereco();
            Mock<Loja> mockLoja = new Mock<Loja>();
            mockLoja.SetupGet(p => p.Id).Returns(1);
            endereco.Loja = mockLoja.Object;
            endereco.Logradouro = "Rua x";
            endereco.Numero = "100";
            // Act
            ServicoEnderecos servicoEnderecos = new ServicoEnderecos(_mockRepositorioEnderecos.Object);
            servicoEnderecos.PreSalvar(endereco);

            // Assert feito através de disparo de exceção
        }

        [TestMethod]
        [ExpectedException(typeof(OperacaoEntidadeException))]
        public void DeveDispararExcecaoNoPreSalvar_QuandoBairroDoEnderecoForTransiente()
        {
            // Arrange
            Endereco endereco = new Endereco();
            Mock<Loja> mockLoja = new Mock<Loja>();
            mockLoja.SetupGet(p => p.Id).Returns(1);
            endereco.Loja = mockLoja.Object;
            endereco.Logradouro = "Rua x";
            endereco.Numero = "100";
            Bairro bairro = new Bairro();
            endereco.Bairro = bairro;

            // Act
            ServicoEnderecos servicoEnderecos = new ServicoEnderecos(_mockRepositorioEnderecos.Object);
            servicoEnderecos.PreSalvar(endereco);

            // Assert feito através de disparo de exceção
        }

        [TestMethod]
        [ExpectedException(typeof(OperacaoEntidadeException))]
        public void DeveDispararExcecaoNoPreSalvar_QuandoRepositorioIndicarQueHaEnderecoRepetido()
        {
            // Arrange
            Endereco endereco = new Endereco();
            Mock<Loja> mockLoja = new Mock<Loja>();
            mockLoja.SetupGet(p => p.Id).Returns(1);
            endereco.Loja = mockLoja.Object;
            endereco.Logradouro = "Rua x";
            endereco.Numero = "10";
            Mock<Bairro> mockBairro = new Mock<Bairro>();
            mockBairro.SetupGet(p => p.Id).Returns(1);
            endereco.Bairro = mockBairro.Object;
            _mockRepositorioEnderecos.Setup(
                p =>
                    p.Get(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>()))
                .Returns(endereco);
            
                // Act
            ServicoEnderecos servicoEnderecos = new ServicoEnderecos(_mockRepositorioEnderecos.Object);
            servicoEnderecos.PreSalvar(endereco);

            // Assert feito através de disparo de exceção
            _mockRepositorioEnderecos.Verify(p =>
                    p.Get(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>()), Times.Once());
        }

        [TestMethod]
        public void DevePassarPeloPreSalvarDoEndereco()
        {
            // Arrange
            Endereco endereco = new Endereco();
            Mock<Loja> mockLoja = new Mock<Loja>();
            mockLoja.SetupGet(p => p.Id).Returns(1);
            endereco.Loja = mockLoja.Object;
            endereco.Logradouro = "Rua x";
            endereco.Numero = "10";
            Mock<Bairro> mockBairro = new Mock<Bairro>();
            mockBairro.SetupGet(p => p.Id).Returns(1);
            endereco.Bairro = mockBairro.Object;
            _mockRepositorioEnderecos.Setup(
                p =>
                    p.Get(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>()))
                .Returns(null as Endereco);

            // Act
            ServicoEnderecos servicoEnderecos = new ServicoEnderecos(_mockRepositorioEnderecos.Object);
            servicoEnderecos.PreSalvar(endereco);

            // Assert
            _mockRepositorioEnderecos.Verify(p =>
                    p.Get(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>()), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeveRetornarFalhaNoValidadorNoPreAtualizar_QuandoEnderecoForNulo()
        {
            // Arrange
            Endereco endereco = null;

            // Act
            ServicoEnderecos servicoEnderecos = new ServicoEnderecos(_mockRepositorioEnderecos.Object);
            servicoEnderecos.PreAtualizar(endereco);

            // Assert feito através de disparo de exceção
        }

        [TestMethod]
        [ExpectedException(typeof(OperacaoEntidadeException))]
        public void DeveDispararExcecaoNoPreAtualizar_QuandoLojaDoEnderecoForNula()
        {
            // Arrange
            Endereco endereco = new Endereco();

            // Act
            ServicoEnderecos servicoEnderecos = new ServicoEnderecos(_mockRepositorioEnderecos.Object);
            servicoEnderecos.PreAtualizar(endereco);

            // Assert feito através de disparo de exceção
        }

        [TestMethod]
        [ExpectedException(typeof(OperacaoEntidadeException))]
        public void DeveDispararExcecaoNoPreAtualizar_QuandoLojaDoEnderecoForTransiente()
        {
            // Arrange
            Endereco endereco = new Endereco();
            endereco.Loja = new Loja();

            // Act
            ServicoEnderecos servicoEnderecos = new ServicoEnderecos(_mockRepositorioEnderecos.Object);
            servicoEnderecos.PreAtualizar(endereco);

            // Assert feito através de disparo de exceção
        }

        [TestMethod]
        [ExpectedException(typeof(OperacaoEntidadeException))]
        public void DeveDispararExcecaoNoPreAtualizar_QuandoLogradouroDoEnderecoForVazio()
        {
            // Arrange
            Endereco endereco = new Endereco();
            Mock<Loja> mockLoja = new Mock<Loja>();
            mockLoja.SetupGet(p => p.Id).Returns(1);
            endereco.Loja = new Loja();

            // Act
            ServicoEnderecos servicoEnderecos = new ServicoEnderecos(_mockRepositorioEnderecos.Object);
            servicoEnderecos.PreAtualizar(endereco);

            // Assert feito através de disparo de exceção
        }

        [TestMethod]
        [ExpectedException(typeof(OperacaoEntidadeException))]
        public void DeveDispararExcecaoNoPreAtualizar_QuandoNumeroDoEnderecoForNulo()
        {
            // Arrange
            Endereco endereco = new Endereco();
            Mock<Loja> mockLoja = new Mock<Loja>();
            mockLoja.SetupGet(p => p.Id).Returns(1);
            endereco.Loja = mockLoja.Object;
            endereco.Logradouro = "Rua x";

            // Act
            ServicoEnderecos servicoEnderecos = new ServicoEnderecos(_mockRepositorioEnderecos.Object);
            servicoEnderecos.PreAtualizar(endereco);

            // Assert feito através de disparo de exceção
        }

        [TestMethod]
        [ExpectedException(typeof(OperacaoEntidadeException))]
        public void DeveDispararExcecaoNoPreAtualizar_QuandoBairroDoEnderecoForNulo()
        {
            // Arrange
            Endereco endereco = new Endereco();
            Mock<Loja> mockLoja = new Mock<Loja>();
            mockLoja.SetupGet(p => p.Id).Returns(1);
            endereco.Loja = mockLoja.Object;
            endereco.Logradouro = "Rua x";
            endereco.Numero = "100";
            // Act
            ServicoEnderecos servicoEnderecos = new ServicoEnderecos(_mockRepositorioEnderecos.Object);
            servicoEnderecos.PreAtualizar(endereco);

            // Assert feito através de disparo de exceção
        }

        [TestMethod]
        [ExpectedException(typeof(OperacaoEntidadeException))]
        public void DeveDispararExcecaoNoPreAtualizar_QuandoBairroDoEnderecoForTransiente()
        {
            // Arrange
            Endereco endereco = new Endereco();
            Mock<Loja> mockLoja = new Mock<Loja>();
            mockLoja.SetupGet(p => p.Id).Returns(1);
            endereco.Loja = mockLoja.Object;
            endereco.Logradouro = "Rua x";
            endereco.Numero = "100";
            Bairro bairro = new Bairro();
            endereco.Bairro = bairro;

            // Act
            ServicoEnderecos servicoEnderecos = new ServicoEnderecos(_mockRepositorioEnderecos.Object);
            servicoEnderecos.PreSalvar(endereco);

            // Assert feito através de disparo de exceção
        }

        [TestMethod]
        [ExpectedException(typeof(OperacaoEntidadeException))]
        public void DeveDispararExcecaoNoPreAtualizar_QuandoRepositorioIndicarQueHaEnderecoRepetido()
        {
            // Arrange
            long idEnderecoRepositorio = 1;
            long idEndereco = 2;
            Mock<Endereco> mockEnderecoRepositorio = new Mock<Endereco>();
            Mock<Loja> mockLoja = new Mock<Loja>();
            mockLoja.SetupGet(p => p.Id).Returns(1);
            mockEnderecoRepositorio.SetupGet(p => p.Loja).Returns(mockLoja.Object);
            mockEnderecoRepositorio.SetupGet(p => p.Logradouro).Returns("Rua x");
            mockEnderecoRepositorio.SetupGet(p => p.Numero).Returns("10");
            Mock<Bairro> mockBairro = new Mock<Bairro>();
            mockBairro.SetupGet(p => p.Id).Returns(1);
            mockEnderecoRepositorio.SetupGet(p => p.Bairro).Returns(mockBairro.Object);
            mockEnderecoRepositorio.SetupGet(p => p.Id).Returns(idEnderecoRepositorio);

            Mock<Endereco> mockEndereco = new Mock<Endereco>();
            mockEndereco.SetupGet(p => p.Loja).Returns(mockLoja.Object);
            mockEndereco.SetupGet(p => p.Logradouro).Returns("Rua x");
            mockEndereco.SetupGet(p => p.Numero).Returns("10");
            mockBairro.SetupGet(p => p.Id).Returns(1);
            mockEndereco.SetupGet(p => p.Bairro).Returns(mockBairro.Object);
            mockEndereco.SetupGet(p => p.Id).Returns(idEndereco);

            _mockRepositorioEnderecos.Setup(
                p =>
                    p.Get(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>()))
                .Returns(mockEnderecoRepositorio.Object);

            // Act
            ServicoEnderecos servicoEnderecos = new ServicoEnderecos(_mockRepositorioEnderecos.Object);
            servicoEnderecos.PreAtualizar(mockEndereco.Object);

            // Assert feito através de disparo de exceção
            _mockRepositorioEnderecos.Verify(p =>
                    p.Get(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>()), Times.Once());
        }

        [TestMethod]
        public void DevePassarPeloPreAtualizarDoEndereco_QuandoNaoHaEnderecoRepetido()
        {
            // Arrange
            Endereco endereco = new Endereco();
            Mock<Loja> mockLoja = new Mock<Loja>();
            mockLoja.SetupGet(p => p.Id).Returns(1);
            endereco.Loja = mockLoja.Object;
            endereco.Logradouro = "Rua x";
            endereco.Numero = "10";
            Mock<Bairro> mockBairro = new Mock<Bairro>();
            mockBairro.SetupGet(p => p.Id).Returns(1);
            endereco.Bairro = mockBairro.Object;
            _mockRepositorioEnderecos.Setup(
                p =>
                    p.Get(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>()))
                .Returns(null as Endereco);

            // Act
            ServicoEnderecos servicoEnderecos = new ServicoEnderecos(_mockRepositorioEnderecos.Object);
            servicoEnderecos.PreAtualizar(endereco);

            // Assert
            _mockRepositorioEnderecos.Verify(p =>
                    p.Get(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>()), Times.Once());
        }

        [TestMethod]
        public void DevePassarPeloPreAtualizarDoEndereco_QuandoEnderecoDoRepositorioEOMesmoSendoAtualizado()
        {
            // Arrange
            long idEnderecoRepositorio = 1;
            Mock<Endereco> mockEnderecoRepositorio = new Mock<Endereco>();
            Mock<Loja> mockLoja = new Mock<Loja>();
            mockLoja.SetupGet(p => p.Id).Returns(1);
            mockEnderecoRepositorio.SetupGet(p => p.Loja).Returns(mockLoja.Object);
            mockEnderecoRepositorio.SetupGet(p => p.Logradouro).Returns("Rua x");
            mockEnderecoRepositorio.SetupGet(p => p.Numero).Returns("10");
            Mock<Bairro> mockBairro = new Mock<Bairro>();
            mockBairro.SetupGet(p => p.Id).Returns(1);
            mockEnderecoRepositorio.SetupGet(p => p.Bairro).Returns(mockBairro.Object);
            mockEnderecoRepositorio.SetupGet(p => p.Id).Returns(idEnderecoRepositorio);

            Mock<Endereco> mockEndereco = new Mock<Endereco>();
            mockEndereco.SetupGet(p => p.Loja).Returns(mockLoja.Object);
            mockEndereco.SetupGet(p => p.Logradouro).Returns("Rua x");
            mockEndereco.SetupGet(p => p.Numero).Returns("10");
            mockBairro.SetupGet(p => p.Id).Returns(1);
            mockEndereco.SetupGet(p => p.Bairro).Returns(mockBairro.Object);
            mockEndereco.SetupGet(p => p.Id).Returns(idEnderecoRepositorio);

            _mockRepositorioEnderecos.Setup(
                p =>
                    p.Get(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>()))
                .Returns(mockEnderecoRepositorio.Object);

            // Act
            ServicoEnderecos servicoEnderecos = new ServicoEnderecos(_mockRepositorioEnderecos.Object);
            servicoEnderecos.PreAtualizar(mockEndereco.Object);

            // Assert
            _mockRepositorioEnderecos.Verify(p =>
                    p.Get(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>()), Times.Once());
        }

        Mock<IRepositorioEnderecos> _mockRepositorioEnderecos = new Mock<IRepositorioEnderecos>();
        Mock<IServicoLogradouros> _mockServicoLogradouros = new Mock<IServicoLogradouros>();
        Mock<IServicoBairros> _mockServicoBairros = new Mock<IServicoBairros>();
        Mock<IServicoCidades> _mockServicoCidades = new Mock<IServicoCidades>();
        Mock<IServicoEstados> _mockServicoEstados = new Mock<IServicoEstados>();
    }
}
