using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FAST.Thread.Contract;

namespace FAST.Thread
{
    public class ThreadList<TId> : List<IThread<TId>>
    {
        public IThread<TId> this[TId nameThread]
        {
            get
            {
                return this.Where(p => p.NameThread.Equals(nameThread)).SingleOrDefault();
            }
        }

    }
}
