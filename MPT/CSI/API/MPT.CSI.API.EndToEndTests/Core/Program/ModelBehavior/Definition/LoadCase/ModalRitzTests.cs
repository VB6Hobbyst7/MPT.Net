#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Support;


namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadCase
{
    [TestFixture]
    public class ModalRitz_Get : CsiGet
    {

        
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
            ref int[] maxNumberGenerationCycles,
            ref double[] targetDynamicParticipationRatio)
        {
          
        }
    }
    
    [TestFixture]
    public class ModalRitz_Set : CsiSet
    {

        public void SetCase(string name)
        {
          
        }

        
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
            int[] maxNumberGenerationCycles,
            double[] targetDynamicParticipationRatio)
        {
          
        }
    }
}
#endif