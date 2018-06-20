using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase;
using MPT.CSI.API.Core.Support;


namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadCase
{
    [TestFixture]
    public class TimeHistoryModalNonlinear_Get : CsiGet
    {
#if !BUILD_ETABS2015

        public void GetLoads(string name,
            ref eLoadType[] loadTypes,
            ref string[] loadNames,
            ref string[] functions,
            ref double[] scaleFactor,
            ref double[] timeFactor,
            ref double[] arrivalTime,
            ref string[] coordinateSystems,
            ref double[] angles)
        {
          
        }
#endif
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        
        public void GetInitialCase(string name,
            ref string initialCase)
        {
          
        }

        
        public void GetTimeStep(string name,
            ref int numberOfOutputSteps,
            ref double timeStepSize)
        {
          
        }

        
        public void GetModalCase(string name,
            ref string modalCase)
        {
          
        }

        
        public void GetSolutionControlParameters(string name,
            ref double staticPeriod,
            ref double maxSubstepSize,
            ref double minSubstepSize,
            ref double relativeForceConvergenceTolerance,
            ref double relativeEnergyConvergenceTolerance,
            ref int maxIterationLimit,
            ref int minIterationLimit,
            ref double convergenceFactor)
        {
          
        }
        
        
        public void GetDampingType(string name,
            ref eDampingType dampingType)
        {
          
        }
        
        
        public void GetDampingConstant(string name,
            ref double damping)
        {
          
        }
        

        
        public void GetDampingInterpolated(string name,
            ref eDampingTypeInterpolated dampingType,
            ref double[] periodsOrFrequencies,
            ref double[] damping)
        {
          
        }

        
        public void GetDampingProportional(string name,
            ref eDampingTypeProportional dampingType,
            ref double massProportionalDampingCoefficient,
            ref double stiffnessProportionalDampingCoefficient,
            ref double periodOrFrequencyPt1,
            ref double periodOrFrequencyPt2,
            ref double dampingPt1,
            ref double dampingPt2)
        {
          
        }

        
        public void GetDampingOverrides(string name,
            ref int[] modes,
            ref double[] damping)
        {
          
        }
#endif
    }
    
    [TestFixture]
    public class TimeHistoryModalNonlinear_Set : CsiSet
    {
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

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
            string[] functions,
            double[] scaleFactor,
            double[] timeFactor,
            double[] arrivalTime,
            string[] coordinateSystems,
            double[] angles)
        {
          
        }

        
        public void SetTimeStep(string name,
            int numberOfOutputSteps,
            double timeStepSize)
        {
          
        }

        
        public void SetModalCase(string name,
            string modalCase)
        {
          
        }

        
        public void SetSolutionControlParameters(string name,
            double staticPeriod,
            double maxSubstepSize,
            double minSubstepSize,
            double relativeForceConvergenceTolerance,
            double relativeEnergyConvergenceTolerance,
            int maxIterationLimit,
            int minIterationLimit,
            double convergenceFactor)
        {
          
        }
        
        
        public void SetDampingConstant(string name,
            double damping)
        {
          
        }

        
        public void SetDampingInterpolated(string name,
            eDampingTypeInterpolated dampingType,
            double[] periodsOrFrequencies,
            double[] damping)
        {
          
        }

        
        public void SetDampingProportional(string name,
            eDampingTypeProportional dampingType,
            double massProportionalDampingCoefficient,
            double stiffnessProportionalDampingCoefficient,
            double periodOrFrequencyPt1,
            double periodOrFrequencyPt2,
            double dampingPt1,
            double dampingPt2)
        {
          
        }
        
        
        public void SetDampingOverrides(string name,
            int[] modes,
            double[] damping)
        {
          
        }
#endif
    }
}
