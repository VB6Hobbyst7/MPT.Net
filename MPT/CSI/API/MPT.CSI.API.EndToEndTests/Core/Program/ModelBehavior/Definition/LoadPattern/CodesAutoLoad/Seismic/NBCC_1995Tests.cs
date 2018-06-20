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
    public class NBCC_1995_Get : CsiGet
    {
        public void GetLoad(string name,
            ref eSeismicLoadDirection loadDirection,
            ref double eccentricity,
            ref eTimePeriodOption periodOption,
            ref eCtType_NBCC_1995 CtType,
            ref double userSpecifiedPeriod,
            ref bool userSpecifiedHeights,
            ref double coordinateTopZ,
            ref double coordinateBottomZ,
            ref double R,
            ref double I,
            ref double Ds,
            ref eZa_NBCC_1995 Za,
            ref eZv_NBCC_1995 Zv,
            ref eSource ZvRatioSource,
            ref double ZvRatio,
            ref double F)
        {
          
        }
    }
    
    [TestFixture]
    public class NBCC_1995_Set : CsiSet
    {
        
        public void SetLoad(string name,
            eSeismicLoadDirection loadDirection,
            double eccentricity,
            eTimePeriodOption periodOption,
            eCtType_NBCC_1995 CtType,
            double userSpecifiedPeriod,
            bool userSpecifiedHeights,
            double coordinateTopZ,
            double coordinateBottomZ,
            double R,
            double I,
            double Ds,
            eZa_NBCC_1995 Za,
            eZv_NBCC_1995 Zv,
            eSource ZvRatioSource,
            double ZvRatio,
            double F)
        {
          
        }
    }
}
#endif
