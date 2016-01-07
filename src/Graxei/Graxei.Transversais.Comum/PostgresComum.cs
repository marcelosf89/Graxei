using System;

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
