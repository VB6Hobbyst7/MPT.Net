using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase;
using MPT.CSI.API.Core.Support;


namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadCase
{
    [TestFixture]
    public class ResponseSpectrum_Get : CsiGet
    {        
        public void GetLoads(string name,
            ref eDegreeOfFreedom[] loadDirections,
            ref string[] functions,
            ref double[] scaleFactor,
            ref string[] coordinateSystems,
            ref double[] angles)
        {
          
        }

        
        public void GetModalCase(string name,
            ref string modalCase)
        {
          
        }

        
        public void GetModalCombination(string name,
            ref eModalCombination modalCombination,
            ref double gmcF1,
            ref double gmcF2,
            ref ePeriodicPlusRigidModalCombination periodicPlusRigidModalCombination,
            ref double td)
        {
          
        }

        
        public void GetDirectionalCombination(string name,
            ref eDirectionalCombination directionalCombination,
            ref double scaleFactor)
        {
          
        }

        
        public void GetEccentricity(string name,
            ref double eccentricity)
        {
          
        }

        
        public void GetDiaphragmEccentricityOverride(string name,
            ref string[] diaphragms,
            ref double[] eccentricities)
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
    }
    
    [TestFixture]
    public class ResponseSpectrum_Set : CsiSet
    {
      
        public void SetCase(string name)
        {
          
        }
        
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        public void SetLoads(string name,
            eDegreeOfFreedom[] loadDirections,
            string[] functions,
            double[] scaleFactor,
            string[] coordinateSystems,
            double[] angles)
        {
          
        }

        
        public void SetModalCase(string name,
            string modalCase)
        {
          
        }

        
        public void SetModalCombination(string name,
            eModalCombination modalCombination,
            double gmcF1,
            double gmcF2,
            ePeriodicPlusRigidModalCombination periodicPlusRigidModalCombination,
            double td)
        {
          
        }
        
        
        public void SetDirectionalCombination(string name,
            eDirectionalCombination directionalCombination,
            double scaleFactor)
        {
          
        }

        
        public void SetEccentricity(string name,
            double eccentricity)
        {
          
        }

        
        public void SetDiaphragmEccentricityOverride(string name,
            string diaphragm,
            double eccentricities,
            bool delete = false)
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
