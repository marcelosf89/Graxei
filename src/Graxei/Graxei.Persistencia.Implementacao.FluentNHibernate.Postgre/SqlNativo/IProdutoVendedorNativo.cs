using Graxei.Transversais.ContratosDeDados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.SqlNativo
{
    public interface IProdutoVendedorNativo
    {
        IList<ProdutoLojaPrecoContrato> Get(string sql);
    }
}
