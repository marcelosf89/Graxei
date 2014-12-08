using System.Linq;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using NHibernate.Linq;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public class TiposTelefoneNHibernateMySQL : PadraoNHibernateMySQL<TipoTelefone>, IRepositorioTiposTelefone
    {
        public TipoTelefone Get(string tipo)
        {
            return GetSessaoAtual().Query<TipoTelefone>().SingleOrDefault(p => p.Nome == tipo);
        }
    }
}