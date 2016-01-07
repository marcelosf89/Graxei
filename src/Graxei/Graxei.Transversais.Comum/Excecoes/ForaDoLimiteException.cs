using Graxei.Transversais.ContratosDeDados;
using System;
using System.Collections.Generic;

namespace Graxei.Transversais.Comum.Excecoes
{
    public class ForaDoLimiteException: Exception
    {
        public IList<PesquisaContrato> List { get; set; }
        public long Max { get; set; }

        public ForaDoLimiteException(IList<PesquisaContrato> list, long max)
        {
            this.List = list; this.Max = max;
        }
    }
}
