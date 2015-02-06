using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.SqlResolver.Interface;
using Graxei.Transversais.ContratosDeDados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.SqlResolver.Factory
{
    public class ListaProdutosLojaSqlResolverFactory : IListaProdutosLojaSqlResolverFactory
    {
        public IListaProdutosLojaSqlResolver Get(PesquisaProdutoContrato pesquisaProdutoContrato)
        {
            if (pesquisaProdutoContrato.MeusProdutos)
            {
                return new ListaProdutosLojaMeusProdutosResolver(pesquisaProdutoContrato);
            }

            return new ListaProdutosLojaCompletoResolver(pesquisaProdutoContrato);
        }
    }
}
