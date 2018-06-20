using System;
using NUnit.Framework;

namespace MPT.GIS.Tests
{
    [TestFixture]
    public class CoordinateTests
    {
        public const double ALTITUDE = 1234.567;
        public const double LATITUDE_VALID = 45.1233;
        public const double LONGITUDE_VALID = 105.3213;

        public const double LATITUDE_INVALID = 95.1236;     // > |90|
        public const double LONGITUDE_INVALID = 200.8563;   // > |180|


        [Test]
        public void Coordinate_Initialize_Empty_Initializes_Empty()
        {
            Coordinate coordinate = new Coordinate();

            Assert.That(coordinate.Altitude, Is.EqualTo(null));
            Assert.That(coordinate.Latitude, Is.EqualTo(0));
            Assert.That(coordinate.Longitude, Is.EqualTo(0));
        }

        [TestCase(LATITUDE_VALID, LONGITUDE_VALID)]
        [TestCase(-LATITUDE_VALID, LONGITUDE_VALID)]
        [TestCase(LATITUDE_VALID, -LONGITUDE_VALID)]
        [TestCase(-LATITUDE_VALID, -LONGITUDE_VALID)]
        public void Coordinate_Initialize_LatLong_Initializes_LatLong(double latitude, double longitude)
        {
            Coordinate coordinate = new Coordinate(latitude, longitude);

            Assert.That(coordinate.Altitude, Is.EqualTo(null));
            Assert.That(coordinate.Latitude, Is.EqualTo(latitude));
            Assert.That(coordinate.Longitude, Is.EqualTo(longitude));
        }

        [TestCase(LATITUDE_VALID, LONGITUDE_VALID, ALTITUDE)]
        [TestCase(LATITUDE_VALID, LONGITUDE_VALID, -ALTITUDE)]
        [TestCase(LATITUDE_VALID, LONGITUDE_VALID, 0)]
        [TestCase(LATITUDE_VALID, LONGITUDE_VALID, null)]
        public void Coordinate_Initialize_LatLongAltitude_Initializes_LatLongAltitude(double latitude, double longitude, double altitude)
        {
            Coordinate coordinate = new Coordinate(latitude, longitude, altitude);

            Assert.That(coordinate.Altitude, Is.EqualTo(altitude));
            Assert.That(coordinate.Latitude, Is.EqualTo(latitude));
            Assert.That(coordinate.Longitude, Is.EqualTo(longitude));
        }

        [TestCase(LATITUDE_INVALID, LONGITUDE_INVALID)]
        [TestCase(-LATITUDE_INVALID, LONGITUDE_INVALID)]
        [TestCase(LATITUDE_INVALID, -LONGITUDE_INVALID)]
        [TestCase(-LATITUDE_INVALID, -LONGITUDE_INVALID)]
        public void Coordinate_Initialize_LatLong_Invalid_Initializes_LatLong_Limited(double latitude, double longitude)
        {
            int signLatitude = Math.Sign(latitude);
            int signLongitude = Math.Sign(longitude);

            Coordinate coordinate = new Coordinate(latitude, longitude);

            Assert.That(coordinate.Altitude, Is.EqualTo(null));
            Assert.That(coordinate.Latitude, Is.EqualTo(signLatitude * 90));
            Assert.That(coordinate.Longitude, Is.EqualTo(signLongitude * 180));
        }

        [TestCase(LATITUDE_VALID, LONGITUDE_VALID, ALTITUDE)]
        [TestCase(LATITUDE_VALID, LONGITUDE_VALID, -ALTITUDE)]
        [TestCase(LATITUDE_VALID, LONGITUDE_VALID, 0)]
        [TestCase(LATITUDE_VALID, LONGITUDE_VALID, null)]
        public void Coordinate_Sets_LatLongAltitude_Sets_LatLongAltitude(double latitude, double longitude, double altitude)
        {
            Coordinate coordinate = new Coordinate
            {
                Altitude = altitude,
                Latitude = latitude,
                Longitude = longitude
            };

            Assert.That(coordinate.Altitude, Is.EqualTo(altitude));
            Assert.That(coordinate.Latitude, Is.EqualTo(latitude));
            Assert.That(coordinate.Longitude, Is.EqualTo(longitude));
        }

        [TestCase(LATITUDE_INVALID, LONGITUDE_INVALID, ALTITUDE)]
        [TestCase(-LATITUDE_INVALID, LONGITUDE_INVALID, ALTITUDE)]
        [TestCase(LATITUDE_INVALID, -LONGITUDE_INVALID, ALTITUDE)]
        [TestCase(-LATITUDE_INVALID, -LONGITUDE_INVALID, ALTITUDE)]
        public void Coordinate_Set_LatLong_Invalid_Sets_LatLong_Limited(double latitude, double longitude, double altitude)
        {
            int signLatitude = Math.Sign(latitude);
            int signLongitude = Math.Sign(longitude);

            Coordinate coordinate = new Coordinate
            {
                Altitude = altitude,
                Latitude = latitude,
                Longitude = longitude
            };

            Assert.That(coordinate.Altitude, Is.EqualTo(altitude));
            Assert.That(coordinate.Latitude, Is.EqualTo(signLatitude * 90));
            Assert.That(coordinate.Longitude, Is.EqualTo(signLongitude * 180));
        }

        #region Operator Overloads
        [Test]
        public void Equals_Coordinate()
        {
            Coordinate coordinate1 = new Coordinate(4, 5);
            Coordinate coordinate2 = new Coordinate(4, 5);
            Coordinate coordinate1a = new Coordinate(1, 5);
            Coordinate coordinate1b = new Coordinate(4, 1);

            Assert.That(coordinate1.Equals(coordinate2));
            Assert.IsFalse(coordinate1.Equals(coordinate1a));
            Assert.IsFalse(coordinate1.Equals(coordinate1b));

            Coordinate coordinate3 = new Coordinate(6, 7, 8);
            Coordinate coordinate4 = new Coordinate(6, 7, 8);
            Coordinate coordinate3a = new Coordinate(6, 7, 7);
            Coordinate coordinate3b = new Coordinate(5, 7, 8);

            Assert.That(coordinate3.Equals(coordinate4));
            Assert.IsFalse(coordinate3.Equals(coordinate3a));
            Assert.IsFalse(coordinate3.Equals(coordinate3b));
            Assert.IsFalse(coordinate1.Equals(coordinate3b));
        }

        private class NonMatchingObject
        {}

        [Test]
        public void Equals_Object()
        {

            Coordinate coordinate1 = new Coordinate(4, 5);
            object coordinate2 = new Coordinate(4, 5);
            object coordinate1a = new Coordinate(1, 5);
            object coordinate1b = new Coordinate(4, 1);

            Assert.That(coordinate1.Equals(coordinate2));
            Assert.IsFalse(coordinate1.Equals(coordinate1a));
            Assert.IsFalse(coordinate1.Equals(coordinate1b));

            Coordinate coordinate3 = new Coordinate(6, 7, 8);
            object coordinate4 = new Coordinate(6, 7, 8);
            object coordinate3a = new Coordinate(6, 7, 7);
            object coordinate3b = new Coordinate(5, 7, 8);

            Assert.That(coordinate3.Equals(coordinate4));
            Assert.IsFalse(coordinate3.Equals(coordinate3a));
            Assert.IsFalse(coordinate3.Equals(coordinate3b));
            Assert.IsFalse(coordinate1.Equals(coordinate3b));

            object coordinate5 = new NonMatchingObject();
            Assert.IsFalse(coordinate1.Equals(coordinate5));
        }

        [Test]
        public void Equals()
        {

            Coordinate coordinate1 = new Coordinate(4, 5);
            Coordinate coordinate2 = new Coordinate(4, 5);

            Assert.That(coordinate1 == coordinate2);

            Coordinate coordinate3 = new Coordinate(6, 7, 8);
            Coordinate coordinate4 = new Coordinate(6, 7, 8);

            Assert.That(coordinate3 == coordinate4);
        }

        [Test]
        public void Not_Equals()
        {
            Coordinate coordinate1 = new Coordinate(4, 5);
            Coordinate coordinate1a = new Coordinate(1, 5);
            Coordinate coordinate1b = new Coordinate(4, 1);

            Assert.That(coordinate1 != coordinate1a);
            Assert.That(coordinate1 != coordinate1b);

            Coordinate coordinate3 = new Coordinate(6, 7, 8);
            Coordinate coordinate3a = new Coordinate(6, 7, 7);
            Coordinate coordinate3b = new Coordinate(5, 7, 8);
            
            Assert.That(coordinate3 != coordinate3a);
            Assert.That(coordinate3 != coordinate3b);
            Assert.That(coordinate1 != coordinate3b);
        }

        [Test]
        public void GetHashCode()
        {
            Coordinate coordinate1 = new Coordinate(4, 5);
            Coordinate coordinate2 = new Coordinate(4, 5);
            Coordinate coordinate1a = new Coordinate(1, 5);
            Coordinate coordinate1b = new Coordinate(4, 1);

            Assert.That(coordinate1.GetHashCode(), Is.EqualTo(coordinate2.GetHashCode()));
            Assert.That(coordinate1.GetHashCode(), Is.Not.EqualTo(coordinate1a.GetHashCode()));
            Assert.That(coordinate1.GetHashCode(), Is.Not.EqualTo(coordinate1b.GetHashCode()));

            Coordinate coordinate3 = new Coordinate(4, 5, 6);
            Coordinate coordinate4 = new Coordinate(4, 5, 6);
            object coordinate3a = new Coordinate(6, 7, 7);
            object coordinate3b = new Coordinate(5, 7, 8);

            Assert.That(coordinate3.GetHashCode(), Is.EqualTo(coordinate4.GetHashCode()));
            Assert.That(coordinate3.GetHashCode(), Is.Not.EqualTo(coordinate3a.GetHashCode()));
            Assert.That(coordinate3.GetHashCode(), Is.Not.EqualTo(coordinate3b.GetHashCode()));
            Assert.That(coordinate1.GetHashCode(), Is.Not.EqualTo(coordinate4.GetHashCode()));
        }

        [Test]
        public void Plus_Coordinate_Plus_Offset()
        {

        }


        [Test]
        public void Minus_Coordinate_Minus_Coordinate()
        {

        }

        [Test]
        public void Minus_Coordinate_Minus_Offset()
        {

        }


        [Test]
        public void Times_Coordinate_Times_Scale()
        {

        }

        [Test]
        public void Times_Scale_Times_Coordinate()
        {
            Coordinate coordinate = new Coordinate(1, 2, 3);
        }

        [Test]
        public void Divide_Coordinate_Divided_By_Scale()
        {

        }

        [Test]
        public void Conversion_Implicit_Point_To_Coordinate()
        {

        }

        [Test]
        public void Conversion_Implicit_Point3D_To_Coordinate()
        {

        }

        [Test]
        public void Conversion_Explicit_Coordinate_To_Point()
        {

        }

        [Test]
        public void Conversion_Explicit_Coordinate_To_Point3D()
        {

        }
        #endregion

    }
}
