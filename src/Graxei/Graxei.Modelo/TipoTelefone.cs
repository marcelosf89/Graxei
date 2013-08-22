using FAST.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Modelo
{

    public class TipoTelefone : Entidade
    {

        public virtual String Abreviacao { get; set; }
        public virtual String Nome { get; set; }

    }

}
