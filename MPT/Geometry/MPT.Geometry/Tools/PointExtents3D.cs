using System.Collections.Generic;

using NMath = System.Math;

using MPT.Math;

namespace MPT.Geometry.Tools
{
    public class PointExtents3D : Extents3D<Point3D, Point, PointExtents>
    {
        public PointExtents3D()
        { }


        public PointExtents3D(IEnumerable<Point3D> coordinates) : base (coordinates)
        { }

        public PointExtents3D(PointExtents3D extents) : base (extents)
        { }

        public PointExtents3D(Extents<Point> extents, double zCoordinate) : base(extents, zCoordinate)
        { }

        /// <summary>
        /// Adds the specified extents.
        /// </summary>
        /// <param name="extents">The extents.</param>
        /// <param name="zCoordinate">The z-coordinate of the extents object.</param>
        public override void Add(Extents<Point> extents, double zCoordinate)
        {
            List<Point3D> points = new List<Point3D>()
                                    {
                                        new Point3D(extents.MaxX, extents.MaxY, zCoordinate),
                                        new Point3D(extents.MaxX, extents.MinY, zCoordinate),
                                        new Point3D(extents.MinX, extents.MinY, zCoordinate),
                                        new Point3D(extents.MinX, extents.MaxY, zCoordinate),
                                        new Point3D(extents.MaxX, extents.MaxY, zCoordinate),
                                        new Point3D(extents.MaxX, extents.MinY, zCoordinate),
                                        new Point3D(extents.MinX, extents.MinY, zCoordinate),
                                        new Point3D(extents.MinX, extents.MaxY, zCoordinate),
                                    };
            Add(points);
        }

        /// <summary>
        /// Updates the extents to include the specified coordinate.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        public override void Add(Point3D coordinate)
        {
            if (coordinate.Y > MaxY)
            {
                MaxY = NMath.Min(coordinate.Y, _minY);
            }
            if (coordinate.Y < MinY)
            {
                MinY = NMath.Max(coordinate.Y, _maxY);
            }

            if (coordinate.X > MaxX)
            {
                MaxX = NMath.Min(coordinate.X, _minX);
            }
            if (coordinate.X < MinX)
            {
                MinX = NMath.Max(coordinate.X, _maxX);
            }

            if (coordinate.Z > MaxZ)
            {
                MaxZ = NMath.Min(coordinate.Z, _minZ);
            }
            if (coordinate.Z < MinZ)
            {
                MinZ = NMath.Max(coordinate.Z, _maxZ);
            }
        }

        /// <summary>
        /// Determines whether the coordinate lies within the extents.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns><c>true</c> if the specified coordinates are within the extents; otherwise, <c>false</c>.</returns>
        public override bool IsWithinExtents(Point3D coordinate)
        {
            return ((MinX <= coordinate.X && coordinate.X <= MaxX) &&
                    (MinY <= coordinate.Y && coordinate.Y <= MaxY) &&
                    (MinZ <= coordinate.Z && coordinate.Z <= MaxZ));
        }

        /// <summary>
        /// Projects this instance to a 2-dimensional extents object in the X-Y plane.
        /// </summary>
        /// <returns>Extents.</returns>
        public override Extents<Point> ProjectXY()
        {
            PointExtents extents = new PointExtents();
            extents.Add(new Point(MaxX, MaxY));
            extents.Add(new Point(MaxX, MinY));
            extents.Add(new Point(MinX, MinY));
            extents.Add(new Point(MinX, MaxY));
            return extents;
        }

        /// <summary>
        /// Projects this instance to a 2-dimensional extents object in the X-Z plane, where the y-coordinate is to be taken as the y-coordinate.
        /// </summary>
        /// <returns>Extents.</returns>
        public override Extents<Point> ProjectXZ()
        {
            PointExtents extents = new PointExtents();
            extents.Add(new Point(MaxX, MaxZ));
            extents.Add(new Point(MaxX, MinZ));
            extents.Add(new Point(MinX, MinZ));
            extents.Add(new Point(MinX, MaxZ));
            return extents;
        }

        /// <summary>
        /// Projects this instance to a 2-dimensional extents object in the Y-Z plane, where the x-coordinate is to be taken as the z-coordinate.
        /// </summary>
        /// <returns>Extents.</returns>
        public override Extents<Point> ProjectYZ()
        {
            PointExtents extents = new PointExtents();
            extents.Add(new Point(MaxZ, MaxY));
            extents.Add(new Point(MaxZ, MinY));
            extents.Add(new Point(MinZ, MinY));
            extents.Add(new Point(MinZ, MaxY));
            return extents;
        }


        /// <summary>
        /// Returns a rectangle boundary of this instance.
        /// </summary>
        /// <returns>NRectangle.</returns>
        public override IList<Point3D> Boundary()
        {
            return new List<Point3D>()
            {
                new Point3D(MinX, MaxY, MinZ),
                new Point3D(MaxX, MaxY, MinZ),
                new Point3D(MaxX, MinY, MinZ),
                new Point3D(MinX, MinY, MinZ),
                new Point3D(MinX, MaxY, MaxZ),
                new Point3D(MaxX, MaxY, MaxZ),
                new Point3D(MaxX, MinY, MaxZ),
                new Point3D(MinX, MinY, MaxZ),
            };
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Extents.</returns>
        public PointExtents3D Clone()
        {
            return new PointExtents3D(this);
        }
    }
}
