using System.Collections.Generic;
using Graxei.FluentNHibernate.UnitOfWork;
using Graxei.Persistencia.Contrato;
using FAST.Modelo;
using NHibernate;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public abstract class PadraoNHibernateMySQLLeitura<T> : IRepositorioEntidades<T> where T : Entidade
    {
        public T GetPorId(long id)
        {
            return SessaoAtual.Get<T>(id);
        }

        public IList<T> Todos()
        {
            return SessaoAtual.CreateCriteria<T>().List<T>();
        }

        protected ISession SessaoAtual
        {
            get { return UnitOfWorkNHibernate.Instance.SessaoAtual; }
        }
    }
}
