using System.Collections.Generic;
using Graxei.Transversais.ContratosDeDados.Interfaces;

namespace Graxei.Transversais.ContratosDeDados
{
    public class ListaLojas : ILista<ListaLojasContrato>
    {
        private IList<ListaLojasContrato> _lista;

        private int _total;

        public ListaLojas(IList<ListaLojasContrato> lista, int total)
        {
            _lista = lista;
            _total = total;
        }

        public IList<ListaLojasContrato> Lista
        {
            get { return _lista; }
        }

        public int Total { get { return _total; } 
        }
    }
}