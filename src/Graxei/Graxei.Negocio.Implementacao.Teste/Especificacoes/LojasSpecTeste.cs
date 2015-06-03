using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Negocio.Contrato.Especificacoes;
using Graxei.Negocio.Implementacao.Especificacoes;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Comum.Excecoes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Negocio.Implementacao.Teste.Especificacoes
{
    [TestClass]
    public class LojasSpecTeste
    {
        [TestMethod]
        public void EspecificacaoLojaSalvar_DeveRetornarNaoSatisfeitoQuandoLojaForNula()
        {
            // Arrange/Act
            ResultadoEspecificacao resultadoEspecificacao = new LojasSalvar(null).Satisfeita(null);

            // Assert
            Assert.IsTrue(SpecCommon.ResultadoEspecificacaoNotOk(resultadoEspecificacao, Erros.LojaNula));
        }

        [TestMethod]
        [ExpectedException(typeof(OperacaoEntidadeException))]
        public void EspecificacaoLojaSalvar_DeveDispararExcecaoQuandoLojaNaoForTransiente()
        {
            // Arrange
            Loja loja = new Loja { Id = 1 };

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new LojasSalvar(null).Satisfeita(loja);

            // Assertion made by exception expected to be thrown
        }

        [TestMethod]
        public void EspecificacaoLojaSalvar_DeveRetornarNaoSatisfeitoQuandoNomeDaLojaForNulo()
        {
            // Arrange
            Loja loja = new Loja();

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new LojasSalvar(null).Satisfeita(loja);

            // Assert
            Assert.IsTrue(SpecCommon.ResultadoEspecificacaoNotOk(resultadoEspecificacao, Erros.LojaNomeNulo));
        }

        [TestMethod]
        public void EspecificacaoLojaSalvar_DeveRetornarNaoSatisfeitoQuandoNomeDaLojaForVazio()
        {
            // Arrange
            Loja loja = new Loja { Nome = "" };

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new LojasSalvar(null).Satisfeita(loja);

            // Assert
            Assert.IsTrue(SpecCommon.ResultadoEspecificacaoNotOk(resultadoEspecificacao, Erros.LojaNomeNulo));
        }

        [TestMethod]
        public void EspecificacaoLojaSalvar_DeveRetornarNaoSatisfeitoQuandoListaDeUsuariosForNula()
        {
            // Arrange
            Loja loja = new Loja { Nome = "Loja ABC" };

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new LojasSalvar(null).Satisfeita(loja);

            // Assert
            Assert.IsTrue(SpecCommon.ResultadoEspecificacaoNotOk(resultadoEspecificacao, Erros.LojasListaUsuariosVazia));
        }

        [TestMethod]
        public void EspecificacaoLojaSalvar_DeveRetornarNaoSatisfeitoQuandoListaDeUsuariosForVazia()
        {
            // Arrange
            Loja loja = new Loja { Nome = "Loja ABC" };
            loja.AdicionarUsuarios(new List<Usuario>());

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new LojasSalvar(null).Satisfeita(loja);

            // Assert
            Assert.IsTrue(SpecCommon.ResultadoEspecificacaoNotOk(resultadoEspecificacao, Erros.LojasListaUsuariosVazia));
        }

        [TestMethod]
        public void EspecificacaoLojaSalvar_DeveRetornarNaoSatisfeitoQuandoJaExistirUmaLojaComMesmoNome()
        {
            // Arrange
            string nomeLoja = "Loja ABC";
            Loja loja = new Loja { Nome = nomeLoja};
            loja.AdicionarUsuario(new Usuario{ Nome = "José"});
            _mockServicoLojas.Setup(p => p.Get(nomeLoja)).Returns(loja);

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new LojasSalvar(_mockServicoLojas.Object).Satisfeita(loja);

            // Assert
            Assert.IsTrue(SpecCommon.ResultadoEspecificacaoNotOk(resultadoEspecificacao, Erros.LojaJaExiste));
        }

        [TestMethod]
        public void EspecificacaoLojaSalvar_DeveRetornarSatisfeito()
        {
            // Arrange
            string nomeLoja = "Loja ABC";
            Loja loja = new Loja { Nome = nomeLoja };
            loja.AdicionarUsuario(new Usuario { Nome = "José" });
            _mockServicoLojas.Setup(p => p.Get(nomeLoja)).Returns(null as Loja);

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new LojasSalvar(_mockServicoLojas.Object).Satisfeita(loja);

            // Assert
            Assert.IsTrue(resultadoEspecificacao.Ok);
        }

        [TestMethod]
        public void EspecificacaoLojaAtualizar_DeveRetornarNaoSatisfeitoQuandoLojaForNula()
        {
            // Arrange/Act
            ResultadoEspecificacao resultadoEspecificacao = new LojasAtualizar(null).Satisfeita(null);

            // Assert
            Assert.IsTrue(SpecCommon.ResultadoEspecificacaoNotOk(resultadoEspecificacao, Erros.LojaNula));
        }

        [TestMethod]
        [ExpectedException(typeof(OperacaoEntidadeException))]
        public void EspecificacaoLojaAtualizar_DeveDispararExcecaoQuandoLojaForTransiente()
        {
            // Arrange
            Loja loja = new Loja();

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new LojasAtualizar(null).Satisfeita(loja);

            // Assertion made by exception expected to be thrown
        }

        [TestMethod]
        public void EspecificacaoLojaAtualizar_DeveRetornarNaoSatisfeitoQuandoNomeDaLojaForNulo()
        {
            // Arrange
            Loja loja = new Loja { Id = 1 };

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new LojasAtualizar(null).Satisfeita(loja);

            // Assert
            Assert.IsTrue(SpecCommon.ResultadoEspecificacaoNotOk(resultadoEspecificacao, Erros.LojaNomeNulo));
        }

        [TestMethod]
        public void EspecificacaoLojaAtualizar_DeveRetornarNaoSatisfeitoQuandoNomeDaLojaForVazio()
        {
            // Arrange
            Loja loja = new Loja { Id = 1, Nome = "" };

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new LojasAtualizar(null).Satisfeita(loja);

            // Assert
            Assert.IsFalse(resultadoEspecificacao.Ok);
        }

        [TestMethod]
        public void EspecificacaoLojaAtualizar_DeveRetornarNaoSatisfeitoQuandoListaDeUsuariosForNula()
        {
            // Arrange
            Loja loja = new Loja { Id = 1, Nome = "Loja ABC" };

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new LojasAtualizar(null).Satisfeita(loja);

            // Assert
            Assert.IsTrue(SpecCommon.ResultadoEspecificacaoNotOk(resultadoEspecificacao, Erros.LojasListaUsuariosVazia));
        }

        [TestMethod]
        public void EspecificacaoLojaAtualizar_DeveRetornarNaoSatisfeitoQuandoListaDeUsuariosForVazia()
        {
            // Arrange
            string nomeLoja = "Loja ABC";
            Loja loja = new Loja { Id = 1, Nome = nomeLoja };
            loja.AdicionarUsuarios(new List<Usuario>());
            _mockServicoLojas.Setup(p => p.Get(nomeLoja)).Returns(loja);

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new LojasAtualizar(_mockServicoLojas.Object).Satisfeita(loja);

            // Assert
            Assert.IsTrue(SpecCommon.ResultadoEspecificacaoNotOk(resultadoEspecificacao, Erros.LojasListaUsuariosVazia));
        }

        [TestMethod]
        public void EspecificacaoLojaAtualizar_DeveRetornarNaoSatisfeitoQuandoJaExistirUmaLojaComIdsDiferentesComMesmoNome()
        {
            // Arrange
            string nomeLoja = "Loja ABC";
            Loja loja = new Loja { Id = 1, Nome = nomeLoja };
            loja.AdicionarUsuario(new Usuario { Nome = "José" });
            Loja lojaRepetida = new Loja { Id = 99, Nome = nomeLoja };
            _mockServicoLojas.Setup(p => p.Get(nomeLoja)).Returns(lojaRepetida);

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new LojasAtualizar(_mockServicoLojas.Object).Satisfeita(loja);

            // Assert
            Assert.IsTrue(SpecCommon.ResultadoEspecificacaoNotOk(resultadoEspecificacao, Erros.LojaJaExiste));
        }

        [TestMethod]
        public void EspecificacaoLojaAtualizar_DeveRetornarSatisfeitoQuandoJaExistirUmaLojaComMesmoNomeMasEAPropriaLoja()
        {
            // Arrange
            string nomeLoja = "Loja ABC";
            Loja loja = new Loja { Id = 789, Nome = nomeLoja };
            loja.AdicionarUsuario(new Usuario { Nome = "José" });
            Loja lojaRepetida = new Loja { Id = 789, Nome = nomeLoja };
            _mockServicoLojas.Setup(p => p.Get(nomeLoja)).Returns(lojaRepetida);

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new LojasAtualizar(_mockServicoLojas.Object).Satisfeita(loja);

            // Assert
            Assert.IsTrue(resultadoEspecificacao.Ok);
        }

        [TestMethod]
        public void EspecificacaoLojaAtualizar_DeveRetornarSatisfeito()
        {
            // Arrange
            string nomeLoja = "Loja ABC";
            Loja loja = new Loja { Id = 789, Nome = nomeLoja };
            loja.AdicionarUsuario(new Usuario { Nome = "José" });
            _mockServicoLojas.Setup(p => p.Get(nomeLoja)).Returns(null as Loja);

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new LojasAtualizar(_mockServicoLojas.Object).Satisfeita(loja);

            // Assert
            Assert.IsTrue(resultadoEspecificacao.Ok);

        }
        [TestInitialize]
        public void SetUp()
        {
            _mockServicoLojas = new Mock<IServicoLojas>();
        }


        [TestCleanup]
        public void Clear()
        {
            _mockServicoLojas = null;
        }

        private Mock<IServicoLojas> _mockServicoLojas;
    }
}
