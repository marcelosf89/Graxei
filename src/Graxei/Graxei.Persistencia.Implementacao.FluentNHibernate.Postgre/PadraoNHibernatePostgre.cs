using Graxei.Persistencia.Contrato;
using Graxei.Modelo.Generico;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public abstract class PadraoNHibernatePostgre<T> : PadraoNHibernatePostgreLeitura<T>, IRepositorioIrrestrito<T> where T : Entidade
    {
        public T Salvar(T t)
        {
            SessaoAtual.SaveOrUpdate(t);
            return t;
        }

        public void Excluir(T t)
        {
            SessaoAtual.Delete(t);
        }

    }
}
