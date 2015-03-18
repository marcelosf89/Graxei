using System.ComponentModel.DataAnnotations;
using Graxei.Modelo.Generico;
using System;
using Graxei.Transversais.Idiomas;

namespace Graxei.Modelo
{
    public class Estado : Entidade
    {

        [Required(ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "SiglaObrigatoria")]
        [StringLength(2)]
        public virtual string Sigla { get; set; }
        [Required(ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "NomeObrigatorio")]
        [StringLength(50)]
        public virtual string Nome { get; set; }

        #region Métodos Sobrescritos
        public override bool Equals(object obj)
        {
            if (!(obj is Estado))
            {
                return false;
            }
            Estado es = (Estado)obj;
            return (es.Sigla == this.Sigla);
        }

        public override int GetHashCode()
        {
            if (!(String.IsNullOrEmpty(Sigla)))
            {
                return Sigla.GetHashCode() + 11;
            }
            return 0;
        }

        public override string ToString()
        {
            return Sigla;
        }
        #endregion

        public virtual bool Validar()
        {
            return !(String.IsNullOrEmpty(Sigla) && String.IsNullOrEmpty(Nome));
        }
    }
}
