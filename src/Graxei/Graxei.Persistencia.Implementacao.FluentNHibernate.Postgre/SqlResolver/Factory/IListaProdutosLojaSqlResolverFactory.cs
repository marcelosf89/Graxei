using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.SqlResolver.Interface;
using Graxei.Transversais.ContratosDeDados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.SqlResolver.Factory
{
    public interface IListaProdutosLojaSqlResolverFactory
    {
        IListaProdutosLojaSqlResolver Get(PesquisaProdutoContrato pesquisaProdutoContrato);
    }
}
