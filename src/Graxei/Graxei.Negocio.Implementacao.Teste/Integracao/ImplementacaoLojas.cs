using System.Configuration;
using System.Data;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Graxei.FluentNHibernate.Convencoes;
using Graxei.FluentNHibernate.Mapeamento;
using Graxei.FluentNHibernate.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;
using System.Collections.Generic;

namespace Graxei.Negocio.Implementacao.Teste.Integracao
{
    [TestClass]
    public class ImplementacaoLojas
    {
        ///[AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            string server = ConfigurationManager.AppSettings["dbserver"];
            string username = ConfigurationManager.AppSettings["dbusername"];
            string password = ConfigurationManager.AppSettings["dbpassword"];
            _factory =
                Fluently.
                    Configure().CurrentSessionContext<ThreadLocalSessionContext>().
                    Database(MySQLConfiguration
                                 .Standard
                                 .ConnectionString(c => c.Server(server)
                                                         .Database("graxei")
                                                            .Username(username)
                                                            .Password(password)
                                 ).ShowSql()
                    ).
                    Mappings(m =>
                             m.FluentMappings.AddFromAssemblyOf<ProdutoMap>()
                              .Conventions.Add<ClasseComumConvencao>()).
                    BuildConfiguration().BuildSessionFactory();
            UnitOfWorkNHibernate.GetInstancia(_factory);

            ResetDatabase(_factory);
        }

        private static void ResetDatabase(ISessionFactory factory)
        {
            ISession session = _factory.GetCurrentSession();
            session.CreateSQLQuery("call graxei.cleanup_database()").ExecuteUpdate();
        }

        private static ISessionFactory _factory;
    }
}
