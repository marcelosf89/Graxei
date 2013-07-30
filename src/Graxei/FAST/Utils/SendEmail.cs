using System;
using System.Net;
using System.Net.Mail;
using FAST.Exceptions;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace FAST.Utils
{
    /// <summary>
    /// Classe para enviar mensagens de email
    /// </summary>
    public sealed class SendEmail : IDisposable
    {
        #region Fields
        private MailMessage _mailMessage;
        private SmtpClient _smtpClient;
        #endregion

        #region Properties
        /// <summary>
        /// Grava e recupera o Body
        /// </summary>
        public string Body
        {
            get { return this._mailMessage.Body; }
            set { this._mailMessage.Body = value; }
        }

        /// <summary>
        /// Grava e recupera o Host
        /// </summary>
        public string Host
        {
            get { return this._smtpClient.Host; }
            set { this._smtpClient.Host = value; }
        }

        /// <summary>
        /// Grava e recupera o Port
        /// </summary>
        public int Port
        {
            get { return this._smtpClient.Port; }
            set { this._smtpClient.Port = value; }
        }

        /// <summary>
        /// Grava e recupera se o Body é HTML
        /// </summary>
        public bool IsBodyHtml
        {
            get { return this._mailMessage.IsBodyHtml; }
            set { this._mailMessage.IsBodyHtml = value; }
        }

        /// <summary>
        /// Grava e recupera o Subject
        /// </summary>
        public string Subject
        {
            get { return this._mailMessage.Subject; }
            set { this._mailMessage.Subject = value; }
        }

        /// <summary>
        /// Recupera o To
        /// </summary>
        public MailAddressCollection To
        {
            get { return this._mailMessage.To; }
        }

        /// <summary>
        /// Recupera o CC
        /// </summary>
        public MailAddressCollection CC
        {
            get { return this._mailMessage.CC; }
        }

        /// <summary>
        /// Recupera o Bcc
        /// </summary>
        public MailAddressCollection Bcc
        {
            get { return this._mailMessage.Bcc; }
        }

        /// <summary>
        /// Grave e recupera o Attachment
        /// </summary>
        public AttachmentCollection Attachments
        {
            get { return this._mailMessage.Attachments; }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Grande variedade de construtores com Host, Port, MailFrom, MailTo, DisplayFromName e DisplayToName
        /// </summary>
        public SendEmail()
        {
            this._mailMessage = new MailMessage();
            this._smtpClient = new SmtpClient();
        }

        /// <summary>
        /// Construtor inicializando o host
        /// </summary>
        /// <param name="host">Host do servidor de SMTP</param>
        public SendEmail(string host)
            : this()
        {
            this._smtpClient.Host = host;
        }

        /// <summary>
        /// Construtor inicializando o host e o port
        /// </summary>
        /// <param name="host">Host do servidor de SMTP</param>
        /// <param name="port">Port do servidor de SMTP</param>
        public SendEmail(string host, int port)
            : this(host)
        {
            this._smtpClient.Port = port;
        }

        /// <summary>
        /// Construtor inicializando o from e o to da mensagem
        /// </summary>
        /// <param name="mailFrom">From da mensagem</param>
        /// <param name="mailTo">To da mensagem</param>
        public SendEmail(string mailFrom, string mailTo)
        {
            this._mailMessage = new MailMessage(mailFrom, mailTo);
            this._smtpClient = new SmtpClient();
        }

        /// <summary>
        /// Construtor inicializando o host, o from e o to da mensagem
        /// </summary>
        /// <param name="host">Host do servidor de SMTP</param>
        /// <param name="mailFrom">From da mensagem</param>
        /// <param name="mailTo">To da mensagem</param>
        public SendEmail(string host, string mailFrom, string mailTo)
            : this(mailFrom, mailTo)
        {
            this._smtpClient.Host = host;
        }

        /// <summary>
        /// Construtor inicializando o host, o port, o from e o to da mensagem
        /// </summary>
        /// <param name="host">Host do servidor de SMTP</param>
        /// <param name="port">Port do servidor de SMTP</param>
        /// <param name="mailFrom">From da mensagem</param>
        /// <param name="mailTo">To da mensagem</param>
        public SendEmail(string host, int port, string mailFrom, string mailTo)
            : this(host, mailFrom, mailTo)
        {
            this._smtpClient.Port = port;
        }

        /// <summary>
        /// Construtor mais completo, inicializando todo o objeto MailAdress de from e to
        /// </summary>
        /// <param name="mailFrom">From da mensagem</param>
        /// <param name="displayFromName">Nome que será apresentado como from</param>
        /// <param name="mailTo">To da mensagem</param>
        /// <param name="displayToName">Nome que será apresentado como to</param>
        public SendEmail(string mailFrom, string displayFromName, string mailTo, string displayToName)
        {
            this._mailMessage = new MailMessage(new MailAddress(mailFrom, displayFromName),
                                                new MailAddress(mailTo, displayToName));
            this._smtpClient = new SmtpClient();
        }

        /// <summary>
        /// Construtor mais completo, inicializando o host e todo o objeto MailAdress de from e to
        /// </summary>
        /// <param name="host">Host do servidor de SMTP</param>
        /// <param name="mailFrom">From da mensagem</param>
        /// <param name="displayFromName">Nome que será apresentado como from</param>
        /// <param name="mailTo">To da mensagem</param>
        /// <param name="displayToName">Nome que será apresentado como to</param>
        public SendEmail(string host, string mailFrom, string displayFromName, string mailTo, string displayToName)
            : this(mailFrom, displayFromName, mailTo, displayToName)
        {
            this._smtpClient.Host = host;
        }

        /// <summary>
        /// Construtor mais completo, inicializando o host, o port e todo o objeto MailAdress de from e to
        /// </summary>
        /// <param name="host">Host do servidor de SMTP</param>
        /// <param name="port">Port do servidor de SMTP</param>
        /// <param name="mailFrom">From da mensagem</param>
        /// <param name="displayFromName">Nome que será apresentado como from</param>
        /// <param name="mailTo">To da mensagem</param>
        /// <param name="displayToName">Nome que será apresentado como to</param>
        public SendEmail(string host, int port, string mailFrom, string displayFromName, string mailTo, string displayToName)
            : this(host, mailFrom, displayFromName, mailTo, displayToName)
        {
            this._smtpClient.Port = port;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Adiciona bcc
        /// </summary>
        /// <param name="mailBcc">Endereço de email para bcc</param>
        /// <param name="displayBccName">Nome que será apresentado como bcc</param>
        public void AddBcc(string mailBcc, string displayBccName)
        {
            this._mailMessage.Bcc.Add(new MailAddress(mailBcc, displayBccName));
        }

        /// <summary>
        /// Adiciona cc
        /// </summary>
        /// <param name="mailCC">Endereço de email para cc</param>
        /// <param name="displayCCName">Nome que será apresentado como cc</param>
        public void AddCC(string mailCC, string displayCCName)
        {
            this._mailMessage.CC.Add(new MailAddress(mailCC, displayCCName));
        }

        /// <summary>
        /// Adiciona to
        /// </summary>
        /// <param name="mailTo">Endereço de email para to</param>
        /// <param name="displayToName">Nome que será apresentado como to</param>
        public void AddTo(string mailTo, string displayToName)
        {
            this._mailMessage.To.Add(new MailAddress(mailTo, displayToName));
        }

        /// <summary>
        /// Adiciona Certificate
        /// </summary>
        /// <param name="mailTo">Endereço de email para to</param>
        /// <param name="displayToName">Nome que será apresentado como to</param>
        public void AddCertificate(X509Certificate certificate)
        {
            this._smtpClient.ClientCertificates.Add(certificate);
        }

        /// <summary>
        /// Envia a mensagem
        /// </summary>
        /// <exception cref="SendEmailException">Falta de informação ou falha no envio</exception>
        public void Send()
        {
            if (string.IsNullOrEmpty(this._smtpClient.Host))
            {
                throw new SendEmailException("A propriedade Host deve conter o endereço de um servidor SMTP");
            }
            if (string.IsNullOrEmpty(this._mailMessage.From.Address))
            {
                throw new SendEmailException("A propriedade From deve conter um email");
            }
            if (this._mailMessage.To.Count == 0)
            {
                throw new SendEmailException("A propriedade To deve possuir pelo menos um destinatário");
            }
            try
            {
                ServicePointManager.ServerCertificateValidationCallback =
                delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };

                this._smtpClient.Send(this._mailMessage);
            }
            catch (Exception exception)
            {
                throw new SendEmailException("Erro ao enviar a mensagem", exception);
            }
        }

        /// <summary>
        /// Enable or Disable default credentials
        /// </summary>
        /// <param name="value"></param>
        public void UseDefaultCredentials(bool value)
        {
                        
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            _smtpClient.UseDefaultCredentials = value;

        }

        /// <summary>
        /// Enable or disable SSL Encryptation
        /// </summary>
        /// <param name="value"></param>
        public void EnableSSL(bool value)
        {
            this._smtpClient.EnableSsl = value;
        }

        /// <summary>
        /// Grava as credenciais para envio de mensagens autenticadas
        /// </summary>
        /// <param name="userName">Nome de usuário do servidor SMTP</param>
        /// <param name="password">Senha do login fornecido</param>
        public void SetCredentials(string userName, string password)
        {
            this._smtpClient.UseDefaultCredentials = false;
            this._smtpClient.Credentials = new NetworkCredential(userName, password);            
        }

        /// <summary>
        /// Grava as credenciais para envio de mensagens autenticadas
        /// </summary>
        /// <param name="userName">Nome de usuário do servidor SMTP</param>
        /// <param name="password">Senha do login fornecido</param>
        /// <param name="password">dominio de rede do fornecido</param>
        public void SetCredentials(string userName, string password, string domain)
        {
            this._smtpClient.UseDefaultCredentials = false;
            this._smtpClient.Credentials = new NetworkCredential(userName, password, domain);
        }

        /// <summary>
        /// Grava o From
        /// </summary>
        /// <param name="mailFrom">Endereço de email para from</param>
        /// <param name="displayFromName">Nome que será apresentado como from</param>
        public void SetFrom(string mailFrom, string displayFromName)
        {
            this._mailMessage.From = new MailAddress(mailFrom, displayFromName);
        }

        /// <summary>
        /// Grava o reply to
        /// </summary>
        /// <param name="mailReplyTo">Endereço de email para reply to</param>
        /// <param name="displayReplyToName">Nome que será apresentado como reply to</param>
        public void SetReplyTo(string mailReplyTo, string displayReplyToName)
        {
            this._mailMessage.ReplyToList.Add(new MailAddress(mailReplyTo, displayReplyToName));
        }

        /// <summary>
        /// Grava o sender
        /// </summary>
        /// <param name="mailSender">Endereço de email para sender</param>
        /// <param name="displaySenderName">Nome que será apresentado como sender</param>
        public void SetSender(string mailSender, string displaySenderName)
        {
            this._mailMessage.Sender = new MailAddress(mailSender, displaySenderName);
        }
        #endregion

        #region IDisposable Members
        /// <summary>
        /// Descarta objetos que também herdam de IDisposable
        /// </summary>
        public void Dispose()
        {
            this._mailMessage.Dispose();
        }
        #endregion
    }
}
