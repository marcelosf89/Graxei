using Graxei.Modelo;
using Graxei.Persistencia.Contrato;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public class LojasNHibernateMySQL : PadraoNHibernateMySQL<Loja>, IRepositorioLojas
    {
        #region Implementação de IRepositorioLojas

        public Loja Get(string nome)
        {
            return SessaoAtual.QueryOver<Loja>()
                              .Where(p => p.Nome.Trim().ToLower() == nome.Trim().ToLower())
                              .SingleOrDefault<Loja>();
        }

        #endregion
    }
}
