using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.API.Core.Support;
using System.Linq;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Property
{
    [TestFixture]
    public class TendonSection_Get : CsiGet
    {

        [Test]
        public void Count()
        {
            int numberOfObjects = _app.Model.Definitions.Properties.TendonSection.Count();
            Assert.That(numberOfObjects, Is.EqualTo(CSiDataLine.NumberOfSectionTendonsExpected));
        }

        [Test]
        public void GetNameList()
        {
            string[] names;
            _app.Model.Definitions.Properties.TendonSection.GetNameList(out names);

            Assert.That(names.Length, Is.EqualTo(CSiDataLine.NumberOfSectionTendonsExpected));
            Assert.That(names.Contains(CSiDataLine.NameSectionTendonAsLoads));
            Assert.That(names.Contains(CSiDataLine.NameSectionTendonAsElements));
        }

        [Test]
        public void GetProperty_Modeled_As_Loads()
        {
            string nameMaterial;
            eTendonModelingOption modelingOption;
            double area;
            int color;
            string notes;
            string GUID;

            _app.Model.Definitions.Properties.TendonSection.GetProperty(CSiDataLine.NameSectionTendonAsLoads,
                out nameMaterial,
                out modelingOption,
                out area,
                out color,
                out notes,
                out GUID);

            string oldNotes = CSiDataLine.NameSectionTendonAsLoads;
            Assert.That(nameMaterial, Is.EqualTo(CSiDataMaterial.NameTendon));
            Assert.That(modelingOption, Is.EqualTo(eTendonModelingOption.Loads));
            Assert.That(area, Is.EqualTo(1));
            Assert.That(color, Is.Not.EqualTo(-1));
            Assert.That(notes, Is.EqualTo(oldNotes));
            Assert.That(GUID, Is.EqualTo(CSiData.OldGUID));
        }

        [Test]
        public void GetProperty_Modeled_As_Elements()
        {
            string nameMaterial;
            eTendonModelingOption modelingOption;
            double area;
            int color;
            string notes;
            string GUID;

            _app.Model.Definitions.Properties.TendonSection.GetProperty(CSiDataLine.NameSectionTendonAsElements,
                out nameMaterial,
                out modelingOption,
                out area,
                out color,
                out notes,
                out GUID);

            string oldNotes = CSiDataLine.NameSectionTendonAsElements;
            Assert.That(nameMaterial, Is.EqualTo(CSiDataMaterial.NameTendon));
            Assert.That(modelingOption, Is.EqualTo(eTendonModelingOption.Elements));
            Assert.That(area, Is.EqualTo(1));
            Assert.That(color, Is.Not.EqualTo(-1));
            Assert.That(notes, Is.EqualTo(oldNotes));
            Assert.That(GUID, Is.EqualTo(CSiData.OldGUID));
        }
    }
    
    [TestFixture]
    public class TendonSection_Set : CsiSet
    {
      
        public void ChangeName(string currentName, 
            string newName)
        {
          
        }

        
        public void Delete(string name)
        {
          
        }

        
        public void SetProperty(string name,
            string nameMaterial,
            eTendonModelingOption modelingOption,
            double area,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }
    }
}
