#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Support;
using System.Linq;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.NamedAssign
{
    [TestFixture]
    public class FrameReleases_Get : CsiGet
    {

        [Test]
        public void Count()
        {
            int numberOfObjects = _app.Model.Definitions.NamedAssigns.FrameReleases.Count();
            Assert.That(numberOfObjects, Is.EqualTo(CSiDataLine.NumberOfNamedAssignFrameReleaseExpected));
        }

        [Test]
        public void GetNameList()
        {
            string[] names;
            _app.Model.Definitions.NamedAssigns.FrameReleases.GetNameList(out names);

            Assert.That(names.Length, Is.EqualTo(CSiDataLine.NumberOfNamedAssignFrameReleaseExpected));
            Assert.That(names.Contains(CSiDataLine.NameNamedAssignRelease));
        }

        [Test]
        public void GetReleases()
        {
            DegreesOfFreedomLocal iEndRelease;
            DegreesOfFreedomLocal jEndRelease;
            Fixity iEndFixity;
            Fixity jEndFixity;

            _app.Model.Definitions.NamedAssigns.FrameReleases.GetReleases(CSiDataLine.NameNamedAssignRelease, 
                out iEndRelease,
                out jEndRelease,
                out iEndFixity,
                out jEndFixity);

            Assert.That(iEndRelease.U1, Is.EqualTo(CSiDataLine.OldIEndReleaseNamedAssign.U1));
            Assert.That(iEndRelease.U2, Is.EqualTo(CSiDataLine.OldIEndReleaseNamedAssign.U2));
            Assert.That(iEndRelease.U3, Is.EqualTo(CSiDataLine.OldIEndReleaseNamedAssign.U3));
            Assert.That(iEndRelease.R1, Is.EqualTo(CSiDataLine.OldIEndReleaseNamedAssign.R1));
            Assert.That(iEndRelease.R2, Is.EqualTo(CSiDataLine.OldIEndReleaseNamedAssign.R2));
            Assert.That(iEndRelease.R3, Is.EqualTo(CSiDataLine.OldIEndReleaseNamedAssign.R3));

            Assert.That(jEndRelease.U1, Is.EqualTo(CSiDataLine.OldJEndReleaseNamedAssign.U1));
            Assert.That(jEndRelease.U2, Is.EqualTo(CSiDataLine.OldJEndReleaseNamedAssign.U2));
            Assert.That(jEndRelease.U3, Is.EqualTo(CSiDataLine.OldJEndReleaseNamedAssign.U3));
            Assert.That(jEndRelease.R1, Is.EqualTo(CSiDataLine.OldJEndReleaseNamedAssign.R1));
            Assert.That(jEndRelease.R2, Is.EqualTo(CSiDataLine.OldJEndReleaseNamedAssign.R2));
            Assert.That(jEndRelease.R3, Is.EqualTo(CSiDataLine.OldJEndReleaseNamedAssign.R3));

            Assert.That(iEndFixity.U1, Is.EqualTo(CSiDataLine.OldIEndFixityNamedAssign.U1));
            Assert.That(iEndFixity.U2, Is.EqualTo(CSiDataLine.OldIEndFixityNamedAssign.U2));
            Assert.That(iEndFixity.U3, Is.EqualTo(CSiDataLine.OldIEndFixityNamedAssign.U3));
            Assert.That(iEndFixity.R1, Is.EqualTo(CSiDataLine.OldIEndFixityNamedAssign.R1));
            Assert.That(iEndFixity.R2, Is.EqualTo(CSiDataLine.OldIEndFixityNamedAssign.R2));
            Assert.That(iEndFixity.R3, Is.EqualTo(CSiDataLine.OldIEndFixityNamedAssign.R3));

            Assert.That(jEndFixity.U1, Is.EqualTo(CSiDataLine.OldJEndFixityNamedAssign.U1));
            Assert.That(jEndFixity.U2, Is.EqualTo(CSiDataLine.OldJEndFixityNamedAssign.U2));
            Assert.That(jEndFixity.U3, Is.EqualTo(CSiDataLine.OldJEndFixityNamedAssign.U3));
            Assert.That(jEndFixity.R1, Is.EqualTo(CSiDataLine.OldJEndFixityNamedAssign.R1));
            Assert.That(jEndFixity.R2, Is.EqualTo(CSiDataLine.OldJEndFixityNamedAssign.R2));
            Assert.That(jEndFixity.R3, Is.EqualTo(CSiDataLine.OldJEndFixityNamedAssign.R3));
        }
    }
    
    [TestFixture]
    public class FrameReleases_Set : CsiSet
    {
      
        public void ChangeName(string currentName, 
            string newName)
        {
          
        }

        
        public void Delete(string name)
        {
          
        }

        
        public void SetReleases(string name,
            DegreesOfFreedomLocal iEndRelease,
            DegreesOfFreedomLocal jEndRelease,
            Fixity iEndFixity,
            Fixity jEndFixity)
        {
          
        }
    }
}
#endif
