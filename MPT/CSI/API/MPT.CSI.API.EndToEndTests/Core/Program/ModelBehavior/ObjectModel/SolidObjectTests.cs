#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using System.Linq;
using NUnit.Framework;
using MPT.CSI.API.Core.Program;

using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.ObjectModel
{
    [TestFixture]
    public class SolidObject_Get : CsiGet
    {
        #region Query
        [Test]
        public void GetSelected_None_Selected_Returns_False()
        {
            bool isSelected;
            _app.Model.ObjectModel.SolidObject.GetSelected(CSiDataSolid.NameObject, out isSelected);

            Assert.IsFalse(isSelected);
        }

        [Test]
        public void Count()
        {
            int numberOfObjects = _app.Model.ObjectModel.SolidObject.Count();
            Assert.That(numberOfObjects, Is.EqualTo(CSiDataSolid.NumberOfObjectsExpected));
        }

        [Test]
        public void GetNameList()
        {
            string[] names;
            _app.Model.ObjectModel.SolidObject.GetNameList(out names);

            Assert.That(names.Length, Is.EqualTo(CSiDataSolid.NumberOfObjectsExpected));
            Assert.That(names.Contains(CSiDataSolid.NameObject));
        }

        [Test]
        public void GetTransformationMatrix()
        {
            double[] directionCosines;
            _app.Model.ObjectModel.SolidObject.GetTransformationMatrix(CSiDataSolid.NameObject, out directionCosines);

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
        public void GetTransformationMatrix_From_Current_Global_Coordinate_System()
        {
            double[] directionCosines;
            _app.Model.ObjectModel.SolidObject.GetTransformationMatrix(CSiDataSolid.NameObject, out directionCosines, isGlobal: false);

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
            _app.Model.ObjectModel.SolidObject.GetPoints(CSiDataSolid.NameObject, out points);

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
        public void GetGUID()
        {
            string guid;
            _app.Model.ObjectModel.SolidObject.GetGUID(CSiDataSolid.NameObject, out guid);

            Assert.That(string.IsNullOrEmpty(guid));
        }
        
        [Test]
        public void GetElement()
        {
            // TODO: This is a hack to make the test pass due to an error with solid elements in the custom coordinate system. Remove coordinate switching.
            _app.Model.SetPresentCoordSystem(CSiData.CoordinateSystemGlobal);
            _app.Model.Analyze.CreateAnalysisModel();
            _app.Model.SetPresentCoordSystem(CSiData.CoordinateSystemCustom);
            string[] elementNames;
            _app.Model.ObjectModel.SolidObject.GetElement(CSiDataSolid.NameObject, out elementNames);

            Assert.That(elementNames.Length, Is.EqualTo(1));
            Assert.That(elementNames.Contains(CSiDataSolid.NameElement));
        }

#if BUILD_SAP2000v20
        [Test]
        public void GetGroupAssign() // Verification Incident 15096
        {
            string[] groupNames;

            _app.Model.ObjectModel.SolidObject.GetGroupAssign(NameObject, out groupNames);

            Assert.That(groupNames.Length == 3);
            Assert.That(groupNames.Contains(CSiData.OldGroupNames[0]));
            Assert.That(groupNames.Contains(CSiData.OldGroupNames[1]));
            Assert.That(groupNames.Contains(CSiData.OldGroupNames[2] + " Solids"));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("NonExistingName")]
        public void GetGroupAssign_Using_NonExisting_Name_Throws_CSiException(string objectName) // Verification Incident 15096
        {
            string[] groupNames;

            Assert.That(() =>
            {
                _app.Model.ObjectModel.SolidObject.GetGroupAssign(objectName, out groupNames);
            },
            Throws.Exception.TypeOf<CSiException>());
        }
#endif
        public void GetAutoMesh(string name,
            ref eMeshType meshType,
            ref int numberOfObjectsAlongPoint12,
            ref int numberOfObjectsAlongPoint13,
            ref int numberOfObjectsAlongPoint15,
            ref double maxSizeOfObjectsAlongPoint12,
            ref double maxSizeOfObjectsAlongPoint13,
            ref double maxSizeOfObjectsAlongPoint15,
            ref bool restraintsOnEdge,
            ref bool restraintsOnFace)
        {

        }


        public void GetSpring(string name,
            ref eSpringType[] springTypes,
            ref double[] stiffnesses,
            ref eSpringSimpleType[] springSimpleTypes,
            ref string[] linkProperties,
            ref eFace[] faces,
            ref eSpringLocalOneType[] springLocalOneTypes,
            ref int[] directions,
            ref bool[] areOutward,
            ref double[] vectorComponentsX,
            ref double[] vectorComponentsY,
            ref double[] vectorComponentsZ,
            ref double[] angleOffsets,
            ref string[] coordinateSystems)
        {

        }


        public void GetEdgeConstraint(string name,
            ref bool constraintExists)
        {

        }
        #endregion

        #region Axes

        [Test]
        public void GetLocalAxes()
        {
            AngleLocalAxes angleOffset;
            bool isAdvanced;
            _app.Model.ObjectModel.SolidObject.GetLocalAxes(CSiDataSolid.NameObject, out angleOffset, out isAdvanced);

            Assert.IsFalse(isAdvanced);
            Assert.That(angleOffset.AngleA, Is.EqualTo(0));
            Assert.That(angleOffset.AngleB, Is.EqualTo(0));
            Assert.That(angleOffset.AngleC, Is.EqualTo(0));
        }

        
        public void GetLocalAxesAdvanced(string name,
            ref bool isActive,
            ref int plane2,
            ref eReferenceVector axisVectorOption,
            ref string axisCoordinateSystem,
            ref eReferenceVectorDirection[] axisVectorDirection,
            ref string[] axisPoint,
            ref double[] axisReferenceVector,
            ref int localPlaneByReferenceVector,
            ref eReferenceVector planeVectorOption,
            ref string planeCoordinateSystem,
            ref eReferenceVectorDirection[] planeVectorDirection,
            ref string[] planePoint,
            ref double[] planeReferenceVector)
        {
          
        }
        #endregion

        #region Cross-Section & Material Properties
        [Test]
        public void GetSection()
        {
            string propertyName;
            _app.Model.ObjectModel.SolidObject.GetSection(CSiDataSolid.NameObject, out propertyName);

            Assert.That(propertyName, Is.EqualTo(CSiDataSolid.NameSection));
        }

        [Test]
        public void GetMaterialTemperature()
        {
            double temperature;
            string patternName;
            _app.Model.ObjectModel.SolidObject.GetMaterialTemperature(CSiDataSolid.NameObject, out temperature, out patternName);

            Assert.That(temperature, Is.EqualTo(98));
            Assert.That(patternName, Is.EqualTo(CSiData.JointPattern));
        }

        #endregion

        #region Loads
   
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

        
        public void GetLoadPorePressure(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref double[] porePressureLoadValues,
            ref string[] jointPatternNames,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void GetLoadSurfacePressure(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref eFace[] faceApplied,
            ref double[] surfacePressureLoadValues,
            ref string[] jointPatternNames,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void GetLoadStrain(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref eStrainComponent[] components,
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
        #endregion
    }
    
        
        
    [TestFixture]
    public class SolidObject_Set : CsiSet
    {
        
        
        public void SetGUID(string name,
            string GUID = "")
        {
          
        }

        
        public void SetLocalAxes(string name,
            AngleLocalAxes angleOffset,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        
        public void SetLocalAxesAdvanced(string name,
            bool isActive,
            int plane2,
            eReferenceVector axisVectorOption,
            string axisCoordinateSystem,
            eReferenceVectorDirection[] axisVectorDirection,
            string[] axisPoint,
            double[] axisReferenceVector,
            int localPlaneByReferenceVector,
            eReferenceVector planeVectorOption,
            string planeCoordinateSystem,
            eReferenceVectorDirection[] planeVectorDirection,
            string[] planePoint,
            double[] planeReferenceVector,
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

        
        public void SetMaterialTemperature(string name,
            double temperature,
            string patternName,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetAutoMesh(string name,
            eMeshType meshType,
            int numberOfObjectsAlongPoint12 = 2,
            int numberOfObjectsAlongPoint13 = 2,
            int numberOfObjectsAlongPoint15 = 2,
            double maxSizeOfObjectsAlongPoint12 = 0,
            double maxSizeOfObjectsAlongPoint13 = 0,
            double maxSizeOfObjectsAlongPoint15 = 0,
            bool restraintsOnEdge = false,
            bool restraintsOnFace = false,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        
        public void SetSpring(string name,
            eSpringType springType,
            double stiffness,
            eSpringSimpleType springSimpleType,
            string linkProperty,
            eFace face,
            eSpringLocalOneType springLocalOneType,
            int direction,
            bool isOutward,
            Coordinate3DCartesian vector,
            double angleOffset,
            bool replace,
            string coordinateSystem = CoordinateSystems.Local,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        
        public void DeleteSpring(string name,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetEdgeConstraint(string name,
            bool constraintExists,
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

        
        public void SetLoadPorePressure(string name,
            string loadPattern,
            double porePressureLoadValue,
            string jointPatternName,
            bool replace = true,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void DeleteLoadPorePressure(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetLoadSurfacePressure(string name,
            string loadPattern,
            eFace faceApplied,
            double surfacePressureLoadValue,
            string jointPatternName,
            bool replace = true,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void DeleteLoadSurfacePressure(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetLoadStrain(string name,
            string loadPattern,
            eStrainComponent component,
            double strainLoadValue,
            string jointPatternName,
            bool replace = true,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void DeleteLoadStrain(string name,
            string loadPattern,
            eStrainComponent component,
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
    }
}
#endif