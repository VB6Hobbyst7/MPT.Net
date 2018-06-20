using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Concrete;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Design
{
    [TestFixture]
    public class DesignConcrete_Get : CsiGet
    {
#if !BUILD_ETABS2015 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17


        public void ResultsAreAvailable()
        {
          
        }
#endif
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017


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
#endif


        public void GetCode(ref string codeName)
        {
          
        }
        
        
        public void GetDesignSection(string nameFrame,
            ref string nameSection)
        {
          
        }
        
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17

        public void GetComboAutoGenerate(ref bool autoGenerate)
        {
          
        }
#endif
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        public void GetComboStrength(ref string[] nameLoadCombinations)
        {
          
        }
#endif

        public void GetSummaryResultsBeam(string name, 
            ref int numberItems,
            ref string[] frameName,
            ref double[] location,
            ref string[] topCombo,
            ref double[] topArea,
            ref string[] botCombo,
            ref double[] botArea,
            ref string[] VMajorCombo,
            ref double[] VMajorArea,
            ref string[] TLCombo,
            ref double[] TLArea,
            ref string[] TTCombo,
            ref double[] TTArea,
            ref string[] errorSummary,
            ref string[] warningSummary,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void GetSummaryResultsColumn(string name,
            ref int numberItems,
            ref string[] frameName,
            ref int[] myOption,
            ref double[] location,
            ref string[] PMMCombo,
            ref double[] PMMArea,
            ref double[] PMMRatio,
            ref string[] VMajorCombo,
            ref double[] AVMajor,
            ref string[] VMinorCombo,
            ref double[] AVMinor,
            ref string[] errorSummary,
            ref string[] warningSummary,
            eItemType itemType = eItemType.Object)
        {
          
        }

      
      
        public void GetSummaryResultsJoint(string name,
            ref int numberItems,
            ref string[] frameName,
            ref string[] LCJSRatioMajor,
            ref double[] JSRatioMajor,
            ref string[] LCJSRatioMinor,
            ref double[] JSRatioMinor,
            ref string[] LCBCCRatioMajor,
            ref double[] BCCRatioMajor,
            ref string[] LCBCCRatioMinor,
            ref double[] BCCRatioMinor,
            ref string[] errorSummary,
            ref string[] warningSummary,
            eItemType itemType = eItemType.Object)
        {
          
        }
    }
    
    [TestFixture]
    public class DesignConcrete_Set : CsiSet
    {
      
        public void SetCode(ConcreteCode code)
        {
          
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        public void DeleteResults()
        {
          
        }

        
        public void ResetOverwrites()
        {
          
        }
#endif


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
        
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17

        public void SetComboAutoGenerate(bool autoGenerate)
        {
          
        }
#endif
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
      
        public void SetComboStrength(string nameLoadCombination, 
            bool selectLoadCombination)
        {
          
        }
#endif
    }
}
