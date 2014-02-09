using System;
using System.Collections.Generic;
using System.Linq;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Utilidades.Entidades;
using Graxei.Transversais.Utilidades.Excecoes;
using Graxei.Transversais.Utilidades.NHibernate;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoEnderecos : ServicoPadraoSomenteLeitura<Endereco>, IServicoEnderecos
    {

        public ServicoEnderecos(IRepositorioEnderecos repoEnderecos,IServicoLogradouros servLogradouros,  IServicoBairros servBairros, IServicoCidades servCidades, IServicoEstados servEstados)
        {
            RepositorioEntidades = repoEnderecos;
            _servLogradouros = servLogradouros;
            _servBairros = servBairros;
            _servCidades = servCidades;
            _servEstados = servEstados;
        }

        #region Implementação de IServicoEnderecos

        public IList<Endereco> Todos(Loja loja)
        {
            return RepositorioEnderecos.Todos(loja);
        }

        public IList<Endereco> Todos(long idLoja)
        {
            return RepositorioEnderecos.Todos(idLoja);
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

        public IList<Logradouro> GetLogradouros(string nomeBairro, string nomeCidade, long idEstado)
        {
            return _servLogradouros.GetPorBairro(nomeBairro, nomeCidade, idEstado);
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

        public bool Existe(Endereco endereco)
        {
            if (endereco == null || endereco.Loja == null || !endereco.Validar())
            {
                return false;
            }
            return true;
        }

        #endregion

        #region Métodos Sobrescritos
        public void PreSalvar(Endereco endereco)
        {
            if (!UtilidadeEntidades.IsTransiente(endereco))
            {
                throw new InvalidOperationException(Erros.EnderecoSalvarTransiente);
            }
            ValidarEndereco(endereco);
            VerificarElementosEndereco(endereco);
        }

        public void PreAtualizar(Endereco endereco)
        {
            if (UtilidadeEntidades.IsTransiente(endereco))
            {
                throw new InvalidOperationException(Erros.EnderecoAtualizarNaoTransiente);
            }
            ValidarEndereco(endereco);
            VerificarElementosEndereco(endereco);
            if (Repositorio.ExisteNaLoja(endereco))
            {
                throw new ObjetoJaExisteException(Erros.EnderecoJaExiste);
            }
        }

        public IList<Endereco> EnderecosRepetidos(IList<Endereco> enderecos)
        {
            if (enderecos == null)
            {
                return null;
            }
            IList<ContadorEnderecos> grupoRepetidos =
                (from e in enderecos
                 group e by e.ToString()
                     into g
                     select new ContadorEnderecos() { Endereco = g.Key, Contador = g.Count() }).Where(q => q.Contador > 1).ToList();
            List<Endereco> resultado = new List<Endereco>();
            foreach (ContadorEnderecos c in grupoRepetidos)
            {
                resultado.AddRange(enderecos.Where(p => p.ToString() == c.Endereco).ToList());
            }
            return resultado;
        }

        #endregion

        #region Implementação de IEntidadesExcluir<Endereco>

        //*TODO: implementar
        public void PreExcluir(Endereco t)
        {
        }

        public void Excluir(Endereco t)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Métodos Privados
        private void ValidarEndereco(Endereco endereco)
        {
            if (endereco.Loja == null)
            {
                throw new EntidadeInvalidaException(ErrosInternos.EnderecoLojaNulo);
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

        private void VerificarElementosEndereco(Endereco endereco)
        {
            /// TODO: Considerar criar uma só consulta de endereços
            Bairro bairro = endereco.Bairro;
            Estado estado = _servEstados.GetPorSigla(bairro.Cidade.Estado.Sigla);
            if (estado == null)
            {
                _servEstados.Salvar(bairro.Cidade.Estado);
            }else
            {
                bairro.Cidade.Estado = estado;
            }

            Cidade cidade = bairro.Cidade;
            try
            {
                _servCidades.Salvar(cidade);
            }
            catch (ObjetoJaExisteException)
            {
                cidade = _servCidades.Get(cidade.Nome, cidade.Estado.Id);
                bairro.Cidade = cidade;
            }

            /// TODO: Poderia checar se é transiente
            try
            {
                _servBairros.Salvar(bairro);
            }
            catch (ObjetoJaExisteException)
            {
                bairro = _servBairros.Get(bairro.Nome, cidade.Nome, cidade.Estado.Id);
            }
            endereco.Bairro = bairro;
        }

        #endregion

        #region Propriedades Privadas
        private IRepositorioEnderecos RepositorioEnderecos { get { return (IRepositorioEnderecos)RepositorioEntidades; } }
        private IRepositorioEnderecos Repositorio { get { return (IRepositorioEnderecos)RepositorioEntidades; } }
        #endregion

        #region Atributos Privados
        private readonly IServicoEstados _servEstados;
        private readonly IServicoCidades _servCidades;
        private readonly IServicoBairros _servBairros;
        private readonly IServicoLogradouros _servLogradouros;
        #endregion

        private class ContadorEnderecos
        {
            public string Endereco { get; set; }
            public int Contador { get; set; }
        }

        public enum AtributosOrdem { Sigla, Nome }


    }
}