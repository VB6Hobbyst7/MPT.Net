#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wind;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wind
{
    [TestFixture]
    public class UBC_94_Get : CsiGet
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
            ref eExposureTypeBToD exposureType,
            ref double importanceFactor,
            ref bool userSpecifiedExposure)
        {
          
        }
    }
    
    [TestFixture]
    public class UBC_94_Set : CsiSet
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
            eExposureTypeBToD exposureType,
            double importanceFactor,
            bool userSpecifiedExposure = false)
        {
          
        }
    }
}
#endif