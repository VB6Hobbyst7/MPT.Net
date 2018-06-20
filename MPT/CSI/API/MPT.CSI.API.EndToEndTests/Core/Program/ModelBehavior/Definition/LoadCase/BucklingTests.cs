#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadCase
{
    [TestFixture]
    public class Buckling_Get : CsiGet
    {
        
        public void GetInitialCase(string name, 
            ref string initialCase)
        {
          
        }
        
        
        public void GetParameters(string name,
            ref int numberOfBucklingModes,
            ref double eigenvalueConvergenceTolerance)
        {
          
        }
    }
    
    [TestFixture]
    public class Buckling_Set : CsiSet
    {
        
        
        public void SetCase(string name)
        {
          
        }
      

        public void SetInitialCase(string name, 
            string initialCase)
        {
          
        }
        
        public void SetParameters(string name,
            int numberOfBucklingModes,
            double eigenvalueConvergenceTolerance)
        {
          
        }
    }
}
#endif