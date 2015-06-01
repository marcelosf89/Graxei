using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Apresentacao.MVC4Unity.Controllers;
using Graxei.Apresentacao.MVC4Unity.Infrastructure.Cache;
using Graxei.Apresentacao.MVC4Unity.Models;
using Graxei.Modelo;
using Graxei.Transversais.ContratosDeDados;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Graxei.Apresentacao.Teste
{
    [TestClass]
    public class TesteHomeController
    {
        [TestInitialize]
        public void SetUp()
        {
            _mockCacheComum = new Mock<ICacheComum>();
            _mockConsultasProdutoVendedor = new Mock<IConsultasProdutoVendedor>();
            _mockConsultasLojas = new Mock<IConsultasLojas>();
            _homeController = new HomeController(_mockConsultasProdutoVendedor.Object, null, _mockConsultasLojas.Object, null, _mockCacheComum.Object, null);
            _ipRegiaoModel = new IpRegiaoModel { Pais = _pais, Cidade = _cidade };
        }

        [TestMethod]
        public void DeveAferirValoresParaPesquisaBemSucedidaQuandoListaResultadoForMenorQueDezELojaForInformada()
        {
            // Arrange
            string q = "pecaX loja:loja";
            GetListaComDoisElementos(q);

            // Act
            ViewResult result = (ViewResult)_homeController.Pesquisar(q, _loja);
            PesquisarModel pesquisarModel = (PesquisarModel)result.Model;
            IList<PesquisaContrato> listaReal = pesquisarModel.PesquisaContrato;

            // Assert
            Assert.AreEqual(string.Empty, result.ViewName);
            AssertDoisParaListaEZeroParaNumeroMaximoDePaginas(listaReal, pesquisarModel);
        }

        [TestMethod]
        public void DeveAferirValoresParaPesquisaBemSucedidaQuandoListaResultadoForMaiorQueDezELojaForInformada()
        {
            // Arrange
            string q = "pecaX loja:loja";
            GetListaComOnzeElementos(q);

            // Act
            ViewResult result = (ViewResult)_homeController.Pesquisar(q, _loja);
            PesquisarModel pesquisarModel = (PesquisarModel)result.Model;
            IList<PesquisaContrato> listaReal = pesquisarModel.PesquisaContrato;

            // Assert
            Assert.AreEqual(string.Empty, result.ViewName);
            AssertOnzeParaListaENuloParaNumeroMaximoDePaginas(listaReal, pesquisarModel);
        }

        [TestMethod]
        public void DeveAferirValoresParaPesquisaBemSucedidaQuandoListaResultadoForMenorQueDezELojaNaoForInformada()
        {
            // Arrange
            string q = "pecaX";
            GetListaComDoisElementos(q);

            // Act
            ViewResult result = (ViewResult)_homeController.Pesquisar(q, string.Empty);
            PesquisarModel pesquisarModel = (PesquisarModel)result.Model;
            IList<PesquisaContrato> listaReal = pesquisarModel.PesquisaContrato;

            // Assert
            Assert.AreEqual(string.Empty, result.ViewName);
            AssertDoisParaListaEZeroParaNumeroMaximoDePaginas(listaReal, pesquisarModel);
        }

        [TestMethod]
        public void DeveAferirValoresParaPesquisaBemSucedidaQuandoListaResultadoForMaiorQueDezELojaNaoForInformada()
        {
            // Arrange
            string q = "pecaX";
            GetListaComOnzeElementos(q);

            // Act
            ViewResult result = (ViewResult)_homeController.Pesquisar(q, string.Empty);
            PesquisarModel pesquisarModel = (PesquisarModel)result.Model;
            IList<PesquisaContrato> listaReal = pesquisarModel.PesquisaContrato;

            // Assert
            Assert.AreEqual(string.Empty, result.ViewName);
            AssertOnzeParaListaENuloParaNumeroMaximoDePaginas(listaReal, pesquisarModel);
        }

        [TestMethod]
        public void DeveAtribuirOIpDaRegiaoAoCacheComum()
        {
            // Arrange
            string pais = "Holanda";
            string cidade = "Roterdã";
            short regiao = 100;
            string esperado = pais + cidade + regiao;
            IpRegiaoModel ipRegiaoModel = new IpRegiaoModel { Pais = pais, Cidade = cidade, EstadoCodigo = regiao };
            _mockCacheComum.SetupSet(p => p.IpRegiaoModel = ipRegiaoModel);

            // Act
            _homeController.SetRegionIP(pais, cidade, regiao.ToString());

            // Assert
            _mockCacheComum.VerifySet(p => p.IpRegiaoModel = ipRegiaoModel, Times.Once);
        }

        [TestMethod]
        public void QuandoLojaNãoExistirDeveRetornar404()
        {
            // Arrange
            string lojaInexistente = "lojaQueNaoExiste";
            _mockConsultasLojas.Setup(p => p.GetPorUrl(lojaInexistente)).Returns(null as Loja);

            // Act
            ViewResult viewResult = (ViewResult)_homeController.IndexLoja(lojaInexistente, "q");

            // Assert
            Assert.AreEqual("Error404", viewResult.ViewName);
        }

        [TestMethod]
        public void QuandoLojaExistirDeveRetornarIndex()
        {
            // Arrange
            string lojaExistente = "lojaQueExiste";
            _mockConsultasLojas.Setup(p => p.GetPorUrl(lojaExistente)).Returns(new Loja());

            // Act
            ViewResult viewResult = (ViewResult)_homeController.IndexLoja(lojaExistente, "q");

            // Assert
            Assert.AreEqual("Index", viewResult.ViewName);
        }

        private void AssertOnzeParaListaENuloParaNumeroMaximoDePaginas(IList<PesquisaContrato> listaReal, PesquisarModel pesquisarModel)
        {
            Assert.AreEqual(11, listaReal.Count);
            Assert.IsNull(pesquisarModel.NumeroMaximoPagina);
        }

        private void AssertDoisParaListaEZeroParaNumeroMaximoDePaginas(IList<PesquisaContrato> listaReal, PesquisarModel pesquisarModel)
        {
            Assert.AreEqual(2, listaReal.Count);
            Assert.AreEqual(0, pesquisarModel.NumeroMaximoPagina);
        }

        private void GetListaComDoisElementos(string q)
        {
            IList<PesquisaContrato> lista = new List<PesquisaContrato>{
                new PesquisaContrato{Id =  0, Codigo = "AAA"},
                new PesquisaContrato{Id =  1, Codigo = "BBB"}
            };

            SetupMocks(q, lista);
        }

        private void GetListaComOnzeElementos(string q)
        {
            IList<PesquisaContrato> lista = new List<PesquisaContrato>{
                new PesquisaContrato{Id =  0, Codigo = "AAA"},
                new PesquisaContrato{Id =  1, Codigo = "BBB"},
                new PesquisaContrato{Id =  2, Codigo = "CCC"},
                new PesquisaContrato{Id =  3, Codigo = "DDD"},
                new PesquisaContrato{Id =  4, Codigo = "EEE"},
                new PesquisaContrato{Id =  5, Codigo = "FFF"},
                new PesquisaContrato{Id =  6, Codigo = "GGG"},
                new PesquisaContrato{Id =  7, Codigo = "HHH"},
                new PesquisaContrato{Id =  8, Codigo = "III"},
                new PesquisaContrato{Id =  9, Codigo = "JJJ"},
                new PesquisaContrato{Id =  10, Codigo = "KKK"},
            };

            SetupMocks(q, lista);
        }

        private void SetupMocks(string q, IList<PesquisaContrato> retorno)
        {
            _mockConsultasProdutoVendedor.Setup(p => p.Get(q, _pais, _cidade, 0)).Returns(retorno);
            _mockCacheComum.SetupGet(p => p.IpRegiaoModel).Returns(_ipRegiaoModel);
        }

        private Mock<ICacheComum> _mockCacheComum;

        private Mock<IConsultasProdutoVendedor> _mockConsultasProdutoVendedor;

        private Mock<IConsultasLojas> _mockConsultasLojas;

        private HomeController _homeController;

        string _loja = "loja";
        
        string _pais = "Indonesia";
        
        string _cidade = "Jacarta";

        IpRegiaoModel _ipRegiaoModel;

    }
}
