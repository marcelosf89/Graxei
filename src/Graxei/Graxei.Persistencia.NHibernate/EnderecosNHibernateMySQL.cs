using System.Collections.Generic;
using System.Linq;
using Graxei.Modelo;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Utilidades.Excecoes;
using Graxei.Transversais.Utilidades.NHibernate;
using NHibernate.Linq;

namespace Graxei.Persistencia.Implementacao.NHibernate
{
    public class EnderecosNHibernateMySQL : PadraoNHibernateMySQL<Endereco>, IRepositorioEnderecos
    {
  
        /*#region Construtor
        public EnderecosNHibernateMySQL(IRepositorioEstados repoEstados, IRepositorioCidades repoCidades, IRepositorioBairros repoBairros)
        {
            _repoEstados = repoEstados;
            _repoCidades = repoCidades;
            _repoBairros = repoBairros;
        }
        #endregion*/

        #region Implementation of IRepositorioEnderecos
       /* public IList<Estado> GetEstados()
        {
            return _repoEstados.Todos();
        }

        public IList<Cidade> GetCidades(Estado estado)
        {
            return _repoCidades.Get(estado);
        }

        public IList<Cidade> GetCidades(int idEstado)
        {
            return _repoCidades.GetPorEstado(idEstado);
        }

        public IList<Bairro> GetBairros(Cidade cidade)
        {
            return _repoBairros.Get(cidade);
        }

        public IList<Bairro> GetBairros(int idCidade)
        {
            return _repoBairros.Get(idCidade);
        }
        */
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

        /* public Estado GetEstado(int idEstado)
        {
            return _repoEstados.GetPorId(idEstado);
        }

        public Estado GetEstadoPorSigla(string sigla)
        {
            return _repoEstados.GetPorSigla(sigla);
        }

        public Estado GetEstadoPorNome(string nome)
        {
            return _repoEstados.GetPorNome(nome);
        }

        public Cidade GetCidade(int idCidade)
        {
            return _repoCidades.GetPorId(idCidade);
        }

        public Cidade GetCidade(string nome)
        {
            return _repoCidades.Get(nome);
        }

        public Bairro GetBairro(int idBairro)
        {
            return _repoBairros.GetPorId(idBairro);
        }

        public Bairro GetBairro(string nome)
        {
            return _repoBairros.Get(nome);
        }*/
        #endregion

        #region Atributos Privados
        private readonly IRepositorioEstados _repoEstados;
        private readonly IRepositorioCidades _repoCidades;
        private readonly IRepositorioBairros _repoBairros;
        #endregion
    }
}