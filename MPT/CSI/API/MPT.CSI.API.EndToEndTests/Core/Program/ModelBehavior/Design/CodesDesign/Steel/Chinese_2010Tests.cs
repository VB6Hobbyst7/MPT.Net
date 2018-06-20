#if !BUILD_CSiBridgev18 && !BUILD_CSiBridgev19 && !BUILD_CSiBridgev20
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Steel;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Design.CodesDesign.Steel
{
    [TestFixture]
    public class Chinese_2010_Get : CsiGet
    {
      
        public void GetOverwrite(string name,
            eOverwrites_Chinese_2010 item,
            ref double value,
            ref bool programDetermined)
        {
          
        }


        
        public void GetPreference(ePreferences_Chinese_2010 item,
            ref double value)
        {
          
        }
    }
    
    [TestFixture]
    public class Chinese_2010_Set : CsiSet
    {

        
        public void SetOverwrite(string name,
            eOverwrites_Chinese_2010 item,
            double value,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetPreference(ePreferences_Chinese_2010 item,
            double value)
        {
          
        }
    }
}
#endif