using System;
using System.Runtime.Serialization;

namespace FAST.Exceptions
{
    /// <summary>
    /// Classe de exce��o para envio de mesnagens
    /// </summary>
    [Serializable]
    public class SendEmailException : Exception
    {
        #region Constructors
        /// <summary>
        /// Construtor padr�o
        /// </summary>
        public SendEmailException() : base() { }

        /// <summary>
        /// Construtor inicializando a mensagem
        /// </summary>
        /// <param name="message">Mensagem da exce��o</param>
        public SendEmailException(string message) : base(message) { }

        /// <summary>
        /// Construtor inicializando a mensagem e a innerException
        /// </summary>
        /// <param name="message">Mensagem da exce��o</param>
        /// <param name="innerException">Exce��o interna da exce��o</param>
        public SendEmailException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Construtor com serializa��o por causa da Design Patterns
        /// </summary>
        /// <param name="info">Informa��o de serializa��o</param>
        /// <param name="context">Contexto de stream</param>
        protected SendEmailException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endregion
    }
}
