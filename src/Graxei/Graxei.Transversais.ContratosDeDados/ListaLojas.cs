using System;
using System.Collections.Generic;
using Graxei.Transversais.ContratosDeDados.Interfaces;
using Graxei.Transversais.ContratosDeDados.TinyTypes;
using Graxei.Transversais.Idiomas;

namespace Graxei.Transversais.ContratosDeDados
{
    public class ListaLojas : ILista<ListaLojasContrato>
    {
        private IList<ListaLojasContrato> _lista;

        private ListaTotalElementos _total;

        private ListaElementoAtual _atual;

        public ListaLojas(IList<ListaLojasContrato> lista, ListaTotalElementos total, ListaElementoAtual atual)
        {
            if (atual.Atual > total.Total)
            {
                throw new ArgumentOutOfRangeException(ErrosInternos.TotalMenorQueAtual);
            }
            _lista = lista;
            _total = total;
            _atual = atual;
        }

        public IList<ListaLojasContrato> Lista
        {
            get { return _lista; }
        }

        public ListaTotalElementos Total
        {
            get { return _total; }
        }

        public ListaElementoAtual Atual { get { return _atual; } }
    }
}