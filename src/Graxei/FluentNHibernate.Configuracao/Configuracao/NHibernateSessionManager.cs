using System;
using FAST.Log;
using NHibernate;
using NHibernate.Cfg;
using System.Reflection;
using NHibernate.Context;
using Graxei.FluentNHibernate.Configuracao;

namespace Graxei.FluentNHibernate.Configuracao
{
    public static class NHibernateSessionManager
    {
        /// <summary>
        /// Define uma ANNOTATION para o enumerator ContextMode
        /// </summary>
        internal class StringValue : System.Attribute
        {
            private string _value;

            public StringValue(string value)
            {
                _value = value;
            }

            public string Value
            {
                get { return _value; }
            }

        }

        /// <summary>
        /// Define o enumerator que tem a lista de poss�veis contextos de aplica��o
        /// </summary>
        internal enum ContextMode
        {
            [StringValue("web")]
            WEB_CONTEXT = 1,
            [StringValue("call")]
            CALL_CONTEXT = 2,
        }

        /// <summary>
        /// Define um contexto corrente
        /// </summary>
        private static ContextMode currentContext;

        /// <summary>
        /// Define a fabrica de sess�es do nhibernate
        /// </summary>
        private static readonly ISessionFactory _sessionFactory;

        /// <summary>
        /// Obtem um valor string do enumerator
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string GetStringValue(Enum value)
        {
            string output = null;
            Type type = value.GetType();

            //Check first in our cached results...

            //Look for our 'StringValueAttribute' 

            //in the field's custom attributes

            FieldInfo fi = type.GetField(value.ToString());
            StringValue[] attrs =
               fi.GetCustomAttributes(typeof(StringValue),
                                       false) as StringValue[];
            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }

            return output;
        }

        /// <summary>
        /// Criar somente uma vez a f�brica de sess�es do nhibernate
        /// </summary>
        static NHibernateSessionManager()
        {

            _sessionFactory = CreateSessionFactory();

            //Se o modo corrente for CALL, ent�o coloque uma sess�o no contexto.
            //Isto significa que cada chamada ter� uma unica sess�o do nhibernate.
                BindSession();
            
            //Se o modo corrente for WEB, ent�o o binding de sess�o ser� feito
            //No inicio da requis�o atrav�s de um HTTPModule

        }

        //Define o contexto de opera��o da aplica��o
        private static void SetContextMode()
        {
            if (new Configuration().Properties["current_session_context_class"].Equals("web"))
                currentContext = ContextMode.WEB_CONTEXT;
            else
                currentContext = ContextMode.CALL_CONTEXT;
        }

        //Aloca uma sess�o do nhibernate no contexto da aplica��o
        public static void BindSession()
        {
            if (!CurrentSessionContext.HasBind(_sessionFactory))
            {
                CurrentSessionContext.Bind(_sessionFactory.OpenSession());
            }
        }

        private static object Configuration()
        {
            /* TODO: Implementar Configura��o */
            throw new NotImplementedException();
        }

        //Desaloca a sess�o do nhibernate no contexto da aplica��o
        public static void UnBindSession()
        {
            ISession session = CurrentSessionContext.Unbind(_sessionFactory);

            if (session == null) return;

            session.Clear();

            if (session.IsOpen)
                session.Close();

            session.Dispose();
            session = null;
        }

        //Criar uma fabrica de sess�o do nhibernate
        private static ISessionFactory CreateSessionFactory()
        {

           return NHibernateSessionFactory.Instance.SessionFactory();

        }

        //Obtem a sess�o corrente do nhibernate
        public static ISession GetCurrentSession()
        {
            ISession m_session;

            //captura a sess�o corrente do contexto
            m_session = _sessionFactory.GetCurrentSession();

            //se a sess�o estiver fechada, ent�o a mesma deve ser aberta
            if (!m_session.IsOpen)
                m_session = _sessionFactory.OpenSession();

            //se a sess�o estiver desconectada, ent�o a mesma deve ser reconectada
            if (!m_session.IsConnected)
                m_session.Reconnect();

            return m_session;

        }

        public static ISessionFactory SessionFactory
        {
            get
            {
                return _sessionFactory;
            }
        }

        /// <summary>
        /// Recupera a configuracao do NHibernate
        /// </summary>
        /// <returns>Configura��o do NHibernate</returns>
        public static Configuration GetConfiguration()
        {
            return new Configuration().Configure();
        }
    }
}