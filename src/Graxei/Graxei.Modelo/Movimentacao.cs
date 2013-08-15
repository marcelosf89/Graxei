using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Modelo
{
    public class Movimentacao
    {
        public virtual Produto Produto { get; set; }
        public virtual Endereco Endereco { get; set; }
        public virtual double Quantidade { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual SentidoMovimentacao Sentido { get; set; }
    }

    public enum SentidoMovimentacao
    {
        ENTRADA, SAIDA
    }
}
