// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="ICoordinateSystems.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
    /// <summary>
    /// Implements the coordinate systems in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableTransformationMatrix" />
    public interface ICoordinateSystems : IChangeableName, ICountable, IDeletable, IListableNames, IObservableTransformationMatrix
#else
    /// <summary>
    /// Implements the coordinate systems in the application.
    /// </summary>
    public interface ICoordinateSystems
#endif
    {
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Retrieves the translation and rotation of the specified coordinate system origin and axes in relation to the global coordinate system.
        /// </summary>
        /// <param name="nameCoordinateSystem">The name of an existing coordinate system.</param>
        /// <param name="xCoordinateOrigin">The global X coordinate of the origin of the coordinate system. [L]</param>
        /// <param name="yCoordinateOrigin">The global Y coordinate of the origin of the coordinate system. [L]</param>
        /// <param name="zCoordinateOrigin">The global Z coordinate of the origin of the coordinate system. [L]</param>
        /// <param name="rzCoordinateOrigin">The rotation of an axis of the new coordinate system relative to the global coordinate system is defined as follows:
        /// (1) Rotate the coordinate system about the positive global Z-axis as defined by the RZ item.
        /// (2) Rotate the coordinate system about the positive global Y-axis as defined by the RY item.
        /// (3) Rotate the coordinate system about the positive global X-axis as defined by the RX item.
        /// Note that the order in which these rotations are performed is important.
        /// RX, RY and RZ are angles in degrees [deg].</param>
        /// <param name="ryCoordinateOrigin">The rotation of an axis of the new coordinate system relative to the global coordinate system is defined as follows:
        /// (1) Rotate the coordinate system about the positive global Z-axis as defined by the RZ item.
        /// (2) Rotate the coordinate system about the positive global Y-axis as defined by the RY item.
        /// (3) Rotate the coordinate system about the positive global X-axis as defined by the RX item.
        /// Note that the order in which these rotations are performed is important.
        /// RX, RY and RZ are angles in degrees [deg].</param>
        /// <param name="rxCoordinateOrigin">The rotation of an axis of the new coordinate system relative to the global coordinate system is defined as follows:
        /// (1) Rotate the coordinate system about the positive global Z-axis as defined by the RZ item.
        /// (2) Rotate the coordinate system about the positive global Y-axis as defined by the RY item.
        /// (3) Rotate the coordinate system about the positive global X-axis as defined by the RX item.
        /// Note that the order in which these rotations are performed is important.
        /// RX, RY and RZ are angles in degrees [deg].</param>
        void GetCoordinateSystem(string nameCoordinateSystem,
            ref double xCoordinateOrigin,
            ref double yCoordinateOrigin,
            ref double zCoordinateOrigin,
            ref double rzCoordinateOrigin,
            ref double ryCoordinateOrigin,
            ref double rxCoordinateOrigin);

        /// <summary>
        /// Sets the translation and rotation of the specified coordinate system origin and axes in relation to the global coordinate system.
        /// Modifying the Global coordinate system will fail and return an error.
        /// </summary>
        /// <param name="nameCoordinateSystem">The name of an existing coordinate system.</param>
        /// <param name="xCoordinateOrigin">The global X coordinate of the origin of the coordinate system. [L]</param>
        /// <param name="yCoordinateOrigin">The global Y coordinate of the origin of the coordinate system. [L]</param>
        /// <param name="zCoordinateOrigin">The global Z coordinate of the origin of the coordinate system. [L]</param>
        /// <param name="rzCoordinateOrigin">The rotation of an axis of the new coordinate system relative to the global coordinate system is defined as follows:
        /// (1) Rotate the coordinate system about the positive global Z-axis as defined by the RZ item.
        /// (2) Rotate the coordinate system about the positive global Y-axis as defined by the RY item.
        /// (3) Rotate the coordinate system about the positive global X-axis as defined by the RX item.
        /// Note that the order in which these rotations are performed is important.
        /// RX, RY and RZ are angles in degrees [deg].</param>
        /// <param name="ryCoordinateOrigin">The rotation of an axis of the new coordinate system relative to the global coordinate system is defined as follows:
        /// (1) Rotate the coordinate system about the positive global Z-axis as defined by the RZ item.
        /// (2) Rotate the coordinate system about the positive global Y-axis as defined by the RY item.
        /// (3) Rotate the coordinate system about the positive global X-axis as defined by the RX item.
        /// Note that the order in which these rotations are performed is important.
        /// RX, RY and RZ are angles in degrees [deg].</param>
        /// <param name="rxCoordinateOrigin">The rotation of an axis of the new coordinate system relative to the global coordinate system is defined as follows:
        /// (1) Rotate the coordinate system about the positive global Z-axis as defined by the RZ item.
        /// (2) Rotate the coordinate system about the positive global Y-axis as defined by the RY item.
        /// (3) Rotate the coordinate system about the positive global X-axis as defined by the RX item.
        /// Note that the order in which these rotations are performed is important.
        /// RX, RY and RZ are angles in degrees [deg].</param>
        void SetCoordinateSystem(string nameCoordinateSystem,
            double xCoordinateOrigin,
            double yCoordinateOrigin,
            double zCoordinateOrigin,
            double rzCoordinateOrigin,
            double ryCoordinateOrigin,
            double rxCoordinateOrigin);
#endif
    }
}