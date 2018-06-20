#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17 && !BUILD_CSiBridgev16 && !BUILD_CSiBridgev17
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    [TestFixture]
    public class JTG_B02_2013_Get : CsiGet
    {
      
        public void GetFunction(string name,
            ref eSpectrumDirection_JTG_B02_2013 direction,
            ref double peakAcceleration,
            ref double Tg,
            ref double Ci,
            ref double Cs,
            ref double dampingRatio)
        {
          
        }
    }
    
    [TestFixture]
    public class JTG_B02_2013_Set : CsiSet
    {

        
        
        public void SetFunction(string name,
            eSpectrumDirection_JTG_B02_2013 direction,
            double peakAcceleration,
            double Tg,
            double Ci,
            double Cs,
            double dampingRatio)
        {
          
        }
    }
}
#endif