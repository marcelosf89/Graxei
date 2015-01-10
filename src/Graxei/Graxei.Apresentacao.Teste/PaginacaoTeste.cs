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
        private Mock<IViewDataContainer> mockViewDataContainer;

        private Mock<ILinkBuilderStrategy> mockLinkBuilderStrategy;

        [TestInitialize]
        public void SetUp()
        {
            mockViewDataContainer = new Mock<IViewDataContainer>();
            mockLinkBuilderStrategy = new Mock<ILinkBuilderStrategy>();
        }

        [TestMethod]
        public void ImparMenosQueMaximoElementos_FimListaIgualTotalPaginas_QuandoMaximoPaginasNoGrupoMaiorQueTotalElementos()
        {
            // Act
           int maximoPaginasNoGrupo = 5;
           int totalElementos = 45;
           int expected = (totalElementos / 10);
           ImparMenosQueMaximoElementos imparMenosQueMaximoElementos = new ImparMenosQueMaximoElementos(new AjaxHelper(new ViewContext(), mockViewDataContainer.Object), new TotalElementosLista(totalElementos), It.IsAny<PaginaAtualLista>(), maximoPaginasNoGrupo, mockLinkBuilderStrategy.Object);

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
            ImparMenosQueMaximoElementos imparMenosQueMaximoElementos = new ImparMenosQueMaximoElementos(new AjaxHelper(new ViewContext(), mockViewDataContainer.Object), new TotalElementosLista(totalElementos), It.IsAny<PaginaAtualLista>(), maximoPaginasNoGrupo, mockLinkBuilderStrategy.Object);

            // Assert
            Assert.AreEqual(maximoPaginasNoGrupo, imparMenosQueMaximoElementos.GetUltimaPaginaGrupoAtual());
        }

        [TestMethod]
        public void ImparMenosQueMaximoElementos_InicioListaIgualUm()
        {
            // Act
            int maximoPaginasNoGrupo = 5;
            int totalElementos = 543;
            ImparMenosQueMaximoElementos imparMenosQueMaximoElementos = new ImparMenosQueMaximoElementos(new AjaxHelper(new ViewContext(), mockViewDataContainer.Object), new TotalElementosLista(totalElementos), It.IsAny<PaginaAtualLista>(), maximoPaginasNoGrupo, mockLinkBuilderStrategy.Object);

            // Assert
            Assert.AreEqual(1, imparMenosQueMaximoElementos.GetPrimeiraPaginaGrupoAtual());
        }

        [TestMethod]
        public void ImparMenosQueMaximoElementos_ElementoSubstituirIgualAAtualMenos1()
        {
            // Act
            int maximoPaginasNoGrupo = 7;
            int paginaAtual = 2;
            int expected = paginaAtual - 1;
            ImparMenosQueMaximoElementos imparMenosQueMaximoElementos = new ImparMenosQueMaximoElementos(new AjaxHelper(new ViewContext(), mockViewDataContainer.Object), new TotalElementosLista(73), new PaginaAtualLista(paginaAtual), maximoPaginasNoGrupo, mockLinkBuilderStrategy.Object);

            // Assert
            Assert.AreEqual(expected, imparMenosQueMaximoElementos.GetElementoParaSubstituir());
        }

        [TestMethod]
        public void ImparMenosQueMaximoElementos_RegraNaoAtende_QuandoMaximoDeLinksDePaginaçãoNoGrupoNaoEImpar()
        {
            // Act
            int maximoPaginasNoGrupo = 6;
            int paginaAtual = 2;
            int expected = paginaAtual - 1;
            ImparMenosQueMaximoElementos imparMenosQueMaximoElementos = new ImparMenosQueMaximoElementos(new AjaxHelper(new ViewContext(), mockViewDataContainer.Object), new TotalElementosLista(73), new PaginaAtualLista(paginaAtual), maximoPaginasNoGrupo, mockLinkBuilderStrategy.Object);

            // Assert
            Assert.IsFalse(imparMenosQueMaximoElementos.RegraAtende());
        }

        [TestMethod]
        public void ImparMenosQueMaximoElementos_RegraNaoAtende_QuandoItemAtualEstaAlemDoMeioDaLista()
        {
            // Act
            int maximoPaginasNoGrupo = 5;
            int paginaAtual = 4;
            ImparMenosQueMaximoElementos imparMenosQueMaximoElementos = new ImparMenosQueMaximoElementos(new AjaxHelper(new ViewContext(), mockViewDataContainer.Object), new TotalElementosLista(73), new PaginaAtualLista(paginaAtual), maximoPaginasNoGrupo, mockLinkBuilderStrategy.Object);

            // Assert
            Assert.IsFalse(imparMenosQueMaximoElementos.RegraAtende());
        }

        [TestMethod]
        public void ImparMenosQueMaximoElementos_RegraNaoAtende_QuandoItemAtualEstaAbaixoDoMeioDaListaMasElementoAtualEMaiorQueQuantidadePaginacaoPorVez()
        {
            // Act
            int maximoPaginasNoGrupo = 5;
            int paginaAtual = 6;
            ImparMenosQueMaximoElementos imparMenosQueMaximoElementos = new ImparMenosQueMaximoElementos(new AjaxHelper(new ViewContext(), mockViewDataContainer.Object), new TotalElementosLista(73), new PaginaAtualLista(paginaAtual), maximoPaginasNoGrupo, mockLinkBuilderStrategy.Object);

            // Assert
            Assert.IsFalse(imparMenosQueMaximoElementos.RegraAtende());
        }

        [TestMethod]
        public void ImparMenosQueMaximoElementos_RegraAtende_QuandoItemAtualEstaAbaixoDoMeioDaLista()
        {
            // Act
            int maximoPaginasNoGrupo = 5;
            int paginaAtual = 2;
            ImparMenosQueMaximoElementos imparMenosQueMaximoElementos = new ImparMenosQueMaximoElementos(new AjaxHelper(new ViewContext(), mockViewDataContainer.Object), new TotalElementosLista(73), new PaginaAtualLista(paginaAtual), maximoPaginasNoGrupo, mockLinkBuilderStrategy.Object);

            // Assert
            Assert.IsTrue(imparMenosQueMaximoElementos.RegraAtende());
        }

        [TestMethod]
        public void ImparMenosQueMaximoElementos_RegraAtende_QuandoItemAtualEstaExatamenteNoMeioDaLista()
        {
            // Act
            int maximoPaginasNoGrupo = 5;
            int paginaAtual = 3;
            ImparMenosQueMaximoElementos imparMenosQueMaximoElementos = new ImparMenosQueMaximoElementos(new AjaxHelper(new ViewContext(), mockViewDataContainer.Object), new TotalElementosLista(73), new PaginaAtualLista(paginaAtual), maximoPaginasNoGrupo, mockLinkBuilderStrategy.Object);

            // Assert
            Assert.IsTrue(imparMenosQueMaximoElementos.RegraAtende());
        }
    }
}