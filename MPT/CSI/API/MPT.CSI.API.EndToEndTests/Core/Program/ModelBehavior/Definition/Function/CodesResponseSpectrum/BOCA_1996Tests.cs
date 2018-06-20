#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    [TestFixture]
    public class BOCA_1996_Get : CsiGet
    {
      
        public void GetFunction(string name,
            ref double Aa,
            ref double Av,
            ref double S,
            ref double R,
            ref double dampingRatio)
        {
          
        }
    }
    
    [TestFixture]
    public class BOCA_1996_Set : CsiSet
    {
     
        
        public void SetFunction(string name,
            double Aa,
            double Av,
            double S,
            double R,
            double dampingRatio)
        {
          
        } 
    }
}
#endif