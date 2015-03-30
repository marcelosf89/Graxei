using System.Linq;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using NHibernate.Linq;
using System.Collections.Generic;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public class PlanosRepositorio: PadraoNHibernatePostgre<Plano>, IRepositorioPlanos
    {
        public Plano GetPlano(long idLoja)
        {
            return SessaoAtual.Query<Loja>().Where(p => p.Id == idLoja).Select(q => q.Plano).SingleOrDefault();
        }

        public IList<Plano> GetPlanosAtivos()
        {
            return SessaoAtual.Query<Plano>().Where(p => p.EstaAtivo).ToList<Plano>();
        }
    }
}