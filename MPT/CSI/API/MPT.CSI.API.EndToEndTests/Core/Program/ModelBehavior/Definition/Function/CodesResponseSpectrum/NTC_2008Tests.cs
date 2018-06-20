#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17 && !BUILD_CSiBridgev16 && !BUILD_CSiBridgev17
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    [TestFixture]
    public class NTC_2008_Get : CsiGet
    {
      
        public void GetFunction(string name,
            ref eParameters_NTC_2008 parametersOption,
            ref double latitude,
            ref double longitude,
            ref eIsland_NTC_2008 island,
            ref eLimitState_NTC_2008 limitState,
            ref eUsageClass_NTC_2008 usageClass,
            ref double nominalLife,
            ref double peakAcceleration,
            ref double F0,
            ref double Tcs,
            ref eSpectrumType_NTC_2008 spectrumType,
            ref eSiteClass_NTC_2008 soilType,
            ref eTopography_NTC_2008 topography,
            ref double hRatio,
            ref double damping,
            ref double q)
        {
          
        }
    }
    
    [TestFixture]
    public class NTC_2008_Set : CsiSet
    {

        
        public void SetFunction(string name,
            eParameters_NTC_2008 parametersOption,
            double latitude,
            double longitude,
            eIsland_NTC_2008 island,
            eLimitState_NTC_2008 limitState,
            eUsageClass_NTC_2008 usageClass,
            double nominalLife,
            double peakAcceleration,
            double F0,
            double Tcs,
            eSpectrumType_NTC_2008 spectrumType,
            eSiteClass_NTC_2008 soilType,
            eTopography_NTC_2008 topography,
            double hRatio,
            double damping,
            double q)
        {
          
        } 
    }
}
#endif