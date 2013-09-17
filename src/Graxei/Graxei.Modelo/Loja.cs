using FAST.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Graxei.Modelo
{
    public class Loja : Entidade
    {
        public virtual string Nome { get;  set; }
        public virtual byte[] Logotipo { get; set; }
        public virtual IList<Endereco> Enderecos { get; protected set; }

        #region Métodos Sobrescritos
        public override bool Equals(object obj)
        {
            if (!(obj is Loja))
            {
                return false;
            }
            Loja lj = (Loja)obj;
            return (lj.Nome == this.Nome);
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
