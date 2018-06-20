using System;
using System.Collections.Generic;
using MPT.Geometry.Intersection;
using MPT.Math;
using NUnit.Framework;

namespace MPT.Geometry.UnitTests.Intersection
{
    [TestFixture]
    public class ProjectionVerticalTests
    {
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
                                            new Point(-5, -5),
                                            new Point(-5, -2),
                                            new Point(-2, -2),
                                            new Point(-2, 2),
                                            new Point(-5, 2),
                                            new Point(-5, 5),
                                        };

        [Test]
        public void NumberOfIntersections_Of_Polyline_Throws_Argument_Exception()
        {
            Point coordinate = new Point(1, 1);
            Assert.That(() => ProjectionVertical.NumberOfIntersections(coordinate, polyline.ToArray()),
                                Throws.Exception
                                    .TypeOf<ArgumentException>());
        }


        [TestCase(-6, ExpectedResult = 2)]
        [TestCase(-5, ExpectedResult = 1)] // On bottom edge
        [TestCase(0, ExpectedResult = 1)]
        [TestCase(4.8, ExpectedResult = 1)] // On top edge
        [TestCase(6, ExpectedResult = 0)]
        public int NumberOfIntersections_Between_Left_And_Right_of_Square(double y)
        {
            Point coordinate = new Point(1, y);
            return ProjectionVertical.NumberOfIntersections(coordinate, square.ToArray());
        }

        [TestCase(-6, ExpectedResult = 2)]
        [TestCase(-5, ExpectedResult = 1)]  // On bottom vertex
        [TestCase(0, ExpectedResult = 1)]   // On left segment
        [TestCase(4, ExpectedResult = 1)]   // On top vertex
        [TestCase(6, ExpectedResult = 0)]
        public int NumberOfIntersections_Aligned_With_Left_of_Square(double y)
        {
            Point coordinate = new Point(5, y);
            return ProjectionVertical.NumberOfIntersections(coordinate, square.ToArray());
        }

        [TestCase(-6, ExpectedResult = 0)]
        [TestCase(-5, ExpectedResult = 0)]
        [TestCase(0, ExpectedResult = 0)]
        [TestCase(3.8, ExpectedResult = 0)]
        [TestCase(6, ExpectedResult = 0)]
        public int NumberOfIntersections_Left_Of_Square(double y)
        {
            Point coordinate = new Point(6, y);
            return ProjectionVertical.NumberOfIntersections(coordinate, square.ToArray());
        }

        [TestCase(-6, ExpectedResult = 4)]
        [TestCase(-5, ExpectedResult = 1)]  // On bottom vertical segment
        [TestCase(-4, ExpectedResult = 3)]
        [TestCase(-2, ExpectedResult = 1)]  // On bottom vertical segment of center gap
        [TestCase(0, ExpectedResult = 2)]   // In center gap
        [TestCase(2, ExpectedResult = 1)]   // On top vertical segment of center gap
        [TestCase(4, ExpectedResult = 1)]
        [TestCase(5, ExpectedResult = 1)]   // On top vertical segment
        [TestCase(6, ExpectedResult = 0)]
        public int NumberOfIntersections_Intersection_Multiple_Solid_Void(double y)
        {
            Point coordinate = new Point(1, y);
            return ProjectionVertical.NumberOfIntersections(coordinate, comb.ToArray());
        }

        [TestCase(-6, ExpectedResult = 4)]
        [TestCase(-5, ExpectedResult = 1)]  // On bottom vertex
        [TestCase(-4, ExpectedResult = 1)]  // On bottom left segment
        [TestCase(0, ExpectedResult = 2)]   // In center gap
        [TestCase(4, ExpectedResult = 1)]   // On top right segment
        [TestCase(5, ExpectedResult = 1)]   // On top vertex
        [TestCase(6, ExpectedResult = 0)]
        public int NumberOfIntersections_Intersection_Multiple_Solid_Void_On_Tooth_Segment(double y)
        {
            Point coordinate = new Point(-5, y);
            return ProjectionVertical.NumberOfIntersections(coordinate, comb.ToArray());
        }

        [TestCase(9.9, 10, ExpectedResult = false)]
        [TestCase(10, 10, ExpectedResult = true)]
        [TestCase(10.1, 10, ExpectedResult = false)]
        [TestCase(-0.1, 0, ExpectedResult = false)]
        [TestCase(0, 0, ExpectedResult = true)]
        [TestCase(0.1, 0, ExpectedResult = false)]
        [TestCase(-9.9, -10, ExpectedResult = false)]
        [TestCase(-10, -10, ExpectedResult = true)]
        [TestCase(-10.1, -10, ExpectedResult = false)]
        public bool PointIsWithinSegmentWidth_Vertical(double xPtN, double xLeftEnd)
        {
            Point ptI = new Point(xLeftEnd, 1);
            Point ptJ = new Point(xLeftEnd, 15);

            return ProjectionVertical.PointIsWithinSegmentWidth(xPtN, ptI, ptJ);
        }

        [TestCase(9.9, 10, 10.2, ExpectedResult = false)]
        [TestCase(10, 10, 10.2, ExpectedResult = true)]
        [TestCase(10.1, 10, 10.2, ExpectedResult = true)]
        [TestCase(10.2, 10, 10.2, ExpectedResult = true)]
        [TestCase(10.3, 10, 10.2, ExpectedResult = false)]
        [TestCase(-9.9, -10, -10.2, ExpectedResult = false)]
        [TestCase(-10, -10, -10.2, ExpectedResult = true)]
        [TestCase(-10.1, -10, -10.2, ExpectedResult = true)]
        [TestCase(-10.2, -10, -10.2, ExpectedResult = true)]
        [TestCase(-10.3, -10, -10.2, ExpectedResult = false)]
        public bool PointIsWithinSegmentWidth_Horizontal(double xPtN, double xLeftEnd, double xRightEnd)
        {
            Point ptI = new Point(xLeftEnd, 1);
            Point ptJ = new Point(xRightEnd, 1);

            return ProjectionVertical.PointIsWithinSegmentWidth(xPtN, ptI, ptJ);
        }

        [TestCase(9.9, 10, 10.2, ExpectedResult = false)]
        [TestCase(10, 10, 10.2, ExpectedResult = true)]
        [TestCase(10.1, 10, 10.2, ExpectedResult = true)]
        [TestCase(10.2, 10, 10.2, ExpectedResult = true)]
        [TestCase(10.3, 10, 10.2, ExpectedResult = false)]
        [TestCase(-9.9, -10, -10.2, ExpectedResult = false)]
        [TestCase(-10, -10, -10.2, ExpectedResult = true)]
        [TestCase(-10.1, -10, -10.2, ExpectedResult = true)]
        [TestCase(-10.2, -10, -10.2, ExpectedResult = true)]
        [TestCase(-10.3, -10, -10.2, ExpectedResult = false)]
        public bool PointIsWithinSegmentWidth_Sloped(double xPtN, double xLeftEnd, double xRightEnd)
        {
            Point ptI = new Point(xLeftEnd, 1);
            Point ptJ = new Point(xRightEnd, 15);

            return ProjectionVertical.PointIsWithinSegmentWidth(xPtN, ptI, ptJ);
        }

        [TestCase(-2, -1, 15, ExpectedResult = true)]
        [TestCase(-2, 0, 15, ExpectedResult = true)]
        [TestCase(-2, 1, 15, ExpectedResult = true)]
        [TestCase(0, 1, 15, ExpectedResult = true)]
        [TestCase(0.9, 1, 15, ExpectedResult = true)]
        [TestCase(1, 1, 15, ExpectedResult = true)]
        [TestCase(1.1, 1, 15, ExpectedResult = true)]
        [TestCase(14.9, 1, 15, ExpectedResult = true)]
        [TestCase(15, 1, 15, ExpectedResult = true)]
        [TestCase(15.1, 1, 15, ExpectedResult = false)]
        [TestCase(-2, 15, -1, ExpectedResult = true)]
        [TestCase(-2, 15, 0, ExpectedResult = true)]
        [TestCase(-2, 15, 1, ExpectedResult = true)]
        [TestCase(0, 15, 1, ExpectedResult = true)]
        [TestCase(0.9, 15, 1, ExpectedResult = true)]
        [TestCase(1, 15, 1, ExpectedResult = true)]
        [TestCase(1.1, 15, 1, ExpectedResult = true)]
        [TestCase(14.9, 15, 1, ExpectedResult = true)]
        [TestCase(15, 15, 1, ExpectedResult = true)]
        [TestCase(15.1, 15, 1, ExpectedResult = false)]
        public bool PointIsBelowSegmentBottom_Vertical_Segment(double yPtN, double yBottomEnd, double yTopEnd)
        {
            Point ptI = new Point(10, yBottomEnd);
            Point ptJ = new Point(10, yTopEnd);

            return ProjectionVertical.PointIsBelowSegmentBottom(yPtN, ptI, ptJ);
        }

        [TestCase(-2, -1, ExpectedResult = true)]
        [TestCase(-0.9, -1, ExpectedResult = false)]
        [TestCase(-2, 0, ExpectedResult = true)]
        [TestCase(-2, 1, ExpectedResult = true)]
        [TestCase(0, 1, ExpectedResult = true)]
        [TestCase(0.9, 1, ExpectedResult = true)]
        [TestCase(1, 1, ExpectedResult = true)]
        [TestCase(1.1, 1, ExpectedResult = false)]
        public bool PointIsBelowSegmentBottom_Horizontal_Segment(double yPtN, double yBottomEnd)
        {
            Point ptI = new Point(10, yBottomEnd);
            Point ptJ = new Point(20, yBottomEnd);

            return ProjectionVertical.PointIsBelowSegmentBottom(yPtN, ptI, ptJ);
        }

        [TestCase(-2, -1, 15, ExpectedResult = true)]
        [TestCase(-2, 0, 15, ExpectedResult = true)]
        [TestCase(-2, 1, 15, ExpectedResult = true)]
        [TestCase(0, 1, 15, ExpectedResult = true)]
        [TestCase(0.9, 1, 15, ExpectedResult = true)]
        [TestCase(1, 1, 15, ExpectedResult = true)]
        [TestCase(1.1, 1, 15, ExpectedResult = true)]
        [TestCase(14.9, 1, 15, ExpectedResult = true)]
        [TestCase(15, 1, 15, ExpectedResult = true)]
        [TestCase(15.1, 1, 15, ExpectedResult = false)]
        [TestCase(-2, 15, -1, ExpectedResult = true)]
        [TestCase(-2, 15, 0, ExpectedResult = true)]
        [TestCase(-2, 15, 1, ExpectedResult = true)]
        [TestCase(0, 15, 1, ExpectedResult = true)]
        [TestCase(0.9, 15, 1, ExpectedResult = true)]
        [TestCase(1, 15, 1, ExpectedResult = true)]
        [TestCase(1.1, 15, 1, ExpectedResult = true)]
        [TestCase(14.9, 15, 1, ExpectedResult = true)]
        [TestCase(15, 15, 1, ExpectedResult = true)]
        [TestCase(15.1, 15, 1, ExpectedResult = false)]
        public bool PointIsBelowSegmentBottom_Sloped_Segment(double yPtN, double yBottomEnd, double yTopEnd)
        {
            Point ptI = new Point(10, yBottomEnd);
            Point ptJ = new Point(20, yTopEnd);

            return ProjectionVertical.PointIsBelowSegmentBottom(yPtN, ptI, ptJ);
        }

        // Using f(y) = 1 + 0.5 * y
        [TestCase(1.5, ExpectedResult = 1)] // Bottom of segment
        [TestCase(2, ExpectedResult = 2)] // On segment end
        [TestCase(2.5, ExpectedResult = 3)] // Between segment end
        [TestCase(3, ExpectedResult = 4)] // On segment end
        [TestCase(3.5, ExpectedResult = 5)] // Top of segment
        public double IntersectionPointY_Sloped(double xPtN)
        {
            Point ptI = new Point(2, 2);
            Point ptJ = new Point(3, 4);

            return ProjectionVertical.IntersectionPointY(xPtN, ptI, ptJ);
        }

        [Test]
        public void IntersectionPointY_Vertical_Throws_Argument_Exception()
        {
            Point ptI = new Point(2, 2);
            Point ptJ = new Point(2, 4);

            Assert.That(() => ProjectionVertical.IntersectionPointY(2, ptI, ptJ),
                Throws.Exception
                    .TypeOf<ArgumentException>());
        }

        [TestCase(1.9, ExpectedResult = 2)] // Bottom of segment
        [TestCase(2, ExpectedResult = 2)] // On segment
        [TestCase(2.1, ExpectedResult = 2)] // Top of segment
        public double IntersectionPointY_Horizontal(double xPtN)
        {
            Point ptI = new Point(2, 2);
            Point ptJ = new Point(3, 2);

            return ProjectionVertical.IntersectionPointY(xPtN, ptI, ptJ);
        }

        [TestCase(-2, -1, ExpectedResult = true)]
        [TestCase(-2, 1, ExpectedResult = true)]
        [TestCase(-2, 0, ExpectedResult = true)]
        [TestCase(0, 2, ExpectedResult = true)]
        [TestCase(1, 2, ExpectedResult = true)]
        [TestCase(2, 2, ExpectedResult = false)]    // Pt is on segment intersection
        [TestCase(3, 2, ExpectedResult = false)]
        [TestCase(-1, -2, ExpectedResult = false)]
        [TestCase(1, -2, ExpectedResult = false)]
        [TestCase(0, -2, ExpectedResult = false)]
        [TestCase(2, 0, ExpectedResult = false)]
        [TestCase(2, 1, ExpectedResult = false)]
        public bool PointIsBelowSegmentIntersection_Within_Segment(double yPtN, double yIntersection)
        {
            Point vertexI = new Point(1, -100);
            Point vertexJ = new Point(2, 100);
            return ProjectionVertical.PointIsBelowSegmentIntersection(yPtN, yIntersection, vertexI, vertexJ);
        }

        [TestCase(-2, -1.1, ExpectedResult = false)]
        [TestCase(-2, 1.1, ExpectedResult = false)]
        [TestCase(0, 1.1, ExpectedResult = false)]
        [TestCase(1, 1.1, ExpectedResult = false)]
        public bool PointIsBelowSegmentIntersection_Outside_Segment(double yPtN, double yIntersection)
        {
            Point vertexI = new Point(1, -1);
            Point vertexJ = new Point(2, 1);
            return ProjectionVertical.PointIsBelowSegmentIntersection(yPtN, yIntersection, vertexI, vertexJ);
        }
    }
}
