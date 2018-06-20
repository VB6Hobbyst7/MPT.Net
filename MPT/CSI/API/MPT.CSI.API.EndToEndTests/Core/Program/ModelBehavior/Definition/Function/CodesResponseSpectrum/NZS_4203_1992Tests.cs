#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    [TestFixture]
    public class NZS_4203_1992_Get : CsiGet
    {
      
        public void GetFunction(string name,
            ref double scalingFactor,
            ref eSiteClass_NZS_4203_1992 subsoilCategory,
            ref double dampingRatio)
        {
          
        }
    }
    
    [TestFixture]
    public class NZS_4203_1992_Set : CsiSet
    {

        public void SetFunction(string name,
            double scalingFactor,
            eSiteClass_NZS_4203_1992 subsoilCategory,
            double dampingRatio)
        {
          
        }
    }
}
#endif