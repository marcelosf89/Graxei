using System;
using NLog;

namespace FAST.Log
{
    /// <summary>
    /// Enum com todos os levels de log
    /// </summary>
    public enum Level
    {
        /// <summary>
        /// Erro fatal, irrecuperável
        /// </summary>
        Fatal,
        /// <summary>
        /// Erro não capturado mas recuperável
        /// </summary>
        Error,
        /// <summary>
        /// Aviso de possibilidade de erro
        /// </summary>
        Warn,
        /// <summary>
        /// Pontos de depuração
        /// </summary>
        Debug
    }

    /// <summary>
    /// Classe para realização de log de exceções
    /// </summary>
    public sealed class ExceptionLogger
    {
        #region Singleton
        private static readonly ExceptionLogger _instance = new ExceptionLogger();

        /// <summary>
        /// Instancia do objeto singleton
        /// </summary>
        public static ExceptionLogger Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Construtor privado do Singleton, inicia o _logger
        /// </summary>
        private ExceptionLogger()
        {
            this._logger = LogManager.GetCurrentClassLogger();
        }
        #endregion

        #region Fields
        private Logger _logger;
        #endregion

        #region Public Methods
        /// <summary>
        ///  Realiza log da mensagem de diagnóstico e da exception no level especificado
        /// </summary>
        /// <param name="level">O level do log como Warn, Error ou Fatal</param>
        /// <param name="message">Uma mensagem de diagnóstico</param>
        /// <param name="exception">A exception a ser logada</param>
        public void LogException(Level level, string message, Exception exception)
        {
            LogLevel logLevel = LogLevel.Off;
            switch (level)
            {
                case Level.Debug:
                    logLevel = LogLevel.Debug;
                    break;
                case Level.Warn:
                    logLevel = LogLevel.Warn;
                    break;
                case Level.Error:
                    logLevel = LogLevel.Error;
                    break;
                case Level.Fatal:
                    logLevel = LogLevel.Fatal;
                    break;
            }
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
            }
            string fullMessage = message + "\r\nType: " + exception.GetType().Name + "\r\nStackTrace: \r\n" + exception.StackTrace ;
            this._logger.LogException(logLevel, fullMessage, exception);
        }
        #endregion
    }
}
