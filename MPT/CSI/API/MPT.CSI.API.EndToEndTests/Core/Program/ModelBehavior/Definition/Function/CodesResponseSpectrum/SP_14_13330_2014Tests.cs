#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17 && !BUILD_CSiBridgev16 && !BUILD_CSiBridgev17
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    [TestFixture]
    public class SP_14_13330_2014_Get : CsiGet
    {
      
        public void GetFunction(string name,
            ref eSpectrumDirection_SP_14_13330_2014 direction,
            ref eSeismicIntensity_SP_14_13330_2014 regionSeismicity,
            ref eSiteClass_SP_14_13330_2014 soilCategory,
            ref double K0Factor,
            ref double K1Factor,
            ref double KPsiFactor,
            ref bool isSoilNonlinear,
            ref double ASoil,
            ref double dampingRatio)
        {
          
        }
    }
    
    [TestFixture]
    public class SP_14_13330_2014_Set : CsiSet
    {
        
        
        public void SetFunction(string name,
            eSpectrumDirection_SP_14_13330_2014 direction,
            eSeismicIntensity_SP_14_13330_2014 regionSeismicity,
            eSiteClass_SP_14_13330_2014 soilCategory,
            double K0Factor,
            double K1Factor,
            double KPsiFactor,
            bool isSoilNonlinear,
            double ASoil,
            double dampingRatio)
        {
          
        }
    }
}
#endif