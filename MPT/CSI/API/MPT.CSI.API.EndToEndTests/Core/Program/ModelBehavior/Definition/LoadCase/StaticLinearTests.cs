using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Support;


namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadCase
{
    [TestFixture]
    public class StaticLinear_Get : CsiGet
    {
        
        
        public void GetInitialCase(string name,
            ref string initialCase)
        {
          
        }

        
        public void GetLoads(string name,
            ref eLoadType[] loadTypes,
            ref string[] loadNames,
            ref double[] scaleFactor)
        {
          
        }
    }
    
    [TestFixture]
    public class StaticLinear_Set : CsiSet
    {
      
        public void SetCase(string name)
        {

            
        }

        
        public void SetInitialCase(string name,
            string initialCase)
        {
          
        }

        
        public void SetLoads(string name,
            eLoadType[] loadTypes,
            string[] loadNames,
            double[] scaleFactor)
        {
          
        }
    }
}
