using System;
using System.Collections.Generic;
using Graxei.Aplicacao.Contrato.Operacoes;
using Graxei.Modelo;
using Graxei.Transversais.Utilidades;

namespace Graxei.Aplicacao.Implementacao.Operacoes
{
    public class GerenciamentoMensageria : IGerenciamentoMensageria
    {

        #region Construtor
        public GerenciamentoMensageria()
        {
        }
        #endregion

        #region Implementação de IMensageria

        /// <summary>
        /// Envia email
        /// </summary>
        /// <param name="nomeLoja">O nome da nova loja</param>
        /// <param name="usuario">O usuário a ser associado à loja</param>
        /// <returns></returns>
        public void Enviar(Mensagem mensagem, ConfiguracaoMail configuracao)
        {
            try
            {
                SendEmail mail = new SendEmail();
                mail.Subject = mensagem.Assunto;
                mail.Body = mensagem.Conteudo;
                mail.Host = configuracao.Servidor;
                mail.Port = configuracao.Porta;
                mail.AddTo(mensagem.DestinatarioEndereco, mensagem.DestinatarioNome); ;
                mail.SetFrom(configuracao.RemetenteEndereco, configuracao.RemetenteNome);
                mail.SetCredentials(configuracao.CredencialNome, configuracao.CredencialSenha);
                mail.EnableSSL(configuracao.RequerAutenticacao);
                mail.Send();
            }
            catch (Exception)
            {
                throw;
            }
       }

        #endregion

        #region Atributos Privados

        #endregion




    }
}
