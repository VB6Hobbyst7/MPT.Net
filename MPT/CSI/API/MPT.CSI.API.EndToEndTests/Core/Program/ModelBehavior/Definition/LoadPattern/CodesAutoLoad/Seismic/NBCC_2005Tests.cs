#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic
{
    [TestFixture]
    public class NBCC_2005_Get : CsiGet
    {
      
        public void GetLoad(string name,
            ref eSeismicLoadDirection loadDirection,
            ref double eccentricity,
            ref eTimePeriodOption periodOption,
            ref eCtType_NBCC_2005 CtType,
            ref double userSpecifiedPeriod,
            ref bool userSpecifiedHeights,
            ref double coordinateTopZ,
            ref double coordinateBottomZ,
            ref eSiteClass_NBCC_2005 siteClass,
            ref double Ro,
            ref double I,
            ref double PGA,
            ref double S02,
            ref double S05,
            ref double S1,
            ref double S2,
            ref double Fa,
            ref double Fv,
            ref double Mv,
            ref double Rd)
        {
          
        }
    }
    
    [TestFixture]
    public class NBCC_2005_Set : CsiSet
    {
        
        
        public void SetLoad(string name,
            eSeismicLoadDirection loadDirection,
            double eccentricity,
            eTimePeriodOption periodOption,
            eCtType_NBCC_2005 CtType,
            double userSpecifiedPeriod,
            bool userSpecifiedHeights,
            double coordinateTopZ,
            double coordinateBottomZ,
            eSiteClass_NBCC_2005 siteClass,
            double Ro,
            double I,
            double PGA,
            double S02,
            double S05,
            double S1,
            double S2,
            double Fa,
            double Fv,
            double Mv,
            double Rd)
        {
          
        }
    }
}
#endif
