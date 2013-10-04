using NHibernate;

namespace Graxei.FluentNHibernate.UnitOfWork
{
    public interface IUnitOfWorkNHibernate : IUnitOfWork
    {
        ISession SessaoAtual { get; }
    }
}
