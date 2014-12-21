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

        public override bool Equals(object obj)
        {
            if (!(obj is ListaTotalElementos) || obj == null)
            {
                return false;
            }

            ListaTotalElementos that = (ListaTotalElementos)obj;
            return that.Total == this.Total;
        }

        public override int GetHashCode()
        {
            return this.Total.GetHashCode() ^ 3;
        }
    }
}