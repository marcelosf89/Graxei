using Graxei.Transversais.ContratosDeDados.Listas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.SqlResolver.Interface
{
    public interface IListaProdutosLojaSqlResolver
    {
        IList<ListaProdutosLojaContrato> Get(int pagina, int tamanhoPagina);
        long GetConsultaDeContagem();
    }
}
