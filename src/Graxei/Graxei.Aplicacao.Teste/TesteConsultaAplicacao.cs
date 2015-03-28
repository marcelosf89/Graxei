using Graxei.Aplicacao.Implementacao.Consultas;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Negocio.Contrato.Factories;
using Graxei.Persistencia.Implementacao.Teste;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.ContratosDeDados.Listas;
using Graxei.Transversais.ContratosDeDados.TinyTypes;
using Graxei.Transversais.Utilidades;
using Graxei.Transversais.Utilidades.Entidades;
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
    public class TesteConsultaAplicacao
    {
        [TestInitialize]
        public void SetUp()
        {
            _mockBairroBuilder = new Mock<IBairrosBuilder>();
            _mockBairroBuilder.Setup(p => p.SetNome(NomeBairro)).Returns(_mockBairroBuilder.Object);
            _mockBairroBuilder.Setup(p => p.SetCidade(It.IsAny<string>())).Returns(_mockBairroBuilder.Object);
            _mockBairroBuilder.Setup(p => p.SetIdEstado(It.IsAny<long>())).Returns(_mockBairroBuilder.Object);
            _mockServicoBairros = new Mock<IServicoBairros>();
            _mockServicoCidades = new Mock<IServicoCidades>();
            _mockServicoEstados = new Mock<IServicoEstados>();
            _mockServicoEnderecos = new Mock<IServicoEnderecos>();
            _mockServicoUsuarios = new Mock<IServicoUsuarios>();
        }

        [TestCleanup]
        public void Clear()
        {
            _mockBairroBuilder = null;
            _mockServicoBairros = null;
            _mockServicoCidades = null;
            _mockServicoEstados = null;
            _mockServicoEnderecos = null;
            _mockServicoUsuarios = null;
        }

        [TestMethod]
        public void ConsultaListaProdutosLoja_RecuperarLista()
        {
            // Arrange
            ListaProdutosLoja expectedListaProdutosLoja = RepositorioCommon.Construir(RepositorioCommon.GetDoisElementos(), 1, 1);
            Mock<IServicoListaProdutosLoja> mockRepositorioListaProdutosLoja = new Mock<IServicoListaProdutosLoja>();
            mockRepositorioListaProdutosLoja.Setup(p => p.Get(It.IsAny<PesquisaProdutoContrato>(), It.IsAny<int>())).Returns(expectedListaProdutosLoja);

            // Act
            ListaProdutosLoja actualListaProdutosLoja = new ConsultaListaProdutosLoja(mockRepositorioListaProdutosLoja.Object).Get(It.IsAny<PesquisaProdutoContrato>(), It.IsAny<int>());

            // Assert
            Assert.AreEqual(expectedListaProdutosLoja, actualListaProdutosLoja);
        }

        [TestMethod]
        public void ConsultaLogradouro_RecuperarLista()
        {
            // Arrange
            IList<Logradouro> esperado = GetListaComDoisLogradouros();
            Mock<IServicoLogradouros> mockLogradouros = new Mock<IServicoLogradouros>();
            mockLogradouros.Setup(p => p.GetPorBairro("bairro", "cidade", 1)).Returns(esperado);

            // Act
            IList<Logradouro> real = new ConsultaLogradouros(mockLogradouros.Object).Get("bairro", "cidade", 1);

            // Assert
            Assert.AreEqual(esperado[1].Nome, real[1].Nome);
        }

        [TestMethod]
        public void ConsultaBairro_RecuperarBairroViaGet()
        {
            // Arrange
            Bairro esperado = new Bairro { Nome = NomeBairro };
            _mockBairroBuilder.Setup(p => p.Build()).Returns(esperado);

            // Act
            Bairro real = new ConsultaBairros(_mockServicoBairros.Object, _mockBairroBuilder.Object).Get(NomeBairro, string.Empty, 1);

            // Assert
            Assert.AreEqual(esperado.Nome, real.Nome);
        }

        [TestMethod]
        public void ConsultaBairro_RecuperarBairroViaGetPorId()
        {
            //Arrange
            Bairro esperado = new Bairro { Nome = NomeBairro };
            _mockServicoBairros.Setup(p => p.GetPorId(1)).Returns(esperado);

            // Act
            Bairro real = new ConsultaBairros(_mockServicoBairros.Object, null).Get(1);

            // Assert
            Assert.AreEqual(esperado.Nome, real.Nome);
        }

        [TestMethod]
        public void ConsultaBairro_RecuperarBairroViaGetPorCidade()
        {
            //Arrange
            Bairro esperado = new Bairro { Nome = NomeBairro };
            _mockServicoBairros.Setup(p => p.GetPorId(1)).Returns(esperado);

            // Act
            Bairro real = new ConsultaBairros(_mockServicoBairros.Object, null).Get(1);

            // Assert
            Assert.AreEqual(esperado.Nome, real.Nome);
        }

        [TestMethod]
        public void ConsultaBairro_RecuperarListaPorCidadeEstado()
        {
            string nomeCidade = "Cidade 1";
            long idEstado = 1;
            Bairro bairro1 = new Bairro { Nome = "Palitinho", Cidade = new Cidade { Nome = nomeCidade, Estado = new Estado { Sigla = "RJ" } } };
            Bairro bairro2 = new Bairro { Nome = "Palmitinho", Cidade = new Cidade { Nome = nomeCidade, Estado = new Estado { Sigla = "MG" } } };
            IList<Bairro> esperado = new List<Bairro>();
            esperado.Add(bairro1);
            esperado.Add(bairro2);
            IList<Bairro> forMock = new List<Bairro>(esperado);

            _mockServicoBairros.Setup(p => p.GetPorCidade(nomeCidade, idEstado)).Returns(forMock);

            // Act
            IList<Bairro> real = new ConsultaBairros(_mockServicoBairros.Object, null).GetPorCidade(nomeCidade, idEstado);
            
            // Assert
            Assert.IsTrue(Listas.ListasIguais<Bairro>(real, esperado));

        }
        
        [TestMethod]
        public void ConsultaCidade_RecuperarListaPorEstado()
        {
            // Arrange
            IList<Cidade> esperado = GetListaComDuasCidades();
            _mockServicoCidades.Setup(p => p.GetPorEstado(1)).Returns(esperado);

            // Act
            IList<Cidade> real = new ConsultaCidades(_mockServicoCidades.Object).GetPorEstado(1);
            
            // Assert
            Assert.AreEqual(esperado[1].Nome, real[1].Nome);
        }

        [TestMethod]
        public void ConsultaListaProdutosLojas_RecuperarLista()
        {
            // Arrange
            PesquisaProdutoContrato pesquisa = new PesquisaProdutoContrato{ IdLoja = 1 };
            ListaProdutosLoja esperado = new ListaProdutosLoja(null, new TotalElementosLista(10), new PaginaAtualLista(5));
            Mock<IServicoListaProdutosLoja> mock = new Mock<IServicoListaProdutosLoja>();
            mock.Setup(p => p.Get(pesquisa, 5)).Returns(esperado);

            // Act
            ListaProdutosLoja real = new ConsultaListaProdutosLoja(mock.Object).Get(pesquisa, 5);

            // Assert
            Assert.AreEqual(esperado.Atual, real.Atual);
        }

        [TestMethod]
        public void ConsultaCidade_RecuperarListaNomeEEstado()
        {
            // Arrange
            Cidade esperado = new Cidade { Nome = NomeCidade };
            _mockServicoCidades.Setup(p => p.Get(NomeCidade, IdEstado)).Returns(esperado);

            // Act
            Cidade real = new ConsultaCidades(_mockServicoCidades.Object).Get(NomeCidade, IdEstado);

            // Assert
            Assert.AreEqual(esperado.Nome, real.Nome);
        }

        [TestMethod]
        public void ConsultaEnderecos_RecuperarListaPorLoja()
        {
            // Arrange
            List<Endereco> esperado = GetListaComDoisEnderecos();
            _mockServicoEnderecos.Setup(p => p.GetPorLoja(IdLoja)).Returns(esperado);

            // Act
            List<Endereco> real = new ConsultaEnderecos(_mockServicoEnderecos.Object).GetPorLoja(IdLoja);

            // Assert
            Assert.AreEqual(esperado[1].Numero, real[1].Numero);
        }

        [TestMethod]
        public void ConsultaEnderecos_RecuperarPorId()
        {
            // Arrange
            Endereco esperado = new Endereco { Numero = NumeroEndereco };
            _mockServicoEnderecos.Setup(p => p.Get(1)).Returns(esperado);

            // Act
            Endereco real = new ConsultaEnderecos(_mockServicoEnderecos.Object).Get(1);

            // Assert
            Assert.AreEqual(esperado.Numero, real.Numero);
        }

        [TestMethod]
        public void ConsultaEstados_RecuperarPorId()
        {
            // Arrange
            Estado esperado = new Estado { Sigla = SiglaEstado };
            _mockServicoEstados.Setup(p => p.GetPorId(1)).Returns(esperado);

            // Act
            Estado real = new ConsultaEstados(_mockServicoEstados.Object).Get(1);

            // Assert
            Assert.AreEqual(esperado.Sigla, real.Sigla);
        }

        [TestMethod]
        public void ConsultaEstados_RecuperarLista()
        {
            // Arrange
            IList<Estado> esperado = GetListaComDoisEstados();
            _mockServicoEstados.Setup(p => p.Todos(EstadoOrdem.Sigla)).Returns(esperado);

            // Act
            IList<Estado> real = new ConsultaEstados(_mockServicoEstados.Object).GetEstados(EstadoOrdem.Sigla);

            // Assert
            Assert.AreEqual(esperado[1].Sigla, real[1].Sigla);
        }

        [TestMethod]
        public void ConsultaFabricantes_RecuperarLista()
        {
            // Arrange
            List<string> esperado = new List<string> { "Fabricante 1", "Fabricante 2" };

            Mock<IServicoFabricantes> mockServicoFabricantes = new Mock<IServicoFabricantes>();
            mockServicoFabricantes.Setup(p => p.TodosNomes()).Returns(esperado);

            // Act
            IList<string> real = new ConsultaFabricantes(mockServicoFabricantes.Object).TodosNomes();

            // Assert
            Assert.AreEqual(esperado[1], real[1]);
        }

        [TestMethod]
        public void ConsultaLogin_AutenticarPorLogin()
        {
            // Arrange
            Usuario esperado = GetUsuario();
            _mockServicoUsuarios.Setup(p => p.AutenticarPorLogin(UsuarioLogin, "*****")).Returns(esperado);

            // Act
            Usuario real = new ConsultaLogin(_mockServicoUsuarios.Object).AutenticarPorLogin(UsuarioLogin, "*****");

            // Assert
            Assert.AreEqual(esperado.Login, real.Login);
        }

        [TestMethod]
        public void ConsultaLogin_GetPorNome()
        {
            // Arrange
            Usuario esperado = GetUsuario();
            _mockServicoUsuarios.Setup(p => p.GetPorNome(UsuarioLogin)).Returns(esperado);

            // Act
            Usuario real = new ConsultaLogin(_mockServicoUsuarios.Object).GetPorNome(UsuarioLogin);

            // Assert
            Assert.AreEqual(esperado.Login, real.Login);
        }
        private Usuario GetUsuario()
        {
            return new Usuario { Login = UsuarioLogin };
        }

        private IList<Logradouro> GetListaComDoisLogradouros()
        {
            IList<Logradouro> logradouros = new List<Logradouro>();
            Logradouro logradouro = new Logradouro();
            logradouro.Nome = "Logradouro 1";
            logradouros.Add(logradouro);
            logradouro = new Logradouro();
            logradouro.Nome = "Logradouro 2";
            logradouros.Add(logradouro);
            return logradouros;
        }

        private IList<Cidade> GetListaComDuasCidades()
        {
            IList<Cidade> cidades = new List<Cidade>();
            Cidade cidade = new Cidade();
            cidade.Nome = "Cidade 1";
            cidades.Add(cidade);
            cidade = new Cidade();
            cidade.Nome = "Cidade 2";
            cidades.Add(cidade);
            return cidades;
        }

        private List<Endereco> GetListaComDoisEnderecos()
        {
            List<Endereco> enderecos = new List<Endereco>();
            Endereco endereco = new Endereco { Numero = "54321" };
            enderecos.Add(endereco);
            endereco = new Endereco { Numero = NumeroEndereco };
            enderecos.Add(endereco);
            return enderecos;
        }

        private List<Estado> GetListaComDoisEstados()
        {
            List<Estado> estados = new List<Estado>();
            Estado estado = new Estado { Sigla = "AA" };
            estados.Add(estado);
            estado = new Estado { Sigla = SiglaEstado };
            estados.Add(estado);
            return estados;
        }
        
        private const string NomeBairro = "Bairro 1";
        private const string NomeCidade = "Cidade 1";
        private const string NumeroEndereco = "2048";
        private const long IdEstado = 2;
        private const long IdLoja = 101;
        private const string SiglaEstado = "ZZ";
        private const string UsuarioLogin = "adminuser";
        private Mock<IBairrosBuilder> _mockBairroBuilder;
        private Mock<IServicoBairros> _mockServicoBairros;
        private Mock<IServicoCidades> _mockServicoCidades;
        private Mock<IServicoEstados> _mockServicoEstados;
        private Mock<IServicoEnderecos> _mockServicoEnderecos;
        private Mock<IServicoUsuarios> _mockServicoUsuarios;
    }
}
