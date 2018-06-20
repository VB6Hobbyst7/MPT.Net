using System.Collections.Generic;
using NUnit.Framework;

namespace MPT.GIS.Tests
{
    [TestFixture]
    public class ExtentsTests
    {
        /// <summary>
        /// A 5-point star that spans from pole-to-pole.
        /// </summary>
        public static List<Coordinate> StarRegion = new List<Coordinate>()
        {
            new Coordinate(89, 0),
            new Coordinate(24, 20),
            new Coordinate(24, 90),
            new Coordinate(-20, 40),
            new Coordinate(-90, 60),
            new Coordinate(-45, 0),
            new Coordinate(-90, -60),
            new Coordinate(-20, -40),
            new Coordinate(24, -90),
            new Coordinate(24, -20),
        };

        [Test]
        public void Extents_Initialize_With_Other_Extents()
        {
            Extents extents = new Extents(StarRegion);
            Extents newExtents = new Extents(extents);

            Assert.That(newExtents.MaxLatitude, Is.EqualTo(89));
            Assert.That(newExtents.MinLatitude, Is.EqualTo(-90));
            Assert.That(newExtents.MaxLongitude, Is.EqualTo(90));
            Assert.That(newExtents.MinLongitude, Is.EqualTo(-90));
        }

        [Test]
        public void Add_Beyond_Max_Limits_To_Max()
        {
            Extents extents = new Extents();
            Assert.That(extents.MaxLatitude, Is.EqualTo(-90));
            Assert.That(extents.MinLatitude, Is.EqualTo(90));
            Assert.That(extents.MaxLongitude, Is.EqualTo(-180));
            Assert.That(extents.MinLongitude, Is.EqualTo(180));

            extents.Add(new Coordinate(300, 10));
            Assert.That(extents.MaxLatitude, Is.EqualTo(90));
            Assert.That(extents.MinLatitude, Is.EqualTo(90));
            Assert.That(extents.MaxLongitude, Is.EqualTo(10));
            Assert.That(extents.MinLongitude, Is.EqualTo(10));

            extents.Add(new Coordinate(10, 300));
            Assert.That(extents.MaxLatitude, Is.EqualTo(90));
            Assert.That(extents.MinLatitude, Is.EqualTo(10));
            Assert.That(extents.MaxLongitude, Is.EqualTo(180));
            Assert.That(extents.MinLongitude, Is.EqualTo(10));

            extents.Add(new Coordinate(-10, -300));
            Assert.That(extents.MaxLatitude, Is.EqualTo(90));
            Assert.That(extents.MinLatitude, Is.EqualTo(-10));
            Assert.That(extents.MaxLongitude, Is.EqualTo(180));
            Assert.That(extents.MinLongitude, Is.EqualTo(-180));

            extents.Add(new Coordinate(-300, -10));
            Assert.That(extents.MaxLatitude, Is.EqualTo(90));
            Assert.That(extents.MinLatitude, Is.EqualTo(-90));
            Assert.That(extents.MaxLongitude, Is.EqualTo(180));
            Assert.That(extents.MinLongitude, Is.EqualTo(-180));
        }


        [Test]
        public void Add_Points()
        {
            Extents extents = new Extents();
            Assert.That(extents.MaxLatitude, Is.EqualTo(-90));
            Assert.That(extents.MinLatitude, Is.EqualTo(90));
            Assert.That(extents.MaxLongitude, Is.EqualTo(-180));
            Assert.That(extents.MinLongitude, Is.EqualTo(180));

            // Pt 1
            int point = 1;
            extents.Add(new Coordinate(StarRegion[point - 1].Latitude, StarRegion[point - 1].Longitude));
            Assert.That(extents.MaxLatitude, Is.EqualTo(89));
            Assert.That(extents.MinLatitude, Is.EqualTo(89));
            Assert.That(extents.MaxLongitude, Is.EqualTo(0));
            Assert.That(extents.MinLongitude, Is.EqualTo(0));

            // Pt 2
            point = 2;
            extents.Add(new Coordinate(StarRegion[point - 1].Latitude, StarRegion[point - 1].Longitude));
            Assert.That(extents.MaxLatitude, Is.EqualTo(89));
            Assert.That(extents.MinLatitude, Is.EqualTo(24));
            Assert.That(extents.MaxLongitude, Is.EqualTo(20));
            Assert.That(extents.MinLongitude, Is.EqualTo(0));

            // Pt 3
            point = 3;
            extents.Add(new Coordinate(StarRegion[point - 1].Latitude, StarRegion[point - 1].Longitude));
            Assert.That(extents.MaxLatitude, Is.EqualTo(89));
            Assert.That(extents.MinLatitude, Is.EqualTo(24));
            Assert.That(extents.MaxLongitude, Is.EqualTo(90));
            Assert.That(extents.MinLongitude, Is.EqualTo(0));

            // Pt 4
            point = 4;
            extents.Add(new Coordinate(StarRegion[point - 1].Latitude, StarRegion[point - 1].Longitude));
            Assert.That(extents.MaxLatitude, Is.EqualTo(89));
            Assert.That(extents.MinLatitude, Is.EqualTo(-20));
            Assert.That(extents.MaxLongitude, Is.EqualTo(90));
            Assert.That(extents.MinLongitude, Is.EqualTo(0));

            // Pt 5
            point = 5;
            extents.Add(new Coordinate(StarRegion[0].Latitude, StarRegion[0].Longitude));
            extents.Add(new Coordinate(StarRegion[point - 1].Latitude, StarRegion[point - 1].Longitude));
            Assert.That(extents.MaxLatitude, Is.EqualTo(89));
            Assert.That(extents.MinLatitude, Is.EqualTo(-90));
            Assert.That(extents.MaxLongitude, Is.EqualTo(90));
            Assert.That(extents.MinLongitude, Is.EqualTo(0));

            // Pt 6
            point = 6;
            extents.Add(new Coordinate(StarRegion[point - 1].Latitude, StarRegion[point - 1].Longitude));
            Assert.That(extents.MaxLatitude, Is.EqualTo(89));
            Assert.That(extents.MinLatitude, Is.EqualTo(-90));
            Assert.That(extents.MaxLongitude, Is.EqualTo(90));
            Assert.That(extents.MinLongitude, Is.EqualTo(0));

            // Pt 7
            point = 7;
            extents.Add(new Coordinate(StarRegion[point - 1].Latitude, StarRegion[point - 1].Longitude));
            Assert.That(extents.MaxLatitude, Is.EqualTo(89));
            Assert.That(extents.MinLatitude, Is.EqualTo(-90));
            Assert.That(extents.MaxLongitude, Is.EqualTo(90));
            Assert.That(extents.MinLongitude, Is.EqualTo(-60));

            // Pt 8
            point = 8;
            extents.Add(new Coordinate(StarRegion[point - 1].Latitude, StarRegion[point - 1].Longitude));
            Assert.That(extents.MaxLatitude, Is.EqualTo(89));
            Assert.That(extents.MinLatitude, Is.EqualTo(-90));
            Assert.That(extents.MaxLongitude, Is.EqualTo(90));
            Assert.That(extents.MinLongitude, Is.EqualTo(-60));

            // Pt 9
            point = 9;
            extents.Add(new Coordinate(StarRegion[point - 1].Latitude, StarRegion[point - 1].Longitude));
            Assert.That(extents.MaxLatitude, Is.EqualTo(89));
            Assert.That(extents.MinLatitude, Is.EqualTo(-90));
            Assert.That(extents.MaxLongitude, Is.EqualTo(90));
            Assert.That(extents.MinLongitude, Is.EqualTo(-90));

            // Pt 10
            point = 10;
            extents.Add(new Coordinate(StarRegion[point - 1].Latitude, StarRegion[point - 1].Longitude));
            Assert.That(extents.MaxLatitude, Is.EqualTo(89));
            Assert.That(extents.MinLatitude, Is.EqualTo(-90));
            Assert.That(extents.MaxLongitude, Is.EqualTo(90));
            Assert.That(extents.MinLongitude, Is.EqualTo(-90));
        }

        [Test]
        public void Add_IEnumerable()
        {
            Extents extents = new Extents();

            Assert.That(extents.MaxLatitude, Is.EqualTo(-90));
            Assert.That(extents.MinLatitude, Is.EqualTo(90));
            Assert.That(extents.MaxLongitude, Is.EqualTo(-180));
            Assert.That(extents.MinLongitude, Is.EqualTo(180));
            
            extents.Add(StarRegion);

            Assert.That(extents.MaxLatitude, Is.EqualTo(89));
            Assert.That(extents.MinLatitude, Is.EqualTo(-90));
            Assert.That(extents.MaxLongitude, Is.EqualTo(90));
            Assert.That(extents.MinLongitude, Is.EqualTo(-90));
        }

        [Test]
        public void Add_Extents()
        {
            Extents extents = new Extents();
            Assert.That(extents.MaxLatitude, Is.EqualTo(-90));
            Assert.That(extents.MinLatitude, Is.EqualTo(90));
            Assert.That(extents.MaxLongitude, Is.EqualTo(-180));
            Assert.That(extents.MinLongitude, Is.EqualTo(180));

            extents.Add(StarRegion);
            Assert.That(extents.MaxLatitude, Is.EqualTo(89));
            Assert.That(extents.MinLatitude, Is.EqualTo(-90));
            Assert.That(extents.MaxLongitude, Is.EqualTo(90));
            Assert.That(extents.MinLongitude, Is.EqualTo(-90));

            Extents newExtents = new Extents();
            newExtents.Add(extents);
            Assert.That(newExtents.MaxLatitude, Is.EqualTo(89));
            Assert.That(newExtents.MinLatitude, Is.EqualTo(-90));
            Assert.That(newExtents.MaxLongitude, Is.EqualTo(90));
            Assert.That(newExtents.MinLongitude, Is.EqualTo(-90));

            Extents wideExtents = new Extents();
            wideExtents.Add(new Coordinate(45, 120));
            newExtents.Add(wideExtents);
            Assert.That(newExtents.MaxLatitude, Is.EqualTo(89));
            Assert.That(newExtents.MinLatitude, Is.EqualTo(-90));
            Assert.That(newExtents.MaxLongitude, Is.EqualTo(120));
            Assert.That(newExtents.MinLongitude, Is.EqualTo(-90));
            
            Extents wideNegativeExtents = new Extents();
            wideNegativeExtents.Add(new Coordinate(-35, -140));
            newExtents.Add(wideNegativeExtents);
            Assert.That(newExtents.MaxLatitude, Is.EqualTo(89));
            Assert.That(newExtents.MinLatitude, Is.EqualTo(-90));
            Assert.That(newExtents.MaxLongitude, Is.EqualTo(120));
            Assert.That(newExtents.MinLongitude, Is.EqualTo(-140));
        }

        [Test]
        public void IsWithinExtents_Within_Extents_Returns_True()
        {
            Extents extents = new Extents();
            extents.Add(StarRegion);

            Assert.IsTrue(extents.IsWithinExtents(new Coordinate(25, 10)));
        }

        [Test]
        public void IsWithinExtents_Not_Within_Extents_Returns_False()
        {
            Extents extents = new Extents();
            extents.Add(StarRegion);

            Assert.IsFalse(extents.IsWithinExtents(new Coordinate(25, 100)));   // Partially within extents
            Assert.IsFalse(extents.IsWithinExtents(new Coordinate(95, 10)));    // Partial within extents
            Assert.IsFalse(extents.IsWithinExtents(new Coordinate(95, 100)));   // Totally outside extents
        }

        [Test]
        public void GetExtents_Returns_New_Extents()
        {
            Extents newExtents = new Extents(StarRegion);
            Assert.That(newExtents.MaxLatitude, Is.EqualTo(89));
            Assert.That(newExtents.MinLatitude, Is.EqualTo(-90));
            Assert.That(newExtents.MaxLongitude, Is.EqualTo(90));
            Assert.That(newExtents.MinLongitude, Is.EqualTo(-90));
        }

        [Test]
        public void Extents_Clone_As_Object()
        {
            Extents extents = new Extents(StarRegion);
            object newExtents = extents.Clone();
            Extents castExtents = (Extents) newExtents;

            Assert.That(castExtents.MaxLatitude, Is.EqualTo(89));
            Assert.That(castExtents.MinLatitude, Is.EqualTo(-90));
            Assert.That(castExtents.MaxLongitude, Is.EqualTo(90));
            Assert.That(castExtents.MinLongitude, Is.EqualTo(-90));
        }

        [Test]
        public void Extents_Clone_As_Extents()
        {
            Extents extents = new Extents(StarRegion);
            Extents newExtents = extents.Clone();

            Assert.That(newExtents.MaxLatitude, Is.EqualTo(89));
            Assert.That(newExtents.MinLatitude, Is.EqualTo(-90));
            Assert.That(newExtents.MaxLongitude, Is.EqualTo(90));
            Assert.That(newExtents.MinLongitude, Is.EqualTo(-90));
        }
    }
}
