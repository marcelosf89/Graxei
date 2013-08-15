using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Modelo
{

    public class Fabricante
    {
        public virtual string Nome { get; set; }

        #region Métodos Sobrescritos
        public override bool Equals(object obj)
        {
            if (!(obj is Fabricante))
            {
                return false;
            }
            Fabricante fb = (Fabricante)obj;
            return (fb.Nome == this.Nome);
        }

        public override int GetHashCode()
        {
            int retorno = 0;
            if (!(String.IsNullOrEmpty(Nome)))
            {
                retorno += Nome.GetHashCode();
            }
            return retorno + 11;
        }
        #endregion
    }

}
