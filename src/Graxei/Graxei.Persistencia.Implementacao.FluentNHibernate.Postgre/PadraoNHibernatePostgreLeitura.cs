using System.Collections.Generic;
using Graxei.FluentNHibernate.UnitOfWork;
using Graxei.Persistencia.Implementacao.NHibernate.Interfaces;
using NHibernate;
using Graxei.Modelo.Generico;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public abstract class PadraoNHibernatePostgreLeitura<T> : INHiberatePostgre<T> where T : Entidade
    {

        public T GetPorId(long id)
        {
            return SessaoAtual.Get<T>(id);
        }

        public IList<T> Todos()
        {
            return SessaoAtual.CreateCriteria<T>().List<T>();
        }

        public ISession SessaoAtual
        {
            get { return UnitOfWorkNHibernate.GetInstancia().SessaoAtual; }
        }
    }
}
