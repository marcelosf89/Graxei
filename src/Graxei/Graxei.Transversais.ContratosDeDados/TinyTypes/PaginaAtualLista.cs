using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Transversais.ContratosDeDados.TinyTypes
{
    public class PaginaAtualLista
    {
        public PaginaAtualLista(int atual)
        {
            _atual = atual;
        }

        private int _atual;

        public int Atual
        {
            get { return _atual; }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PaginaAtualLista) || obj == null)
            {
                return false;
            }

            PaginaAtualLista that = (PaginaAtualLista)obj;
            return that.Atual == this.Atual;
        }

        public override int GetHashCode()
        {
            return this.Atual.GetHashCode() ^ 3;
        }
    }
}
