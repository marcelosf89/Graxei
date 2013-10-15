using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FAST.Modelo;

namespace Graxei.Modelo
{
    public class Atributo : ExclusaoLogica
    {
        public virtual string Nome { get; set; }
        public virtual string Rotulo { get; set; }
        public virtual int Tamanho { get; set; }
        public virtual ProdutoVendedor ProdutoVendedor { get; protected internal set; }
    }
}
