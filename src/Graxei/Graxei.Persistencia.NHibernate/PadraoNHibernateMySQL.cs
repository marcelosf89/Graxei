using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graxei.Persistencia.Contrato;
using Graxei.FluentNHibernate.Configuracao;
using FAST.Modelo;
using NHibernate;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public abstract class PadraoNHibernateMySQL<T> : IRepositorioEntidades<T> where T : Entidade
    {
        public void Salvar(T t)
        {
            SessaoAtual.SaveOrUpdate(t);
        }

        public void Excluir(T t)
        {
            SessaoAtual.Delete(t);
        }

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
            get { return NHibernateSessionPerRequest.GetCurrentSession(); }
        }
    }
}
