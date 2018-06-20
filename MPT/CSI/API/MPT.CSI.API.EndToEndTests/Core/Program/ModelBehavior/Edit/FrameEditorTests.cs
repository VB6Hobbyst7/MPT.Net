#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Edit
{
    [TestFixture]
    public class FrameEditor
    {
       
        public void DivideAtDistance(string name, 
            double divideDistance,
            bool fromIEnd,
            ref string[] newName)
        {
          
        }

       
       
        public void DivideAtIntersections(string name,
            ref int numberOfObjects,
            ref string[] newName)
        {
          
        }

        
        public void DivideByRatio(string name,
            int numberOfObjects,
            double ratioFirstLast,
            ref string[] newName)
        {
          
        }

    
    
        public void Extend(string name,
            bool IEnd,
            bool JEnd,
            string extendLine1,
            string extendline2 = "")
        {
          
        }

     
     
        public void Join(string name1,
            string name2)
        {
          
        }

      
      
        public void Trim(string name,
            bool IEnd,
            bool JEnd,
            string trimLine1,
            string trimline2 = "")
        {
          
        }

   
   
        public void ChangeConnectivity(string name,
            string point1,
            string point2)
        {
          
        }
    }
}
#endif