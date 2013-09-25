using System.Collections.Generic;
using Graxei.Search.Backend;
using Graxei.Search.Filter;
using Graxei.Search.Store;
using Graxei.Search.Store.Optimization;

namespace Graxei.Search.Engine
{
    /// <summary>
    /// Interface which gives access to the different directory providers and their configuration.
    /// </summary>
    public interface ISearchFactoryImplementor : ISearchFactory
    {
        IBackendQueueProcessorFactory BackendQueueProcessorFactory { get; set; }

        IDictionary<System.Type, DocumentBuilder> DocumentBuilders { get; }

        Dictionary<IDirectoryProvider, object> GetLockableDirectoryProviders();

        IWorker Worker { get; }

        void AddOptimizerStrategy(IDirectoryProvider provider, IOptimizerStrategy optimizerStrategy);

        IOptimizerStrategy GetOptimizerStrategy(IDirectoryProvider provider);

        IFilterCachingStrategy GetFilterCachingStrategy();

        FilterDef GetFilterDefinition(string name);

        LuceneIndexingParameters GetIndexingParameters(IDirectoryProvider provider);

        void AddIndexingParameters(IDirectoryProvider provider, LuceneIndexingParameters indexingParameters);

        void Close();
    }
}