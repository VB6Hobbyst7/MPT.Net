#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Function
{
    [TestFixture]
    public class SteadyState_Get : CsiGet
    {
      
        public void GetFromFile(string name,
            ref string fileName,
            ref int headerLinesSkip,
            ref int prefixCharactersSkip,
            ref int pointsPerLine,
            ref eFileValueType valueType,
            ref bool freeFormat,
            ref int numberFixed,
            ref eFrequencyType frequencyTypeInFile)
        {
          
        }

        
        public void GetUser(string name,
            ref double[] frequencies,
            ref double[] values)
        {
          
        }
    }
    
    [TestFixture]
    public class SteadyState_Set : CsiSet
    {

        
        public void SetFromFile(string name,
            string fileName,
            int headerLinesSkip,
            int prefixCharactersSkip,
            int pointsPerLine,
            eFileValueType valueType,
            bool freeFormat,
            int numberFixed = 10,
            eFrequencyType frequencyTypeInFile = eFrequencyType.HZ)
        {
          
        }

        
        public void SetUser(string name,
            double[] frequencies,
            double[] values)
        {
          
        }
    }
}
#endif
