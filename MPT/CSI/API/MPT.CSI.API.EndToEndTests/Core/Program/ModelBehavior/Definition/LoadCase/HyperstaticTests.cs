#if !BUILD_ETABS2015
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadCase
{
    [TestFixture]
    public class Hyperstatic_Get : CsiGet
    {
      
        public void GetBaseCase(string name,
            ref string nameBaseCase)
        {
          
        }
    }
    
    [TestFixture]
    public class Hyperstatic_Set : CsiSet
    {

        
        public void SetBaseCase(string name,
            string nameBaseCase)
        {
          
        }

        
        public void SetCase(string name)
        {
          
        }
    }
}
#endif