using System.Collections.Generic;
using System.Linq;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using NHibernate.Linq;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public class EnderecosNHibernateMySQL : PadraoNHibernateMySQL<Endereco>, IRepositorioEnderecos
    {
  
        #region Construtor
        public EnderecosNHibernateMySQL(IRepositorioEstados repoEstados, IRepositorioCidades repoCidades, IRepositorioBairros repoBairros)
        {
            _repoEstados = repoEstados;
            _repoCidades = repoCidades;
            _repoBairros = repoBairros;
        }
        #endregion

        #region Implementation of IRepositorioEnderecos
        public IList<Endereco> Todos(Loja loja)
        {
            return SessaoAtual.Query<Endereco>()
                              .Where(p => p.Loja.Equals(loja))
                              .ToList();
        }
 
        public IList<Endereco> Todos(long idLoja)
        {
            return SessaoAtual.Query<Endereco>()
                              .Where(p => p.Loja != null && p.Loja.Id == idLoja)
                              .ToList();
        }

        public bool Existe(Endereco endereco)
        {
            return SessaoAtual.Query<Endereco>().Count(
                p => p.Loja.Nome.Trim().ToLower() == endereco.Loja.Nome.Trim().ToLower()
                     && p.Logradouro.Trim().ToLower() == endereco.Logradouro.Trim().ToLower()
                     && p.Numero.Trim().ToLower() == endereco.Numero.Trim().ToLower()
                     && p.Bairro.Nome.Trim().ToLower() == endereco.Bairro.Nome.Trim().ToLower()
                     && p.Bairro.Cidade.Nome.Trim().ToLower() == endereco.Bairro.Cidade.Nome.Trim().ToLower()
                     && p.Bairro.Cidade.Estado.Sigla.Trim().ToLower() == endereco.Bairro.Cidade.Estado.Sigla.Trim().ToLower()) > 0;
        }
        #endregion

        #region Atributos Privados
        private readonly IRepositorioEstados _repoEstados;
        private readonly IRepositorioCidades _repoCidades;
        private readonly IRepositorioBairros _repoBairros;
        #endregion
    }
}