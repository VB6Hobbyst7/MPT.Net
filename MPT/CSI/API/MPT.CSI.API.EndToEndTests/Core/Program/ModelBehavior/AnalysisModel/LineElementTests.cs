using System.Linq;
using NUnit.Framework;
using MPT.CSI.API.Core.Program;

using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.AnalysisModel
{
    [TestFixture]
    public class LineElement_Get : CsiGetAnalysisModel
    {
        #region Query
        [Test]
        public void Count()
        {
            int numberOfObjects = _app.Model.AnalysisModel.LineElement.Count();
            Assert.That(numberOfObjects, Is.EqualTo(CSiDataLine.NumberOfElementsExpected));
        }

        [Test]
        public void GetNameList()
        {
            string[] names;
            _app.Model.AnalysisModel.LineElement.GetNameList(out names);

            Assert.That(names.Length, Is.EqualTo(CSiDataLine.NumberOfElementsExpected));
            Assert.That(names.Contains(CSiDataLine.NameElementFrame));
            Assert.That(names.Contains(CSiDataLine.NameElementFrameCurved));
            Assert.That(names.Contains(CSiDataLine.NameElementFrameOffsets));
            Assert.That(names.Contains(CSiDataLine.NameElementFrameReleases));
            Assert.That(names.Contains(CSiDataLine.NameElementCable));
            Assert.That(names.Contains(CSiDataLine.NameElementCableMultiSegment));
            Assert.That(names.Contains(CSiDataLine.NameElementTendonAsElements));
            Assert.That(names.Contains(CSiDataLine.NameElementTendonAsElements2));
            Assert.That(names.Contains(CSiDataLine.NameElementTendonAsElements3));
            Assert.That(names.Contains(CSiDataLine.NameElementTendonAsElements4));
            Assert.That(names.Contains(CSiDataLine.NameElementTendonAsElements5));
            Assert.That(names.Contains(CSiDataLine.NameElementTendonAsElements6));
            Assert.That(names.Contains(CSiDataLine.NameElementTendonAsElements7));
            Assert.That(!names.Contains(CSiDataLine.NameElementTendonAsLoads)); // As element is defined as loads
        }

        [Test]
        public void GetTransformationMatrix()
        {
            double[] directionCosines;
            _app.Model.AnalysisModel.LineElement.GetTransformationMatrix(CSiDataLine.NameElementFrame, out directionCosines);

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
        public void GetPoints()
        {
            string[] points;
            _app.Model.AnalysisModel.LineElement.GetPoints(CSiDataLine.NameElementFrameOffsets, out points);

            Assert.That(points.Length, Is.EqualTo(CSiDataLine.NameElementFrameOffsetJoints.Length));
            Assert.That(points.Contains(CSiDataLine.NameElementFrameOffsetJoints[0]));
            Assert.That(points.Contains(CSiDataLine.NameElementFrameOffsetJoints[1]));
        }

        [Test]
        public void GetPoints_Meshed()
        {
            string[] points;
            _app.Model.AnalysisModel.LineElement.GetPoints(CSiDataLine.NameElementFrame, out points);

            Assert.That(points.Length, Is.EqualTo(CSiDataLine.NameElementFrameMeshedJoints.Length));
            Assert.That(points.Contains(CSiDataLine.NameElementFrameMeshedJoints[0]));
            Assert.That(points.Contains(CSiDataLine.NameElementFrameMeshedJoints[1]));
        }

        [Test]
        public void GetObject()
        {
            string objectName;
            _app.Model.AnalysisModel.LineElement.GetObject(CSiDataLine.NameElementFrameOffsets, out objectName);
            
            Assert.That(objectName, Is.EqualTo(CSiDataLine.NameObjectFrameOffsets));
        }

        [Test]
        public void GetObject_Meshed()
        {
            string objectName;
            _app.Model.AnalysisModel.LineElement.GetObject(CSiDataLine.NameElementFrame, out objectName);

            Assert.That(objectName, Is.EqualTo(CSiDataLine.NameObjectFrame));
        }

        [Test]
        public void GetObject_Cable()
        {
            string objectName;
            eLineTypeObject objectType;
            double relativeDistanceI;
            double relativeDistanceJ;

            _app.Model.AnalysisModel.LineElement.GetObject(CSiDataLine.NameElementCable, 
                out objectName,
                out objectType,
                out relativeDistanceI,
                out relativeDistanceJ);

            Assert.That(objectName, Is.EqualTo(CSiDataLine.NameObjectCable));
            Assert.That(objectType, Is.EqualTo(eLineTypeObject.Cable));
            Assert.That(relativeDistanceI, Is.EqualTo(0));
            Assert.That(relativeDistanceJ, Is.EqualTo(CSiDataLine.RelativeDistanceCable).Within(0.001));
        }

        [Test]
        public void GetObject_CurvedFrame()
        {
            string objectName;
            eLineTypeObject objectType;
            double relativeDistanceI;
            double relativeDistanceJ;

            _app.Model.AnalysisModel.LineElement.GetObject(CSiDataLine.NameElementFrameCurved,
                out objectName,
                out objectType,
                out relativeDistanceI,
                out relativeDistanceJ);

            Assert.That(objectName, Is.EqualTo(CSiDataLine.NameObjectFrameCurved));
            Assert.That(objectType, Is.EqualTo(eLineTypeObject.CurvedFrame));
            Assert.That(relativeDistanceI, Is.EqualTo(0));
            Assert.That(relativeDistanceJ, Is.EqualTo(CSiDataLine.RelativeDistanceFrameCurved).Within(0.001));
        }

        [Test]
        public void GetObject_StraightFrame()
        {
            string objectName;
            eLineTypeObject objectType;
            double relativeDistanceI;
            double relativeDistanceJ;

            _app.Model.AnalysisModel.LineElement.GetObject(CSiDataLine.NameElementFrame,
                out objectName,
                out objectType,
                out relativeDistanceI,
                out relativeDistanceJ);

            Assert.That(objectName, Is.EqualTo(CSiDataLine.NameObjectFrame));
            Assert.That(objectType, Is.EqualTo(eLineTypeObject.StraightFrame));
            Assert.That(relativeDistanceI, Is.EqualTo(0));
            Assert.That(relativeDistanceJ, Is.EqualTo(CSiDataLine.RelativeDistanceFrameMeshed).Within(0.001));
        }

        [Test]
        public void GetObject_StraightFrame_With_Offsets()
        {
            string objectName;
            eLineTypeObject objectType;
            double relativeDistanceI;
            double relativeDistanceJ;

            _app.Model.AnalysisModel.LineElement.GetObject(CSiDataLine.NameElementFrameOffsets,
                out objectName,
                out objectType,
                out relativeDistanceI,
                out relativeDistanceJ);

            Assert.That(objectName, Is.EqualTo(CSiDataLine.NameObjectFrameOffsets));
            Assert.That(objectType, Is.EqualTo(eLineTypeObject.StraightFrame));
            Assert.That(relativeDistanceI, Is.EqualTo(0));
            Assert.That(relativeDistanceJ, Is.EqualTo(CSiDataLine.RelativeDistanceFrameOffsets).Within(0.001));
        }

        [Test]
        public void GetObject_Tendon_As_Elements()
        {
            string objectName;
            eLineTypeObject objectType;
            double relativeDistanceI;
            double relativeDistanceJ;

            _app.Model.AnalysisModel.LineElement.GetObject(CSiDataLine.NameElementTendonAsElements,
                out objectName,
                out objectType,
                out relativeDistanceI,
                out relativeDistanceJ);

            Assert.That(objectName, Is.EqualTo(CSiDataLine.NameObjectTendonAsElements));
            Assert.That(objectType, Is.EqualTo(eLineTypeObject.Tendon));
            Assert.That(relativeDistanceI, Is.EqualTo(0));
            Assert.That(relativeDistanceJ, Is.EqualTo(CSiDataLine.RelativeDistanceTendonAsElements).Within(0.001));
        }


        [Test]
        public void GetObject_Tendon_As_Loads_Throws_CSiException()  // As object is modeled as loads rather than elements
        {
            string objectName;
            eLineTypeObject objectType;
            double relativeDistanceI;
            double relativeDistanceJ;

            Assert.Throws<CSiException>(() =>
            {
                _app.Model.AnalysisModel.LineElement.GetObject(CSiDataLine.NameElementTendonAsLoads,
                out objectName,
                out objectType,
                out relativeDistanceI,
                out relativeDistanceJ);
            });
        }

        #endregion

        #region Axes
        [Test]
        public void GetLocalAxes()
        {
            AngleLocalAxes angleOffset;
            _app.Model.AnalysisModel.LineElement.GetLocalAxes(CSiDataLine.NameElementFrame, out angleOffset);
            
            Assert.That(angleOffset.AngleA, Is.EqualTo(0));
            Assert.That(angleOffset.AngleB, Is.EqualTo(0));
            Assert.That(angleOffset.AngleC, Is.EqualTo(0));
        }
        #endregion

        #region Modifiers
        [Test]
        public void GetModifiers()
        {
            FrameModifier modifier;
            _app.Model.AnalysisModel.LineElement.GetModifiers(CSiDataLine.NameElementFrame, out modifier);

            Assert.That(modifier.CrossSectionalArea, Is.EqualTo(CSiDataLine.OldModifiers.CrossSectionalArea));
            Assert.That(modifier.ShearV2, Is.EqualTo(CSiDataLine.OldModifiers.ShearV2));
            Assert.That(modifier.ShearV3, Is.EqualTo(CSiDataLine.OldModifiers.ShearV3));
            Assert.That(modifier.Torsion, Is.EqualTo(CSiDataLine.OldModifiers.Torsion));
            Assert.That(modifier.BendingM2, Is.EqualTo(CSiDataLine.OldModifiers.BendingM2));
            Assert.That(modifier.BendingM3, Is.EqualTo(CSiDataLine.OldModifiers.BendingM3));
            Assert.That(modifier.MassModifier, Is.EqualTo(CSiDataLine.OldModifiers.MassModifier));
            Assert.That(modifier.WeightModifier, Is.EqualTo(CSiDataLine.OldModifiers.WeightModifier));
        }
        #endregion

        #region Cross-Section & Material Properties
        [Test]
        public void GetSection()
        {
            string propertyName;
            _app.Model.AnalysisModel.LineElement.GetSection(CSiDataLine.NameElementFrame, out propertyName);

            Assert.That(propertyName, Is.EqualTo(CSiDataLine.NameSectionFrame));
        }

        [Test]
        public void GetSection_ISection()
        {
            string propertyName;
            eLineTypeObject objectType;
            bool isPrismatic;
            double nonPrismaticTotalLength;
            double nonPrismaticRelativeStartLocation;

            _app.Model.AnalysisModel.LineElement.GetSection(CSiDataLine.NameElementFrame, 
                out propertyName,
                out objectType,
                out isPrismatic,
                out nonPrismaticTotalLength,
                out nonPrismaticRelativeStartLocation);

            Assert.That(propertyName, Is.EqualTo(CSiDataLine.NameSectionFrame));
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
            _app.Model.AnalysisModel.LineElement.GetMaterialTemperature(CSiDataLine.NameElementFrame, out temperature, out patternName);

            Assert.That(temperature, Is.EqualTo(30));
            Assert.That(patternName, Is.EqualTo(CSiData.JointPattern));
        }
#endif
        #endregion

        #region Frame Properties
        public void GetInsertionPoint(string name,
            ref Displacements offsetDistancesI,
            ref Displacements offsetDistancesJ)
        {
          
        }
        
        public void GetTensionCompressionLimits(string name,
            ref bool limitCompressionExists,
            ref double limitCompression,
            ref bool limitTensionExists,
            ref double limitTension)
        {
          
        }
        
        
        public void GetEndLengthOffset(string name,
            ref double lengthIEnd,
            ref double lengthJEnd,
            ref double rigidZoneFactor)
        {
          
        }
        
        [Test]
        public void GetReleases()
        {
            DegreesOfFreedomLocal iEndRelease;
            DegreesOfFreedomLocal jEndRelease;
            Fixity iEndFixity;
            Fixity jEndFixity;

            _app.Model.AnalysisModel.LineElement.GetReleases(CSiDataLine.NameElementFrameReleases,
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

        public void GetLoadDistributed(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref eLoadForceType[] forceTypes,
            ref string[] coordinateSystems,
            ref eLoadDirection[] loadDirections,
            ref double[] relativeDistanceStartFromI,
            ref double[] relativeDistanceEndFromI,
            ref double[] absoluteDistanceStartFromI,
            ref double[] absoluteDistanceEndFromI,
            ref double[] startLoadValues,
            ref double[] endLoadValues,
            eItemTypeElement itemType = eItemTypeElement.Element)
        {
          
        }

        
        public void GetLoadPoint(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref eLoadForceType[] forceTypes,
            ref string[] coordinateSystems,
            ref eLoadDirection[] loadDirections,
            ref double[] relativeDistanceFromI,
            ref double[] absoluteDistanceFromI,
            ref double[] pointLoadValues,
            eItemTypeElement itemType = eItemTypeElement.Element)
        {
          
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        public void GetLoadDeformation(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref DegreesOfFreedomLocal[] degreesOfFreedom,
            ref Deformations[] deformations,
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

        
        public void GetLoadTargetForce(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref ForcesActive[] forcesActive,
            ref Deformations[] deformations,
            ref Forces[] relativeForcesLocation,
            eItemTypeElement itemType = eItemTypeElement.Element)
        {
          
        }
        
        
        public void GetPDeltaForce(string name,
            ref int numberForces,
            ref double[] pDeltaForces,
            ref ePDeltaDirection[] directions,
            ref string[] coordinateSystems)
        {
          
        }
#endif
        #endregion
    }
}
