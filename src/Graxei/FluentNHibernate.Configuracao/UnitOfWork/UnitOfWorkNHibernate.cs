using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Web;
using FAST.Log;
using Graxei.FluentNHibernate.Configuracao;
using NHibernate;
using NHibernate.Context;
using ISession = NHibernate.ISession;

namespace Graxei.FluentNHibernate.UnitOfWork
{
    public class UnitOfWorkNHibernate : IUnitOfWorkNHibernate, IHttpModule, IDispatchMessageInspector, IServiceBehavior
    {

        #region Binders
        //Desaloca a sessão do NHibernate no contexto da aplicação
        public void UnBindSession()
        {
            ISession session = CurrentSessionContext.Unbind(_sessionFactory.GetSessionFactory());
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

        //Aloca uma sessão do NHibernate no contexto da aplicação
        public void BindSession()
        {
            if (!CurrentSessionContext.HasBind(_sessionFactory.GetSessionFactory()))
            {
                CurrentSessionContext.Bind(_sessionFactory.OpenSession());
            }
        }
        #endregion

        #region Singleton

        private UnitOfWorkNHibernate()
        {
            _sessionFactory = NHibernateWebSessionFactory.GetInstancia();
        }

        private UnitOfWorkNHibernate(ISessionFactory sessionFactory)
        {
            _sessionFactory = NHibernateWebSessionFactory.GetInstancia(sessionFactory);
        }
        public static UnitOfWorkNHibernate GetInstancia()
        {
            return _instancia ?? (_instancia = new UnitOfWorkNHibernate());
        }

        public static UnitOfWorkNHibernate GetInstancia(ISessionFactory sessionFactory)
        {
            _instancia = new UnitOfWorkNHibernate(sessionFactory);
            return _instancia;
        }

        private static UnitOfWorkNHibernate _instancia;

        #endregion

        #region Métodos de transação do NHibernate
        /// <summary>
        /// Abre uma transação se já não houver uma já existente
        /// </summary>
        /// <exception cref="Exception">Erro ao iniciar uma transação</exception>
        public void IniciarTransacao()
        {
            try
            {
                if (!TemTransacaoAberta())
                {
                    SessaoAtual.BeginTransaction();
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
        public void ConfirmarTransacao()
        {
            try
            {
                ISession session = SessaoAtual;
                if (TemTransacaoAberta())
                {
                    session.Transaction.Commit();
                    session.Flush();
                }
                else
                {
                    throw new Exception("Não há transação aberta");
                }
            }
            catch (Exception exception)
            {
                ExceptionLogger.Instance.LogException(Level.Error, "Commit Exception", exception);
                DesfazerTransacao();
                throw exception;
            }
        }

        /// <summary>
        /// Executa o rollback de uma transação
        /// </summary>
        public void DesfazerTransacao()
        {
            if (TemTransacaoAberta())
            {
                SessaoAtual.Transaction.Rollback();
            }
        }

        /// <summary>
        /// Questiona se há uma transação aberta
        /// </summary>
        /// <returns>True ou False</returns>
        private bool TemTransacaoAberta()
        {
            ISession session = SessaoAtual;
            return session.Transaction != null &&
                   (session.Transaction.IsActive &&
                    !(session.Transaction.WasCommitted || session.Transaction.WasRolledBack));
        }

        /// <summary>
        /// Obtém a sessão atual do NHibernate
        /// </summary>
        public ISession SessaoAtual
        {
            get
            {
                ISession session;

                session = _sessionFactory.GetSessionFactory().GetCurrentSession();

                if (!session.IsOpen)
                    session = _sessionFactory.GetSessionFactory().OpenSession();

                //se a sessão estiver desconectada, então a mesma deve ser reconectada
                if (!session.IsConnected)
                    session.Reconnect();

                return session;
            }
        }

        #endregion

        #region Implementação de IDispatchMessageInspector
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            BeginRequest(null, null);
            return null;

        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementação de IServiceBehavior

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
            throw new NotImplementedException();
        }

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
        #endregion

        #region Implementação de IHttpModule
        public void Init(HttpApplication context)
        {
            context.BeginRequest += BeginRequest;
            context.EndRequest += EndRequest;
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

        #endregion

        #region Atributos Privados
        private INHibernateFactory _sessionFactory;
        #endregion

    }
}
