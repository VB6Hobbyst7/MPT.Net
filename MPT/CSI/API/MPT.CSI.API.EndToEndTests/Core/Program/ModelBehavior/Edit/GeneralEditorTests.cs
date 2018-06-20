using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using System.Linq;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.Edit;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Edit
{
    [TestFixture]
    public class GeneralEditor
    {
      
        public void Move(Coordinate3DCartesian offsets)
        {
          
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
       
       
        public void ExtrudeAreaToSolidLinearNormal(string name,
            string sectionPropertyName,
            int numberPositive3,
            double thicknessPositive3,
            int numberNegative3,
            double thicknessNegative3,
            int numberOfSolids,
            ref string[] solidNames,
            bool remove = true)
        {
          
        }

        
        public void ExtrudeAreaToSolidLinearUser(string name,
            string sectionPropertyName,
            Coordinate3DCartesian offsets,
            int numberOfIncrements,
            int numberOfSolids,
            ref string[] solidNames,
            bool remove = true)
        {
          
        }

      
      
        public void ExtrudeAreaToSolidRadial(string name,
            string sectionPropertyName,
            eAxisExtrusion rotateAxis,
            Coordinate3DCartesian radialExtrusionCenter,
            double incrementAngle,
            double totalRise,
            int numberOfAngleIncrements,
            ref int numberOfSolids,
            ref string[] solidNames,
            bool remove = true)
        {
          
        }

        
        public void ExtrudeFrameToAreaLinear(string name,
            string sectionPropertyName,
            Coordinate3DCartesian offsets,
            int numberOfAreas,
            ref string[] areaNames,
            bool remove = true)
        {
          
        }

        
        public void ExtrudeFrameToAreaRadial(string name,
            string sectionPropertyName,
            eAxisExtrusion rotateAxis,
            Coordinate3DCartesian radialExtrusionCenter,
            double incrementAngle,
            double totalRise,
            int numberOfAreas,
            ref string[] areaNames,
            bool remove = true)
        {
          
        }

        
        public void ExtrudePointToFrameLinear(string name,
            string sectionPropertyName,
            Coordinate3DCartesian offsets,
            int numberOfFrames,
            ref string[] frameNames)
        {
          
        }

        
        public void ExtrudePointToFrameRadial(string name,
            string sectionPropertyName,
            eAxisExtrusion rotateAxis,
            Coordinate3DCartesian radialExtrusionCenter,
            double incrementAngle,
            double totalRise,
            int numberOfFrames,
            ref string[] frameNames)
        {
          
        }

        
        public void ReplicateLinear(Coordinate3DCartesian offsets,
            int numberReplication,
            ref int numberOfObjects,
            ref string[] objectNames,
            ref eObjectType[] objectTypes,
            bool remove = false)
        {
          
        }

        
        public void ReplicateMirror(eAxisOrientation planeAxis,
            Coordinate3DCartesian planeAxisCoordinate1,
            Coordinate3DCartesian planeAxisCoordinate2, 
            Coordinate3DCartesian planeCoordinate,
            ref int numberOfObjects,
            ref string[] objectNames,
            ref eObjectType[] objectTypes,
            bool remove = false)
        {
          
        }

        
        public void ReplicateRadial(eAxisOrientation rotationAxis,
            Coordinate3DCartesian axisCoordinate,
            Coordinate3DCartesian axis3DCoordinate,
            int numberReplication,
            double incrementAngle,
            ref int numberOfObjects,
            ref string[] objectNames,
            ref eObjectType[] objectTypes,
            bool remove = false)
        {
          
        }
#endif
    }
}
