#if BUILD_ETABS2016 || BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Property
{
    [TestFixture]
    public class AreaSpring_Get : CsiGet
    {

        public void GetNameList(ref string[] names)
        {
          
        }
        
        
        public void GetAreaSpringProperties(string name,
            ref double U1,
            ref double U2,
            ref double U3,
            ref eLinkDirection nonlinearOption3,
            ref int springOption,
            ref string soilProfile,
            ref double endLengthRatio,
            ref double period,
            ref int color,
            ref string notes,
            ref string GUID)
        {
          
        }
    }
    
    [TestFixture]
    public class AreaSpring_Set : CsiSet
    {
      
        public void ChangeName(string nameExisting,
            string nameNew)
        {
          
        }
        
        
        public void Delete(string name)
        {
          
        }
        
       
        public void SetAreaSpringProperties(string name,
            double U1,
            double U2,
            double U3,
            eLinkDirection nonlinearOption3,
            int springOption = 1,
            string soilProfile = "",
            double endLengthRatio = 0,
            double period = 0,
            int color = 0,
            string notes = "",
            string GUID = "")
        {
          
        }
    }
}
#endif