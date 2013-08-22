using FAST.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graxei.Modelo
{
    public class Endereco : Entidade
    {
        public virtual string Logradouro { get; set; }

        public virtual string Numero { get; set; }

        public virtual string Complemento { get; set; }

        public virtual TipoLogradouro TipoLogradouro { get; set; }

        public virtual Loja Loja { get;  set; }

        public virtual Bairro Bairro { get; set; }

        public virtual IList<Telefone> Telefones { get; protected set; }

        #region Métodos Sobrescritos
        public override bool Equals(object obj)
        {
            if (!(obj is Endereco))
            {
                return false;
            }
            Endereco en = (Endereco)obj;
            return (en.Logradouro == this.Logradouro && en.Numero == this.Numero && en.Complemento == this.Complemento 
                &&  en.Bairro == this.Bairro && en.Loja == this.Loja);
        }

        public override int GetHashCode()
        {
            int result = 0;
            if (!(String.IsNullOrEmpty(Logradouro)))
            {
                result += Logradouro.GetHashCode();
            }
            if (!(String.IsNullOrEmpty(Numero)))
            {
                result += Numero.GetHashCode();
            }
            if (!(String.IsNullOrEmpty(Complemento)))
            {
                result += Complemento.GetHashCode();
            }
            if (Loja != null)
            {
                result += Loja.GetHashCode();
            }
            if (Bairro != null)
            {
                result += Bairro.GetHashCode();
            }
            return result;
        }
        #endregion

    }
}