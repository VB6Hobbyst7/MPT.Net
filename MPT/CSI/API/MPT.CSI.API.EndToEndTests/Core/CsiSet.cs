using NUnit.Framework;

namespace MPT.CSI.API.EndToEndTests.Core
{
    [TestFixture]
    public abstract class CsiSet : CsiGetSetBase
    {
        [SetUp]
        public void Setup()
        {
            setup(CSiData.pathModelSet);
        }

        [TearDown]
        public void TearDown()
        {
            tearDown();
        }
    }
}
