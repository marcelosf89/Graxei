using System;
using System.Collections.Generic;
using Graxei.Transversais.ContratosDeDados.Interfaces;
using Graxei.Transversais.ContratosDeDados.TinyTypes;
using Graxei.Transversais.Idiomas;

namespace Graxei.Transversais.ContratosDeDados.Listas
{
    public class ListaLojas : AbstractItemLista<ListaLojasContrato>
    {
        public ListaLojas(IList<ListaLojasContrato> lista, TotalElementosLista total, PaginaAtualLista atual)
            : base(lista, total, atual)
        {
        }
    }
}