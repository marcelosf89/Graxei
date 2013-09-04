using FAST.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Modelo
{
    public class Estado : Entidade
    {

        public virtual string Sigla { get; set; }
        public virtual string Nome { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Estado))
            {
                return false;
            }
            Estado es = (Estado)obj;
            return (es.Sigla == this.Sigla);
        }

        public override int GetHashCode()
        {
            if (!(String.IsNullOrEmpty(Sigla)))
            {
                return Sigla.GetHashCode() + 11;
            }
            return 0;
        }
    }
}
