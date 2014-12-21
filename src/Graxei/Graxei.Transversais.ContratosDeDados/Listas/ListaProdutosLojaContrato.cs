using Graxei.Transversais.ContratosDeDados.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Transversais.ContratosDeDados.Listas
{
    public sealed class ListaProdutosLojaContrato : IItemLista
    {
        public long Id { get; set; }

        public string Descricao { get; set; }

        public double Preco {  get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is ListaProdutosLojaContrato) || obj == null)
            {
                return false;
            }

            ListaProdutosLojaContrato that = (ListaProdutosLojaContrato)obj;
            return (this.Id == that.Id && this.Descricao == that.Descricao);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode() ^ 11 + this.Descricao.GetHashCode() ^ 9;
        }
    }
}
