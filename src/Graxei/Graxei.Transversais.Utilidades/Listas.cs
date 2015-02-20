using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Transversais.Utilidades
{
    public class Listas
    {
        public static bool NulaOuVazia<T>(IList<T> lista) where T : class
        {
            return (lista == null || lista.Count == 0);
        }
    }
}
