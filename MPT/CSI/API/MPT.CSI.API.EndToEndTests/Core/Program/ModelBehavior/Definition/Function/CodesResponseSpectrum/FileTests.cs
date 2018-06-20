#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    [TestFixture]
    public class File_Get : CsiGet
    {
      
        public void GetFunction(string name,
            ref string fileName,
            ref int headerLinesSkip,
            ref double dampingRatio,
            ref eFileValueType valueType)
        {
          
        }
    }
    
    [TestFixture]
    public class File_Set : CsiSet
    {
        
        public void SetFunction(string name,
            string fileName,
            int headerLinesSkip,
            double dampingRatio,
            eFileValueType valueType)
        {
          
        }
    }
}
#endif