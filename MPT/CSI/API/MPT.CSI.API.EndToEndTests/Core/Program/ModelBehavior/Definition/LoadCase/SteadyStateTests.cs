#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase;
using MPT.CSI.API.Core.Support;


namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadCase
{
    [TestFixture]
    public class SteadyState_Get : CsiGet
    {

        public void GetInitialCase(string name,
            ref string initialCase)
        {
          
        }

        
        public void GetLoads(string name,
            ref eLoadType[] loadTypes,
            ref string[] loadNames,
            ref string[] functions,
            ref double[] scaleFactor,
            ref double[] phaseAngle,
            ref string[] coordinateSystems,
            ref double[] angles)
        {
          
        }

        
        public void GetFrequencyData(string name,
            ref double frequencyFirst,
            ref double frequencyLast,
            ref int numberOfFrequencies,
            ref bool addModalFrequencies,
            ref bool addSignedFractionalDeviations,
            ref bool addSpecifiedFrequencies,
            ref string modalCase,
            ref int numberSignedFractionalDeviations,
            ref double[] signedFractionalDeviations,
            ref int numberSpecifiedFrequencies,
            ref double[] specifiedFrequencies)
        {
          
        }

        
        public void GetDampingConstant(string name,
            ref double massProportionalDampingCoefficient,
            ref double stiffnessProportionalDampingCoefficient)
        {
          
        }

        
        public void GetDampingInterpolated(string name,
            ref eFrequencyType frequencyUnit,
            ref double[] frequencies,
            ref double[] massProportionalDampingCoefficients,
            ref double[] stiffnessProportionalDampingCoefficients)
        {
          
        }

        
        public void GetDampingType(string name,
            ref eDampingTypeHysteretic dampingType)
        {
          
        }
    }
    
    [TestFixture]
    public class SteadyState_Set : CsiSet
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
            string[] functions,
            double[] scaleFactor,
            double[] phaseAngle,
            string[] coordinateSystems,
            double[] angles)
        {
          
        }
        
    
        public void SetFrequencyData(string name,
            double frequencyFirst,
            double frequencyLast,
            int numberOfFrequencies,
            bool addModalFrequencies,
            bool addSignedFractionalDeviations,
            bool addSpecifiedFrequencies,
            string modalCase,
            double[] signedFractionalDeviations,
            double[] specifiedFrequencies)
        {
          
        }

        
        public void SetDampingConstant(string name,
            double massProportionalDampingCoefficient,
            double stiffnessProportionalDampingCoefficient)
        {
          
        }
        
        public void SetDampingInterpolated(string name,
            eFrequencyType frequencyUnit,
            double[] frequencies,
            double[] massProportionalDampingCoefficients,
            double[] stiffnessProportionalDampingCoefficients)
        {
          
        }
    }
}
#endif

