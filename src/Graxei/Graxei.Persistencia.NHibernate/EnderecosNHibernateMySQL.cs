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
 
        public IList<Endereco> Todos(int idLoja)
        {
            return SessaoAtual.Query<Endereco>()
                              .Where(p => p.Loja != null && p.Loja.Id == idLoja)
                              .ToList();
        }

        public new void Salvar(Endereco endereco)
        {
            Bairro bairro = endereco.Bairro;
            if (bairro == null)
            {
                throw new EntidadeInvalidaException(Erros.BairroNulo);
            }
            Cidade cidade = bairro.Cidade;
            if (cidade == null)
            {
                throw new EntidadeInvalidaException(Erros.CidadeNulo);
            }
            Estado estado = cidade.Estado;
            if (estado == null)
            {
                throw new EntidadeInvalidaException(Erros.EstadoNulo);
            }
            if (!UtilidadeEntidades.IsTransiente(estado))
            {
                Estado repEstado = _repoEstados.GetPorId(estado.Id);
                if (!repEstado.Equals(estado))
                {
                    estado.Nome = repEstado.Nome;
                }
            }
            if (!UtilidadeEntidades.IsTransiente(cidade))
            {
                Cidade repCidade = _repoCidades.GetPorId(cidade.Id);
                if (!repCidade.Equals(cidade))
                {
                    cidade.Nome = repCidade.Nome;
                }
            }
            if (!UtilidadeEntidades.IsTransiente(bairro))
            {
                Bairro repBairro = _repoBairros.GetPorId(bairro.Id);
                if (!repBairro.Equals(bairro))
                {
                    bairro.Nome = repBairro.Nome;
                }
            }
            base.Salvar(endereco);
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