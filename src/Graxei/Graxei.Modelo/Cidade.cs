﻿using System.ComponentModel.DataAnnotations;
using Graxei.Modelo.Generico;
using System;
using Graxei.Transversais.Idiomas;

namespace Graxei.Modelo
{

    public class Cidade : Entidade
    {
        [Required(ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName = "NomeObrigatorio")]
        [StringLength(100)]
        public virtual string Nome { get; set; }
        public virtual Estado Estado { get; set; }

        #region Métodos Sobrescritos
        public override bool Equals(object obj)
        {
            if (!(obj is Cidade))
            {
                return false;
            }
            Cidade cd = (Cidade)obj;
            return (cd.Nome == this.Nome && cd.Estado.Equals(this.Estado));
        }

        public override int GetHashCode()
        {
            if (!(String.IsNullOrEmpty(Nome)) && (Estado != null))
            {
                return (Nome.GetHashCode() * Estado.GetHashCode()) + 19;
            }
            return 0;
        }

        public override string ToString()
        {
            return Nome;
        }
        #endregion

        #region Overrides of Entidade

/*        public override void Validar()
        {
            if ()
        }
        */
        #endregion

        public virtual bool Validar()
        {
            return (!String.IsNullOrEmpty(Nome) && Estado != null && Estado.Validar());
        }
    }

}
