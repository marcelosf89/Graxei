using Graxei.Modelo;
using System;
using System.Collections.Generic;

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
