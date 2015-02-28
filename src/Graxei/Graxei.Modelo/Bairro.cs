using System.ComponentModel.DataAnnotations;
using Graxei.Modelo.Generico;
using System;
using Graxei.Transversais.Idiomas;

namespace Graxei.Modelo
{

    public class Bairro : Entidade
    {
        [Required(ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "NomeObrigatorio")]
        [StringLength(100)]
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

        public override string ToString()
        {
            return Nome;
        }
        #endregion

        #region Overrides of Entidade

        public virtual bool Validar()
        {
            return (!String.IsNullOrEmpty(this.Nome) && this.Cidade != null && this.Cidade.Validar());
        }

        #endregion
    }

}
