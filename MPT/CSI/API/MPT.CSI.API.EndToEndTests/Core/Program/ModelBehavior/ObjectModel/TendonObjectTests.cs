using System.Linq;
using NUnit.Framework;
using MPT.CSI.API.Core.Program;

using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.ObjectModel
{
    [TestFixture]
    public class TendonObject_Get : CsiGet
    {
        #region Query
        [Test]
        public void GetSelected_None_Selected_Returns_False()
        {
            bool isSelected;
            _app.Model.ObjectModel.TendonObject.GetSelected(CSiDataLine.NameObjectTendonAsLoads, out isSelected);

            Assert.IsFalse(isSelected);
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        [Test]
        public void Count()
        {
            int numberOfObjects = _app.Model.ObjectModel.TendonObject.Count();
            Assert.That(numberOfObjects, Is.EqualTo(CSiDataLine.NumberOfObjectTendonsExpected));
        }

        [Test]
        public void GetNameList()
        {
            string[] names;
            _app.Model.ObjectModel.TendonObject.GetNameList(out names);

            Assert.That(names.Length, Is.EqualTo(CSiDataLine.NumberOfObjectTendonsExpected));
            Assert.That(names.Contains(CSiDataLine.NameObjectTendonAsLoads));
            Assert.That(names.Contains(CSiDataLine.NameObjectTendonAsElements));
        }

        [Test]
        public void GetTransformationMatrix()
        {
            double[] directionCosines;
            _app.Model.ObjectModel.TendonObject.GetTransformationMatrix(CSiDataLine.NameObjectTendonAsLoads, out directionCosines);

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
        public void GetTransformationMatrix_From_Current_Global_Coordinate_System()
        {
            double[] directionCosines;
            _app.Model.ObjectModel.TendonObject.GetTransformationMatrix(CSiDataLine.NameObjectTendonAsLoads, out directionCosines, isGlobal: false);

            Assert.That(directionCosines.Length, Is.EqualTo(9));

            // Row 1
            Assert.That(directionCosines[0], Is.EqualTo(0.707).Within(0.001));
            Assert.That(directionCosines[1], Is.EqualTo(0));
            Assert.That(directionCosines[2], Is.EqualTo(-0.707).Within(0.001));

            // Row 2
            Assert.That(directionCosines[3], Is.EqualTo(-0.707).Within(0.001));
            Assert.That(directionCosines[4], Is.EqualTo(0));
            Assert.That(directionCosines[5], Is.EqualTo(-0.707).Within(0.001));

            // Row 3
            Assert.That(directionCosines[6], Is.EqualTo(0));
            Assert.That(directionCosines[7], Is.EqualTo(1));
            Assert.That(directionCosines[8], Is.EqualTo(0));
        }

        [Test]
        public void GetPoints()
        {
            string[] points;
            _app.Model.ObjectModel.TendonObject.GetPoints(CSiDataLine.NameObjectTendonAsLoads, out points);

            Assert.That(points.Length, Is.EqualTo(2));
            Assert.That(points.Contains("2"));
            Assert.That(points.Contains("11"));
        }

        [Test]
        public void GetGUID()
        {
            string guid;
            _app.Model.ObjectModel.TendonObject.GetGUID(CSiDataLine.NameObjectTendonAsLoads, out guid);

            Assert.That(!string.IsNullOrEmpty(guid));
            //Assert.That(guid, Is.EqualTo("b8072180-bb6d-41b5-8b73-f2b04d1a1ec1"));
            //Assert.That(guid, Is.EqualTo("5f730958-554f-47ee-8d63-58b505ea19f7"));
        }

        [Test]
        public void GetElement_Modeled_As_Loads()
        {
            // TODO: This is a hack to make the test pass due to an error with solid elements in the custom coordinate system. Remove coordinate switching.
            _app.Model.SetPresentCoordSystem(CSiData.CoordinateSystemGlobal);
            _app.Model.Analyze.CreateAnalysisModel();
            _app.Model.SetPresentCoordSystem(CSiData.CoordinateSystemCustom);
            string[] elementNames;
            _app.Model.ObjectModel.TendonObject.GetElement(CSiDataLine.NameObjectTendonAsLoads, out elementNames);

            Assert.That(elementNames.Length, Is.EqualTo(0));
        }

        [Test]
        public void GetElement_Modeled_As_Elements()
        {
            // TODO: This is a hack to make the test pass due to an error with solid elements in the custom coordinate system. Remove coordinate switching.
            _app.Model.SetPresentCoordSystem(CSiData.CoordinateSystemGlobal);
            _app.Model.Analyze.CreateAnalysisModel();
            _app.Model.SetPresentCoordSystem(CSiData.CoordinateSystemCustom);
            string[] elementNames;
            _app.Model.ObjectModel.TendonObject.GetElement(CSiDataLine.NameObjectTendonAsElements, out elementNames);

            Assert.That(elementNames.Length, Is.EqualTo(7));
            Assert.That(elementNames.Contains(CSiDataLine.NameElementTendonAsElements));
            Assert.That(elementNames.Contains(CSiDataLine.NameElementTendonAsElements2));
            Assert.That(elementNames.Contains(CSiDataLine.NameElementTendonAsElements3));
            Assert.That(elementNames.Contains(CSiDataLine.NameElementTendonAsElements4));
            Assert.That(elementNames.Contains(CSiDataLine.NameElementTendonAsElements5));
            Assert.That(elementNames.Contains(CSiDataLine.NameElementTendonAsElements6));
            Assert.That(elementNames.Contains(CSiDataLine.NameElementTendonAsElements7));
        }
#endif
#if BUILD_SAP2000v20
        [Test]
        public void GetGroupAssign() // Verification Incident 15096
        {
            string objectName = "59";
            string[] groupNames;

            _app.Model.ObjectModel.TendonObject.GetGroupAssign(objectName, out groupNames);

            Assert.That(groupNames.Length == 3);
            Assert.That(groupNames.Contains(CSiData.OldGroupNames[0]));
            Assert.That(groupNames.Contains(CSiData.OldGroupNames[1]));
            Assert.That(groupNames.Contains(CSiData.OldGroupNames[2] + " Tendons"));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("NonExistingName")]
        public void GetGroupAssign_Using_NonExisting_Name_Throws_CSiException(string objectName) // Verification Incident 15096
        {
            string[] groupNames;

            Assert.That(() =>
            {
                _app.Model.ObjectModel.TendonObject.GetGroupAssign(objectName, out groupNames);
            },
            Throws.Exception.TypeOf<CSiException>());
        }
#endif
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017

        public void GetNameListOnStory(string storyName, 
                        ref string[] names)
        {
          
        }
#endif
        #endregion

        #region Axes
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        [Test]
        public void GetLocalAxes()
        {
            AngleLocalAxes angleOffset;
            bool isAdvanced;
            _app.Model.ObjectModel.TendonObject.GetLocalAxes(CSiDataLine.NameObjectTendonAsLoads, out angleOffset, out isAdvanced);

            Assert.IsFalse(isAdvanced);
            Assert.That(angleOffset.AngleA, Is.EqualTo(0));
            Assert.That(angleOffset.AngleB, Is.EqualTo(0));
            Assert.That(angleOffset.AngleC, Is.EqualTo(0));
        }
#endif
        #endregion

        #region Cross-Section & Material Properties
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        [Test]
        public void GetSection()
        {
            string propertyName;
            _app.Model.ObjectModel.TendonObject.GetSection(CSiDataLine.NameObjectTendonAsLoads, out propertyName);

            Assert.That(propertyName, Is.EqualTo(CSiDataLine.NameSectionTendonAsLoads));
        }

        [Test]
        public void GetMaterialTemperature()
        {
            double temperature;
            string patternName;
            _app.Model.ObjectModel.TendonObject.GetMaterialTemperature(CSiDataLine.NameObjectTendonAsLoads, out temperature, out patternName);

            Assert.That(temperature, Is.EqualTo(52));
            Assert.That(patternName, Is.EqualTo(string.Empty));
        }





        public void GetTensionCompressionLimits(string name,
            ref bool limitCompressionExists,
            ref double limitCompression,
            ref bool limitTensionExists,
            ref double limitTension)
        {
          
        }
        
        
        public void GetTendonData(string name,
            ref int numberPoints,
            ref eTendonGeometryDefinition[] tendonGeometryDefinitions,
            ref Coordinate3DCartesian[] coordinates,
            string coordinateSystem = CoordinateSystems.Global)
        {
          
        }
        
        
        public void GetTendonGeometry(string name,
            ref int numberPoints,
            ref Coordinate3DCartesian[] coordinates,
            string coordinateSystem = CoordinateSystems.Global)
        {
          
        }
        
        
        public void GetDiscretization(string name,
            ref double maxDiscretizationLength)
        {
          
        }
#endif
        #endregion

        #region Loads
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        public void GetLoadedGroup(string name,
            ref string groupName)
        {
          
        }
        
        
        public void GetLoadGravity(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref double[] xLoadMultiplier,
            ref double[] yLoadMultiplier,
            ref double[] zLoadMultiplier,
            ref string[] coordinateSystems,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void GetLoadDeformation(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref double[] U1,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        
        public void GetLoadStrain(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref double[] strainLoadValues,
            ref string[] jointPatternNames,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void GetLoadTemperature(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref double[] temperatureLoadValues,
            ref string[] jointPatternNames,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        
        public void GetLoadForceStress(string name,
            ref int numberItems,
            ref string[] tendonNames,
            ref string[] loadPatterns,
            ref eTendonJack[] jackedFrom,
            ref eTendonLoadType[] loadTypes,
            ref double[] values,
            ref double[] curvatureCoefficients,
            ref double[] wobbleCoefficients,
            ref double[] lossAnchorages,
            ref double[] lossShortenings,
            ref double[] lossCreep,
            ref double[] lossShrinkages,
            ref double[] lossSteelRelax,
            eItemType itemType = eItemType.Object)
        {
          
        }

#endif
        #endregion
    }

    [TestFixture]
    public class TendonObject_Set : CsiSet
    {
        
        public void SetSelected(string name,
            bool isSelected,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017


        
        public void SetGUID(string name,
            string GUID = "")
        {
          
        }

        
        public void SetLocalAxes(string name,
            AngleLocalAxes angleOffset,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        
        public void ChangeName(string currentName, string newName)
        {
          
        }

        
        public void Delete(string name)
        {
          
        }

        
        public void AddByCoordinate(ref Coordinate3DCartesian[] coordinates,
            ref string name,
            string nameProperty = "Default",
            string userName = "",
            string coordinateSystem = CoordinateSystems.Global)
        {
          
        }

        
        public void AddByPoint(string[] pointNames,
            ref string name,
            string nameProperty = "Default",
            string userName = "")
        {
          
        }

        
        public void SetGroupAssign(string name,
            string groupName,
            bool remove = false,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetSection(string name, 
            string propertyName, 
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetMaterialTemperature(string name,
            double temperature,
            string patternName,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetTensionCompressionLimits(string name,
            bool limitCompressionExists,
            double limitCompression,
            bool limitTensionExists,
            double limitTension,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetTendonData(string name,
            int numberPoints,
            eTendonGeometryDefinition[] tendonGeometryDefinitions,
            Coordinate3DCartesian[] coordinates,
            string coordinateSystem = CoordinateSystems.Global)
        {
          
        }

        
        public void SetDiscretization(string name,
            double maxDiscretizationLength,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        
        public void SetLoadedGroup(string name,
            string groupName,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        
        public void SetLoadGravity(string name,
            string loadPattern,
            double xLoadMultiplier,
            double yLoadMultiplier,
            double zLoadMultiplier,
            string coordinateSystem = CoordinateSystems.Global,
            bool replace = true,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void DeleteLoadGravity(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        
        public void SetLoadDeformation(string name,
            string loadPattern,
            double U1,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        
        public void DeleteLoadDeformation(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        
        public void SetLoadStrain(string name,
            string loadPattern,
            double strainLoadValue,
            string jointPatternName,
            bool replace = true,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        
        public void DeleteLoadStrain(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        
        public void SetLoadTemperature(string name,
            string loadPattern,
            double temperatureLoadValue,
            string jointPatternName,
            bool replace,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        
        public void DeleteLoadTemperature(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetLoadForceStress(string name,
            string loadPattern,
            eTendonJack jackedFrom,
            eTendonLoadType loadType,
            double value,
            double curvatureCoefficient,
            double wobbleCoefficient,
            double lossAnchorage,
            double lossShortening,
            double lossCreep,
            double lossShrinkage,
            double lossSteelRelax,
            bool replace = true,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        
        public void DeleteLoadForceStress(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
          
        }
#endif
    }
}
