using System.Collections.Generic;
using FAST.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Utilidades.Excecoes;
using Graxei.Transversais.Utilidades.NHibernate;

namespace Graxei.Negocio.Implementacao
{
    public abstract class ServicoPadraoEntidades<T> : ServicoPadraoSomenteLeitura<T>, IEntidadesIrrestrito<T> where T : Entidade
    {

        #region Implementações Padrão
        #endregion

        #region Implementação de IServicoEntidades<T>

        public void Salvar(T t)
        {
            if (_repositorioEntidades == null)
            {
                throw new OperacaoEntidadeException(string.Format("RepositorioEntidades é nulo. Entidade: {0}", t.GetType()));
            }
            _repositorioEntidades.Salvar(t);    
        }

        public void Excluir(T t)
        {
            if (_repositorioEntidades == null)
            {
                throw new OperacaoEntidadeException(string.Format("RepositorioEntidades é nulo. Entidade: {0}", t));
            }
            _repositorioEntidades.Excluir(t);    
        }

        public new IRepositorioIrrestrito<T> RepositorioEntidades { get { return _repositorioEntidades;  } }

        #endregion

        protected new IRepositorioIrrestrito<T> _repositorioEntidades;
    }
}
