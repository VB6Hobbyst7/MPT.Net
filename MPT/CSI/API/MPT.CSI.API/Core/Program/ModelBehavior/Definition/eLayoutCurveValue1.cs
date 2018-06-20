// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-14-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="eLayoutCurveValue1.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_CSiBridgev18 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Depends on the eLayoutCurveType.
    /// TODO: This is not to be used as an enum, but a reference for later object behavior.
    /// </summary>
    public enum eLayoutCurveValue1
    {
        /// <summary>
        /// Not used.
        /// </summary>
        NotUsed = 0,

        /// <summary>
        /// Curve Radius [L].
        /// </summary>
        CurveRadius = 1,

        /// <summary>
        /// Curve Radius [L].
        /// </summary>
        CurveRadius1 = 2,

        /// <summary>
        /// (Plan) Angle measured from the X-axis of the coordinate system in which the general reference line is defined, to the axis of symmetry of the parabolic curve. [deg].
        /// (Elevation) Angle measured from the horizontal, up station axis, to the axis of symmetry of the parabolic curve. [deg].
        /// </summary>
        AngleToAxisSymmetry = 3,

        /// <summary>
        /// Number of control points. 
        /// This is currently hard-wired internally to 4.
        /// </summary>
        NumberControlPts = 4,

        /// <summary>
        /// Number of control points.
        /// </summary>
        NumberControlPts2 = 5,

        /// <summary>
        /// Not used.
        /// </summary>
        NotUsed6 = 6,

        /// <summary>
        /// Not used.
        /// </summary>
        NotUsed7 = 7
    }
}
#endif