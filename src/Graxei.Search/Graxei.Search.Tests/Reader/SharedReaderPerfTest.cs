using NHibernate.Cfg;
using NUnit.Framework;

namespace Graxei.Search.Tests.Reader
{
    [TestFixture]
    public class SharedReaderPerfTest : ReaderPerfTestCase
    {
        protected override void Configure(Configuration configuration)
        {
            base.Configure(configuration);
            configuration.SetProperty(Environment.ReaderStrategy, "shared");
        }
    }
}