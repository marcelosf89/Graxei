using Graxei.Transversais.ContratosDeDados.Interfaces;
using Graxei.Transversais.ContratosDeDados.TinyTypes;
using Graxei.Transversais.Idiomas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
