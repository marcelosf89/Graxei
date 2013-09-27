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
        public new void PreSalvar(Loja loja)
        {
            if (string.IsNullOrEmpty(loja.Nome))
            {
                throw new ObjetoJaExisteException("Nome da loja deve ser preenchido");
            }
            Loja repetida = Get(loja.Nome);
            if (repetida != null)
            {
                throw new ValidacaoEntidadeException("Esta loja já existe");
            }
        }

        public new void PreAtualizar(Loja loja)
        {
            if (string.IsNullOrEmpty(loja.Nome))
            {
                throw new ObjetoJaExisteException("Nome da loja deve ser preenchido");
            }
            Loja repetida = Get(loja.Nome);
            if (repetida != null && repetida.Id != loja.Id)
            {
                throw new ValidacaoEntidadeException("Esta loja já existe");
            }
        }
        #endregion

        private IRepositorioLojas RepositorioLojas { get { return (IRepositorioLojas) _repositorioEntidades; } }

        #region Implementation of IServicoLojas

        public Loja Get(string nome)
        {
            return RepositorioLojas.Get(nome);
        }

        public void Salvar(Loja loja, Usuario usuario)
        {
            IList<Usuario> us = new List<Usuario>();
            us.Add(usuario);
            Salvar(loja, us);
        }

        public new void Salvar(Loja loja)
        {
            if (UtilidadeEntidades.IsTransiente(loja))
            {
                throw new InvalidOperationException(Erros.LojaSalvarInvalido);
            }
            base.Salvar(loja);
        }

        public void Salvar(Loja loja, IList<Usuario> usuarios)
        {
            foreach (Usuario u in usuarios)
            {
                LojaUsuario lu = new LojaUsuario();
                lu.Loja = loja;
                lu.Usuario = u;
            }
            Salvar(loja);
        }

        #endregion
    }
}
