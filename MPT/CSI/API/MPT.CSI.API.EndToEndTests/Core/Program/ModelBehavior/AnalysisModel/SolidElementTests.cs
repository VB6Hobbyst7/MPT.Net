#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using System.Linq;
using NUnit.Framework;
using MPT.CSI.API.Core.Program;

using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.AnalysisModel
{
    [TestFixture]
    public class SolidElement_Get : CsiGetAnalysisModel
    {
        #region Query
        [Test]
        public void Count()
        {
            int numberOfObjects = _app.Model.AnalysisModel.SolidElement.Count();
            Assert.That(numberOfObjects, Is.EqualTo(CSiDataSolid.NumberOfElementsExpected));
        }

        [Test]
        public void GetNameList()
        {
            string[] names;
            _app.Model.AnalysisModel.SolidElement.GetNameList(out names);

            Assert.That(names.Length, Is.EqualTo(CSiDataSolid.NumberOfElementsExpected));
            Assert.That(names.Contains(CSiDataSolid.NameElement));
            Assert.That(names.Contains(CSiDataSolid.NameElementMeshed));
        }

        [Test]
        public void GetTransformationMatrix()
        {
            double[] directionCosines;
            _app.Model.AnalysisModel.SolidElement.GetTransformationMatrix(CSiDataSolid.NameElement, out directionCosines);

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
        public void GetPoints()
        {
            string[] points;
            _app.Model.AnalysisModel.SolidElement.GetPoints(CSiDataSolid.NameElement, out points);

            Assert.That(points.Length, Is.EqualTo(CSiDataSolid.ElementJoints.Length));
            Assert.That(points.Contains(CSiDataSolid.ElementJoints[0]));
            Assert.That(points.Contains(CSiDataSolid.ElementJoints[1]));
            Assert.That(points.Contains(CSiDataSolid.ElementJoints[2]));
            Assert.That(points.Contains(CSiDataSolid.ElementJoints[3]));
            Assert.That(points.Contains(CSiDataSolid.ElementJoints[4]));
            Assert.That(points.Contains(CSiDataSolid.ElementJoints[5]));
            Assert.That(points.Contains(CSiDataSolid.ElementJoints[6]));
            Assert.That(points.Contains(CSiDataSolid.ElementJoints[7]));
        }

        [Test]
        public void GetPoints_Meshed()
        {
            string[] points;
            _app.Model.AnalysisModel.SolidElement.GetPoints(CSiDataSolid.NameElementMeshed, out points);

            Assert.That(points.Length, Is.EqualTo(CSiDataSolid.ElementJointsMeshed.Length));
            Assert.That(points.Contains(CSiDataSolid.ElementJointsMeshed[0]));
            Assert.That(points.Contains(CSiDataSolid.ElementJointsMeshed[1]));
            Assert.That(points.Contains(CSiDataSolid.ElementJointsMeshed[2]));
            Assert.That(points.Contains(CSiDataSolid.ElementJointsMeshed[3]));
            Assert.That(points.Contains(CSiDataSolid.ElementJointsMeshed[4]));
            Assert.That(points.Contains(CSiDataSolid.ElementJointsMeshed[5]));
            Assert.That(points.Contains(CSiDataSolid.ElementJointsMeshed[6]));
            Assert.That(points.Contains(CSiDataSolid.ElementJointsMeshed[7]));
        }

        [Test]
        public void GetObject()
        {
            string objectName;
            _app.Model.AnalysisModel.SolidElement.GetObject(CSiDataSolid.NameElement, out objectName);
            
            Assert.That(objectName, Is.EqualTo(CSiDataSolid.NameObject));
        }

        [Test]
        public void GetObject_Meshed()
        {
            string objectName;
            _app.Model.AnalysisModel.SolidElement.GetObject(CSiDataSolid.NameElementMeshed, out objectName);

            Assert.That(objectName, Is.EqualTo(CSiDataSolid.NameObjectMeshed));
        }

        #endregion

        #region Axes
        [Test]
        public void GetLocalAxes()
        {
            AngleLocalAxes angleOffset;
            _app.Model.AnalysisModel.SolidElement.GetLocalAxes(CSiDataSolid.NameElement, out angleOffset);
            
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
            _app.Model.AnalysisModel.SolidElement.GetSection(CSiDataSolid.NameElement, out propertyName);

            Assert.That(propertyName, Is.EqualTo(CSiDataSolid.NameSection));
        }

        [Test]
        public void GetMaterialTemperature()
        {
            double temperature;
            string patternName;
            _app.Model.AnalysisModel.SolidElement.GetMaterialTemperature(CSiDataSolid.NameElement, out temperature, out patternName);

            Assert.That(temperature, Is.EqualTo(98));
            Assert.That(patternName, Is.EqualTo(CSiData.JointPattern));
        }
        #endregion

        #region Loads
        public void GetLoadTemperature(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref eLoadTemperatureType[] temperatureLoadType,
            ref double[] temperatureLoadValues,
            ref string[] jointPatternNames,
            eItemTypeElement itemType = eItemTypeElement.Element)
        {
          
        }

        
        public void GetLoadSurfacePressure(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref eFace[] faceApplied,
            ref double[] surfacePressureLoadValues,
            ref string[] jointPatternNames,
            eItemTypeElement itemType = eItemTypeElement.Element)
        {
          
        }

        
        public void GetLoadStrain(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref eStrainComponent[] component,
            ref double[] strainLoadValues,
            ref string[] jointPatternNames,
            eItemTypeElement itemType = eItemTypeElement.Element)
        {
          
        }

        
        public void GetLoadPorePressure(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref double[] porePressureLoadValues,
            ref string[] jointPatternNames,
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
        #endregion
    }
}
#endif