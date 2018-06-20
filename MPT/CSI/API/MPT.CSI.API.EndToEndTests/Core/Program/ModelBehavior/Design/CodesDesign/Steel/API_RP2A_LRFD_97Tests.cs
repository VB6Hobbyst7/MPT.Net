#if BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_SAP2000v18 || BUILD_SAP2000v19 || BUILD_SAP2000v20
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Steel;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Design.CodesDesign.Steel
{
    [TestFixture]
    public class API_RP2A_LRFD_97_Get : CsiGet
    {
      
        public void GetOverwrite(string name,
            eOverwrites_API_RP2A_LRFD_97 item,
            ref double value,
            ref bool programDetermined)
        {
          
        }


        public void GetPreference(ePreferences_API_RP2A_LRFD_97 item,
            ref double value)
        {
          
        }
    }
    
    [TestFixture]
    public class API_RP2A_LRFD_97_Set : CsiSet
    {
      
        
        public void SetOverwrite(string name,
            eOverwrites_API_RP2A_LRFD_97 item,
            double value,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        
        public void SetPreference(ePreferences_API_RP2A_LRFD_97 item,
            double value)
        {
          
        }
    }
}
#endif