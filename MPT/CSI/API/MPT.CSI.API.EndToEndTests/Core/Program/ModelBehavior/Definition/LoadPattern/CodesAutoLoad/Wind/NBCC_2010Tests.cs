#if BUILD_SAP2000v19 || BUILD_SAP2000v20 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wind;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wind
{
    [TestFixture]
    public class NBCC_2010_Get : CsiGet
    {
      
        public void GetLoad(string name,
            ref eWindExposure exposureFrom,
            ref double directionAngle,
            ref double Cpw,
            ref double Cpl,
            ref bool userSpecifiedHeights,
            ref double coordinateTopZ,
            ref double coordinateBottomZ,
            ref double q,
            ref double gustFactor,
            ref double importanceFactor,
            ref eTerrainType terrainType,
            ref double CeWindward,
            ref double CeLeeward,
            ref bool userSpecifiedExposure)
        {
          
        }
    }
    
    [TestFixture]
    public class NBCC_2010_Set : CsiSet
    {
        
        
        public void SetLoad(string name,
            eWindExposure exposureFrom,
            double directionAngle,
            double Cpw,
            double Cpl,
            bool userSpecifiedHeights,
            double coordinateTopZ,
            double coordinateBottomZ,
            double q,
            double gustFactor,
            double importanceFactor,
            eTerrainType terrainType,
            double CeWindward,
            double CeLeeward,
            bool userSpecifiedExposure = false)
        {
          
        }
    }
}
#endif