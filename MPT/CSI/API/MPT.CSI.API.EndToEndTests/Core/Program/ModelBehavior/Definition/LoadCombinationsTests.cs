using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition
{
    [TestFixture]
    public class LoadCombinations_Get : CsiGet
    {
      
        public void GetNameList(ref string[] namesCombo)
        {
          
        }

        
        public void GetCaseList(string nameLoadCombo,
            ref int numberLoadCombos,
            ref eCaseComboType[] caseComboType,
            ref string[] caseComboNames,
            ref double[] scaleFactor)
        {
          
        }

        
        public void GetType(string nameLoadCombo,
            ref eLoadComboType combo)
        {

            
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
       
        public void Count()
        {
          
        }
        
        
        public void CountCase(string nameLoadCombo,
            ref int loadCaseComboCount)
        {
          
        }
        
        
        public void GetNote(string nameLoadCombo,
            ref string note)
        {
          
        }
#endif
    }
    
    [TestFixture]
    public class LoadCombinations_Set : CsiSet
    {
      
      
        public void Add(string nameLoadCombo,
            eLoadComboType comboType)
        {
          
        }

        
        
        public void AddDesignDefaultCombos(bool designSteel,
            bool designConcrete,
            bool designAluminum,
            bool designColdFormed)
        {
          
        }

        
        public void Delete(string nameLoadCombo)
        {
          
        }

        
        public void DeleteCase(string nameLoadCombo,
            eCaseComboType caseComboType,
            string caseComboName)
        {
          
        }
        
        

        
        public void SetCaseList(string nameLoadCombo,
            eCaseComboType caseComboType,
            string caseComboNames,
            double scaleFactor)
        {
          
        }
        
        
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        
        public void ChangeName(string nameLoadCombo,
            string newName)
        {
          
        }

        
        public void SetNote(string nameLoadCombo,
            string note)
        {
          
        }
        
        
        public void SetType(string nameLoadCombo,
            eLoadComboType comboType)
        {
          
        }
#endif
    }
}
