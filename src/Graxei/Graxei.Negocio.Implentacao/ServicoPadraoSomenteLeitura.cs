using System.Collections.Generic;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Utilidades.Excecoes;
using Graxei.Modelo.Generico;

namespace Graxei.Negocio.Implementacao
{
    public abstract class ServicoPadraoSomenteLeitura<T> : IServicoEntidades<T> where T : Entidade
    {

        #region Implementação de IServicoEntidades<T>

        public void Validar(T t)
        {
        }

        public T GetPorId(long id)
        {
            if (RepositorioEntidades == null)
            {
                throw new OperacaoEntidadeException("RepositorioEntidades é nulo");
            }
            return RepositorioEntidades.GetPorId(id);
        }

        public IList<T> Todos()
        {
            if (RepositorioEntidades == null)
            {
                throw new OperacaoEntidadeException("RepositorioEntidades é nulo. Entidade: {0}");
            }
            return RepositorioEntidades.Todos();
        }

        public IRepositorioEntidades<T> RepositorioEntidades { get; protected set; }

        #endregion
    }
}
