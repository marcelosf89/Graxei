using System.Linq;
using FAST.Modelo;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using NHibernate.Linq;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public class PlanosNHibernateMySQL : PadraoNHibernateMySQL<Plano>, IRepositorioPlanos
    {

        public System.Collections.Generic.IList<Plano> GetPlanosAtivos()
        {
            return GetSessaoAtual().Query<Plano>().Where(p => p.EstaAtivo).ToList<Plano>();
        }
    }
}