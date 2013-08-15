using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Modelo
{

    public class TipoTelefone
    {

        public virtual String Abreviacao { get; set; }
        public virtual String Nome { get; set; }
        public virtual Endereco Endereco { get; set; }

    }

}
