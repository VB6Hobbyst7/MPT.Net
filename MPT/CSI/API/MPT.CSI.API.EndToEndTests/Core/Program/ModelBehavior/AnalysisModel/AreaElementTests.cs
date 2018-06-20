using System.Linq;
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.AnalysisModel
{
    [TestFixture]
    public class AreaElement_Get : CsiGetAnalysisModel
    {
        #region Query
        [Test]
        public void Count()
        {
            int numberOfObjects = _app.Model.AnalysisModel.AreaElement.Count();
            Assert.That(numberOfObjects, Is.EqualTo(CSiDataArea.NumberOfElementsShellExpected));
        }

        [Test]
        public void GetNameList()
        {
            string[] names;
            _app.Model.AnalysisModel.AreaElement.GetNameList(out names);

            Assert.That(names.Length, Is.EqualTo(CSiDataArea.NumberOfElementsShellExpected));
            Assert.That(names.Contains(CSiDataArea.NameElementShellThin));
            Assert.That(names.Contains(CSiDataArea.NameElementShellThick));
        }

        [Test]
        public void GetTransformationMatrix()
        {
            double[] directionCosines;
            _app.Model.AnalysisModel.AreaElement.GetTransformationMatrix(CSiDataArea.NameElementShellThin, out directionCosines);

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
            _app.Model.AnalysisModel.AreaElement.GetPoints(CSiDataArea.NameElementShellThin, out points);

            Assert.That(points.Length, Is.EqualTo(CSiDataArea.JointsShellThin.Length));
            Assert.That(points.Contains(CSiDataArea.JointsShellThin[0]));
            Assert.That(points.Contains(CSiDataArea.JointsShellThin[1]));
            Assert.That(points.Contains(CSiDataArea.JointsShellThin[2]));
            Assert.That(points.Contains(CSiDataArea.JointsShellThin[3]));
        }

        [Test]
        public void GetPoints_Meshed()
        {
            string[] points;
            _app.Model.AnalysisModel.AreaElement.GetPoints(CSiDataArea.NameElementShellThick, out points);

            Assert.That(points.Length, Is.EqualTo(CSiDataArea.JointsShellThick.Length));
            Assert.That(points.Contains(CSiDataArea.JointsShellThick[0]));
            Assert.That(points.Contains(CSiDataArea.JointsShellThick[1]));
            Assert.That(points.Contains(CSiDataArea.JointsShellThick[2]));
            Assert.That(points.Contains(CSiDataArea.JointsShellThick[3]));
        }

        [Test]
        public void GetObject()
        {
            string objectName;
            _app.Model.AnalysisModel.AreaElement.GetObject(CSiDataArea.NameElementShellThin, out objectName);
            
            Assert.That(objectName, Is.EqualTo(CSiDataArea.NameObjectShellThin));
        }

        [Test]
        public void GetObject_Meshed()
        {
            string objectName;
            _app.Model.AnalysisModel.AreaElement.GetObject(CSiDataArea.NameElementShellThick, out objectName);

            Assert.That(objectName, Is.EqualTo(CSiDataArea.NameObjectShellThick));
        }
        #endregion

        #region Axes


        [Test]
        public void GetLocalAxes()
        {
            AngleLocalAxes angleOffset;
            _app.Model.AnalysisModel.AreaElement.GetLocalAxes(CSiDataArea.NameElementShellThin, out angleOffset);
            
            Assert.That(angleOffset.AngleA, Is.EqualTo(0));
            Assert.That(angleOffset.AngleB, Is.EqualTo(0));
            Assert.That(angleOffset.AngleC, Is.EqualTo(0));
        }
        #endregion

        #region Modifiers

        [Test]
        public void GetModifiers()
        {
            AreaModifier modifier;
            _app.Model.AnalysisModel.AreaElement.GetModifiers(CSiDataArea.NameElementShellThin, out modifier);

            Assert.That(modifier.MembraneF11, Is.EqualTo(CSiDataArea.OldModifiers.MembraneF11));
            Assert.That(modifier.MembraneF22, Is.EqualTo(CSiDataArea.OldModifiers.MembraneF22));
            Assert.That(modifier.MembraneF12, Is.EqualTo(CSiDataArea.OldModifiers.MembraneF12));
            Assert.That(modifier.BendingM11, Is.EqualTo(CSiDataArea.OldModifiers.BendingM11));
            Assert.That(modifier.BendingM22, Is.EqualTo(CSiDataArea.OldModifiers.BendingM22));
            Assert.That(modifier.BendingM12, Is.EqualTo(CSiDataArea.OldModifiers.BendingM12));
            Assert.That(modifier.ShearV13, Is.EqualTo(CSiDataArea.OldModifiers.ShearV13));
            Assert.That(modifier.ShearV23, Is.EqualTo(CSiDataArea.OldModifiers.ShearV23));
            Assert.That(modifier.MassModifier, Is.EqualTo(CSiDataArea.OldModifiers.MassModifier));
            Assert.That(modifier.WeightModifier, Is.EqualTo(CSiDataArea.OldModifiers.WeightModifier));
        }
        #endregion

        #region Cross-Section & Material Properties
        [Test]
        public void GetSection()
        {
            string propertyName;
            _app.Model.AnalysisModel.AreaElement.GetSection(CSiDataArea.NameElementShellThin, out propertyName);

            Assert.That(propertyName, Is.EqualTo(CSiDataArea.NameSectionShellThin));
        }


        public void GetThickness(string name,
            ref eAreaThicknessType thicknessType,
            ref string thicknessPattern,
            ref double thicknessPatternScaleFactor,
            ref double[] thicknesses)
        {
          
        }


      
      
        public void GetMaterialOverwrite(string name,
            ref string propertyName)
        {
          
        }
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        [Test]
        public void GetMaterialTemperature()
        {
            double temperature;
            string patternName;
            _app.Model.AnalysisModel.AreaElement.GetMaterialTemperature(CSiDataArea.NameElementShellThin, out temperature, out patternName);

            Assert.That(temperature, Is.EqualTo(50));
            Assert.That(patternName, Is.EqualTo(CSiData.JointPattern));    
        }
#endif
        #endregion

        #region Area Properties


        public void GetOffsets(string name,
            ref eAreaOffsetType offsetType,
            ref string offsetPattern,
            ref double offsetPatternScaleFactor,
            ref double[] offsets)
        {
          
        }


        #endregion

        #region Loads
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        
        
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
#endif
        
        
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
        #endregion
    }
}
