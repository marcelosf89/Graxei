using Graxei.Comum.Teste;
using Graxei.Persistencia.Contrato.PesquisaProduto;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.ContratosDeDados.Listas;
using Graxei.Transversais.ContratosDeDados.TinyTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Graxei.Negocio.Implementacao.Teste
{
    [TestClass]
    public class TesteServicoProdutoVendedor
    {
        [TestInitialize]
        public void SetUp()
        {
            _mockPesquisaProdutoFactory = new Mock<IPesquisaProdutoFactory>();
        }

        [TestMethod]
        public void GetUltimaPagina_DeveRetornarListaPesquisaContrato()
        {
            // Arrange
            string criterio = "criterio";
            MinhaPesquisaProdutoRepositorioTeste pesquisaProdutoRepositorio = new MinhaPesquisaProdutoRepositorioTeste();
            _mockPesquisaProdutoFactory.Setup(p => p.Get(criterio)).Returns(new MinhaPesquisaProdutoRepositorioTeste());
            ListaPesquisaContrato esperado = pesquisaProdutoRepositorio.GetEsperado();

            // Act
            ServicoProdutoVendedor servico = new ServicoProdutoVendedor(null, null, null, null, null, null, _mockPesquisaProdutoFactory.Object);
            ListaPesquisaContrato real = servico.GetUltimaPagina(criterio);

            // Assert
            Assert.IsTrue(PesquisaContratoComum.AssertListaPesquisaContrato(esperado, real));
        }

        private Mock<IPesquisaProdutoFactory> _mockPesquisaProdutoFactory;

        private class MinhaPesquisaProdutoRepositorioTeste : IPesquisaProdutoRepositorio
        {
            public IList<PesquisaContrato> Get(int pagina)
            {
                return null;
            }

            public ListaPesquisaContrato GetUltimaPagina(int tamanhoPagina)
            {
                return GetEsperado();
            }

            public ListaPesquisaContrato GetEsperado()
            {
                IList<PesquisaContrato> lista = PesquisaContratoComum.GetLista();
                TotalElementosLista totalElementos = new TotalElementosLista(1);
                PaginaAtualLista elementoAtual = new PaginaAtualLista(1);
                return new ListaPesquisaContrato(lista, totalElementos, elementoAtual);
            }

        }
    }
}
