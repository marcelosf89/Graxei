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
  
    }
}
