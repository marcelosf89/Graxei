using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FAST.Thread.Contract;

namespace FAST.Thread.ExceptionDefs
{
    public class ExceptionCloseThread<TId> : Exception
    {
        public IThread<TId> Thread { get; private set; }
        public ExceptionCloseThread(IThread<TId> fwt)
            : base()
        {
            this.Thread = fwt;
            this.Thread.Dispose();
        }

    }
}
