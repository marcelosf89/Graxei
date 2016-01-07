using Graxei.Modelo.Generico;
using System;

namespace Graxei.Modelo
{

    public class TipoTelefone : Entidade
    {

        public virtual String Abreviacao { get; set; }
        public virtual String Nome { get; set; }

        #region Métodos Sobrescritos
        public override bool Equals(object obj)
        {
            if (!(obj is TipoTelefone))
            {
                return false;
            }
            TipoTelefone tt = (TipoTelefone)obj;
            return (tt.Abreviacao == this.Abreviacao);
        }

        public override int GetHashCode()
        {
            int retorno = 0;
            if (!(String.IsNullOrEmpty(Abreviacao)))
            {
                retorno += Abreviacao.GetHashCode();
            }
            return retorno + 11;
        }
        #endregion
    }

}
