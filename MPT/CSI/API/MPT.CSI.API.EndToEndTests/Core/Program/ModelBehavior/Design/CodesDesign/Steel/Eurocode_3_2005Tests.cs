using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Steel;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Design.CodesDesign.Steel
{
    [TestFixture]
    public class Eurocode_3_2005_Get : CsiGet
    {
      
        public void GetOverwrite(string name,
            eOverwrites_Eurocode_3_2005 item,
            ref double value,
            ref bool programDetermined)
        {
          
        }
        
        public void GetPreference(ePreferences_Eurocode_3_2005 item,
            ref double value)
        {
          
        }
    }
    
    [TestFixture]
    public class Eurocode_3_2005_Set : CsiSet
    {

        
        public void SetOverwrite(string name,
            eOverwrites_Eurocode_3_2005 item,
            double value,
            eItemType itemType = eItemType.Object)
        {
          
        }

        
        public void SetPreference(ePreferences_Eurocode_3_2005 item,
            double value)
        {
          
        }
    }
}
