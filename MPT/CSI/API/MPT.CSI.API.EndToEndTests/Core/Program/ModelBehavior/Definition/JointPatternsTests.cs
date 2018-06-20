#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition
{
    [TestFixture]
    public class JointPatterns_Get : CsiGet
    {
      
        public void Count()
        {
          
        }

        
        
        public void GetNameList(ref string[] namesJointPatterns)
        {
          
        }
    }
    
    [TestFixture]
    public class JointPatterns_Set : CsiSet
    {
      
        public void ChangeName(string nameJointPattern,
            string newName)
        {
          
        }

        
        
        public void Delete(string nameJointPattern)
        {
          
        }

        
        
        public void SetPattern(string nameJointPattern)
        {
          
        }
    }
}
#endif