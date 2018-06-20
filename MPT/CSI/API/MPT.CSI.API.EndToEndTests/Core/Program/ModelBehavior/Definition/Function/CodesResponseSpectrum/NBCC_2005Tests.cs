#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    [TestFixture]
    public class NBCC_2005_Get : CsiGet
    {
      
        public void GetFunction(string name,
            ref double PGA,
            ref double S02,
            ref double S05,
            ref double S1,
            ref double S2,
            ref eSiteClass_NBCC_2005 siteClass,
            ref double Fa,
            ref double Fv,
            ref double dampingRatio)
        {
          
        }
    }
    
    [TestFixture]
    public class NBCC_2005_Set : CsiSet
    {
      
        public void SetFunction(string name,
            double PGA,
            double S02,
            double S05,
            double S1,
            double S2,
            eSiteClass_NBCC_2005 siteClass,
            double Fa,
            double Fv,
            double dampingRatio)
        {
          
        }
    }
}
#endif