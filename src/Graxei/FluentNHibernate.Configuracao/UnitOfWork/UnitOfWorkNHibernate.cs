using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Web;
using FAST.Layers.UnitOfWork.Contrato;
using FAST.Log;
using Graxei.FluentNHibernate.Configuracao;
using NHibernate;
using NHibernate.Context;
using ISession = NHibernate.ISession;

namespace Graxei.FluentNHibernate.UnitOfWork
{
    public class UnitOfWorkNHibernate : IHttpModule, IDispatchMessageInspector, IServiceBehavior
    {

        private static UnitOfWorkNHibernate _instance;

        public static UnitOfWorkNHibernate Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UnitOfWorkNHibernate();
                }
                return _instance;
            }
        }


        private UnitOfWorkNHibernate()
        {
            _sessionFactory = NHibernateSessionFactory.Instance.SessionFactory();
        }

        //Desaloca a sessão do nhibernate no contexto da aplicação
        private void UnBindSession()
        {
            //CommitTransaction();
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
        private void BindSession()
        {
            if (!CurrentSessionContext.HasBind(_sessionFactory))
            {
                CurrentSessionContext.Bind(_sessionFactory.OpenSession());
            }
           //BeginTransaction();
        }
        /// <summary>
        /// Questiona se há uma transação aberta
        /// </summary>
        /// <returns>True ou False</returns>
        private bool HasOpenTransaction()
        {
            ISession session = GetCurrentSession();
            return session.Transaction != null &&
                   (session.Transaction.IsActive &&
                    !(session.Transaction.WasCommitted || session.Transaction.WasRolledBack));
        }

        /// <summary>
        /// Abre uma transação se já não houver uma já existente
        /// </summary>
        /// <exception cref="Exception">Erro ao inicializar uma transação</exception>
        private void BeginTransaction()
        {
            try
            {
                if (!HasOpenTransaction())
                {
                    GetCurrentSession().BeginTransaction();
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
        private void CommitTransaction()
        {
            try
            {
                ISession session = GetCurrentSession();
                if (HasOpenTransaction())
                {
                    session.Transaction.Commit();
                    session.Flush();
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
                GetCurrentSession().Transaction.Rollback();
            }
        }

        public ISessionFactory SessionFactory
        {
            get
            {
                return _sessionFactory;
            }
        }

        //Obtem a sessão corrente do nhibernate
        public ISession GetCurrentSession()
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

        /// <summary>
        /// Define a fabrica de sessões do nhibernate
        /// </summary>
        private ISessionFactory _sessionFactory;

       #region Implementation of IDispatchMessageInspector
       public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
       {
           BeginRequest(null, null);
           return null;

       }

       public void BeforeSendReply(ref Message reply, object correlationState)
       {
           EndRequest(null, null);
       }

       #endregion

       #region Implementation of IServiceBehavior

       public void ApplyDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
       {
           foreach (ChannelDispatcher cd in serviceHostBase.ChannelDispatchers)
           {
               foreach (EndpointDispatcher ed in cd.Endpoints)
               {
                   ed.DispatchRuntime.MessageInspectors.Add(this);
               }
           }
       }

       public void Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
       {

       }

       public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint>                                               endpoints, BindingParameterCollection bindingParameters)
       {
       }
       #endregion

       #region Implementation of IHttpModule

       public void Init(HttpApplication context)
       {
           context.BeginRequest += BeginRequest;
           context.EndRequest += EndRequest;
           context.Error += Error;
       }


       public void Dispose()
       {
       }

       #endregion

       #region Extensões dos eventos da sessão
       private void BeginRequest(object sender, EventArgs e)
       {
           BindSession();
       }

       private void EndRequest(object sender, EventArgs e)
       {
           UnBindSession();
       }

       private void Error(object sender, EventArgs e)
       {
           ISession session = null;
           try
           {
               //RollbackTransaction();
           } finally
           {
               UnBindSession();    
           }
       }
       #endregion

    }
}
