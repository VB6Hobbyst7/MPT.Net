#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17 && !BUILD_CSiBridgev16 && !BUILD_CSiBridgev17
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic
{
    [TestFixture]
    public class NTC_2008_Get : CsiGet
    {
      
        public void GetLoad(string name,
            ref eSeismicLoadDirection loadDirection,
            ref double eccentricity,
            ref eTimePeriodOption periodOption,
            ref eC1Type_NTC_2008 C1Type,
            ref double userSpecifiedPeriod,
            ref bool userSpecifiedHeights,
            ref double coordinateTopZ,
            ref double coordinateBottomZ,
            ref eSiteClass_Eurocode_8_2004 soilType,
            ref double PGA,
            ref eParameters_NTC_2008 parameterOptions,
            ref double latitude,
            ref double longitude,
            ref eIsland_NTC_2008 island,
            ref eLimitState_NTC_2008 limitState,
            ref eUsageClass_NTC_2008 usageClass,
            ref double nominalLife,
            ref double F0,
            ref double Tcs,
            ref eSpectrumType_NTC_2008 spectrumType,
            ref eTopography_NTC_2008 topography,
            ref double hRatio,
            ref double damping,
            ref double q,
            ref double lambda)
        {
          
        }
    }
    
    [TestFixture]
    public class NTC_2008_Set : CsiSet
    {
        
        public void SetLoad(string name,
            eSeismicLoadDirection loadDirection,
            double eccentricity,
            eTimePeriodOption periodOption,
            eC1Type_NTC_2008 C1Type,
            double userSpecifiedPeriod,
            bool userSpecifiedHeights,
            double coordinateTopZ,
            double coordinateBottomZ,
            eSiteClass_Eurocode_8_2004 soilType,
            double PGA,
            eParameters_NTC_2008 parameterOptions,
            double latitude,
            double longitude,
            eIsland_NTC_2008 island,
            eLimitState_NTC_2008 limitState,
            eUsageClass_NTC_2008 usageClass,
            double nominalLife,
            double F0,
            double Tcs,
            eSpectrumType_NTC_2008 spectrumType,
            eTopography_NTC_2008 topography,
            double hRatio,
            double damping,
            double q,
            double lambda)
        {
          
        }
    }
}
#endif
