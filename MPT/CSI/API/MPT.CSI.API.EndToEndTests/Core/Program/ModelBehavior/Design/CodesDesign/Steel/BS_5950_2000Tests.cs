#if !BUILD_CSiBridgev18 && !BUILD_CSiBridgev19 && !BUILD_CSiBridgev20
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Steel;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Design.CodesDesign.Steel
{
    [TestFixture]
    public class BS_5950_2000_Get : CsiGet
    {
      
        public void GetOverwrite(string name,
            eOverwrites_BS_5950_20005 item,
            ref double value,
            ref bool programDetermined)
        {
          
        }
        
        public void GetPreference(ePreferences_BS_5950_2000 item,
            ref double value)
        {
          
        }
    }
    
    [TestFixture]
    public class BS_5950_2000_Set : CsiSet
    {
        
        public void SetOverwrite(string name,
            eOverwrites_BS_5950_20005 item,
            double value,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        
        public void SetPreference(ePreferences_BS_5950_2000 item,
            double value)
        {
          
        }
    }
}
#endif