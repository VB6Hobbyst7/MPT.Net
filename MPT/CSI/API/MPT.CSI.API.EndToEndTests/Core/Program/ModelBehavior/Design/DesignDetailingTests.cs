#if BUILD_ETABS2016 || BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Design
{
    [TestFixture]
    public class DesignDetailing_Get : CsiGet
    {
        
        public void ResultsAreAvailable()
        {
          
        }

        
        public void GetBeamLongitudinalRebarData(string name,
            ref int numberOfRebarSets,
            ref string[] barSizeNames,
            ref double[] barAreas,
            ref int[] numberOfBars,
            ref string[] locations,
            ref double[] clearCovers,
            ref double[] startCoordinates,
            ref double[] barLengths,
            ref double[] bendingAnglesStart,
            ref double[] bendingAnglesEnd,
            ref string[] rebarSetGUIDs)
        {
          
        }


  
  
        public void GetBeamTieRebarData(string name,
            ref int numberOfRebarSets,
            ref string[] barSizeNames,
            ref double[] barAreas,
            ref double[] numberLegs,
            ref string[] locations,
            ref double[] clearCovers,
            ref double[] startCoordinates,
            ref double[] spacings,
            ref double[] lengths,
            ref string[] rebarSetGUID)
        {
          
        }



 
 
        public void GetColumnLongitudinalRebarData(string name,
            ref int numberOfRebarSets,
            ref string[] barSizeNames,
            ref double[] barAreas,
            ref int[] numberOfCBars,
            ref int[] numberOfR3Bars,
            ref int[] numberOfR2Bars,
            ref string[] locations,
            ref double[] clearCovers,
            ref string[] rebarSetGUID)
        {
          
        }

 
 
        public void GetColumnTieRebarData(string name,
            ref int numberOfRebarSets,
            ref string[] barSizeNames,
            ref double[] barAreas,
            ref eRebarConfiguration[] pattern,
            ref eConfinementType[] confineType,
            ref int[] numberOfLegs2Dir,
            ref int[] numberOfLegs3Dir,
            ref string[] locations,
            ref double[] clearCovers,
            ref double[] startCoordinates,
            ref double[] spacings,
            ref double[] heights,
            ref string[] rebarSetGUID)
        {
          
        }
        
        
        public void GetDetailedBeamLineData(string beamLineID,
            ref string[] objectUniqueNames,
            ref int numberOfSpans,
            ref double[] spanLength,
            ref int[] numberOfLongitudinalBars,
            ref double[] longitudinalBarDiameters,
            ref string[] longitudinalBarNotations,
            ref double[] longitudinalBarStartDistances,
            ref int[] longitudinalBarStartBends,
            ref int[] longitudinalBarEndBends,
            ref double[] longitudinalBarLengths,
            ref int[] longitudinalBarNumberOfLayers,
            ref int[] numberOfTieBars,
            ref int[] numberOfTieVerticalLegs,
            ref double[] tieBarDiameters,
            ref string[] tieBarNotations,
            ref double[] tieBarStartDistances,
            ref double[] tieBarSpacings,
            ref int[] tieBarTypes)
        {
          
        }

        
        public void GetDetailedBeamLines(ref string[] beamLineIDs)
        {
          
        }

        
        public void GetDetailedColumnStackData(string columnStackID,
            ref string[] objectUniqueNames,
            ref int numberOfRebarSets,
            ref int[] numberOfLongitudinalBars,
            ref double[] longitudinalBarDiameters,
            ref string[] longitudinalBarNotations,
            ref double[] longitudinalBarStartDistances,
            ref int[] longitudinalBarStartBends,
            ref int[] longitudinalBarEndBends,
            ref double[] longitudinalBarLengths,
            ref int[] longitudinalBarNumberOfLayers,
            ref int numberOfTieZones,
            ref string[] tieBarZones,
            ref int[] numberOfTieBars,
            ref int[] numberOfTieVerticalLegs,
            ref double[] tieBarDiameters,
            ref string[] tieBarNotations,
            ref double[] tieBarStartDistances,
            ref double[] tieBarSpacings,
            ref int[] tieBarTypes)
        {
          
        }

        
        public void GetDetailedColumnStacks(ref string[] columnStackIDs)
        {
          
        }

        
        public void GetDetailedSlabBottomBarData(string slabName,
            ref string[] names,
            ref int[] numberOfBars,
            ref double[] barDiameters,
            ref string[] barNotations,
            ref string[] barMaterials,
            ref double[] startX,
            ref double[] startY,
            ref double[] startZ,
            ref double[] endX,
            ref double[] endY,
            ref double[] endZ,
            ref double[] widthsLeft,
            ref double[] widthsRight,
            ref double[] offsetsFromTop,
            ref double[] offsetsFromBottom,
            ref int[] startBarBends,
            ref int[] endBarBends,
            ref string[] GUIDs)
        {
          
        }

        
        public void GetDetailedSlabBottomBarData(string slabName,
            ref string[] names,
            ref int[] numberOfBars,
            ref double[] barDiameters,
            ref string[] barNotations,
            ref string[] barMaterials,
            ref double[] startX,
            ref double[] startY,
            ref double[] startZ,
            ref double[] endX,
            ref double[] endY,
            ref double[] endZ,
            ref double[] widthsLeft,
            ref double[] widthsRight,
            ref double[] offsetsFromTop,
            ref double[] offsetsFromBottom,
            ref int[] startBarBends,
            ref int[] endBarBends,
            ref string[] GUIDs,
            ref string[] stripNames,
            ref int[] spanNumbers)
        {
          
        }

        
        public void GetDetailedSlabs(ref string[] names,
            ref double[] slabElevations,
            ref string[] GUIDs)
        {
          
        }

        
        
        public void GetDetailedSlabTopBarData(string slabName,
            ref string[] names,
            ref int[] numberOfBars,
            ref double[] barDiameters,
            ref string[] barNotations,
            ref string[] barMaterials,
            ref double[] startX,
            ref double[] startY,
            ref double[] startZ,
            ref double[] endX,
            ref double[] endY,
            ref double[] endZ,
            ref double[] widthsLeft,
            ref double[] widthsRight,
            ref double[] offsetsFromTop,
            ref double[] offsetsFromBottom,
            ref int[] startBarBends,
            ref int[] endBarBends,
            ref string[] GUIDs)
        {
          
        }

 
 
        public void GetDetailedSlabTopBarData(string slabName,
            ref string[] names,
            ref int[] numberOfBars,
            ref double[] barDiameters,
            ref string[] barNotations,
            ref string[] barMaterials,
            ref double[] startX,
            ref double[] startY,
            ref double[] startZ,
            ref double[] endX,
            ref double[] endY,
            ref double[] endZ,
            ref double[] widthsLeft,
            ref double[] widthsRight,
            ref double[] offsetsFromTop,
            ref double[] offsetsFromBottom,
            ref int[] startBarBends,
            ref int[] endBarBends,
            ref string[] GUIDs,
            ref string[] stripNames,
            ref int[] spanNumbers)
        {
          
        }

        
        public void GetSimilarBeamLines(string beamLineID,
            ref int numberOfSimilarBeams,
            ref int[] numberOfUniqueObjects,
            ref string[] objectUniqueNames)
        {
          
        }


        
        public void GetSimilarColumnStacks(string columnStackID,
            ref int numberOfSimilarColumns,
            ref int[] numberOfUniqueObjects,
            ref string[] objectUniqueNames)
        {
   
   
        }


        
        public void GetSimilarSlabs(string slabName,
            ref int numberOfSimilarSlabs,
            ref string[] names)
        {
          
        }
    }
    
    [TestFixture]
    public class DesignDetailing_Set : CsiSet
    {
      
      
        public void StartDesign()
        { 
        
        }

        
        public void StartDesign(bool overwriteExisting)
        {
          
        }

        
        public void DeleteResults()
        {
          
        }
    }
}
#endif