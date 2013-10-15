using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Graxei.FluentNHibernate.Configuracao
{
    public interface INHibernateFactory
    {
        ISessionFactory GetSessionFactory();
    }
}
