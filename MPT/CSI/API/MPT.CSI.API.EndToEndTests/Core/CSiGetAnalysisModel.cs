using MPT.CSI.API.Core.Program;
using NUnit.Framework;

namespace MPT.CSI.API.EndToEndTests.Core
{
    [TestFixture]
    public abstract class CsiGetAnalysisModel : CsiGet
    {
        [TestFixtureSetUp]
        public new void Setup()
        {
            _app.Model.Analyze.CreateAnalysisModel();
        }
    }
}
