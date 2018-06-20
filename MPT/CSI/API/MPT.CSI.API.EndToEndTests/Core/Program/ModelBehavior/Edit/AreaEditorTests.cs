#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Edit
{
    [TestFixture]
    public class AreaEditor
    {
        
        
        public void Divide(string name,
            eMeshType meshType,
            ref int numberOfAreas,
            ref string[] areaNames,
            int numberOfObjectsAlongPoint12 = 2,
            int numberOfObjectsAlongPoint13 = 2,
            double maxSizeOfObjectsAlongPoint12 = 0,
            double maxSizeOfObjectsAlongPoint13 = 0,
            bool pointOnEdgeFromGrid = false,
            bool pointOnEdgeFromLine = false,
            bool pointOnEdgeFromPoint = false,
            bool extendCookieCutLines = false,
            double rotation = 0,
            double maxSizeGeneral = 0,
            bool localAxesOnEdge = false,
            bool localAxesOnFace = false,
            bool restraintsOnEdge = false,
            bool restraintsOnFace = false
            )
        {
          
        }
        
        public void ExpandShrink(eAreaOffsetType offsetType,
            double offset)
        {
          
        }
        
        public void Merge(ref int numberOfAreas,
            ref string[] areaNames)
        {
          
        }

        
        public void PointAdd()
        {
          
        }

        
        public void PointRemove()
        {
          
        }

        
        
        public void ChangeConnectivity(string name,
            ref string[] pointNames)
        {
          
        }
    }
}
#endif