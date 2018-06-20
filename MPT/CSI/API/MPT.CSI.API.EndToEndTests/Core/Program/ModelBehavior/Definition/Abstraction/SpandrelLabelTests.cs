#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Abstraction
{
    [TestFixture]
    public class SpandrelLabel_Get : CsiGet
    {

        public void GetNameList(ref string[] names)
        {
          
        }

        
        public void GetNameList(ref string[] names,
            ref bool[] isMultiStory)
        {
          
        }

        
        public void GetSpandrel(string name,
            ref bool isMultiStory)
        {
          
        }
    }
    
    [TestFixture]
    public class SpandrelLabel_Set : CsiSet
    {
      
        public void ChangeName(string nameExisting,
            string nameNew)
        {
          
        }

        
        public void Delete(string name)
        {
          
        }

        
        public void SetSpandrel(string name,
            bool isMultiStory)
        {
          
        }
    }
}
#endif