using FAST.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Modelo
{

    public class Categoria : Entidade
    {
        public virtual string Nome { get; set; }
        public virtual Categoria CategoriaPai { get; set; }

        #region Métodos Sobrescritos
        public override bool Equals(object obj)
        {
            if (!(obj is Categoria))
            {
                return false;
            }
            Categoria cg = (Categoria)obj;
            return (cg.Nome == this.Nome && cg.CategoriaPai == this.CategoriaPai);
        }

        public override int GetHashCode()
        {
            int retorno = 0;
            if (!(String.IsNullOrEmpty(Nome)))
            {
                retorno += Nome.GetHashCode();
            }
            if (CategoriaPai != null)
            {
                retorno += CategoriaPai.GetHashCode();
            }
            return retorno + 11;
        }
        #endregion
    }

}
