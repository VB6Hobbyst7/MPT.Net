#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.TimeDependent;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.TimeDependent.CodesTendon;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Property.TimeDependent.CodesTendon
{
    [TestFixture]
    public class CEB_FIP_90_Get : CsiGet
    {
      
        public void GetMethod(string name,
            ref bool considerSteelRelaxation,
            ref eCEBFIP90Class classification,
            ref eIntegrationType integrationType,
            ref int numberSeriesTerms,
            double temperature = 0)
        {
          
        }
    }
    
    [TestFixture]
    public class CEB_FIP_90_Set : CsiSet
    {

        public void SetMethod(string name,
            bool considerSteelRelaxation,
            eCEBFIP90Class classification,
            eIntegrationType integrationType,
            int numberSeriesTerms,
            double temperature = 0)
        {
          
        }
    }
}
#endif