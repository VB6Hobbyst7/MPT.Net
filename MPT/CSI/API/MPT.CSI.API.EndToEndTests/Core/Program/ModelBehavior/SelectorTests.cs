using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior
{
    [TestFixture]
    public class Selector_Get : CsiGet
    {
      
        public void All(bool deselect = false)
        {
          
        }

        
        public void ClearSelection()
        {
          
        }

        public void Group(string name,
            bool deselect = false)
        {
          
        }
        
#if !BUILD_ETABS2015

        public void InvertSelection()
        {
          
        }

        
        public void PreviousSelection()
        {
          
        }

        
        public void GetSelected(ref int numberItems,
            ref int[] objectType,
            ref string[] objectName)
        {
          
        }
#endif
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        public void Constraint(string name,
            bool deselect = false)
        {
          
        }
        
        
        public void PropertyArea(string name,
            bool deselect = false)
        {
          
        }

        
        public void PropertyCable(string name,
            bool deselect = false)
        {
          
        }

        
        public void PropertyFrame(string name,
            bool deselect = false)
        {
          
        }

        
        public void PropertyLink(string name,
            bool deselect = false)
        {
          
        }

        
        public void PropertyLinkFrequencyDependent(string name,
            bool deselect = false)
        {
          
        }

        
        public void PropertyMaterial(string name,
            bool deselect = false)
        {
          
        }

        
        public void PropertySolid(string name,
            bool deselect = false)
        {
          
        }

        
        public void PropertyTendon(string name,
            bool deselect = false)
        {
          
        }

        
        public void LinesParallelToCoordAxis(ref bool[] parallelTo,
            string coordinateSystem = CoordinateSystems.Global,
            double tolerance = 0.057,
            bool deselect = false)
        {
          
        }

        
        public void LinesParallelToLine(string name,
            bool deselect = false)
        {
          
        }

        
        public void PlaneXY(string name,
            bool deselect = false)
        {
          
        }

        
        public void PlaneXZ(string name,
            bool deselect = false)
        {
          
        }

        
        public void PlaneYZ(string name,
            bool deselect = false)
        {
          
        }

        
        public void SupportedPoints(ref bool[] DOF,
            string coordinateSystem = "Local",
            bool deselect = false,
            bool selectRestraints = true,
            bool selectJointSprings = true,
            bool selectLineSprings = true,
            bool selectAreaSprings = true,
            bool selectSolidSprings = true,
            bool selectOneJointLinks = true)
        {
          
        }
#endif
    }
}
