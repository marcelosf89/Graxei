using System.ComponentModel.DataAnnotations;
using FAST.Modelo;
using System;
using Graxei.Transversais.Idiomas;

namespace Graxei.Modelo
{

    public class Logradouro : Entidade
    {
        
        [StringLength(100)]
        public virtual string Nome { get; set; }
        public virtual string CEP { get; set; }
        public virtual Bairro Bairro { get; set; }

        #region Métodos Sobrescritos
        public override bool Equals(object obj)
        {
            if (!(obj is Logradouro))
            {
                return false;
            }
            Logradouro ld = (Logradouro)obj;
            return (ld.Nome == this.Nome && ld.Bairro == this.Bairro);
        }

        public override int GetHashCode()
        {
            if (!(String.IsNullOrEmpty(Nome)) && (Bairro != null))
            {
                return (Nome.GetHashCode() * Bairro.GetHashCode()) + 19;
            }
            return 0;
        }

        public override string ToString()
        {
            return Nome;
        }
        #endregion

    }

}
