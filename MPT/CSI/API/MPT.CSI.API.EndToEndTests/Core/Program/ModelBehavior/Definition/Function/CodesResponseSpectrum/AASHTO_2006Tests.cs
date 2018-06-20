#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    [TestFixture]
    public class AASHTO_2006_Get : CsiGet
    {
      
        public void GetFunction(string name,
            ref double A,
            ref eSiteClass_AASHTO_2006 soilProfileType,
            ref double dampingRatio)
        {
          
        }
    }
    
     [TestFixture]
    public class AASHTO_2006_Set : CsiSet
    {
      
        public void SetFunction(string name,
            double A,
            eSiteClass_AASHTO_2006 soilProfileType,
            double dampingRatio)
        {
          
        }
    }
}
#endif
