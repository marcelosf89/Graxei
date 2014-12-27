using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.SqlResolver.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.SqlResolver.Factory
{
    public interface IListaProdutosLojaSqlResolverFactory
    {
        IListaProdutosLojaSqlResolver Get(long idLoja, string criterio, bool meusProdutos);
    }
}
