using Graxei.Aplicacao.Contrato.Transacionais;
using Graxei.Apresentacao.Areas.Administrativo.Controllers;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.Idiomas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Graxei.Apresentacao.Teste.AreaAdministrativo
{
    [TestClass]
    public class ProdutoControllerTest
    {
        [TestMethod]
        public void QuandoSalvarFalhar_RetorneJsonComSucessoFalse()
        {
            // Arrange
            Mock<IGerenciamentoProdutos> mockGerenciamentProdutos = new Mock<IGerenciamentoProdutos>();
            mockGerenciamentProdutos.Setup(p => p.SalvarLista(It.IsAny<List<ProdutoLojaPrecoContrato>>())).Throws(new Exception("Falhou"));
            string jsonEsperado = string.Format("{{ Sucesso = False, Mensagem = {0} }}", Erros.ListaNaoAtualizada);
            
            // Act
            JsonResult result =  new ProdutosController(null, null, null, null, mockGerenciamentProdutos.Object).Salvar(It.IsAny<IList<ProdutoLojaPrecoContrato>>());

            // Assert
            Assert.AreEqual(jsonEsperado, result.Data.ToString());
        }

        public void QuandoSalvarFuncionar_RetorneJsonComSucessoTrue()
        {
            // Arrange
            Mock<IGerenciamentoProdutos> mockGerenciamentProdutos = new Mock<IGerenciamentoProdutos>();
            mockGerenciamentProdutos.Setup(p => p.SalvarLista(It.IsAny<List<ProdutoLojaPrecoContrato>>()));
            string jsonEsperado = string.Format("{{ Sucesso = True, Mensagem = {0} }}", Sucesso.ListaAtualizada);

            // Act
            JsonResult result = new ProdutosController(null, null, null, null, mockGerenciamentProdutos.Object).Salvar(It.IsAny<IList<ProdutoLojaPrecoContrato>>());

            // Assert
            Assert.AreEqual(jsonEsperado, result.Data.ToString());
        }
    }
}
