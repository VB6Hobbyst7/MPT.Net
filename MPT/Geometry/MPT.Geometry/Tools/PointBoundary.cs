using System;
using System.Collections.Generic;

using MPT.Math;

namespace MPT.Geometry.Tools
{
    public class PointBoundary : Boundary<Point>
    {
        #region Initialization


        public PointBoundary()
        {

        }


        public PointBoundary(IEnumerable<Point> coordinates) : base(coordinates)
        {
           // _coordinates = coordinates;
        }
        #endregion

        #region Methods: Public

        public override void Clear()
        {
            _coordinates = new List<Point>();
        }

        // TODO: For all boundary changes, clusters and holes occur when a shape is entirely outside of or inside of the shape group.
        // This is to be handled by linking the shape areas by a collinear segment.
        // All positive shapes are determined by CCW travel.
        // All negative shapes are determined by CW travel.

        // TODO: Finish
        /// <summary>
        /// Adds to boundary.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void Add(IList<Point> coordinates)
        {
            throw new NotImplementedException();
        }

        // TODO: Finish
        /// <summary>
        /// Removes from boundary.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void Remove(IList<Point> coordinates)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
