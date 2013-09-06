using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FAST.Log;
using Graxei.FluentNHibernate.Configuracao;

namespace EDMManager.UnitOfWork.Implementation
{
   public  class UnitOfWorkNHibernate: FAST.Layers.UnitOfWork.Contrato.IUnitOfWork
    {
        /// <summary>
        /// Questiona se há uma transação aberta
        /// </summary>
        /// <returns>True ou False</returns>
        private bool HasOpenTransaction()
        {
            return NHibernateSessionPerRequest.GetCurrentSession().Transaction != null &&
                NHibernateSessionPerRequest.GetCurrentSession().Transaction.IsActive &&
                !NHibernateSessionPerRequest.GetCurrentSession().Transaction.WasCommitted &&
                !NHibernateSessionPerRequest.GetCurrentSession().Transaction.WasRolledBack;
        }

        /// <summary>
        /// Abre uma transação se já não houver uma já existente
        /// </summary>
        /// <exception cref="Exception">Erro ao inicializar uma transação</exception>
        public void BeginTransaction()
        {
            try
            {

                if (!HasOpenTransaction())
                {
                    NHibernateSessionPerRequest.GetCurrentSession().BeginTransaction();
                }
            }
            catch (Exception exception)
            {
                ExceptionLogger.Instance.LogException(Level.Error, "Begin Exception", exception);
                throw exception;
            }
        }

        /// <summary>
        /// Executa Commit em uma transação existente, executa RollBack caso haja uma exceção
        /// </summary>
        /// <exception cref="Exception">Erro ao comitar uma transação</exception>
        public void CommitTransaction()
        {
            try
            {

                if (HasOpenTransaction())
                {
                    NHibernateSessionPerRequest.GetCurrentSession().Transaction.Commit();
                    NHibernateSessionPerRequest.GetCurrentSession().Flush();
                }
            }
            catch (Exception exception)
            {
                ExceptionLogger.Instance.LogException(Level.Error, "Commit Exception", exception);
                RollbackTransaction();
                throw exception;
            }
        }

        /// <summary>
        /// Executa o rollback de uma transação
        /// </summary>
        public void RollbackTransaction()
        {
            if (HasOpenTransaction())
            {
                NHibernateSessionPerRequest.GetCurrentSession().Transaction.Rollback();
            }
        }
    }
}
