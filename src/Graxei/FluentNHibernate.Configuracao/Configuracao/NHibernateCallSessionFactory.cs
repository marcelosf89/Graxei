using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Graxei.FluentNHibernate.Convencoes;
using Graxei.FluentNHibernate.Mapeamento;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;

namespace Graxei.FluentNHibernate.Configuracao
{
    /// <summary>
    /// Classe para ger�ncia da sess�o do NHibernate
    /// </summary>
    public sealed class NHibernateCallSessionFactory : INHibernateFactory
    {

        #region Singleton
        private static readonly NHibernateWebSessionFactory _instance = new NHibernateWebSessionFactory();
        private string _user;
        /// <summary>
        /// Inst�ncia do objeto singleton
        /// </summary>
        public static NHibernateWebSessionFactory Instance
        {
            get
            {
                return _instance; 
            }
        }

        public ISession GetSession()
        {
            return _sessionFactory.GetCurrentSession();
        }

        /// <summary>
        /// Inst�ncia do objeto singleton
        /// </summary>

        #endregion

        #region M�todos P�blicos

        public ISessionFactory GetSessionFactory()
        {
            if (this._sessionFactory == null)
            {
                Configuration config =
                Fluently.
                Configure().CurrentSessionContext<CallSessionContext>().
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
