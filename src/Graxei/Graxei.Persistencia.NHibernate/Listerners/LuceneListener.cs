using NHibernate.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Implementacao.NHibernate.Listerners
{
    public class LuceneListener : IPreInsertEventListener, IPreUpdateEventListener, IPreDeleteEventListener
    {
        public bool OnPreInsert(PreInsertEvent e)
        {
        }
    }
}
