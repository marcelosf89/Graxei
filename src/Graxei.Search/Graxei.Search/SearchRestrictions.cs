using Lucene.Net.Analysis.Standard;
using Lucene.Net.QueryParsers;
using NHibernate.Criterion;

namespace Graxei.Search
{
    using System;

    [Obsolete("Use SetCriteriaQuery against IFullTextSession")]
    public static class SearchRestrictions
    {
        public static ICriterion Query(Lucene.Net.Search.Query luceneQuery)
        {
            return new LuceneQueryExpression(luceneQuery);
        }

        public static ICriterion Query(string luceneQuery)
        {
            QueryParser parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30,string.Empty, new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30));
            return Query(parser.Parse(luceneQuery));
        }

        public static ICriterion Query(string defaultField, string luceneQuery)
        {
            QueryParser parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30,defaultField, new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30));
            return Query(parser.Parse(luceneQuery));
        }
    }
}