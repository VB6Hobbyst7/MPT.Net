#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    [TestFixture]
    public class NCHRP_2007_Get : CsiGet
    {
      
        public void GetFunction(string name,
            ref eSeismicCoefficient_NCHRP_2007 seismicCoefficientOption,
            ref double latitude,
            ref double longitude,
            ref string zipCode,
            ref double Ss,
            ref double S1,
            ref eSiteClass_NCHRP_2007 siteClass,
            ref double Fa,
            ref double Fv,
            ref double dampingRatio)
        {
          
        }
    }
    
    [TestFixture]
    public class NCHRP_2007_Set : CsiSet
    {

        
        public void SetFunction(string name,
            eSeismicCoefficient_NCHRP_2007 seismicCoefficientOption,
            double latitude,
            double longitude,
            string zipCode,
            double Ss,
            double S1,
            eSiteClass_NCHRP_2007 siteClass,
            double Fa,
            double Fv,
            double dampingRatio)
        {
          
        }
    }
}
#endif