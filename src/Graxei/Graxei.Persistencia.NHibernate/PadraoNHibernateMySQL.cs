using Graxei.Persistencia.Contrato;
using FAST.Modelo;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public abstract class PadraoNHibernateMySQL<T> : PadraoNHibernateMySQLLeitura<T>, IRepositorioIrrestrito<T> where T : Entidade
    {
        public T Salvar(T t)
        {
            GetSessaoAtual().SaveOrUpdate(t);
            return t;
        }

        public void Excluir(T t)
        {
            GetSessaoAtual().Delete(t);
        }

    }
}
