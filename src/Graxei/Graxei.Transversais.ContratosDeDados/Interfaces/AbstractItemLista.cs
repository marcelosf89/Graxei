using Graxei.Transversais.ContratosDeDados.TinyTypes;
using Graxei.Transversais.Idiomas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Transversais.ContratosDeDados.Interfaces
{
    public class AbstractItemLista<T> : ILista<T> where T : IItemLista
    {
        public AbstractItemLista(IList<T> lista, TotalElementosLista total, PaginaAtualLista atual)
        {
            if (atual == null)
            {
                atual = new PaginaAtualLista(0);
            }
            if (total == null)
            {
                total = new TotalElementosLista(0);
            }

            if (atual.Atual > total.Total)
            {
                throw new ArgumentOutOfRangeException(ErrosInternos.TotalMenorQueAtual);
            }
            
            if (lista == null)
            {
                lista = new List<T>();
            }

            _lista = lista;
            _total = total;
            _atual = atual;
        }

        public virtual IList<T> Lista
        {
            get { return _lista; }
        }

        public TotalElementosLista Total
        {
            get { return _total; }
        }

        public PaginaAtualLista Atual { get { return _atual; } }

        protected IList<T> _lista;

        protected TotalElementosLista _total;

        protected PaginaAtualLista _atual;
    }
}
