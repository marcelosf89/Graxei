using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public class ConsultasSQL
    {
        public const string ExcluirProdutosVendedorDeLoja =
            "UPDATE produtos_vendedor SET excluida = 1 WHERE id_loja = :p0";
    }
}
