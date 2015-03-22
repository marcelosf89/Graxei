using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Negocio.Contrato.Especificacoes;
using Graxei.Negocio.Implementacao.Especificacoes;
using Graxei.Transversais.Utilidades.Excecoes;
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
        public void DeveRetornarNaoSatisfeitoAoSalvarQuandoLojaForNula()
        {
            // Arrange/Act
            ResultadoEspecificacao resultadoEspecificacao = new LojasSalvar(null).Satisfeita(null);

            // Assert
            Assert.IsFalse(resultadoEspecificacao.Ok);
        }

        [TestMethod]
        [ExpectedException(typeof(OperacaoEntidadeException))]
        public void DeveRetornarNaoSatisfeitoAoSalvarQuandoLojaForTransiente()
        {
            // Arrange
            Loja loja = new Loja { Id = 1 };

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new LojasSalvar(null).Satisfeita(loja);

            // Assertion made by exception expected to be thrown
        }

        [TestMethod]
        public void DeveRetornarNaoSatisfeitoAoSalvarQuandoNomeDaLojaLojaForNulo()
        {
            // Arrange
            Loja loja = new Loja();

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new LojasSalvar(null).Satisfeita(loja);

            // Assert
            Assert.IsFalse(resultadoEspecificacao.Ok);
        }

        [TestMethod]
        public void DeveRetornarNaoSatisfeitoAoSalvarQuandoNomeDaLojaLojaForVazio()
        {
            // Arrange
            Loja loja = new Loja { Nome = "" };

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new LojasSalvar(null).Satisfeita(loja);

            // Assert
            Assert.IsFalse(resultadoEspecificacao.Ok);
        }

        [TestMethod]
        public void DeveRetornarNaoSatisfeitoAoSalvarQuandoListaDeUsuariosForNula()
        {
            // Arrange
            Loja loja = new Loja { Nome = "Loja ABC" };

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new LojasSalvar(null).Satisfeita(loja);

            // Assert
            Assert.IsFalse(resultadoEspecificacao.Ok);
        }

        [TestMethod]
        public void DeveRetornarNaoSatisfeitoAoSalvarQuandoListaDeUsuariosForVazia()
        {
            // Arrange
            string nomeLoja = "Loja ABC";
            Loja loja = new Loja { Nome = nomeLoja};
            loja.AdicionarUsuario(new Usuario{ Nome = "José"});
            _mockServicoLojas.Setup(p => p.Get(nomeLoja)).Returns(loja);

            // Act
            ResultadoEspecificacao resultadoEspecificacao = new LojasSalvar(_mockServicoLojas.Object).Satisfeita(loja);

            // Assert
            Assert.IsFalse(resultadoEspecificacao.Ok);
        }

        [TestMethod]
        public void DeveRetornarSatisfeitoAoSalvar()
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
