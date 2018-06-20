using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition
{
    [TestFixture]
    public class LoadCases_Get : CsiGet
    {
        
        public void Count()
        {
          
        }

        
        public void Count(eLoadCaseType caseType)
        {
          
        }

        
        public void GetNameList(ref string[] namesOfLoadCaseType)
        {
          
        }

        
        public void GetNameList(ref int numberNames,
            ref string[] namesOfLoadCaseType,
            eLoadCaseType caseType)
        {
          
        }

        
        public void GetCaseTypes(string nameLoadCase,
            ref eLoadCaseType loadCaseType,
            ref int loadCaseSubType,
            ref eLoadPatternType designType,
            ref eSpecificationSource designTypeOption,
            ref eAutoCreatedCase autoCreatedCase)
        {
          
        }
    }
    
    [TestFixture]
    public class LoadCases_Set : CsiSet
    {
      
        public void ChangeName(string loadCaseName,
            string newName)
        {
          
        }

        
        public void Delete(string loadCaseName)
        {
          
        }

        
        public void SetDesignType(string nameLoadCase,
            eSpecificationSource designTypeOption,
            eLoadPatternType designType = eLoadPatternType.Dead)
        {
          
        }
    }
}
