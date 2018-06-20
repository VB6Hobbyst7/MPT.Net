using System;
using NUnit.Framework;

namespace MPT.GIS.Tests
{
    [TestFixture]
    public class LocationTests
    {
        public const int ALTITUDE = 1234;
        public const double LATITUDE_VALID = 45.1233;
        public const double LONGITUDE_VALID = 105.3213;

        public const double LATITUDE_INVALID = 95.1236;     // > |90|
        public const double LONGITUDE_INVALID = 200.8563;   // > |180|

        [TestCase(LATITUDE_VALID, LONGITUDE_VALID)]
        [TestCase(-LATITUDE_VALID, LONGITUDE_VALID)]
        [TestCase(LATITUDE_VALID, -LONGITUDE_VALID)]
        [TestCase(-LATITUDE_VALID, -LONGITUDE_VALID)]
        public void Location_Initialize_Partial_Initializes_Partial(double latitude, double longitude)
        {
            string name = "Foo";
            Location location = new Location(name, latitude, longitude);

            Assert.That(location.Name, Is.EqualTo(name));
            Assert.That(location.Latitude, Is.EqualTo(latitude));
            Assert.That(location.Longitude, Is.EqualTo(longitude));
            Assert.That(location.OtherName, Is.EqualTo(string.Empty));
            Assert.That(location.Elevation, Is.EqualTo(0));
        }

        [TestCase(LATITUDE_VALID, LONGITUDE_VALID)]
        [TestCase(-LATITUDE_VALID, LONGITUDE_VALID)]
        [TestCase(LATITUDE_VALID, -LONGITUDE_VALID)]
        [TestCase(-LATITUDE_VALID, -LONGITUDE_VALID)]
        [TestCase(LATITUDE_VALID, LONGITUDE_VALID, ALTITUDE)]
        [TestCase(LATITUDE_VALID, LONGITUDE_VALID, -ALTITUDE)]
        [TestCase(LATITUDE_VALID, LONGITUDE_VALID, 0)]
        [TestCase(LATITUDE_VALID, LONGITUDE_VALID, null)]
        public void Location_Initialize_All_Initializes_All(double latitude, double longitude, int altitude)
        {
            string name = "Foo";
            string otherName = "OtherFoo";
            Location location = new Location(name, latitude, longitude, otherName, altitude);

            Assert.That(location.Name, Is.EqualTo(name));
            Assert.That(location.Latitude, Is.EqualTo(latitude));
            Assert.That(location.Longitude, Is.EqualTo(longitude));
            Assert.That(location.OtherName, Is.EqualTo(otherName));
            Assert.That(location.Elevation, Is.EqualTo(altitude));
        }

        [TestCase(LATITUDE_INVALID, LONGITUDE_INVALID)]
        [TestCase(-LATITUDE_INVALID, LONGITUDE_INVALID)]
        [TestCase(LATITUDE_INVALID, -LONGITUDE_INVALID)]
        [TestCase(-LATITUDE_INVALID, -LONGITUDE_INVALID)]
        public void Location_Initialize_Partial_Invalid_Initializes_Partial_Limited(double latitude, double longitude)
        {
            int signLatitude = Math.Sign(latitude);
            int signLongitude = Math.Sign(longitude);
            string name = "Foo";
            Location location = new Location(name, latitude, longitude);

            Assert.That(location.Name, Is.EqualTo(name));
            Assert.That(location.Latitude, Is.EqualTo(signLatitude * 90));
            Assert.That(location.Longitude, Is.EqualTo(signLongitude * 180));
            Assert.That(location.OtherName, Is.EqualTo(string.Empty));
            Assert.That(location.Elevation, Is.EqualTo(0));
        }

        [TestCase(LATITUDE_VALID, LONGITUDE_VALID)]
        [TestCase(-LATITUDE_VALID, LONGITUDE_VALID)]
        [TestCase(LATITUDE_VALID, -LONGITUDE_VALID)]
        [TestCase(-LATITUDE_VALID, -LONGITUDE_VALID)]
        public void Location_Initialize_With_Coordinate_Partial_Initializes_Partial(double latitude, double longitude)
        {
            string name = "Foo";
            Coordinate coordinate = new Coordinate(latitude, longitude);
            Location location = new Location(name, coordinate);

            Assert.That(location.Name, Is.EqualTo(name));
            Assert.That(location.Latitude, Is.EqualTo(coordinate.Latitude));
            Assert.That(location.Longitude, Is.EqualTo(coordinate.Longitude));
            Assert.That(location.OtherName, Is.EqualTo(string.Empty));
            Assert.That(location.Elevation, Is.EqualTo(0));
        }

        [TestCase(LATITUDE_VALID, LONGITUDE_VALID)]
        [TestCase(-LATITUDE_VALID, LONGITUDE_VALID)]
        [TestCase(LATITUDE_VALID, -LONGITUDE_VALID)]
        [TestCase(-LATITUDE_VALID, -LONGITUDE_VALID)]
        [TestCase(LATITUDE_VALID, LONGITUDE_VALID, ALTITUDE)]
        [TestCase(LATITUDE_VALID, LONGITUDE_VALID, -ALTITUDE)]
        [TestCase(LATITUDE_VALID, LONGITUDE_VALID, 0)]
        [TestCase(LATITUDE_VALID, LONGITUDE_VALID, null)]
        public void Location_Initialize_With_Coordinate_All_Initializes_All(double latitude, double longitude, int altitude)
        {
            string name = "Foo";
            string otherName = "OtherFoo";
            Coordinate coordinate = new Coordinate(latitude, longitude, altitude);
            Location location = new Location(name, coordinate, otherName);

            Assert.That(location.Name, Is.EqualTo(name));
            Assert.That(location.Latitude, Is.EqualTo(coordinate.Latitude));
            Assert.That(location.Longitude, Is.EqualTo(coordinate.Longitude));
            Assert.That(location.OtherName, Is.EqualTo(otherName));
            Assert.That(location.Elevation, Is.EqualTo(coordinate.Altitude));
        }

        [TestCase(LATITUDE_INVALID, LONGITUDE_INVALID)]
        [TestCase(-LATITUDE_INVALID, LONGITUDE_INVALID)]
        [TestCase(LATITUDE_INVALID, -LONGITUDE_INVALID)]
        [TestCase(-LATITUDE_INVALID, -LONGITUDE_INVALID)]
        public void Location_Initialize_With_Coordinate_Partial_Invalid_Initializes_Partial_Limited(double latitude, double longitude)
        {
            int signLatitude = Math.Sign(latitude);
            int signLongitude = Math.Sign(longitude);
            string name = "Foo";
            Coordinate coordinate = new Coordinate(latitude, longitude);
            Location location = new Location(name, coordinate);

            Assert.That(location.Name, Is.EqualTo(name));
            Assert.That(location.Latitude, Is.EqualTo(signLatitude * 90));
            Assert.That(location.Longitude, Is.EqualTo(signLongitude * 180));
            Assert.That(location.OtherName, Is.EqualTo(string.Empty));
            Assert.That(location.Elevation, Is.EqualTo(0));
        }

        [Test]
        public void Location_Initialize_With_Coordinate_NULL_Initializes_Empty()
        {
            string name = "Foo";
            Location location = new Location(name, null);

            Assert.That(location.Name, Is.EqualTo(name));
            Assert.That(location.Latitude, Is.EqualTo(0));
            Assert.That(location.Longitude, Is.EqualTo(0));
            Assert.That(location.OtherName, Is.EqualTo(string.Empty));
            Assert.That(location.Elevation, Is.EqualTo(0));
        }

        [Test]
        public void Override_ToString_Empty_Returns_Default_ToString()
        {
            Location location = new Location("", 0, 0);
            Assert.That(location.ToString(), Is.EqualTo("MPT.GIS.Location"));
        }

        [Test]
        public void Override_ToString_With_Name()
        {
            Location location = new Location("Foo", 0, 0);
            Assert.That(location.ToString(), Is.EqualTo("Foo"));
        }

        [Test]
        public void Override_ToString_With_Name_And_OtherName()
        {
            Location location = new Location("Foo", 0, 0, "OtherFoo");
            Assert.That(location.ToString(), Is.EqualTo("Foo (OtherFoo)"));
        }
    }
}
