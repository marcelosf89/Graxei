using NHibernate.Cfg;
using NUnit.Framework;

namespace Graxei.Search.Tests.Reader
{
    [TestFixture]
    public class NotSharedReaderPerfTest : ReaderPerfTestCase
    {
        protected override void Configure(Configuration configuration)
        {
            base.Configure(configuration);
            configuration.SetProperty(Environment.ReaderStrategy, "not-shared");
        }
    }
}