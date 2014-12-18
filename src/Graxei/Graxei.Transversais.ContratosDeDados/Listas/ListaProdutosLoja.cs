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
    public class ListaProdutosLoja : ILista<ListaProdutosLojaContrato>
    {
        private IList<ListaProdutosLojaContrato> _lista;

        private ListaTotalElementos _total;

        private ListaElementoAtual _atual;

        public ListaProdutosLoja(IList<ListaProdutosLojaContrato> lista, ListaTotalElementos total, ListaElementoAtual atual)
        {
            if (atual.Atual > total.Total)
            {
                throw new ArgumentOutOfRangeException(ErrosInternos.TotalMenorQueAtual);
            }
            _lista = lista;
            _total = total;
            _atual = atual;
        }

        public IList<ListaProdutosLojaContrato> Lista
        {
            get
            {
                return _lista;
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
    }
}
