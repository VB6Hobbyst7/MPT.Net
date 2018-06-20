#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic
{
    [TestFixture]
    public class NZS_1170_2004_Get : CsiGet
    {
      
        public void GetLoad(string name,
            ref eSeismicLoadDirection loadDirection,
            ref double eccentricity,
            ref eTimePeriodOption periodOption,
            ref double userSpecifiedPeriod,
            ref bool userSpecifiedHeights,
            ref double coordinateTopZ,
            ref double coordinateBottomZ,
            ref eSiteClass_NZS_1170_2004 siteClass,
            ref double R,
            ref double Z,
            ref double distanceToSeismicSource,
            ref double Sp,
            ref double Mu)
        {
          
        }
    }
    
    [TestFixture]
    public class NZS_1170_2004_Set : CsiSet
    {

        
        public void SetLoad(string name,
            eSeismicLoadDirection loadDirection,
            double eccentricity,
            eTimePeriodOption periodOption,
            double userSpecifiedPeriod,
            bool userSpecifiedHeights,
            double coordinateTopZ,
            double coordinateBottomZ,
            eSiteClass_NZS_1170_2004 siteClass,
            double R,
            double Z,
            double distanceToSeismicSource,
            double Sp,
            double Mu)
        {
          
        }
    }
}
#endif
