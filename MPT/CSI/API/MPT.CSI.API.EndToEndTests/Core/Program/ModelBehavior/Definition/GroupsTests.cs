using System.Linq;
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition
{
    [TestFixture]
    public class Groups_Get : CsiGet
    {
      
        public void Count()
        {
          
        }

        
        public void GetNameList(ref string[] groupNames)
        {
          
        }


        public void GetAssignments(string nameGroup,
            ref eObjectType[] objectTypes,
            ref string[] objectNames)
        {
          
        }


        
        public void GetGroup(string name,
            ref int color,
            ref bool specifiedForSelection,
            ref bool specifiedForSectionCutDefinition,
            ref bool specifiedForSteelDesign,
            ref bool specifiedForConcreteDesign,
            ref bool specifiedForAluminumDesign,
            ref bool specifiedForColdFormedDesign,
            ref bool specifiedForStaticNLActiveStage,
            ref bool specifiedForBridgeResponseOutput,
            ref bool specifiedForAutoSeismicOutput,
            ref bool specifiedForAutoWindOutput,
            ref bool specifiedForMassAndWeight)
        {
          
        }

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017

        public void GetGroup(string name,
            ref int color,
            ref bool specifiedForSelection,
            ref bool specifiedForSectionCutDefinition,
            ref bool specifiedForSteelDesign,
            ref bool specifiedForConcreteDesign,
            ref bool specifiedForStaticNLActiveStage,
            ref bool specifiedForAutoSeismicOutput,
            ref bool specifiedForAutoWindOutput,
            ref bool specifiedForMassAndWeight,
            ref bool specifiedForSteelJoistDesign,
            ref bool specifiedForWallDesign,
            ref bool specifiedForBasePlateDesign,
            ref bool specifiedForConnectionDesign)
        {
          
        }
#endif
    }
    
    [TestFixture]
    public class Groups_Set : CsiSet
    {

        
        public void Delete(string nameGroup)
        {
          
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        public void ChangeName(string nameExisting,
            string nameNew)
        {
          
        }

        
        public void Clear(string nameGroup)
        {
          
        }
#endif

        
        public void SetGroup(string name,
            int color = -1,
            bool specifiedForSelection = true,
            bool specifiedForSectionCutDefinition = true,
            bool specifiedForSteelDesign = true,
            bool specifiedForConcreteDesign = true,
            bool specifiedForAluminumDesign = true,
            bool specifiedForColdFormedDesign = true,
            bool specifiedForStaticNLActiveStage = true,
            bool specifiedForBridgeResponseOutput = true,
            bool specifiedForAutoSeismicOutput = true,
            bool specifiedForAutoWindOutput = true,
            bool specifiedForMassAndWeight = true)
        {
          
        }
        
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        
        public void SetGroup(string name,
            int color = -1,
            bool specifiedForSelection = true,
            bool specifiedForSectionCutDefinition = true,
            bool specifiedForSteelDesign = true,
            bool specifiedForConcreteDesign = true,
            bool specifiedForStaticNLActiveStage = true,
            bool specifiedForAutoSeismicOutput = true,
            bool specifiedForAutoWindOutput = true,
            bool specifiedForMassAndWeight = true,
            bool specifiedForSteelJoistDesign = true,
            bool specifiedForWallDesign = true,
            bool specifiedForBasePlateDesign = true,
            bool specifiedForConnectionDesign = true)
        {
          
        }
#endif
    }
}
