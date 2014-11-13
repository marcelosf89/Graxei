using System.Collections.Generic;
using System.Linq;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Utilidades.NHibernate;
using NHibernate.Linq;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public class CidadesNHibernatePostgre : PadraoNHibernatePostgre<Cidade>, IRepositorioCidades
    {
        #region Implementação de IRepositorioCidades

        public Cidade Get(string nome, long idEstado)
        {
            nome = nome.Trim().ToLower();
            return SessaoAtual.Query<Cidade>()
                .SingleOrDefault<Cidade>(p => p.Nome.Trim().ToLower() == nome && p.Estado.Id == idEstado);
        }

        public Cidade Get(string nome, Estado estado)
        {
            return SessaoAtual.Query<Cidade>()
                .SingleOrDefault<Cidade>(p => p.Nome.Trim().ToLower() == nome.Trim().ToLower() && p.Estado.Id == estado.Id);
                //Queries.CompararStrings(p.Nome, nome) 
            //&& p.Estado.Equals(estado)
        }

        public IList<Cidade> Get(Estado estado)
        {
            return SessaoAtual.Query<Cidade>()
                              .Where(p => p.Estado != null && p.Estado.Equals(estado))
                              .ToList();
        }

        public IList<Cidade> GetPorEstado(long idEstado)
        {
            return SessaoAtual.Query<Cidade>()
                              .Where(p => p.Estado != null && p.Estado.Id == idEstado)
                              .ToList();
        }

        public IList<Cidade> GetPorSiglaEstado(string sigla)
        {
            return SessaoAtual.Query<Cidade>()
                  .Where(p => p.Estado != null && Queries.CompararStrings(p.Estado.Sigla, sigla))
                  .ToList();
        }

        public IList<Cidade> GetPorNomeEstado(string nome)
        {
            return SessaoAtual.Query<Cidade>()
                  .Where(p => p.Estado != null && Queries.CompararStrings(p.Estado.Nome, nome))
                  .ToList();
        }

        #endregion
    }
}
