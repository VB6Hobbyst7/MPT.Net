#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wind;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wind
{
    [TestFixture]
    public class ASCE_7_05_Get : CsiGet
    {
      
        public void GetLoad(string name,
            ref eWindExposure exposureFrom,
            ref double directionAngle,
            ref bool userSpecifiedHeights,
            ref double coordinateTopZ,
            ref double coordinateBottomZ,
            ref double windSpeed,
            ref double Cpw,
            ref double Cpl,
            ref int ASCECase,
            ref double e1,
            ref double e2,
            ref eExposureTypeBToD exposureType,
            ref double importanceFactor,
            ref double Kzt,
            ref double gustFactor,
            ref double Kd,
            ref double solidGrossRatio,
            ref bool userSpecifiedExposure)
        {
          
        }
    }
    
    [TestFixture]
    public class ASCE_7_05_Set : CsiSet
    {

        
        public void SetLoad(string name,
            eWindExposure exposureFrom,
            double directionAngle,
            bool userSpecifiedHeights,
            double coordinateTopZ,
            double coordinateBottomZ,
            double windSpeed,
            double Cpw,
            double Cpl,
            int ASCECase,
            double e1,
            double e2,
            eExposureTypeBToD exposureType,
            double importanceFactor,
            double Kzt,
            double gustFactor,
            double Kd,
            double solidGrossRatio = 0.2,
            bool userSpecifiedExposure = false)
        {
          
        }
      
    }
}
#endif