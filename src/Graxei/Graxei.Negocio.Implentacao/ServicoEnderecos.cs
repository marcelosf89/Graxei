using System;
using System.Collections.Generic;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Utilidades.Entidades;
using Graxei.Transversais.Utilidades.Excecoes;
using Graxei.Transversais.Utilidades.NHibernate;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoEnderecos : ServicoPadraoEntidades<Endereco>, IServicoEnderecos
    {

        public ServicoEnderecos(IRepositorioEnderecos repoEnderecos,IServicoLogradouros servLogradouros,  IServicoBairros servBairros, IServicoCidades servCidades, IServicoEstados servEstados)
        {
            _repositorioEntidades = repoEnderecos;
            _servLogradouros = servLogradouros;
            _servBairros = servBairros;
            _servCidades = servCidades;
            _servEstados = servEstados;
        }

        #region Implementation of IServicoEnderecos

        public IList<Endereco> Todos(Loja loja)
        {
            return Repositorio.Todos(loja);
        }

        public IList<Endereco> Todos(long idLoja)
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

        public IList<Cidade> GetCidades(long idEstado)
        {
            return _servCidades.GetPorEstado(idEstado);
        }

        public IList<Bairro> GetBairros(Cidade cidade)
        {
            return _servBairros.Get(cidade);
        }

        public IList<Bairro> GetBairros(long idCidade)
        {
            return _servBairros.GetPorCidade(idCidade);
        }

        public IList<Bairro> GetBairros(string nomeCidade, long idEstado)
        {
            return _servBairros.GetPorCidade(nomeCidade, idEstado);
        }

        public IList<Logradouro> GetLogradouros(Bairro bairro)
        {
            return _servLogradouros.Get(bairro);
        }

        public IList<Logradouro> GetLogradouros(long idBairro)
        {
            return _servLogradouros.GetPorBairro(idBairro);
        }

        public IList<Logradouro> GetLogradouros(string nomeBairro, string nomeCidade)
        {
            return _servLogradouros.GetPorBairro(nomeBairro, nomeCidade);
        }

        public Estado GetEstado(long idEstado)
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

        public Cidade GetCidade(long idCidade)
        {
            return _servCidades.GetPorId(idCidade);
        }

        public Cidade GetCidade(string nome, long idEstado)
        {
            return _servCidades.Get(nome, idEstado);
        }

        public Cidade GetCidade(string nome, Estado estado)
        {
            return _servCidades.Get(nome, estado);
        }

        public Bairro GetBairro(long idBairro)
        {
            return _servBairros.GetPorId(idBairro);
        }

        public Bairro GetBairro(string nomeBairro, string nomeCidade, long idEstado)
        {
            return _servBairros.Get(nomeBairro, nomeCidade, idEstado);
        }

        public Bairro GetBairro(string nomeBairro, Cidade cidade)
        {
            return _servBairros.Get(nomeBairro, cidade);
        }

        public Bairro GetBairro(string nomeBairro, long idCidade)
        {
            return _servBairros.Get(nomeBairro, idCidade);
        }

        public Logradouro GetLogradouro(long idLogradouro)
        {
            return _servLogradouros.GetPorId(idLogradouro);
        }

        public Logradouro GetLogradouro(string nomeLogradouro, string nomeBairro, long idCidade)
        {
            return _servLogradouros.Get(nomeLogradouro, nomeBairro, idCidade);
        }

        public Logradouro GetLogradouro(string nomeLogradouro, Bairro bairro)
        {
            return _servLogradouros.Get(nomeLogradouro, bairro);
        }

        public Logradouro GetLogradouro(string nomeLogradouro, long idBairro)
        {
            return _servLogradouros.Get(nomeLogradouro, idBairro);
        }


        #endregion

        #region Atributos Privados
        private readonly IServicoEstados _servEstados;
        private readonly IServicoCidades _servCidades;
        private readonly IServicoBairros _servBairros;
        private readonly IServicoLogradouros _servLogradouros;
        #endregion

        #region Propriedades Privadas
        private IRepositorioEnderecos Repositorio { get { return (IRepositorioEnderecos) _repositorioEntidades; } }
        #endregion

        #region Métodos Sobrescritos
        public void PreSalvar(Endereco endereco)
        {
            Estado estado = null;
            Cidade cidade = null;
            Bairro bairro = null;
            if (endereco.Loja == null)
            {
                throw new EntidadeInvalidaException(Erros.EnderecoLojaNulo);
            }
            if (endereco.Bairro == null)
            {
                throw new EntidadeInvalidaException(Erros.BairroNulo);
            }
            bairro = endereco.Bairro;
            if (endereco.Bairro.Cidade == null)
            {
                throw new EntidadeInvalidaException(Erros.CidadeNulo);
            }
            cidade = endereco.Bairro.Cidade;
            if (endereco.Bairro.Cidade.Estado == null)
            {
                throw new EntidadeInvalidaException(Erros.EstadoNulo);
            }
            estado = endereco.Bairro.Cidade.Estado;
            if (string.IsNullOrEmpty(endereco.Logradouro))
            {
                throw new EntidadeInvalidaException(Erros.LogradouroNulo);
            }
            if (string.IsNullOrEmpty(endereco.Numero))
            {
                throw new EntidadeInvalidaException(Erros.EnderecoNumeroNulo);
            }
            /* TODO: ver como vai ficar a situação de atualização do estado */
            if (!UtilidadeEntidades.IsTransiente(cidade))
            {
                Cidade c = _servCidades.GetPorId(cidade.Id);
                if (!c.Equals(cidade))
                {
                    cidade = c;
                }
            } else
            {
                Cidade c = _servCidades.Get(cidade.Nome, estado);
                if (c != null)
                {
                    cidade = c;
                }
            }
            _servCidades.Salvar(cidade);
            if (!UtilidadeEntidades.IsTransiente(bairro))
            {
                Bairro b = _servBairros.GetPorId(bairro.Id);
                if (!b.Equals(bairro))
                {
                    bairro = b;
                    bairro.Cidade = cidade;
                }
            }else
            {
                Bairro b = _servBairros.Get(bairro.Nome, cidade.Nome, estado);
                if (b != null)
                {
                    bairro = b;
                }
            }
            _servBairros.Salvar(bairro);
            endereco.Bairro = bairro;
        }

        #endregion

        public enum AtributosOrdem { Sigla, Nome }
    }
}