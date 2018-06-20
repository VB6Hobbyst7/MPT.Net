#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    [TestFixture]
    public class Eurocode_8_2004_Get : CsiGet
    {
      
        public void GetFunction(string name,
            ref eParameters_Eurocode_8_2004 country,
            ref eSpectrumDirection_Eurocode_8_2004 direction,
            ref eSpectrumType_Eurocode_8_2004 spectrumType,
            ref eSiteClass_Eurocode_8_2004 groundType,
            ref double ag,
            ref double S,
            ref double AvgOverAg,
            ref double Tb,
            ref double Tc,
            ref double Td,
            ref double beta,
            ref double q,
            ref double dampingRatio)
        {
          
        }
    }
    
    [TestFixture]
    public class Eurocode_8_2004_Set : CsiSet
    {
      
        public void SetFunction(string name,
            eParameters_Eurocode_8_2004 country,
            eSpectrumDirection_Eurocode_8_2004 direction,
            eSpectrumType_Eurocode_8_2004 spectrumType,
            eSiteClass_Eurocode_8_2004 groundType,
            double ag,
            double S,
            double AvgOverAg,
            double Tb,
            double Tc,
            double Td,
            double beta,
            double q,
            double dampingRatio)
        {
          
        }
    }
}
#endif