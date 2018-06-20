#if BUILD_CSiBridgev18 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
using NUnit.Framework;
using MPT.CSI.API.Core.Program;

using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Design.CodesDesign.Concrete
{
    [TestFixture]
    public class AASHTO_LRFD_2012_Get : CsiGet
    {
      
        public void GetOverwrite(string name,
            eOverwrites_AASHTO_LRFD_2012 item,
            ref double value,
            ref bool programDetermined)
        {
          
        }

     
        public void GetPreference(ePreferences_AASHTO_LRFD_2012 item,
            ref double value)
        {
          
        }
    }
    
     [TestFixture]
    public class AASHTO_LRFD_2012_Set : CsiSet
    {
         
        public void SetOverwrite(string name,
            eOverwrites_AASHTO_LRFD_2012 item,
            double value,
            eItemType itemType = eItemType.Object)
        {
          
        }
        
          
        public void SetPreference(ePreferences_AASHTO_LRFD_2012 item,
            double value)
        {
          
        }
    }
}
#endif
