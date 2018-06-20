using System.Linq;
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.AnalysisResult;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Support;
using MPT.CSI.API.EndToEndTests.Core;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.AnalysisResult
{
    [TestFixture]
    public class Results_Get : CsiGet
    {    
        
        
        public void AreaForceShell(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] pointNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref double[] F11,
            ref double[] F22,
            ref double[] F12,
            ref double[] FMax,
            ref double[] FMin,
            ref double[] FAngle,
            ref double[] FVM,
            ref double[] M11,
            ref double[] M22,
            ref double[] M12,
            ref double[] MMax,
            ref double[] MMin,
            ref double[] MAngle,
            ref double[] V13,
            ref double[] V23,
            ref double[] VMax,
            ref double[] VAngle)
        {
          
        }

        
        public void AreaJointForceShell(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] pointNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Loads[] jointForces)
        {
          
        }

        
        public void AreaStressShell(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] pointNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Stress[] stressesTop,
            ref Stress[] stressesBottom,
            ref double[] S13Avg,
            ref double[] S23Avg,
            ref double[] SMaxAvg,
            ref double[] SAngleAvg)
        {
          
        }

        
        
        public void AreaStressShellLayered(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] pointNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Stress[] stresses,
            ref double[] S13Avg,
            ref double[] S23Avg,
            ref double[] SMaxAvg,
            ref double[] SAngleAvg,
            ref string[] layers,
            ref int[] integrationPointNumbers,
            ref double[] integrationPointLocations)
        {
          
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        public void AreaJointForcePlane(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] pointNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Loads[] jointForces)
        {
          
        }

        
        public void AreaStressPlane(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] pointNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Stress[] stresses)
        {
          
        }

        public void AssembledJointMass(string massSourceName,
            string name,
            eItemTypeElement itemType,
            ref string[] pointElementNames,
            ref string[] massSourceNames,
            ref Mass[] masses)
        {
            
        }
#else
  
        public void AssembledJointMass(string name,
            eItemTypeElement itemType,
            ref string[] pointElementNames,
            ref Mass[] masses)
        {
          
        }

        
        public void BaseReaction(ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Reactions[] reactions,
            ref Coordinate3DCartesian baseReactionCoordinate)
        {
          
        }

        
        public void BaseReactionWithCentroid(ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Reactions[] reactions,
            ref Coordinate3DCartesian baseReactionCoordinate,
            ref Coordinate3DCartesian[] centroidFxCoordinates,
            ref Coordinate3DCartesian[] centroidFyCoordinates,
            ref Coordinate3DCartesian[] centroidFzCoordinates)
        {
          
        }
        
        
        public void FrameForce(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Forces[] forces,
            ref double[] objectStations,
            ref double[] elementStations)
        {
          
        }


        public void FrameJointForce(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] pointNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Loads[] jointForces)
        {
          
        }
#endif
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17 && !BUILD_CSiBridgev16 && !BUILD_CSiBridgev17
        
        
        public void JointResponseSpectrum(string name,
            eItemTypeElement itemType,
            string namedSet,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] loadCases,
            ref string[] coordinateSystems,
            ref eDirection[] directions,
            ref double[] damping,
            ref double[] percentSpectrumWidening,
            ref double[] abscissaValues,
            ref double[] ordinateValues)
        {
          
        }
#endif
#if BUILD_ETABS2016 || BUILD_ETABS2017        

        public void JointDrifts(ref string[] storyNames,
            ref string[] labels,
            ref string[] names,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref double[] displacementsX,
            ref double[] displacementsY,
            ref double[] driftsX,
            ref double[] driftsY)
        {
          
        }
#endif

        public void JointAcceleration(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Deformations[] accelerations)
        {
          
        }

        
        public void JointAccelerationAbsolute(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Deformations[] accelerations)
        {
          
        }
        
        
        public void JointDisplacement(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Deformations[] displacements)
        {
          
        }

        
        public void JointDisplacementAbsolute(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Deformations[] displacements)
        {
          
        }

        
        public void JointReaction(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Loads[] jointForces)
        {
          
        }

        
        public void JointVelocity(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Deformations[] velocities)
        {
          
        }


        
        public void JointVelocityAbsolute(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Deformations[] velocities)
        {
          
        }
        
        
        public void LinkDeformation(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Deformations[] deformations)
        {
          
        }

        
        public void LinkForce(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] pointNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Forces[] forces)
        {
          
        }
        
        
        public void LinkJointForce(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] pointNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Loads[] jointForces)
        {
          
        }
        
        
        public void ModalLoadParticipationRatios(ref string[] loadCases,
            ref string[] itemTypes,
            ref string[] items,
            ref double[] percentStaticLoadParticipationRatio,
            ref double[] percentDynamicLoadParticipationRatio)
        {
          
        }

        
        public void ModalParticipatingMassRatios(ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref double[] periods,
            ref Mass[] massRatios,
            ref Mass[] massRatioSums)
        {
          
        }


        
        public void ModalParticipationFactors(ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref double[] periods,
            ref Mass[] participationFactors,
            ref double[] modalMasses,
            ref double[] modalStiffnesses)
        {
          
        }


        
        public void ModalPeriod(ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref double[] periods,
            ref double[] frequencies,
            ref double[] circularFrequencies,
            ref double[] eigenvalues)
        {
          
        }


        
        public void ModeShape(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Deformations[] displacements)
        {
           
           
        }
        
        
        public void PanelZoneDeformation(string name,
            eItemTypeElement itemType,
            ref string[] elementNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Deformations[] deformations)
        {
          
        }

        
        public void PanelZoneForce(string name,
            eItemTypeElement itemType,
            ref string[] elementNames,
            ref string[] pointNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Forces[] forces)
        {
          
        }
        
        
        public void SectionCutAnalysis(ref string[] sectionCuts,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Loads[] analysisForces)
        {
          
        }

        
        public void SectionCutDesign(ref string[] sectionCuts,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Forces[] designForces)
        {
          
        }
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        public void SolidJointForce(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] pointNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Loads[] jointForces)
        {
          
        }

        
        public void SolidStress(string name,
            eItemTypeElement itemType,
            ref string[] objectNames,
            ref string[] elementNames,
            ref string[] pointNames,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref Stress[] stresses,
            ref double[] SMid,
            ref double[] DirectionCosineMax1,
            ref double[] DirectionCosineMax2,
            ref double[] DirectionCosineMax3,
            ref double[] DirectionCosineMid1,
            ref double[] DirectionCosineMid2,
            ref double[] DirectionCosineMid3,
            ref double[] DirectionCosineMin1,
            ref double[] DirectionCosineMin2,
            ref double[] DirectionCosineMin3)
        {
          
        }
#endif
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        
        
        public void PierForce(ref string[] storyNames,
            ref string[] pierNames,
            ref string[] loadCases,
            ref eLocationVertical[] locations,
            ref Forces[] forces)
        {
          

        }

        
        public void SpandrelForce(ref string[] storyNames,
            ref string[] spandrelNames,
            ref string[] loadCases,
            ref eLocationVertical[] locations,
            ref Forces[] forces)
        {
          
        }
#endif    
#if BUILD_CSiBridgev18 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20

        public void BridgeSuperstructureCutLongitudinalStress(string name,
            int cutIndex,
            int pointIndex,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref double[] stresses)
        {
          
        }
#endif
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        public void StepLabel(string loadCase,
            double stepNumber,
            ref string label)
        {
          
        }
#endif
#if BUILD_ETABS2016 || BUILD_ETABS2017

        public void StoryDrifts(ref string[] stories,
            ref string[] labels,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref string[] directions,
            ref double[] drifts,
            ref double[] displacementsX,
            ref double[] displacementsY,
            ref double[] displacementsZ)
        {
          
        }
#endif


        public void BucklingFactor(ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref double[] bucklingFactors)
        {
          
        }

        
        public void GeneralizedDisplacement(string name,
            ref string[] names,
            ref string[] loadCases,
            ref string[] stepTypes,
            ref double[] stepNumbers,
            ref string[] types,
            ref double[] generalizedDisplacements)
        {
          
        }
    }
}
