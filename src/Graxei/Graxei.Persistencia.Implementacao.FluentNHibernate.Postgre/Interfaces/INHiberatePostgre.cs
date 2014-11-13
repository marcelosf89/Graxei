using FAST.Modelo;
using Graxei.FluentNHibernate.UnitOfWork;
using Graxei.Persistencia.Contrato;
using NHibernate;

namespace Graxei.Persistencia.Implementacao.NHibernate.Interfaces
{
    public interface INHiberatePostgre<T> : IRepositorioEntidades<T> where T : Entidade
    {
        ISession SessaoAtual { get; }
    }
}
