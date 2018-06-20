#if BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_SAP2000v18 || BUILD_SAP2000v19 || BUILD_SAP2000v20
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.ColdFormed;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Design.CodesDesign.ColdFormed
{
    [TestFixture]
    public class AISI_LRFD_96_Get : CsiGet
    {
      
        public void GetOverwrite(string name,
            eOverwrites_AISI_LRFD_96 item,
            ref double value,
            ref bool programDetermined)
        {
          
        }


        public void GetPreference(ePreferences_AISI_LRFD_96 item,
            ref double value)
        {       
        
        }
    }
    
    [TestFixture]
    public class AISI_LRFD_96_Set : CsiSet
    {

        public void SetOverwrite(string name,
            eOverwrites_AISI_LRFD_96 item,
            double value,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetPreference(ePreferences_AISI_LRFD_96 item,
            double value)
        {
          
        }
    }
}
#endif