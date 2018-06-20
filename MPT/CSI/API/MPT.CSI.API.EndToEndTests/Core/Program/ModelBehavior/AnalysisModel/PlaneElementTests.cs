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
    public class PlaneElement_Get : CsiGetAnalysisModel
    {
        #region Query
        [Test]
        public void Count()
        {
            int numberOfObjects = _app.Model.AnalysisModel.PlaneElement.Count();
            Assert.That(numberOfObjects, Is.EqualTo(CSiDataArea.NumberOfObjectsPlaneExpected));
        }

        [Test]
        public void GetNameList()
        {
            string[] names;
            _app.Model.AnalysisModel.PlaneElement.GetNameList(out names);

            Assert.That(names.Length, Is.EqualTo(CSiDataArea.NumberOfObjectsPlaneExpected));
            Assert.That(names.Contains(CSiDataArea.NameElementPlaneStrain));
            Assert.That(names.Contains(CSiDataArea.NameElementPlaneStress));
        }

        [Test]
        public void GetTransformationMatrix()
        {
            double[] directionCosines;
            _app.Model.AnalysisModel.PlaneElement.GetTransformationMatrix(CSiDataArea.NameElementPlaneStress, out directionCosines);

            Assert.That(directionCosines.Length, Is.EqualTo(9));

            // Row 1
            Assert.That(directionCosines[0], Is.EqualTo(1));
            Assert.That(directionCosines[1], Is.EqualTo(0));
            Assert.That(directionCosines[2], Is.EqualTo(0));

            // Row 2
            Assert.That(directionCosines[3], Is.EqualTo(0));
            Assert.That(directionCosines[4], Is.EqualTo(0));
            Assert.That(directionCosines[5], Is.EqualTo(-1));

            // Row 3
            Assert.That(directionCosines[6], Is.EqualTo(0));
            Assert.That(directionCosines[7], Is.EqualTo(1));
            Assert.That(directionCosines[8], Is.EqualTo(0));
        }

        [Test]
        public void GetPoints()
        {
            string[] points;
            _app.Model.AnalysisModel.PlaneElement.GetPoints(CSiDataArea.NameElementPlaneStress, out points);

            Assert.That(points.Length, Is.EqualTo(CSiDataArea.JointsPlaneStress.Length));
            Assert.That(points.Contains(CSiDataArea.JointsPlaneStress[0]));
            Assert.That(points.Contains(CSiDataArea.JointsPlaneStress[1]));
            Assert.That(points.Contains(CSiDataArea.JointsPlaneStress[2]));
            Assert.That(points.Contains(CSiDataArea.JointsPlaneStress[3]));
        }

        [Test]
        public void GetObject()
        {
            string objectName;
            _app.Model.AnalysisModel.PlaneElement.GetObject(CSiDataArea.NameElementPlaneStress, out objectName);
            
            Assert.That(objectName, Is.EqualTo(CSiDataArea.NameObjectPlaneStress));
        }
        #endregion

        #region Axes
        [Test]
        public void GetLocalAxes()
        {
            AngleLocalAxes angleOffset;
            _app.Model.AnalysisModel.PlaneElement.GetLocalAxes(CSiDataArea.NameElementPlaneStress, out angleOffset);

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
            _app.Model.AnalysisModel.PlaneElement.GetSection(CSiDataArea.NameElementPlaneStress, out propertyName);

            Assert.That(propertyName, Is.EqualTo(CSiDataArea.NameSectionPlaneStress));
        }


        [Test]
        public void GetMaterialTemperature()
        {
            double temperature;
            string patternName;
            _app.Model.AnalysisModel.PlaneElement.GetMaterialTemperature(CSiDataArea.NameElementPlaneStress, out temperature, out patternName);

            Assert.That(temperature, Is.EqualTo(66));
            Assert.That(patternName, Is.EqualTo(CSiData.JointPattern));
        }
        #endregion

        #region Loads

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

        
        public void GetLoadPorePressure(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref double[] porePressureLoadValues,
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

        
        public void GetLoadUniform(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref string[] coordinateSystems,
            ref eLoadDirection[] directionApplied,
            ref double[] uniformLoadValues,
            eItemTypeElement itemType = eItemTypeElement.Element)
        {
          
        }

        
        public void GetLoadRotate(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref double[] rotateLoadValues,
            eItemTypeElement itemType = eItemTypeElement.Element)
        {
          
        }
        #endregion
    }
}
#endif
