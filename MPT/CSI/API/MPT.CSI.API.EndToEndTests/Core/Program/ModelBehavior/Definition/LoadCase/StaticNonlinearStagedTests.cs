using System.Linq;
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase;
using MPT.CSI.API.Core.Support;


namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadCase
{
    [TestFixture]
    public class StaticNonlinearStaged_Get : CsiGet
    {
        
        public void GetInitialCase(string name,
            ref string initialCase)
        {
          
        }

        
        public void GetMassSource(string name,
            ref string sourceName)
        {
          
        }
        
        
        public void GetGeometricNonlinearity(string name,
            ref eGeometricNonlinearity geometricNonlinearityType)
        {
          
        }

        
        public void GetSolutionControlParameters(string name,
            ref int maxTotalSteps,
            ref int maxNullSteps,
            ref int maxConstantStiffnessIterationsPerStep,
            ref int maxNewtonRaphsonIterationsPerStep,
            ref double relativeIterationConvergenceTolerance,
            ref bool useEventStepping,
            ref double relativeEventLumpingTolerance,
            ref int maxNumberLineSearches,
            ref double relativeLineSearchAcceptanceTolerance,
            ref double lineSearchStepFactor)
        {
          
        }


        
        public void GetMaterialNonlinearity(string name,
            ref bool considerTimeDependent)
        {
          
        }

        
        public void GetResultsSaved(string name,
            ref eStageSavedOption stageSavedOption,
            ref int minStepsForInstantanousLoad,
            ref int minStepsForTimeDependentItems)
        {
          
        }


        public void GetHingeUnloading(string name,
            ref eHingeUnloadingType hingeUnloadingType)
        {
          
        }
        
        
        public void GetTargetForceParameters(string name,
            ref double convergenceTolerance,
            ref int maxIterations,
            ref double accelerationFactor,
            ref bool continueIfNoConvergence)
        {
          
        }


#if BUILD_ETABS2015 || BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_SAP2000v18 || BUILD_CSiBridgev16 || BUILD_CSiBridgev17 ||BUILD_CSiBridgev18 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20


        public void GetStageData(string name,
            ref int stage,
            ref eStageOperations[] operations,
            ref eObjectType[] objectTypes,
            ref string[] nameObjects,
            ref int[] ages,
            ref string[] loadOrObjectTypes,
            ref string[] loadOrObjectNames,
            ref double[] scaleFactors)
        {
          
        }

        
        public void GetStageDefinitions(string name,
            ref int[] duration,
            ref bool[] outputIsToBeSaved,
            ref string[] nameOutput,
            ref string[] comment)
        {

        
        }
#else
       
     
        public void GetStageData(string name,
            ref int stage,
            ref eStageOperations[] operations,
            ref eObjectType[] objectTypes,
            ref string[] nameObjects,
            ref double[] ages,
            ref string[] loadOrObjectTypes,
            ref string[] loadOrObjectNames,
            ref double[] scaleFactors)
        {
          
        }

        
        public void GetStageDefinitions(string name,
            ref double[] duration,
            ref bool[] outputIsToBeSaved,
            ref string[] nameOutput,
            ref string[] comment)
        {
          
        }
#endif
    }
    
    [TestFixture]
    public class StaticNonlinearStaged_Set : CsiSet
    {
      
        public void SetCase(string name)
        {
          
        }

        
        public void SetInitialCase(string name,
            string initialCase)
        {
          
        }

        
        public void SetMassSource(string name,
            string sourceName)
        {
          
        }

        
        public void SetGeometricNonlinearity(string name,
            eGeometricNonlinearity geometricNonlinearityType)
        {
          
        }
        
        public void SetSolutionControlParameters(string name,
            int maxTotalSteps,
            int maxNullSteps,
            int maxConstantStiffnessIterationsPerStep,
            int maxNewtonRaphsonIterationsPerStep,
            double relativeIterationConvergenceTolerance,
            bool useEventStepping,
            double relativeEventLumpingTolerance,
            int maxNumberLineSearches,
            double relativeLineSearchAcceptanceTolerance,
            double lineSearchStepFactor)
        {
          
        }

        
        public void SetMaterialNonlinearity(string name,
            ref bool considerTimeDependent)
        {
          
        }

        
        public void SetResultsSaved(string name,
            eStageSavedOption stageSavedOption,
            int minStepsForInstantanousLoad = 1,
            int minStepsForTimeDependentItems = 1)
        {
          
        }

        
        public void SetHingeUnloading(string name,
            eHingeUnloadingType hingeUnloadingType)
        {
          
        }

        
        public void SetTargetForceParameters(string name,
            double convergenceTolerance,
            int maxIterations,
            double accelerationFactor,
            bool continueIfNoConvergence)
        {
          
        }
      
#if BUILD_ETABS2015 || BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_SAP2000v18 || BUILD_CSiBridgev16 || BUILD_CSiBridgev17 ||BUILD_CSiBridgev18 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20


        
        public void SetStageData(string name,
            int stage,
            eStageOperations[] operations,
            eObjectType[] objectTypes,
            string[] nameObjects,
            int[] ages,
            string[] loadOrObjectTypes,
            string[] loadOrObjectNames,
            double[] scaleFactors)
        {
          
        }

        
        public void SetStageDefinitions(string name,
            int[] duration,
            bool[] outputIsToBeSaved,
            string[] nameOutput,
            string[] comment)
        {
          
        }
#else

        public void SetStageData(string name,
            int stage,
            eStageOperations[] operations,
            eObjectType[] objectTypes,
            string[] nameObjects,
            double[] ages,
            string[] loadOrObjectTypes,
            string[] loadOrObjectNames,
            double[] scaleFactors)
        {
          
        }  
#endif
    }
}
