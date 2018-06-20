using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPT.Geometry.Intersection;
using MPT.Math;
using NUnit.Framework;

namespace MPT.Geometry.UnitTests.Intersection
{
    [TestFixture]
    public class PointIntersectionTests
    {

        [TestCase(0.9, 2, 1, 2, ExpectedResult = false)] // Left
        [TestCase(1.1, 2, 1, 2, ExpectedResult = false)] // Right
        [TestCase(1, 2.1, 1, 2, ExpectedResult = false)] // Above
        [TestCase(1, 1.9, 1, 2, ExpectedResult = false)] // Below
        [TestCase(1, 2, 1, 2, ExpectedResult = true)] // On
        [TestCase(-0.9, -2, -1, -2, ExpectedResult = false)] // Left
        [TestCase(-1.1, -2, -1, -2, ExpectedResult = false)] // Right
        [TestCase(-1, -2.1, -1, -2, ExpectedResult = false)] // Above
        [TestCase(-1, -1.9, -1, -2, ExpectedResult = false)] // Below
        [TestCase(-1, -2, -1, -2, ExpectedResult = true)] // On
        [TestCase(-0.9, 2, -1, 2, ExpectedResult = false)] // Left
        [TestCase(-1.1, 2, -1, 2, ExpectedResult = false)] // Right
        [TestCase(-1, 2.1, -1, 2, ExpectedResult = false)] // Above
        [TestCase(-1, 1.9, -1, 2, ExpectedResult = false)] // Below
        [TestCase(-1, 2, -1, 2, ExpectedResult = true)] // On
        [TestCase(0.9, -2, 1, -2, ExpectedResult = false)] // Left
        [TestCase(1.1, -2, 1, -2, ExpectedResult = false)] // Right
        [TestCase(1, -2.1, 1, -2, ExpectedResult = false)] // Above
        [TestCase(1, -1.9, 1, -2, ExpectedResult = false)] // Below
        [TestCase(1, -2, 1, -2, ExpectedResult = true)] // On
        public bool PointsOverlap(double x, double y, double x1, double y1)
        {
            Point point = new Point(x1, y1);
            return PointIntersection.PointsOverlap(point, new Point(x, y));
        }

        private List<Point> polyline = new List<Point>()
                                        {
                                            new Point(-5, 5),
                                            new Point(4, 5),
                                            new Point(6, -5),
                                            new Point(-5, -5),
                                        };

        private List<Point> square = new List<Point>()
                                        {
                                            new Point(-5, 5),
                                            new Point(4, 5),
                                            new Point(6, -5),
                                            new Point(-5, -5),
                                            new Point(-5, 5),
                                        };

        private List<Point> comb = new List<Point>()
                                        {
                                            new Point(-5, 5),
                                            new Point(5, 5),
                                            new Point(5, -5),
                                            new Point(2, -5),
                                            new Point(2, 2),
                                            new Point(-2, 2),
                                            new Point(-2, -5),
                                            new Point(-5, -5),
                                            new Point(-5, 5),
                                        };

        [Test]
        public void IsWithinShape_Of_Polyline_Throws_Argument_Exception()
        {
            Point coordinate = new Point(1, 1);
            Assert.That(() => PointIntersection.IsWithinShape(coordinate, polyline.ToArray()),
                                Throws.Exception
                                    .TypeOf<ArgumentException>());
        }


        [TestCase(-6, ExpectedResult = false)]
        [TestCase(-5, ExpectedResult = true)]
        [TestCase(0, ExpectedResult = true)]
        [TestCase(4.8, ExpectedResult = true)] // Intercept with right sloping line segment
        [TestCase(6, ExpectedResult = false)]
        public bool IsWithinShape_Between_Top_And_Bottom_of_Square(double x)
        {
            Point coordinate = new Point(x, 1);
            return PointIntersection.IsWithinShape(coordinate, square.ToArray());
        }

        [TestCase(-6, ExpectedResult = false)]
        [TestCase(-5, ExpectedResult = true)]
        [TestCase(0, ExpectedResult = true)]
        [TestCase(4, ExpectedResult = true)]
        [TestCase(6, ExpectedResult = false)]
        public bool IsWithinShape_Aligned_With_Top_of_Square(double x)
        {
            Point coordinate = new Point(x, 5);
            return PointIntersection.IsWithinShape(coordinate, square.ToArray());
        }

        [TestCase(-6, ExpectedResult = false)]
        [TestCase(-5, ExpectedResult = false)]
        [TestCase(0, ExpectedResult = false)]
        [TestCase(3.8, ExpectedResult = false)] // Intercept with right sloping line
        [TestCase(6, ExpectedResult = false)]
        public bool IsWithinShape_Above_Square(double x)
        {
            Point coordinate = new Point(x, 6);
            return PointIntersection.IsWithinShape(coordinate, square.ToArray());
        }

        [TestCase(-6, ExpectedResult = false)]
        [TestCase(-5, ExpectedResult = true)]
        [TestCase(-4, ExpectedResult = true)]
        [TestCase(-2, ExpectedResult = true)]
        [TestCase(0, ExpectedResult = false)]
        [TestCase(2, ExpectedResult = true)]
        [TestCase(4, ExpectedResult = true)]
        [TestCase(5, ExpectedResult = true)]
        [TestCase(6, ExpectedResult = false)]
        public bool IsWithinShape_Intersection_Multiple_Solid_Void(double x)
        {
            Point coordinate = new Point(x, 1);
            return PointIntersection.IsWithinShape(coordinate, comb.ToArray());
        }

        [TestCase(-6, ExpectedResult = false)]
        [TestCase(-5, ExpectedResult = true)]
        [TestCase(-4, ExpectedResult = true)]
        [TestCase(0, ExpectedResult = false)]
        [TestCase(4, ExpectedResult = true)]
        [TestCase(5, ExpectedResult = true)]
        [TestCase(6, ExpectedResult = false)]
        public bool IsWithinShape_Intersection_Multiple_Solid_Void_On_Tooth_Segment(double x)
        {
            Point coordinate = new Point(x, -5);
            return PointIntersection.IsWithinShape(coordinate, comb.ToArray());
        }
    }
}
