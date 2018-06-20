#if BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_SAP2000v18 || BUILD_SAP2000v19 || BUILD_SAP2000v20
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Aluminum;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Design
{
    [TestFixture]
    public class DesignAluminum_Get : CsiGet
    {
#if  !BUILD_SAP2000v16 && !BUILD_SAP2000v17 && !BUILD_CSiBridgev16 && !BUILD_CSiBridgev17
       
       
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

#if !BUILD_SAP2000v16 && !BUILD_SAP2000v17 && !BUILD_CSiBridgev16 && !BUILD_CSiBridgev17
       
       
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

    }
    
    [TestFixture]
    public class DesignAluminum_Set : CsiSet
    {
        public void SetCode(AluminumCode code)
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

      
#if !BUILD_SAP2000v16 && !BUILD_SAP2000v17 && !BUILD_CSiBridgev16 && !BUILD_CSiBridgev17      
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
    }
}
#endif
