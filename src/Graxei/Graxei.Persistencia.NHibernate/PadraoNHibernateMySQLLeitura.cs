using System.Collections.Generic;
using Graxei.FluentNHibernate.UnitOfWork;
using Graxei.Persistencia.Contrato;
using FAST.Modelo;
using Graxei.Persistencia.Implementacao.NHibernate.Interfaces;
using NHibernate;
using Microsoft.Practices.Unity;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public abstract class PadraoNHibernateMySQLLeitura<T> : INHiberateMySQL<T> where T : Entidade
    {
        protected ISession sessao;

        public T GetPorId(long id)
        {
            return GetSessaoAtual().Get<T>(id);
        }

        public IList<T> Todos()
        {
            return GetSessaoAtual().CreateCriteria<T>().List<T>();
        }

        public ISession GetSessaoAtual()
        {
            if (sessao == null)
            {
                sessao = UnitOfWorkNHibernate.GetInstancia().SessaoAtual;
            }

            return sessao;
        }

        public void SetSessaoAtual(ISession session) 
        {
            sessao = session;
        }
    }
}
