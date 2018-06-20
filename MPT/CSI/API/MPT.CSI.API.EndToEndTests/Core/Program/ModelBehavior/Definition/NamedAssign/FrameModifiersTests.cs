#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using System.Linq;
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.NamedAssign
{
    [TestFixture]
    public class FrameModifiers_Get : CsiGet
    {
        [Test]
        public void Count()
        {
            int numberOfObjects = _app.Model.Definitions.NamedAssigns.FrameModifiers.Count();
            Assert.That(numberOfObjects, Is.EqualTo(CSiDataLine.NumberOfNamedAssignFrameExpected));
        }

        [Test]
        public void GetNameList()
        {
            string[] names;
            _app.Model.Definitions.NamedAssigns.FrameModifiers.GetNameList(out names);

            Assert.That(names.Length, Is.EqualTo(CSiDataLine.NumberOfNamedAssignFrameExpected));
            Assert.That(names.Contains(CSiDataLine.NameNamedAssignFrame));
        }

        [Test]
        public void GetModifiers()
        {
            FrameModifier oldModifiers = new FrameModifier
            {
                CrossSectionalArea = 3.1,
                ShearV2 = 3.2,
                ShearV3 = 3.3,
                Torsion = 3.4,
                BendingM2 = 3.5,
                BendingM3 = 3.6,
                MassModifier = 3.7,
                WeightModifier = 3.8,
            };

            FrameModifier modifier;
            _app.Model.Definitions.NamedAssigns.FrameModifiers.GetModifiers(CSiDataLine.NameNamedAssignFrame, out modifier);

            Assert.That(modifier.CrossSectionalArea, Is.EqualTo(oldModifiers.CrossSectionalArea));
            Assert.That(modifier.ShearV2, Is.EqualTo(oldModifiers.ShearV2));
            Assert.That(modifier.ShearV3, Is.EqualTo(oldModifiers.ShearV3));
            Assert.That(modifier.Torsion, Is.EqualTo(oldModifiers.Torsion));
            Assert.That(modifier.BendingM2, Is.EqualTo(oldModifiers.BendingM2));
            Assert.That(modifier.BendingM3, Is.EqualTo(oldModifiers.BendingM3));
            Assert.That(modifier.MassModifier, Is.EqualTo(oldModifiers.MassModifier));
            Assert.That(modifier.WeightModifier, Is.EqualTo(oldModifiers.WeightModifier));
        }
    }
    
     [TestFixture]
    public class FrameModifiers_Set : CsiSet
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