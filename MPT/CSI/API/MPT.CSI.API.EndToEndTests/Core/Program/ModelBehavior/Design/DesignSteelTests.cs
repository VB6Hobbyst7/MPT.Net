using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.Design;
using MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Steel;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Design
{
    [TestFixture]
    public class DesignSteel_Get : CsiGet
    {
#if !BUILD_ETABS2015 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17

        public void ResultsAreAvailable()
        {
          
        }
#endif


        public void VerifyPassed(ref int numberNotPassedOrChecked, 
            ref int numberDidNotPass, 
            ref int numberNotChecked, 
            ref string[] namesNotPassedOrChecked)
        {
          
        }

        
        public void VerifySections(ref int numberDifferentSections, 
            ref string[] namesDifferentSections)
        {
          
        }

        
        public void GetCode(ref string codeName)
        {
          
        }

        
        public void GetDesignSection(string nameFrame, 
            ref string nameSection)
        {
          
        }


        
        public void GetGroup(ref string[] nameGroups)
        {
          
        }
        
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17

        public void GetComboAutoGenerate(ref bool autoGenerate)
        {
          
        }
#endif


        public void GetComboDeflection(ref string[] nameLoadCombinations)
        {
          
        }

        
        public void GetComboStrength(ref string[] nameLoadCombinations)
        {
          
        }

        
        public void GetSummaryResults(string name, 
            ref string[] frameNames, 
            ref double[] ratios, 
            ref int[] ratioTypes, 
            ref double[] locations, 
            ref string[] comboNames, 
            ref string[] errorSummaries, 
            ref string[] warningSummaries, 
            eItemType itemType = eItemType.Object)
        {
          
        }
        
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        public void GetDetailedResultsText(string itemName,
                                           eItemType itemType,
                                           int table,
                                           string field,
                                           ref int numberFrames,
                                           ref string[] frameNames,
                                           ref string[] textResults)
        {
          
        }
        
        
        public void GetDetailedResultsNumerical(string itemName,
                                           eItemType itemType,
                                           int table,
                                           string field,
                                           ref int numberFrames,
                                           ref string[] frameNames,
                                           ref double[] numericalResults)
        {
          
        }
#endif

        public void GetTargetDisplacement(ref int numberItems,
            ref string[] loadCase,
            ref string[] namePoint,
            ref double[] displacementTargets,
            ref bool allSpecifiedTargetsActive)
        {
          
        }

        
        public void GetTargetPeriod(ref int numberItems,
            ref string modalCase,
            ref int[] modeNumbers,
            ref double[] periodTargets,
            ref bool allSpecifiedTargetsActive)
        {
          
        }
    }
    
    [TestFixture]
    public class DesignSteel_Set : CsiSet
    {
      
        public void SetCode(DesignSteel code)
        {
          
        }

        
        public void DeleteResults()
        {
          
        }
        
        
        public void ResetOverwrites()
        {
          
        }

        
        public void StartDesign()
        {
          
        }

        
        public void SetCode(string codeName)
        {
          
        }

        
        public void SetDesignSection(string itemName, 
            string nameSection, 
            bool resetToLastAnalysisSection, 
            eItemType itemType = eItemType.Object)
        {
          
        }
      
        
        public void SetGroup(string nameGroup,
            bool selectForDesign)
        {
          
        }
        
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17

        public void SetComboAutoGenerate(bool autoGenerate)
        {
          
        }
#endif
        
        public void SetComboDeflection(string nameLoadCombination,
            bool selectLoadCombination)
        {
          
        }

        
        public void SetComboStrength(string nameLoadCombination,
            bool selectLoadCombination)
        {
          
        }

        
        public void SetAutoSelectNull(string itemName, 
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetTargetDisplacement(int numberItems,
            ref string[] loadCase,
            ref string[] namePoint,
            ref double[] displacementTargets,
            bool allSpecifiedTargetsActive)
        {
          
        }

        
        public void SetTargetPeriod(int numberItems,
            string modalCase,
            ref int[] modeNumbers,
            ref double[] periodTargets,
            bool allSpecifiedTargetsActive)
        {
          
        }
    }
}
