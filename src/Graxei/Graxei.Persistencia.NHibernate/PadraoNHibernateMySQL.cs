using Graxei.Persistencia.Contrato;
using FAST.Modelo;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public abstract class PadraoNHibernateMySQL<T> : PadraoNHibernateMySQLLeitura<T>, IRepositorioIrrestrito<T> where T : Entidade
    {
        public void Salvar(T t)
        {
            SessaoAtual.SaveOrUpdate(t);
        }

        public void Excluir(T t)
        {
            SessaoAtual.Delete(t);
        }

    }
}
