#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.TimeDependent;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Property.TimeDependent.CodesConcrete
{
    [TestFixture]
    public class CEB_FIP_90_Get : CsiGet
    {
      
        public void GetMethod(string name,
            ref bool considerConcreteAge,
            ref bool considerConcreteCreep,
            ref bool considerConcreteShrinkage,
            ref double CEBFIPsCoefficient,
            ref double relativeHumidity,
            ref double notionalSize,
            ref double shrinkageCoefficient,
            ref double shrinkageStartAge,
            ref eIntegrationType creepIntegrationType,
            ref int numberSeriesTerms,
            double temperature = 0)
        {
          
        }
    }
    
    
    [TestFixture]
    public class CEB_FIP_90_Set : CsiSet
    {
      

        public void SetMethod(string name,
            bool considerConcreteAge,
            bool considerConcreteCreep,
            bool considerConcreteShrinkage,
            double CEBFIPsCoefficient,
            double relativeHumidity,
            double notionalSize,
            double shrinkageCoefficient,
            double shrinkageStartAge,
            eIntegrationType creepIntegrationType,
            int numberSeriesTerms,
            double temperature = 0)
        {
          
        }
    }
}

#endif