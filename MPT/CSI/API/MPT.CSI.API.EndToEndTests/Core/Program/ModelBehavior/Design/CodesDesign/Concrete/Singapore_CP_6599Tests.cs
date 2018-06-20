#if BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_SAP2000v18 || BUILD_SAP2000v19 || BUILD_SAP2000v20
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Concrete;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Design.CodesDesign.Concrete
{
    [TestFixture]
    public class Singapore_CP_6599_Get : CsiGet
    {
      
        public void GetOverwrite(string name,
            eOverwrites_Singapore_CP_6599 item,
            ref double value,
            ref bool programDetermined)
        {
          
        }


        
        public void GetPreference(ePreferences_Singapore_CP_6599 item,
            ref double value)
        {
          
        }
    }
    
    [TestFixture]
    public class Singapore_CP_6599_Set : CsiSet
    {
        
        public void SetOverwrite(string name,
            eOverwrites_TS_500_2000 item,
            double value,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetPreference(ePreferences_Singapore_CP_6599 item,
            double value)
        {
          
        }
    }
}
#endif