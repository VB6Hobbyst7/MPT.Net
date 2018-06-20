#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Steel;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Design.CodesDesign.Steel
{
    [TestFixture]
    public class AISC_ASD_89_Get : CsiGet
    {
      
        public void GetOverwrite(string name,
            eOverwrites_AISC_ASD_89 item,
            ref double value,
            ref bool programDetermined)
        {
          
        }

        
        public void GetPreference(ePreferences_AISC_ASD_89 item,
            ref double value)
        {
          
        }
    }
    
    [TestFixture]
    public class AISC_ASD_89_Set : CsiSet
    {
      
        public void SetOverwrite(string name,
            eOverwrites_AISC_ASD_89 item,
            double value,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        
        public void SetPreference(ePreferences_AISC_ASD_89 item,
            double value)
        {
          
        }
    }
}
#endif