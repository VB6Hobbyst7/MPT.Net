#if BUILD_ETABS2016 || BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Property
{
    [TestFixture]
    public class LineSpring_Get : CsiGet
    {
        
        public void GetNameList(ref string[] names)
        {
          
        }

        
        public void GetLineSpringProperties(string name,
            ref double U1,
            ref double U2,
            ref double U3,
            ref double R1,
            ref eLinkDirection nonlinearOption2,
            ref eLinkDirection nonlinearOption3,
            ref int color,
            ref string notes,
            ref string GUID)
        {
          
        }
    }
    
    [TestFixture]
    public class LineSpring_Set : CsiSet
    {
      
        public void ChangeName(string nameExisting,
            string nameNew)
        {
          
        }


        
        public void Delete(string name)
        {
          
        }

        
        public void SetLineSpringProperties(string name,
            double U1,
            double U2,
            double U3,
            double R1,
            eLinkDirection nonlinearOption2,
            eLinkDirection nonlinearOption3,
            int color = 0,
            string notes = "",
            string GUID = "")
        {
          
        }
    }
}
#endif