// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-10-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="CoordinateSystems.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Represents the coordinate systems in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.ICoordinateSystems" />
    public class CoordinateSystems : CSiApiBase, ICoordinateSystems
    {
        #region Fields        
        /// <summary>
        /// The global coordinate system name.
        /// </summary>
        public const string Global = "GLOBAL";

        /// <summary>
        /// The local coordinate system name.
        /// </summary>
        public const string Local = "LOCAL";

        #endregion

        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="CoordinateSystems" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public CoordinateSystems(CSiApiSeed seed) : base(seed) { }


        #endregion
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        #region Methods: Interface

        /// <summary>
        /// The function returns zero if the new name is successfully applied, otherwise it returns a nonzero value.
        /// Changing the name of the Global coordinate system will fail and return an error.
        /// </summary>
        /// <param name="name">The existing name of a defined coordinate system.</param>
        /// <param name="newName">The new name for the coordinate system.</param>
        /// <exception cref="CSiReservedNameException">
        /// Cannot change global coordinate system name " + Global
        /// or
        /// Cannot change coordinate system name " + newName + "to global coordinate system name " + Global
        /// </exception>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ChangeName(string name,
            string newName)
        {
            if (Global.CompareTo(name) == 0)
            {
                throw new CSiReservedNameException("Cannot change global coordinate system name " + Global);
            }
            if (Global.CompareTo(newName) == 0)
            {
                throw new CSiReservedNameException("Cannot change coordinate system name " + newName + "to global coordinate system name " + Global);
            }

            _callCode = _sapModel.CoordSys.ChangeName(name, newName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// The function returns the number of defined coordinate systems.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int Count()
        {
            return _sapModel.CoordSys.Count();
        }

        /// <summary>
        /// The function deletes the specified coordinate system.
        /// Attempting to delete the Global coordinate system will fail and return an error.
        /// </summary>
        /// <param name="nameCoordinateSystem">The name of an existing coordinate system.</param>
        /// <exception cref="CSiReservedNameException">Cannot delete global coordinate system " + Global</exception>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void Delete(string nameCoordinateSystem)
        {
            if (Global.CompareTo(nameCoordinateSystem) == 0)
            {
                throw new CSiReservedNameException("Cannot delete global coordinate system " + Global);
            }

            _callCode = _sapModel.CoordSys.Delete(nameCoordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves the names of all defined coordinate systems.
        /// </summary>
        /// <param name="coordinateSystemNames">The coordinate system names retrieved by the program.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetNameList(out string[] coordinateSystemNames)
        {
            coordinateSystemNames = new string[0];
            _callCode = _sapModel.CoordSys.GetNameList(ref _numberOfItems, ref coordinateSystemNames);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Returns the 3x3 direction cosines to transform local coordinates to global coordinates by the equation [directionCosines]*[localCoordinates] = [globalCoordinates].
        /// Direction cosines returned are ordered by row, and then by column.
        /// </summary>
        /// <param name="nameObject">The name of an existing object.</param>
        /// <param name="directionCosines">Value is an array of nine direction cosines that define the transformation matrix from the local coordinate system to the global coordinate system.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetTransformationMatrix(string nameObject,
            out double[] directionCosines)
        {
            directionCosines = new double[0];
            _callCode = _sapModel.CoordSys.GetTransformationMatrix(nameObject, ref directionCosines);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        #endregion

        #region Methods: Public

        // === Get/Set ===

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
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetCoordinateSystem(string nameCoordinateSystem,
            ref double xCoordinateOrigin,
            ref double yCoordinateOrigin,
            ref double zCoordinateOrigin,
            ref double rzCoordinateOrigin,
            ref double ryCoordinateOrigin,
            ref double rxCoordinateOrigin)
        {
            _callCode = _sapModel.CoordSys.GetCoordSys(nameCoordinateSystem, ref xCoordinateOrigin, ref yCoordinateOrigin, ref zCoordinateOrigin, ref rzCoordinateOrigin, ref ryCoordinateOrigin, ref rxCoordinateOrigin);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

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
        /// <exception cref="CSiReservedNameException">Cannot modify global coordinate system " + Global</exception>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetCoordinateSystem(string nameCoordinateSystem,
            double xCoordinateOrigin,
            double yCoordinateOrigin,
            double zCoordinateOrigin,
            double rzCoordinateOrigin,
            double ryCoordinateOrigin,
            double rxCoordinateOrigin)
        {
            if (Global.CompareTo(nameCoordinateSystem) == 0)
            {
                throw new CSiReservedNameException("Cannot modify global coordinate system " + Global);
            }
            _callCode = _sapModel.CoordSys.SetCoordSys(nameCoordinateSystem, xCoordinateOrigin, yCoordinateOrigin, zCoordinateOrigin, rzCoordinateOrigin, ryCoordinateOrigin, rxCoordinateOrigin);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        #endregion
#endif
    }
}
