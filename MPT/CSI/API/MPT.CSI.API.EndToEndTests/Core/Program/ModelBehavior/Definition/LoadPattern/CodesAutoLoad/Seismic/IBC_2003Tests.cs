#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic
{
    [TestFixture]
    public class IBC_2003_Get : CsiGet
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
            ref eSiteClass_IBC_2003 siteClass,
            ref double omega,
            ref double R,
            ref double Ss,
            ref double S1,
            ref eSource seismicCoefficientSource,
            ref double Fa,
            ref double Fv,
            ref double Cd,
            ref eSeismicGroup_IBC_2003 Sg)
        {
          
        }
    }
    
    [TestFixture]
    public class IBC_2003_Set : CsiSet
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
            eSiteClass_IBC_2003 siteClass,
            double omega,
            double R,
            double Ss,
            double S1,
            eSource seismicCoefficientSource,
            double Fa,
            double Fv,
            double Cd,
            eSeismicGroup_IBC_2003 Sg)
        {
          
        }
    }
}
#endif
