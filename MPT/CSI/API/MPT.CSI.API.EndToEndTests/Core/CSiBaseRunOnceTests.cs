using MPT.CSI.API.Core.Program;
using NUnit.Framework;

namespace MPT.CSI.API.EndToEndTests.Core
{
    [TestFixture]
    public abstract class CSiBaseRunOnceTests
    {
        protected CSiApplication _app;

        [TestFixtureSetUp]
        public void Setup()
        {
            _app = new CSiApplication(CSiData.pathApp);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            _app.Dispose();
        }
    }
}
