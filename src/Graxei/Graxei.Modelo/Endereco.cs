﻿using System.Runtime.CompilerServices;
using Graxei.Modelo.Generico;
using System;
using System.Collections.Generic;

namespace Graxei.Modelo
{
    public class Endereco : ExclusaoLogica
    {
        public Endereco()
        {

        }

        public virtual string Logradouro { get; set; }

        public virtual string Numero { get; set; }
        
        public virtual string Complemento { get; set; }
        
        public virtual Loja Loja { get;  set; }
        
        public virtual Bairro Bairro { get; set; }
        
        public virtual IList<Telefone> Telefones { get; set; }

        public virtual String Cnpj { get; set; }

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

        public virtual void AdicionarTelefone(Telefone telefone)
        {
            if (Telefones == null)
            {
                Telefones = new List<Telefone>();
            }
            Telefones.Add(telefone);
            telefone.Endereco = this;
        }

        public virtual void AdicionarTelefones(List<Telefone> telefones)
        {
            if (telefones == null)
            {
                return;
            }
            if (Telefones == null)
            {
                Telefones = new List<Telefone>();
            }
            else
            {
                List<Telefone> novaLista = new List<Telefone>();
                novaLista.AddRange(telefones);
                Telefones = novaLista;
            }
            ((List<Telefone>)Telefones).AddRange(telefones);
            foreach (Telefone telefone in telefones)
            {
                telefone.Endereco = this;
            }
        }

        public virtual void SubstituirTelefones(List<Telefone> telefones)
        {
            if (telefones == null)
            {
                return;
            }
            Telefones = new List<Telefone>();
            ((List<Telefone>)Telefones).AddRange(telefones);
            foreach (Telefone telefone in telefones)
            {
                telefone.Endereco = this;
            }
        }

        public override string ToString()
        {
            if (Bairro == null || this.Bairro.Cidade == null || this.Bairro.Cidade.Estado == null)
            {
                return "<Endereço Incompleto>";
            }
            string retorno=
                string.Format(@" - {0} - {1} - {2}", this.Bairro,
                              this.Bairro.Cidade, this.Bairro.Cidade.Estado);
            retorno = GetLogradouroMaisNumero() + retorno;

            return retorno;
        }

        public virtual string GetLogradouroMaisNumero()
        {
            string retorno = string.Format(@"{0}, {1}|*COMP*|", this.Logradouro, this.Numero);

            if (!string.IsNullOrEmpty(this.Complemento))
            {
                retorno = retorno.Replace("|*COMP*|", ", " + this.Complemento);
            }
            else
            {
                retorno = retorno.Remove(retorno.IndexOf("|*COMP*|", System.StringComparison.Ordinal), 8);
            }

            return retorno;
        }
        public virtual bool Validar()
        {
            return (!String.IsNullOrEmpty(this.Logradouro) && !String.IsNullOrEmpty(this.Numero)
                    && this.Bairro != null && this.Bairro.Validar());
        }

        
    }
}