using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Aplicacao.Contrato.Operacoes;
using Graxei.Apresentacao.Areas.Administrativo.Controllers;
using Graxei.Apresentacao.Areas.Administrativo.Infraestutura.Cache;
using Graxei.Apresentacao.Areas.Administrativo.Models;
using Graxei.Apresentacao.Infrastructure;
using Graxei.Modelo;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.Utilidades.Entidades;
using Graxei.Transversais.Utilidades.TransformacaoDados.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Graxei.Apresentacao.Teste.AreaAdministrativo
{
    [TestClass]
    public class TesteEnderecosController
    {
        [TestInitialize]
        public void SetUp()
        {
            _mockConsultaEnderecos = new Mock<IConsultaEnderecos>();
            _mockOperacaoEnderecos = new Mock<IOperacaoEndereco>();
            _mockConsultaEstados = new Mock<IConsultaEstados>();
            _mockTransformacaoEndereco = new Mock<ITransformacaoMutua<Endereco, EnderecoVistaContrato>>();
            _mockCacheElementosEndereco = new Mock<ICacheElementosEndereco>();
            _mockConsultasCidades = new Mock<IConsultaCidades>();
        }

        [TestMethod]
        public void DeveFazerTodosAsserts_QuandoEditarForChamadoEIdEstadoMaiorQueZero()
        {
            // Arrange / Act
            string vistaEsperada = "ModalEndereco";
            EnderecoVistaContrato contratoEsperado = GetContrato(99);
            PartialViewResult viewResult = ArrangeAndActEditar(contratoEsperado, 99);
            EnderecoVistaContrato enderecoVistaContrato = (EnderecoVistaContrato)viewResult.Model;

            // Assert
            Assert.AreEqual(vistaEsperada, viewResult.ViewName);
            _mockCacheElementosEndereco.Verify(p => p.SetCidades(It.IsAny<IList<Cidade>>()), Times.Once);
            Assert.AreEqual(contratoEsperado, enderecoVistaContrato);
        }

        [TestMethod]
        public void DeveFazerTodosAsserts_QuandoEditarForChamadoEIdEstadoIgualZero()
        {
            // Arrange / Act
            string vistaEsperada = "ModalEndereco";
            EnderecoVistaContrato contratoEsperado = GetContrato();
            PartialViewResult viewResult = ArrangeAndActEditar(contratoEsperado);
            EnderecoVistaContrato enderecoVistaContrato = (EnderecoVistaContrato)viewResult.Model;

            // Assert
            Assert.AreEqual(vistaEsperada, viewResult.ViewName);
            _mockCacheElementosEndereco.Verify(p => p.SetCidades(It.IsAny<IList<Cidade>>()), Times.Never);
            Assert.IsTrue(AssertEnderecoVistaContrato(contratoEsperado, enderecoVistaContrato));
        }

        [TestMethod]
        public void DeveFazerTodosAsserts_QuandoNovoForChamado()
        {
            // Arrange
            string vistaEsperada = "ModalEndereco";
            long idLoja = 998;
            EnderecoVistaContrato contratoEsperado = new EnderecoVistaContrato
            {
                IdLoja = idLoja
            };
            _mockConsultaEstados.Setup(p => p.GetEstados(EstadoOrdem.Sigla)).Returns(new List<Estado>());

            // Act
            EnderecosController enderecosController = new EnderecosController(null, null, _mockConsultaEstados.Object, null, null, null, null, null);
            PartialViewResult viewResult = (PartialViewResult)enderecosController.Novo(idLoja);
            EnderecoVistaContrato enderecoVistaContrato = (EnderecoVistaContrato)viewResult.Model;

            // Assert
            Assert.AreEqual(vistaEsperada, viewResult.ViewName);
            Assert.IsTrue(AssertEnderecoVistaContrato(contratoEsperado, enderecoVistaContrato));
        }

        private PartialViewResult ArrangeAndActEditar(EnderecoVistaContrato enderecoVistaContrato, long idEstado = 0)
        {
            // Arrange
            Endereco endereco = Get(idEstado);
            _mockConsultaEnderecos.Setup(p => p.Get(It.IsAny<long>())).Returns(endereco);
            _mockOperacaoEnderecos.Setup(p => p.GetComBaseEm(It.IsAny<EnderecoVistaContrato>())).Returns(endereco);
            _mockConsultaEstados.Setup(p => p.GetEstados(EstadoOrdem.Sigla)).Returns(new List<Estado>());
            _mockTransformacaoEndereco.Setup(p => p.Transformar(It.IsAny<Endereco>())).Returns(enderecoVistaContrato);
            _mockCacheElementosEndereco.Setup(p => p.SetCidades(It.IsAny<IList<Cidade>>()));
            _mockConsultasCidades.Setup(p => p.GetPorEstado(It.IsAny<long>()));

            // Act
            EnderecosController enderecosController = new EnderecosController(_mockConsultaEnderecos.Object, null, _mockConsultaEstados.Object, _mockConsultasCidades.Object, null, _mockOperacaoEnderecos.Object, _mockCacheElementosEndereco.Object, _mockTransformacaoEndereco.Object);
            return (PartialViewResult)enderecosController.Editar(99);
        }

        private EnderecoVistaContrato GetContrato(long idEstado = 0)
        {
            return new EnderecoVistaContrato
            {
                IdLoja = 101,
                Logradouro = "Rua ABC",
                Id = 98,
                Numero = "100",
                Bairro = "Palitinho",
                Cidade = "Gotham",
                IdEstado = idEstado
            };
        }

        private Endereco Get(long idEstado = 0)
        {
            return new Endereco
            {
                Id = 98,
                Logradouro = "Rua ABC",
                Numero = "100",
                Bairro = new Bairro
                {
                    Nome = "Palitinho",
                    Cidade = new Cidade
                    {
                        Nome = "Gotham",
                        Estado = new Estado { Id = idEstado, Sigla = "PA" }
                    }
                },
                Loja = new Loja {
                    Id = 101,
                    Nome = "Minha Loja"
                }
            };
        }

        private bool AssertEnderecoVistaContrato(EnderecoVistaContrato endereco1, EnderecoVistaContrato endereco2)
        {
            bool result = endereco1.Id == endereco2.Id;
            result &= endereco1.IdLoja == endereco2.IdLoja;
            result &= endereco1.Bairro  == endereco2.Bairro;
            result &= endereco1.Cidade == endereco2.Cidade;
            result &= endereco1.IdEstado == endereco2.IdEstado;
            return result;
        }

        private Mock<IConsultaEnderecos> _mockConsultaEnderecos;
        private Mock<IOperacaoEndereco> _mockOperacaoEnderecos;
        private Mock<IConsultaEstados> _mockConsultaEstados;
        private Mock<IConsultaCidades> _mockConsultasCidades;
        private Mock<ITransformacaoMutua<Endereco, EnderecoVistaContrato>> _mockTransformacaoEndereco;
        private Mock<ICacheElementosEndereco> _mockCacheElementosEndereco;
    }
}
