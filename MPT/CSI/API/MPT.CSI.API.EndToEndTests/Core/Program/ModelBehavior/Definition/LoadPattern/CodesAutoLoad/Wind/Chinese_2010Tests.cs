#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wind;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wind
{
    [TestFixture]
    public class Chinese_2010_Get : CsiGet
    {
      
        public void GetLoad(string name,
            ref eWindExposure exposureFrom,
            ref double directionAngle,
            ref double buildingWidth,
            ref double Us,
            ref bool uniformTaper,
            ref double BhOverB0,
            ref bool userSpecifiedHeights,
            ref double coordinateTopZ,
            ref double coordinateBottomZ,
            ref double wzero,
            ref eRoughnessBToD Rt,
            ref ePhiZSource phiZSource,
            ref eT1Source T1Source,
            ref double userT,
            ref double dampingRatio,
            ref bool userSpecifiedExposure)
        {
          
        }
    }
    
    [TestFixture]
    public class Chinese_2010_Set : CsiSet
    {
      
        
        public void SetLoad(string name,
            eWindExposure exposureFrom,
            double directionAngle,
            double buildingWidth,
            double Us,
            bool uniformTaper,
            double BhOverB0,
            bool userSpecifiedHeights,
            double coordinateTopZ,
            double coordinateBottomZ,
            double wzero,
            eRoughnessBToD Rt,
            ePhiZSource phiZSource,
            eT1Source T1Source,
            double userT,
            double dampingRatio,
            bool userSpecifiedExposure = false)
        {
          
        }
    }
}
#endif