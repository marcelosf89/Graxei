using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FAST.Thread.Contract
{
    public interface IThread<TId> : IDisposable
    {
        TId NameThread { get; }
        void Start();
        void Stop();
    }
}
