using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase;
using MPT.CSI.API.Core.Support;



namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadCase
{
    [TestFixture]
    public class StaticNonlinear_Get : CsiGet
    {
        
        public void GetInitialCase(string name,
            ref string initialCase)
        {
          
        }
        
        public void GetLoads(string name,
            ref eLoadType[] loadTypes,
            ref string[] loadNames,
            ref double[] scaleFactor)
        {
          
        }
        
        
        public void GetMassSource(string name,
            ref string sourceName)
        {
          
        }

        
        
        public void GetModalCase(string name,
            ref string modalCase)
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
        
        
        public void GetTargetForceParameters(string name,
            ref double convergenceTolerance,
            ref int maxIterations,
            ref double accelerationFactor,
            ref bool continueIfNoConvergence)
        {
          
        }

        
        public void GetHingeUnloading(string name,
            ref eHingeUnloadingType hingeUnloadingType)
        {
          
        }

        
        public void GetResultsSaved(string name,
            ref bool saveMultipleSteps,
            ref int minSavedStates,
            ref int maxSavedStates,
            ref bool savePositiveDisplacementIncrementsOnly)
        {
          
        }

        
        public void GetLoadApplication(string name,
            ref eLoadControl loadControl,
            ref eLoadControlDisplacement controlDisplacementType,
            ref double targetDisplacement,
            ref eMonitoredDisplacementType monitoredDisplacementType,
            ref eDegreeOfFreedom degreeOfFreedom,
            ref string namePoint,
            ref string nameGeneralizedDisplacement)
        {
          
        }
    }
    
    [TestFixture]
    public class StaticNonlinear_Set : CsiSet
    {
        
        public void SetCase(string name)
        {
          
        }

        
        public void SetInitialCase(string name,
            string initialCase)
        {
          
        }
        
        
        public void SetLoads(string name,
            eLoadType[] loadTypes,
            string[] loadNames,
            double[] scaleFactor)
        {
          
        }

        
        public void SetMassSource(string name,
            string sourceName)
        {
          
        }

        
        public void SetModalCase(string name,
            string modalCase)
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

        
        public void SetTargetForceParameters(string name,
            double convergenceTolerance,
            int maxIterations,
            double accelerationFactor,
            bool continueIfNoConvergence)
        {
          
        }

        
        public void SetHingeUnloading(string name,
            eHingeUnloadingType hingeUnloadingType)
        {
          
        }

        
        public void SetResultsSaved(string name,
            bool saveMultipleSteps,
            int minSavedStates = 10,
            int maxSavedStates = 100,
            bool savePositiveDisplacementIncrementsOnly = true)
        {
          
        }

        
        public void SetLoadApplication(string name,
            eLoadControl loadControl,
            eLoadControlDisplacement controlDisplacementType,
            double targetDisplacement,
            eMonitoredDisplacementType monitoredDisplacementType,
            eDegreeOfFreedom degreeOfFreedom,
            string namePoint,
            string nameGeneralizedDisplacement)
        {
          
        }
    }
}
