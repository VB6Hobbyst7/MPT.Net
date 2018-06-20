#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17 && !BUILD_CSiBridgev16 && !BUILD_CSiBridgev17
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.ObjectModel
{
    [TestFixture]
    public class ExternalAnalysisResults_Get : CsiGet
    {      
      
        public void PresetFrameCases(string name,
            int numberCaseNames,
            ref string[] caseNames)
        {
          
        }

        
        public void GetFrameForces(string name,
            string initialCase,
            int caseStep,
            ref double[] P,
            ref double[] V2,
            ref double[] V3,
            ref double[] T,
            ref double[] M2,
            ref double[] M3)
        {
          
        }

      
      
        public void GetFrameStations(string name,
            ref double[] distancesFromIEnd)
        {
          
        }
    }
    
    [TestFixture]
    public class ExternalAnalysisResults_Set : CsiSet
    {
      
        public void DeleteAllFrameForces()
        {
          
        }

       
       
        public void DeleteFrameForces(string name)
        {
          
        }

 
 
        public void SetFrameForce(string name,
            string initialCase,
            double[] P,
            double[] V2,
            double[] V3,
            double[] T,
            double[] M2,
            double[] M3)
        {
          
        }

  
  
        public void SetFrameForceMultiple(string[] frameNames,
            string[] loadCases,
            int[] firstSteps,
            int[] lastSteps,
            double[] P,
            double[] V2,
            double[] V3,
            double[] T,
            double[] M2,
            double[] M3)
        {
          
        }

      
      
        public void SetFrameStations(string name,
            ref double[] distancesFromIEnd)
        {
          
        }
    }
}
#endif