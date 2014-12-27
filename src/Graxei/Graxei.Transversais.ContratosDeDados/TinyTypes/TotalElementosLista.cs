using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Transversais.ContratosDeDados.TinyTypes
{
    public class TotalElementosLista
    {
        public TotalElementosLista(long total)
        {
            _total = total;
        }

        private long _total;

        public long Total
        {
            get { return _total; }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is TotalElementosLista) || obj == null)
            {
                return false;
            }

            TotalElementosLista that = (TotalElementosLista)obj;
            return that.Total == this.Total;
        }

        public override int GetHashCode()
        {
            return this.Total.GetHashCode() ^ 3;
        }
    }
}