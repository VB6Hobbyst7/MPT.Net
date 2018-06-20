#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Abstraction
{
    [TestFixture]
    public class Tower_Get : CsiGet
    {
        public void GetNameList(ref string[] names)
        {
          
        }

        
        public void GetActiveTower(ref string towerName)
        {
                      
        }
    }
    
    [TestFixture]
    public class Tower_Set : CsiSet
    {
        public void ChangeName(string nameExisting,
            string nameNew)
        {
     
        }
        
        public void Delete(string name)
        {
          
        }
        
        public void Delete(string name,
            bool associate,
            string associateWithTower = "")
        {
          
        }
        
        
        public void AddCopyOfTower(string towerName,
            string newTowerName)
        {
          
        }

        
        public void AddNewTower(string towerName,
            int numberOfStories,
            double typicalStoryHeight,
            double bottomStoryHeight)
        {
          
        }

        
        public void AllowMultipleTowers(bool allowMultipleTowers,
            string retainedTower = "",
            bool combine = true)
        {
          
        }
        
        
        public void SetActiveTower(string towerName)
        {
          
        }
    }
}
#endif