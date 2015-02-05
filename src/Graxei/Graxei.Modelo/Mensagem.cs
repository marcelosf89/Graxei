using System.Runtime.CompilerServices;
using FAST.Modelo;
using System;
using System.Collections.Generic;

namespace Graxei.Modelo
{
    public class Mensagem 
    {
        public Mensagem(string assunto, string conteudo, string destinatarioNome, string destinatarioEndereco)
        {
            this.Assunto = assunto;
            this.Conteudo = conteudo;
            this.DestinatarioNome = destinatarioNome;
            this.DestinatarioEndereco = destinatarioEndereco;
        }
                    
        public virtual string Assunto { get; set; }

        public virtual string Conteudo { get; set; }
             
        public virtual string DestinatarioNome { get; set; }

        public virtual string DestinatarioEndereco { get; set; }
        
        #region Métodos Sobrescritos
        public override bool Equals(object obj)
        {
            if (!(obj is Mensagem))
            {
                return false;
            }
            Mensagem msg = (Mensagem)obj;
            return (msg.DestinatarioEndereco == this.DestinatarioEndereco && msg.Assunto == this.Assunto);
        }

        public override int GetHashCode()
        {
            int retorno = 0;
            if (!(String.IsNullOrEmpty(DestinatarioEndereco)))
            {
                retorno += DestinatarioEndereco.GetHashCode();
            }
            return retorno;
        }

        #endregion

    }
}