using Graxei.Modelo;
using Graxei.Negocio.Implementacao;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Utilidades.Excecoes;
using Graxei.Transversais.Utilidades.NHibernate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Graxei.Negocio.Contrato.Teste
{
    [TestClass]
    public class Lojas
    {
        [TestInitialize]
        public void Iniciar()
        {
            _repositorioLojas = new Mock<IRepositorioLojas>();
            _servicoLojasUsuario = new Mock<IServicoLojaUsuario>();
            _servicoUsuario = new Mock<IServicoUsuarios>();
            _servicoEndereco = new Mock<IServicoEnderecos>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Criar_LojaNula_DeveDispararExcecao()
        {
            // Act
            IServicoLojas servicoLojas = new ServicoLojas(_repositorioLojas.Object, _servicoLojasUsuario.Object, _servicoUsuario.Object, _servicoEndereco.Object);
            servicoLojas.Salvar(null, null, null);   
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Criar_ListaUsuariosNula_DeveDispararExcecao()
        {
            // Arrange
            Loja loja = new Loja();

            // Act
            IServicoLojas servicoLojas = new ServicoLojas(_repositorioLojas.Object, _servicoLojasUsuario.Object, _servicoUsuario.Object, _servicoEndereco.Object);
            servicoLojas.Salvar(loja, null, null);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Criar_ListaUsuariosVazia_DeveDispararExcecao()
        {
            // Arrange
            Loja loja = new Loja();
            IList<Usuario> usuarios = new List<Usuario>();
            
            // Act
            IServicoLojas servicoLojas = new ServicoLojas(_repositorioLojas.Object, _servicoLojasUsuario.Object, _servicoUsuario.Object, _servicoEndereco.Object);
            servicoLojas.Salvar(loja, usuarios, null);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Criar_UsuariosLogNulo_DeveDispararExcecao()
        {
            // Arrange
            Loja loja = new Loja();
            IList<Usuario> usuarios = new List<Usuario>();
            usuarios.Add(new Usuario());
            
            // Act
            IServicoLojas servicoLojas = new ServicoLojas(_repositorioLojas.Object, _servicoLojasUsuario.Object, _servicoUsuario.Object, _servicoEndereco.Object);
            servicoLojas.Salvar(loja, usuarios, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidacaoEntidadeException))]
        public void Criar_LojaSemNome_DeveDispararExcecao()
        {
            // Arrange
            Loja loja = new Loja();
            IList<Usuario> usuarios = new List<Usuario>();
            usuarios.Add(new Usuario());
            Usuario usuarioLog = new Usuario();
            // Act
            IServicoLojas servicoLojas = new ServicoLojas(_repositorioLojas.Object, _servicoLojasUsuario.Object, _servicoUsuario.Object, _servicoEndereco.Object);
            servicoLojas.Salvar(loja, usuarios, usuarioLog);
            
        }

        [TestMethod]
        public void Criar_SemAssociacaoComUsuario_DeveDispararExcecao()
        {
            // Arrange
            Mock<Loja> loja = new Mock<Loja>();
            loja.SetupProperty(p => p.Id, 1);
            loja.SetupProperty(p => p.Nome, "Loja");

            // Act
            IServicoLojas servicoLojas = new ServicoLojas(_repositorioLojas.Object, _servicoLojasUsuario.Object, _servicoUsuario.Object, _servicoEndereco.Object);
            servicoLojas.Salvar(loja.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(RepetidoEmColecaoException))]
        public void HaEnderecosRepetidos_DeveDispararExcecao()
        {
            // Arrange
            string lojaExistente = "Loja";
            IList<Usuario> usuarios = new List<Usuario>();
            usuarios.Add(new Usuario());
            Usuario usuarioLog = new Usuario();
            _repositorioLojas.Setup(p => p.Get(It.IsAny<string>())).Returns(It.Is<Loja>(null));
            _servicoUsuario.Setup(p => p.GetPorLogin(It.IsAny<string>())).Returns(usuarioLog);
            Mock<Endereco> endereco = new Mock<Endereco>();
            endereco.Setup(p => p.ToString()).Returns("Endereço");
            IList<Endereco> enderecos = new List<Endereco>();
            enderecos.Add(endereco.Object);
            Mock<Loja> loja = new Mock<Loja>();
            loja.SetupProperty(p => p.Nome, lojaExistente);
            loja.SetupProperty(p => p.Enderecos, enderecos);

            _servicoEndereco.Setup(p => p.EnderecosRepetidos(It.IsAny<IList<Endereco>>())).Returns(enderecos);

            // Act
            IServicoLojas servicoLojas = new ServicoLojas(_repositorioLojas.Object, _servicoLojasUsuario.Object, _servicoUsuario.Object, _servicoEndereco.Object);
            servicoLojas.Salvar(loja.Object, usuarios, usuarioLog);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjetoJaExisteException))]
        public void Criar_LojaJaExiste_DeveDispararExcecao()
        {
            // Arrange
            string lojaExistente = "Loja Existente";
            Mock<Loja> loja = new Mock<Loja>();
            loja.SetupProperty(p => p.Nome, lojaExistente);
            IList<Usuario> usuarios = new List<Usuario>();
            usuarios.Add(new Usuario());
            Usuario usuarioLog = new Usuario();
            _repositorioLojas.Setup(p => p.Get(It.IsAny<string>())).Returns(loja.Object);

            // Act
            IServicoLojas servicoLojas = new ServicoLojas(_repositorioLojas.Object, _servicoLojasUsuario.Object, _servicoUsuario.Object, _servicoEndereco.Object);
            servicoLojas.Salvar(loja.Object, usuarios, usuarioLog);
        }

        [TestMethod]
        public void Criar_LojaCriada()
        {
            // Arrange
            string lojaExistente = "Loja";
            IList<Usuario> usuarios = new List<Usuario>();
            usuarios.Add(new Usuario());
            Usuario usuarioLog = new Usuario();
            _repositorioLojas.Setup(p => p.Get(It.IsAny<string>())).Returns(It.Is<Loja>(null));
            _servicoUsuario.Setup(p => p.GetPorLogin(It.IsAny<string>())).Returns(usuarioLog);
            Mock<Endereco> endereco = new Mock<Endereco>();
            endereco.Setup(p => p.ToString()).Returns("Endereço");
            IList<Endereco> enderecos = new List<Endereco>();
            enderecos.Add(endereco.Object);
            Mock<Loja> loja = new Mock<Loja>();
            loja.SetupProperty(p => p.Nome, lojaExistente);
            loja.SetupProperty(p => p.Enderecos, enderecos);
            _servicoEndereco.Setup(p => p.EnderecosRepetidos(It.IsAny<IList<Endereco>>())).Returns(It.Is<IList<Endereco>>(null));

            // Act
            IServicoLojas servicoLojas = new ServicoLojas(_repositorioLojas.Object, _servicoLojasUsuario.Object, _servicoUsuario.Object, _servicoEndereco.Object);
            servicoLojas.Salvar(loja.Object, usuarios, usuarioLog);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidacaoEntidadeException))]
        public void Modificar_NomeNaoInformado_DeveDispararExcecao()
        {
            // Arrange
            Mock<Loja> loja = new Mock<Loja>();
            loja.SetupProperty(p => p.Id, 1);
            Mock<IRepositorioLojas> repositorioLojas = new Mock<IRepositorioLojas>();
            Mock<IServicoLojaUsuario> servicoLojasUsuario = new Mock<IServicoLojaUsuario>();
            Mock<IServicoUsuarios> servicoUsuario = new Mock<IServicoUsuarios>();
            Mock<IServicoEnderecos> servicoEndereco = new Mock<IServicoEnderecos>();            

            // Act
            IServicoLojas servicoLojas = new ServicoLojas(repositorioLojas.Object, servicoLojasUsuario.Object, servicoUsuario.Object, servicoEndereco.Object);
            servicoLojas.Salvar(loja.Object);

        }

        [TestMethod]
        [ExpectedException(typeof(ObjetoJaExisteException))]
        public void Modificar_LojaJaExiste_DeveDispararExcecao()
        {
            // Arrange
            long id = 1;
            long idRepetida = 2;
            string lojaExistente = "Loja Existente";
            Mock<Loja> loja = new Mock<Loja>();
            loja.SetupProperty(p => p.Id, id);
            loja.SetupProperty(p => p.Nome, lojaExistente);
            Mock<Loja> lojaRepetida = new Mock<Loja>();
            lojaRepetida.SetupProperty(p => p.Id, idRepetida);
            lojaRepetida.SetupProperty(p => p.Nome, lojaExistente);
            IList<Usuario> usuarios = new List<Usuario>();
            usuarios.Add(new Usuario());
            Usuario usuarioLog = new Usuario();
            _repositorioLojas.Setup(p => p.Get(It.IsAny<string>())).Returns(lojaRepetida.Object);

            // Act
            IServicoLojas servicoLojas = new ServicoLojas(_repositorioLojas.Object, _servicoLojasUsuario.Object, _servicoUsuario.Object, _servicoEndereco.Object);
            servicoLojas.Salvar(loja.Object, usuarios, usuarioLog);
        }



        private Mock<IRepositorioLojas> _repositorioLojas = new Mock<IRepositorioLojas>();
        private Mock<IServicoLojaUsuario> _servicoLojasUsuario = new Mock<IServicoLojaUsuario>();
        private Mock<IServicoUsuarios> _servicoUsuario = new Mock<IServicoUsuarios>();
        private Mock<IServicoEnderecos> _servicoEndereco = new Mock<IServicoEnderecos>();

    }
}
