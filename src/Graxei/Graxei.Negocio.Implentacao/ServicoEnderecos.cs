using System;
using System.Collections.Generic;
using System.Linq;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Negocio.Contrato.Comportamento;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Utilidades.Excecoes;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoEnderecos : GravacaoTemplateMethod<Endereco>, IServicoEnderecos
    {

        public ServicoEnderecos(IRepositorioEnderecos repositorioEnderecos, IServicoBairros servBairros, IServicoCidades servCidades)
        {
            _servBairros = servBairros;
            _servCidades = servCidades;
            _repositorioEnderecos = repositorioEnderecos;
        }

        #region Implementação de IServicoEnderecos

        public IList<Endereco> Todos(Loja loja)
        {
            return _repositorioEnderecos.Todos(loja);
        }

        public IList<Endereco> Todos(long idLoja)
        {
            return _repositorioEnderecos.Todos(idLoja);
        }

        #endregion

        #region Métodos Sobrescritos
        public override void PreSalvar(Endereco endereco)
        {
            ValidarEndereco(endereco);
            VerificarElementosEndereco(endereco);
            ChecarRepetidosAoSalvar(endereco);
        }

        public override void PreAtualizar(Endereco endereco)
        {
            ValidarEndereco(endereco);
            VerificarElementosEndereco(endereco);
            ChecarRepetidosAoAtualizar(endereco);
        }

        public void ChecarRepetidosAoSalvar(Endereco endereco)
        {
            IList<Endereco> enderecos = _repositorioEnderecos.Todos(endereco.Loja.Id);
            if (enderecos.Contains(endereco))
            {
                throw new RepetidoEmColecaoException(Erros.EnderecoJaExiste);
            }
        }

        public void ChecarRepetidosAoAtualizar(Endereco endereco)
        {
            IList<Endereco> enderecos = _repositorioEnderecos.Todos(endereco.Loja.Id);
            Endereco enderecoRepetido = enderecos.FirstOrDefault(p => p.Equals(endereco) && p.Id != endereco.Id);
            if (enderecoRepetido != null)
            {
                throw new RepetidoEmColecaoException(Erros.EnderecoJaExiste);
            }
        }

        public override Endereco Salvar(Endereco endereco)
        {
            PreGravar(endereco);
            return _repositorioEnderecos.Salvar(endereco);
        }

        public override Endereco GetPorId(long id)
        {
            return _repositorioEnderecos.GetPorId(id);
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
                     select new ContadorEnderecos { Endereco = g.Key, Contador = g.Count() }).Where(q => q.Contador > 1).ToList();
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
            if (endereco.Bairro == null)
            {
                throw new EntidadeInvalidaException(ErrosInternos.EnderecoBairroNulo);
            }
        }

        private void VerificarElementosEndereco(Endereco endereco)
        {
            Bairro bairro = endereco.Bairro;
            if (_servBairros.Get(bairro.Nome, bairro.Cidade.Nome, bairro.Cidade.Estado.Id) == null)
            {
                Cidade cidade = _servCidades.Get(bairro.Cidade.Nome, bairro.Cidade.Estado.Id);
                if (cidade == null)
                {
                    _servCidades.Salvar(bairro.Cidade);
                }
                _servBairros.Salvar(bairro);
            }
        }

        #endregion

        #region Atributos Privados
        private readonly IServicoCidades _servCidades;
        private readonly IServicoBairros _servBairros;
        private IRepositorioEnderecos _repositorioEnderecos;
        #endregion

        private class ContadorEnderecos
        {
            public string Endereco { get; set; }
            public int Contador { get; set; }
        }

        public enum AtributosOrdem { Sigla, Nome }


    }
}