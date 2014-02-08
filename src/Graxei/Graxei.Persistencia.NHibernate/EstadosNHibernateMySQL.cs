using System.Collections.Generic;
using System.Linq;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Utilidades.Entidades;
using Graxei.Transversais.Utilidades.NHibernate;
using NHibernate.Linq;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public class EstadosNHibernateMySQL : PadraoNHibernateMySQL<Estado>, IRepositorioEstados
    {
        #region Implementação de IRepositorioEstados

        public Estado GetPorSigla(string sigla)
        {
            return SessaoAtual.Query<Estado>()
                              .SingleOrDefault(e => e.Sigla.Trim().ToLower() == sigla.Trim().ToLower());
             //Queries.CompararStrings(e.Sigla, sigla)
        }

        public Estado GetPorNome(string nome)
        {
            return SessaoAtual.Query<Estado>()
                              .SingleOrDefault(e => Queries.CompararStrings(e.Nome, nome));
        }

        public IList<Estado> GetPorSiglaOuNome(string sigla, string nome)
        {
            return SessaoAtual.Query<Estado>()
                              .Where(e => e.Sigla.Trim().ToLower() == sigla.Trim().ToLower() ||
                                          e.Nome.Trim().ToLower() == nome.Trim().ToLower()).ToList<Estado>();
        }

        public IList<Estado> Todos(EstadoOrdem ordem)
        {
            string hql = string.Format(ConsultasHql.EstadosOrderBy, ordem);
            return SessaoAtual.CreateQuery(hql).List<Estado>();
        }

        #endregion
    }
}