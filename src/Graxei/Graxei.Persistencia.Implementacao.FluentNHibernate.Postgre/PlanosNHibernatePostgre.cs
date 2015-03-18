using System.Linq;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using NHibernate.Linq;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public class PlanosNHibernatePostgre : PadraoNHibernatePostgre<Plano>, IRepositorioPlanos
    {

        public System.Collections.Generic.IList<Plano> GetPlanosAtivos()
        {
            return SessaoAtual.Query<Plano>().Where(p => p.EstaAtivo).ToList<Plano>();
        }
    }
}