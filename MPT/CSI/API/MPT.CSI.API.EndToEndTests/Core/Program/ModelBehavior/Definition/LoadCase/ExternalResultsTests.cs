#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadCase
{
    [TestFixture]
    public class ExternalResults_Set : CsiSet
    {
      
        public void SetCase(string name)
        {
          
        }

        
        public void SetNumberSteps(string name,
            int firstStep,
            int lastStep)
        {
          
        }
    }
}
#endif
