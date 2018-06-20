#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wave;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadPattern
{
    [TestFixture]
    public class AutoWave_Get : CsiGet
    {
      
        public void GetAutoCode(string name,
            ref string codeName)
        {
          
        }
    }
    
    [TestFixture]
    public class AutoWave_Set : CsiSet
    {
      
        
        public void SetAutoCode(AutoLoad autoLoad)
        {
          
        }


        
        public void SetAutoCode(AutoWaveLoad autoWaveLoad)
        {
          
        }

        
        public void SetNone(string name)
        {
          
        }
    }
}
#endif
