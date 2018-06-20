using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Concrete;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Design.CodesDesign.Concrete
{
    [TestFixture]
    public class Eurocode_2_2004_Get : CsiGet
    {
      
        public void GetOverwrite(string name,
            eOverwrites_Eurocode_2_2004 item,
            ref double value,
            ref bool programDetermined)
        {
          
        }

        
        public void GetPreference(ePreferences_Eurocode_2_2004 item,
            ref double value)
        {
          
        }
    }
    
    [TestFixture]
    public class Eurocode_2_2004_Set : CsiSet
    {

        
        public void SetOverwrite(string name,
            eOverwrites_Eurocode_2_2004 item,
            double value,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetPreference(ePreferences_Eurocode_2_2004 item,
            double value)
        {
          
        }
    }
}
