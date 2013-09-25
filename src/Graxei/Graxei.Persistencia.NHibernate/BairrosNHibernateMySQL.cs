using System.Collections.Generic;
using System.Linq;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Utilidades.NHibernate;
using NHibernate.Linq;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public class BairrosNHibernateMySQL : PadraoNHibernateMySQL<Bairro>, IRepositorioBairros
    {
        #region Implementação de IRepositorioBairros

        public IList<Bairro> Get(Cidade cidade)
        {
            return SessaoAtual.Query<Bairro>()
                              .Where(p => p.Cidade != null && p.Cidade.Equals(cidade))
                              .ToList();
        }

        public IList<Bairro> Get(int idCidade)
        {
            return SessaoAtual.Query<Bairro>()
                              .Where(p => p.Cidade != null && p.Cidade.Id == idCidade)
                              .ToList();
        }

        public Bairro Get(string nomeBairro, string nomeCidade, int idEstado)
        {
            return SessaoAtual.Query<Bairro>()
                .SingleOrDefault(p => 
                                      Queries.CompararStrings(p.Nome, nomeBairro)
                                   && Queries.CompararStrings(p.Cidade.Nome, nomeBairro)
                                   && p.Cidade.Estado.Id == idEstado);
        }

        public Bairro Get(string nomeBairro, int idCidade)
        {
            return SessaoAtual.Query<Bairro>()
                .SingleOrDefault(p =>
                                      Queries.CompararStrings(p.Nome, nomeBairro)
                                   && p.Cidade.Id == idCidade);
        }

        public Bairro Get(string nomeBairro, string nomeCidade, Estado estado)
        {
            return SessaoAtual.Query<Bairro>()
                .SingleOrDefault(p =>
                                      p.Nome.Trim().ToLower() == nomeBairro.Trim().ToLower()
                                   && p.Cidade.Nome.Trim().ToLower() == nomeCidade.Trim().ToLower()
                                   && p.Cidade.Estado.Id == estado.Id);
        }

        public Bairro Get(string nomeBairro, Cidade cidade)
        {
            /* TODO: resolver os CompararString */
            return SessaoAtual.Query<Bairro>()
                .SingleOrDefault(p =>
                                      //Queries.CompararStrings(p.Nome, nomeBairro)
                                      p.Nome.Trim().ToLower() == nomeBairro.Trim().ToLower()
                                   && p.Cidade.Id == cidade.Id);
        }

        public IList<Bairro> GetPorCidade(string nomeCidade, int idEstado)
        {
            return SessaoAtual.Query<Bairro>()
                .Where(p =>
                       //Queries.CompararStrings(p.Nome, nomeBairro)
                       p.Cidade.Nome.Trim().ToLower() == nomeCidade.Trim().ToLower()
                       && p.Cidade.Estado.Id == idEstado)
                .ToList<Bairro>();
        }

        #endregion
    }
}