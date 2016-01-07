using Graxei.Transversais.ContratosDeDados.Listas;
using System.Collections.Generic;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.SqlResolver.Interface
{
    public interface IListaProdutosLojaSqlResolver
    {
        IList<ListaProdutosLojaContrato> Get(int pagina, int tamanhoPagina);
        long GetConsultaDeContagem();
    }
}
