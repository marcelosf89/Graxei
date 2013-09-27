using System.Collections.Generic;
using FAST.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Utilidades.Excecoes;
using Graxei.Transversais.Utilidades.NHibernate;

namespace Graxei.Negocio.Implementacao
{
    public abstract class ServicoPadraoEntidades<T> : IServicoEntidades<T> where T : Entidade
    {

        #region Implementações Padrão

        public void PreSalvar(T t){}

        public void PreAtualizar(T t){}

        public void PreExcluir(T t){}

        #endregion

        #region Implementação de IServicoEntidades<T>

        public void Salvar(T t)
        {
            if (_repositorioEntidades == null)
            {
                throw new OperacaoEntidadeException(string.Format("RepositorioEntidades é nulo. Entidade: {0}", t));
            }
            if (UtilidadeEntidades.IsTransiente(t))
            {
                PreSalvar(t);
            } else
            {
                PreAtualizar(t);
            }
            _repositorioEntidades.Salvar(t);    
        }

        public void Excluir(T t)
        {
            PreExcluir(t);
            if (_repositorioEntidades == null)
            {
                throw new OperacaoEntidadeException(string.Format("RepositorioEntidades é nulo. Entidade: {0}", t));
            }
            _repositorioEntidades.Excluir(t);    
        }

        public T GetPorId(long id)
        {
            if (_repositorioEntidades == null)
            {
                throw new OperacaoEntidadeException("RepositorioEntidades é nulo");
            }
            return _repositorioEntidades.GetPorId(id);
        }

        public IList<T> Todos()
        {
            if (_repositorioEntidades == null)
            {
                throw new OperacaoEntidadeException("RepositorioEntidades é nulo. Entidade: {0}");
            }
            return _repositorioEntidades.Todos();
        }

        public IRepositorioEntidades<T> RepositorioEntidades { get { return _repositorioEntidades;  } }

        #endregion

        protected IRepositorioEntidades<T> _repositorioEntidades;
    }
}
