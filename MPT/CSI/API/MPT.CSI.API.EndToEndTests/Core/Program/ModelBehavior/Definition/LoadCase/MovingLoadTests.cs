#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadCase
{
    [TestFixture]
    public class MovingLoad_Get : CsiGet
    {

        
        public void GetInitialCase(string name,
            ref string initialCase)
        {
          
        }
        
        
        public void GetLoads(string name,
            ref string[] vehicleClass,
            ref double[] scaleFactor,
            ref double[] minLanesLoaded,
            ref double[] maxLanesLoaded)
        {
          
        }

        
        public void GetLanesLoaded(string name,
            int loadNumber,
            ref string[] nameLanes)
        {
          
        }

        
        public void GetDirectionalFactors(string name,
            ref double verticalLoad,
            ref double brakingLoad,
            ref double centrifugalLoad)
        {
          
        }

        
        public void GetMultiLaneScaleFactor(string name,
            ref double[] scaleFactors)
        {
          
        }
    }
    
    [TestFixture]
    public class MovingLoad_Set : CsiSet
    {
      
        public void SetCase(string name)
        {
          
        }

        
        public void SetInitialCase(string name,
            string initialCase)
        {
          
        }

        
        public void SetLoads(string name,
            string[] vehicleClass,
            double[] scaleFactor,
            double[] minLanesLoaded,
            double[] maxLanesLoaded)
        {
          
        }

        
        public void SetLanesLoaded(string name,
            int loadNumber,
            string[] nameLanes)
        {
          
        }

        
        public void SetDirectionalFactors(string name,
            double verticalLoad,
            double brakingLoad,
            double centrifugalLoad)
        {
          
        }
        
        
        public void SetMultiLaneScaleFactor(string name,
            double[] scaleFactors)
        {
          
        }
    }
}
#endif
