using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Tests.TestObjects;
using Unity;

namespace Tests
{
    [TestClass]
    public class ComplianceFixture
    {
        [TestMethod]
        public void Compliance_CanResolveIUnityContainer()
        {
            using (IUnityContainer container = new UnityContainer())
            {
                var instance = container.Resolve<IUnityContainer>();
                Assert.IsNotNull(instance);
            }
        }

        [TestMethod]
        [Ignore]
        public void Compliance_CanResolveUnityContainer()
        {
            using (IUnityContainer container = new UnityContainer())
            {
                var instance = container.Resolve<IUnityContainer>();
                Assert.IsNotNull(instance);
            }
        }

        [TestMethod]
        public void Compliance_CanResolveISystemObject()
        {
            using (IUnityContainer container = new UnityContainer())
            {
                var instance = container.Resolve<object>();
                Assert.IsNotNull(instance);
            }
        }

        

        [TestMethod]
        public void Compliance_CanMapType()
        {
            IUnityContainer container = new UnityContainer()
                .RegisterType(typeof(ILogger), typeof(MockLogger));

            object logger = container.Resolve(typeof(ILogger));

            Assert.IsNotNull(logger);
            Assert.IsInstanceOfType(logger, typeof(MockLogger));
        }


    }
}