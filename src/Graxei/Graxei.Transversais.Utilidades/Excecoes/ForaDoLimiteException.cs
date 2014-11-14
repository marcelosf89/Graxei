using Graxei.Modelo;
using Graxei.Transversais.ContratosDeDados;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Transversais.Utilidades.Excecoes
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
