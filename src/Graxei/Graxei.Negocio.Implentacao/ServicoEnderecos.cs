using System.Collections.Generic;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Utilidades.Entidades;
using Graxei.Transversais.Utilidades.Excecoes;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoEnderecos : ServicoPadraoEntidades<Endereco>, IServicoEnderecos
    {

        public ServicoEnderecos(IRepositorioEnderecos repoEnderecos, IServicoBairros servBairros, IServicoCidades servCidades, IServicoEstados servEstados)
        {
            _repositorioEntidades = repoEnderecos;
            _servBairros = servBairros;
            _servCidades = servCidades;
            _servEstados = servEstados;
        }

        #region Implementation of IServicoEnderecos

        public IList<Endereco> Todos(Loja loja)
        {
            return Repositorio.Todos(loja);
        }

        public IList<Endereco> Todos(int idLoja)
        {
            return Repositorio.Todos(idLoja);
        }

        public IList<Estado> GetEstados(EstadoOrdem ordem)
        {
            return _servEstados.Todos(ordem);
        }

        /// <summary>
        /// Recupera todos os Estados
        /// </summary>
        /// <returns></returns>
        public IList<Estado> GetEstados()
        {
            return _servEstados.Todos();
        }

        public IList<Cidade> GetCidades(Estado estado)
        {
            return _servCidades.Get(estado);
        }

        public IList<Cidade> GetCidades(int idEstado)
        {
            return _servCidades.GetPorEstado(idEstado);
        }

        public IList<Bairro> GetBairros(Cidade cidade)
        {
            return _servBairros.Get(cidade);
        }

        public IList<Bairro> GetBairros(int idCidade)
        {
            return _servBairros.GetPorCidade(idCidade);
        }

        public IList<Bairro> GetBairros(string nomeCidade, int idEstado)
        {
            return _servBairros.GetPorCidade(nomeCidade, idEstado);
        }

        public Estado GetEstado(int idEstado)
        {
            return _servEstados.GetPorId(idEstado);
        }

        public Estado GetEstadoPorSigla(string sigla)
        {
            return _servEstados.GetPorSigla(sigla);
        }

        public Estado GetEstadoPorNome(string nome)
        {
            return _servEstados.GetPorNome(nome);
        }

        public Cidade GetCidade(int idCidade)
        {
            return _servCidades.GetPorId(idCidade);
        }

        public Cidade GetCidade(string nome, int idEstado)
        {
            return _servCidades.Get(nome, idEstado);
        }

        public Cidade GetCidade(string nome, Estado estado)
        {
            return _servCidades.Get(nome, estado);
        }

        public Bairro GetBairro(int idBairro)
        {
            return _servBairros.GetPorId(idBairro);
        }

        public Bairro GetBairro(string nomeBairro, string nomeCidade, int idEstado)
        {
            return _servBairros.Get(nomeBairro, nomeCidade, idEstado);
        }

        public Bairro GetBairro(string nomeBairro, Cidade cidade)
        {
            return _servBairros.Get(nomeBairro, cidade);
        }

        public Bairro GetBairro(string nomeBairro, int idCidade)
        {
            return _servBairros.Get(nomeBairro, idCidade);
        }

        #endregion

        #region Atributos Privados
        private readonly IServicoEstados _servEstados;
        private readonly IServicoCidades _servCidades;
        private readonly IServicoBairros _servBairros;
        #endregion

        #region Propriedades Privadas
        private IRepositorioEnderecos Repositorio { get { return (IRepositorioEnderecos) _repositorioEntidades; } }
        #endregion

        #region Métodos Sobrescritos
        public new void PreSalvar(Endereco endereco)
        {
            if (endereco.Bairro == null)
            {
                throw new EntidadeInvalidaException(Erros.BairroNulo);
            }
            if (endereco.Bairro.Cidade == null)
            {
                throw new EntidadeInvalidaException(Erros.CidadeNulo);
            }
            if (endereco.Bairro.Cidade.Estado == null)
            {
                throw new EntidadeInvalidaException(Erros.EstadoNulo);
            }
            if (string.IsNullOrEmpty(endereco.Logradouro))
            {
                throw new EntidadeInvalidaException(Erros.LogradouroNulo);
            }
            if (string.IsNullOrEmpty(endereco.Numero))
            {
                throw new EntidadeInvalidaException(Erros.EnderecoNumeroNulo);
            }
        }
        #endregion

        public enum AtributosOrdem { Sigla, Nome }
    }
}