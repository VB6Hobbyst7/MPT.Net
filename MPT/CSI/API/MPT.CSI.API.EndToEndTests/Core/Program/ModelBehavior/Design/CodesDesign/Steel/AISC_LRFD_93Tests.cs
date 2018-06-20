#if !BUILD_CSiBridgev18 && !BUILD_CSiBridgev19 && !BUILD_CSiBridgev20
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Steel;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Design.CodesDesign.Steel
{
    [TestFixture]
    public class AISC_LRFD_93_Get : CsiGet
    {
      
        public void GetOverwrite(string name,
            eOverwrites_AISC_LRFD_93 item,
            ref double value,
            ref bool programDetermined)
        {
          
        }

        
        

        
        public void GetPreference(ePreferences_AISC_LRFD_93 item,
            ref double value)
        {
          
        }
    }
    
    [TestFixture]
    public class AISC_LRFD_93_Set : CsiSet
    {
        public void SetOverwrite(string name,
            eOverwrites_AISC_LRFD_93 item,
            double value,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        
        public void SetPreference(ePreferences_AISC_LRFD_93 item,
            double value)
        {
          
        }
    }
}
#endif