#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;

using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Support;
using System.Linq;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.AnalysisModel
{
    [TestFixture]
    public class LinkElement_Get : CsiGetAnalysisModel
    {
        #region Query
        [Test]
        public void Count()
        {
            int numberOfObjects = _app.Model.AnalysisModel.LinkElement.Count();
            Assert.That(numberOfObjects, Is.EqualTo(CSiDataLink.NumberOfObjectsExpected));
        }

        [Test]
        public void GetNameList()
        {
            string[] names;
            _app.Model.AnalysisModel.LinkElement.GetNameList(out names);

            Assert.That(names.Length, Is.EqualTo(CSiDataLink.NumberOfObjectsExpected));
            Assert.That(names.Contains(CSiDataLink.NameElementSinglePoint));
            Assert.That(names.Contains(CSiDataLink.NameElementTwoPoints));
        }

        [Test]
        public void GetTransformationMatrix()
        {
            double[] directionCosines;
            _app.Model.AnalysisModel.LinkElement.GetTransformationMatrix(CSiDataLink.NameElementTwoPoints, out directionCosines);

            Assert.That(directionCosines.Length, Is.EqualTo(9));

            // Row 1
            Assert.That(directionCosines[0], Is.EqualTo(0.894).Within(0.001));
            Assert.That(directionCosines[1], Is.EqualTo(-0.447).Within(0.001));
            Assert.That(directionCosines[2], Is.EqualTo(0).Within(0.001));

            // Row 2
            Assert.That(directionCosines[3], Is.EqualTo(0).Within(0.001));
            Assert.That(directionCosines[4], Is.EqualTo(0).Within(0.001));
            Assert.That(directionCosines[5], Is.EqualTo(-1).Within(0.001));

            // Row 3
            Assert.That(directionCosines[6], Is.EqualTo(0.447).Within(0.001));
            Assert.That(directionCosines[7], Is.EqualTo(0.894).Within(0.001));
            Assert.That(directionCosines[8], Is.EqualTo(0).Within(0.001));
        }

        [Test]
        public void GetPoints_Single_Joint_Link()
        {
            string[] points;
            _app.Model.AnalysisModel.LinkElement.GetPoints(CSiDataLink.NameElementSinglePoint, out points);

            Assert.That(points.Length, Is.EqualTo(CSiDataLink.SinglePointJoints.Length));
            Assert.That(points.Contains(CSiDataLink.SinglePointJoints[0]));
            Assert.That(points.Contains(CSiDataLink.SinglePointJoints[1]));
        }

        [Test]
        public void GetPoints_2_Joint_Link()
        {
            string[] points;
            _app.Model.AnalysisModel.LinkElement.GetPoints(CSiDataLink.NameElementTwoPoints, out points);

            Assert.That(points.Length, Is.EqualTo(CSiDataLink.TwoPointsJoints.Length));
            Assert.That(points.Contains(CSiDataLink.TwoPointsJoints[0]));
            Assert.That(points.Contains(CSiDataLink.TwoPointsJoints[1]));
        }


        [Test]
        public void GetObject_Single_Joint()
        {
            string objectName;
            _app.Model.AnalysisModel.LinkElement.GetObject(CSiDataLink.NameElementSinglePoint, out objectName);
            
            Assert.That(objectName, Is.EqualTo(CSiDataLink.NameObjectSinglePoint));
        }

        [Test]
        public void GetObject_2_Joint()
        {
            string objectName;
            _app.Model.AnalysisModel.LinkElement.GetObject(CSiDataLink.NameElementTwoPoints, out objectName);

            Assert.That(objectName, Is.EqualTo(CSiDataLink.NameObjectTwoPoints));
        }

        [Test]
        public void GetObject_And_Type_Single_Joint()
        {
            string objectName;
            ePointTypeObject objectType;

            _app.Model.AnalysisModel.LinkElement.GetObject(CSiDataLink.NameElementSinglePoint, out objectName, out objectType);

            Assert.That(objectName, Is.EqualTo(CSiDataLink.NameObjectSinglePoint));
            Assert.That(objectType, Is.EqualTo(ePointTypeObject.Point));
        }

        [Test]
        public void GetObject_And_Type_2_Joint()
        {
            string objectName;
            ePointTypeObject objectType;

            _app.Model.AnalysisModel.LinkElement.GetObject(CSiDataLink.NameElementTwoPoints, out objectName, out objectType);

            Assert.That(objectName, Is.EqualTo(CSiDataLink.NameObjectTwoPoints));
            Assert.That(objectType, Is.EqualTo(ePointTypeObject.Point));
        }
        #endregion

        #region Axes
        [Test]
        public void GetLocalAxes()
        {
            AngleLocalAxes angleOffset;
            _app.Model.AnalysisModel.LinkElement.GetLocalAxes(CSiDataLink.NameObjectTwoPoints, out angleOffset);
            
            Assert.That(angleOffset.AngleA, Is.EqualTo(0));
            Assert.That(angleOffset.AngleB, Is.EqualTo(0));
            Assert.That(angleOffset.AngleC, Is.EqualTo(0));
        }
        #endregion

        #region Cross-Section & Material Properties
        [Test]
        public void GetSection()
        {
            string propertyName;
            _app.Model.AnalysisModel.LinkElement.GetSection(CSiDataLink.NameObjectTwoPoints, out propertyName);

            Assert.That(propertyName, Is.EqualTo(CSiDataLink.NameSectionMultiLinearElastic));
        }

        public void GetSectionFrequencyDependent(string name,
            ref string propertyName)
        {
          
        }

        #endregion

        #region Loads
        
        public void GetLoadDeformation(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref DegreesOfFreedomLocal[] degreesOfFreedom,
            ref Deformations[] deformations,
            eItemTypeElement itemType = eItemTypeElement.Element)
        {
          
        }
        
        
        public void GetLoadGravity(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref string[] coordinateSystems,
            ref double[] xLoadMultiplier,
            ref double[] yLoadMultiplier,
            ref double[] zLoadMultiplier,
            eItemTypeElement itemType = eItemTypeElement.Element)
        {
          
        }

        
        public void GetLoadTargetForce(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref ForcesActive[] forcesActive,
            ref Deformations[] deformations,
            ref Forces[] relativeForcesLocation,
            eItemTypeElement itemType = eItemTypeElement.Element)
        {
          
        }
        #endregion
    }
}
#endif