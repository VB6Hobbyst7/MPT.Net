#if !BUILD_ETABS2015
using System;
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Support;


namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadCase
{
    [TestFixture]
    public class ModalEigen_Get : CsiGet
    {
#if !BUILD_ETABS2016 && !BUILD_ETABS2017


        public void GetInitialCase(string name,
            ref string initialCase)
        {
          
        }

        
        public void GetNumberModes(string name,
            ref int maxNumberModes,
            ref int minNumberModes)
        {
          
        }

        
        public void GetLoads(string name,
            ref eLoadTypeModal[] loadTypes,
            ref string[] loadNames,
            ref double[] targetMassParticipationRatios,
            ref bool[] isStaticCorrectionModeCalculated)
        {
          
        }

        
        public void GetParameters(string name,
            ref double eigenvalueShiftFrequency,
            ref double cutoffFrequencyRadius,
            ref double convergenceTolerance,
            ref bool allowAutoFrequencyShifting)
        {
          
        }
#endif
    }
    
    [TestFixture]
    public class ModalEigen_Set : CsiSet
    {
      
        public void SetCase(string name)
        {
          
        }

#if !BUILD_ETABS2016 && !BUILD_ETABS2017
        
        
        public void SetInitialCase(string name,
            string initialCase)
        {
          
        }

        
        public void SetNumberModes(string name,
            int maxNumberModes,
            int minNumberModes)
        {
          
        }

        
        public void SetLoads(string name,
            eLoadTypeModal[] loadTypes,
            string[] loadNames,
            double[] targetMassParticipationRatios,
            bool[] isStaticCorrectionModeCalculated)
        {
          
        }

        
        public void SetParameters(string name,
            double eigenvalueShiftFrequency,
            double cutoffFrequencyRadius,
            double convergenceTolerance,
            bool allowAutoFrequencyShifting)
        {

        }
#endif
    }
}
#endif