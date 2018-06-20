using System.Linq;
using NUnit.Framework;
using MPT.CSI.API.Core.Program;

using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.Frame;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Property
{
    [TestFixture]
    public class FrameSection_Get : CsiGet
    {

        [Test]
        public void Count()
        {
            int numberOfObjects = _app.Model.Definitions.Properties.FrameSection.Count();
            Assert.That(numberOfObjects, Is.EqualTo(CSiDataLine.NumberOfSectionFramesExpected));
        }

        [Test]
        public void GetNameList()
        {
            string[] names;
            _app.Model.Definitions.Properties.FrameSection.GetNameList(out names);

            Assert.That(names.Length, Is.EqualTo(CSiDataLine.NumberOfSectionFramesExpected));
            Assert.That(names.Contains(CSiDataLine.NameSectionFrame));
            Assert.That(names.Contains(CSiDataLine.NameSectionFrameCol));
        }

        [Test]
        public void GetNameList_By_Type_All()
        {
            eFrameSectionType frameType = eFrameSectionType.All;
            string[] names;
            _app.Model.Definitions.Properties.FrameSection.GetNameList(out names, frameType);

            Assert.That(names.Length, Is.EqualTo(CSiDataLine.NumberOfSectionFramesExpected));
            Assert.That(names.Contains(CSiDataLine.NameSectionFrame));
            Assert.That(names.Contains(CSiDataLine.NameSectionFrameCol));
        }

        [Test]
        public void GetModifiers_Has_No_Modifers()
        {
            FrameModifier oldModifiers = new FrameModifier
            {
                CrossSectionalArea = 1,
                ShearV2 = 1,
                ShearV3 = 1,
                Torsion = 1,
                BendingM2 = 1,
                BendingM3 = 1,
                MassModifier = 1,
                WeightModifier = 1,
            };

            FrameModifier modifier;
            _app.Model.Definitions.Properties.FrameSection.GetModifiers(CSiDataLine.NameSectionFrameCol, out modifier);

            Assert.That(modifier.CrossSectionalArea, Is.EqualTo(oldModifiers.CrossSectionalArea));
            Assert.That(modifier.ShearV2, Is.EqualTo(oldModifiers.ShearV2));
            Assert.That(modifier.ShearV3, Is.EqualTo(oldModifiers.ShearV3));
            Assert.That(modifier.Torsion, Is.EqualTo(oldModifiers.Torsion));
            Assert.That(modifier.BendingM2, Is.EqualTo(oldModifiers.BendingM2));
            Assert.That(modifier.BendingM3, Is.EqualTo(oldModifiers.BendingM3));
            Assert.That(modifier.MassModifier, Is.EqualTo(oldModifiers.MassModifier));
            Assert.That(modifier.WeightModifier, Is.EqualTo(oldModifiers.WeightModifier));
        }

        [Test]
        public void GetModifiers_Has_Modifers()
        {
            FrameModifier oldModifiers = new FrameModifier
            {
                CrossSectionalArea = 1.5,
                ShearV2 = 1.6,
                ShearV3 = 1.7,
                Torsion = 1.8,
                BendingM2 = 1.9,
                BendingM3 = 2.0,
                MassModifier = 2.1,
                WeightModifier = 2.2,
            };

            FrameModifier modifier;
            _app.Model.Definitions.Properties.FrameSection.GetModifiers(CSiDataLine.NameSectionFrame, out modifier);

            Assert.That(modifier.CrossSectionalArea, Is.EqualTo(oldModifiers.CrossSectionalArea));
            Assert.That(modifier.ShearV2, Is.EqualTo(oldModifiers.ShearV2));
            Assert.That(modifier.ShearV3, Is.EqualTo(oldModifiers.ShearV3));
            Assert.That(modifier.Torsion, Is.EqualTo(oldModifiers.Torsion));
            Assert.That(modifier.BendingM2, Is.EqualTo(oldModifiers.BendingM2));
            Assert.That(modifier.BendingM3, Is.EqualTo(oldModifiers.BendingM3));
            Assert.That(modifier.MassModifier, Is.EqualTo(oldModifiers.MassModifier));
            Assert.That(modifier.WeightModifier, Is.EqualTo(oldModifiers.WeightModifier));
        }


        [TestCase(CSiDataLine.NameSectionFrame, ExpectedResult = eFrameSectionType.ISection)]
        public eFrameSectionType GetType(string name)
        {
            eFrameSectionType frameSectionType;
            _app.Model.Definitions.Properties.FrameSection.GetType(CSiDataLine.NameSectionFrame, out frameSectionType);
            return frameSectionType;
        }
#if !BUILD_ETABS2015

        public void GetRebarType(string name,
            ref eRebarType rebarType)
        {
          
        }
        
        
        public void GetNameInPropertyFile(string name,
            ref string nameInFile,
            ref string fileName,
            ref string nameMaterial,
            ref eFrameSectionType frameSectionType)
        {
          
        }
        
        
        public void GetSectionDesignerSection(string name,
            ref string nameMaterial,
            ref string[] shapeNames,
            ref eSectionDesignerSectionType[] sectionTypes,
            ref eSectionDesignerDesignOption designType,
            ref int color,
            ref string notes,
            ref string GUID)
        {
          
        }
#endif
#if BUILD_ETABS2016 || BUILD_ETABS2017

        public void GetSteelTee(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref double tf,
            ref double tw,
            ref double r,
            ref bool mirrorAbout3,
            ref int color,
            ref string notes,
            ref string GUID)
        {
          
        }

        
        public void GetSteelAngle(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref double tf,
            ref double tw,
            ref double r,
            ref bool mirrorAbout2,
            ref bool mirrorAbout3,
            ref int color,
            ref string notes,
            ref string GUID)
        {
          
        }
        

        public void GetConcreteTee(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref double tf,
            ref double twF,
            ref double tfT,
            ref bool mirrorAbout3,
            ref int color,
            ref string notes,
            ref string GUID)
        {
          
        }
        
        
        public void GetConcreteL(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref double tf,
            ref double twC,
            ref double tfT,
            ref bool mirrorAbout2,
            ref bool mirrorAbout3,
            ref int color,
            ref string notes,
            ref string GUID)
        {
          
        }
#endif
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        public void GetAutoSelectAluminum(string name,
            ref string[] sectionNames,
            ref string autoStartSection,
            ref string notes,
            ref string GUID)
        {
          
        }

        
        public void GetAutoSelectColdFormed(string name,
            ref string[] sectionNames,
            ref string autoStartSection,
            ref string notes,
            ref string GUID)
        {
          
        }


        public void GetHybridISection(string name,
            ref string nameMaterialTopFlange,
            ref string nameMaterialWeb,
            ref string nameMaterialBottomFlange,
            ref double t3,
            ref double t2,
            ref double tf,
            ref double tw,
            ref double t2Bottom,
            ref double tfBottom,
            ref int color,
            ref string notes,
            ref string GUID)
        {
          
        }

        
        public void GetHybridUSection(string name,
            ref string nameMaterialTopFlange,
            ref string nameMaterialWeb,
            ref string nameMaterialBottomFlange,
            ref double D1,
            ref double B1,
            ref double B2,
            ref double B3,
            ref double B4,
            ref double tw,
            ref double tf,
            ref double tfb,
            ref int color,
            ref string notes,
            ref string GUID)
        {
          
        }
        

        public void GetColdC(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref double thickness,
            ref double radius,
            ref double lipDepth,
            ref int color,
            ref string notes,
            ref string GUID)
        {
          
        }

        
        public void GetColdHat(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref double thickness,
            ref double radius,
            ref double lipDepth,
            ref int color,
            ref string notes,
            ref string GUID)
        {
          
        }

        
        public void GetColdZ(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref double thickness,
            ref double radius,
            ref double lipDepth,
            ref double lipAngle,
            ref int color,
            ref string notes,
            ref string GUID)
        {
          
        }

        public void GetPrecastI(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double B1,
            ref double B2,
            ref double B3,
            ref double B4,
            ref double D1,
            ref double D2,
            ref double D3,
            ref double D4,
            ref double D5,
            ref double D6,
            ref double D7,
            ref double T1,
            ref double T2,
            ref double C1,
            ref int color,
            ref string notes,
            ref string GUID)
        {
          
        }
        
        
        public void GetPrecastU(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double B1,
            ref double B2,
            ref double B3,
            ref double B4,
            ref double B5,
            ref double B6,
            ref double D1,
            ref double D2,
            ref double D3,
            ref double D4,
            ref double D5,
            ref double D6,
            ref double D7,
            ref int color,
            ref string notes,
            ref string GUID)
        {
          
        }
#endif
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017

        public void GetAllFrameProperties(ref string[] names,
            ref eFrameSectionType[] frameType,
            ref double[] t3,
            ref double[] t2,
            ref double[] tf,
            ref double[] tw,
            ref double[] t2b,
            ref double[] tfb)
        {
          
        }
        

        public void GetPlate(string name,
           ref string fileName,
           ref string nameMaterial,
           ref double t3,
           ref double t2,
           ref int color,
           ref string notes,
           ref string GUID)
        {
          
        }

        
        public void GetRod(string name,
           ref string fileName,
           ref string nameMaterial,
           ref double t3,
           ref int color,
           ref string notes,
           ref string GUID)
        {
          
        }
#endif

       

        public void GetSectionProperties(string name,
            ref double Ag,
            ref double As2,
            ref double As3,
            ref double J,
            ref double I22,
            ref double I33,
            ref double S22,
            ref double S33,
            ref double Z22,
            ref double Z33,
            ref double r22,
            ref double r33)
        {
          
        }


        public void GetPropertyFileNameList(string fileName,
            ref string[] sectionNames,
            ref eFrameSectionType[] frameSectionTypes,
            eFrameSectionType frameSectionType)
        {
          
        }


        


        public void GetGeneral(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref double Ag,
            ref double As2,
            ref double As3,
            ref double J,
            ref double I22,
            ref double I33,
            ref double S22,
            ref double S33,
            ref double Z22,
            ref double Z33,
            ref double r22,
            ref double r33,
            ref int color,
            ref string notes,
            ref string GUID)
        {
          
        }
        
        
        public void GetNonPrismatic(string name,
            ref string[] startSections,
            ref string[] endSections,
            ref double[] lengths,
            ref ePrismaticType[] prismaticTypes,
            ref ePrismaticInertiaType[] EI33,
            ref ePrismaticInertiaType[] EI22,
            ref int color,
            ref string notes,
            ref string GUID)
        {
          
        }



        public void GetAutoSelectSteel(string name,
            ref string[] sectionNames,
            ref string autoStartSection,
            ref string notes,
            ref string GUID)
        {
          
        }


        public void GetChannel(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref double tf,
            ref double tw,
            ref int color,
            ref string notes,
            ref string GUID)
        {
          
        }

        
        public void GetDoubleAngle(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref double tf,
            ref double tw,
            ref double separation,
            ref int color,
            ref string notes,
            ref string GUID)
        {
          
        }

        
        public void GetDoubleChannel(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref double tf,
            ref double tw,
            ref double separation,
            ref int color,
            ref string notes,
            ref string GUID)
        {
          
        }



        [Test]
        public void GetNameList_By_Type_ISection()
        {
            eFrameSectionType frameType = eFrameSectionType.ISection;
            string[] names;
            _app.Model.Definitions.Properties.FrameSection.GetNameList(out names, frameType);

            Assert.That(names.Length, Is.EqualTo(CSiDataLine.NumberOfSectionFramesExpected));
            Assert.That(names.Contains(CSiDataLine.NameSectionFrame));
            Assert.That(names.Contains(CSiDataLine.NameSectionFrameCol));
        }

        public void GetISection(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref double tf,
            ref double tw,
            ref double t2Bottom,
            ref double tfBottom,
            ref int color,
            ref string notes,
            ref string GUID)
        {
          
        }
        
        
        public void GetCoverPlatedI(string name,
            ref string sectionName,
            ref double fyTopFlange,
            ref double fyWeb,
            ref double fyBottomFlange,
            ref double tc,
            ref double bc,
            ref string nameMaterialTop,
            ref double tcBottom,
            ref double bcBottom,
            ref string nameMaterialBottom,
            ref int color,
            ref string notes,
            ref string GUID)
        {
          
        }



        public void GetCircle(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref int color,
            ref string notes,
            ref string GUID)
        {
          
        }

        
        public void GetRectangle(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref int color,
            ref string notes,
            ref string GUID)
        {
          
        }

        
        public void GetPipe(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double tw,
            ref int color,
            ref string notes,
            ref string GUID)
        {
          
        }

        
        public void GetTube(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref double tf,
            ref double tw,
            ref int color,
            ref string notes,
            ref string GUID)
        {
          
        }

        
        public void GetTee(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref double tf,
            ref double tw,
            ref int color,
            ref string notes,
            ref string GUID)
        {
          
        }

        
        public void GetAngle(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref double tf,
            ref double tw,
            ref int color,
            ref string notes,
            ref string GUID)
        {
          
        }
        


        public void GetRebarBeam(string name,
            ref string materialNameLongitudinal,
            ref string materialNameConfinement,
            ref double coverTop,
            ref double coverBottom,
            ref double topLeftArea,
            ref double topRightArea,
            ref double bottomLeftArea,
            ref double bottomRightArea)
        {
          
        }

        
        public void GetRebarColumn(string name,
            ref string materialPropertyLongitudinal,
            ref string materialNameConfinement,
            ref int rebarConfiguration,
            ref int confinementType,
            ref double cover,
            ref int numberOfCircularBars,
            ref int numberOfRectangularBars3Axis,
            ref int numberOfRectangularBars2Axis,
            ref string rebarSize,
            ref string tieSize,
            ref double tieSpacingLongitudinal,
            ref int numberOfConfinementBars2Axis,
            ref int numberOfConfinementBars3Axis,
            ref bool toBeDesigned)
        {
          
        }
        
   }
    
    [TestFixture]
    public class FrameSection_Set : CsiSet
    {
#if !BUILD_ETABS2015

        
        public void SetSectionDesignerSection(string name,
            string nameMaterial,
            eSectionDesignerDesignOption designType,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }
#endif
#if BUILD_ETABS2016 || BUILD_ETABS2017

        
        public void SetSteelTee(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            double r,
            bool mirrorAbout3,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }

        
        public void SetSteelAngle(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            double r,
            bool mirrorAbout2,
            bool mirrorAbout3,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }


        
        public void SetConcreteTee(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double twF,
            double tfT,
            bool mirrorAbout3,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }

        
        public void SetConcreteL(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double twC,
            double tfT,
            bool mirrorAbout2,
            bool mirrorAbout3,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }
#endif
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        
        
        public void SetAutoSelectAluminum(string name,
            string[] sectionNames,
            string autoStartSection,
            string notes = "",
            string GUID = "")
        {
          
        }

        
        public void SetAutoSelectColdFormed(string name,
            string[] sectionNames,
            string autoStartSection,
            string notes = "",
            string GUID = "")
        {
          
        }

        
        public void SetHybridISection(string name,
            string nameMaterialTopFlange,
            string nameMaterialWeb,
            string nameMaterialBottomFlange,
            double t3,
            double t2,
            double tf,
            double tw,
            double t2Bottom,
            double tfBottom,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }
        
        
        public void SetHybridUSection(string name,
            string nameMaterialTopFlange,
            string nameMaterialWeb,
            string nameMaterialBottomFlange,
            double D1,
            double B1,
            double B2,
            double B3,
            double B4,
            double tw,
            double tf,
            double tfb,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }

        
        public void SetColdC(string name,
            string nameMaterial,
            double t3,
            double t2,
            double thickness,
            double radius,
            double lipDepth,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }

        
        public void SetColdHat(string name,
            string nameMaterial,
            double t3,
            double t2,
            double thickness,
            double radius,
            double lipDepth,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }

        
        public void SetColdZ(string name,
            string nameMaterial,
            double t3,
            double t2,
            double thickness,
            double radius,
            double lipDepth,
            double lipAngle,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }

        
        public void SetPrecastI(string name,
            string nameMaterial,
            double B1,
            double B2,
            double B3,
            double B4,
            double D1,
            double D2,
            double D3,
            double D4,
            double D5,
            double D6,
            double D7,
            double T1,
            double T2,
            double C1,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }

        
        
        public void SetPrecastU(string name,
            string nameMaterial,
            double B1,
            double B2,
            double B3,
            double B4,
            double B5,
            double B6,
            double D1,
            double D2,
            double D3,
            double D4,
            double D5,
            double D6,
            double D7,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }
#endif
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017


        
        public void SetPlate(string name,
            string nameMaterial,
            double t3,
            double t2,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }
        
        
        public void SetRod(string name,
            string nameMaterial,
            double t3,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }
#endif

      
        public void ChangeName(string currentName, 
            string newName)
        {
          
        }

        
        public void Delete(string name)
        {
          
        }

        public void ImportSectionProperty(string name,
            string nameMaterial,
            string fileName,
            string sectionName,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }

        
        public void SetModifiers(string name,
            AreaModifier modifiers)
        {
          
        }

        
        public void SetGeneral(string name,
            string nameMaterial,
            double t3,
            double t2,
            double Ag,
            double As2,
            double As3,
            double J,
            double I22,
            double I33,
            double S22,
            double S33,
            double Z22,
            double Z33,
            double r22,
            double r33,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }

        
        public void SetNonPrismatic(string name,
            string[] startSections,
            string[] endSections,
            double[] lengths,
            ePrismaticType[] prismaticTypes,
            ePrismaticInertiaType[] EI33,
            ePrismaticInertiaType[] EI22,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }

        
        public void SetAutoSelectSteel(string name,
            string[] sectionNames,
            string autoStartSection,
            string notes = "",
            string GUID = "")
        {
          
        }

        
        public void SetChannel(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }

        
        public void SetDoubleAngle(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            double separation,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }
        
        
        public void SetDoubleChannel(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            double separation,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }

        
        public void SetISection(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            ref double t2Bottom,
            ref double tfBottom,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }

        
        public void SetCoverPlatedI(string name,
            string sectionName,
            double fyTopFlange,
            double fyWeb,
            double fyBottomFlange,
            double tc,
            double bc,
            string nameMaterialTop,
            double tcBottom,
            double bcBottom,
            string nameMaterialBottom,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }
        
        
        public void SetCircle(string name,
            string nameMaterial,
            double t3,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }

        
        public void SetRectangle(string name,
            string nameMaterial,
            double t3,
            double t2,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }

        
        public void SetPipe(string name,
            string nameMaterial,
            double t3,
            double tw,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }

        
        public void SetTube(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }

        
        public void SetTee(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }
        
        
        public void SetAngle(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }

        
        public void SetRebarBeam(string name,
            string materialNameLongitudinal,
            string materialNameConfinement,
            double coverTop,
            double coverBottom,
            double topLeftArea,
            double topRightArea,
            double bottomLeftArea,
            double bottomRightArea)
        {
          
        }

        
        
        public void SetRebarColumn(string name,
            string materialPropertyLongitudinal,
            string materialNameConfinement,
            eRebarConfiguration rebarConfiguration,
            eConfinementType confinementType,
            double cover,
            int numberOfCircularBars,
            int numberOfRectangularBars3Axis,
            int numberOfRectangularBars2Axis,
            string rebarSize,
            string tieSize,
            double tieSpacingLongitudinal,
            int numberOfConfinementBars2Axis,
            int numberOfConfinementBars3Axis,
            bool toBeDesigned)
        {
          
        }
    }
}
