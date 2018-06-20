// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="IFrameCurved.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;

namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has settable/gettable curved frame attributes.
    /// </summary>
    public interface IFrameCurved
    {
        /// <summary>
        /// This function retrieves definition data for all curved frame objects and returns the data in arrays.
        /// </summary>
        /// <param name="numberItems">The number of curved frame objects returned.</param>
        /// <param name="frameObjectNames">Names of the frame objects returned.</param>
        /// <param name="types">Curved frame types for each object returned.</param>
        /// <param name="globalX">The x coordinate point in the global coordinate system. [L]
        /// For <paramref name="types" /> = <see cref="eCurvedFrameType.CircleThirdPointName" /> and <see cref="eCurvedFrameType.ParabolaThirdPointName" /> these items do not apply.
        /// For <paramref name="types" /> = <see cref="eCurvedFrameType.CircleThirdPointCoordinates" /> and <see cref="eCurvedFrameType.ParabolaThirdPointCoordinates" /> these are the coordinates of the third point on the curved frame.
        /// For <paramref name="types" /> = <see cref="eCurvedFrameType.CirclePlanarCoordinatesAndRadius" /> these are the coordinates of the planar point that lies in the plane of the curved frame.</param>
        /// <param name="globalY">The y coordinate point in the global coordinate system. [L]
        /// For <paramref name="types" /> = <see cref="eCurvedFrameType.CircleThirdPointName" /> and <see cref="eCurvedFrameType.ParabolaThirdPointName" /> these items do not apply.
        /// For <paramref name="types" /> = <see cref="eCurvedFrameType.CircleThirdPointCoordinates" /> and <see cref="eCurvedFrameType.ParabolaThirdPointCoordinates" /> these are the coordinates of the third point on the curved frame.
        /// For <paramref name="types" /> = <see cref="eCurvedFrameType.CirclePlanarCoordinatesAndRadius" /> these are the coordinates of the planar point that lies in the plane of the curved frame.</param>
        /// <param name="globalZ">The z coordinate point in the global coordinate system. [L]
        /// For <paramref name="types" /> = <see cref="eCurvedFrameType.CircleThirdPointName" /> and <see cref="eCurvedFrameType.ParabolaThirdPointName" /> these items do not apply.
        /// For <paramref name="types" /> = <see cref="eCurvedFrameType.CircleThirdPointCoordinates" /> and <see cref="eCurvedFrameType.ParabolaThirdPointCoordinates" /> these are the coordinates of the third point on the curved frame.
        /// For <paramref name="types" /> = <see cref="eCurvedFrameType.CirclePlanarCoordinatesAndRadius" /> these are the coordinates of the planar point that lies in the plane of the curved frame.</param>
        /// <param name="pointNames">The name of the point object that is the third point on the curved frame.
        /// This item applies for MyType 1 and 4.</param>
        /// <param name="radius">This is an array of the radii of the circular curved frame.
        /// This item only applies for MyType 3. [L]</param>
        /// <param name="numberSegments">This is an array that includes the number of segments into which the program internally divides the curved frame.</param>
        void GetCurved(ref int numberItems,
            ref string[] frameObjectNames,
            ref eCurvedFrameType[] types,
            ref double[] globalX,
            ref double[] globalY,
            ref double[] globalZ,
            ref string[] pointNames,
            ref double[] radius,
            ref int[] numberSegments);

        /// <summary>
        /// This function changes the curve data for a curved frame object and sets straight frame objects to be curved.
        /// </summary>
        /// <param name="frameObjectName">The name of a defined curved frame object.</param>
        /// <param name="type">Curved frame types for each object returned.</param>
        /// <param name="globalX">The x coordinate point in the global coordinate system. [L]
        /// For <paramref name="type" /> = <see cref="eCurvedFrameType.CircleThirdPointName" /> and <see cref="eCurvedFrameType.ParabolaThirdPointName" /> these items do not apply.
        /// For <paramref name="type" /> = <see cref="eCurvedFrameType.CircleThirdPointCoordinates" /> and <see cref="eCurvedFrameType.ParabolaThirdPointCoordinates" /> these are the coordinates of the third point on the curved frame.
        /// For <paramref name="type" /> = <see cref="eCurvedFrameType.CirclePlanarCoordinatesAndRadius" /> these are the coordinates of the planar point that lies in the plane of the curved frame.</param>
        /// <param name="globalY">The y coordinate point in the global coordinate system. [L]
        /// For <paramref name="type" /> = <see cref="eCurvedFrameType.CircleThirdPointName" /> and <see cref="eCurvedFrameType.ParabolaThirdPointName" /> these items do not apply.
        /// For <paramref name="type" /> = <see cref="eCurvedFrameType.CircleThirdPointCoordinates" /> and <see cref="eCurvedFrameType.ParabolaThirdPointCoordinates" /> these are the coordinates of the third point on the curved frame.
        /// For <paramref name="type" /> = <see cref="eCurvedFrameType.CirclePlanarCoordinatesAndRadius" /> these are the coordinates of the planar point that lies in the plane of the curved frame.</param>
        /// <param name="globalZ">The z coordinate point in the global coordinate system. [L]
        /// For <paramref name="type" /> = <see cref="eCurvedFrameType.CircleThirdPointName" /> and <see cref="eCurvedFrameType.ParabolaThirdPointName" /> these items do not apply.
        /// For <paramref name="type" /> = <see cref="eCurvedFrameType.CircleThirdPointCoordinates" /> and <see cref="eCurvedFrameType.ParabolaThirdPointCoordinates" /> these are the coordinates of the third point on the curved frame.
        /// For <paramref name="type" /> = <see cref="eCurvedFrameType.CirclePlanarCoordinatesAndRadius" /> these are the coordinates of the planar point that lies in the plane of the curved frame.</param>
        /// <param name="pointName">The name of the point object that is the third point on the curved frame.
        /// This item applies for MyType 1 and 4.</param>
        /// <param name="radius">This is an array of the radii of the circular curved frame.
        /// This item only applies for MyType 3. [L]</param>
        /// <param name="numberSegments">This is an array that includes the number of segments into which the program internally divides the curved frame.</param>
        /// <param name="coordSystem">This is the coordinate system in which the coordinates x, y and z are defined.</param>
        void SetCurved(string frameObjectName,
            eCurvedFrameType type,
            double globalX,
            double globalY,
            double globalZ,
            string pointName,
            double radius,
            int numberSegments,
            string coordSystem = CoordinateSystems.Global);

        /// <summary>
        /// This function sets a curved frame object straight.
        /// </summary>
        /// <param name="name">The name of a defined curved frame object.</param>
        void SetStraight(string name);

        /// <summary>
        /// This function retrieves the type of frame object (straight or curved).
        /// </summary>
        /// <param name="name">The name of a defined frame object.</param>
        /// <param name="frameType">The type of frame object.</param>
        void GetType(string name,
            ref eFrameType frameType);
    }
}
#endif