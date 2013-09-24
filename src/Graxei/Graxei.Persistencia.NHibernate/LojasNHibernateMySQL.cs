using System.Linq;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Utilidades.NHibernate;
using NHibernate.Linq;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public class LojasNHibernateMySQL : PadraoNHibernateMySQL<Loja>, IRepositorioLojas
    {

        #region Implementação de IRepositorioLojas

        public Loja Get(string nome)
        {
            return SessaoAtual.Query<Loja>()
                              .SingleOrDefault<Loja>(loja => Queries.StringPadrao(loja.Nome) == Queries.StringPadrao(nome));
        }

        #endregion
    }
}
