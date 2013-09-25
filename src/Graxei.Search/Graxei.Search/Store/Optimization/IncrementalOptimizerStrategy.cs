using System.Collections.Generic;
using Lucene.Net.Index;
using Graxei.Search.Backend;
using Graxei.Search.Engine;

namespace Graxei.Search.Store.Optimization
{
    using System.IO;

    using Impl;
    using NHibernate;

    /// <summary>
    /// Optimization strategy triggered after a certain amount of operations
    /// </summary>
    public class IncrementalOptimizerStrategy : IOptimizerStrategy
    {
		private static readonly IInternalLogger log = LoggerProvider.LoggerFor(typeof(IncrementalOptimizerStrategy));

        private int operationMax = -1;
        private int transactionMax = -1;
        private long operations;
        private long transactions;
        private IDirectoryProvider directoryProvider;

        #region Property methods

        public bool NeedOptimization
        {
            get
            {
                return (operationMax != -1 && operations >= operationMax) ||
                       (transactionMax != -1 && transactions >= transactionMax);
            }
        }

        #endregion

        #region Public methods

        public void Initialize(IDirectoryProvider directoryProvider, IDictionary<string, string> indexProperties,
                               ISearchFactoryImplementor searchFactoryImplementor)
        {
            this.directoryProvider = directoryProvider;
            string maxString;

            indexProperties.TryGetValue("optimizer.operation_limit.max", out maxString);
            if (!string.IsNullOrEmpty(maxString))
            {
                int.TryParse(maxString, out operationMax);
            }

            indexProperties.TryGetValue("optimizer.transaction_limit.max", out maxString);
            if (!string.IsNullOrEmpty(maxString))
            {
                int.TryParse(maxString, out transactionMax);
            }
        }

        public void OptimizationForced()
        {
            operations = 0;
            transactions = 0;
        }

        public void AddTransaction(long theOperations)
        {
            operations += theOperations;
            transactions++;
        }

        public void Optimize(Workspace workspace)
        {
            if (!NeedOptimization)
            {
                return;
            }

            if (log.IsDebugEnabled)
            {
                log.Debug("Optimize " + directoryProvider.Directory + " after " + operations + " operations and " + transactions + " transactions");
            }

            using (IndexWriter writer = workspace.GetIndexWriter(directoryProvider))
            {
                try
                {
                    writer.Optimize();
                }
                catch (IOException e)
                {
                    throw new SearchException("Unable to optimize directoryProvider: " + directoryProvider.Directory, e);
                }

                OptimizationForced();
            }
        }

        #endregion
    }
}