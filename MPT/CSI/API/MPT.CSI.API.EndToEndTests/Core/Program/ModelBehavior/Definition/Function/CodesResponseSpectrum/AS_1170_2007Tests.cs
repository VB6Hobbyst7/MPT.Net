#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    [TestFixture]
    public class AS_1170_2007_Get : CsiGet
    {
      
        public void GetFunction(string name,
            ref eSiteClass_AS_1170_2007 siteClass,
            ref double kp,
            ref double Z,
            ref double Sp,
            ref double u,
            ref double dampingRatio)
        {
          
        }
    }
    
    [TestFixture]
    public class AS_1170_2007_Set : CsiSet
    {
      
        
        public void SetFunction(string name,
            eSiteClass_AS_1170_2007 siteClass,
            double kp,
            double Z,
            double Sp,
            double u,
            double dampingRatio)
        {
          
        }
    }
}
#endif