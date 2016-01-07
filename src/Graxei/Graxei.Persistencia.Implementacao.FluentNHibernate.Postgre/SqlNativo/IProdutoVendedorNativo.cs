using Graxei.Transversais.ContratosDeDados;
using System.Collections.Generic;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.SqlNativo
{
    public interface IProdutoVendedorNativo
    {
        IList<ProdutoLojaPrecoContrato> Get(string sql);
    }
}
