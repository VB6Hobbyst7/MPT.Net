#if BUILD_SAP2000v19 || BUILD_SAP2000v20 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic
{
    [TestFixture]
    public class NBCC_2015_Get : CsiGet
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
            ref double S5,
            ref double S10,
            ref double F02,
            ref double F05,
            ref double F1,
            ref double F2,
            ref double F5,
            ref double F10,
            ref double Mv,
            ref double Rd)
        {
          
        }
    }
    
    [TestFixture]
    public class NBCC_2015_Set : CsiSet
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
            double S5,
            double S10,
            double F02,
            double F05,
            double F1,
            double F2,
            double F5,
            double F10,
            double Mv,
            double Rd)
        {
          
        }
    }
}
#endif
