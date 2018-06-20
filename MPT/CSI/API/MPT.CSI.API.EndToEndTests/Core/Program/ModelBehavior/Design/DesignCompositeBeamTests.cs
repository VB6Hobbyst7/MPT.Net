#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Design
{
    [TestFixture]
    public class DesignCompositeBeam_Get : CsiGet
    {
#if !BUILD_ETABS2015 

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

      
      
        public void GetComboDeflection(ref string[] nameLoadCombinations)
        {
          
        }

      
      
        public void GetComboStrength(ref string[] nameLoadCombinations)
        {
          
        }

      
      
        public void GetSummaryResults(string name,
            ref string[] designSections,
            ref double[] beamFy,
            ref double[] studDiameters,
            ref string[] studLayouts,
            ref bool[] isBeamShored,
            ref double[] beamCambers,
            ref string[] passFail,
            ref double[] reactionsLeft,
            ref double[] reactionsRight,
            ref double[] MMaxNegative,
            ref double[] MMaxPositive,
            ref double[] percentCompositeConnection,
            ref double[] overallRatios,
            ref double[] studRatios,
            ref double[] strengthRatiosPM,
            ref double[] constructionRatiosPM,
            ref double[] strengthShearRatios,
            ref double[] constructionShearRatios,
            ref double[] deflectionRatiosPostConcreteDL,
            ref double[] deflectionRatiosSDL,
            ref double[] deflectionRatiosLL,
            ref double[] deflectionRatiosTotalCamber,
            ref double[] frequencyRatios,
            ref double[] MDampingRatios,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
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
    public class DesignCompositeBeam_Set : CsiSet
    {
      
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
#endif