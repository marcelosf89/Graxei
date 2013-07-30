using System;
using NLog;

namespace FAST.Log
{

    /// <summary>
    /// Classe para realização de log de exceções
    /// </summary>
    public sealed class InformationLogger
    {
        #region Singleton
        private static readonly InformationLogger _instance = new InformationLogger();

        /// <summary>
        /// Instancia do objeto singleton
        /// </summary>
        public static InformationLogger Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Construtor privado do Singleton, inicia o _logger
        /// </summary>
        private InformationLogger()
        {
            this._logger = LogManager.GetCurrentClassLogger();
        }
        #endregion

        #region Fields
        private Logger _logger;
        #endregion

        #region Public Methods
        
        public void LogInformation(string userName, string message)
        {
            string fullMessage = "User: " + userName + "\r\nStackTrace: \r\n" + message ;
            this._logger.Log(LogLevel.Info, fullMessage);
        }
        #endregion
    }
}
