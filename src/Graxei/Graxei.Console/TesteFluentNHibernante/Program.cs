

using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Graxei.Modelo;
using NHibernate;
using NHibernate.Search.Event;
namespace TesteFluentNHibernante
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeSessionFactory();
        }

        private static void InitializeSessionFactory()

        {

            var config = Fluently.Configure()

                .Database(MySQLConfiguration.Standard

                              .ConnectionString(c => c.Server("graxei.c6lcvckogtg5.sa-east-1.rds.amazonaws.com").Database("graxei").Username("supergraxei").Password("73#tr071.")

                                  )
                                  .ShowSql()                           

                )

                .Mappings(m =>

                          m.FluentMappings.AddFromAssemblyOf<Produto>())

                .BuildConfiguration();              

 

//          Add NHibernate.Search listeners

            config.SetListener(NHibernate.Event.ListenerType.PostUpdate, new FullTextIndexEventListener());

            config.SetListener(NHibernate.Event.ListenerType.PostInsert, new FullTextIndexEventListener());

            config.SetListener(NHibernate.Event.ListenerType.PostDelete, new FullTextIndexEventListener());

 

            var factory = config.BuildSessionFactory();

            _sessionFactory= factory;

        }

        private static ISessionFactory _sessionFactory;
    }
}
