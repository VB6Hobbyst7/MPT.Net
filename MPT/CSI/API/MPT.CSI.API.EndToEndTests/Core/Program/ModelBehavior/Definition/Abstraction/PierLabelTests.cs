#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Abstraction
{
    [TestFixture]
    public class PierLabel_Get : CsiGet
    {

        
        public void GetNameList(ref string[] names)
        {
          
        }

        
        public void GetPier(string name)
        {
          
        }
#if !BUILD_ETABS2015

        public void GetSectionProperties(string name,
            ref string[] storyNames,
            ref double[] axisAngles,
            ref int[] numberOfAreaObjects,
            ref int[] numberOfLineObjects,
            ref double[] widthBottom,
            ref double[] thicknessBottom,
            ref double[] widthTop,
            ref double[] thicknessTop,
            ref string[] materialPropertyNames,
            ref double[] centerOfGravityBottomX,
            ref double[] centerOfGravityBottomY,
            ref double[] centerOfGravityBotZ,
            ref double[] centerOfGravityTopX,
            ref double[] centerOfGravityTopY,
            ref double[] centerOfGravityTopZ)
        {
          
        }
#endif
    }
    
    [TestFixture]
    public class PierLabel_Set : CsiSet
    {
      
        public void ChangeName(string nameExisting,
            string nameNew)
        {
          
        }


        
        public void Delete(string name)
        {
          
        }
        
        
        public void SetPier(string name)
        {
          
        }
    }
}
#endif