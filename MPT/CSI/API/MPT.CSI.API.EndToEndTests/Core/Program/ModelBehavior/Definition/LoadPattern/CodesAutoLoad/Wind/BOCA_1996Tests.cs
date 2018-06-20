#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wind;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wind
{
    [TestFixture]
    public class BOCA_1996_Get : CsiGet
    {
      
        public void GetLoad(string name,
            ref eWindExposure exposureFrom,
            ref double directionAngle,
            ref double Cpw,
            ref double Cpl,
            ref bool userSpecifiedHeights,
            ref double coordinateTopZ,
            ref double coordinateBottomZ,
            ref double windSpeed,
            ref eExposureTypeAToD exposureType,
            ref double importanceFactor,
            ref bool userSpecifiedGust,
            ref double gustFactor,
            ref bool userSpecifiedExposure)
        {
          
        }
    }
    
    [TestFixture]
    public class BOCA_1996_Set : CsiSet
    {
      
        
        public void SetLoad(string name,
            eWindExposure exposureFrom,
            double directionAngle,
            double Cpw,
            double Cpl,
            bool userSpecifiedHeights,
            double coordinateTopZ,
            double coordinateBottomZ,
            double windSpeed,
            eExposureTypeAToD exposureType,
            double importanceFactor,
            double gustFactor,
            bool userSpecifiedGust = false,
            bool userSpecifiedExposure = false)
        {
          
        }
    }
}
#endif