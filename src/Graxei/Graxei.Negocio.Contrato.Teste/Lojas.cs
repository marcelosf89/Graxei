using Graxei.Modelo;
using Graxei.Negocio.Implementacao;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Utilidades.Excecoes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Graxei.Negocio.Contrato.Teste
{
    [TestClass]
    public class Lojas
    {
        [TestMethod]
        [ExpectedException(typeof(ValidacaoEntidadeException))]
        public void Modificar_NomeNaoInformado_DeveDispararExcecao()
        {
            // Arrange
            Loja loja = new Loja();
            Mock<IRepositorioLojas> repositorioLojas = new Mock<IRepositorioLojas>();
            Mock<IServicoLojaUsuario> servicoLojasUsuario = new Mock<IServicoLojaUsuario>();
            Mock<IServicoUsuarios> servicoUsuario = new Mock<IServicoUsuarios>();
            Mock<IServicoEnderecos> servicoEndereco = new Mock<IServicoEnderecos>();

            // Act
            IServicoLojas mockServicoLojas = new ServicoLojas(repositorioLojas.Object, servicoLojasUsuario.Object, servicoUsuario.Object, servicoEndereco.Object);
            mockServicoLojas.Setup(p => p.Salvar(loja)).Throws(new ValidacaoEntidadeException(Validacoes.NomeLojaObrigatório));
            IServicoLojas servicoLojas = mockServicoLojas.Object;
            servicoLojas.Salvar(loja);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjetoJaExisteException))]
        public void Modificar_LojaJaExiste_DeveDispararExcecao()
        {
            // Arrange
            Loja loja = new Loja();

            // Act
            Mock<IServicoLojas> mockServicoLojas = new Mock<IServicoLojas>();
            mockServicoLojas.Setup(p => p.PreAtualizar(loja)).Throws(new ObjetoJaExisteException(Erros.LojaJaExiste));
            IServicoLojas servicoLojas = mockServicoLojas.Object;
            servicoLojas.PreAtualizar(loja);
        }



    }
}
