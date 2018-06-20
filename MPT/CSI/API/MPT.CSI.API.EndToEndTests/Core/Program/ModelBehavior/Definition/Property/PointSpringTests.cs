#if BUILD_ETABS2016 || BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Property
{
    [TestFixture]
    public class PointSpring_Get : CsiGet
    {

        
        public void GetNameList(ref string[] names)
        {
          
        }

        
        public void GetPointSpringProperties(string name,
            ref eSpringStiffnessDerivation stiffnessSource,
            ref Stiffness effectiveStiffness,
            ref string coordinateSystem,
            ref string soilProfile,
            ref string footing,
            ref double period,
            ref int color,
            ref string notes,
            ref string GUID)
        {
          
        }

        
        public void GetLinks(string name,
                ref string[] linkNames,
                ref eAxialDirections[] linkAxialDirections,
                ref double[] linkAngles)
        {
          
        }
    }
    
    [TestFixture]
    public class PointSpring_Set : CsiSet
    {
      
        public void ChangeName(string nameExisting,
            string nameNew)
        {
          
        }

        
        public void Delete(string name)
        {
          
        }

        
        public void SetPointSpringProperties(string name,
            eSpringStiffnessDerivation stiffnessSource,
            Stiffness effectiveStiffness,
            string coordinateSystem = CoordinateSystems.Global,
            string soilProfile = "",
            string footing = "",
            double period = 0,
            int color = 0,
            string notes = "",
            string GUID = "")
        {
          
        }

        
        public void SetLinks(string name,
                ref string[] linkNames,
                ref eAxialDirections[] linkAxialDirections,
                ref double[] linkAngles)
        {
          
        }
    }
}
#endif