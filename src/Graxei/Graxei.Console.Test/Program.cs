using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Graxei.FluentNHibernate.Mapeamento;
using Graxei.Modelo;
using NHibernate;
using NHibernate.Search.Event;
using NHibernate.Tool.hbm2ddl;
using System.Collections.Generic;

namespace Graxei.Console.Test
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

                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<TipoLogradouroMap>())
                          //.ExposeConfiguration( cfg => new SchemaExport(cfg).Create(true,true))
                .BuildConfiguration();



            //          Add NHibernate.Search listeners

            config.SetListener(NHibernate.Event.ListenerType.PostUpdate, new FullTextIndexEventListener());

            config.SetListener(NHibernate.Event.ListenerType.PostInsert, new FullTextIndexEventListener());

            config.SetListener(NHibernate.Event.ListenerType.PostDelete, new FullTextIndexEventListener());



            var factory = config.BuildSessionFactory();

            _sessionFactory = factory;


            using (var s = _sessionFactory.OpenSession())
            {
                using (var transaction = s.BeginTransaction())
                {
                    Produto pro = new Produto();
                    pro.Descricao = "Teste do Goo.Search";
                    pro.Codigo = "GOO";

                    s.Save(pro);
                    transaction.Commit();
                }


                using (var search = NHibernate.Search.Search.CreateFullTextSession(s))
                {
                    using (var transaction = s.BeginTransaction())
                    {
                        //Lucene.Net.Search.Query query = new 

                        IList<Produto> carSearchResults = search.CreateFullTextQuery<Produto>("Descricao:Teste")
                            //.CreateFullTextQueryAllField<Produto>("Teste")
                            .SetMaxResults(5)
                            .List<Produto>();

                        foreach (var car in carSearchResults)
                        {
                            System.Console.WriteLine(car.Descricao);
                        }

                        transaction.Commit();
                    }
                }
            }

            System.Console.ReadLine();

        }

        private static ISessionFactory _sessionFactory;
    }
}
