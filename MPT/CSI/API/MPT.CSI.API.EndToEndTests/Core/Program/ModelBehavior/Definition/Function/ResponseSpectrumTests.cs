#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Function
{
    [TestFixture]
    public class ResponseSpectrum_Get : CsiGet
    {   

        public void SetAutoCode(ResponseSpectrumFunction responseSpectrumFunction)
        {
          
        }
    }
}
#endif