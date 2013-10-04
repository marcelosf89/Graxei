using System.Collections.Generic;
using FAST.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Utilidades.Excecoes;

namespace Graxei.Negocio.Implementacao
{
    public abstract class ServicoPadraoSomenteLeitura<T> : IServicoEntidades<T> where T : Entidade
    {

        #region Implementação de IServicoEntidades<T>
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
