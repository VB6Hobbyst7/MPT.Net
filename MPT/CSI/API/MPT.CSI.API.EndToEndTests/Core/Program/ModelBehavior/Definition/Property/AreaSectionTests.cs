using System.Linq;
using NUnit.Framework;
using MPT.CSI.API.Core.Program;

using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Property
{
    [TestFixture]
    public class AreaSection_Get : CsiGet
    {
        [Test]
        public void Count()
        {
            int numberOfObjects = _app.Model.Definitions.Properties.AreaSection.Count();
            Assert.That(numberOfObjects, Is.EqualTo(CSiDataArea.NumberOfSectionsExpected));
        }

        [Test]
        public void GetNameList()
        {
            string[] names;
            _app.Model.Definitions.Properties.AreaSection.GetNameList(out names);
            
            Assert.That(names.Length, Is.EqualTo(CSiDataArea.NumberOfSectionsExpected));
            Assert.That(names.Contains(CSiDataArea.NameSectionShellThin));
            Assert.That(names.Contains(CSiDataArea.NameSectionShellThick));
            Assert.That(names.Contains(CSiDataArea.NameSectionShellLayered));
            Assert.That(names.Contains(CSiDataArea.NameSectionPlateThin));
            Assert.That(names.Contains(CSiDataArea.NameSectionPlateThick));
            Assert.That(names.Contains(CSiDataArea.NameSectionMembrane));
            Assert.That(names.Contains(CSiDataArea.NameSectionPlaneStrain));
            Assert.That(names.Contains(CSiDataArea.NameSectionPlaneStress));
            Assert.That(names.Contains(CSiDataArea.NameSectionASolid));
        }

        [Test]
        public void GetNameList_By_Type_All()
        {
            string[] names;
            eAreaSectionType areaType = eAreaSectionType.All;
            _app.Model.Definitions.Properties.AreaSection.GetNameList(out names, areaType);

            Assert.That(names.Length, Is.EqualTo(CSiDataArea.NumberOfSectionsExpected));
            Assert.That(names.Contains(CSiDataArea.NameSectionShellThin));
            Assert.That(names.Contains(CSiDataArea.NameSectionShellThick));
            Assert.That(names.Contains(CSiDataArea.NameSectionShellLayered));
            Assert.That(names.Contains(CSiDataArea.NameSectionPlateThin));
            Assert.That(names.Contains(CSiDataArea.NameSectionPlateThick));
            Assert.That(names.Contains(CSiDataArea.NameSectionMembrane));
            Assert.That(names.Contains(CSiDataArea.NameSectionPlaneStrain));
            Assert.That(names.Contains(CSiDataArea.NameSectionPlaneStress));
            Assert.That(names.Contains(CSiDataArea.NameSectionASolid));
        }

        [Test]
        public void GetNameList_By_Type_ASolid()
        {
            string[] names;
            eAreaSectionType areaType = eAreaSectionType.ASolid;
            _app.Model.Definitions.Properties.AreaSection.GetNameList(out names, areaType);

            Assert.That(names.Length, Is.EqualTo(CSiDataArea.NumberOfSectionsASolidExpected));
            Assert.That(names.Contains(CSiDataArea.NameSectionASolid));
        }

        [Test]
        public void GetNameList_By_Type_Plane()
        {
            string[] names;
            eAreaSectionType areaType = eAreaSectionType.Plane;
            _app.Model.Definitions.Properties.AreaSection.GetNameList(out names, areaType);

            Assert.That(names.Length, Is.EqualTo(CSiDataArea.NumberOfSectionsPlaneExpected));
            Assert.That(names.Contains(CSiDataArea.NameSectionPlaneStrain));
            Assert.That(names.Contains(CSiDataArea.NameSectionPlaneStress));
        }

        [Test]
        public void GetNameList_By_Type_Shell()
        {
            string[] names;
            eAreaSectionType areaType = eAreaSectionType.Shell;
            _app.Model.Definitions.Properties.AreaSection.GetNameList(out names, areaType);

            Assert.That(names.Length, Is.EqualTo(CSiDataArea.NumberOfSectionsShellExpected));   
            Assert.That(names.Contains(CSiDataArea.NameSectionShellThin));
            Assert.That(names.Contains(CSiDataArea.NameSectionShellThick));
            Assert.That(names.Contains(CSiDataArea.NameSectionShellLayered));
            Assert.That(names.Contains(CSiDataArea.NameSectionPlateThin));
            Assert.That(names.Contains(CSiDataArea.NameSectionPlateThick));
            Assert.That(names.Contains(CSiDataArea.NameSectionMembrane));
        }


        [Test]
        public void GetModifiers_Has_No_Modifiers()
        {
            AreaModifier oldModifiers = new AreaModifier
            {
                MembraneF11 = 1,
                MembraneF22 = 1,
                MembraneF12 = 1,
                BendingM11 = 1,
                BendingM22 = 1,
                BendingM12 = 1,
                ShearV13 = 1,
                ShearV23 = 1,
                MassModifier = 1,
                WeightModifier = 1
            };

            AreaModifier modifier;
            _app.Model.Definitions.Properties.AreaSection.GetModifiers(CSiDataArea.NameSectionPlateThick, out modifier);

            Assert.That(modifier.MembraneF11, Is.EqualTo(oldModifiers.MembraneF11));
            Assert.That(modifier.MembraneF22, Is.EqualTo(oldModifiers.MembraneF22));
            Assert.That(modifier.MembraneF12, Is.EqualTo(oldModifiers.MembraneF12));
            Assert.That(modifier.BendingM11, Is.EqualTo(oldModifiers.BendingM11));
            Assert.That(modifier.BendingM22, Is.EqualTo(oldModifiers.BendingM22));
            Assert.That(modifier.BendingM12, Is.EqualTo(oldModifiers.BendingM12));
            Assert.That(modifier.ShearV13, Is.EqualTo(oldModifiers.ShearV13));
            Assert.That(modifier.ShearV23, Is.EqualTo(oldModifiers.ShearV23));
            Assert.That(modifier.MassModifier, Is.EqualTo(oldModifiers.MassModifier));
            Assert.That(modifier.WeightModifier, Is.EqualTo(oldModifiers.WeightModifier));
        }

        [Test]
        public void GetModifiers_Has_Modifiers()
        {
            AreaModifier oldModifiers = new AreaModifier
            {
                MembraneF11 = 1.7,
                MembraneF22 = 1.8,
                MembraneF12 = 1.9,
                BendingM11 = 2.0,
                BendingM22 = 2.1,
                BendingM12 = 2.2,
                ShearV13 = 2.3,
                ShearV23 = 2.4,
                MassModifier = 2.5,
                WeightModifier = 2.6
            };

            AreaModifier modifier;
            _app.Model.Definitions.Properties.AreaSection.GetModifiers(CSiDataArea.NameSectionShellThin, out modifier);

            Assert.That(modifier.MembraneF11, Is.EqualTo(oldModifiers.MembraneF11));
            Assert.That(modifier.MembraneF22, Is.EqualTo(oldModifiers.MembraneF22));
            Assert.That(modifier.MembraneF12, Is.EqualTo(oldModifiers.MembraneF12));
            Assert.That(modifier.BendingM11, Is.EqualTo(oldModifiers.BendingM11));
            Assert.That(modifier.BendingM22, Is.EqualTo(oldModifiers.BendingM22));
            Assert.That(modifier.BendingM12, Is.EqualTo(oldModifiers.BendingM12));
            Assert.That(modifier.ShearV13, Is.EqualTo(oldModifiers.ShearV13));
            Assert.That(modifier.ShearV23, Is.EqualTo(oldModifiers.ShearV23));
            Assert.That(modifier.MassModifier, Is.EqualTo(oldModifiers.MassModifier));
            Assert.That(modifier.WeightModifier, Is.EqualTo(oldModifiers.WeightModifier));
        }

        [TestCase(CSiDataArea.NameSectionShellThin, ExpectedResult = eAreaSectionType.Shell)]
        [TestCase(CSiDataArea.NameSectionShellThick, ExpectedResult = eAreaSectionType.Shell)]
        [TestCase(CSiDataArea.NameSectionShellLayered, ExpectedResult = eAreaSectionType.Shell)]
        [TestCase(CSiDataArea.NameSectionPlateThin, ExpectedResult = eAreaSectionType.Shell)]
        [TestCase(CSiDataArea.NameSectionPlateThick, ExpectedResult = eAreaSectionType.Shell)]
        [TestCase(CSiDataArea.NameSectionMembrane, ExpectedResult = eAreaSectionType.Shell)]
        [TestCase(CSiDataArea.NameSectionPlaneStrain, ExpectedResult = eAreaSectionType.Plane)]
        [TestCase(CSiDataArea.NameSectionPlaneStress, ExpectedResult = eAreaSectionType.Plane)]
        [TestCase(CSiDataArea.NameSectionASolid, ExpectedResult = eAreaSectionType.ASolid)]
        public eAreaSectionType GetType(string name)
        {
            eAreaSectionType areaPropertyType;
            _app.Model.Definitions.Properties.AreaSection.GetType(name, out areaPropertyType);
            return areaPropertyType;
        }


        public void GetShellDesign(string name,
            ref string nameMaterial,
            ref eShellSteelLayoutOption rebarLayout,
            ref double designCoverTopDirection1,
            ref double designCoverTopDirection2,
            ref double designCoverBottomDirection1,
            ref double designCoverBottomDirection2)
        {
          
        }

        
        public void GetShellLayer(string name,
            ref string[] layerNames,
            ref double[] distanceOffsets,
            ref double[] thicknesses,
            ref eShellLayerType[] layerTypes,
            ref int[] numberOfIntegrationPoints,
            ref string[] materialProperties,
            ref double[] materialAngles,
            ref eMaterialComponentBehaviorType[] S11Types,
            ref eMaterialComponentBehaviorType[] S22Types,
            ref eMaterialComponentBehaviorType[] S12Types)
        {
          
        }


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        public void GetShell(string name,
            ref eShellType shellType,
            ref bool includeDrillingDOF,
            ref string nameMaterial,
            ref double materialAngle,
            ref double membraneThickness,
            ref double bendingThickness,
            ref int color,
            ref string notes,
            ref string GUID)
        {

        }

        
        public void GetPlane(string name,
            ref ePlaneType planeType,
            ref string nameMaterial,
            ref double materialAngle,
            ref double thickness,
            ref bool incompatibleModes,
            ref int color,
            ref string notes,
            ref string GUID)
        {

        }


        public void GetASolid(string name,
            ref string nameMaterial,
            ref double materialAngle,
            ref double arcAngle,
            ref bool incompatibleBendingModes,
            ref string coordinateSystem,
            ref int color,
            ref string notes,
            ref string GUID)
        {

        }
#endif
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017

        public void GetDeck(string name,
            ref double thickness,
            ref eDeckType deckType,
            ref eShellType shellType,
            ref string nameMaterial,
            ref int color,
            ref string notes,
            ref string GUID)
        {
          
        }

        
        public void GetDeckFilled(string name,
            ref double slabDepth,
            ref double ribDepth,
            ref double ribWidthTop,
            ref double ribWidthBottom,
            ref double ribSpacing,
            ref double shearThickness,
            ref double unitWeight,
            ref double shearStudDiameter,
            ref double shearStudHeight,
            ref double shearStudFu)
        {
          
        }

        
        public void GetDeckUnfilled(string name,
            ref double ribDepth,
            ref double ribWidthTop,
            ref double ribWidthBottom,
            ref double ribSpacing,
            ref double shearThickness,
            ref double unitWeight)
        {
          
        }

        
        public void GetDeckSolidSlab(string name,
            ref double slabDepth,
            ref double shearStudDiameter,
            ref double shearStudHeight,
            ref double shearStudFu)
        {
          
        }

        
        public void GetSlab(string name,
            ref double thickness,
            ref eSlabType slabType,
            ref eShellType shellType,
            ref string nameMaterial,
            ref int color,
            ref string notes,
            ref string GUID)
        {
          
        }

        
        public void GetSlabRibbed(string name,
            ref double overallDepth,
            ref double slabThickness,
            ref double stemWidthTop,
            ref double stemWithBottom,
            ref double ribSpacing,
            ref eLocalAxisPlane ribsParallelToAxis)
        {
          
        }

        
        public void GetSlabWaffle(string name,
            ref double overallDepth,
            ref double slabThickness,
            ref double stemWidthTop,
            ref double stemWithBottom,
            ref double ribSpacingLocal1,
            ref double ribSpacingLocal2)
        {
          
        }

        
        public void GetWall(string name,
            ref double thickness,
            ref eWallPropertyType walltype,
            ref eShellType shellType,
            ref string nameMaterial,
            ref int color,
            ref string notes,
            ref string GUID)
        {
          
        }

        
        public void GetWallAutoSelectList(string name,
            ref string[] autoSelectList,
            ref string startingProperty)
        {
          
        }
#endif
    }
    
    [TestFixture]
    public class AreaSection_Set : CsiSet
    {
      
        public void ChangeName(string currentName, 
            string newName)
        {
          
        }

        
        public void Delete(string name)
        {
          
        }

        
        public void SetModifiers(string name, 
            AreaModifier modifiers)
        {
          
        }

        
        public void SetShellDesign(string name,
            string nameMaterial,
            eShellSteelLayoutOption rebarLayout,
            double designCoverTopDirection1,
            double designCoverTopDirection2,
            double designCoverBottomDirection1,
            double designCoverBottomDirection2)
        {
          
        }

        
        public void SetShellLayer(string name,
            string[] layerNames,
            double[] distanceOffsets,
            double[] thicknesses,
            eShellLayerType[] layerTypes,
            int[] numberOfIntegrationPoints,
            string[] materialProperties,
            double[] materialAngles,
            eMaterialComponentBehaviorType[] S11Types,
            eMaterialComponentBehaviorType[] S22Types,
            eMaterialComponentBehaviorType[] S12Types)
        {
          
        }
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        
        public void SetASolid(string name,
            string nameMaterial,
            double materialAngle,
            double arcAngle,
            bool incompatibleBendingModes,
            string coordinateSystem = CoordinateSystems.Global,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }

        
        public void SetPlane(string name,
            ePlaneType planeType,
            string nameMaterial,
            double materialAngle,
            double thickness,
            bool incompatibleModes,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }
      

        
        public void SetShell(string name,
            eShellType shellType,
            bool includeDrillingDOF,
            string nameMaterial,
            double materialAngle,
            double membraneThickness,
            double bendingThickness,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }
#endif
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017

        
        public void SetDeck(string name,
            double thickness,
            eDeckType deckType,
            eShellType shellType,
            string nameMaterial,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }

        
        public void SetDeckFilled(string name,
            double slabDepth,
            double ribDepth,
            double ribWidthTop,
            double ribWidthBottom,
            double ribSpacing,
            double shearThickness,
            double unitWeight,
            double shearStudDiameter,
            double shearStudHeight,
            double shearStudFu)
        {
          
        }


        
        public void SetDeckUnfilled(string name,
            double ribDepth,
            double ribWidthTop,
            double ribWidthBottom,
            double ribSpacing,
            double shearThickness,
            double unitWeight)
        {
          
        }

        
        public void SetDeckSolidSlab(string name,
            double slabDepth,
            double shearStudDiameter,
            double shearStudHeight,
            double shearStudFu)
        {
          
        }
        
        
        public void SetSlab(string name,
            double thickness,
            eSlabType slabType,
            eShellType shellType,
            string nameMaterial,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }
        
        
        public void SetSlabRibbed(string name,
            double overallDepth,
            double slabThickness,
            double stemWidthTop,
            double stemWithBottom,
            double ribSpacing,
            eLocalAxisPlane ribsParallelToAxis)
        {
          
        }

        
        public void SetSlabWaffle(string name,
            double overallDepth,
            double slabThickness,
            double stemWidthTop,
            double stemWithBottom,
            double ribSpacingLocal1,
            double ribSpacingLocal2)
        {
          
        }


        
        public void SetWall(string name,
            double thickness,
            eWallPropertyType walltype,
            eShellType shellType,
            string nameMaterial,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }

        
        public void SetWallAutoSelectList(string name,
            string[] autoSelectList,
            string startingProperty = "Median")
        {
          
        }
#endif
    }
}
