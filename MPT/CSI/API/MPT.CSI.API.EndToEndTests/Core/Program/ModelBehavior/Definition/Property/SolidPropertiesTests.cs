#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Support;
using System.Linq;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Property
{
   [TestFixture]
    public class SolidProperties_Get : CsiGet
    {
        [Test]
        public void Count()
        {
            int numberOfObjects = _app.Model.Definitions.Properties.SolidProperties.Count();
            Assert.That(numberOfObjects, Is.EqualTo(CSiDataSolid.NumberOfSectionsExpected));
        }

        [Test]
        public void GetNameList()
        {
            string[] names;
            _app.Model.Definitions.Properties.SolidProperties.GetNameList(out names);

            Assert.That(names.Length, Is.EqualTo(CSiDataSolid.NumberOfSectionsExpected));
            Assert.That(names.Contains(CSiDataSolid.NameSection));
        }

        [Test]
        public void GetProperty()
        {
            string nameMaterial;
            AngleLocalAxes materialAngle;
            bool includeIncompatibleBendingModes;
            int color;
            string notes;
            string GUID;

            _app.Model.Definitions.Properties.SolidProperties.GetProperty(CSiDataSolid.NameSection,
                out nameMaterial,
                out materialAngle,
                out includeIncompatibleBendingModes,
                out color,
                out notes,
                out GUID);

            string oldNotes = CSiDataSolid.NameSection;
            Assert.That(nameMaterial, Is.EqualTo(CSiDataMaterial.NameConcrete));
            Assert.That(materialAngle.AngleA, Is.EqualTo(0));
            Assert.That(materialAngle.AngleB, Is.EqualTo(0));
            Assert.That(materialAngle.AngleC, Is.EqualTo(0));
            Assert.That(includeIncompatibleBendingModes, Is.EqualTo(true));
            Assert.That(color, Is.Not.EqualTo(-1));
            Assert.That(notes, Is.EqualTo(oldNotes));
            Assert.That(GUID, Is.EqualTo(CSiData.OldGUID));
        }

        [Test]
        public void GetProperty_NonDefault()
        {
            string nameMaterial;
            AngleLocalAxes materialAngle;
            bool includeIncompatibleBendingModes;
            int color;
            string notes;
            string GUID;

            _app.Model.Definitions.Properties.SolidProperties.GetProperty(CSiDataSolid.NameSectionNonDefault,
                out nameMaterial,
                out materialAngle,
                out includeIncompatibleBendingModes,
                out color,
                out notes,
                out GUID);

            string oldNotes = CSiDataSolid.NameSectionNonDefault;
            Assert.That(nameMaterial, Is.EqualTo(CSiDataMaterial.NameSteel));
            Assert.That(materialAngle.AngleA, Is.EqualTo(CSiDataSolid.OldMaterialAngle.AngleA));
            Assert.That(materialAngle.AngleB, Is.EqualTo(CSiDataSolid.OldMaterialAngle.AngleB));
            Assert.That(materialAngle.AngleC, Is.EqualTo(CSiDataSolid.OldMaterialAngle.AngleC));
            Assert.That(includeIncompatibleBendingModes, Is.EqualTo(false));
            Assert.That(color, Is.Not.EqualTo(-1));
            Assert.That(notes, Is.EqualTo(oldNotes));
            Assert.That(GUID, Is.EqualTo(CSiData.OldGUID));
        }
    }
    
    [TestFixture]
    public class SolidProperties_Set : CsiSet
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
            AngleLocalAxes materialAngle,
            bool includeIncompatibleBendingModes,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }
    }
}
#endif