using System;
using System.Runtime.Serialization;

namespace FAST.Exceptions
{
    /// <summary>
    /// Classe de exceção para envio de mesnagens
    /// </summary>
    [Serializable]
    public class SendEmailException : Exception
    {
        #region Constructors
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public SendEmailException() : base() { }

        /// <summary>
        /// Construtor inicializando a mensagem
        /// </summary>
        /// <param name="message">Mensagem da exceção</param>
        public SendEmailException(string message) : base(message) { }

        /// <summary>
        /// Construtor inicializando a mensagem e a innerException
        /// </summary>
        /// <param name="message">Mensagem da exceção</param>
        /// <param name="innerException">Exceção interna da exceção</param>
        public SendEmailException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Construtor com serialização por causa da Design Patterns
        /// </summary>
        /// <param name="info">Informação de serialização</param>
        /// <param name="context">Contexto de stream</param>
        protected SendEmailException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endregion
    }
}
