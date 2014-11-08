using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Transversais.ContratosDeDados.TinyTypes
{
    public class ListaTotalElementos
    {
        public ListaTotalElementos(int total)
        {
            _total = total;
        }

        private int _total;

        public int Total
        {
            get { return _total; }
        }
    }
}