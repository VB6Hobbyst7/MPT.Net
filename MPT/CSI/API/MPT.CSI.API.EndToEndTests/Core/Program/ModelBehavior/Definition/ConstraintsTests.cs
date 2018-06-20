using NUnit.Framework;
using MPT.CSI.API.Core.Program;

using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition
{
    [TestFixture]
    public class Constraints_Get : CsiGet
    {
        
        public void GetNameList(ref string[] namesConstraint)
        {
          
        }


        public void GetDiaphragm(string nameConstraint,
            ref eConstraintAxis axis,
            ref string nameCoordinateSystem)
        {
          
        }
        
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
       
        public void Count()
        {
          
        }

        
        public void Count(eConstraintType constraintType)
        {
          
        }
        
        
        public void GetConstraintType(string nameConstraint,
            ref eConstraintType constraintType)
        {
          
        }

        
        public void GetSpecialRigidDiaphragmList(ref string[] diaphragms)
        {
          
        }
        
        
        public void GetBeam(string nameConstraint, 
            ref eConstraintAxis axis,
            ref string nameCoordinateSystem)
        {
          
        }

        
        public void GetBody(string nameConstraint, 
            ref DegreesOfFreedomGlobal degreesOfFreedom,
            ref string nameCoordinateSystem)
        {
          
        }

        
        public void GetEqual(string nameConstraint,
            ref DegreesOfFreedomGlobal degreesOfFreedom,
            ref string nameCoordinateSystem)
        {
          
        }

        
        public void GetLine(string nameConstraint,
            ref DegreesOfFreedomGlobal degreesOfFreedom,
            ref string nameCoordinateSystem)
        {
          
        }

        
        public void GetLocal(string nameConstraint,
            ref DegreesOfFreedomGlobal degreesOfFreedom)
        {
          
        }

        
        public void GetPlate(string nameConstraint,
            ref eConstraintAxis axis,
            ref string nameCoordinateSystem)
        {
          
        }
        
        
        public void GetRod(string nameConstraint,
            ref eConstraintAxis axis,
            ref string nameCoordinateSystem)
        {
          
        }

        
        public void GetWeld(string nameConstraint,
            ref DegreesOfFreedomGlobal degreesOfFreedom,
            ref double tolerance,
            ref string nameCoordinateSystem)
        {
          
        }
#endif
    }
    
    [TestFixture]
    public class Constraints_Set : CsiSet
    {
      
        public void Delete(string nameConstraint)
        {
          
        }

        
        public void SetDiaphragm(string nameConstraint,
            eConstraintAxis axis = eConstraintAxis.AutoAxis,
            string nameCoordinateSystem = CoordinateSystems.Global)
        {
          
        }
        
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
       
        public void ChangeName(string nameConstraint,
            string newName)
        {
          
        }
        
        public void SetBeam(string nameConstraint,
            eConstraintAxis axis,
            string nameCoordinateSystem)
        {
          
        }

        
        public void SetBody(string nameConstraint,
            DegreesOfFreedomGlobal degreesOfFreedom,
            string nameCoordinateSystem)
        {
          
        }

        
        public void SetEqual(string nameConstraint,
            DegreesOfFreedomGlobal degreesOfFreedom,
            string nameCoordinateSystem)
        {
          
        }

        
        public void SetLine(string nameConstraint,
            DegreesOfFreedomGlobal degreesOfFreedom,
            string nameCoordinateSystem)
        {
          
        }

        
        public void SetLocal(string nameConstraint,
            DegreesOfFreedomGlobal degreesOfFreedom)
        {
          
        }

        
        public void SetPlate(string nameConstraint,
            eConstraintAxis axis = eConstraintAxis.AutoAxis,
            string nameCoordinateSystem = CoordinateSystems.Global)
        {
          
        }
        
        
        public void SetRod(string nameConstraint,
            eConstraintAxis axis = eConstraintAxis.AutoAxis,
            string nameCoordinateSystem = CoordinateSystems.Global)
        {
          
        }

        
        public void SetWeld(string nameConstraint,
            DegreesOfFreedomGlobal degreesOfFreedom,
            double tolerance,
            string nameCoordinateSystem)
        {
          
        }
#endif
    }
}
