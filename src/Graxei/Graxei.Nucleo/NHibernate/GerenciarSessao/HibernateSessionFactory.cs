using NHibernate;
using NHibernate.Cfg;

namespace Graxei.Nucleo.NHibernate.GerenciarSessao
{
    /// <summary>
    /// Classe para gerência da sessão do NHibernate
    /// </summary>
    public sealed class HibernateSessionFactory
    {
        #region Singleton
        private static readonly HibernateSessionFactory _instance = new HibernateSessionFactory();
        private string _user;
        /// <summary>
        /// Instância do objeto singleton
        /// </summary>
        public static HibernateSessionFactory Instance
        {
            get
            {
                return _instance; 
            }
        }

        /// <summary>
        /// Instância do objeto singleton
        /// </summary>

        #endregion

        #region Fields
        private ISessionFactory _sessionFactory;
        #endregion

        #region Private Methods
        /// <summary>
        /// Inicializa o HibernateSessionManager criando um SessionFactory
        /// </summary>
        private void initSessionFactory()
        {
            this._sessionFactory = this.SessionFactory();
        }

        public ISessionFactory SessionFactory()
        {
            if (this._sessionFactory == null)
            {
                // Aqui o Nhibernate vai procurar as configurações no 
                // arquivo "NHibernate.config"
                var config = new Configuration().Configure();

                this._sessionFactory = config.BuildSessionFactory();
            }

            return _sessionFactory;
        }

        #endregion
    }
}
