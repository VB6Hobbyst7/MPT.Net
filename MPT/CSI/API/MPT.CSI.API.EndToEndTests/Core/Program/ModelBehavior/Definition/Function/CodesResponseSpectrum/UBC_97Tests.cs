#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    [TestFixture]
    public class UBC_97_Get : CsiGet
    {
      
        public void GetFunction(string name,
            ref double Ca,
            ref double Cv,
            ref double dampingRatio)
        {
          
        }
    }
    
    [TestFixture]
    public class UBC_97_Set : CsiSet
    {
        
        
        public void SetFunction(string name,
            double Ca,
            double Cv,
            double dampingRatio)
        {
          
        }
    }
}
#endif