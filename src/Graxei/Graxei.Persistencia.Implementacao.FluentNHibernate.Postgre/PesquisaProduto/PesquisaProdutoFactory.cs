using Graxei.Persistencia.Contrato.PesquisaProduto;
using Graxei.Transversais.Comum;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.PesquisaProduto
{
    public class PesquisaProdutoFactory : IPesquisaProdutoFactory
    {
        public IPesquisaProdutoRepositorio Get(string criterio)
        {
            string loja = CuringasString.GetLojaNaDescricao(criterio);
            if (string.IsNullOrEmpty(loja))
            {
                return new PesquisaProdutoSimples(criterio);
            }

            return new PesquisaProdutoLoja(criterio, loja);
        }
    }
}
