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
            _servicoUsuario = new Mock<IServicoUsuarios>();
            _servicoEndereco = new Mock<IServicoEnderecos>();
        }

        ////[TestMethod]
        ////[ExpectedException(typeof(ArgumentNullException))]
        ////public void Criar_LojaNula_DeveDispararExcecao()
        ////{
        ////    // Act
        ////    IServicoLojas servicoLojas = new ServicoLojas(_repositorioLojas.Object);
        ////    servicoLojas.Salvar(null);   
        ////}

        ////[TestMethod]
        ////[ExpectedException(typeof(ArgumentNullException))]
        ////public void Criar_ListaUsuariosNula_DeveDispararExcecao()
        ////{
        ////    // Arrange
        ////    Loja loja = new Loja();

        ////    // Act
        ////    IServicoLojas servicoLojas = new ServicoLojas(_repositorioLojas.Object);
        ////    servicoLojas.Salvar(loja);

        ////}

        ////[TestMethod]
        ////[ExpectedException(typeof(ArgumentNullException))]
        ////public void Criar_ListaUsuariosVazia_DeveDispararExcecao()
        ////{
        ////    // Arrange
        ////    Loja loja = new Loja();
        ////    IList<Usuario> usuarios = new List<Usuario>();
            
        ////    // Act
        ////    IServicoLojas servicoLojas = new ServicoLojas(_repositorioLojas.Object);
        ////    servicoLojas.Salvar(loja);

        ////}
      
        ////[TestMethod]
        ////public void Criar_SemAssociacaoComUsuario_DeveDispararExcecao()
        ////{
        ////    // Arrange
        ////    Mock<Loja> loja = new Mock<Loja>();
        ////    loja.SetupProperty(p => p.Id, 1);
        ////    loja.SetupProperty(p => p.Nome, "Loja");

        ////    // Act
        ////    IServicoLojas servicoLojas = new ServicoLojas(_repositorioLojas.Object);
        ////    servicoLojas.Salvar(loja.Object);
        ////}

        ////[TestMethod]
        ////[ExpectedException(typeof(ObjetoJaExisteException))]
        ////public void Criar_LojaJaExiste_DeveDispararExcecao()
        ////{
        ////    // Arrange
        ////    string lojaExistente = "Loja Existente";
        ////    Mock<Loja> loja = new Mock<Loja>();
        ////    loja.SetupProperty(p => p.Nome, lojaExistente);
        ////    IList<Usuario> usuarios = new List<Usuario>();
        ////    usuarios.Add(new Usuario());
        ////    Usuario usuarioLog = new Usuario();
        ////    _repositorioLojas.Setup(p => p.Get(It.IsAny<string>())).Returns(loja.Object);

        ////    // Act
        ////    IServicoLojas servicoLojas = new ServicoLojas(_repositorioLojas.Object);
        ////    servicoLojas.Salvar(loja.Object);
        ////}

        ////[TestMethod]
        ////[ExpectedException(typeof(ValidacaoEntidadeException))]
        ////public void Modificar_NomeNaoInformado_DeveDispararExcecao()
        ////{
        ////    // Arrange
        ////    Mock<Loja> loja = new Mock<Loja>();
        ////    loja.SetupProperty(p => p.Id, 1);
        ////    Mock<IRepositorioLojas> repositorioLojas = new Mock<IRepositorioLojas>();
        ////    Mock<IServicoUsuarios> servicoUsuario = new Mock<IServicoUsuarios>();
        ////    Mock<IServicoEnderecos> servicoEndereco = new Mock<IServicoEnderecos>();            

        ////    // Act
        ////    IServicoLojas servicoLojas = new ServicoLojas(repositorioLojas.Object);
        ////    servicoLojas.Salvar(loja.Object);

        ////}

        ////[TestMethod]
        ////[ExpectedException(typeof(ObjetoJaExisteException))]
        ////public void Modificar_LojaJaExiste_DeveDispararExcecao()
        ////{
        ////    // Arrange
        ////    long id = 1;
        ////    long idRepetida = 2;
        ////    string lojaExistente = "Loja Existente";
        ////    Mock<Loja> loja = new Mock<Loja>();
        ////    loja.SetupProperty(p => p.Id, id);
        ////    loja.SetupProperty(p => p.Nome, lojaExistente);
        ////    Mock<Loja> lojaRepetida = new Mock<Loja>();
        ////    lojaRepetida.SetupProperty(p => p.Id, idRepetida);
        ////    lojaRepetida.SetupProperty(p => p.Nome, lojaExistente);
        ////    IList<Usuario> usuarios = new List<Usuario>();
        ////    usuarios.Add(new Usuario());
        ////    Usuario usuarioLog = new Usuario();
        ////    _repositorioLojas.Setup(p => p.Get(It.IsAny<string>())).Returns(lojaRepetida.Object);

        ////    // Act
        ////    IServicoLojas servicoLojas = new ServicoLojas(_repositorioLojas.Object);
        ////    servicoLojas.Salvar(loja.Object);
        ////}

        private Mock<IRepositorioLojas> _repositorioLojas = new Mock<IRepositorioLojas>();
        private Mock<IServicoUsuarios> _servicoUsuario = new Mock<IServicoUsuarios>();
        private Mock<IServicoEnderecos> _servicoEndereco = new Mock<IServicoEnderecos>();

    }
}
