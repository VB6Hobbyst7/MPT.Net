#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior
{
    [TestFixture]
    public class Options_Get : CsiGet
    {
        
        
        public void GetDimensions(ref double cuttingPlaneTol,
            ref double worldSpacing,
            ref double nudgeValue,
            ref int pixelClickSize,
            ref int pixelSnapSize,
            ref int screenLineThickness,
            ref int printLineThickness,
            ref int maxFont,
            ref int minFont,
            ref int zoomStep,
            ref int shrinkFactor,
            ref int textFileMaxChar)
        {
          
        }

    }
    
    [TestFixture]
    public class Options_Set : CsiSet
    {
       
       
        public void SetDimensions(double cuttingPlaneTol,
            double worldSpacing,
            double nudgeValue,
            int pixelClickSize,
            int pixelSnapSize,
            int screenLineThickness,
            int printLineThickness,
            int maxFont,
            int minFont,
            int zoomStep,
            int shrinkFactor,
            int textFileMaxChar)
        {
          
        }
    }
}
#endif