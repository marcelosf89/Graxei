using System;
using System.Configuration;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Graxei.FluentNHibernate.Convencoes;
using Graxei.FluentNHibernate.Mapeamento;
using Graxei.Transversais.Idiomas;
using NHibernate;
using NHibernate.Context;
using Configuration = NHibernate.Cfg.Configuration;
using NHibernate.Tool.hbm2ddl;

namespace Graxei.FluentNHibernate.Configuracao
{
    public sealed class NHibernateWebSessionFactory : INHibernateFactory
    {

        private static NHibernateWebSessionFactory _instance;
        private string _user;

        private NHibernateWebSessionFactory()
        {
        }

        private NHibernateWebSessionFactory(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public static NHibernateWebSessionFactory GetInstancia()
        {
            return _instance ?? (_instance = new NHibernateWebSessionFactory());
        }

        public static NHibernateWebSessionFactory GetInstancia(ISessionFactory sessionFactory)
        {
            if (_instance != null)
            {
                throw new InvalidOperationException(ErrosEstrutura.CriacaoNHSessionFactory);
            }
            _instance = new NHibernateWebSessionFactory(sessionFactory);
            return _instance;
        }

        public ISessionFactory GetSessionFactory()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["graxei"].ToString();
            if (this._sessionFactory == null)
            {
                FluentConfiguration config = Fluently.Configure();

                switch (_type)
                {
                    case "MYSQL":
                        config.CurrentSessionContext<WebSessionContext>()
                              .Database(MySQLConfiguration.Standard.ConnectionString(connectionString)
                              .ShowSql()).Mappings(m => m.FluentMappings.AddFromAssemblyOf<ProdutoMap>()
                              .Conventions.Add<ClasseComumConvencao>()).BuildConfiguration();
                        break;
                    case "POSTGRESQL":
                    default:
                        config.CurrentSessionContext<WebSessionContext>()
                              .Database(PostgreSQLConfiguration.Standard.ConnectionString(connectionString)
                              .ShowSql()).Mappings(m => m.FluentMappings.AddFromAssemblyOf<ProdutoMap>()
                              .Conventions.Add<ClasseComumConvencao>())
                              .BuildConfiguration();
                        break;
                }
                     
                config.ExposeConfiguration(cfg => { //new SchemaExport(cfg).Execute(true, true, false);
                     });
                this._sessionFactory = config.BuildSessionFactory();
            }

            return _sessionFactory;
        }

        public ISession OpenSession()
        {
            return _sessionFactory.OpenSession();
        }

        public ISession GetSession()
        {
            return _sessionFactory.GetCurrentSession();
        }

        private ISessionFactory _sessionFactory;
        private string _type = ConfigurationManager.AppSettings["dbtype"];
    }
}
