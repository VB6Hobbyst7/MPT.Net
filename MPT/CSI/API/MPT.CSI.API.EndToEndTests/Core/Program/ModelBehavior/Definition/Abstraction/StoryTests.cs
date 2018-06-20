#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Abstraction
{
    [TestFixture]
    public class Story_Get : CsiGet
    {
      
        public void GetNameList(ref string[] names)
        {
          
        }

        
        public void GetGUID(string name,
            ref string GUID)
        {
          
        }

        
        public void GetElevation(string name,
            ref double elevation)
        {
          
        }

        
        public void GetHeight(string name,
            ref double height)
        {
          
        }
        
        
        public void GetMasterStory(string name,
            ref bool isMasterStory)
        {
          
        }

        
        public void GetSimilarTo(string name,
            ref bool isMasterStory,
            ref string similarToStory)
        {
          
        }

        
        public void GetSplice(string name,
            ref bool spliceAbove,
            ref double spliceHeight)
        {
          
        }

        
        public void GetStories(ref string[] storyNames,
            ref double[] storyElevations,
            ref double[] storyHeights,
            ref bool[] isMasterStory,
            ref string[] similarToStory,
            ref bool[] spliceAbove,
            ref double[] spliceHeights)
        {
          
        }
    }
    
    [TestFixture]
    public class Story_Set : CsiSet
    {

        
        public void SetGUID(string name,
            string GUID = "")
        {
          
        }

        
        public void SetElevation(string name,
            double elevation)
        {
          
        }

        
        public void SetHeight(string name,
            double height)
        {
          
        }

        
        public void SetMasterStory(string name,
            bool isMasterStory)
        {
          
        }

        
        public void SetSimilarTo(string name,
            string similarToStory)
        {
          
        }

        
        public void SetSplice(string name,
            bool spliceAbove,
            double spliceHeight)
        {
          
        }

        
        public void SetStories(string[] storyNames,
            double[] storyElevations,
            double[] storyHeights,
            bool[] isMasterStory,
            string[] similarToStory,
            bool[] spliceAbove,
            double[] spliceHeights)
        {
          
        }
    }
}
#endif