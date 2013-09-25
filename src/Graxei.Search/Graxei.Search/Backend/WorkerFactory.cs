using System.Collections;
using NHibernate.Cfg;
using Graxei.Search.Backend.Impl;
using Graxei.Search.Impl;

namespace Graxei.Search.Backend
{
    public static class WorkerFactory
    {
        public static IWorker CreateWorker(Configuration cfg, SearchFactoryImpl searchFactory)
        {
            IWorker worker = new TransactionalWorker();
            worker.Initialize((IDictionary) cfg.Properties, searchFactory);
            return worker;
        }
    }
}