using Graxei.Apresentacao.Areas.Administrativo.Infraestutura;
using Graxei.Apresentacao.Areas.Administrativo.Infraestutura.Cache;
using Graxei.Modelo;
using Graxei.Transversais.Utilidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Graxei.Apresentacao.Teste.AreaAdministrativo
{
    [TestClass]
    public class TesteCacheEnderecos
    {
        [TestInitialize]
        public void SetUp()
        {
            _mockSessionState = new Mock<HttpSessionStateBase>();
        }

        [TestMethod]
        public void CacheSessaoHttp_DeveRetornarAsCidadesVindasDoCache()
        {
            // Arrange
            IList<Cidade> esperado = GetCidades();
            IList<Cidade> mockCidades = new List<Cidade>(esperado);
            _mockSessionState.Setup(p => p[ChavesSessao.CidadesAtual]).Returns(mockCidades);
            
            // Act
            CacheEnderecosSessaoHttp cache = new CacheEnderecosSessaoHttp(_mockSessionState.Object);
            IList<Cidade> real = cache.GetCidades();

            // Assert
            Assert.IsTrue(Listas.ListasIguais<Cidade>(esperado, real));
        }


        [TestMethod]
        public void CacheSessaoHttp_DeveRetornarOsBairrosVindosDoCache()
        {
            // Arrange
            IList<Bairro> esperado = GetBairros();
            IList<Bairro> mockBairros = new List<Bairro>(esperado);
            _mockSessionState.Setup(p => p[ChavesSessao.BairrosAtual]).Returns(mockBairros);

            // Act
            CacheEnderecosSessaoHttp cache = new CacheEnderecosSessaoHttp(_mockSessionState.Object);
            IList<Bairro> real = cache.GetBairros();

            // Assert
            Assert.IsTrue(Listas.ListasIguais<Bairro>(esperado, real));
        }

        [TestMethod]
        public void CacheSessaoHttp_DeveRetornarOsLogradourosVindosDoCache()
        {
            // Arrange
            IList<Logradouro> esperado = GetLogradouros();
            IList<Logradouro> mockLogradouros = new List<Logradouro>(esperado);
            _mockSessionState.Setup(p => p[ChavesSessao.LogradourosAtual]).Returns(mockLogradouros);

            // Act
            CacheEnderecosSessaoHttp cache = new CacheEnderecosSessaoHttp(_mockSessionState.Object);
            IList<Logradouro> real = cache.GetLogradouros();

            // Assert
            Assert.IsTrue(Listas.ListasIguais<Logradouro>(esperado, real));
        }

        [TestMethod]
        public void CacheSessaoHttp_DevePreencherAsCidadesNoCache()
        {
            // Arrange
            IList<Cidade> esperado = GetCidades();
            IList<Cidade> mockCidades = new List<Cidade>(esperado);
            _mockSessionState.SetupSet(p => p[ChavesSessao.CidadesAtual] = esperado);

            // Act
            CacheEnderecosSessaoHttp cache = new CacheEnderecosSessaoHttp(_mockSessionState.Object);
            cache.SetCidades(esperado);

            // Assert
            _mockSessionState.VerifySet(p => p[ChavesSessao.CidadesAtual] = esperado, Times.Once);
        }

        [TestMethod]
        public void CacheSessaoHttp_DevePreencherOsBairrosNoCache()
        {
            // Arrange
            IList<Bairro> esperado = GetBairros();
            IList<Bairro> mockBairros = new List<Bairro>(esperado);
            _mockSessionState.SetupSet(p => p[ChavesSessao.BairrosAtual] = esperado);

            // Act
            CacheEnderecosSessaoHttp cache = new CacheEnderecosSessaoHttp(_mockSessionState.Object);
            cache.SetBairros(esperado);

            // Assert
            _mockSessionState.VerifySet(p => p[ChavesSessao.BairrosAtual] = esperado, Times.Once);
        }

        [TestMethod]
        public void CacheSessaoHttp_DevePreencherOsLogradourosNoCache()
        {
            // Arrange
            IList<Logradouro> esperado = GetLogradouros();
            IList<Logradouro> mockLogradouros = new List<Logradouro>(esperado);
            _mockSessionState.SetupSet(p => p[ChavesSessao.LogradourosAtual] = esperado);

            // Act
            CacheEnderecosSessaoHttp cache = new CacheEnderecosSessaoHttp(_mockSessionState.Object);
            cache.SetLogradouros(esperado);

            // Assert
            _mockSessionState.VerifySet(p => p[ChavesSessao.LogradourosAtual] = esperado, Times.Once);
        }
        private IList<Cidade> GetCidades()
        {
            IList<Cidade> cidades = new List<Cidade>();
            Cidade cidade = new Cidade { Nome = "Bangkok", Estado = new Estado { Sigla = "RJ" } };
            cidades.Add(cidade);
            cidade = new Cidade() { Nome = "Hanoi", Estado = new Estado { Sigla = "MG" } };
            cidades.Add(cidade);
            return cidades;
        }

        private IList<Bairro> GetBairros()
        {
            IList<Cidade> cidades = GetCidades();
            IList<Bairro> bairros = new List<Bairro>();
            Bairro bairro = new Bairro { Nome = "Periférico", Cidade = cidades[0] };
            bairros.Add(bairro);
            bairro = new Bairro { Nome = "Central", Cidade = cidades[1] };
            bairros.Add(bairro);
            return bairros;
        }

        private IList<Logradouro> GetLogradouros()
        {
            IList<Bairro> bairros = GetBairros();
            IList<Logradouro> logradouros = new List<Logradouro>();
            Logradouro logradouro = new Logradouro{ Nome = "Rua XYZ", Bairro = bairros[0] };
            logradouros.Add(logradouro);
            logradouro = new Logradouro { Nome = "Rua ABC", Bairro = bairros[1] };
            logradouros.Add(logradouro);
            return logradouros;
        }
        private Mock<HttpSessionStateBase> _mockSessionState; 
    }
}
