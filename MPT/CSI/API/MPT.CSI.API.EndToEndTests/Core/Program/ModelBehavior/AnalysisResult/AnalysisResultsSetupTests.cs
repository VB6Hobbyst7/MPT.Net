using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.AnalysisResult;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.AnalysisResult
{
    [TestFixture]
    public class AnalysisResultsSetup_Get : CsiGet
    {


        public void GetCaseSelectedForOutput(string name,
            ref bool selected)
        {
          
        }

        
        public void GetComboSelectedForOutput(string name,
            ref bool selected)
        {

        }


        
        public void GetOptionBaseReactLocation(ref Coordinate3DCartesian coordinates)
        {
          
        }

        
        public void GetOptionBucklingMode(ref int buckleModeStart,
            ref int buckleModeEnd,
            ref bool buckleModeAll)
        {
            
            
        }

        
        public void GetOptionModeShape(ref int modeShapeStart,
            ref int modeShapeEnd,
            ref bool modeShapesAll)
        {
          
        }

        
        public void GetOptionDirectHistory(ref eAnalysisMultiStepOptions outputOption)
        {
          
        }

        
        public void GetOptionMultiStepStatic(ref eAnalysisMultiStepOptions outputOption)
        {
          
        }

        
        public void GetOptionModalHistory(ref eAnalysisMultiStepOptions outputOption)
        {
          
        }

        
        public void GetOptionNLStatic(ref eAnalysisMultiStepOptions outputOption)
        {
          
        }

        
        public void GetOptionMultiValuedCombo(ref eAnalysisMultiValuedOptions outputOption)
        {
          
        }
        
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        public void GetOptionPSD(ref eAnalysisPSDOptions outputOption)
        {
          
        }
        
        
        public void GetOptionSteadyState(ref eAnalysisSteadyStateOptions outputOption,
            ref eSteadyStateOptions steadyStateOption)
        {
          
        }
#endif
    }
    
    [TestFixture]
    public class AnalysisResultsSetup_Set : CsiSet
    {
      
        public void DeselectAllCasesAndCombosForOutput()
        {
          
        }
        
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
       
       
        public void SelectAllSectionCutsForOutput(bool selected)
        {
          
        }
#endif

        
        public void SetCaseSelectedForOutput(string name,
            bool selected = true)
        {

        }

        
        public void SetComboSelectedForOutput(string name,
            bool selected = true)
        {

        }

        
        public void SetOptionBaseReactLocation(Coordinate3DCartesian coordinates)
        {
          
        }

        
        
        public void SetOptionBucklingMode(int buckleModeStart,
            int buckleModeEnd,
            bool buckleModeAll)
        {
          
        }

        
        public void SetOptionModeShape(int modeShapeStart,
            int modeShapeEnd,
            bool modeShapesAll)
        {
          
        }

        
        public void SetOptionDirectHistory(eAnalysisMultiStepOptions outputOption)
        {
          
        }

        
        public void SetOptionMultiStepStatic(eAnalysisMultiStepOptions outputOption)
        {
          
        }

        
        public void SetOptionModalHistory(eAnalysisMultiStepOptions outputOption)
        {
          
        }

        
        public void SetOptionNLStatic(eAnalysisMultiStepOptions outputOption)
        {
          
        }

        
        public void SetOptionMultiValuedCombo(eAnalysisMultiValuedOptions outputOption)
        {
          
        }
        
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        public void SetOptionPSD(eAnalysisPSDOptions outputOption)
        {
          
        }
        
        public void SetOptionSteadyState(eAnalysisSteadyStateOptions outputOption,
            eSteadyStateOptions steadyStateOption)
        {
          
        }
#endif
    }
}
