using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Modelo
{
    
    public class Telefone
    {
        public virtual string Numero { get; set; }
        public virtual Endereco Endereco { get; set; }
    }
}
