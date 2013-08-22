using FAST.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Modelo
{

    public class Cidade : Entidade
    {
        public virtual string Nome { get; set; }
        public virtual Estado Estado { get; set; }

        #region Métodos Sobrescritos
        public override bool Equals(object obj)
        {
            if (!(obj is Cidade))
            {
                return false;
            }
            Cidade cd = (Cidade)obj;
            return (cd.Nome == this.Nome && cd.Estado == this.Estado);
        }

        public override int GetHashCode()
        {
            if (!(String.IsNullOrEmpty(Nome)) && (Estado != null))
            {
                return (Nome.GetHashCode() * Estado.GetHashCode()) + 19;
            }
            return 0;
        }
        #endregion

    }

}
