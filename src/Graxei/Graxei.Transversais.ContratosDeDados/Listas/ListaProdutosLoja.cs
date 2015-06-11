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
    public sealed class ListaProdutosLoja : AbstractItemLista<ListaProdutosLojaContrato>
    {
        public ListaProdutosLoja(IList<ListaProdutosLojaContrato> lista, TotalElementosLista total, PaginaAtualLista atual)
            : base(lista, total, atual)
        {
        }

        public override IList<ListaProdutosLojaContrato> Lista
        {
            get
            {
                List<ListaProdutosLojaContrato> retorno = new List<ListaProdutosLojaContrato>();
                retorno.AddRange(_lista);
                return retorno;
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ListaProdutosLoja) || obj == null)
            {
                return false;
            }

            ListaProdutosLoja that = (ListaProdutosLoja)obj;
            bool retorno = that._lista.ToList().SequenceEqual(this._lista.ToList());
            retorno &= that._total.Equals(this._total);
            retorno &= that._atual.Equals(this._atual);
            return retorno;
        }
    }
}
