#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadPattern
{
    [TestFixture]
    public class AutoSeismic_Get : CsiGet
    {

        public void GetAutoCode(string name,
            ref string codeName)
        {
          
        }
        
        
        public void GetDiaphragmEccentricityOverride(string patternName,
            ref string[] diaphragmNames,
            ref double[] eccentricities)
        {
          
        }
    }
    
    [TestFixture]
    public class AutoSeismic_Set : CsiSet
    {


        public void SetAutoCode(AutoLoad autoLoad)
        {
          
        }
        
        
        public void SetAutoCode(AutoSeismicLoad autoSeismicLoad)
        {
          
        }

        
        public void SetNone(string name)
        {
          
        }

        
        public void SetDiaphragmEccentricityOverride(string patternName,
            string diaphragmName,
            double eccentricity,
            bool delete = false)
        {
          
        }
    }
}
#endif
