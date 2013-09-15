using System;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Web;
using Graxei.FluentNHibernate.UnitOfWork;
using NHibernate;
using NHibernate.Cfg;

namespace Graxei.FluentNHibernate.Configuracao
{

    public class NHibernateSessionPerRequest : IHttpModule, IDispatchMessageInspector, IServiceBehavior
    {

        /// <summary>
        /// Recupera a configuração do NHibernate
        /// </summary>
        /// <returns>Configuração do NHibernate</returns>
        public static Configuration GetConfiguration()
        {
            return new Configuration().Configure();
        }

        static NHibernateSessionPerRequest()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += BeginRequest;
            context.EndRequest += EndRequest;
        }

        public static ISession GetCurrentSession()
        {
            return UnitOfWorkNHibernate.GetCurrentSession();
        }

        public void Dispose() { }

        private static void BeginRequest(object sender, EventArgs e)
        {
            UnitOfWorkNHibernate.BindSession();
        }

        private static void EndRequest(object sender, EventArgs e)
        {
            UnitOfWorkNHibernate.UnBindSession();
        }

        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel, System.ServiceModel.InstanceContext instanceContext)
        {
            BeginRequest(null, null);
            return null;
        }

        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            EndRequest(null, null);
        }


        public void AddBindingParameters(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {

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
    }

}
