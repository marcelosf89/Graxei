using Graxei.Modelo.Generico;
using System;

namespace Graxei.Modelo
{

    public class UnidadeMedida : Entidade
    {

        public virtual string Sigla { get; set; }
        public virtual string Descricao { get; set; }

        #region Métodos Sobrescritos
        public override bool Equals(object obj)
        {
            if (!(obj is UnidadeMedida))
            {
                return false;
            }
            UnidadeMedida um = (UnidadeMedida)obj;
            return (um.Sigla == this.Sigla);
        }

        public override int GetHashCode()
        {
            int retorno = 0;
            if (!(String.IsNullOrEmpty(Sigla)))
            {
                retorno += Sigla.GetHashCode();
            }
            return retorno + 11;
        }
        #endregion

    }

}
