using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.SqlResolver.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.SqlResolver.Factory
{
    public class ListaProdutosLojaSqlResolverFactory : IListaProdutosLojaSqlResolverFactory
    {
        public IListaProdutosLojaSqlResolver Get(bool meusProdutos)
        {
            if (meusProdutos)
            {
                return new ListaProdutosLojaMeusProdutosResolver();
            }

            return new ListaProdutosLojaCompletoResolver();
        }
    }
}
