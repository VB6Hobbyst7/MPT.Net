#if BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_SAP2000v18 || BUILD_SAP2000v19 || BUILD_SAP2000v20
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Design.CodesDesign.Steel
{
    [TestFixture]
    public class Chinese_2002_Get : CsiGet
    {
      
        public void GetOverwrite(string name,
            int item,
            ref double value,
            ref bool programDetermined)
        {
          
        }
        
        public void GetPreference(int item,
            ref double value)
        {
          
        }
    }
    
    [TestFixture]
    public class Chinese_2002_Set : CsiSet
    {
        
        public void SetOverwrite(string name,
            int item,
            double value,
            eItemType itemType = eItemType.Object)
        {
          
        }
      
      
        public void SetPreference(int item,
            double value)
        {
          
        }
    }
}
#endif