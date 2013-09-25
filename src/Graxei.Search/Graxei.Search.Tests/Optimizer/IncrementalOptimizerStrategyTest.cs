using NHibernate.Cfg;
using NUnit.Framework;

namespace Graxei.Search.Tests.Optimizer
{
    [TestFixture]
    [Ignore("Does not complete")]
    public class IncrementalOptimizerStrategyTest : OptimizerTestCase
    {
        protected override void Configure(Configuration configuration)
        {
            base.Configure(configuration);
            configuration.SetProperty("hibernate.search.default.optimizer.transaction_limit.max", "10");
        }
    }
}