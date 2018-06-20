#if BUILD_SAP2000v19 || BUILD_SAP2000v20 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    [TestFixture]
    public class NBCC_2015_Get : CsiGet
    {
      
        public void GetFunction(string name,
            ref double PGA,
            ref double S02,
            ref double S05,
            ref double S1,
            ref double S2,
            ref double S5,
            ref double S10,
            ref eSiteClass_NBCC_2015 siteClass,
            ref double F02,
            ref double F05,
            ref double F1,
            ref double F2,
            ref double F5,
            ref double F10,
            ref double dampingRatio)
        {
          
        }
    }
    
    [TestFixture]
    public class NBCC_2015_Set : CsiSet
    {
        
        
        public void SetFunction(string name,
            double PGA,
            double S02,
            double S05,
            double S1,
            double S2,
            double S5,
            double S10,
            eSiteClass_NBCC_2015 siteClass,
            double F02,
            double F05,
            double F1,
            double F2,
            double F5,
            double F10,
            double dampingRatio)
        {
          
        }
    }
}
#endif