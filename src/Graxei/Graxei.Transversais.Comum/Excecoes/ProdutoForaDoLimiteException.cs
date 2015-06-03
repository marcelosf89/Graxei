using Graxei.Modelo;
using Graxei.Transversais.ContratosDeDados;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Transversais.Comum.Excecoes
{
    public class ProdutoForaDoLimiteException: Exception
    {
        public IList<Produto> List { get; set; }
        public long Max { get; set; }

        public ProdutoForaDoLimiteException(IList<Produto> list, long max)
        {
            this.List = list; this.Max = max;
        }
    }
}
