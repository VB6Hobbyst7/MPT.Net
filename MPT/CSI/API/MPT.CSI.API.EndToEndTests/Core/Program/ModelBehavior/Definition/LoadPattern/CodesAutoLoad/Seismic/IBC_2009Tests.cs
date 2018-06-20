#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic
{
    [TestFixture]
    public class IBC_2009_Get : CsiGet
    {
      
        public void GetLoad(string name,
            ref eSeismicLoadDirection loadDirection,
            ref double eccentricity,
            ref eTimePeriodOption periodOption,
            ref eCtType_IBC_2006 CtType,
            ref double userSpecifiedPeriod,
            ref bool userSpecifiedHeights,
            ref double coordinateTopZ,
            ref double coordinateBottomZ,
            ref eSiteClass_IBC_2006 siteClass,
            ref double omega,
            ref double R,
            ref double I,
            ref double Ss,
            ref double S1,
            ref double Fa,
            ref double Fv,
            ref double Cd,
            ref eSeismicCoefficient_IBC_2006 seismicCoefficientSource,
            ref double latitude,
            ref double longitude,
            ref string zipCode,
            ref double TL)
        {
          
        }
    }
    
    [TestFixture]
    public class IBC_2009_Set : CsiSet
    {
        
        
        public void SetLoad(string name,
            eSeismicLoadDirection loadDirection,
            double eccentricity,
            eTimePeriodOption periodOption,
            eCtType_IBC_2006 CtType,
            double userSpecifiedPeriod,
            bool userSpecifiedHeights,
            double coordinateTopZ,
            double coordinateBottomZ,
            eSiteClass_IBC_2006 siteClass,
            double omega,
            double R,
            double I,
            double Ss,
            double S1,
            double Fa,
            double Fv,
            double Cd,
            eSeismicCoefficient_IBC_2006 seismicCoefficientSource,
            double latitude,
            double longitude,
            string zipCode,
            double TL)
        {
          
        }
    }
}
#endif
