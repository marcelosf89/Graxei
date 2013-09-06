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
        /// Define o enumerator que tem a lista de possíveis contextos de aplicação
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
        /// Define a fabrica de sessões do nhibernate
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
        /// Criar somente uma vez a fábrica de sessões do nhibernate
        /// </summary>
        static NHibernateSessionManager()
        {

            //Seta o modo de operação da aplicação
            SetContextMode();

            _sessionFactory = CreateSessionFactory();

            //Se o modo corrente for CALL, então coloque uma sessão no contexto.
            //Isto significa que cada chamada terá uma unica sessão do nhibernate.
            if (currentContext == ContextMode.CALL_CONTEXT)
            {
                BindSession();
            }

            //Se o modo corrente for WEB, então o binding de sessão será feito
            //No inicio da requisão através de um HTTPModule

        }

        //Define o contexto de operação da aplicação
        private static void SetContextMode()
        {
            if (new Configuration().Properties["current_session_context_class"].Equals("web"))
                currentContext = ContextMode.WEB_CONTEXT;
            else
                currentContext = ContextMode.CALL_CONTEXT;
        }

        //Aloca uma sessão do nhibernate no contexto da aplicação
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

        //Desaloca a sessão do nhibernate no contexto da aplicação
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

        //Criar uma fabrica de sessão do nhibernate
        private static ISessionFactory CreateSessionFactory()
        {

           return NHibernateSessionFactory.Instance.SessionFactory();

        }

        //Obtem a sessão corrente do nhibernate
        public static ISession GetCurrentSession()
        {
            ISession m_session;

            //captura a sessão corrente do contexto
            m_session = _sessionFactory.GetCurrentSession();

            //se a sessão estiver fechada, então a mesma deve ser aberta
            if (!m_session.IsOpen)
                m_session = _sessionFactory.OpenSession();

            //se a sessão estiver desconectada, então a mesma deve ser reconectada
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
        /// Questiona se há uma transação aberta
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
        /// Abre uma transação se já não houver uma já existente
        /// </summary>
        /// <exception cref="Exception">Erro ao inicializar uma transação</exception>
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
        /// Executa Commit em uma transação existente, executa RollBack caso haja uma exceção
        /// </summary>
        /// <exception cref="Exception">Erro ao comitar uma transação</exception>
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
        /// Executa o rollback de uma transação
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
        /// <returns>Configuração do NHibernate</returns>
        public static Configuration GetConfiguration()
        {
            return new Configuration().Configure();
        }
    }
}