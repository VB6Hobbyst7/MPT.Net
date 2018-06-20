using System.Linq;
using NUnit.Framework;
using MPT.CSI.API.Core.Program;

using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel;
using MPT.CSI.API.Core.Support;
using eObjectType = MPT.CSI.API.Core.Program.ModelBehavior.Definition.eObjectType;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.ObjectModel
{
    [TestFixture]
    public class PointObject_Get : CsiGet
    {
        [Test]
        public void GetSelected_None_Selected_Returns_False()
        {
            bool isSelected;
            _app.Model.ObjectModel.PointObject.GetSelected(CSiDataPoint.NameObject, out isSelected);

            Assert.IsFalse(isSelected);
        }

        [Test]
        public void Count()
        {
            int numberOfObjects = _app.Model.ObjectModel.PointObject.Count();
            Assert.That(numberOfObjects, Is.EqualTo(CSiDataPoint.NumberOfObjectsExpected));
        }

        public void CountRestraint()
        {
          
        }
        
        
        public void CountSpring()
        {
          
        }

        
        public void CountPanelZone()
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

        
        public void GetCoordinate(string name,
            ref Coordinate3DCartesian coordinate,
            string coordinateSystem = CoordinateSystems.Global)
        {
          
        }
        
        
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
        
        
        public void GetConnectivity(string name,
            ref int numberItems,
            ref eObjectType[] objectTypes,
            ref string[] objectNames,
            ref int[] pointNumbers)
        {
          
        }


        
        public void GetCommonTo(string name,
            ref int commonTo)
        {
          
        }



        

        [Test]
        public void GetNameList()
        {
            string[] names;
            _app.Model.ObjectModel.PointObject.GetNameList(out names);

            Assert.That(names.Length, Is.EqualTo(CSiDataPoint.NumberOfObjectsExpected));
            Assert.That(names.Contains(CSiDataPoint.NameObject));
            Assert.That(names.Contains(CSiDataPoint.NameObjectMass));
            Assert.That(names.Contains(CSiDataPoint.NameObjectTransformedAxis));
            Assert.That(names.Contains(CSiDataPoint.NameObjectTransformedAxisAdvanced));
        }

        [Test]
        public void GetTransformationMatrix()
        {
            double[] directionCosines;
            _app.Model.ObjectModel.PointObject.GetTransformationMatrix(CSiDataPoint.NameObject, out directionCosines);

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
            _app.Model.ObjectModel.PointObject.GetTransformationMatrix(CSiDataPoint.NameObject, out directionCosines, isGlobal: false);

            Assert.That(directionCosines.Length, Is.EqualTo(9));

            // Row 1
            Assert.That(directionCosines[0], Is.EqualTo(0.707).Within(0.001));
            Assert.That(directionCosines[1], Is.EqualTo(0.707).Within(0.001));
            Assert.That(directionCosines[2], Is.EqualTo(0));

            // Row 2
            Assert.That(directionCosines[3], Is.EqualTo(-0.707).Within(0.001));
            Assert.That(directionCosines[4], Is.EqualTo(0.707).Within(0.001));
            Assert.That(directionCosines[5], Is.EqualTo(0));

            // Row 3
            Assert.That(directionCosines[6], Is.EqualTo(0));
            Assert.That(directionCosines[7], Is.EqualTo(0));
            Assert.That(directionCosines[8], Is.EqualTo(1).Within(0.001));
        }

        [Test]
        public void GetTransformationMatrix_Of_Transformed_Axis()
        {
            double[] directionCosines;
            _app.Model.ObjectModel.PointObject.GetTransformationMatrix(CSiDataPoint.NameObjectTransformedAxis, out directionCosines);

            Assert.That(directionCosines.Length, Is.EqualTo(9));

            // Row 1
            Assert.That(directionCosines[0], Is.EqualTo(0.785).Within(0.001));
            Assert.That(directionCosines[1], Is.EqualTo(0.022).Within(0.001));
            Assert.That(directionCosines[2], Is.EqualTo(0.619).Within(0.001));

            // Row 2
            Assert.That(directionCosines[3], Is.EqualTo(0.366).Within(0.001));
            Assert.That(directionCosines[4], Is.EqualTo(0.790).Within(0.001));
            Assert.That(directionCosines[5], Is.EqualTo(-0.491).Within(0.001));

            // Row 3
            Assert.That(directionCosines[6], Is.EqualTo(-0.500).Within(0.001));
            Assert.That(directionCosines[7], Is.EqualTo(0.612).Within(0.001));
            Assert.That(directionCosines[8], Is.EqualTo(0.612).Within(0.001));
        }

        [Test]
        public void GetTransformationMatrix_Of_Transformed_Axis_Advanced()
        {
            double[] directionCosines;
            _app.Model.ObjectModel.PointObject.GetTransformationMatrix(CSiDataPoint.NameObjectTransformedAxisAdvanced, out directionCosines);

            Assert.That(directionCosines.Length, Is.EqualTo(9));

            // Row 1
            Assert.That(directionCosines[0], Is.EqualTo(0.785).Within(0.001));
            Assert.That(directionCosines[1], Is.EqualTo(0.022).Within(0.001));
            Assert.That(directionCosines[2], Is.EqualTo(0.619).Within(0.001));

            // Row 2
            Assert.That(directionCosines[3], Is.EqualTo(0.366).Within(0.001));
            Assert.That(directionCosines[4], Is.EqualTo(0.790).Within(0.001));
            Assert.That(directionCosines[5], Is.EqualTo(-0.491).Within(0.001));

            // Row 3
            Assert.That(directionCosines[6], Is.EqualTo(-0.500).Within(0.001));
            Assert.That(directionCosines[7], Is.EqualTo(0.612).Within(0.001));
            Assert.That(directionCosines[8], Is.EqualTo(0.612).Within(0.001));
        }

        

        [Test]
        public void GetGUID()
        {
            string guid;
            _app.Model.ObjectModel.PointObject.GetGUID(CSiDataPoint.NameObject, out guid);

            Assert.That(string.IsNullOrEmpty(guid));
        }

        [Test]
        public void GetElement()
        {
#if !BUILD_ETABS2016 && !BUILD_ETABS2017
            // TODO: This is a hack to make the test pass due to an error with solid elements in the custom coordinate system. Remove coordinate switching.
            _app.Model.SetPresentCoordSystem(CSiData.CoordinateSystemGlobal);
            _app.Model.Analyze.CreateAnalysisModel();
            _app.Model.SetPresentCoordSystem(CSiData.CoordinateSystemCustom);
#endif
            string[] elementNames;
            _app.Model.ObjectModel.PointObject.GetElement(CSiDataPoint.NameObject, out elementNames);

            Assert.That(elementNames.Length, Is.EqualTo(1));
            Assert.That(elementNames.Contains(CSiDataPoint.NameObject));
        }


#if BUILD_SAP2000v20
        [Test]
        public void GetGroupAssign() // Verification Incident 15096
        {
            string objectName = "Point1";
            string[] groupNames;

            _app.Model.ObjectModel.PointObject.GetGroupAssign(objectName, out groupNames);

            Assert.That(groupNames.Length == 3);
            Assert.That(groupNames.Contains(CSiData.OldGroupNames[0]));
            Assert.That(groupNames.Contains(CSiData.OldGroupNames[1]));
            Assert.That(groupNames.Contains(CSiData.OldGroupNames[2] + " Points"));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("NonExistingName")]
        public void GetGroupAssign_Using_NonExisting_Name_Throws_CSiException(string objectName) // Verification Incident 15096
        {
            string[] groupNames;

            Assert.That(() =>
            {
                _app.Model.ObjectModel.PointObject.GetGroupAssign(objectName, out groupNames);
            },
            Throws.Exception.TypeOf<CSiException>());
        }
#endif
        
        [Test]
        public void GetLocalAxes()
        {
            AngleLocalAxes angleOffset;
            bool isAdvanced;
            _app.Model.ObjectModel.PointObject.GetLocalAxes(CSiDataPoint.NameObject, out angleOffset, out isAdvanced);

            Assert.IsFalse(isAdvanced);
            Assert.That(angleOffset.AngleA, Is.EqualTo(0));
            Assert.That(angleOffset.AngleB, Is.EqualTo(0));
            Assert.That(angleOffset.AngleC, Is.EqualTo(0));
        }

        [Test]
        public void GetLocalAxes_Of_Transformed_Axis()
        {
            AngleLocalAxes angleOffset;
            bool isAdvanced;
            _app.Model.ObjectModel.PointObject.GetLocalAxes(CSiDataPoint.NameObjectTransformedAxis, out angleOffset, out isAdvanced);

            Assert.IsFalse(isAdvanced);
            Assert.That(angleOffset.AngleA, Is.EqualTo(25));
            Assert.That(angleOffset.AngleB, Is.EqualTo(30));
            Assert.That(angleOffset.AngleC, Is.EqualTo(45));
        }

        [Test]
        public void GetLocalAxes_Of_Transformed_Axis_Advanced()
        {
            AngleLocalAxes angleOffset;
            bool isAdvanced;
            _app.Model.ObjectModel.PointObject.GetLocalAxes(CSiDataPoint.NameObjectTransformedAxisAdvanced, out angleOffset, out isAdvanced);

            Assert.IsTrue(isAdvanced);
            Assert.That(angleOffset.AngleA, Is.EqualTo(25));
            Assert.That(angleOffset.AngleB, Is.EqualTo(30));
            Assert.That(angleOffset.AngleC, Is.EqualTo(45));
        }

        [Test]
        public void GetMass_Has_No_Mass()
        {
            Mass masses;

            _app.Model.ObjectModel.PointObject.GetMass(CSiDataPoint.NameObject, out masses);

            Assert.That(masses.U1, Is.EqualTo(0));
            Assert.That(masses.U2, Is.EqualTo(0));
            Assert.That(masses.U3, Is.EqualTo(0));
            Assert.That(masses.R1, Is.EqualTo(0));
            Assert.That(masses.R2, Is.EqualTo(0));
            Assert.That(masses.R3, Is.EqualTo(0));
        }

        [Test]
        public void GetMass_Has_Mass()
        {
            Mass masses;

            _app.Model.ObjectModel.PointObject.GetMass(CSiDataPoint.NameObjectMass, out masses);

            Assert.That(masses.U1, Is.EqualTo(1));
            Assert.That(masses.U2, Is.EqualTo(2));
            Assert.That(masses.U3, Is.EqualTo(3));
            Assert.That(masses.R1, Is.EqualTo(4));
            Assert.That(masses.R2, Is.EqualTo(5));
            Assert.That(masses.R3, Is.EqualTo(6));
        }

        public void GetSpecialPoint(string name,
            ref bool isSpecialPoint)
        {
          
        }
        
       
        public void GetPanelZone(string name,
            ref ePanelZonePropertyType propertyType,
            ref double thickness,
            ref double k1,
            ref double k2,
            ref string linkProperty,
            ref ePanelZoneConnectivity connectivity,
            ref ePanelZoneLocalAxis localAxisFrom,
            ref double localAxisAngle)
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

        
        public void GetLoadForce(string name,
            ref int numberItems,
            ref string[] pointNames,
            ref string[] loadPatterns,
            ref int[] loadPatternSteps,
            ref string[] coordinateSystem,
            ref Loads[] forces,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        
        

        public void GetLoadDisplacement(string name,
            ref int numberItems,
            ref string[] pointNames,
            ref string[] loadPatterns,
            ref int[] loadPatternSteps,
            ref string[] coordinateSystem,
            ref Displacements[] displacements,
            eItemType itemType = eItemType.Object)
        {
          
        }
   
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        public void CountConstraint(string name = "")
        {
          
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
        

        public void GetMergeNumber(string name,
            ref int mergeNumber)
        {
          
        }
        
        
        public void GetPatternValue(string name,
            string patternName,
            ref double value)
        {
          
        }
        
        
        public void GetLoadForceWithGUID(string name, 
            ref int numberItems, 
            ref string[] pointNames, 
            ref string[] loadPatterns, 
            ref string[] GUIDs, 
            ref int[] loadPatternSteps,
            ref string[] coordinateSystem, 
            ref Loads[] forces, 
            eItemType itemType = eItemType.Object)
        {
          
        }
#endif
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017

        public void GetNameListOnStory(string storyName,
                        ref string[] names)
        {
          
        }

        
        public void GetLabelFromName(string name,
            ref string label,
            ref string story)
        {
          
        }

        
        public void GetLabelNameList(ref string[] names,
            ref string[] labels,
            ref string[] stories)
        {
          
        }

        
        public void GetNameFromLabel(string label,
            string story,
            ref string name)
        {
          
        }

        public void GetAllPoints(ref string[] names,
            ref Coordinate3DCartesian[] coordinates,
            string coordinateSystem = CoordinateSystems.Global)
        {
          
        }
        

        public void GetDiaphragm(string name,
            ref eDiaphragmOption diaphragmOption,
            ref string diaphragmName)
        {
          
        }
#else
  
        public void GetConstraint(string name,
            ref int numberItems,
            ref string[] pointNames,
            ref string[] constraintNames,
            eItemType itemType = eItemType.Object)
        {
          
        }
#endif
#if BUILD_ETABS2016 || BUILD_ETABS2017

        public void GetSpringAssignment(string name,
            ref string nameSpring)
        {
          
        }
#endif

    }

    [TestFixture]
    public class PointObject_Set : CsiSet
    { 
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

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
        
        public void AddByCoordinate(Coordinate3DCylindrical coordinate,
            ref string name,
            string userName = "",
            string coordinateSystem = CoordinateSystems.Global,
            bool mergeOff = false,
            int mergeNumber = 0)
        {
          
        }

        
        public void AddByCoordinate(Coordinate3DSpherical coordinate,
            ref string name,
            string userName = "",
            string coordinateSystem = CoordinateSystems.Global,
            bool mergeOff = false,
            int mergeNumber = 0)
        {
          
        }

        
        public void SetMergeNumber(string name,
            int mergeNumber,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetPatternByPressure(string name,
            string patternName,
            double zCoordinateAtZeroPressure,
            double weightPerUnitVolume,
            double uniformForcePerUnitArea,
            ePatternRestriction restrictionCurrent,
            ePatternRestriction restrictionCombined = ePatternRestriction.AllValuesUsed,
            bool replace = false,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetPatternByCoordinates(string name,
            string patternName,
            double a,
            double b,
            double c,
            double d,
            ePatternRestriction restrictionCombined = ePatternRestriction.AllValuesUsed,
            bool replace = false,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void DeletePatternValue(string name,
            string patternName,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetLoadForceWithGUID(string name,
            string loadPattern,
            Loads force,
            string GUID,
            bool replace = false,
            string coordinateSystem = CoordinateSystems.Global)
        {
          
        }
        
        public void DeleteLoadForceWithGUID(string name,
            string GUID)
        {
          
        }
#endif
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        
        public void SetDiaphragm(string name,
            eDiaphragmOption diaphragmOption,
            string diaphragmName = "")
        {
          
        }
#else
        public void SetConstraint(string name,
            string constraintName,
            bool replace = true,
            eItemType itemType = eItemType.Group)
        {
          
        }

        
        public void DeleteConstraint(string name,
            eItemType itemType = eItemType.Object)
        {
          
        }
#endif
#if BUILD_ETABS2016 || BUILD_ETABS2017
        public void SetSpringAssignment(string name,
            string nameSpring,
            eItemType itemType = eItemType.Object)
        {
          
        }
#endif
    
        public void SetGUID(string name,
            string GUID = "")
        {
          
        }
        
        
        
        
        public void ChangeName(string currentName, string newName)
        {
          
        }

        
        public void Delete(string name)
        {
          
        }

        
        public void AddByCoordinate(Coordinate3DCartesian coordinate,
            ref string name,
            string userName = "",
            string coordinateSystem = CoordinateSystems.Global,
            bool mergeOff = false,
            int mergeNumber = 0)
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

        
        public void SetMass(string name,
            Mass masses,
            bool isLocalCoordinateSystem = true,
            bool replace = false,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        public void SetMass(string name,
            MassVolume masses,
            string materialProperty,
            bool isLocalCoordinateSystem = true,
            bool replace = false,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetMass(string name,
            MassWeight masses,
            bool isLocalCoordinateSystem = true,
            bool replace = false,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void DeleteMass(string name,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        
        public void SetSpecialPoint(string name,
            bool isSpecialPoint,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        public void DeleteSpecialPoint(string name,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetPanelZone(string name,
            ePanelZonePropertyType propertyType,
            double thickness,
            double k1,
            double k2,
            string linkProperty,
            ePanelZoneConnectivity connectivity,
            ePanelZoneLocalAxis localAxisFrom,
            double localAxisAngle,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void DeletePanelZone(string name,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetRestraint(string name,
            DegreesOfFreedomLocal degreesOfFreedom,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void DeleteRestraint(string name,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetSpring(string name,
            Stiffness stiffnesses,
            bool isLocalCoordinateSystem = false,
            bool replace = false,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        
        public void SetSpringCoupled(string name,
            StiffnessCoupled stiffnesses,
            bool isLocalCoordinateSystem = false,
            bool replace = false,
            eItemType itemType = eItemType.Object)
        {
          
        }
        

        public void DeleteSpring(string name,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetLoadForce(string name,
            string loadPattern,
            Loads force,
            bool replace = false, 
            string coordinateSystem = CoordinateSystems.Global,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void DeleteLoadForce(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetLoadDisplacement(string name,
            string loadPattern,
            Loads force,
            bool replace = false,
            string coordinateSystem = CoordinateSystems.Global,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        
        public void DeleteLoadDisplacement(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
          
        }
      
    }
}
