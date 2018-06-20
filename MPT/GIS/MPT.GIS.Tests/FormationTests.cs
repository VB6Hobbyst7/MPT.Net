using System;
using NUnit.Framework;

namespace MPT.GIS.Tests
{
    [TestFixture]
    public class FormationTests
    {
        public const string NAME_FORMATION = "FooFormation";
        public const string NAME_OTHER_FORMATION = "OtherFormation";
        public const string NAME_SUBFORMATION = "BarFormation";

        public const int ALTITUDE = 1234;
        public const double LATITUDE_VALID = 45.1233;
        public const double LONGITUDE_VALID = 105.3213;

        public const double LATITUDE_INVALID = 95.1236;     // > |90|
        public const double LONGITUDE_INVALID = 200.8563;   // > |180|

        [TestCase(LATITUDE_VALID, LONGITUDE_VALID)]
        [TestCase(-LATITUDE_VALID, LONGITUDE_VALID)]
        [TestCase(LATITUDE_VALID, -LONGITUDE_VALID)]
        [TestCase(-LATITUDE_VALID, -LONGITUDE_VALID)]
        public void Formation_Initialize_Partial_Initializes_Partial(double latitude, double longitude)
        {
            Formation formation = new Formation(NAME_FORMATION, latitude, longitude);

            Assert.That(formation.Name, Is.EqualTo(NAME_FORMATION));
            Assert.That(formation.Latitude, Is.EqualTo(latitude));
            Assert.That(formation.Longitude, Is.EqualTo(longitude));
            Assert.That(formation.OtherName, Is.EqualTo(string.Empty));
            Assert.That(formation.Elevation, Is.EqualTo(0));
            Assert.That(formation.SubFormationName, Is.EqualTo(string.Empty));
        }

        [TestCase(LATITUDE_VALID, LONGITUDE_VALID)]
        [TestCase(-LATITUDE_VALID, LONGITUDE_VALID)]
        [TestCase(LATITUDE_VALID, -LONGITUDE_VALID)]
        [TestCase(-LATITUDE_VALID, -LONGITUDE_VALID)]
        [TestCase(LATITUDE_VALID, LONGITUDE_VALID, ALTITUDE)]
        [TestCase(LATITUDE_VALID, LONGITUDE_VALID, -ALTITUDE)]
        [TestCase(LATITUDE_VALID, LONGITUDE_VALID, 0)]
        [TestCase(LATITUDE_VALID, LONGITUDE_VALID, null)]
        public void Formation_Initialize_All_Initializes_All(double latitude, double longitude, int altitude)
        {
            Formation formation = new Formation(NAME_FORMATION, latitude, longitude, NAME_OTHER_FORMATION, altitude, NAME_SUBFORMATION);

            Assert.That(formation.Name, Is.EqualTo(NAME_FORMATION));
            Assert.That(formation.Latitude, Is.EqualTo(latitude));
            Assert.That(formation.Longitude, Is.EqualTo(longitude));
            Assert.That(formation.OtherName, Is.EqualTo(NAME_OTHER_FORMATION));
            Assert.That(formation.Elevation, Is.EqualTo(altitude));
            Assert.That(formation.SubFormationName, Is.EqualTo(NAME_SUBFORMATION));
        }

        [TestCase(LATITUDE_INVALID, LONGITUDE_INVALID)]
        [TestCase(-LATITUDE_INVALID, LONGITUDE_INVALID)]
        [TestCase(LATITUDE_INVALID, -LONGITUDE_INVALID)]
        [TestCase(-LATITUDE_INVALID, -LONGITUDE_INVALID)]
        public void Formation_Initialize_Partial_Invalid_Initializes_Partial_Limited(double latitude, double longitude)
        {
            int signLatitude = Math.Sign(latitude);
            int signLongitude = Math.Sign(longitude);
            Formation formation = new Formation(NAME_FORMATION, latitude, longitude);

            Assert.That(formation.Name, Is.EqualTo(NAME_FORMATION));
            Assert.That(formation.Latitude, Is.EqualTo(signLatitude * 90));
            Assert.That(formation.Longitude, Is.EqualTo(signLongitude * 180));
            Assert.That(formation.OtherName, Is.EqualTo(string.Empty));
            Assert.That(formation.Elevation, Is.EqualTo(0));
            Assert.That(formation.SubFormationName, Is.EqualTo(string.Empty));
        }

        [TestCase(LATITUDE_VALID, LONGITUDE_VALID)]
        [TestCase(-LATITUDE_VALID, LONGITUDE_VALID)]
        [TestCase(LATITUDE_VALID, -LONGITUDE_VALID)]
        [TestCase(-LATITUDE_VALID, -LONGITUDE_VALID)]
        public void Formation_Initialize_With_Coordinate_Partial_Initializes_Partial(double latitude, double longitude)
        {
            Coordinate coordinate = new Coordinate(latitude, longitude);
            Formation formation = new Formation(NAME_FORMATION, coordinate);

            Assert.That(formation.Name, Is.EqualTo(NAME_FORMATION));
            Assert.That(formation.Latitude, Is.EqualTo(coordinate.Latitude));
            Assert.That(formation.Longitude, Is.EqualTo(coordinate.Longitude));
            Assert.That(formation.OtherName, Is.EqualTo(string.Empty));
            Assert.That(formation.Elevation, Is.EqualTo(0));
            Assert.That(formation.SubFormationName, Is.EqualTo(string.Empty));
        }

        [TestCase(LATITUDE_VALID, LONGITUDE_VALID)]
        [TestCase(-LATITUDE_VALID, LONGITUDE_VALID)]
        [TestCase(LATITUDE_VALID, -LONGITUDE_VALID)]
        [TestCase(-LATITUDE_VALID, -LONGITUDE_VALID)]
        [TestCase(LATITUDE_VALID, LONGITUDE_VALID, ALTITUDE)]
        [TestCase(LATITUDE_VALID, LONGITUDE_VALID, -ALTITUDE)]
        [TestCase(LATITUDE_VALID, LONGITUDE_VALID, 0)]
        [TestCase(LATITUDE_VALID, LONGITUDE_VALID, null)]
        public void Formation_Initialize_With_Coordinate_All_Initializes_All(double latitude, double longitude, int altitude)
        {
            Coordinate coordinate = new Coordinate(latitude, longitude, altitude);
            Formation formation = new Formation(NAME_FORMATION, coordinate, NAME_OTHER_FORMATION, NAME_SUBFORMATION);

            Assert.That(formation.Name, Is.EqualTo(NAME_FORMATION));
            Assert.That(formation.Latitude, Is.EqualTo(coordinate.Latitude));
            Assert.That(formation.Longitude, Is.EqualTo(coordinate.Longitude));
            Assert.That(formation.OtherName, Is.EqualTo(NAME_OTHER_FORMATION));
            Assert.That(formation.Elevation, Is.EqualTo(coordinate.Altitude));
            Assert.That(formation.SubFormationName, Is.EqualTo(NAME_SUBFORMATION));
        }

        [TestCase(LATITUDE_INVALID, LONGITUDE_INVALID)]
        [TestCase(-LATITUDE_INVALID, LONGITUDE_INVALID)]
        [TestCase(LATITUDE_INVALID, -LONGITUDE_INVALID)]
        [TestCase(-LATITUDE_INVALID, -LONGITUDE_INVALID)]
        public void Formation_Initialize_With_Coordinate_Partial_Invalid_Initializes_Partial_Limited(double latitude, double longitude)
        {
            int signLatitude = Math.Sign(latitude);
            int signLongitude = Math.Sign(longitude);
            Coordinate coordinate = new Coordinate(latitude, longitude);
            Formation formation = new Formation(NAME_FORMATION, coordinate);

            Assert.That(formation.Name, Is.EqualTo(NAME_FORMATION));
            Assert.That(formation.Latitude, Is.EqualTo(signLatitude * 90));
            Assert.That(formation.Longitude, Is.EqualTo(signLongitude * 180));
            Assert.That(formation.OtherName, Is.EqualTo(string.Empty));
            Assert.That(formation.Elevation, Is.EqualTo(0));
            Assert.That(formation.SubFormationName, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Formation_Initialize_With_Coordinate_NULL_Initializes_Empty()
        {
            Formation formation = new Formation(NAME_FORMATION, null);

            Assert.That(formation.Name, Is.EqualTo(NAME_FORMATION));
            Assert.That(formation.Latitude, Is.EqualTo(0));
            Assert.That(formation.Longitude, Is.EqualTo(0));
            Assert.That(formation.OtherName, Is.EqualTo(string.Empty));
            Assert.That(formation.Elevation, Is.EqualTo(0));
            Assert.That(formation.SubFormationName, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Override_ToString_Empty_Returns_Default_ToString()
        {
            Formation formation = new Formation(string.Empty, 0, 0);
            Assert.That(formation.ToString(), Is.EqualTo("MPT.GIS.Formation"));
        }

        [Test]
        public void Override_ToString_With_Name()
        {
            Formation formation = new Formation(NAME_FORMATION, 0, 0);
            Assert.That(formation.ToString(), Is.EqualTo(NAME_FORMATION));
        }

        [Test]
        public void Override_ToString_With_Name_And_OtherName()
        {
            Formation formation = new Formation(NAME_FORMATION, 0, 0, NAME_OTHER_FORMATION);
            Assert.That(formation.ToString(), Is.EqualTo($"{NAME_FORMATION} ({NAME_OTHER_FORMATION})"));
        }

        [Test]
        public void Override_ToString_With_Name_And_SubFormationName()
        {
            Formation formation = new Formation(NAME_FORMATION, 0, 0, subformationName: NAME_SUBFORMATION);
            Assert.That(formation.ToString(), Is.EqualTo($"{NAME_SUBFORMATION} (of {NAME_FORMATION})"));
        }

        [Test]
        public void Override_ToString_With_Name_SubFormationName_And_OtherName()
        {
            Formation formation = new Formation(NAME_FORMATION, 0, 0, NAME_OTHER_FORMATION, 0, NAME_SUBFORMATION);
            Assert.That(formation.ToString(), Is.EqualTo($"{NAME_SUBFORMATION} (of {NAME_FORMATION})({NAME_OTHER_FORMATION})"));
        }
    }
}
