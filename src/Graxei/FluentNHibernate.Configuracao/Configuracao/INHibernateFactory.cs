using NHibernate;

namespace Graxei.FluentNHibernate.Configuracao
{
    public interface INHibernateFactory
    {
        ISession OpenSession();
        ISession GetSession();
        ISessionFactory GetSessionFactory();
    }
}
