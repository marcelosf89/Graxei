using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FAST.Thread.Contract;

namespace FAST.Thread.ExceptionDefs
{
    public class ExceptionThread<TId> : Exception
    {
        public IThread<TId> Thread { get; private set; }
        public ExceptionThread(IThread<TId> fwt)
            : base()
        {
            this.Thread = fwt;
        }

    }
}
