#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17 && !BUILD_CSiBridgev16 && !BUILD_CSiBridgev17
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    [TestFixture]
    public class CJJ_166_2011_Get : CsiGet
    {
      
        public void GetFunction(string name,
            ref eSpectrumDirection_CJJ_166_2011 direction,
            ref double peakAcceleration,
            ref double Tg,
            ref double dampingRatio)
        {
          
        }
    }
    
    [TestFixture]
    public class CJJ_166_2011_Set : CsiSet
    {
      
        
        public void SetFunction(string name,
            eSpectrumDirection_CJJ_166_2011 direction,
            double peakAcceleration,
            double Tg,
            double dampingRatio)
        {
          
        }
    }
}
#endif