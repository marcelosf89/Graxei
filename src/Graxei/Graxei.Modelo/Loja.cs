﻿using Graxei.Modelo.Generico;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Graxei.Transversais.Idiomas;
using System.Text;

namespace Graxei.Modelo
{
    public class Loja : ExclusaoLogica
    {
        [Required(ErrorMessageResourceType = typeof(Textos), ErrorMessageResourceName = "NomeObrigatorio")]
        [StringLength(80)]
        [Display(ResourceType = typeof(Propriedades), Name = "Nome")]
        public virtual string Nome { get; set; }

        public virtual byte[] Logotipo { get; set; }

        public virtual IList<Endereco> Enderecos { get; protected internal set; }

        public virtual IList<Usuario> Usuarios { get; protected internal set; }

        public virtual Plano Plano { get; set; }

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

        public virtual void AdicionarUsuario(Usuario usuario)
        {
            if (Usuarios == null)
            {
                Usuarios = new List<Usuario>();
            }
            if (!Usuarios.Contains(usuario))
            {
                Usuarios.Add(usuario);
            }
        }

        public virtual void AdicionarUsuarios(IList<Usuario> usuarios)
        {
            if (Usuarios == null)
            {
                Usuarios = new List<Usuario>();
            }
            foreach (Usuario usuario in usuarios)
            {
                AdicionarUsuario(usuario);
            }
        }

        public virtual bool Validar()
        {
            return (!string.IsNullOrEmpty(this.Nome));
        }

        [Display(ResourceType = typeof(Propriedades), Name = "HabilitarUrl")]
        public virtual bool HabilitarUrl { get; set; }

        [StringLength(100)]
        [Display(ResourceType = typeof(Propriedades), Name = "Url")]
        public virtual String Url { get; set; }

        [Display(ResourceType = typeof(Propriedades), Name = "DescricaoPaginaInicial")]
        public virtual String DescricaoPaginaInicial { get; set; }

    }
}
