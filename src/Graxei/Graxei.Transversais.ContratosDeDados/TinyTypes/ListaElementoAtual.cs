using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Transversais.ContratosDeDados.TinyTypes
{
    public class ListaElementoAtual
    {
        public ListaElementoAtual(int atual)
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
            if (!(obj is ListaElementoAtual) || obj == null)
            {
                return false;
            }

            ListaElementoAtual that = (ListaElementoAtual)obj;
            return that.Atual == this.Atual;
        }

        public override int GetHashCode()
        {
            return this.Atual.GetHashCode() ^ 3;
        }
    }
}
