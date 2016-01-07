using System;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Comum.Excecoes;
using Graxei.Transversais.Comum.NHibernate;
using Graxei.Modelo.Generico;

namespace Graxei.Negocio.Implementacao
{
    public abstract class ServicoPadraoEntidades<T> : ServicoPadraoSomenteLeitura<T>, IEntidadesIrrestrito<T> where T : Entidade
    {

        public abstract void PreSalvar(T t);

        public abstract void PreAtualizar(T t);

        public void PreExcluir(T t) {}

        public T Salvar(T t)
        {
            if (RepositorioEntidades == null)
            {
                throw new OperacaoEntidadeException(string.Format("RepositorioEntidades é nulo. Entidade: {0}", t.GetType()));
            }
            if (UtilidadeEntidades.IsTransiente(t))
            {
                PreSalvar(t);
            }else
            {
                PreAtualizar(t);
            }
            return RepositorioIrrestrito.Salvar(t);    
        }

        public void Excluir(T t)
        {
            if (RepositorioEntidades == null)
            {
                throw new OperacaoEntidadeException(string.Format("RepositorioEntidades é nulo. Entidade: {0}", t));
            }
            RepositorioIrrestrito.Excluir(t);    
        }

        private IRepositorioIrrestrito<T> RepositorioIrrestrito
        {
            get
            {
                if (!(RepositorioEntidades is IRepositorioIrrestrito<T>))
                {
                    throw new InvalidCastException(
                        "Repositório de entidades para serviço padrão deve ser do tipo 'irrestrito'");
                }
                return (IRepositorioIrrestrito<T>) RepositorioEntidades;
            }
        }
    }
}
