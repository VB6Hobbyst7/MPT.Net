using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition
{
    [TestFixture]
    public class GeneralizedDisplacements_Get : CsiGet
    {       
       
        public void Count()
        {
          
        }

        
        public void GetNameList(ref string[] names)
        {
          
        }

        
        public void CountPoint(string name,
            ref int count)
        {
          
        }



        
        public void GetPoint(string name,
            ref int numberItems,
            ref string[] pointNames,
            ref Deformations[] scaleFactors)
        {
            
            
        }

        
        public void GetTypeGeneralizedDisplacement(string name,
            ref eGeneralizedDisplacementType type)
        {
          
        }
    }
    
    [TestFixture]
    public class GeneralizedDisplacements_Set : CsiSet
    {
      
        public void ChangeName(string currentName, string newName)
        {
          
        }

        
        public void Delete(string name)
        {
          
        }

        
        public void Add(string name, 
            eGeneralizedDisplacementType type)
        {
          
        }
        
        public void DeletePoint(string nameDisplacement,
            string namePoint)
        {
          
        }

        
        
        public void SetPoint(string name,
            string pointName,
            Deformations scaleFactors)
        {
          
        }

        
        public void SetTypeGeneralizedDisplacement(string name,
            eGeneralizedDisplacementType type)
        {
          
        }
    }
}
