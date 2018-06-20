#if BUILD_CSiBridgev18 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
using NUnit.Framework;
using MPT.CSI.API.Core.Program;

using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Design.CodesDesign.Concrete
{
    [TestFixture]
    public class AASHTO_2007_Get : CsiGet
    {
      
        public void GetOverwrite(string name,
            eOverwrites_AASHTO_07 item,
            ref double value,
            ref bool programDetermined)
        {
           
           
        }

        
        public void GetPreference(ePreferences_AASHTO_07 item,
            ref double value)
        {
          
        }
    }
    
    [TestFixture]
    public class AASHTO_2007_Set : CsiSet
    {
        public void SetOverwrite(string name,
            eOverwrites_AASHTO_07 item,
            double value,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
        public void SetPreference(ePreferences_AASHTO_07 item,
            double value)
        {
          
        }
    }
}
#endif
