#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    [TestFixture]
    public class Eurocode_8_Get : CsiGet
    {
      
        public void GetFunction(string name,
            ref double AG,
            ref eSiteClass_Eurocode_8 S,
            ref double n,
            ref double dampingRatio)
        {
          
        }
    }
    
    [TestFixture]
    public class Eurocode_8_Set : CsiSet
    {
      
        
        public void SetFunction(string name,
            double AG,
            eSiteClass_Eurocode_8 S,
            double n,
            double dampingRatio)
        {
          
        }
    }
}
#endif