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
    public sealed class ListaProdutosLoja : ILista<ListaProdutosLojaContrato>
    {
        private IList<ListaProdutosLojaContrato> _lista;

        private ListaTotalElementos _total;

        private ListaElementoAtual _atual;

        public ListaProdutosLoja(IList<ListaProdutosLojaContrato> lista, ListaTotalElementos total, ListaElementoAtual atual)
        {
            if (atual == null)
            {
                atual = new ListaElementoAtual(0);
            }
            if (total == null)
            {
                total = new ListaTotalElementos(0);
            }

            if (atual.Atual > total.Total)
            {
                throw new ArgumentOutOfRangeException(ErrosInternos.TotalMenorQueAtual);
            }
            
            if (lista == null)
            {
                lista = new List<ListaProdutosLojaContrato>();
            }

            _lista = lista;
            _total = total;
            _atual = atual;
        }

        public IList<ListaProdutosLojaContrato> Lista
        {
            get
            {
                List<ListaProdutosLojaContrato> retorno = new List<ListaProdutosLojaContrato>();
                retorno.AddRange(_lista);
                return retorno;
            }
        }

        public ListaTotalElementos Total
        {
            get
            {
                return _total;
            }
        }

        public ListaElementoAtual Atual
        {
            get
            {
                return _atual;
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
