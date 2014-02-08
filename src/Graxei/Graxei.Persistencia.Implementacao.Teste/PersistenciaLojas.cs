using Graxei.Persistencia.Contrato;
using Graxei.Persistencia.Implementacao.NHibernate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Graxei.Persistencia.Implementacao.Teste
{
    [TestClass]
    public class PersistenciaLojas
    {
        [TestMethod]
        public void Nova_DeveIncluirLoja()
        {
            // Arrange
            IRepositorioProdutoVendedor repositorioProdutoVendedor = new ProdutoVendedorNHibernateMySQL();
            IRepositorioLojas repositorioLojas = new LojasNHibernateMySQL(repositorioProdutoVendedor);
        }
    }
}
