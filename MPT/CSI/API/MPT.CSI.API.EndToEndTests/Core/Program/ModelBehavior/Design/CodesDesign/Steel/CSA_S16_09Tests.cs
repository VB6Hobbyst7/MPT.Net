using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Steel;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Design.CodesDesign.Steel
{
    [TestFixture]
    public class CSA_S16_09_Get : CsiGet
    {
      
        public void GetOverwrite(string name,
            eOverwrites_CSA_S16_09 item,
            ref double value,
            ref bool programDetermined)
        {
          
        }
        
        
        public void GetPreference(ePreferences_CSA_S16_09 item,
            ref double value)
        {
          
        }
    }
    
    [TestFixture]
    public class CSA_S16_09_Set : CsiSet
    {
      
        public void SetOverwrite(string name,
            eOverwrites_CSA_S16_09 item,
            double value,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        
        public void SetPreference(ePreferences_CSA_S16_09 item,
            double value)
        {
          
        }
    }
}
