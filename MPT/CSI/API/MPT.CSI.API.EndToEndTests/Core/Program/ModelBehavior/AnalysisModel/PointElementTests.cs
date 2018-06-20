using System.Linq;
using NUnit.Framework;
using MPT.CSI.API.Core.Program;

using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Support;
using eObjectType = MPT.CSI.API.Core.Program.ModelBehavior.Definition.eObjectType;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.AnalysisModel
{
    [TestFixture]
    public class PointElement_Get : CsiGetAnalysisModel
    {
        #region Query

        [Test]
        public void Count()
        {
            int numberOfObjects = _app.Model.AnalysisModel.PointElement.Count();
            Assert.That(numberOfObjects, Is.EqualTo(CSiDataPoint.NumberOfElementsExpected));
        }


        public void CountConstraint(string name = "")
        {
          
        }

        
        public void CountRestraint()
        {
          
        }

        
        public void CountSpring()
        {
          
        }

        
        public void CountLoadForce(string name = "",
            string loadPattern = "")
        {
          
        }

        
        public void CountLoadDisplacements(string name = "",
            string loadPattern = "")
        {
          
        }


        [Test]
        public void GetNameList()
        {
            string[] names;
            _app.Model.AnalysisModel.PointElement.GetNameList(out names);

            Assert.That(names.Length, Is.EqualTo(CSiDataPoint.NumberOfElementsExpected));
            Assert.That(names.Contains(CSiDataPoint.NameElement));
        }

        [Test]
        public void GetTransformationMatrix()
        {
            double[] directionCosines;
            _app.Model.AnalysisModel.PointElement.GetTransformationMatrix(CSiDataPoint.NameElement, out directionCosines);

            Assert.That(directionCosines.Length, Is.EqualTo(9));

            // Row 1
            Assert.That(directionCosines[0], Is.EqualTo(1));
            Assert.That(directionCosines[1], Is.EqualTo(0));
            Assert.That(directionCosines[2], Is.EqualTo(0));

            // Row 2
            Assert.That(directionCosines[3], Is.EqualTo(0));
            Assert.That(directionCosines[4], Is.EqualTo(1));
            Assert.That(directionCosines[5], Is.EqualTo(0));

            // Row 3
            Assert.That(directionCosines[6], Is.EqualTo(0));
            Assert.That(directionCosines[7], Is.EqualTo(0));
            Assert.That(directionCosines[8], Is.EqualTo(1));
        }



        [Test]
        public void GetObject()
        {
            string objectName;
            _app.Model.AnalysisModel.PointElement.GetObject(CSiDataPoint.NameElement, out objectName);
            
            Assert.That(objectName, Is.EqualTo(CSiDataPoint.NameObject));
        }


        public void GetObject(string name, ref string nameObject, ref ePointTypeObject objectType)
        {
          
        }

        
        public void GetCoordinate(string name,
            ref Coordinate3DCartesian coordinate,
            string coordinateSystem = CoordinateSystems.Global)
        {
          
        }
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        public void GetCoordinate(string name,
            ref Coordinate3DCylindrical coordinate,
            string coordinateSystem = CoordinateSystems.Global)
        {
          
        }
        
        
        public void GetCoordinate(string name,
            ref Coordinate3DSpherical coordinate,
            string coordinateSystem = CoordinateSystems.Global)
        {
          
        }
#endif

        public void GetConnectivity(string name,
            ref int numberItems,
            ref eObjectType[] objectTypes,
            ref string[] objectNames,
            ref int[] pointNumbers)
        {
          
        }
        #endregion

        #region Axes
        [Test]
        public void GetLocalAxes()
        {
            AngleLocalAxes angleOffset;
            _app.Model.AnalysisModel.PointElement.GetLocalAxes(CSiDataPoint.NameElement, out angleOffset);
            
            Assert.That(angleOffset.AngleA, Is.EqualTo(0));
            Assert.That(angleOffset.AngleB, Is.EqualTo(0));
            Assert.That(angleOffset.AngleC, Is.EqualTo(0));
        }
        #endregion

        #region Point Properties

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        public void GetMergeNumber(string name,
            ref int mergeNumber)
        {
          
        }
#endif

        public void GetPatternValue(string name,
            string patternName,
            ref double value)
        {
          
        }

        #endregion

        #region Support & Connections
        
        public void GetConstraint(string name,
            ref int numberItems,
            ref string[] pointNames,
            ref string[] constraintNames,
            eItemTypeElement itemType = eItemTypeElement.Element)
        {
          
        }
        
        
        public void GetRestraint(string name,
            ref DegreesOfFreedomLocal degreesOfFreedom)
        {
          
        }
        
        
        public void GetSpring(string name,
            ref Stiffness stiffnesses)
        {
          
        }

        
        public void GetSpringCoupled(string name,
            ref StiffnessCoupled stiffnesses)
        {
          
        }
        
        public void IsSpringCoupled(string name)
        {
          
        }


        #endregion

        #region Loads
        
        public void GetLoadForce(string name,
            ref int numberItems,
            ref string[] pointNames,
            ref string[] loadPatterns,
            ref int[] loadPatternSteps,
            ref string[] coordinateSystem,
            ref Loads[] forces,
            eItemTypeElement itemType = eItemTypeElement.Element)
        {
          
        }

        
        public void GetLoadDisplacement(string name,
            ref int numberItems,
            ref string[] pointNames,
            ref string[] loadPatterns,
            ref int[] loadPatternSteps,
            ref string[] coordinateSystem,
            ref Displacements[] displacements,
            eItemTypeElement itemType = eItemTypeElement.Element)
        {
          
        }

        #endregion
    }
}
