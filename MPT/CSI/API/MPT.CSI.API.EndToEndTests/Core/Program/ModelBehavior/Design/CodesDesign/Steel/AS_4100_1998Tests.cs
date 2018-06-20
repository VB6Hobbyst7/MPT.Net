#if !BUILD_CSiBridgev18 && !BUILD_CSiBridgev19 && !BUILD_CSiBridgev20
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Steel;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Design.CodesDesign.Steel
{
    [TestFixture]
    public class AS_4100_1998_Get : CsiGet
    {
      
        public void GetOverwrite(string name,
            eOverwrites_AS_4100_1998 item,
            ref double value,
            ref bool programDetermined)
        {
          
        }

        
        public void GetPreference(ePreferences_AS_4100_1998 item,
            ref double value)
        {
          
        }
    }
    
    [TestFixture]
    public class AS_4100_1998_Set : CsiSet
    {

        
        public void SetOverwrite(string name,
            eOverwrites_AS_4100_1998 item,
            double value,
            eItemType itemType = eItemType.Object)
        {
        
        
        }      

        
        public void SetPreference(ePreferences_AS_4100_1998 item,
            double value)
        {
        
        
        }
    }
}
#endif