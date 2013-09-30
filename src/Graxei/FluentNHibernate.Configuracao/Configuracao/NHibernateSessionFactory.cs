using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Graxei.FluentNHibernate.Convencoes;
using Graxei.FluentNHibernate.Mapeamento;
using Graxei.Modelo;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;

namespace Graxei.FluentNHibernate.Configuracao
{
    /// <summary>
    /// Classe para gerência da sessão do NHibernate
    /// </summary>
    public sealed class NHibernateSessionFactory
    {

        #region Singleton
        private static readonly NHibernateSessionFactory _instance = new NHibernateSessionFactory();
        private string _user;
        /// <summary>
        /// Instância do objeto singleton
        /// </summary>
        public static NHibernateSessionFactory Instance
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

        #region Métodos Públicos

        public ISessionFactory SessionFactory()
        {
            if (this._sessionFactory == null)
            {
                Configuration config =
                Fluently.
                    Configure().CurrentSessionContext<WebSessionContext>().
                //Configure().CurrentSessionContext<CallSessionContext>().
                Database(MySQLConfiguration.Standard
                                           .ConnectionString(c => c.Server("graxei.c6lcvckogtg5.sa-east-1.rds.amazonaws.com").Database("graxei").Username("supergraxei").Password("73#tr071.")
                         ).ShowSql()
                ).
                Mappings(m =>
                         m.FluentMappings.AddFromAssemblyOf<ProdutoMap>().Conventions.Add<ClasseComumConvencao>()).
                BuildConfiguration();
                this._sessionFactory = config.BuildSessionFactory();
            }

            return _sessionFactory;
        }
        #endregion

        #region Fields
        private ISessionFactory _sessionFactory;
        #endregion

    }
}
