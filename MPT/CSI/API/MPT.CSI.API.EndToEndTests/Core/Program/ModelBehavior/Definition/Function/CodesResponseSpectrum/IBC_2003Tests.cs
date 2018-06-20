#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    [TestFixture]
    public class IBC_2003_Get : CsiGet
    {
      
        public void GetFunction(string name,
            ref double Sds,
            ref double Sd1,
            ref double dampingRatio)
        {
          
        }
    }
    
    [TestFixture]
    public class IBC_2003_Set : CsiSet
    {

        
        public void SetFunction(string name,
            double Sds,
            double Sd1,
            double dampingRatio)
        {
          
        }
    }
}
#endif