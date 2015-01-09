using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.Aop
{
    public class LogErroPostgresBehavior : IInterceptionBehavior
    {
        public bool WillExecute
        {
            get
            {
                return true;
            }
        }


        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return null;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext) 
        { 
            return null; 
        }
    }
}
