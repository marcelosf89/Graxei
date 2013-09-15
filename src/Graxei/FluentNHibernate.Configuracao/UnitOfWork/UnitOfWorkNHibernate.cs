using System;
using FAST.Log;
using Graxei.FluentNHibernate.Configuracao;
using NHibernate;
using NHibernate.Context;

namespace Graxei.FluentNHibernate.UnitOfWork
{
   public  class UnitOfWorkNHibernate: FAST.Layers.UnitOfWork.Contrato.IUnitOfWork
    {

       /// <summary>
        /// Criar somente uma vez a fábrica de sessões do nhibernate
        /// </summary>
       static UnitOfWorkNHibernate()
       {

           _sessionFactory = CreateSessionFactory();

           //Se o modo corrente for CALL, então coloque uma sessão no contexto.
           //Isto significa que cada chamada terá uma unica sessão do nhibernate.
           BindSession();

           //Se o modo corrente for WEB, então o binding de sessão será feito
           //No inicio da requisão através de um HTTPModule
       }
        
        //Desaloca a sessão do nhibernate no contexto da aplicação
        public static void UnBindSession()
        {
            ISession session = CurrentSessionContext.Unbind(_sessionFactory);
            if (session == null)
            {
                return;
            }

            session.Clear();
            if (session.IsOpen)
            {
                session.Close();
            }
            session.Dispose();
            session = null;
        }

        //Aloca uma sessão do nhibernate no contexto da aplicação
        public static void BindSession()
        {
            if (!CurrentSessionContext.HasBind(_sessionFactory))
            {
                CurrentSessionContext.Bind(_sessionFactory.OpenSession());
            }
        }
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

        public static ISessionFactory SessionFactory
        {
            get
            {
                return _sessionFactory;
            }
        }

        //Obtem a sessão corrente do nhibernate
        public static ISession GetCurrentSession()
        {
            ISession session;

            //captura a sessão corrente do contexto
            session = _sessionFactory.GetCurrentSession();

            //se a sessão estiver fechada, então a mesma deve ser aberta
            if (!session.IsOpen)
                session = _sessionFactory.OpenSession();

            //se a sessão estiver desconectada, então a mesma deve ser reconectada
            if (!session.IsConnected)
                session.Reconnect();

            return session;
        }

        //Criar uma fabrica de sessão do nhibernate
        private static ISessionFactory CreateSessionFactory()
        {

            return NHibernateSessionFactory.Instance.SessionFactory();

        }

        /// <summary>
        /// Define a fabrica de sessões do nhibernate
        /// </summary>
        private static readonly ISessionFactory _sessionFactory;
    }
}
