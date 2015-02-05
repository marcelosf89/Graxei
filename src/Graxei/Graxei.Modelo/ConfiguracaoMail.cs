using System.Runtime.CompilerServices;
using FAST.Modelo;
using System;
using System.Collections.Generic;

namespace Graxei.Modelo
{
    public class ConfiguracaoMail 
    {
        public ConfiguracaoMail(string credencialNome, string credencialSenha, string servidor, int porta, string remetenteNome, string remetendeEndereco)
        {
            this.CredencialNome = credencialNome;
            this.CredencialSenha = credencialSenha;
            this.Servidor = servidor;
            this.Porta = porta;
            this.RemetenteNome = remetenteNome;
            this.RemetenteEndereco = remetendeEndereco;
        }
                    
        public virtual string CredencialNome { get; set; }

        public virtual string CredencialSenha { get; set; }

        public virtual string Servidor { get; set; }
        
        public virtual int Porta { get;  set; }

        public virtual string RemetenteNome { get; set; }

        public virtual string RemetenteEndereco { get; set; }

        #region Métodos Sobrescritos
        public override bool Equals(object obj)
        {
            if (!(obj is ConfiguracaoMail))
            {
                return false;
            }
            ConfiguracaoMail cfm = (ConfiguracaoMail)obj;
            return (cfm.Servidor == this.Servidor && cfm.Porta == this.Porta);
        }

        public override int GetHashCode()
        {
            int retorno = 0;
            if (!(String.IsNullOrEmpty(Servidor)))
            {
                retorno += Servidor.GetHashCode();
            }
            return retorno;
        }

        #endregion

    }
}