using System;
using FAST.Log;
using NHibernate;
using NHibernate.Cfg;
using System.Reflection;
using NHibernate.Context;
using Graxei.FluentNHibernate.Configuracao;

namespace Graxei.FluentNHibernate.GerenciarSessao
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

            //Seta o modo de opera��o da aplica��o
            SetContextMode();

            _sessionFactory = CreateSessionFactory();

            //Se o modo corrente for CALL, ent�o coloque uma sess�o no contexto.
            //Isto significa que cada chamada ter� uma unica sess�o do nhibernate.
            if (currentContext == ContextMode.CALL_CONTEXT)
            {
                BindSession();
            }

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
        /// Questiona se h� uma transa��o aberta
        /// </summary>
        /// <returns>True ou False</returns>
        public static bool HasOpenTransaction()
        {
            return NHibernateSessionPerRequest.GetCurrentSession().Transaction != null &&
                NHibernateSessionPerRequest.GetCurrentSession().Transaction.IsActive &&
                !NHibernateSessionPerRequest.GetCurrentSession().Transaction.WasCommitted &&
                !NHibernateSessionPerRequest.GetCurrentSession().Transaction.WasRolledBack;
        }

        /// <summary>
        /// Abre uma transa��o se j� n�o houver uma j� existente
        /// </summary>
        /// <exception cref="Exception">Erro ao inicializar uma transa��o</exception>
        public static void BeginTransaction()
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
        /// Executa Commit em uma transa��o existente, executa RollBack caso haja uma exce��o
        /// </summary>
        /// <exception cref="Exception">Erro ao comitar uma transa��o</exception>
        public static void CommitTransaction()
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
        /// Executa o rollback de uma transa��o
        /// </summary>
        public static void RollbackTransaction()
        {
            if (HasOpenTransaction())
            {
                NHibernateSessionPerRequest.GetCurrentSession().Transaction.Rollback();
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