#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.TimeDependent.CodesTendon;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Property.TimeDependent
{
    [TestFixture]
    public class TimeDependentTendon_Get : CsiGet
    {
      
        public void GetScaleFactors(string name,
            ref double scaleFactorRelaxation,
            double temperature = 0)
        {
          
        }
    }
    
    [TestFixture]
    public class TimeDependentTendon_Set : CsiSet
    {
      
        public void SetMethod(TimeDependentTendonMethod method)
        {
          
        }

        public void SetScaleFactors(string name,
            double scaleFactorRelaxation,
            double temperature = 0)
        {
          
        }
    }
}
#endif