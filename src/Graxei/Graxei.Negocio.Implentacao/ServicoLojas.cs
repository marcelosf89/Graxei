using System;
using System.Collections.Generic;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Utilidades.Excecoes;
using Graxei.Transversais.Utilidades.NHibernate;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoLojas : ServicoPadraoEntidades<Loja>, IServicoLojas
    {

        #region Construtor
        public ServicoLojas(IRepositorioLojas repositorioLojas)
        {
            _repositorioEntidades = repositorioLojas;
        }
        #endregion

        #region Métodos de sobrescrita
        private void PreSalvar(Loja loja)
        {
            if (string.IsNullOrEmpty(loja.Nome))
            {
                throw new ValidacaoEntidadeException(Validacoes.NomeLojaObrigatório);
            }
            Loja repetida = Get(loja.Nome);
            if (repetida != null)
            {
                throw new ObjetoJaExisteException(Erros.LojaJaExiste);
            }
        }

        private void PreAtualizar(Loja loja)
        {
            if (string.IsNullOrEmpty(loja.Nome))
            {
                throw new ValidacaoEntidadeException(Validacoes.NomeLojaObrigatório);
            }
            Loja repetida = Get(loja.Nome);
            if (repetida != null && repetida.Id != loja.Id)
            {
                throw new ObjetoJaExisteException(Erros.LojaJaExiste);
            }
        }
        #endregion

        #region Implementação de IServicoLojas

        public Loja Get(string nome)
        {
            return RepositorioLojas.Get(nome);
        }

        public void Salvar(Loja loja, Usuario usuario)
        {
            if (UtilidadeEntidades.IsTransiente(loja))
            {
                PreSalvar(loja);
            } else
            {
                PreAtualizar(loja);
            }
            RepositorioLojas.Salvar(loja, usuario);
        }

        public new void Salvar(Loja loja)
        {
            // Se é uma nova loja, deve ser associado pelo menos um usuário
            if (UtilidadeEntidades.IsTransiente(loja))
            {
                throw new InvalidOperationException(Erros.LojaSalvarInvalido);
            }
            base.Salvar(loja);
        }

        public void Salvar(Loja loja, IList<Usuario> usuarios)
        {
            if (UtilidadeEntidades.IsTransiente(loja))
            {
                PreSalvar(loja);
            }
            else
            {
                PreAtualizar(loja);
            }
            RepositorioLojas.Salvar(loja, usuarios);
        }

        public void Salvar(Loja loja, Usuario usuario, IList<Endereco> enderecos)
        {
            throw new NotImplementedException();
        }

        public void Salvar(Loja loja, IList<Usuario> usuarios, IList<Endereco> enderecos)
        {
            throw new NotImplementedException();
        }

        public IServicoEnderecos ServicoEnderecos { get { return _servicoEnderecos; } }
        #endregion

        #region Atributos Privados
        private IRepositorioLojas RepositorioLojas { get { return (IRepositorioLojas)_repositorioEntidades; } }
        #endregion

        #region Implementation of IExcluirEntidade<Loja>

        public new void Excluir(Loja loja)
        {
            /* TODO: implementar restrições de exclusão */
            RepositorioLojas.Excluir(loja);
        }

        #endregion

        #region Implementação de IServicoEntidades<Loja>

        public Loja GetPorId(long id)
        {
            return RepositorioLojas.GetPorId(id);
        }

        public IList<Loja> Todos()
        {
            throw new NotImplementedException();
        }

        public IRepositorioEntidades<Loja> RepositorioEntidades { get; private set; }

        #endregion

        #region Atributos Privados

        private IServicoEnderecos _servicoEnderecos;

        #endregion
    }
}
