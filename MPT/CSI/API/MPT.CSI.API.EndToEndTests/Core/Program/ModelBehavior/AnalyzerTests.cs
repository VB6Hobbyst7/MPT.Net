using System.Linq;
using IO = System.IO;
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior
{
    [TestFixture]
    public class Analyzer_Get : CsiGet
    {

        public void GetCaseStatus(ref string[] caseName,
            ref eCaseStatus[] caseStatus)
        {
          
        }

        
        public void GetActiveDOF(ref DegreesOfFreedomGlobal activeDOFs)
        {
          
        }

       
       
        public void GetSolverOption_1(ref eSolverType solverType,
            ref eSolverProcessType solverProcessType,
            ref bool force32BitSolver,
            ref string stiffCase)
        {
          
        }


        
        public void GetRunCaseFlag(ref string[] caseName,
            ref bool[] caseSetToRun)
        {
          
        }
    }
    
    [TestFixture]
    public class Analyzer_Set : CsiSet
    {
      
        public void CreateAnalysisModel()
        {
          
        }

        
        public void RunAnalysis(string filePath)
        {
          
        }

        
        
        public void DeleteResults(string nameLoadCase,
            bool deleteAll = false)
        {
          
        }

        
        
        public void ModifyUnDeformedGeometry(string caseName,
            double scaleFactor,
            int stageOfStageConstruction = -1,
            bool resetToOriginal = false)
        {
          
        }

       
       
        public void ModifyUndeformedGeometryModeShape(string caseName,
            int mode,
            double maxDisplacement,
            int direction,
            bool resetToOriginal = false)
        {
          
        }

       
       
        public void SetActiveDOF(DegreesOfFreedomGlobal activeDOFs)
        {
          
        }

        
        
        public void SetSolverOption_1(eSolverType solverType,
            eSolverProcessType solverProcessType,
            bool force32BitSolver,
            string stiffCase = "")
        {
          
        }

        
        
        public void SetRunCaseFlag(string caseName,
            bool caseSetToRun,
            bool applyToAll = false)
        {
          
        }
    }
}
