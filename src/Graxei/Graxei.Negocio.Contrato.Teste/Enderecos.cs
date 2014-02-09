using Graxei.Modelo;
using Graxei.Negocio.Implementacao;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Utilidades.Excecoes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Negocio.Contrato.Teste
{
    [TestClass]
    public class Enderecos
    {
        [TestInitialize]
        public void Iniciar()
        {
            _mockRepositorioEnderecos = new Mock<IRepositorioEnderecos>();
            _mockServicoLogradouros = new Mock<IServicoLogradouros>();
            _mockServicoBairros = new Mock<IServicoBairros>();
            _mockServicoCidades = new Mock<IServicoCidades>();
            _mockServicoEstados = new Mock<IServicoEstados>();
        }

        [TestMethod]
        public void Endereco_ToStringLogradouroSemComplemento_Valido()
        {
            // Arrange
            string sigla = "RJ";
            string nomeCidade = "Rio de Janeiro";
            string nomeBairro = "Centro";
            string nomeLogradouro = "Avenida Rio Branco";
            string numero = "1";
            string enderecoEsperado = string.Format("{0}, {1} - {2} - {3} - {4}", nomeLogradouro, numero, nomeBairro, nomeCidade, sigla);
            Estado estado = new Estado();
            estado.Sigla = sigla;
            Cidade cidade = new Cidade();
            cidade.Nome = nomeCidade;
            cidade.Estado = estado;
            Bairro bairro = new Bairro();
            bairro.Nome = nomeBairro;
            bairro.Cidade = cidade;
            Endereco endereco = new Endereco();
            endereco.Logradouro = nomeLogradouro;
            endereco.Numero = numero;
            endereco.Bairro = bairro;

            // Act
            string enderecoReal = endereco.ToString();

            // Assert
            Assert.AreEqual(enderecoEsperado, enderecoReal, "Enderecos deveriam ser iguais");
        }

        [TestMethod]
        public void Endereco_ToStringLogradouroComComplemento_Valido()
        {
            // Arrange
            string sigla = "RJ";
            string nomeCidade = "Rio de Janeiro";
            string nomeBairro = "Centro";
            string nomeLogradouro = "Avenida Rio Branco";
            string numero = "1";
            string complemento = "Loja A";
            string enderecoEsperado = string.Format("{0}, {1}, {2} - {3} - {4} - {5}", nomeLogradouro, numero, complemento, nomeBairro, nomeCidade, sigla);
            Estado estado = new Estado();
            estado.Sigla = sigla;
            Cidade cidade = new Cidade();
            cidade.Nome = nomeCidade;
            cidade.Estado = estado;
            Bairro bairro = new Bairro();
            bairro.Nome = nomeBairro;
            bairro.Cidade = cidade;
            Endereco endereco = new Endereco();
            endereco.Logradouro = nomeLogradouro;
            endereco.Numero = numero;
            endereco.Complemento = complemento;
            endereco.Bairro = bairro;

            // Act
            string enderecoReal = endereco.ToString();

            // Assert
            Assert.AreEqual(enderecoEsperado, enderecoReal, "Enderecos deveriam ser iguais");
        }

        [TestMethod]
        public void Enderecos_EnderecosRepetidos_Verdade()
        {
            // Arrange
            IServicoEnderecos servicoEnderecos = new ServicoEnderecos(_mockRepositorioEnderecos.Object, _mockServicoLogradouros.Object, _mockServicoBairros.Object, _mockServicoCidades.Object, _mockServicoEstados.Object);

            string enderecoToString = "Endereço 1";
            Mock<Endereco> endereco1 = new Mock<Endereco>();
            endereco1.Setup(c => c.ToString()).Returns(enderecoToString);
            Mock<Endereco> endereco2 = new Mock<Endereco>();
            endereco2.Setup(c => c.ToString()).Returns(enderecoToString);
            IList<Endereco> enderecos = new List<Endereco>();
            enderecos.Add(endereco1.Object); enderecos.Add(endereco2.Object);

            // Act
            bool esperado = servicoEnderecos.EnderecosRepetidos(enderecos).Any();

            // Assert
            Assert.IsTrue(esperado, "Existem enderecos repetidos na lista");
        }

        [TestMethod]
        public void Enderecos_EnderecosRepetidos_Falso()
        {
            // Arrange
            IServicoEnderecos servicoEnderecos = new ServicoEnderecos(_mockRepositorioEnderecos.Object, _mockServicoLogradouros.Object, _mockServicoBairros.Object, _mockServicoCidades.Object, _mockServicoEstados.Object);
            int id = 0;
            string enderecoToString1 = "Endereço 1";
            string enderecoToString2 = "Endereço 2";

            Mock<Endereco> endereco1 = new Mock<Endereco>();
            endereco1.Setup(c => c.ToString()).Returns(enderecoToString1);
            Mock<Endereco> endereco2 = new Mock<Endereco>();
            endereco2.Setup(c => c.ToString()).Returns(enderecoToString2);
            IList<Endereco> enderecos = new List<Endereco>();
            enderecos.Add(endereco1.Object); enderecos.Add(endereco2.Object);

            // Act
            bool esperado = servicoEnderecos.EnderecosRepetidos(enderecos).Any();

            // Assert
            Assert.IsFalse(esperado, "Não existem enderecos repetidos na lista");
        }

        [TestMethod]
        [ExpectedException(typeof(EntidadeInvalidaException))]
        public void PreSalvar_EnderecosLojaNula_DeveDispararExcecao()
        {
            // Arrange
            IServicoEnderecos servicoEnderecos = new ServicoEnderecos(_mockRepositorioEnderecos.Object, _mockServicoLogradouros.Object, _mockServicoBairros.Object, _mockServicoCidades.Object, _mockServicoEstados.Object);
            Endereco endereco = new Endereco();

            // Act
            servicoEnderecos.PreSalvar(endereco);

            // Assert
            Assert.IsNull(endereco.Loja, "Loja deve ser nula");
        }

        [TestMethod]
        [ExpectedException(typeof(EntidadeInvalidaException))]
        public void PreSalvar_EnderecosLogradouroNaoInformado_DeveDispararExcecao()
        {
            // Arrange
            IServicoEnderecos servicoEnderecos = new ServicoEnderecos(_mockRepositorioEnderecos.Object, _mockServicoLogradouros.Object, _mockServicoBairros.Object, _mockServicoCidades.Object, _mockServicoEstados.Object);
            Endereco endereco = new Endereco();
            Loja loja = new Loja();
            loja.AdicionarEndereco(endereco);

            // Act
            servicoEnderecos.PreSalvar(endereco);

            // Assert
            Assert.IsNotNull(endereco.Loja, "Loja não pode ser nula");
        }

        [TestMethod]
        [ExpectedException(typeof(EntidadeInvalidaException))]
        public void PreSalvar_EnderecosNumeroNaoInformado_DeveDispararExcecao()
        {
            // Arrange
            IServicoEnderecos servicoEnderecos = new ServicoEnderecos(_mockRepositorioEnderecos.Object, _mockServicoLogradouros.Object, _mockServicoBairros.Object, _mockServicoCidades.Object, _mockServicoEstados.Object);
            Endereco endereco = new Endereco();
            endereco.Logradouro = "Rua A";
            Loja loja = new Loja();
            loja.AdicionarEndereco(endereco);
            
            // Act
            servicoEnderecos.PreSalvar(endereco);

            // Assert
            Assert.IsNotNull(endereco.Loja, "Loja não pode ser nula");
        }

        [TestMethod]
        [ExpectedException(typeof(ObjetoJaExisteException))]
        public void PreAtualizar_EnderecoJaExisteNaLoja_DeveDispararExcecao()
        {
            // Arrange
            Estado estado = new Estado();
            Cidade cidade = new Cidade();
            cidade.Estado = estado;
            Bairro bairro = new Bairro();
            bairro.Cidade = cidade;
            long id = 1;
            string logradouro = "Rua A";
            string numero = "1";
            Loja loja = new Loja();
            Mock<Endereco> endereco = new Mock<Endereco>();
            endereco.SetupProperty(p => p.Id, 1);
            endereco.SetupProperty(p => p.Logradouro, logradouro);
            endereco.SetupProperty(p => p.Numero, numero);
            endereco.SetupProperty(p => p.Bairro, bairro);
            endereco.SetupProperty(p => p.Loja, loja);
            _mockRepositorioEnderecos.Setup(p => p.ExisteNaLoja(It.IsAny<Endereco>())).Returns(true);
            _mockServicoEstados.Setup(p => p.GetPorSigla(It.IsAny<string>())).Returns<Estado>(p => p = estado);
            _mockServicoCidades.Setup(p => p.Salvar(It.IsAny<Cidade>()));
            _mockServicoBairros.Setup(p => p.Salvar(It.IsAny<Bairro>()));
            loja.AdicionarEndereco(endereco.Object);
            IServicoEnderecos servicoEnderecos = new ServicoEnderecos(_mockRepositorioEnderecos.Object, _mockServicoLogradouros.Object, _mockServicoBairros.Object, _mockServicoCidades.Object, _mockServicoEstados.Object);
            
            // Act
            servicoEnderecos.PreAtualizar(endereco.Object);

            // Assert
            Assert.IsNotNull(endereco.Object.Loja, "Loja não pode ser nula");
        }

        Mock<IRepositorioEnderecos> _mockRepositorioEnderecos = new Mock<IRepositorioEnderecos>();
        Mock<IServicoLogradouros> _mockServicoLogradouros = new Mock<IServicoLogradouros>();
        Mock<IServicoBairros> _mockServicoBairros = new Mock<IServicoBairros>();
        Mock<IServicoCidades> _mockServicoCidades = new Mock<IServicoCidades>();
        Mock<IServicoEstados> _mockServicoEstados = new Mock<IServicoEstados>();
    }
}
