using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Transversais.Comum
{
    public class CuringasString
    {
        public static string GetLojaNaDescricao(string descricao)
        {
            string loja = string.Empty;
            if (descricao.ToLower().Contains("loja:"))
            {
                int idxOfLoja = descricao.ToLower().IndexOf("loja:");
                int nIdxOf = descricao.Substring(idxOfLoja).IndexOf(' ');

                if (nIdxOf <= 0)
                    loja = descricao.Substring(idxOfLoja + 5);
                else
                    loja = descricao.Substring(idxOfLoja + 5, nIdxOf);
            }
            return loja;
        }

    }
}
