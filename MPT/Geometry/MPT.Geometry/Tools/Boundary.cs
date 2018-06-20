// ***********************************************************************
// Assembly         : MPT.Geometry
// Author           : Mark Thomas
// Created          : 12-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-09-2017
// ***********************************************************************
// <copyright file="Boundary.cs" company="MPTinc">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using GL = MPT.Geometry.GeometryLibrary;

namespace MPT.Geometry.Tools
{
    /// <summary>
    /// Represents boundary coordinates.
    /// </summary>
    public abstract class Boundary<T>
    {
        #region Properties
        /// <summary>
        /// Tolerance to use in all calculations with double types.
        /// </summary>
        /// <value>The tolerance.</value>
        public double Tolerance { get; set; } = GL.ZeroTolerance;

        /// <summary>
        /// The coordinates.
        /// </summary>
        protected IEnumerable<T> _coordinates;
        /// <summary>
        /// The coordinates that compose the boundary.
        /// </summary>
        /// <value>The boundary.</value>
        public IList<T> Coordinates => new ReadOnlyCollection<T>(_coordinates.ToList());
        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="Boundary<T>"/> class.
        /// </summary>
        protected Boundary()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Boundary<T>"/> class.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        protected Boundary(IEnumerable<T> coordinates)
        {
            _coordinates = coordinates;
        }
        #endregion

        #region Methods: Public

        public abstract void Clear();

        public void Reset(IList<T> coordinates)
        {
            Clear();
            Add(coordinates);
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
        public abstract void Add(IList<T> coordinates);

        // TODO: Finish
        /// <summary>
        /// Removes from boundary.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public abstract void Remove(IList<T> coordinates);

        #endregion

        #region Methods: Private

        #endregion
    }
}
