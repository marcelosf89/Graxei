using FAST.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Modelo
{

    public class Bairro : Entidade
    {
        public virtual string Nome { get; set; }
        public virtual Cidade Cidade { get; set; }

        #region Métodos Sobrescritos
        public override bool Equals(object obj)
        {
            if (!(obj is Bairro))
            {
                return false;
            }
            Bairro br = (Bairro)obj;
            return (br.Nome == this.Nome && br.Cidade == this.Cidade);
        }

        public override int GetHashCode()
        {
            if (!(String.IsNullOrEmpty(Nome)) && (Cidade != null))
            {
                return (Nome.GetHashCode() * Cidade.GetHashCode()) + 19;
            }
            return 0;
        }
        #endregion

    }

}
