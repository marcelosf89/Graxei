using Lucene.Net.Search;
using Graxei.Search.Attributes;

namespace Graxei.Search.Tests.Filter
{
    public class ExcludeAllFilterFactory
    {
        [Factory]
        public Lucene.Net.Search.Filter GetFilter()
        {
            return new CachingWrapperFilter(new ExcludeAllFilter());
        }
    }
}