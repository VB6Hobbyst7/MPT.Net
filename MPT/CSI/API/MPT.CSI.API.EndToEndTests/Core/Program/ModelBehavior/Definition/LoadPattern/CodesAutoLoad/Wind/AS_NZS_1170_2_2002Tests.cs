#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wind;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wind
{
    [TestFixture]
    public class AS_NZS_1170_2_2002_Get : CsiGet
    {
      
        public void GetLoad(string name,
            ref eWindExposure exposureFrom,
            ref double directionAngle,
            ref double Cpw,
            ref double Cpl,
            ref double Ka,
            ref double Kc,
            ref double Kl,
            ref double Kp,
            ref bool userSpecifiedHeights,
            ref double coordinateTopZ,
            ref double coordinateBottomZ,
            ref double windSpeed,
            ref int terrainCategory,
            ref bool cycloneRegion,
            ref double Md,
            ref double Ms,
            ref double Mt,
            ref double Cdyn,
            ref bool userSpecifiedExposure)
        {
          
        }
    }
    
    [TestFixture]
    public class AS_NZS_1170_2_2002_Set : CsiSet
    {
      
        
        public void SetLoad(string name,
            eWindExposure exposureFrom,
            double directionAngle,
            double Cpw,
            double Cpl,
            double Ka,
            double Kc,
            double Kl,
            double Kp,
            bool userSpecifiedHeights,
            double coordinateTopZ,
            double coordinateBottomZ,
            double windSpeed,
            int terrainCategory,
            bool cycloneRegion,
            double Md,
            double Ms,
            double Mt,
            double Cdyn,
            bool userSpecifiedExposure = false)
        {
          
        }
    }
}
#endif