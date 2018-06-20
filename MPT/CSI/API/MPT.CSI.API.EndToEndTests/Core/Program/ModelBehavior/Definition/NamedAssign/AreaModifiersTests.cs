#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Support;
using System.Linq;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.NamedAssign
{
    [TestFixture]
    public class AreaModifiers_Get : CsiGet
    {

        [Test]
        public void Count()
        {
            int numberOfObjects = _app.Model.Definitions.NamedAssigns.AreaModifiers.Count();
            Assert.That(numberOfObjects, Is.EqualTo(CSiDataArea.NumberOfNamedAssignExpected));
        }

        [Test]
        public void GetNameList()
        {
            string[] names;
            _app.Model.Definitions.NamedAssigns.AreaModifiers.GetNameList(out names);

            Assert.That(names.Length, Is.EqualTo(CSiDataArea.NumberOfNamedAssignExpected));
            Assert.That(names.Contains(CSiDataArea.NameNamedAssign));
        }

        [Test]
        public void GetModifiers()
        {
            AreaModifier oldModifiers = new AreaModifier
            {
                MembraneF11 = 2.1,
                MembraneF22 = 2.2,
                MembraneF12 = 2.3,
                BendingM11 = 2.4,
                BendingM22 = 2.5,
                BendingM12 = 2.6,
                ShearV13 = 2.7,
                ShearV23 = 2.8,
                MassModifier = 2.9,
                WeightModifier = 3.0
            };

            AreaModifier modifier;
            _app.Model.Definitions.NamedAssigns.AreaModifiers.GetModifiers(CSiDataArea.NameNamedAssign, out modifier);

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
    }
    
    [TestFixture]
    public class AreaModifiers_Set : CsiSet
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
    }
}

#endif
