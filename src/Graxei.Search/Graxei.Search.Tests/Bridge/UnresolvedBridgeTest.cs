using System.Collections;
using NHibernate.Cfg;
using Graxei.Search.Store;
using NUnit.Framework;

namespace Graxei.Search.Tests.Bridge
{
    [TestFixture]
    public class UnresolvedBridgeTest : SearchTestCase
    {
        protected override IList Mappings
        {
            get { return new string[] {}; }
        }

        [Test]
        public void SystemTypeForDocumentId()
        {
            Configuration tempCfg = new Configuration();
            tempCfg.Configure();
            tempCfg.SetProperty("hibernate.search.default.directory_provider", typeof(RAMDirectoryProvider).AssemblyQualifiedName);
            tempCfg.AddClass(typeof(Gangster));
			Assert.Throws<HibernateException>(()=>tempCfg.BuildSessionFactory(),"Unable to guess IFieldBridge for Id");
        }
    }
}