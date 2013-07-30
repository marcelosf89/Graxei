using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using FAST.Thread.Contract;
using FAST.Thread.ExceptionDefs;

namespace FAST.Thread
{
    public sealed class MultiThreadFactory<TId>
    {
        private static readonly MultiThreadFactory<TId> _instance = new MultiThreadFactory<TId>();
        public ThreadList<TId> Threads { get; private set; }

        public static MultiThreadFactory<TId> Instance
        {
            get
            {
                return _instance;
            }
        }

        private MultiThreadFactory()
        {
            Threads = new ThreadList<TId>();
        }

        public void AddThread(IThread<TId> thread)
        {
            try
            {
                if (Threads.Any(p => p.NameThread.Equals(thread.NameThread)))
                {
                    throw new Exception("Erro ao adicionar 2 thread com o mesmo nome");
                }
                System.Threading.Thread fwt = new System.Threading.Thread(thread.Start);
                fwt.Start();
                Threads.Add(thread);
            }
            catch (ExceptionCloseThread<TId> ex)
            {
                ex.Thread.Stop();
                throw ex;
            }
        }

        public void CloseThread(TId idThread)
        {
            IThread<TId> fwt = Threads.Where(p => p.NameThread.ToString().ToUpper().Equals(idThread.ToString().ToUpper())).SingleOrDefault();
            if (fwt == null)
            {
                throw new Exception("Erro ao localizar a thread");
            }
            fwt.Stop();
            Threads.Remove(fwt);
        }

        public void RestartThread(IThread<TId> thread)
        {
            if (Threads.Any(p => p.NameThread.Equals(thread.NameThread)))
            {
                CloseThread(thread.NameThread);
            }
            this.AddThread(thread);
        }

        public IThread<TId> GetThread(TId idThread)
        {
            IThread<TId> fwt = Threads.Where(p => p.NameThread.Equals(idThread)).SingleOrDefault();
            if (fwt == null)
            {
                throw new Exception("Erro ao localizar a thread");
            }
            return fwt;
        }
    }
}
