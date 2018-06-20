#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    [TestFixture]
    public class UBC_94_Get : CsiGet
    {
      
        public void GetFunction(string name,
            double Z,
            int S,
            double dampingRatio)
        {
          
        }
    }
    
    [TestFixture]
    public class UBC_94_Set : CsiSet
    {
        
        
        public void SetFunction(string name,
            double Z,
            int S,
            double dampingRatio)
        {
          
        }
    }
}
#endif