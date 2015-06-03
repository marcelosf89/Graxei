using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Transversais.Comum
{
    public class PostgresComum
    {
        public static string DataValida(DateTime data)
        {
            return data.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
    }
}
