namespace Graxei.IntegracaoConfiguracao.Teste
{
    public class TestesServicoConfiguracao
    {

        public static 
        if (this._sessionFactory == null)
            {
                Configuration config =
                Fluently.
                Configure().CurrentSessionContext<WebSessionContext>().
                    //Configure().CurrentSessionContext<CallSessionContext>().
                Database(MySQLConfiguration
                        .Standard
                        .ConnectionString(c => c.Server(_server)
                                                .Database(_database)
                                                .Username(_username)
                                                .Password(_password)
                         ).ShowSql()
                ).
                Mappings(m =>
                         m.FluentMappings.AddFromAssemblyOf<ProdutoMap>().Conventions.Add<ClasseComumConvencao>()).
                BuildConfiguration();
                this._sessionFactory = config.BuildSessionFactory();
            }
    }
}
