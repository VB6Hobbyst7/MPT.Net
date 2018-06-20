#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wind;
using User = MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wind.User;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadPattern
{
    [TestFixture]
    public class AutoWind_Get : CsiGet
    {
      
        public void GetAutoCode(string name,
            ref string codeName)
        {
          
        }
        
#if BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_CSiBridgev16 || BUILD_CSiBridgev17
        
        
        public void GetExposure(string patternName,
            ref string[] diaphragmNames,
            ref double[] xCoordinates,
            ref double[] yCoordinates,
            ref double[] diaphragmWidth,
            ref double[] diaphragmHeight)
        {
          
        }
#else
  
        public void GetExposure(string patternName,
            ref string[] diaphragmNames,
            ref double[] xCoordinates,
            ref double[] yCoordinates,
            ref double[] diaphragmWidth,
            ref double[] diaphragmDepth,
            ref double[] diaphragmHeight)
        {

        }
#endif
    }
    
    [TestFixture]
    public class AutoWind_Set : CsiSet
    {
      
        public void SetAutoCode(AutoLoad autoLoad)
        {
          
        }


        
        public void SetAutoCode(AutoWindLoad autoWindLoad)
        {
          
        }

        
        public void SetNone(string name)
        {
          
        }
        
#if BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_CSiBridgev16 || BUILD_CSiBridgev17

        
        public void SetExposure(string patternName,
            string diaphragmName,
            double xCoordinate,
            double yCoordinate,
            double diaphragmWidth,
            double diaphragmHeight)
        {
          
        }
#else
        
        public void SetExposure(string patternName,
            string diaphragmName,
            double xCoordinate,
            double yCoordinate,
            double diaphragmWidth,
            double diaphragmDepth,
            double diaphragmHeight)
        {
          
        }  
#endif
    }
}
#endif