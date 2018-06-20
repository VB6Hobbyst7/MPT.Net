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
    public class AreaObject_Get : CsiGet
    {
        [Test]
        public void GetSelected_None_Selected_Returns_False()
        {
            bool isSelected;
            _app.Model.ObjectModel.AreaObject.GetSelected(CSiDataArea.NameObjectShellThin, out isSelected);

            Assert.IsFalse(isSelected);
        }


        public void GetSelectedEdge(string name,
            ref bool[] areEdgesSelected)
        {

        }

        [Test]
        public void Count()
        {
            int numberOfObjects = _app.Model.ObjectModel.AreaObject.Count();
            Assert.That(numberOfObjects, Is.EqualTo(CSiDataArea.NumberOfObjectsExpected));
        }

        [Test]
        public void GetNameList()
        {
            string[] names;
            _app.Model.ObjectModel.AreaObject.GetNameList(out names);
            
            Assert.That(names.Length, Is.EqualTo(CSiDataArea.NumberOfObjectsExpected));
            Assert.That(names.Contains(CSiDataArea.NameObjectShellThin));
            Assert.That(names.Contains(CSiDataArea.NameObjectShellThick));
            Assert.That(names.Contains(CSiDataArea.NameObjectShellLayered));
            Assert.That(names.Contains(CSiDataArea.NameObjectPlateThin));
            Assert.That(names.Contains(CSiDataArea.NameObjectPlateThick));
            Assert.That(names.Contains(CSiDataArea.NameObjectMembrane));
            Assert.That(names.Contains(CSiDataArea.NameObjectPlaneStrain));
            Assert.That(names.Contains(CSiDataArea.NameObjectPlaneStress));
            Assert.That(names.Contains(CSiDataArea.NameObjectASolid));
        }

        [Test]
        public void GetTransformationMatrix()
        {
            double[] directionCosines;
            _app.Model.ObjectModel.AreaObject.GetTransformationMatrix(CSiDataArea.NameObjectShellThin, out directionCosines);

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
            _app.Model.ObjectModel.AreaObject.GetTransformationMatrix(CSiDataArea.NameObjectShellThin, out directionCosines, isGlobal: false);

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
            Assert.That(directionCosines[8], Is.EqualTo(1));
        }

        [Test]
        public void GetPoints()
        {
            string[] points;
            _app.Model.ObjectModel.AreaObject.GetPoints(CSiDataArea.NameObjectShellThin, out points);

            Assert.That(points.Length, Is.EqualTo(4));
            Assert.That(points.Contains("6"));
            Assert.That(points.Contains("Point1"));
            Assert.That(points.Contains("12"));
            Assert.That(points.Contains("15"));
        }

        [Test]
        public void GetGUID()
        {
            string guid;
            _app.Model.ObjectModel.AreaObject.GetGUID(CSiDataArea.NameObjectShellThin, out guid);

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
            _app.Model.ObjectModel.AreaObject.GetElement(CSiDataArea.NameObjectShellThin, out elementNames);

            Assert.That(elementNames.Length, Is.EqualTo(1));
            Assert.That(elementNames.Contains(CSiDataArea.NameElementShellThin));
        }


        [Test]
        public void GetElement_Meshed()
        {
#if !BUILD_ETABS2016 && !BUILD_ETABS2017
            // TODO: This is a hack to make the test pass due to an error with solid elements in the custom coordinate system. Remove coordinate switching.
            _app.Model.SetPresentCoordSystem(CSiData.CoordinateSystemGlobal);
            _app.Model.Analyze.CreateAnalysisModel();
            _app.Model.SetPresentCoordSystem(CSiData.CoordinateSystemCustom);
#endif
            string[] elementNames;
            _app.Model.ObjectModel.AreaObject.GetElement(CSiDataArea.NameObjectShellThick, out elementNames);

            Assert.That(elementNames.Length, Is.EqualTo(4));
            Assert.That(elementNames.Contains(CSiDataArea.NameElementShellThick));
        }
#if BUILD_SAP2000v20
        [Test]
        public void GetGroupAssign() // Verification Incident 15096 
        {
            string objectName = "Area1";
            string[] groupNames;

            _app.Model.ObjectModel.AreaObject.GetGroupAssign(objectName, out groupNames);

            Assert.That(groupNames.Length == 3);
            Assert.That(groupNames.Contains(CSiData.OldGroupNames[0]));
            Assert.That(groupNames.Contains(CSiData.OldGroupNames[1]));
            Assert.That(groupNames.Contains(CSiData.OldGroupNames[2] + " Areas"));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("NonExistingName")]
        public void GetGroupAssign_Using_NonExisting_Name_Throws_CSiException(string objectName) // Verification Incident 15096
        {
            string[] groupNames;

            Assert.That(() =>
            {
                _app.Model.ObjectModel.AreaObject.GetGroupAssign(objectName, out groupNames);
            },
            Throws.Exception.TypeOf<CSiException>());
        }
#endif


        [Test]
        public void GetLocalAxes()
        {
            AngleLocalAxes angleOffset;
            bool isAdvanced;
            _app.Model.ObjectModel.AreaObject.GetLocalAxes(CSiDataArea.NameObjectShellThin, out angleOffset, out isAdvanced);

            Assert.IsFalse(isAdvanced);
            Assert.That(angleOffset.AngleA, Is.EqualTo(0));
            Assert.That(angleOffset.AngleB, Is.EqualTo(0));
            Assert.That(angleOffset.AngleC, Is.EqualTo(0));
        }

        [Test]
        public void GetSection()
        {
            string propertyName;
            _app.Model.ObjectModel.AreaObject.GetSection(CSiDataArea.NameObjectShellThin, out propertyName);

            Assert.That(propertyName, Is.EqualTo(CSiDataArea.NameSectionShellThin));
        }

        [Test]
        public void GetMass()
        {
            double masses;

            _app.Model.ObjectModel.AreaObject.GetMass(CSiDataArea.NameObjectShellThin, out masses);

            Assert.That(masses, Is.EqualTo(0));
        }


        [Test]
        public void GetModifiers()
        {
            AreaModifier modifier;
            _app.Model.ObjectModel.AreaObject.GetModifiers(CSiDataArea.NameObjectShellThin, out modifier);

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


        public void GetMaterialOverwrite(string name,
            ref string propertyName)
        {

        }


        public void GetEdgeConstraint(string name,
            ref bool constraintExists)
        {

        }
        

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

        //public void GetAllAreas(ref string[] areaNames,
        //    ref eAreaDesignOrientation[] designOrientations,
        //    ref int numberOfBoundaryPts,
        //    ref int[] pointDelimiters,
        //    ref string[] pointNames,
        //    ref Coordinate3DCartesian[] coordinates)
        //{
          
        //}

        
        //public void GetOpening(string name,
        //    ref bool isOpening)
        //{
          
        //}

        
        //public void GetDiaphragm(string name,
        //    ref string diaphragmName)
        //{
          
        //}
        
        
        //public void GetDesignOrientation(string name,
        //    ref eAreaDesignOrientation designOrientation)
        //{
          
        //}
        
        //public void GetPier(string name,
        //    ref string namePier)
        //{

        //}

        
        //public void GetSpandrel(string name,
        //    ref string nameSpandrel)
        //{
          
        //}

        
        //public void GetRebarDataPier(string name,
        //    ref string[] layerIds,
        //    ref eWallPierRebarLayerType[] layerTypes,
        //    ref double[] clearCovers,
        //    ref double[] barAreas,
        //    ref double[] barSpacings,
        //    ref int[] numberOfBars,
        //    ref bool[] isConfined,
        //    ref string[] barSizeNames,
        //    ref double[] endZoneLengths,
        //    ref double[] endZoneThicknesses,
        //    ref double[] endZoneOffsets)
        //{
          
        //}

        
        //public void GetRebarDataSpandrel(string name,
        //    ref string[] layerIds,
        //    ref eWallSpandrelRebarLayerType[] layerTypes,
        //    ref double[] clearCovers,
        //    ref double[] barAreas,
        //    ref double[] barSpacings,
        //    ref int[] numberOfBars,
        //    ref bool[] isConfined,
        //    ref int[] barSizeIndices)
        //{
            
            
        //}
#else
        

        [Test]
        public void GetMaterialTemperature()
        {
            double temperature;
            string patternName;
            _app.Model.ObjectModel.AreaObject.GetMaterialTemperature(CSiDataArea.NameObjectShellThin, out temperature, out patternName);

            Assert.That(temperature, Is.EqualTo(50));
            Assert.That(patternName, Is.EqualTo(CSiData.JointPattern));
        }


        public void GetLocalAxesAdvanced(string name,
            ref bool isActive,
            ref int plane2,
            ref eReferenceVector planeVectorOption,
            ref string planeCoordinateSystem,
            ref eReferenceVectorDirection[] planeVectorDirection,
            ref string[] planePoint,
            ref double[] planeReferenceVector)
        {
          
        }
        

        public void GetThickness(string name,
            ref eAreaThicknessType thicknessType,
            ref string thicknessPattern,
            ref double thicknessPatternScaleFactor,
            ref double[] thicknesses)
        {
          
        }


        public void GetNotionalSize(string name,
            ref eNotionalSizeType sizeType,
            ref double value)
        {
          
        }
        
        
        public void GetOffsets(string name,
            ref eAreaOffsetType offsetType,
            ref string offsetPattern,
            ref double offsetPatternScaleFactor,
            ref double[] offsets)
        {
          
        }

        
        public void GetAutoMesh(string name,
            ref eMeshType meshType,
            ref int numberOfObjectsAlongPoint12,
            ref int numberOfObjectsAlongPoint13,
            ref double maxSizeOfObjectsAlongPoint12,
            ref double maxSizeOfObjectsAlongPoint13,
            ref bool pointOnEdgeFromLine,
            ref bool pointOnEdgeFromPoint,
            ref bool extendCookieCutLines,
            ref double rotation,
            ref double maxSizeGeneral,
            ref bool localAxesOnEdge,
            ref bool localAxesOnFace,
            ref bool restraintsOnEdge,
            ref bool restraintsOnFace,
            ref string group,
            ref bool subMesh,
            ref double subMeshSize)
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
        
        
        public void GetLoadRotate(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref double[] rotateLoadValues,
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
        
        public void GetLoadTemperature(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref eLoadTemperatureType[] temperatureLoadType,
            ref double[] temperatureLoadValues,
            ref string[] jointPatternNames,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void GetLoadUniform(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref string[] coordinateSystems,
            ref eLoadDirection[] directionApplied,
            ref double[] uniformLoadValues,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        
        public void GetLoadUniformToFrame(string name,
            ref int numberItems,
            ref string[] loadPatterns,
            ref string[] areaNames,
            ref double[] values,
            ref eLoadDirection[] directions,
            ref eLoadDistributionType[] distributionTypes,
            ref string[] coordinateSystems,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void GetLoadWindPressure(string name,
            ref int numberItems,
            ref string[] areaNames,
            ref string[] loadPatterns,
            ref eWindPressureApplication[] windPressureTypes,
            ref double[] pressureCoefficients,
            eItemType itemType = eItemType.Object)
        {
          
        }
    }
    
    [TestFixture]
    public class AreaObject_Set : CsiSet
    {
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017

        
        public void SetOpening(string name,
            bool isOpening,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetDiaphragm(string name,
            string diaphragmName = "")
        {
          
        }

        
        public void SetPier(string name,
                string namePier,
                eItemType itemType = eItemType.Object)
        {
          
        }

        
        
        public void SetSpandrel(string name,
            string nameSpandrel,
            eItemType itemType = eItemType.Object)
        {
          
        }
#else
        
        public void SetLocalAxesAdvanced(string name,
            bool isActive,
            int plane2,
            eReferenceVector planeVectorOption,
            string planeCoordinateSystem,
            eReferenceVectorDirection[] planeVectorDirection,
            string[] planePoint,
            double[] planeReferenceVector,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetThickness(string name,
            eAreaThicknessType thicknessType,
            string thicknessPattern,
            double thicknessPatternScaleFactor,
            double[] thicknesses,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetNotionalSize(string name,
            eNotionalSizeType sizeType,
            double value)
        {
          
        }

        
        public void SetMaterialTemperature(string name,
            double temperature,
            string patternName,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetOffsets(string name,
            eAreaOffsetType offsetType,
            string offsetPattern,
            double offsetPatternScaleFactor,
            double[] offsets,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetAutoMesh(string name,
            eMeshType meshType,
            int numberOfObjectsAlongPoint12 = 2,
            int numberOfObjectsAlongPoint13 = 2,
            double maxSizeOfObjectsAlongPoint12 = 0,
            double maxSizeOfObjectsAlongPoint13 = 0,
            bool pointOnEdgeFromLine = false,
            bool pointOnEdgeFromPoint = false,
            bool extendCookieCutLines = false,
            double rotation = 0,
            double maxSizeGeneral = 0,
            bool localAxesOnEdge = false,
            bool localAxesOnFace = false,
            bool restraintsOnEdge = false,
            bool restraintsOnFace = false,
            string group = "ALL",
            bool subMesh = false,
            double subMeshSize = 0,
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

        
        public void SetLoadRotate(string name,
            string loadPattern,
            double rotateLoadValue)
        {
          
        }

        
        public void DeleteLoadRotate(string name,
            string loadPattern,
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

        
        public void SetLocalAxes(string name,
            AngleLocalAxes angleOffset,
            eItemType itemType = eItemType.Object)
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

        
        public void SetSelectedEdge(string name,
            int numberOfEdges,
            bool isEdgeSelected)
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
        
        
        public void SetMaterialOverwrite(string name,
            string propertyName,
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

        
        public void SetLoadTemperature(string name,
            string loadPattern,
            eLoadTemperatureType temperatureLoadType,
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

        
        public void SetLoadUniform(string name,
            string loadPattern,
            eLoadDirection directionApplied,
            double uniformLoadValue,
            string coordinateSystem = CoordinateSystems.Global,
            bool replace = true,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void DeleteLoadUniform(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetLoadUniformToFrame(string name,
            string loadPattern,
            double value,
            eLoadDirection direction,
            eLoadDistributionType distributionType,
            bool replace = true,
            string coordinateSystem = CoordinateSystems.Global,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        
        public void DeleteLoadUniformToFrame(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        
        public void SetLoadWindPressure(string name,
            string loadPattern,
            eWindPressureApplication windPressureType,
            double pressureCoefficient,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void DeleteLoadWindPressure(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object)
        {

        }
      
    }
}
