#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.TimeDependent.CodesConcrete;
#endif
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Property.TimeDependent
{
    [TestFixture]
    public class TimeDependentConcrete_Get : CsiGet
    {

        public void GetScaleFactors(string name,
            double scaleFactorAge,
            double scaleFactorCreep,
            double scaleFactorShrinkage,
            double temperature = 0)
        {
          
        }
    }
    
    [TestFixture]
    public class TimeDependentConcrete_Set : CsiSet
    {
        
        public void SetMethod(TimeDependentConcreteMethod method)
        {
          
        }

        
        public void SetScaleFactors(string name,
            double scaleFactorAge,
            double scaleFactorCreep,
            double scaleFactorShrinkage,
            double temperature = 0)
        {
          
        }
    }
}
#endif