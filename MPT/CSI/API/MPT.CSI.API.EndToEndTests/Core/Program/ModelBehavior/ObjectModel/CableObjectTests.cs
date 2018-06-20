#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
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
    public class CableObject_Get : CsiGet
    {
        [Test]
        public void GetSelected_None_Selected_Returns_False()
        {
            bool isSelected;
            _app.Model.ObjectModel.CableObject.GetSelected(CSiDataLine.NameObjectCable, out isSelected);

            Assert.IsFalse(isSelected);
        }

        [Test]
        public void Count()
        {
            int numberOfObjects = _app.Model.ObjectModel.CableObject.Count();
            Assert.That(numberOfObjects, Is.EqualTo(CSiDataLine.NumberOfObjectCablesExpected));
        }

        [Test]
        public void GetNameList()
        {
            string[] names;
            _app.Model.ObjectModel.CableObject.GetNameList(out names);

            Assert.That(names.Length, Is.EqualTo(CSiDataLine.NumberOfObjectCablesExpected));
            Assert.That(names.Contains(CSiDataLine.NameObjectCable));
        }

        [Test]
        public void GetTransformationMatrix()
        {
            double[] directionCosines;
            _app.Model.ObjectModel.CableObject.GetTransformationMatrix(CSiDataLine.NameObjectCable, out directionCosines);

            Assert.That(directionCosines.Length, Is.EqualTo(9));

            // Row 1
            Assert.That(directionCosines[0], Is.EqualTo(0));
            Assert.That(directionCosines[1], Is.EqualTo(0));
            Assert.That(directionCosines[2], Is.EqualTo(-1).Within(0.001));

            // Row 2
            Assert.That(directionCosines[3], Is.EqualTo(-0.894).Within(0.001));
            Assert.That(directionCosines[4], Is.EqualTo(0.447).Within(0.001));
            Assert.That(directionCosines[5], Is.EqualTo(0));

            // Row 3
            Assert.That(directionCosines[6], Is.EqualTo(0.447).Within(0.001));
            Assert.That(directionCosines[7], Is.EqualTo(0.894).Within(0.001));
            Assert.That(directionCosines[8], Is.EqualTo(0));
        }

        [Test]
        public void GetTransformationMatrix_From_Current_Global_Coordinate_System()
        {
            double[] directionCosines;
            _app.Model.ObjectModel.CableObject.GetTransformationMatrix(CSiDataLine.NameObjectCable, out directionCosines, isGlobal: false);

            Assert.That(directionCosines.Length, Is.EqualTo(9));

            // Row 1
            Assert.That(directionCosines[0], Is.EqualTo(-0.632).Within(0.001));
            Assert.That(directionCosines[1], Is.EqualTo(0.316).Within(0.001));
            Assert.That(directionCosines[2], Is.EqualTo(-0.707).Within(0.001));

            // Row 2
            Assert.That(directionCosines[3], Is.EqualTo(-0.632).Within(0.001));
            Assert.That(directionCosines[4], Is.EqualTo(0.316).Within(0.001));
            Assert.That(directionCosines[5], Is.EqualTo(0.707).Within(0.001));

            // Row 3
            Assert.That(directionCosines[6], Is.EqualTo(0.447).Within(0.001));
            Assert.That(directionCosines[7], Is.EqualTo(0.894).Within(0.001));
            Assert.That(directionCosines[8], Is.EqualTo(0));
        }

        [Test]
        public void GetPoints()
        {
            string[] points;
            _app.Model.ObjectModel.CableObject.GetPoints(CSiDataLine.NameObjectCable, out points);

            Assert.That(points.Length, Is.EqualTo(CSiDataLine.NameElementCableJoints.Length));
            Assert.That(points.Contains(CSiDataLine.NameElementCableJoints[0]));
            Assert.That(points.Contains(CSiDataLine.NameElementCableJoints[1]));
        }

        [Test]
        public void GetGUID()
        {
            string guid;
            _app.Model.ObjectModel.CableObject.GetGUID(CSiDataLine.NameObjectCable, out guid);

            Assert.That(!string.IsNullOrEmpty(guid));
        }

        [Test]
        public void GetElement_Single()
        {
            // TODO: This is a hack to make the test pass due to an error with solid elements in the custom coordinate system. Remove coordinate switching.
            _app.Model.SetPresentCoordSystem(CSiData.CoordinateSystemGlobal);
            _app.Model.Analyze.CreateAnalysisModel();
            _app.Model.SetPresentCoordSystem(CSiData.CoordinateSystemCustom);
            string[] elementNames;
            _app.Model.ObjectModel.CableObject.GetElement(CSiDataLine.NameObjectCable, out elementNames);

            Assert.That(elementNames.Length, Is.EqualTo(1));
            Assert.That(elementNames.Contains(CSiDataLine.NameElementCable));
        }

        [Test]
        public void GetElement_Multiple_Meshed()
        {
            // TODO: This is a hack to make the test pass due to an error with solid elements in the custom coordinate system. Remove coordinate switching.
            _app.Model.SetPresentCoordSystem(CSiData.CoordinateSystemGlobal);
            _app.Model.Analyze.CreateAnalysisModel();
            _app.Model.SetPresentCoordSystem(CSiData.CoordinateSystemCustom);
            string[] elementNames;
            _app.Model.ObjectModel.CableObject.GetElement(CSiDataLine.NameObjectCableMultiSegment, out elementNames);

            Assert.That(elementNames.Length, Is.EqualTo(1));
            Assert.That(elementNames.Contains(CSiDataLine.NameElementCableMultiSegment));
        }


#if BUILD_SAP2000v20
        [Test]
        public void GetGroupAssign() // Verification Incident 15096
        {
            string[] groupNames;

            _app.Model.ObjectModel.CableObject.GetGroupAssign(NameObject, out groupNames);

            Assert.That(groupNames.Length == 3);
            Assert.That(groupNames.Contains(CSiData.OldGroupNames[0]));
            Assert.That(groupNames.Contains(CSiData.OldGroupNames[1]));
            Assert.That(groupNames.Contains(CSiData.OldGroupNames[2] + " Cables"));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("NonExistingName")]
        public void GetGroupAssign_Using_NonExisting_Name_Throws_CSiException(string objectName) // Verification Incident 15096
        {
            string[] groupNames;

            Assert.That(() =>
            {
                _app.Model.ObjectModel.CableObject.GetGroupAssign(objectName, out groupNames);
            },
            Throws.Exception.TypeOf<CSiException>());
        }
#endif

        [Test]
        public void GetSection()
        {
            string propertyName;
            _app.Model.ObjectModel.CableObject.GetSection(CSiDataLine.NameObjectCable, out propertyName);

            Assert.That(propertyName, Is.EqualTo(CSiDataLine.NameSectionCable));
        }

        [Test]
        public void GetMass()
        {
            double masses;

            _app.Model.ObjectModel.CableObject.GetMass(CSiDataLine.NameObjectCable, out masses);

            Assert.That(masses, Is.EqualTo(0));
        }

        [Test]
        public void GetModifiers()
        {
            CableModifier modifier;
            _app.Model.ObjectModel.CableObject.GetModifiers(CSiDataLine.NameObjectCable, out modifier);

            Assert.That(modifier.CrossSectionalArea, Is.EqualTo(CSiDataLine.OldModifiersCable.CrossSectionalArea));
            Assert.That(modifier.MassModifier, Is.EqualTo(CSiDataLine.OldModifiersCable.MassModifier));
            Assert.That(modifier.WeightModifier, Is.EqualTo(CSiDataLine.OldModifiersCable.WeightModifier));
        }


        [Test]
        public void GetMaterialTemperature()
        {
            double temperature;
            string patternName;
            _app.Model.ObjectModel.CableObject.GetMaterialTemperature(CSiDataLine.NameObjectCable, out temperature, out patternName);

            Assert.That(temperature, Is.EqualTo(34));
            Assert.That(patternName, Is.EqualTo(string.Empty));
        }


        public void GetMaterialOverwrite(string name,
            ref string propertyName)
        {

        }


        public void GetInsertionPoint(string name,
            ref Displacements offsetDistancesI,
            ref Displacements offsetDistancesJ,
            ref bool isStiffnessTransformed,
            ref string coordinateSystem)
        {
          
        }


        
        public void GetCableData(string name,
            ref eCableGeometryDefinition cableType,
            ref int numberOfSegments,
            ref double weight,
            ref double projectedLoad,
            ref bool useDeformedGeometry,
            ref bool isModelUsingFrameElements,
            ref double[] cableParameters)
        {
          
        }

        
        public void GetCableGeometry(string name,
            ref int numberPoints,
            ref Coordinate3DCartesian[] coordinates,
            ref double[] verticalSag,
            ref double[] distanceAbsolute,
            ref double[] distanceRelative,
            string coordinateSystem = CoordinateSystems.Global)
        {
          
        }

        
        public void GetOutputStations(string name,
            ref eOutputStationType outputStationType,
            ref double maxStationSpacing,
            ref int minStationNumber,
            ref bool noOutputAndDesignAtElementEnds,
            ref bool noOutputAndDesignAtPointLoads)
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


        
        public void GetLoadTargetForce(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref double[] forceValues,
            ref double[] relativeForcesLocations,
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

        
        public void GetLoadDistributed(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref eLoadForceType[] forceTypes,
            ref eLoadDirection[] loadDirections,
            ref double[] loadValues,
            ref string[] coordinateSystems,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void GetLoadDistributedWithGUID(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref string[] GUIDs,
            ref eLoadForceType[] forceTypes,
            ref eLoadDirection[] loadDirections,
            ref double[] loadValues,
            ref string[] coordinateSystems,
            eItemType itemType = eItemType.Object)
        {
          
        }
    }
    
    
    [TestFixture]
    public class CableObject_Set : CsiSet
    {

        
        public void SetGUID(string name,
            string GUID = "")
        {
          
        }

        
        public void SetModifiers(string name, 
            AreaModifier modifiers)
        {
          
        }

        
        public void DeleteModifiers(string name,
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

        
        public void SetSelected(string name,
            bool isSelected,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetSection(string name, 
            string propertyName, 
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetMass(string name,
            double value,
            bool replace,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void DeleteMass(string name,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetInsertionPoint(string name,
            Displacements offsetDistancesI,
            Displacements offsetDistancesJ,
            bool isStiffnessTransformed,
            string coordinateSystem = CoordinateSystems.Global,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetMaterialTemperature(string name,
            double temperature,
            string patternName,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetMaterialOverwrite(string name,
            string propertyName,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetCableData(string name,
            eCableGeometryDefinition cableType,
            int numberOfSegments,
            double weight,
            double projectedLoad,
            double value,
            bool useDeformedGeometry = false,
            bool isModelUsingFrameElements = false)
        {
          
        }

        
        public void SetOutputStations(string name,
            eOutputStationType outputStationType,
            double maxStationSpacing,
            int minStationNumber,
            bool noOutputAndDesignAtElementEnds,
            bool noOutputAndDesignAtPointLoads,
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

        
        public void SetLoadTargetForce(string name,
            string loadPattern,
            double forceValue,
            double relativeForceLocation,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void DeleteLoadTargetForce(string name,
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

        
        public void SetLoadDistributed(string name,
            string loadPattern,
            eLoadForceType forceType,
            eLoadDirection loadDirection,
            double loadValue,
            string coordinateSystem = CoordinateSystems.Global,
            bool replace = true,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        
        public void DeleteLoadDistributed(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetLoadDistributedWithGUID(string name,
            string loadPattern,
            string GUID,
            eLoadForceType forceType,
            eLoadDirection loadDirection,
            double loadValue,
            string coordinateSystem = CoordinateSystems.Global,
            bool replace = true)
        {
          
        }

        
        public void DeleteLoadDistributedWithGUID(string name,
            string GUID)
        {
          
        }
    }
}
#endif