using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Negocio.Contrato.Especificacoes;
using Graxei.Negocio.Implementacao.Especificacoes;
using Graxei.Transversais.Idiomas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Graxei.Negocio.Implementacao.Teste.Especificacoes
{
    [TestClass]
    public class EnderecosSpecTeste
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EspecificacaoEnderecoSalvar_DeveDispararExcecaoQuandoEnderecoForNulo()
        {
            // Arrange/Act
            ResultadoEspecificacao resultadoEspecificacao = new EnderecosSalvar(null).Satisfeita(null);

            // Assert: Exception a ser disparada
        }

        [TestMethod]
        public void EspecificacaoEnderecoSalvar_DeveRetornarNaoSatisfeitosQuandoLojaForNula()
        {
            // Arrange
            Endereco endereco = GetEndereco();
            endereco.Loja = null;

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new EnderecosSalvar(null).Satisfeita(endereco);

            // Assert
            Assert.IsTrue(SpecCommon.ResultadoEspecificacaoNotOk(resultadoEspecificacao, Erros.EnderecoAssociadoLoja));
        }

        [TestMethod]
        public void EspecificacaoEnderecoSalvar_DeveRetornarNaoSatisfeitosQuandoLojaForTransiente()
        {
            // Arrange
            Endereco endereco = GetEndereco();
            endereco.Loja.Id = 0;

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new EnderecosSalvar(null).Satisfeita(endereco);

            // Assert
            Assert.IsTrue(SpecCommon.ResultadoEspecificacaoNotOk(resultadoEspecificacao, Erros.EnderecoAssociadoLoja));
        }

        [TestMethod]
        public void EspecificacaoEnderecoSalvar_DeveRetornarNaoSatisfeitosQuandoLogradouroForNulo()
        {
            // Arrange
            Endereco endereco = GetEndereco();
            endereco.Logradouro = null;

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new EnderecosSalvar(null).Satisfeita(endereco);

            // Assert
            Assert.IsTrue(SpecCommon.ResultadoEspecificacaoNotOk(resultadoEspecificacao, Erros.EnderecoDeveTerLogradouro));
        }

        [TestMethod]
        public void EspecificacaoEnderecoSalvar_DeveRetornarNaoSatisfeitosQuandoLogradouroForVazio()
        {
            // Arrange
            Endereco endereco = GetEndereco();
            endereco.Logradouro = "";

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new EnderecosSalvar(null).Satisfeita(endereco);

            // Assert
            Assert.IsTrue(SpecCommon.ResultadoEspecificacaoNotOk(resultadoEspecificacao, Erros.EnderecoDeveTerLogradouro));
        }

        [TestMethod]
        public void EspecificacaoEnderecoSalvar_DeveRetornarNaoSatisfeitosQuandoNumeroForNulo()
        {
            // Arrange
            Endereco endereco = GetEndereco();
            endereco.Numero = null;

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new EnderecosSalvar(null).Satisfeita(endereco);

            // Assert
            Assert.IsTrue(SpecCommon.ResultadoEspecificacaoNotOk(resultadoEspecificacao, Erros.EnderecoDeveTerNumero));
        }

        [TestMethod]
        public void EspecificacaoEnderecoSalvar_DeveRetornarNaoSatisfeitosQuandoNumeroForVazio()
        {
            // Arrange
            Endereco endereco = GetEndereco();
            endereco.Numero = "";

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new EnderecosSalvar(null).Satisfeita(endereco);

            // Assert
            Assert.IsTrue(SpecCommon.ResultadoEspecificacaoNotOk(resultadoEspecificacao, Erros.EnderecoDeveTerNumero));
        }

        [TestMethod]
        public void EspecificacaoEnderecoSalvar_DeveRetornarNaoSatisfeitosQuandoBairroForNulo()
        {
            // Arrange
            Endereco endereco = GetEndereco();
            endereco.Bairro = null;

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new EnderecosSalvar(null).Satisfeita(endereco);

            // Assert
            Assert.IsTrue(SpecCommon.ResultadoEspecificacaoNotOk(resultadoEspecificacao, Erros.EnderecoDeveTerBairro));
        }

        [TestMethod]
        public void EspecificacaoEnderecoSalvar_DeveRetornarNaoSatisfeitosQuandoUsuarioNaoTiverAcessoALoja()
        {
            // Arrange
            Endereco endereco = GetEndereco();
            Mock<IServicoEnderecos> mockServicoEnderecos = new Mock<IServicoEnderecos>();
            mockServicoEnderecos.Setup(p => p.UsuarioAtualAssociado(It.IsAny<Endereco>())).Returns(false);

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new EnderecosSalvar(mockServicoEnderecos.Object).Satisfeita(endereco);

            // Assert
            Assert.IsTrue(SpecCommon.ResultadoEspecificacaoNotOk(resultadoEspecificacao, Erros.UsuarioSemAcessoLoja));
        }

        [TestMethod]
        public void EspecificacaoEnderecoSalvar_DeveRetornarNaoSatisfeitoQuandoEnderecoForRepetido()
        {
            // Arrange
            Endereco endereco = GetEndereco();
            endereco.Bairro.Id = 1;
            Mock<IServicoEnderecos> mockServicoEnderecos = new Mock<IServicoEnderecos>();
            mockServicoEnderecos.Setup(p => p.UsuarioAtualAssociado(It.IsAny<Endereco>())).Returns(true);
            mockServicoEnderecos.Setup(p => p.Get(endereco.Loja.Id, endereco.Logradouro, endereco.Numero, endereco.Complemento, endereco.Bairro.Id)).Returns(endereco);

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new EnderecosSalvar(mockServicoEnderecos.Object).Satisfeita(endereco);

            // Assert
            Assert.IsTrue(SpecCommon.ResultadoEspecificacaoNotOk(resultadoEspecificacao, Erros.EnderecoRepetidoLoja));
        }

        [TestMethod]
        public void EspecificacaoEnderecoSalvar_DeveRetornarSatisfeito()
        {
            // Arrange
            Endereco endereco = GetEndereco();
            Mock<IServicoEnderecos> mockServicoEnderecos = new Mock<IServicoEnderecos>();
            mockServicoEnderecos.Setup(p => p.UsuarioAtualAssociado(It.IsAny<Endereco>())).Returns(true);

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new EnderecosSalvar(mockServicoEnderecos.Object).Satisfeita(endereco);

            // Assert
            Assert.IsTrue(resultadoEspecificacao.Ok);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EspecificacaoEnderecoAtualizar_DeveDispararExcecaoQuandoEnderecoForNulo()
        {
            // Arrange/Act
            ResultadoEspecificacao resultadoEspecificacao = new EnderecosAtualizar(null).Satisfeita(null);

            // Assert: Exception a ser disparada
        }

        [TestMethod]
        public void EspecificacaoEnderecoAtualizar_DeveRetornarNaoSatisfeitosQuandoLojaForNula()
        {
            // Arrange
            Endereco endereco = GetEndereco();
            endereco.Loja = null;

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new EnderecosAtualizar(null).Satisfeita(endereco);

            // Assert
            Assert.IsTrue(SpecCommon.ResultadoEspecificacaoNotOk(resultadoEspecificacao, Erros.EnderecoAssociadoLoja));
        }

        [TestMethod]
        public void EspecificacaoEnderecoAtualizar_DeveRetornarNaoSatisfeitosQuandoLojaForTransiente()
        {
            // Arrange
            Endereco endereco = GetEndereco();
            endereco.Loja.Id = 0;

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new EnderecosAtualizar(null).Satisfeita(endereco);

            // Assert
            Assert.IsTrue(SpecCommon.ResultadoEspecificacaoNotOk(resultadoEspecificacao, Erros.EnderecoAssociadoLoja));
        }

        [TestMethod]
        public void EspecificacaoEnderecoAtualizar_DeveRetornarNaoSatisfeitosQuandoLogradouroForNulo()
        {
            // Arrange
            Endereco endereco = GetEndereco();
            endereco.Logradouro = null;

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new EnderecosAtualizar(null).Satisfeita(endereco);

            // Assert
            Assert.IsTrue(SpecCommon.ResultadoEspecificacaoNotOk(resultadoEspecificacao, Erros.EnderecoDeveTerLogradouro));
        }

        [TestMethod]
        public void EspecificacaoEnderecoAtualizar_DeveRetornarNaoSatisfeitosQuandoLogradouroForVazio()
        {
            // Arrange
            Endereco endereco = GetEndereco();
            endereco.Logradouro = "";

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new EnderecosAtualizar(null).Satisfeita(endereco);

            // Assert
            Assert.IsTrue(SpecCommon.ResultadoEspecificacaoNotOk(resultadoEspecificacao, Erros.EnderecoDeveTerLogradouro));
        }

        [TestMethod]
        public void EspecificacaoEnderecoAtualizar_DeveRetornarNaoSatisfeitosQuandoNumeroForNulo()
        {
            // Arrange
            Endereco endereco = GetEndereco();
            endereco.Numero = null;

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new EnderecosAtualizar(null).Satisfeita(endereco);

            // Assert
            Assert.IsTrue(SpecCommon.ResultadoEspecificacaoNotOk(resultadoEspecificacao, Erros.EnderecoDeveTerNumero));
        }

        [TestMethod]
        public void EspecificacaoEnderecoAtualizar_DeveRetornarNaoSatisfeitosQuandoNumeroForVazio()
        {
            // Arrange
            Endereco endereco = GetEndereco();
            endereco.Numero = "";

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new EnderecosAtualizar(null).Satisfeita(endereco);

            // Assert
            Assert.IsTrue(SpecCommon.ResultadoEspecificacaoNotOk(resultadoEspecificacao, Erros.EnderecoDeveTerNumero));
        }

        [TestMethod]
        public void EspecificacaoEnderecoAtualizar_DeveRetornarNaoSatisfeitosQuandoBairroForNulo()
        {
            // Arrange
            Endereco endereco = GetEndereco();
            endereco.Bairro = null;

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new EnderecosAtualizar(null).Satisfeita(endereco);

            // Assert
            Assert.IsTrue(SpecCommon.ResultadoEspecificacaoNotOk(resultadoEspecificacao, Erros.EnderecoDeveTerBairro));
        }

        [TestMethod]
        public void EspecificacaoEnderecoAtualizar_DeveRetornarNaoSatisfeitosQuandoUsuarioNaoTiverAcessoALoja()
        {
            // Arrange
            Endereco endereco = GetEndereco();
            Mock<IServicoEnderecos> mockServicoEnderecos = new Mock<IServicoEnderecos>();
            mockServicoEnderecos.Setup(p => p.UsuarioAtualAssociado(It.IsAny<Endereco>())).Returns(false);

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new EnderecosAtualizar(mockServicoEnderecos.Object).Satisfeita(endereco);

            // Assert
            Assert.IsTrue(SpecCommon.ResultadoEspecificacaoNotOk(resultadoEspecificacao, Erros.UsuarioSemAcessoLoja));
        }

        [TestMethod]
        public void EspecificacaoEnderecoAtualizar_DeveRetornarNaoSatisfeitoQuandoEnderecoForRepetido()
        {
            // Arrange
            Endereco endereco = GetEndereco();
            endereco.Id = 1;
            Endereco enderecoRepetido = GetEndereco();
            enderecoRepetido.Id = 2;
            Mock<IServicoEnderecos> mockServicoEnderecos = new Mock<IServicoEnderecos>();
            mockServicoEnderecos.Setup(p => p.UsuarioAtualAssociado(It.IsAny<Endereco>())).Returns(true);
            mockServicoEnderecos.Setup(p => p.Get(endereco.Loja.Id, endereco.Logradouro, endereco.Numero, endereco.Complemento, endereco.Bairro.Id)).Returns(enderecoRepetido);

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new EnderecosAtualizar(mockServicoEnderecos.Object).Satisfeita(endereco);

            // Assert
            Assert.IsTrue(SpecCommon.ResultadoEspecificacaoNotOk(resultadoEspecificacao, Erros.EnderecoRepetidoLoja));
        }

        [TestMethod]
        public void EspecificacaoEnderecoAtualizar_DeveRetornarSatisfeitoQuandoEnderecoExistirMasTratarSeDoMesmoId()
        {
            // Arrange
            Endereco endereco = GetEndereco();
            endereco.Id = 1;
            Mock<IServicoEnderecos> mockServicoEnderecos = new Mock<IServicoEnderecos>();
            mockServicoEnderecos.Setup(p => p.UsuarioAtualAssociado(It.IsAny<Endereco>())).Returns(true);
            mockServicoEnderecos.Setup(p => p.Get(endereco.Loja.Id, endereco.Logradouro, endereco.Numero, endereco.Complemento, endereco.Bairro.Id)).Returns(endereco);

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new EnderecosAtualizar(mockServicoEnderecos.Object).Satisfeita(endereco);

            // Assert
            Assert.IsTrue(resultadoEspecificacao.Ok);
        }

        [TestMethod]
        public void EspecificacaoEnderecoAtualizar_DeveRetornarSatisfeito()
        {
            // Arrange
            Endereco endereco = GetEndereco();
            Mock<IServicoEnderecos> mockServicoEnderecos = new Mock<IServicoEnderecos>();
            mockServicoEnderecos.Setup(p => p.UsuarioAtualAssociado(It.IsAny<Endereco>())).Returns(true);

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new EnderecosAtualizar(mockServicoEnderecos.Object).Satisfeita(endereco);

            // Assert
            Assert.IsTrue(resultadoEspecificacao.Ok);
        }

        private Endereco GetEndereco()
        {
            Endereco endereco = new Endereco();
            Loja loja = new Loja { Id = 1 };
            loja.AdicionarEndereco(endereco);
            endereco.Logradouro = "Logradouro";
            endereco.Numero = "1";
            endereco.Bairro = new Bairro();
            return endereco;
        }
    }
}
