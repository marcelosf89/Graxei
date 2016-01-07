using Graxei.Comum.Teste;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.PesquisaProduto;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.ContratosDeDados.Listas;
using Graxei.Transversais.ContratosDeDados.TinyTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHibernate;
using System.Collections.Generic;

namespace Graxei.Persistencia.Implementacao.Teste
{
    [TestClass]
    public class TestePesquisaProduto
    {
        [TestInitialize]
        public void SetUp()
        {
            _mockSession = new Mock<ISession>();
            
        }

        [TestMethod]
        public void DeveRecuperarContratoDaUltimaPaginaPesquisada()
        {
            // Arrange
            string criterio = "pesquisando";
            IList<PesquisaContrato> lista = PesquisaContratoComum.GetLista();
            TotalElementosLista totalElementos = new TotalElementosLista(1);
            PaginaAtualLista elementoAtual = new PaginaAtualLista(1);
            ListaPesquisaContrato esperado = new ListaPesquisaContrato(lista, totalElementos, elementoAtual);

            _mockSession.Setup(p => p.CreateSQLQuery(It.IsAny<string>())
                                     .SetParameter<string>(It.IsAny<string>(), It.IsAny<string>())
                                     .SetParameter<double>(It.IsAny<string>(), It.IsAny<double>())
                                     .UniqueResult<long>()).Returns(1);

            // Act
            PesquisaProdutoMeuTeste pesquisaProduto = new PesquisaProdutoMeuTeste(criterio);
            pesquisaProduto.SessaoAtual = _mockSession.Object;
            ListaPesquisaContrato real = pesquisaProduto.GetUltimaPagina(1);

            // Assert
            Assert.IsTrue(PesquisaContratoComum.AssertListaPesquisaContrato(esperado, real));
        }

        private Mock<ISession> _mockSession;

        private class PesquisaProdutoMeuTeste : AbstractPesquisaProduto
        {
            public PesquisaProdutoMeuTeste(string criterio)
                : base(criterio)
            {
            }

            public override IList<PesquisaContrato> Get(int pagina)
            {
                return PesquisaContratoComum.GetLista();
            }
        }
    }
}
