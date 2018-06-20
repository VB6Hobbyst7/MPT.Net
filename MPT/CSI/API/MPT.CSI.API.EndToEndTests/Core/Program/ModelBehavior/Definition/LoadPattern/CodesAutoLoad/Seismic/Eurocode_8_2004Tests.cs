#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic
{
    [TestFixture]
    public class Eurocode_8_2004_Get : CsiGet
    {
      
        public void GetLoad(string name,
            ref eSeismicLoadDirection loadDirection,
            ref double eccentricity,
            ref eTimePeriodOption periodOption,
            ref double Ct,
            ref double userSpecifiedPeriod,
            ref bool userSpecifiedHeights,
            ref double coordinateTopZ,
            ref double coordinateBottomZ,
            ref eSiteClass_Eurocode_8_2004 soilType,
            ref double ag,
            ref eParameters_Eurocode_8_2004 country,
            ref eSpectrumType_Eurocode_8_2004 spectrumType,
            ref double S,
            ref double Tb,
            ref double Tc,
            ref double Td,
            ref double beta,
            ref double q,
            ref double lambda)
        {
          
        }
    }
    
    [TestFixture]
    public class Eurocode_8_2004_Set : CsiSet
    {

        
        public void SetLoad(string name,
            eSeismicLoadDirection loadDirection,
            double eccentricity,
            eTimePeriodOption periodOption,
            double Ct,
            double userSpecifiedPeriod,
            bool userSpecifiedHeights,
            double coordinateTopZ,
            double coordinateBottomZ,
            eSiteClass_Eurocode_8_2004 soilType,
            double ag,
            eParameters_Eurocode_8_2004 country,
            eSpectrumType_Eurocode_8_2004 spectrumType,
            double S,
            double Tb,
            double Tc,
            double Td,
            double beta,
            double q,
            double lambda)
        {
          
        }
    }
}
#endif