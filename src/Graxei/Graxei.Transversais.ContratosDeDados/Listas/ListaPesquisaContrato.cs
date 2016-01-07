using Graxei.Transversais.ContratosDeDados.Interfaces;
using Graxei.Transversais.ContratosDeDados.TinyTypes;
using System.Collections.Generic;

namespace Graxei.Transversais.ContratosDeDados.Listas
{
    public class ListaPesquisaContrato : AbstractItemLista<PesquisaContrato>
    {
        public ListaPesquisaContrato(IList<PesquisaContrato> lista, TotalElementosLista total, PaginaAtualLista atual)
            : base(lista, total, atual)
        {
        }
    }
}
