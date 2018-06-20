using NUnit.Framework;

namespace MPT.CSI.API.EndToEndTests.Core
{
    [TestFixture]
    public abstract class CsiGet : CsiGetSetBase
    {

        [TestFixtureSetUp]
        public void Setup()
        {
            setup(CSiData.pathModelQuery);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            tearDown();
        }
    }
}
