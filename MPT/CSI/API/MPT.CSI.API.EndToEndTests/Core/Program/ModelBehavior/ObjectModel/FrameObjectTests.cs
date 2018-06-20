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
    public class FrameObject_Get : CsiGet
    {
        [Test]
        public void GetSelected_None_Selected_Returns_False()
        {
            bool isSelected;
            _app.Model.ObjectModel.FrameObject.GetSelected(CSiDataLine.NameObjectFrame, out isSelected);

            Assert.IsFalse(isSelected);
        }

        [Test]
        public void Count()
        {
            int numberOfObjects = _app.Model.ObjectModel.FrameObject.Count();
            Assert.That(numberOfObjects, Is.EqualTo(CSiDataLine.NumberOfObjectFramesExpected));
        }

        [Test]
        public void GetNameList()
        {
            string[] names;
            _app.Model.ObjectModel.FrameObject.GetNameList(out names);

            Assert.That(names.Length, Is.EqualTo(CSiDataLine.NumberOfObjectFramesExpected));
            Assert.That(names.Contains(CSiDataLine.NameObjectFrame));
        }

        [Test]
        public void GetTransformationMatrix()
        {
            double[] directionCosines;
            _app.Model.ObjectModel.FrameObject.GetTransformationMatrix(CSiDataLine.NameObjectFrame, out directionCosines);

            Assert.That(directionCosines.Length, Is.EqualTo(9));

            // Row 1
            Assert.That(directionCosines[0], Is.EqualTo(0));
            Assert.That(directionCosines[1], Is.EqualTo(1));
            Assert.That(directionCosines[2], Is.EqualTo(0));

            // Row 2
            Assert.That(directionCosines[3], Is.EqualTo(0));
            Assert.That(directionCosines[4], Is.EqualTo(0));
            Assert.That(directionCosines[5], Is.EqualTo(1));

            // Row 3
            Assert.That(directionCosines[6], Is.EqualTo(1));
            Assert.That(directionCosines[7], Is.EqualTo(0));
            Assert.That(directionCosines[8], Is.EqualTo(0));
        }

        [Test]
        public void GetTransformationMatrix_From_Current_Global_Coordinate_System()
        {
            double[] directionCosines;
            _app.Model.ObjectModel.FrameObject.GetTransformationMatrix(CSiDataLine.NameObjectFrame, out directionCosines, isGlobal: false);

            Assert.That(directionCosines.Length, Is.EqualTo(9));

            // Row 1
            Assert.That(directionCosines[0], Is.EqualTo(0));
            Assert.That(directionCosines[1], Is.EqualTo(0.707).Within(0.001));
            Assert.That(directionCosines[2], Is.EqualTo(0.707).Within(0.001));

            // Row 2
            Assert.That(directionCosines[3], Is.EqualTo(0));
            Assert.That(directionCosines[4], Is.EqualTo(-0.707).Within(0.001));
            Assert.That(directionCosines[5], Is.EqualTo(0.707).Within(0.001));

            // Row 3
            Assert.That(directionCosines[6], Is.EqualTo(1).Within(0.001));
            Assert.That(directionCosines[7], Is.EqualTo(0));
            Assert.That(directionCosines[8], Is.EqualTo(0));
        }

        [Test]
        public void GetPoints()
        {
            string[] points;
            _app.Model.ObjectModel.FrameObject.GetPoints(CSiDataLine.NameObjectFrame, out points);

            Assert.That(points.Length, Is.EqualTo(2));
            Assert.That(points.Contains("Point1"));
            Assert.That(points.Contains("2"));
        }

        [Test]
        public void GetGUID()
        {
            string guid;
            _app.Model.ObjectModel.FrameObject.GetGUID(CSiDataLine.NameObjectFrame, out guid);

            Assert.That(!string.IsNullOrEmpty(guid));
        }

        /// <exclude />
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
            _app.Model.ObjectModel.FrameObject.GetElement(CSiDataLine.NameObjectFrame, out elementNames);

            Assert.That(elementNames.Length, Is.EqualTo(2));
            Assert.That(elementNames.Contains(CSiDataLine.NameElementFrame));
            Assert.That(elementNames.Contains(CSiDataLine.NameElementFrame2));
        }


#if BUILD_SAP2000v20
        [Test]
        public void GetGroupAssign() // Verification Incident 15096
        {
            string objectName = "Frame1";
            string[] groupNames;

            _app.Model.ObjectModel.FrameObject.GetGroupAssign(objectName, out groupNames);

            Assert.That(groupNames.Length == 3);
            Assert.That(groupNames.Contains(CSiData.OldGroupNames[0]));
            Assert.That(groupNames.Contains(CSiData.OldGroupNames[1]));
            Assert.That(groupNames.Contains(CSiData.OldGroupNames[2] + " Frames"));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("NonExistingName")]
        public void GetGroupAssign_Using_NonExisting_Name_Throws_CSiException(string objectName) // Verification Incident 15096
        {
            string[] groupNames;

            Assert.That(() =>
            {
                _app.Model.ObjectModel.FrameObject.GetGroupAssign(objectName, out groupNames);
            },
            Throws.Exception.TypeOf<CSiException>());
        }
#endif

        [Test]
        public void GetLocalAxes()
        {
            AngleLocalAxes angleOffset;
            bool isAdvanced;
            _app.Model.ObjectModel.FrameObject.GetLocalAxes(CSiDataLine.NameObjectFrame, out angleOffset, out isAdvanced);

            Assert.IsFalse(isAdvanced);
            Assert.That(angleOffset.AngleA, Is.EqualTo(0));
            Assert.That(angleOffset.AngleB, Is.EqualTo(0));
            Assert.That(angleOffset.AngleC, Is.EqualTo(0));
        }

        [Test]
        public void GetSection()
        {
            string propertyName;
            _app.Model.ObjectModel.FrameObject.GetSection(CSiDataLine.NameObjectFrame, out propertyName);

            Assert.That(propertyName, Is.EqualTo(CSiDataLine.NameSectionFrame));
        }

        public void GetSection(string name,
            ref string propertyName,
            ref string autoSelectList)
        {

        }


        public void GetSectionNonPrismatic(string name,
            ref string propertyName,
            ref double totalLength,
            ref double relativeStartLocation)
        {

        }


        [Test]
        public void GetMass()
        {
            double masses;

            _app.Model.ObjectModel.FrameObject.GetMass(CSiDataLine.NameObjectFrame, out masses);

            Assert.That(masses, Is.EqualTo(0));
        }

        [Test]
        public void GetModifiers()
        {
            FrameModifier modifier;
            _app.Model.ObjectModel.FrameObject.GetModifiers(CSiDataLine.NameObjectFrame, out modifier);

            Assert.That(modifier.CrossSectionalArea, Is.EqualTo(CSiDataLine.OldModifiers.CrossSectionalArea));
            Assert.That(modifier.ShearV2, Is.EqualTo(CSiDataLine.OldModifiers.ShearV2));
            Assert.That(modifier.ShearV3, Is.EqualTo(CSiDataLine.OldModifiers.ShearV3));
            Assert.That(modifier.Torsion, Is.EqualTo(CSiDataLine.OldModifiers.Torsion));
            Assert.That(modifier.BendingM2, Is.EqualTo(CSiDataLine.OldModifiers.BendingM2));
            Assert.That(modifier.BendingM3, Is.EqualTo(CSiDataLine.OldModifiers.BendingM3));
            Assert.That(modifier.MassModifier, Is.EqualTo(CSiDataLine.OldModifiers.MassModifier));
            Assert.That(modifier.WeightModifier, Is.EqualTo(CSiDataLine.OldModifiers.WeightModifier));
        }




        public void GetMaterialOverwrite(string name,
            ref string propertyName)
        {

        }
#if BUILD_ETABS2016 || BUILD_ETABS2017

        public void GetSpringAssignment(string name,
            ref string nameSpring)
        {
          
        }
        
        
        //public void GetColumnSpliceOverwrite(string name,
        //    ref eColumnSpliceOption spliceOption,
        //    ref double height)
        //{
          
        //}
#elif !BUILD_ETABS2015

        public void GetSpring(string name,
            ref eSpringType[] springTypes,
            ref double[] stiffnesses,
            ref eSpringSimpleType[] springSimpleTypes,
            ref string[] linkProperties,
            ref eSpringLocalOneType[] springLocalOneTypes,
            ref int[] directions,
            ref double[] plane23Angle,
            ref double[] vectorComponentsX,
            ref double[] vectorComponentsY,
            ref double[] vectorComponentsZ,
            ref double[] angleOffsets,
            ref string[] coordinateSystems)
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
        
        public void GetAllFrames(ref string[] frameNames,
            ref string[] sectionNames,
            ref string[] storyNames,
            ref string[] pointINames,
            ref string[] pointJNames,
            ref Coordinate3DCartesian[] pointICoordinates,
            ref Coordinate3DCartesian[] pointJCoordinates,
            ref double[] angles,
            ref Displacements[] pointIOffsets,
            ref Displacements[] pointJOffsets,
            ref eCardinalInsertionPoint[] cardinalInsertionPoints,
            string coordinateSystem = CoordinateSystems.Global)
        {
          
        }

        
        //public void GetSupports(string name,
        //    ref string supportNameI,
        //    ref eSupportType supportTypeI,
        //    ref string supportNameJ,
        //    ref eSupportType supportTypeJ)
        //{
          
        //}
        
        //public void GetDesignOrientation(string name,
        //    ref eFrameDesignOrientation designOrientation)
        //{
          
        //}

        
        public void GetPier(string name,
            ref string namePier)
        {
          
        }

        
        public void GetSpandrel(string name,
            ref string nameSpandrel)
        {
          
        }
#else
  
        [Test]
        public void GetMaterialTemperature()
        {
            double temperature;
            string patternName;
            _app.Model.ObjectModel.FrameObject.GetMaterialTemperature(CSiDataLine.NameObjectFrame, out temperature, out patternName);

            Assert.That(temperature, Is.EqualTo(30));
            Assert.That(patternName, Is.EqualTo(CSiData.JointPattern));
        }

        public void GetFireProofing(string name,
            ref eFireProofing type,
            ref double thickness,
            ref double perimeter,
            ref double density,
            ref bool appliedToTopFlange,
            ref bool includeInSelfWeight,
            ref bool includeInGravityLoads,
            ref string includedLoadPattern)
        {
          
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
        

        public void GetDirectAnalysisModifiers(string name,
            ref double EAModifier,
            ref double EIModifier)
        {
          
        }
        

        public void GetTrapezoidal(string name,
            ref string fileName,
            ref string materialName,
            ref double sectionDepth,
            ref double sectionTopWidth,
            ref double sectionBottomWidth,
            ref int color,
            ref string notes,
            ref string GUID)
        {
          
        }
        

        public void GetNotionalSize(string name,
            ref eNotionalSizeType sizeType,
            ref double value)
        {
          
        }
        

        public void GetCurved(ref int numberItems,
            ref string[] frameObjectNames,
            ref eCurvedFrameType[] types,
            ref double[] globalX,
            ref double[] globalY,
            ref double[] globalZ,
            ref string[] pointNames,
            ref double[] radius,
            ref int[] numberSegments)
        {
          
        }
        
        
        public void GetType(string name,
            ref eFrameType frameType)
        {
          
        }

        
        public void GetEndSkew(string name,
            ref double skewI,
            ref double skewJ)
        {
          
        }
        

        public void GetAutoMesh(string name,
            ref bool isAutoMeshed,
            ref bool isAutoMeshedAtPoints,
            ref bool isAutoMeshedAtLines,
            ref int minElementNumber,
            ref double autoMeshMaxLength)
        {
          
        }
        

        public void GetPDeltaForce(string name,
            ref int numberForces,
            ref double[] pDeltaForces,
            ref ePDeltaDirection[] directions,
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

        
        public void GetLoadDistributedWithGUID(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref string[] GUIDs,
            ref eLoadForceType[] forceTypes,
            ref eLoadDirection[] loadDirections,
            ref double[] startLoadValues,
            ref double[] endLoadValues,
            ref double[] absoluteDistanceStartFromI,
            ref double[] absoluteDistanceEndFromI,
            ref double[] relativeDistanceStartFromI,
            ref double[] relativeDistanceEndFromI,
            ref string[] coordinateSystems,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void GetLoadPointWithGUID(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref string[] GUIDs,
            ref eLoadForceType[] forceTypes,
            ref eLoadDirection[] loadDirections,
            ref double[] pointLoadValues,
            ref double[] absoluteDistanceFromI,
            ref double[] relativeDistanceFromI,
            ref string[] coordinateSystems,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        
        public void GetLoadTransfer(string name,
            ref bool loadIsTransferred)
        {
          
        }
#endif




        public void GetTensionCompressionLimits(string name,
            ref bool limitCompressionExists,
            ref double limitCompression,
            ref bool limitTensionExists,
            ref double limitTension)
        {
          
        }
        
        
        
        public void GetLateralBracing(string name,
            ref int numberItems,
            ref string[] frameNames,
            ref eBracingType[] bracingTypes,
            ref eBracingLocation[] bracingLocations,
            ref double[] relativeDistanceStartBracing,
            ref double[] relativeDistanceEndBracing,
            ref double[] actuaDistanceStartBracing,
            ref double[] actualDistanceEndBracing)
        {
          
        }

        
        public void GetInsertionPoint(string name,
            ref Displacements offsetDistancesI,
            ref Displacements offsetDistancesJ,
            ref eCardinalInsertionPoint cardinalPoint,
            ref bool isMirroredLocal2,
            ref bool isStiffnessTransformed,
            ref string coordinateSystem)
        {
          
        }
        
        
        

        public void GetEndLengthOffset(string name,
            ref bool autoOffset,
            ref double lengthIEnd,
            ref double lengthJEnd,
            ref double rigidZoneFactor)
        {
          
        }
        
        


        
        public void GetHingeAssigns(string name,
            ref int[] hingeNumbers,
            ref string[] generatedPropertyNames,
            ref eHingeType[] hingeTypes,
            ref eHingeBehavior[] hingeBehaviors,
            ref string[] sources,
            ref double[] relativeDistances)
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

        [Test]
        public void GetReleases()
        {
            DegreesOfFreedomLocal iEndRelease;
            DegreesOfFreedomLocal jEndRelease;
            Fixity iEndFixity;
            Fixity jEndFixity;

            _app.Model.ObjectModel.FrameObject.GetReleases(CSiDataLine.NameObjectFrameReleases,
                out iEndRelease,
                out jEndRelease,
                out iEndFixity,
                out jEndFixity);

            Assert.That(iEndRelease.U1, Is.EqualTo(CSiDataLine.OldIEndRelease.U1));
            Assert.That(iEndRelease.U2, Is.EqualTo(CSiDataLine.OldIEndRelease.U2));
            Assert.That(iEndRelease.U3, Is.EqualTo(CSiDataLine.OldIEndRelease.U3));
            Assert.That(iEndRelease.R1, Is.EqualTo(CSiDataLine.OldIEndRelease.R1));
            Assert.That(iEndRelease.R2, Is.EqualTo(CSiDataLine.OldIEndRelease.R2));
            Assert.That(iEndRelease.R3, Is.EqualTo(CSiDataLine.OldIEndRelease.R3));

            Assert.That(jEndRelease.U1, Is.EqualTo(CSiDataLine.OldJEndRelease.U1));
            Assert.That(jEndRelease.U2, Is.EqualTo(CSiDataLine.OldJEndRelease.U2));
            Assert.That(jEndRelease.U3, Is.EqualTo(CSiDataLine.OldJEndRelease.U3));
            Assert.That(jEndRelease.R1, Is.EqualTo(CSiDataLine.OldJEndRelease.R1));
            Assert.That(jEndRelease.R2, Is.EqualTo(CSiDataLine.OldJEndRelease.R2));
            Assert.That(jEndRelease.R3, Is.EqualTo(CSiDataLine.OldJEndRelease.R3));

            Assert.That(iEndFixity.U1, Is.EqualTo(CSiDataLine.OldIEndFixity.U1));
            Assert.That(iEndFixity.U2, Is.EqualTo(CSiDataLine.OldIEndFixity.U2));
            Assert.That(iEndFixity.U3, Is.EqualTo(CSiDataLine.OldIEndFixity.U3));
            Assert.That(iEndFixity.R1, Is.EqualTo(CSiDataLine.OldIEndFixity.R1));
            Assert.That(iEndFixity.R2, Is.EqualTo(CSiDataLine.OldIEndFixity.R2));
            Assert.That(iEndFixity.R3, Is.EqualTo(CSiDataLine.OldIEndFixity.R3));

            Assert.That(jEndFixity.U1, Is.EqualTo(CSiDataLine.OldJEndFixity.U1));
            Assert.That(jEndFixity.U2, Is.EqualTo(CSiDataLine.OldJEndFixity.U2));
            Assert.That(jEndFixity.U3, Is.EqualTo(CSiDataLine.OldJEndFixity.U3));
            Assert.That(jEndFixity.R1, Is.EqualTo(CSiDataLine.OldJEndFixity.R1));
            Assert.That(jEndFixity.R2, Is.EqualTo(CSiDataLine.OldJEndFixity.R2));
            Assert.That(jEndFixity.R3, Is.EqualTo(CSiDataLine.OldJEndFixity.R3));
        }
        
               
        public void GetDesignProcedure(string name,
            ref eDesignProcedureType designProcedureMaterial)
        {
          
        }


        public void GetLoadTemperature(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref eLoadTemperatureType[] temperatureLoadTypes,
            ref double[] temperatureLoadValues,
            ref string[] jointPatternNames,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void GetLoadPoint(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref eLoadForceType[] forceTypes,
            ref eLoadDirection[] loadDirections,
            ref double[] pointLoadValues,
            ref double[] absoluteDistanceFromI,
            ref double[] relativeDistanceFromI,
            ref string[] coordinateSystems,
            eItemType itemType = eItemType.Object)
        {
          
        }
    }

    [TestFixture]
    public class FrameObject_Set : CsiSet
    {
#if BUILD_ETABS2016 || BUILD_ETABS2017


        
        public void SetSpringAssignment(string name,
            string nameSpring,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        
        //public void SetColumnSpliceOverwrite(string name,
        //    eColumnSpliceOption spliceOption,
        //    double height,
        //    eItemType itemType = eItemType.Object)
        //{
          
        //}
#elif !BUILD_ETABS2015


        
        public void SetSpring(string name,
            eSpringType springType,
            double stiffness,
            eSpringSimpleType springSimpleType,
            string linkProperty,
            eSpringLocalOneType springLocalOneType,
            int direction,
            double plane23Angle,
            double[] vector,
            double angleOffset,
            bool replace,
            string coordinateSystem = CoordinateSystems.Local,
            eItemType itemType = eItemType.Object)
        {
          
        }
#endif
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017

        
        
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
        
        
        
        public void SetFireProofing(string name,
            eFireProofing type,
            double thickness,
            double perimeter,
            double density,
            bool appliedToTopFlange,
            bool includeInSelfWeight,
            bool includeInGravityLoads,
            string includedLoadPattern,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void DeleteFireProofing(string name,
            eItemType itemType = eItemType.Object)
        {
          
        }  
#endif
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        
        
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
        
        
        public void SetTrapezoidal(string name,
            string materialName,
            double sectionDepth,
            double sectionTopWidth,
            double sectionBottomWidth,
            int color = -1,
            string notes = "",
            string GUID = "")
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

        
        public void SetCurved(string frameObjectName,
            eCurvedFrameType type,
            double globalX,
            double globalY,
            double globalZ,
            string pointName,
            double radius,
            int numberSegments,
            string coordSystem = CoordinateSystems.Global)
        {
          
        }

        
        public void SetStraight(string name)
        {
          
        }

        
        public void SetEndSkew(string name,
            double skewI,
            double skewJ,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetAutoMesh(string name, 
            bool isAutoMeshed, 
            bool isAutoMeshedAtPoints,
            bool isAutoMeshedAtLines,
            int minElementNumber,
            double autoMeshMaxLength,
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

        
        public void SetPDeltaForce(string name,
            double pDeltaForce,
            ePDeltaDirection direction,
            bool replaceExistingLoads,
            string coordinateSystem = CoordinateSystems.Global,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void DeletePDeltaForce(string name,
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

        
        public void SetLoadDistributedWithGUID(string name,
            string loadPattern,
            string GUID,
            eLoadForceType forceType,
            eLoadDirection loadDirection,
            double startLoadValue,
            double endLoadValue,
            double absoluteDistanceStartFromI,
            double absoluteDistanceEndFromI,
            bool distanceIsRelative = true,
            string coordinateSystem = CoordinateSystems.Global,
            bool replace = true)
        {
          
        }
        
        
        public void DeleteLoadDistributedWithGUID(string name,
            string GUID)
        {
          
        }

        
        public void SetLoadPointWithGUID(string name,
            string loadPattern,
            string GUID,
            eLoadForceType forceType,
            eLoadDirection loadDirection,
            double pointLoadValue,
            double absoluteDistanceFromI,
            bool distanceIsRelative = true,
            string coordinateSystem = CoordinateSystems.Global,
            bool replace = true)
        {
          
        }

        
        public void DeleteLoadPointWithGUID(string name,
            string GUID)
        {
          
        }

        
        public void SetLoadTransfer(string name,
            bool loadIsTransferred,
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

        
        public void SetSection(string name,
            string propertyName,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetSection(string name,
            string propertyName,
            double nonPrismaticTotalLength,
            double nonPrismaticRelativeStartLocation,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
            
        
        
        public void SetMaterialOverwrite(string name,
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
            eCardinalInsertionPoint cardinalPoint,
            bool isMirroredLocal2,
            bool isStiffnessTransformed,
            string coordinateSystem = CoordinateSystems.Global,
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

        
        public void SetLateralBracing(string name,
            eBracingType bracingType,
            eBracingLocation bracingLocation,
            double distanceStartBracing,
            double distanceEndBracing,
            bool relativeDistance = true,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        
        public void DeleteLateralBracing(string name,
            eBracingType bracingType,
            eItemType itemType = eItemType.Object)
        {
          
        }



        public void SetEndLengthOffset(string name,
            bool autoOffset,
            double lengthIEnd,
            double lengthJEnd,
            double rigidZoneFactor)
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


        public void SetReleases(string name,
            DegreesOfFreedomLocal iEndRelease,
            DegreesOfFreedomLocal jEndRelease,
            Fixity iEndFixity,
            Fixity jEndFixity)
        {
          
        }




        public void DeleteSpring(string name,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        
        public void SetDesignProcedure(string name,
            eDesignProcedure designProcedure,
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


        
        public void GetLoadDistributed(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref eLoadForceType[] forceTypes,
            ref eLoadDirection[] loadDirections,
            ref double[] startLoadValues,
            ref double[] endLoadValues,
            ref double[] absoluteDistanceStartFromI,
            ref double[] absoluteDistanceEndFromI,
            ref double[] relativeDistanceStartFromI,
            ref double[] relativeDistanceEndFromI,
            ref string[] coordinateSystems,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetLoadDistributed(string name,
            string loadPattern,
            eLoadForceType forceType,
            eLoadDirection loadDirection,
            double startLoadValue,
            double endLoadValue,
            double absoluteDistanceStartFromI,
            double absoluteDistanceEndFromI,
            bool distanceIsRelative = true,
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
      
    }
}
