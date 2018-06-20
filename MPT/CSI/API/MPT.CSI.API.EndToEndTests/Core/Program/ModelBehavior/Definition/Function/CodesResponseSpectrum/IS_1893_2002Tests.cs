#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    [TestFixture]
    public class IS_1893_2002_Get : CsiGet
    {
      
        public void GetFunction(string name,
            ref double Z,
            ref eSiteClass_IS_1893_2002 S,
            ref double dampingRatio)
        {
          
        }
    }
    
    [TestFixture]
    public class IS_1893_2002_Set : CsiSet
    {

        
        public void SetFunction(string name,
            double Z,
            eSiteClass_IS_1893_2002 S,
            double dampingRatio)
        {
          
        }
    }
}
#endif