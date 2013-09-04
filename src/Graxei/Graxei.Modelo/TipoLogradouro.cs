using FAST.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Modelo
{
    
    public class TipoLogradouro : Entidade
    {

        public virtual string Sigla { get; set; }
        public virtual string Nome { get; set; }


        #region Métodos Sobrescritos
        public override bool Equals(object obj)
        {
            if (!(obj is TipoLogradouro))
            {
                return false;
            }
            TipoLogradouro tl = (TipoLogradouro)obj;
            return (tl.Nome == this.Nome);
        }

        public override int GetHashCode()
        {
            if (!(String.IsNullOrEmpty(Nome)))
            {
                return Nome.GetHashCode() + 11;
            }
            return 0;
        }
        #endregion

    }

}
