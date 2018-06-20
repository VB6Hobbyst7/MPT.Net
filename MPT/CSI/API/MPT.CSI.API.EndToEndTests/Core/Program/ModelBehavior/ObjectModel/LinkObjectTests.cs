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
    public class LinkObject_Get : CsiGet
    {
        #region Query
        [Test]
        public void GetSelected_None_Selected_Returns_False()
        {
            bool isSelected;
            _app.Model.ObjectModel.LinkObject.GetSelected(CSiDataLink.NameObjectTwoPoints, out isSelected);

            Assert.IsFalse(isSelected);
        }

        [Test]
        public void Count()
        {
            int numberOfObjects = _app.Model.ObjectModel.LinkObject.Count();
            Assert.That(numberOfObjects, Is.EqualTo(CSiDataLink.NumberOfObjectsExpected));
        }

        [Test]
        public void GetNameList()
        {
            string[] names;
            _app.Model.ObjectModel.LinkObject.GetNameList(out names);

            Assert.That(names.Length, Is.EqualTo(CSiDataLink.NumberOfObjectsExpected));
            Assert.That(names.Contains(CSiDataLink.NameObjectSinglePoint));
            Assert.That(names.Contains(CSiDataLink.NameObjectTwoPoints));
        }

        [Test]
        public void GetTransformationMatrix()
        {
            double[] directionCosines;
            _app.Model.ObjectModel.LinkObject.GetTransformationMatrix(CSiDataLink.NameObjectTwoPoints, out directionCosines);

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
        public void GetTransformationMatrix_From_Current_Global_Coordinate_System()
        {
            double[] directionCosines;
            _app.Model.ObjectModel.LinkObject.GetTransformationMatrix(CSiDataLink.NameObjectTwoPoints, out directionCosines, isGlobal: false);

            Assert.That(directionCosines.Length, Is.EqualTo(9));

            // Row 1
            Assert.That(directionCosines[0], Is.EqualTo(0.632).Within(0.001));
            Assert.That(directionCosines[1], Is.EqualTo(-0.316).Within(0.001));
            Assert.That(directionCosines[2], Is.EqualTo(-0.707).Within(0.001));

            // Row 2
            Assert.That(directionCosines[3], Is.EqualTo(-0.632).Within(0.001));
            Assert.That(directionCosines[4], Is.EqualTo(0.316).Within(0.001));
            Assert.That(directionCosines[5], Is.EqualTo(-0.707).Within(0.001));

            // Row 3
            Assert.That(directionCosines[6], Is.EqualTo(0.447).Within(0.001));
            Assert.That(directionCosines[7], Is.EqualTo(0.894).Within(0.001));
            Assert.That(directionCosines[8], Is.EqualTo(0).Within(0.001));
        }

        [Test]
        public void GetPoints_Single_Joint_Link()
        {
            string[] points;
            _app.Model.ObjectModel.LinkObject.GetPoints(CSiDataLink.NameObjectSinglePoint, out points);

            Assert.That(points.Length, Is.EqualTo(CSiDataLink.SinglePointJoints.Length));
            Assert.That(points.Contains(CSiDataLink.SinglePointJoints[1]));
        }

        [Test]
        public void GetPoints_2_Joint_Link()
        {
            string[] points;
            _app.Model.ObjectModel.LinkObject.GetPoints(CSiDataLink.NameObjectTwoPoints, out points);

            Assert.That(points.Length, Is.EqualTo(CSiDataLink.TwoPointsJoints.Length));
            Assert.That(points.Contains(CSiDataLink.TwoPointsJoints[0]));
            Assert.That(points.Contains(CSiDataLink.TwoPointsJoints[1]));
        }

        [Test]
        public void GetGUID()
        {
            string guid;
            _app.Model.ObjectModel.LinkObject.GetGUID(CSiDataLink.NameObjectTwoPoints, out guid);

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
            _app.Model.ObjectModel.LinkObject.GetElement(CSiDataLink.NameObjectTwoPoints, out elementNames);

            Assert.That(elementNames.Length, Is.EqualTo(1));
            Assert.That(elementNames.Contains(CSiDataLink.NameElementTwoPoints));
        }


#if BUILD_SAP2000v20
        [TestCase("Link1Point1")]
        [TestCase("Link2Points1")]
        public void GetGroupAssign(string objectName) // Verification Incident 15096
        {
            string[] groupNames;

            _app.Model.ObjectModel.LinkObject.GetGroupAssign(objectName, out groupNames);

            Assert.That(groupNames.Length == 3);
            Assert.That(groupNames.Contains(CSiData.OldGroupNames[0]));
            Assert.That(groupNames.Contains(CSiData.OldGroupNames[1]));
            Assert.That(groupNames.Contains(CSiData.OldGroupNames[2] + " Links"));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("NonExistingName")]
        public void GetGroupAssign_Using_NonExisting_Name_Throws_CSiException(string objectName) // Verification Incident 15096
        {
            string[] groupNames;

            Assert.That(() =>
            {
                _app.Model.ObjectModel.LinkObject.GetGroupAssign(objectName, out groupNames);
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

#region Cross-Section & Material Properties
        [Test]
        public void GetSection()
        {
            string propertyName;
            _app.Model.ObjectModel.LinkObject.GetSection(CSiDataLink.NameObjectTwoPoints, out propertyName);

            Assert.That(propertyName, Is.EqualTo(CSiDataLink.NameSectionMultiLinearElastic));
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        public void GetSectionFrequencyDependent(string name,
            ref string propertyName)
        {

        }
#endif

#endregion

#region Axes


        [Test]
        public void GetLocalAxes()
        {
            AngleLocalAxes angleOffset;
            bool isAdvanced;
            _app.Model.ObjectModel.LinkObject.GetLocalAxes(CSiDataLink.NameObjectTwoPoints, out angleOffset, out isAdvanced);

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

#region Loads
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
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
            ref DegreesOfFreedomLocal[] degreesOfFreedom,
            ref Deformations[] deformations,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void GetLoadTargetForce(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref ForcesActive[] forcesActive,
            ref Forces[] forcesValues,
            ref Forces[] relativeForcesLocations,
            eItemType itemType = eItemType.Object)
        {
          
        }
#endif
#endregion
    }

    [TestFixture]
    public class LinkObject_Set : CsiSet
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
        
        
        public void AddByCoordinate(ref Coordinate3DCartesian[] coordinates,
            ref string name,
            bool isSingleJoint,
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
        
        
        public void AddByPoint(string[] pointNames,
            ref string name,
            bool isSingleJoint,
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
        
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        
        
        public void SetSectionFrequencyDependent(string name,
            string propertyName,
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
            DegreesOfFreedomLocal degreesOfFreedom,
            Deformations deformations,
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
            ForcesActive forcesActive,
            Forces forceValues,
            Forces relativeForcesLocation,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void DeleteLoadTargetForce(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
          
        }
#endif
    }
}
