using FAST.Modelo;
using Graxei.Transversais.Idiomas;
using System;
using System.ComponentModel.DataAnnotations;

namespace Graxei.Modelo
{
    public class Fabricante : Entidade
    {
        public new virtual long Id { get; protected internal set; }
     
        [Display(ResourceType = typeof(Propriedades), Name = "Nome")]
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
