#if !BUILD_CSiBridgev18 && !BUILD_CSiBridgev19 && !BUILD_CSiBridgev20
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Steel;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Design.CodesDesign.Steel
{
    [TestFixture]
    public class NZS_3404_1997_Get : CsiGet
    {
      
        public void GetOverwrite(string name,
            eOverwrites_NZS_3404_1997 item,
            ref double value,
            ref bool programDetermined)
        {
          
        }

        
        public void GetPreference(ePreferences_NZS_3404_1997 item,
            ref double value)
        {
          
        }
    }
    
    [TestFixture]
    public class NZS_3404_1997_Set : CsiSet
    {

        public void SetOverwrite(string name,
            eOverwrites_NZS_3404_1997 item,
            double value,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetPreference(ePreferences_NZS_3404_1997 item,
            double value)
        {
          
        }
    }
}
#endif