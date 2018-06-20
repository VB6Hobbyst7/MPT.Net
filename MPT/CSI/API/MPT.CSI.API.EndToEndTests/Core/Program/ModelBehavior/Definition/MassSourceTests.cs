#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition
{
    [TestFixture]
    public class MassSource_Get : CsiGet
    {
        
        
        public void Count()
        {
          
        }
        
        
        public void GetNameList(ref string[] namesMassSource)
        {
          
        }
        
        
        public void GetDefault(ref string nameMassSource)
        {
          
        }

        
        public void GetMassSource(string nameMassSource,
            ref bool massFromElements,
            ref bool massFromMasses,
            ref bool massFromLoads,
            ref bool isDefault,
            ref string[] namesLoadPatterns,
            ref double[] scaleFactors)
        {
          
        }
    }
    
    [TestFixture]
    public class MassSource_Set : CsiSet
    {

        public void ChangeName(string nameMassSource,
            string newName)
        {
          
        }

        
        public void Delete(string nameMassSource)
        {
          
        }

        
        public void SetDefault(string nameMassSource)
        {
          
        }

        
        public void SetMassSource(string nameMassSource,
            bool massFromElements,
            bool massFromMasses,
            bool massFromLoads,
            bool isDefault,
            string[] namesLoadPatterns,
            double[] scaleFactors)
        {
          
        }
    }
}
#endif