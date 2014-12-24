using System;
using System.Collections.Generic;
using Graxei.Transversais.ContratosDeDados.Interfaces;
using Graxei.Transversais.ContratosDeDados.TinyTypes;
using Graxei.Transversais.Idiomas;

namespace Graxei.Transversais.ContratosDeDados.Listas
{
    public class ListaLojas : ILista<ListaLojasContrato>
    {
        private IList<ListaLojasContrato> _lista;

        private TotalElementosLista _total;

        private PaginaAtualLista _atual;

        public ListaLojas(IList<ListaLojasContrato> lista, TotalElementosLista total, PaginaAtualLista atual)
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

        public TotalElementosLista Total
        {
            get { return _total; }
        }

        public PaginaAtualLista Atual { get { return _atual; } }
    }
}