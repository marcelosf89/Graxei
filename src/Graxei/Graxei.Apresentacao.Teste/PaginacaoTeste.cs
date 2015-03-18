using Graxei.Apresentacao.MVC4Unity.Extension.PaginacaoChain;
using Graxei.Apresentacao.MVC4Unity.Extension.PaginacaoChain.LinkBuilderStrategy;
using Graxei.Transversais.ContratosDeDados.TinyTypes;
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
    public class PaginacaoTeste
    {
        private Mock<IViewDataContainer> _mockViewDataContainer;

        private Mock<ILinkBuilderStrategy> _mockLinkBuilderStrategy;

        [TestInitialize]
        public void SetUp()
        {
            _mockViewDataContainer = new Mock<IViewDataContainer>();
            _mockLinkBuilderStrategy = new Mock<ILinkBuilderStrategy>();
        }

        [TestMethod]
        public void ImparMenosQueMaximoElementos_QuandoMaximoPaginasNoGrupoMaiorQueTotalElementos_DeveRetornarHtmlEsperado()
        {
            // Act
           int maximoPaginasNoGrupo = 5;
           int totalElementos = 45;
           int expected = (totalElementos / 10);
           ImparMenosQueMaximoElementos imparMenosQueMaximoElementos = new ImparMenosQueMaximoElementos(new AjaxHelper(new ViewContext(), _mockViewDataContainer.Object), new TotalElementosLista(totalElementos), It.IsAny<PaginaAtualLista>(), maximoPaginasNoGrupo, _mockLinkBuilderStrategy.Object);

            // Assert
            Assert.AreEqual(expected, imparMenosQueMaximoElementos.GetUltimaPaginaGrupoAtual());
        }

        [TestMethod]
        public void ImparMenosQueMaximoElementos_FimListaIgualMaximoPaginasNoGrupo_QuandoMaximoPaginasNoGrupoMenorQueTotalElementos()
        {
            // Act
            int maximoPaginasNoGrupo = 5;
            int totalElementos = 82;
            int expected = maximoPaginasNoGrupo;
            ImparMenosQueMaximoElementos imparMenosQueMaximoElementos = new ImparMenosQueMaximoElementos(new AjaxHelper(new ViewContext(), _mockViewDataContainer.Object), new TotalElementosLista(totalElementos), It.IsAny<PaginaAtualLista>(), maximoPaginasNoGrupo, _mockLinkBuilderStrategy.Object);

            // Assert
            Assert.AreEqual(maximoPaginasNoGrupo, imparMenosQueMaximoElementos.GetUltimaPaginaGrupoAtual());
        }

        [TestMethod]
        public void ImparMenosQueMaximoElementos_InicioListaIgualUm()
        {
            // Act
            int maximoPaginasNoGrupo = 5;
            int totalElementos = 543;
            ImparMenosQueMaximoElementos imparMenosQueMaximoElementos = new ImparMenosQueMaximoElementos(new AjaxHelper(new ViewContext(), _mockViewDataContainer.Object), new TotalElementosLista(totalElementos), It.IsAny<PaginaAtualLista>(), maximoPaginasNoGrupo, _mockLinkBuilderStrategy.Object);

            // Assert
            Assert.AreEqual(1, imparMenosQueMaximoElementos.GetPrimeiraPaginaGrupoAtual());
        }

        [TestMethod]
        public void ImparMenosQueMaximoElementos_ElementoSubstituirIgualAAtualMenos1()
        {
            // Act
            int maximoPaginasNoGrupo = 7;
            int esperado = 2;
            ImparMenosQueMaximoElementos imparMenosQueMaximoElementos = new ImparMenosQueMaximoElementos(new AjaxHelper(new ViewContext(), _mockViewDataContainer.Object), new TotalElementosLista(73), new PaginaAtualLista(esperado), maximoPaginasNoGrupo, _mockLinkBuilderStrategy.Object);

            // Assert
            Assert.AreEqual(esperado, imparMenosQueMaximoElementos.GetElementoParaSubstituir());
        }

        [TestMethod]
        public void ImparMenosQueMaximoElementos_RegraNaoAtende_QuandoMaximoDeLinksDePaginaçãoNoGrupoNaoEImpar()
        {
            // Act
            int maximoPaginasNoGrupo = 6;
            int paginaAtual = 2;
            int expected = paginaAtual - 1;
            ImparMenosQueMaximoElementos imparMenosQueMaximoElementos = new ImparMenosQueMaximoElementos(new AjaxHelper(new ViewContext(), _mockViewDataContainer.Object), new TotalElementosLista(73), new PaginaAtualLista(paginaAtual), maximoPaginasNoGrupo, _mockLinkBuilderStrategy.Object);

            // Assert
            Assert.IsFalse(imparMenosQueMaximoElementos.RegraAtende());
        }

        [TestMethod]
        public void ImparMenosQueMaximoElementos_RegraNaoAtende_QuandoItemAtualEstaAlemDoMeioDaLista()
        {
            // Act
            int maximoPaginasNoGrupo = 5;
            int paginaAtual = 4;
            ImparMenosQueMaximoElementos imparMenosQueMaximoElementos = new ImparMenosQueMaximoElementos(new AjaxHelper(new ViewContext(), _mockViewDataContainer.Object), new TotalElementosLista(73), new PaginaAtualLista(paginaAtual), maximoPaginasNoGrupo, _mockLinkBuilderStrategy.Object);

            // Assert
            Assert.IsFalse(imparMenosQueMaximoElementos.RegraAtende());
        }

        [TestMethod]
        public void ImparMenosQueMaximoElementos_RegraNaoAtende_QuandoItemAtualEstaAbaixoDoMeioDaListaMasElementoAtualEMaiorQueQuantidadePaginacaoPorVez()
        {
            // Act
            int maximoPaginasNoGrupo = 5;
            int paginaAtual = 6;
            ImparMenosQueMaximoElementos imparMenosQueMaximoElementos = new ImparMenosQueMaximoElementos(new AjaxHelper(new ViewContext(), _mockViewDataContainer.Object), new TotalElementosLista(73), new PaginaAtualLista(paginaAtual), maximoPaginasNoGrupo, _mockLinkBuilderStrategy.Object);

            // Assert
            Assert.IsFalse(imparMenosQueMaximoElementos.RegraAtende());
        }

        [TestMethod]
        public void ImparMenosQueMaximoElementos_RegraAtende_QuandoItemAtualEstaAbaixoDoMeioDaLista()
        {
            // Act
            int maximoPaginasNoGrupo = 5;
            int paginaAtual = 2;
            ImparMenosQueMaximoElementos imparMenosQueMaximoElementos = new ImparMenosQueMaximoElementos(new AjaxHelper(new ViewContext(), _mockViewDataContainer.Object), new TotalElementosLista(73), new PaginaAtualLista(paginaAtual), maximoPaginasNoGrupo, _mockLinkBuilderStrategy.Object);

            // Assert
            Assert.IsTrue(imparMenosQueMaximoElementos.RegraAtende());
        }

        [TestMethod]
        public void ImparMenosQueMaximoElementos_RegraAtende_QuandoItemAtualEstaExatamenteNoMeioDaLista()
        {
            // Act
            int maximoPaginasNoGrupo = 5;
            int paginaAtual = 3;
            ImparMenosQueMaximoElementos imparMenosQueMaximoElementos = new ImparMenosQueMaximoElementos(new AjaxHelper(new ViewContext(), _mockViewDataContainer.Object), new TotalElementosLista(73), new PaginaAtualLista(paginaAtual), maximoPaginasNoGrupo, _mockLinkBuilderStrategy.Object);

            // Assert
            Assert.IsTrue(imparMenosQueMaximoElementos.RegraAtende());
        }

        [TestMethod]
        public void ImparCentralizar_QuandoElementoAtualForSeisECentralizarForAplicavel_UltimaPaginaDoGrupoDeveSerOito()
        {
            // Act
            int maximoPaginasNoGrupo = 5;
            int elementoAtual = 6;
            int expectedUltimaPagina = 8;
            PaginaAtualLista paginaAtualLista = new PaginaAtualLista(elementoAtual);
            ImparCentralizar imparCentralizar = new ImparCentralizar(new TotalElementosLista(130), paginaAtualLista, maximoPaginasNoGrupo, _mockLinkBuilderStrategy.Object);

            // Assert
            Assert.AreEqual(expectedUltimaPagina, imparCentralizar.GetUltimaPaginaGrupoAtual());
        }

        [TestMethod]
        public void ImparCentralizar_RegraNaoAtende_QuandoMaximoDeLinksDePaginaçãoNoGrupoNaoEImpar()
        {
            // Act
            int maximoPaginasNoGrupo = 6;
            int paginaAtual = 4;
            int expected = paginaAtual - 1;
            ImparCentralizar imparCentralizar = new ImparCentralizar(new TotalElementosLista(73), new PaginaAtualLista(paginaAtual), maximoPaginasNoGrupo, _mockLinkBuilderStrategy.Object);

            // Assert
            Assert.IsFalse(imparCentralizar.RegraAtende());
        }

        [TestMethod]
        public void ImparCentralizar_RegraNaoAtende_QuandoElementoAtualForMaiorQueMeioDoUltimoGrupoDePaginacao()
        {
            // Act
            int maximoPaginasNoGrupo = 5;
            int paginaAtual = 6;
            ImparCentralizar imparCentralizar = new ImparCentralizar(new TotalElementosLista(74), new PaginaAtualLista(paginaAtual), maximoPaginasNoGrupo, _mockLinkBuilderStrategy.Object);

            // Assert
            Assert.IsFalse(imparCentralizar.RegraAtende());
        }

        [TestMethod]
        public void ImparCentralizar_QuandoElementoAtualForIgualAMeioDoUltimoGrupoDePaginacao_RegraAtende_()
        {
            // Act
            int maximoPaginasNoGrupo = 5;
            int paginaAtual = 6;
            ImparCentralizar imparCentralizar = new ImparCentralizar(new TotalElementosLista(89), new PaginaAtualLista(paginaAtual), maximoPaginasNoGrupo, _mockLinkBuilderStrategy.Object);

            // Assert
            Assert.IsTrue(imparCentralizar.RegraAtende());
        }

        [TestMethod]
        public void ImparCentralizar_RegraNaoAtende_QuandoElementoAtualForMenorQueMeioDaLista()
        {
            // Act
            int maximoPaginasNoGrupo = 5;
            int paginaAtual = 2;
            ImparCentralizar imparCentralizar = new ImparCentralizar(new TotalElementosLista(115), new PaginaAtualLista(paginaAtual), maximoPaginasNoGrupo, _mockLinkBuilderStrategy.Object);

            // Assert
            Assert.IsFalse(imparCentralizar.RegraAtende());
        }

        [TestMethod]
        public void ImparCentralizar_RegraNaoAtende_QuandoTotalElementosMenorQueMaximoDePaginasNoGrupo()
        {
            // Act
            int maximoPaginasNoGrupo = 5;
            int paginaAtual = 4;
            ImparCentralizar imparCentralizar = new ImparCentralizar(new TotalElementosLista(44), new PaginaAtualLista(paginaAtual), maximoPaginasNoGrupo, _mockLinkBuilderStrategy.Object);

            // Assert
            Assert.IsFalse(imparCentralizar.RegraAtende());
        }

        [TestMethod]
        public void ImparUltimoGrupo_QuandoElementoAtualForTrezeTotalDePaginasForTrezeEPaginasNoGrupoForSete_RegraDeveAtender()
        {
            // Act
            int maximoPaginasNoGrupo = 7;
            int elementoAtual = 13;
            PaginaAtualLista paginaAtualLista = new PaginaAtualLista(elementoAtual);
            UltimoGrupoElementos ultimoGrupoElementos = new UltimoGrupoElementos(new AjaxHelper(new ViewContext(), _mockViewDataContainer.Object), new TotalElementosLista(132), paginaAtualLista, maximoPaginasNoGrupo, _mockLinkBuilderStrategy.Object);

            // Assert
            Assert.IsTrue(ultimoGrupoElementos.RegraAtende());
        }

        [TestMethod]
        public void ImparUltimoGrupo_QuandoElementoAtualForOnzeTotalDePaginasForTrezeEPaginasNoGrupoForSete_RegraDeveAtender()
        {
            // Act
            int maximoPaginasNoGrupo = 7;
            int elementoAtual = 11;
            PaginaAtualLista paginaAtualLista = new PaginaAtualLista(elementoAtual);
            UltimoGrupoElementos ultimoGrupoElementos = new UltimoGrupoElementos(new AjaxHelper(new ViewContext(), _mockViewDataContainer.Object), new TotalElementosLista(132), paginaAtualLista, maximoPaginasNoGrupo, _mockLinkBuilderStrategy.Object);

            // Assert
            Assert.IsTrue(ultimoGrupoElementos.RegraAtende());
        }

        [TestMethod]
        public void ImparUltimoGrupo_QuandoElementoAtualForDezTotalDePaginasForTrezeEPaginasNoGrupoForSete_RegraNaoDeveAtender()
        {
            // Act
            int maximoPaginasNoGrupo = 7;
            int elementoAtual = 10;
            PaginaAtualLista paginaAtualLista = new PaginaAtualLista(elementoAtual);
            UltimoGrupoElementos ultimoGrupoElementos = new UltimoGrupoElementos(new AjaxHelper(new ViewContext(), _mockViewDataContainer.Object), new TotalElementosLista(132), paginaAtualLista, maximoPaginasNoGrupo, _mockLinkBuilderStrategy.Object);

            // Assert
            Assert.IsFalse(ultimoGrupoElementos.RegraAtende());
        }

        [TestMethod]
        public void QuandoPaginacaoPodeTratarRequisicao_EntaoHtmlComListaDeveSerMontado(){
            // Arrange
            List<string> htmlItems = new List<string>{"<1/>", "<2/>", "<3/>"};
            _mockLinkBuilderStrategy.Setup(p => p.Build(It.IsAny<long>(), It.IsAny<long>())).Returns(htmlItems);
            _mockLinkBuilderStrategy.Setup(p => p.SubstituirElementoAtual(3)).Returns(htmlItems);
            string esperado = @"<div class=""btn-group""><1/><2/><3/></div>";
            TotalElementosLista totalElementosLista = new TotalElementosLista(10);

            // Act
            string real = new PaginacaoTesteUnitario(null, totalElementosLista, null, 5, _mockLinkBuilderStrategy.Object).Get().ToString();

            // Assert
            Assert.AreEqual(esperado, real);
        }

        [TestMethod]
        public void QuandoPaginacaoNaoPodeTratarRequisicaoENaoHaMaisNinguemNaCadeiaDeRequisicoes_EntaoHtmlComDivVaziaDeveSerRenderizado()
        {
            // Arrange
            string esperado = @"<div></div>";
            TotalElementosLista totalElementosLista = new TotalElementosLista(10);

            // Act
            PaginacaoTesteUnitario paginacao = new PaginacaoTesteUnitario(null, totalElementosLista, null, 5, _mockLinkBuilderStrategy.Object);
            paginacao.MudarRegraAtende(false);
            string real = paginacao.Get().ToString();

            // Assert
            Assert.AreEqual(esperado, real);
        }

        private class PaginacaoTesteUnitario : AbstractPaginacao
        {
            public PaginacaoTesteUnitario(AjaxHelper ajaxHelper, TotalElementosLista listaTotalElementos, PaginaAtualLista listaElementoAtual, int maximoElementosPaginacao, ILinkBuilderStrategy linkBuilderStrategy)
                : base(listaTotalElementos, listaElementoAtual, maximoElementosPaginacao, linkBuilderStrategy)
            {

            }

            public override bool RegraAtende()
            {
                return _regraAtendeVolatil;
            }

            public override long GetPrimeiraPaginaGrupoAtual()
            {
                return 1;
            }

            public override long GetUltimaPaginaGrupoAtual()
            {
                return 1;
            }

            public override long GetElementoParaSubstituir()
            {
                return 3;
            }

            public void MudarRegraAtende(bool novoValor){
                _regraAtendeVolatil = novoValor;
            }
            
            private bool _regraAtendeVolatil = true;
        }


    }
}