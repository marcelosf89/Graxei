using Graxei.Aplicacao.Contrato.TransformacaoDados;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Transversais.ContratosDeDados;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Aplicacao.Teste.Transformacao
{
    [TestClass]
    public class TesteTransformacaoLoja
    {
        [TestMethod]
        public void DeveRetornarLojaVaziaQuandoArgumentoForNulo()
        {
            // Act
            Loja resultado = _lojasTransformacao.Transformar(null as LojaContrato);

            // Assert
            _mockServicoLojas.Verify(p => p.GetPorId(It.IsAny<long>()), Times.Never);
            Assert.AreEqual(0, resultado.Id);
        }

        [TestMethod]
        public void DeveRetornarLojaBaseadaNoServicoQuandoContratoNaoForTransiente()
        {
            // Arrange
            long id = 1;
            string nome = "LojaLoja";
            LojaContrato contrato = new LojaContrato { Id = id, Nome = nome };
            _mockServicoLojas.Setup(p => p.GetPorId(id)).Returns(new Loja());

            // Act
            Loja resultado = _lojasTransformacao.Transformar(contrato);

            // Assert
            _mockServicoLojas.Verify(p => p.GetPorId(It.IsAny<long>()), Times.Once);
            Assert.AreEqual(nome, resultado.Nome);
        }

        [TestMethod]
        public void DeveRetornarLojaBaseadaNoServicoQuandoContratoForTransiente()
        {
            // Arrange
            long id = -1;
            string nome = "LojaLoja";
            LojaContrato contrato = new LojaContrato { Id = id, Nome = nome };
            _mockServicoLojas.Setup(p => p.GetPorId(id)).Returns(new Loja());

            // Act
            Loja resultado = _lojasTransformacao.Transformar(contrato);

            // Assert
            _mockServicoLojas.Verify(p => p.GetPorId(It.IsAny<long>()), Times.Never);
            Assert.AreEqual(nome, resultado.Nome);
        }

        [TestMethod]
        public void DeveRetornarLojaBaseadaNoServicoQuandoContratoForTransienteComIdZero()
        {
            // Arrange
            long id = 0;
            string nome = "LojaLoja";
            LojaContrato contrato = new LojaContrato { Id = id, Nome = nome };
            _mockServicoLojas.Setup(p => p.GetPorId(id)).Returns(new Loja());

            // Act
            Loja resultado = _lojasTransformacao.Transformar(contrato);

            // Assert
            _mockServicoLojas.Verify(p => p.GetPorId(It.IsAny<long>()), Times.Never);
            Assert.AreEqual(nome, resultado.Nome);
        }

        [TestMethod]
        public void DeveRetornarLojaContratoVaziaQuandoArgumentoForNulo()
        {
            // Act
            LojaContrato resultado = _lojasTransformacao.Transformar(null as Loja);

            // Assert
            Assert.AreEqual(0, resultado.Id);
        }

        [TestMethod]
        public void DeveRetornarLojaContratoSemEnderecosBaseadaNaLojaComListaNulaDeEnderecos()
        {
            // Arrange
            long id = 1;
            string nome = "LojaLoja";
            LojaContrato contrato = new LojaContrato { Id = id, Nome = nome };
            Loja loja = new Loja { Id = id, Nome = nome };
            
            // Act
            LojaContrato resultado = _lojasTransformacao.Transformar(loja);

            // Assert
            Assert.AreEqual(nome, resultado.Nome);
            Assert.AreEqual(0, resultado.EnderecosListaContrato.Count);
        }

        [TestMethod]
        public void DeveRetornarLojaContratoSemEnderecosBaseadaNaLojaComListaVaziaDeEnderecos()
        {
            // Arrange
            Loja loja = GetLojaPadrao();

            // Act
            LojaContrato resultado = _lojasTransformacao.Transformar(loja);

            // Assert
            Assert.AreEqual(Nome, resultado.Nome);
            Assert.AreEqual(0, resultado.EnderecosListaContrato.Count);
        }

        [TestMethod]
        public void DeveRetornarLojaContratoComEnderecoBaseadaNaLojaComEndereco()
        {
            // Arrange
            Loja loja = GetLojaPadrao();
            Endereco endereco = new Endereco();
            List<Endereco> enderecos = new List<Endereco>();
            enderecos.Add(endereco);
            loja.AdicionarEnderecos(enderecos);

            // Act
            LojaContrato resultado = _lojasTransformacao.Transformar(loja);

            // Assert
            Assert.AreEqual(Nome, resultado.Nome);
            Assert.AreEqual(1, resultado.EnderecosListaContrato.Count);
        }

        [TestInitialize]
        public void SetUp()
        {
            _mockServicoLojas = new Mock<IServicoLojas>();
            _lojasTransformacao = new LojasTransformacao(_mockServicoLojas.Object);
        }

        [TestCleanup]
        public void Clean()
        {
            _mockServicoLojas = null;
            _lojasTransformacao = null;
        }

        private Loja GetLojaPadrao()
        {
            long id = 1;
            string nome = Nome;
            LojaContrato contrato = new LojaContrato { Id = id, Nome = nome };
            Loja retorno = new Loja { Id = id, Nome = nome };
            return retorno;
        }

        private Mock<IServicoLojas> _mockServicoLojas;

        private LojasTransformacao _lojasTransformacao;

        private const string Nome = "LojaLoja";

        private const long Id = 1;
    }
}