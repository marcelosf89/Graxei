using Graxei.Aplicacao.Contrato.Transacionais;
using Graxei.Aplicacao.Implementacao.Consultas;
using Graxei.Negocio.Contrato;
using Graxei.Transversais.Comum.Data;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.ContratosDeDados.Api.PesquisaProdutos;
using Graxei.Transversais.ContratosDeDados.Listas;
using Graxei.Transversais.ContratosDeDados.TinyTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Aplicacao.Teste
{
    [TestClass]
    public class TesteConsultaProdutoVendedorGet
    {
        [TestInitialize]
        public void SetUp()
        {
            _mockPesquisaProduto = new Mock<IPesquisaProduto>();
            _mockServicoProdutoVendedor = new Mock<IServicoProdutoVendedor>();
            _mockDataSistema = new Mock<IDataSistema>();
            _consultasProdutoVendedor = new ConsultasProdutoVendedor(_mockServicoProdutoVendedor.Object, _mockPesquisaProduto.Object, _mockDataSistema.Object);
        }

        [TestMethod]
        public void DeveRetornarResultadoQuandoListaForMaiorQueZero()
        {
            // Arrange / Act
            ListaPesquisaContrato esperado = GetComum();
            ListaPesquisaContrato real = GetRealDeListaNaoVazia(esperado);

            // Assert
            _mockServicoProdutoVendedor.Verify(p => p.GetUltimaPagina(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            Assert.IsTrue(AssertListas(esperado, real));

        }

        [TestMethod]
        public void DeveAferirRegistroEmHistoricoQuandListaNaoForVazia()
        {
            DateTime data = DateTime.UtcNow;
            HistoricoPesquisa historicoPesquisa = new HistoricoPesquisa
            {
                Criterio = _texto,
                InternetProtocol = ip,
                DataPesquisa = data
            };
            _mockDataSistema.Setup(p => p.Agora).Returns(data);

            ListaPesquisaContrato real = GetRealDeListaNaoVazia(null);

            // Assert
            _mockPesquisaProduto.Verify(p => p.RegistrarAsync(historicoPesquisa), Times.Once);

        }

        [TestMethod]
        public void DeveRetornarResultadoComUltimaPaginaQuandoListaForVazia()
        {
            // Arrange / Act
            ListaPesquisaContrato esperado = GetComum();
            ListaPesquisaContrato real = GetRealDeListaVazia(esperado);

            // Assert
            _mockServicoProdutoVendedor.Verify(p => p.GetUltimaPagina(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.IsTrue(AssertListas(esperado, real));

        }

        [TestMethod]
        public void DeveAferirRegistroEmHistoricoQuandListaForVazia()
        {
            DateTime data = DateTime.UtcNow;
            HistoricoPesquisa historicoPesquisa = new HistoricoPesquisa
            {
                Criterio = _texto,
                InternetProtocol = ip,
                DataPesquisa = data
            };
            _mockDataSistema.Setup(p => p.Agora).Returns(data);

            ListaPesquisaContrato real = GetRealDeListaVazia(null);

            // Assert
            _mockPesquisaProduto.Verify(p => p.RegistrarAsync(historicoPesquisa), Times.Once);

        }

        private ListaPesquisaContrato GetRealDeListaNaoVazia(ListaPesquisaContrato esperado)
        {
            if (esperado == null)
            {
                esperado = GetComum();
            }
            _mockServicoProdutoVendedor.Setup(p => p.Get(_texto, _pais, _cidade, _pagina)).Returns(esperado);

            return _consultasProdutoVendedor.Get(_texto, _pais, _cidade, _pagina, ip);
        }

        private ListaPesquisaContrato GetRealDeListaVazia(ListaPesquisaContrato esperado)
        {
            if (esperado == null)
            {
                esperado = GetComum();
            }
            ListaPesquisaContrato vazio = new ListaPesquisaContrato(new List<PesquisaContrato>(), new TotalElementosLista(0), new PaginaAtualLista(0));

            _mockServicoProdutoVendedor.Setup(p => p.Get(_texto, _pais, _cidade, _pagina)).Returns(vazio);
            _mockServicoProdutoVendedor.Setup(p => p.GetUltimaPagina(_texto, _pais, _cidade)).Returns(esperado);

            return _consultasProdutoVendedor.Get(_texto, _pais, _cidade, _pagina, ip);
        }

        public static ListaPesquisaContrato GetComum()
        {
            PesquisaContrato pesquisaContrato = new PesquisaContrato { Codigo = "000", Preco = 10 };
            IList<PesquisaContrato> lista = new List<PesquisaContrato> { pesquisaContrato };
            pesquisaContrato = new PesquisaContrato { Codigo = "001", Preco = 11 };
            lista.Add(pesquisaContrato);

            return new ListaPesquisaContrato(lista, new TotalElementosLista(32), new PaginaAtualLista(3));
        }

        public static bool AssertListas(ListaPesquisaContrato listaPesquisa1, ListaPesquisaContrato listaPesquisa2)
        {
            if (listaPesquisa1.Lista.Count != listaPesquisa2.Lista.Count)
            {
                return false;
            }

            IList<string> lista1 = (from f in listaPesquisa1.Lista
                                    select string.Format("{0} --- {1}", f.Codigo, f.Preco)).ToList<string>();
            IList<string> lista2 = (from f in listaPesquisa2.Lista
                                    select string.Format("{0} --- {1}", f.Codigo, f.Preco)).ToList<string>();
            if (!(lista1.SequenceEqual(lista2) && lista2.SequenceEqual(lista1)))
            {
                return false;
            }

            return (listaPesquisa1.Atual.Valor == listaPesquisa2.Atual.Valor && listaPesquisa1.Total.Valor == listaPesquisa2.Total.Valor);

        }

        private Mock<IPesquisaProduto> _mockPesquisaProduto;

        private Mock<IServicoProdutoVendedor> _mockServicoProdutoVendedor;

        private Mock<IDataSistema> _mockDataSistema;

        private ConsultasProdutoVendedor _consultasProdutoVendedor;

        private string _texto = "consulta";

        private string _pais = "Nova Zelândia";
        
        private string _cidade = "Auckland";
        
        private int _pagina = 2;
        
        string ip = "127.0.0.1";
    }
}
