#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    [TestFixture]
    public class NBCC_1995_Get : CsiGet
    {
      
        public void GetFunction(string name,
            ref double ZVR,
            ref int ZA,
            ref int Zv,
            ref double dampingRatio)
        {
          
        }
    }
    
    [TestFixture]
    public class NBCC_1995_Set : CsiSet
    {
        
        
        public void SetFunction(string name,
            double ZVR,
            int ZA,
            int Zv,
            double dampingRatio)
        {
          
        }
    }
}
#endif