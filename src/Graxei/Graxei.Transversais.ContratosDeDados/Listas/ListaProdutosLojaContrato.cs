using Graxei.Transversais.ContratosDeDados.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Transversais.ContratosDeDados.Listas
{
    public class ListaProdutosLojaContrato : IItemLista
    {
        public long Id { get; set; }

        public string Descricao { get; set; }

        public decimal Preco {  get; set; }
    }
}
