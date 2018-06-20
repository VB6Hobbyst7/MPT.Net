#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using System.Linq;
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Property
{
    [TestFixture]
    public class CableSection_Get : CsiGet
    {
        [Test]
        public void Count()
        {
            int numberOfObjects = _app.Model.Definitions.Properties.CableSection.Count();
            Assert.That(numberOfObjects, Is.EqualTo(CSiDataLine.NumberOfSectionCablesExpected));
        }

        [Test]
        public void GetNameList()
        {
            string[] names;
            _app.Model.Definitions.Properties.CableSection.GetNameList(out names);

            Assert.That(names.Length, Is.EqualTo(CSiDataLine.NumberOfSectionCablesExpected));
            Assert.That(names.Contains(CSiDataLine.NameSectionCable));
            Assert.That(names.Contains(CSiDataLine.NameSectionCableMultiSegment));
        }

        [Test]
        public void GetModifiers_Has_No_Modifiers()
        {
            CableModifier oldModifiers = new CableModifier
            {
                CrossSectionalArea = 1,
                MassModifier = 1,
                WeightModifier = 1,
            };

            CableModifier modifier;
            _app.Model.Definitions.Properties.CableSection.GetModifiers(CSiDataLine.NameSectionCableMultiSegment, out modifier);

            Assert.That(modifier.CrossSectionalArea, Is.EqualTo(oldModifiers.CrossSectionalArea));
            Assert.That(modifier.MassModifier, Is.EqualTo(oldModifiers.MassModifier));
            Assert.That(modifier.WeightModifier, Is.EqualTo(oldModifiers.WeightModifier));
        }

        [Test]
        public void GetModifiers_Has_Modifiers()
        {
            CableModifier oldModifiers = new CableModifier
            {
                CrossSectionalArea = 1.6,
                MassModifier = 1.7,
                WeightModifier = 1.8,
            };

            CableModifier modifier;
            _app.Model.Definitions.Properties.CableSection.GetModifiers(CSiDataLine.NameSectionCable, out modifier);

            Assert.That(modifier.CrossSectionalArea, Is.EqualTo(oldModifiers.CrossSectionalArea));
            Assert.That(modifier.MassModifier, Is.EqualTo(oldModifiers.MassModifier));
            Assert.That(modifier.WeightModifier, Is.EqualTo(oldModifiers.WeightModifier));
        }

        [Test]
        public void GetProperty()
        {
            string nameMaterial;
            double area;
            int color;
            string notes;
            string GUID;

            _app.Model.Definitions.Properties.CableSection.GetProperty(CSiDataLine.NameSectionCable,
                out nameMaterial,
                out area,
                out color,
                out notes,
                out GUID);

            string oldNotes = CSiDataLine.NameSectionCable;
            Assert.That(nameMaterial, Is.EqualTo(CSiDataMaterial.NameSteel));
            Assert.That(area, Is.EqualTo(CSiDataLine.OldCableArea));
            Assert.That(color, Is.Not.EqualTo(-1));
            Assert.That(notes, Is.EqualTo(oldNotes));
            Assert.That(GUID, Is.EqualTo(CSiData.OldGUID));
        }
    }
    
    [TestFixture]
    public class CableSection_Set : CsiSet
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
        
        
        public void SetProperty(string name,
            string nameMaterial,
            double area,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }
    }
}
#endif
