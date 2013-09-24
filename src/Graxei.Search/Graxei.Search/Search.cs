using Graxei.Search.Impl;
using NHibernate;

namespace Graxei.Search
{
    public static class Search
    {
        public static IFullTextSession CreateFullTextSession(ISession session)
        {
            return session as FullTextSessionImpl ??
                new FullTextSessionImpl(session);
        }
    }
}
