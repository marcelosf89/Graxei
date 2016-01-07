using System.Collections.Generic;
using System.Linq;

namespace Graxei.Transversais.Comum
{
    public class Listas
    {
        public static bool NulaOuVazia<T>(IList<T> lista) where T : class
        {
            return (lista == null || lista.Count == 0);
        }

        public static bool ListasIguais<T>(IList<T> t, IList<T> comparar) where T : class
        {
            bool retorno = t.Count() == comparar.Count();
            retorno &= !t.Except(comparar).Any();
            retorno &= !comparar.Except(t).Any();
            return retorno;
        }
    }
}
