using FAST.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Graxei.Transversais.Idiomas;

namespace Graxei.Modelo
{
    public class Loja : ExclusaoLogica
    {
        [Required(ErrorMessageResourceType = typeof(Textos), ErrorMessageResourceName = "NomeObrigatorio")]
        [StringLength(80)]
        [Display(ResourceType = typeof(Propriedades), Name = "Nome")]
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

        public virtual void AdicionarEndereco(Endereco endereco)
        {
            if (Enderecos == null)
            {
                Enderecos = new List<Endereco>();
            }
            Enderecos.Add(endereco);
            endereco.Loja = this;
        }

        public virtual void AdicionarEnderecos(IList<Endereco> enderecos)
        {
            foreach (Endereco endereco in enderecos)
            {
                AdicionarEndereco(endereco);
            }
        }

        public virtual bool Validar()
        {
            return (!string.IsNullOrEmpty(this.Nome));
        }
    }
}
