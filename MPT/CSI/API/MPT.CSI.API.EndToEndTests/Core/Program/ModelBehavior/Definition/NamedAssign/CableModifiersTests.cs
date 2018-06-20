#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using System.Linq;
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.NamedAssign
{
    [TestFixture]
    public class CableModifiers_Get : CsiGet
    {

        [Test]
        public void Count()
        {
            int numberOfObjects = _app.Model.Definitions.NamedAssigns.CableModifiers.Count();
            Assert.That(numberOfObjects, Is.EqualTo(CSiDataLine.NumberOfNamedAssignCableExpected));
        }

        [Test]
        public void GetNameList()
        {
            string[] names;
            _app.Model.Definitions.NamedAssigns.CableModifiers.GetNameList(out names);

            Assert.That(names.Length, Is.EqualTo(CSiDataLine.NumberOfNamedAssignCableExpected));
            Assert.That(names.Contains(CSiDataLine.NameNamedAssignCable));
        }
        
        [Test]
        public void GetModifiers()
        {
            CableModifier oldModifiers = new CableModifier
            {
                CrossSectionalArea = 4.1,
                MassModifier = 4.7,
                WeightModifier = 4.8,
            };

            CableModifier modifier;
            _app.Model.Definitions.NamedAssigns.CableModifiers.GetModifiers(CSiDataLine.NameNamedAssignCable, out modifier);

            Assert.That(modifier.CrossSectionalArea, Is.EqualTo(oldModifiers.CrossSectionalArea));
            Assert.That(modifier.MassModifier, Is.EqualTo(oldModifiers.MassModifier));
            Assert.That(modifier.WeightModifier, Is.EqualTo(oldModifiers.WeightModifier));
        }
    }
    
    [TestFixture]
    public class CableModifiers_Set : CsiSet
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