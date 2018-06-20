using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition
{
    [TestFixture]
    public class Functions_Get : CsiGet
    {

        public void Count()
        {
          
        }

        
        public void GetNameList(ref string[] names)
        {
          
        }

        
        public void GetType(string name,
            ref eFunctionType functionType,
            ref int functionSubType)
        {
          
        }

        
        public void GetValues(string name,
            ref int numberItems,
            ref double[] timeValue,
            ref double[] functionValue)
        {
          
        }
    }
    
    [TestFixture]
    public class Functions_Set : CsiSet
    {
      
        public void ChangeName(string currentName, string newName)
        {
          
        }

        
        public void Delete(string name)
        {
          
        }

        
        public void ConvertToUser(string name)
        {
          
        }
    }
}
