using System.Collections.Generic;
using System.Linq;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Utilidades.NHibernate;
using NHibernate.Linq;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public class LogradourosNHibernateMySQL : PadraoNHibernateMySQL<Logradouro>, IRepositorioLogradouros
    {
        #region Implementação de IRepositorioBairros

        public IList<Logradouro> Get(Bairro bairro)
        {
            return SessaoAtual.Query<Logradouro>()
                              .Where(p => p.Bairro != null && p.Bairro.Equals(bairro))
                              .ToList();
        }

        public IList<Logradouro> Get(long idBairro)
        {
            return SessaoAtual.Query<Logradouro>()
                              .Where(p => p.Bairro != null && p.Bairro.Id == idBairro)
                              .ToList();
        }

        public Logradouro Get(string nomeLogradouro, string nomeBairro, long idCidade)
        {
            return SessaoAtual.Query<Logradouro>()
                .SingleOrDefault(p => 
                                      Queries.CompararStrings(p.Nome, nomeLogradouro)
                                   && Queries.CompararStrings(p.Bairro.Nome, nomeBairro)
                                   && p.Bairro.Cidade.Id == idCidade);
        }

        public Logradouro Get(string nomeLogradouro, long idBairro)
        {
            return SessaoAtual.Query<Logradouro>()
                .SingleOrDefault(p =>
                                      Queries.CompararStrings(p.Nome, nomeLogradouro)
                                   && p.Bairro.Id == idBairro);
        }

        public Logradouro Get(string nomeLogradouro, string nomeBairro, Cidade cidade)
        {
            return SessaoAtual.Query<Logradouro>()
                .SingleOrDefault(p =>
                                      p.Nome.Trim().ToLower() == nomeLogradouro.Trim().ToLower()
                                   && p.Bairro.Nome.Trim().ToLower() == nomeBairro.Trim().ToLower()
                                   && p.Bairro.Cidade.Id == cidade.Id);
        }

        public Logradouro Get(string nomeLogradouro, Bairro bairro)
        {
            /* TODO: resolver os CompararString */
            return SessaoAtual.Query<Logradouro>()
                .SingleOrDefault(p =>
                                      p.Nome.Trim().ToLower() == nomeLogradouro.Trim().ToLower()
                                   && p.Bairro.Id == bairro.Id);
        }

        public IList<Logradouro> GetPorBairro(string nomeBairro, string nomeCidade)
        {
            return SessaoAtual.Query<Logradouro>()
                .Where(p =>
                       p.Bairro.Nome.Trim().ToLower() == nomeBairro.Trim().ToLower()
                       && p.Bairro.Cidade.Nome == nomeCidade)
                .ToList<Logradouro>();
        }

        #endregion
    }
}