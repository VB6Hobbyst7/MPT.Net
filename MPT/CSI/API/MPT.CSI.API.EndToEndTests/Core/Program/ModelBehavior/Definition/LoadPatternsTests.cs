using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Support;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition
{
    [TestFixture]
    public class LoadPatterns_Get : CsiGet
    {

        
        public void Count()
        {
          
        }

        
        public void GetNameList(ref string[] loadPatternNames)
        {
          
        }

        
        public void GetLoadType(string name,
            ref eLoadPatternType loadPatternType)
        {
          
        }

        
        public void GetSelfWtMultiplier(string name,
            ref double selfWeightMultiplier)
        {
          
        }

        
        public void GetAutoSeismicCode(string name, ref string codeName)
        {
          
        }

        
        public void GetAutoWindCode(string name, ref string codeName)
        {
          
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        public void GetAutoWaveCode(string name, ref string codeName)
        {
          
        }
#endif
    }
    
    [TestFixture]
    public class LoadPatterns_Set : CsiSet
    {
      
        public void Add(string name,
            eLoadPatternType loadPatternType,
            double selfWeightMultiplier = 0,
            bool addLoadCase = true)
        {
          
            
        }

        
        public void Delete(string name)
        {
          
        }

        
        public void ChangeName(string name,
            string newName)
        {
          
        }

        
        public void SetLoadType(string name,
            eLoadPatternType loadPatternType)
        {
          
        }

        
        public void SetSelfWtMultiplier(string name,
            double selfWeightMultiplier)
        {
          
        }
    }
}
