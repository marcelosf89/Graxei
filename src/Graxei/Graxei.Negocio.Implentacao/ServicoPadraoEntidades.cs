using System;
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

        public abstract void PreSalvar(T t);

        public abstract void PreAtualizar(T t);

        public void PreExcluir(T t) {}

        public void Salvar(T t)
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
            RepositorioIrrestrito.Salvar(t);    
        }

        public void Excluir(T t)
        {
            if (RepositorioEntidades == null)
            {
                throw new OperacaoEntidadeException(string.Format("RepositorioEntidades é nulo. Entidade: {0}", t));
            }
            RepositorioIrrestrito.Excluir(t);    
        }

        #endregion

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
