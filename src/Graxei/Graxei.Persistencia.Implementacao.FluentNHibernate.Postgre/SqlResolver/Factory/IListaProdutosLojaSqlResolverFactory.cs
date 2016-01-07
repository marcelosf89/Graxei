using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.SqlResolver.Interface;
using Graxei.Transversais.ContratosDeDados;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.SqlResolver.Factory
{
    public interface IListaProdutosLojaSqlResolverFactory
    {
        IListaProdutosLojaSqlResolver Get(PesquisaProdutoContrato pesquisaProdutoContrato);
    }
}
