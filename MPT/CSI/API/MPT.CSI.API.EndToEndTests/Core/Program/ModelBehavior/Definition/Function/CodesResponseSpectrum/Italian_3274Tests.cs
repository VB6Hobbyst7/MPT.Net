#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    [TestFixture]
    public class Italian_3274_Get : CsiGet
    {
      
        public void GetFunction(string name,
            ref double g,
            ref eSiteClass_Italian_3274 seismicIntensity,
            ref double q,
            ref eLevel_Italian_3274 level,
            ref double dampingRatio)
        {
          
        }
    }
    
    [TestFixture]
    public class Italian_3274_Set : CsiSet
    {
        
        public void SetFunction(string name,
            double g,
            eSiteClass_Italian_3274 seismicIntensity,
            double q,
            eLevel_Italian_3274 level,
            double dampingRatio)
        {
          
        }
    }
}
#endif