#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    [TestFixture]
    public class AASHTO_2007_Get : CsiGet
    {
      
        public void GetFunction(string name,
            ref eSeismicCoefficient_AASHTO_2007 seismicCoefficientOption,
            ref double latitude,
            ref double longitude,
            ref string zipCode,
            ref double Ss,
            ref double S1,
            ref double PGA,
            ref eSiteClass_AASHTO_2007 siteClass,
            ref double Fa,
            ref double Fv,
            ref double Fpga,
            ref double dampingRatio)
        {
          
        }
    }
    
     [TestFixture]
    public class AASHTO_2007_Set : CsiSet
    {
           
        public void SetFunction(string name,
            eSeismicCoefficient_AASHTO_2007 seismicCoefficientOption,
            double latitude,
            double longitude,
            string zipCode,
            double Ss,
            double S1,
            double PGA,
            eSiteClass_AASHTO_2007 siteClass,
            double Fa,
            double Fv,
            double Fpga,
            double dampingRatio)
        {
          
        }
    }
}
#endif