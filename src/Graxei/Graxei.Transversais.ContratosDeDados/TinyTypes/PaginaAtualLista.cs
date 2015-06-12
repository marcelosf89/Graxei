using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Transversais.ContratosDeDados.TinyTypes
{
    public class PaginaAtualLista
    {
        public PaginaAtualLista(int valor)
        {
            _valor = valor;
        }

        private int _valor;

        public int Valor
        {
            get { return _valor; }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PaginaAtualLista) || obj == null)
            {
                return false;
            }

            PaginaAtualLista that = (PaginaAtualLista)obj;
            return that.Valor == this.Valor;
        }

        public override int GetHashCode()
        {
            return this.Valor.GetHashCode() ^ 3;
        }
    }
}
