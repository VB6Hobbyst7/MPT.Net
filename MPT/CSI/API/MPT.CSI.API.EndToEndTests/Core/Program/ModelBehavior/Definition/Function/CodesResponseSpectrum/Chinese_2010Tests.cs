#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    [TestFixture]
    public class Chinese_2010_Get : CsiGet
    {
      
        public void GetFunction(string name,
            ref double alphaMax,
            ref eSeismicIntensity_Chinese_2010 SI,
            ref double Tg,
            ref double PTDF,
            ref double dampingRatio)
        {
          
        }
    }
    
    [TestFixture]
    public class Chinese_2010_Set : CsiSet
    {
      
        public void SetFunction(string name,
            double alphaMax,
            eSeismicIntensity_Chinese_2010 SI,
            double Tg,
            double PTDF,
            double dampingRatio)
        {
          
        }
    }
}
#endif