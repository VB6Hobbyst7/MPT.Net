using System.Collections.Generic;
using NMath = System.Math;

using MPT.Math;

namespace MPT.Geometry.Tools
{
    public class PointExtents : Extents<Point>
    {
        

        public PointExtents()
        { }


        public PointExtents(IEnumerable<Point> coordinates) : base (coordinates)
        { }
        
        public PointExtents(Extents<Point> extents) : base (extents)
        { }

        /// <summary>
        /// Updates the extents to include the specified coordinate.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        public override void Add(Point coordinate)
        {
            if (coordinate.Y > MaxY)
            {
                MaxY = NMath.Min(coordinate.Y, _minYLimit);
            }
            if (coordinate.Y < MinY)
            {
                MinY = NMath.Max(coordinate.Y, _maxYLimit);
            }

            if (coordinate.X > MaxX)
            {
                MaxX = NMath.Min(coordinate.X, _minXLimit);
            }
            if (coordinate.X < MinX)
            {
                MinX = NMath.Max(coordinate.X, _maxXLimit);
            }
        }

        ///// <summary>
        ///// Updates the extents to include the specified coordinates.
        ///// </summary>
        ///// <param name="coordinates">The coordinates.</param>
        //public void Add(IEnumerable<cartCoords> coordinates)
        //{
        //    foreach (cartCoords coordinate in coordinates)
        //    {
        //        Add(coordinate);
        //    }
        //}

        ///// <summary>
        ///// Updates the extents to include the specified extents.
        ///// </summary>
        ///// <param name="extents">The extents.</param>
        //public void Add(Extents<cartCoords> extents)
        //{
        //    if (extents.MaxY > MaxY)
        //    {
        //        MaxY = extents.MaxY;
        //    }
        //    if (extents.MinY < MinY)
        //    {
        //        MinY = extents.MinY;
        //    }
        //    if (extents.MaxX > MaxX)
        //    {
        //        MaxX = extents.MaxX;
        //    }
        //    if (extents.MinX < MinX)
        //    {
        //        MinX = extents.MinX;
        //    }
        //}

       
        /// <summary>
        /// Determines whether the coordinate lies within the extents.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns><c>true</c> if the specified coordinates are within the extents; otherwise, <c>false</c>.</returns>
        public override bool IsWithinExtents(Point coordinate)
        {
            return ((MinX <= coordinate.X && coordinate.X <= MaxX) &&
                    (MinY <= coordinate.Y && coordinate.Y <= MaxY));
        }

        /// <summary>
        /// Returns a rectangle boundary of this instance.
        /// </summary>
        /// <returns>NRectangle.</returns>
        public override IList<Point> Boundary()
        {
            return new List<Point>()
            {
                new Point(MinX, MaxY),
                new Point(MaxX, MaxY),
                new Point(MaxX, MinY),
                new Point(MinX, MinY),
            };
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Extents.</returns>
        public PointExtents Clone()
        {
            return new PointExtents(this);
        }
    }
}
