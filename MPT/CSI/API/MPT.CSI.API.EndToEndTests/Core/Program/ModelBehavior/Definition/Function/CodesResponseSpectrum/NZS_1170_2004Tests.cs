#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    [TestFixture]
    public class NZS_1170_2004_Get : CsiGet
    {
      
        public void GetFunction(string name,
            ref eSiteClass_NZS_1170_2004 siteClass,
            ref double Z,
            ref double R,
            ref double distanceToFault,
            ref double dampingRatio)
        {
          
        }
    }
    
    [TestFixture]
    public class NZS_1170_2004_Set : CsiSet
    {

        public void SetFunction(string name,
            eSiteClass_NZS_1170_2004 siteClass,
            double Z,
            double R,
            double distanceToFault,
            double dampingRatio)
        {
          
        }
    }
}
#endif