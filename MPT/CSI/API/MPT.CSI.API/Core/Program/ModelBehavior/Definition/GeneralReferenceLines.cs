// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-11-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="GeneralReferenceLines.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Represents the general reference line in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.IGeneralReferenceLines" />
    public class GeneralReferenceLines : CSiApiBase, IGeneralReferenceLines
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralReferenceLines" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public GeneralReferenceLines(CSiApiSeed seed) : base(seed) { }
        #endregion

        #region Methods: Interface
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function returns the total number of defined general reference lines in the model.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int Count()
        {
            return _sapModel.GenRefLine.Count();
        }

        /// <summary>
        /// The function deletes a specified general reference line.
        /// </summary>
        /// <param name="name">The name of an existing general reference line.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void Delete(string name)
        {
            _callCode = _sapModel.GenRefLine.Delete(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves the names of all defined general reference lines.
        /// </summary>
        /// <param name="names">General reference line names retrieved by the program.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetNameList(out string[] names)
        {
            names = new string[0];
            _callCode = _sapModel.GenRefLine.GetNameList(ref _numberOfItems, ref names);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion

        #region Methods: Public
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function converts an existing general reference line to a new bridge layout line, with the ability to specify offset values.
        /// </summary>
        /// <param name="name">This is the name of an existing general reference line.</param>
        /// <param name="firstStation">The first station value on the bridge layout line. [L]</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the general reference line is offset to create the bridge layout line.</param>
        /// <param name="offsetX">The distance to offset the general reference line in the x-direction of the specified CSys, to the location of the new bridge layout line.</param>
        /// <param name="offsetY">The distance to offset the general reference line in the y-direction of the specified CSys, to the location of the new bridge layout line.</param>
        /// <param name="offsetZ">The distance to offset the general reference line in the z-direction of the specified CSys, to the location of the new bridge layout line.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ConvertLineToBridgeLayoutLine(string name,
            double firstStation = 0,
            string coordinateSystem = CoordinateSystems.Global,
            double offsetX = 0,
            double offsetY = 0,
            double offsetZ = 0)
        {
            _callCode = _sapModel.GenRefLine.ConvertLineToBLL(name, firstStation, coordinateSystem, offsetX, offsetY, offsetZ);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // === Get/Set ===

        /// <summary>
        /// Gets an existing reference line.
        /// </summary>
        /// <param name="name">The name of an existing general reference line.</param>
        /// <param name="discretizationLength">The maximum segment discretization length of the segments used to define curves in the general reference line. [L]</param>
        /// <param name="discretizationAngle">The maximum discretization angle in degrees for the general reference line. [deg]</param>
        /// <param name="color">The display color assigned to the general reference line.
        /// If Color is specified as -1, the program will automatically assign a color.</param>
        /// <param name="isVisible">The item is True if the general reference line should be displayed in windows displaying the model.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLine(string name,
            ref double discretizationLength,
            ref double discretizationAngle,
            ref int color,
            ref bool isVisible)
        {
            _callCode = _sapModel.GenRefLine.GetLine(name, ref discretizationLength, ref discretizationAngle, ref color, ref isVisible);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Adds a new line or modifies an existing reference line.
        /// </summary>
        /// <param name="name">This is the name of a general reference line.
        /// If this is the name of an existing general reference line, that general reference line is modified; otherwise a new general reference line is added.</param>
        /// <param name="discretizationLength">The maximum segment discretization length of the segments used to define curves in the general reference line. [L]</param>
        /// <param name="discretizationAngle">The maximum discretization angle in degrees for the general reference line. [deg]</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the general reference line is defined.</param>
        /// <param name="color">The display color assigned to the general reference line.
        /// If Color is specified as -1, the program will automatically assign a color.</param>
        /// <param name="isVisible">The item is True if the general reference line should be displayed in windows displaying the model.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLine(string name,
            double discretizationLength,
            double discretizationAngle,
            string coordinateSystem = CoordinateSystems.Global,
            int color = -1,
            bool isVisible = true)
        {
            _callCode = _sapModel.GenRefLine.SetLine(name, discretizationLength, discretizationAngle, coordinateSystem, color, isVisible);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===

        /// <summary>
        /// This function retrieves the general reference line elevation points and the associated parameters.
        /// </summary>
        /// <param name="name">The name of an existing general reference line.</param>
        /// <param name="numberPoints">The number of points used to define the general reference line elevation layout.</param>
        /// <param name="curveTypes">The general reference line elevation layout curve type for each point.</param>
        /// <param name="curveTypeValues1">This is the value of a parameter used to define the general reference line layout.
        /// What it represents depends on <paramref name="curveTypes" />.</param>
        /// <param name="curveTypeValues2">This is the value of a parameter used to define the general reference line layout.
        /// What it represents depends on <paramref name="curveTypes" />.</param>
        /// <param name="curveTypeValues3">This is the value of a parameter used to define the general reference line layout.
        /// What it represents depends on <paramref name="curveTypes" />.</param>
        /// <param name="stationCoordinates">Station coordinate of each point in the coordinate system specified for the general reference line. [L]</param>
        /// <param name="zCoordinates">This is an array of the Z coordinate of each point in the coordinate system specified for the general reference line. [L]</param>
        /// <param name="coordinateSystem">Coordinate system to use for the values retrieved.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLineElevationPoints(string name,
            ref int numberPoints,
            ref eLayoutCurveType[] curveTypes,
            ref double[] curveTypeValues1,
            ref double[] curveTypeValues2,
            ref double[] curveTypeValues3,
            ref double[] stationCoordinates,
            ref double[] zCoordinates,
            string coordinateSystem = CoordinateSystems.Global)
        {
            int[] csiCurveTypes = new int[0];

            _callCode = _sapModel.GenRefLine.GetLineElevPoints(name, ref numberPoints, ref csiCurveTypes, ref curveTypeValues1, ref curveTypeValues2, ref curveTypeValues3, ref stationCoordinates, ref zCoordinates, coordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            curveTypes = new eLayoutCurveType[csiCurveTypes.Length - 1];
            for (int i = 0; i < csiCurveTypes.Length; i++)
            {
                curveTypes[i] = (eLayoutCurveType)csiCurveTypes[i];
            }
        }

        /// <summary>
        /// This function assigns the general reference line elevation layout parameters.
        /// A minimum of three points is required for the Circular, Highway, and Parabolic curves.
        /// The Bezier curve requires a minimum of four points.
        /// The BSpline curve requires a minimum of two points.
        /// The Bezier and BSpline curve types require additional control points as specified by Value2.
        /// These control points are considered to be defined directly after the point specifying the Bezier or BSpline curve.
        /// Any Value1, Value2, or Value3 parameters defined on these control points are ignored.
        /// </summary>
        /// <param name="name">The name of a defined general reference line.</param>
        /// <param name="curveTypes">The general reference line elevation layout curve type for each point.</param>
        /// <param name="curveTypeValues1">This is the value of a parameter used to define the general reference line layout.
        /// What it represents depends on <paramref name="curveTypes" />.</param>
        /// <param name="curveTypeValues2">This is the value of a parameter used to define the general reference line layout.
        /// What it represents depends on <paramref name="curveTypes" />.</param>
        /// <param name="curveTypeValues3">This is the value of a parameter used to define the general reference line layout.
        /// What it represents depends on <paramref name="curveTypes" />.</param>
        /// <param name="stationCoordinates">Station coordinate of each point in the coordinate system specified for the general reference line. [L]</param>
        /// <param name="zCoordinates">This is an array of the Z coordinate of each point in the coordinate system specified for the general reference line. [L]</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLineElevationPoints(string name,
            eLayoutCurveType[] curveTypes,
            double[] curveTypeValues1,
            double[] curveTypeValues2,
            double[] curveTypeValues3,
            double[] stationCoordinates,
            double[] zCoordinates)
        {
            arraysLengthMatch(nameof(curveTypes), curveTypes.Length, nameof(curveTypeValues1), curveTypeValues1.Length);
            arraysLengthMatch(nameof(curveTypes), curveTypes.Length, nameof(curveTypeValues2), curveTypeValues2.Length);
            arraysLengthMatch(nameof(curveTypes), curveTypes.Length, nameof(curveTypeValues3), curveTypeValues3.Length);
            arraysLengthMatch(nameof(curveTypes), curveTypes.Length, nameof(stationCoordinates), stationCoordinates.Length);
            arraysLengthMatch(nameof(curveTypes), curveTypes.Length, nameof(zCoordinates), zCoordinates.Length);

            int[] csiCurveTypes = new int[curveTypes.Length - 1];
            for (int i = 0; i < curveTypes.Length; i++)
            {
                csiCurveTypes[i] = (int)curveTypes[i];
            }


            _callCode = _sapModel.GenRefLine.SetLineElevPoints(name, curveTypes.Length, ref csiCurveTypes, ref curveTypeValues1, ref curveTypeValues2, ref curveTypeValues3, ref stationCoordinates, ref zCoordinates);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===

        /// <summary>
        /// This function retrieves the general reference line plan points and the associated parameters.
        /// </summary>
        /// <param name="name">The name of an existing general reference line.</param>
        /// <param name="numberPoints">The number of points used to define the general reference line elevation layout.</param>
        /// <param name="curveTypes">The general reference line elevation layout curve type for each point.</param>
        /// <param name="curveTypeValues1">This is the value of a parameter used to define the general reference line layout.
        /// What it represents depends on <paramref name="curveTypes" />.</param>
        /// <param name="curveTypeValues2">This is the value of a parameter used to define the general reference line layout.
        /// What it represents depends on <paramref name="curveTypes" />.</param>
        /// <param name="curveTypeValues3">This is the value of a parameter used to define the general reference line layout.
        /// What it represents depends on <paramref name="curveTypes" />.</param>
        /// <param name="xCoordinates">X coordinate of each point in the coordinate system specified for the general reference line. [L]</param>
        /// <param name="yCoordinates">Y coordinate of each point in the coordinate system specified for the general reference line. [L]</param>
        /// <param name="coordinateSystem">Coordinate system to use for the values retrieved.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLinePlanPoints(string name,
            ref int numberPoints,
            ref eLayoutCurveType[] curveTypes,
            ref double[] curveTypeValues1,
            ref double[] curveTypeValues2,
            ref double[] curveTypeValues3,
            ref double[] xCoordinates,
            ref double[] yCoordinates,
            string coordinateSystem = CoordinateSystems.Global)
        {
            int[] csiCurveTypes = new int[0];

            _callCode = _sapModel.GenRefLine.GetLinePlanPoints(name, ref numberPoints, ref csiCurveTypes, ref curveTypeValues1, ref curveTypeValues2, ref curveTypeValues3, ref xCoordinates, ref yCoordinates, coordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            curveTypes = new eLayoutCurveType[csiCurveTypes.Length - 1];
            for (int i = 0; i < csiCurveTypes.Length; i++)
            {
                curveTypes[i] = (eLayoutCurveType)csiCurveTypes[i];
            }
        }

        /// <summary>
        /// This function assigns the general reference line plan layout parameters.
        /// A minimum of three points is required for the Circular, Highway, and Parabolic curves.
        /// The Bezier curve requires a minimum of four points.
        /// The BSpline curve requires a minimum of two points.
        /// The Bezier and BSpline curve types require additional control points as specified by Value2.
        /// These control points are considered to be defined directly after the point specifying the Bezier or BSpline curve.
        /// Any Value1, Value2, or Value3 parameters defined on these control points are ignored.
        /// </summary>
        /// <param name="name">The name of a defined general reference line.</param>
        /// <param name="curveTypes">The general reference line elevation layout curve type for each point.</param>
        /// <param name="curveTypeValues1">This is the value of a parameter used to define the general reference line layout.
        /// What it represents depends on <paramref name="curveTypes" />.</param>
        /// <param name="curveTypeValues2">This is the value of a parameter used to define the general reference line layout.
        /// What it represents depends on <paramref name="curveTypes" />.</param>
        /// <param name="curveTypeValues3">This is the value of a parameter used to define the general reference line layout.
        /// What it represents depends on <paramref name="curveTypes" />.</param>
        /// <param name="xCoordinates">X coordinate of each point in the coordinate system specified for the general reference line. [L]</param>
        /// <param name="yCoordinates">Y coordinate of each point in the coordinate system specified for the general reference line. [L]</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLinePlanPoints(string name,
            eLayoutCurveType[] curveTypes,
            double[] curveTypeValues1,
            double[] curveTypeValues2,
            double[] curveTypeValues3,
            double[] xCoordinates,
            double[] yCoordinates)
        {
            arraysLengthMatch(nameof(curveTypes), curveTypes.Length, nameof(curveTypeValues1), curveTypeValues1.Length);
            arraysLengthMatch(nameof(curveTypes), curveTypes.Length, nameof(curveTypeValues2), curveTypeValues2.Length);
            arraysLengthMatch(nameof(curveTypes), curveTypes.Length, nameof(curveTypeValues3), curveTypeValues3.Length);
            arraysLengthMatch(nameof(curveTypes), curveTypes.Length, nameof(xCoordinates), xCoordinates.Length);
            arraysLengthMatch(nameof(curveTypes), curveTypes.Length, nameof(yCoordinates), yCoordinates.Length);

            int[] csiCurveTypes = new int[curveTypes.Length - 1];
            for (int i = 0; i < curveTypes.Length; i++)
            {
                csiCurveTypes[i] = (int)curveTypes[i];
            }


            _callCode = _sapModel.GenRefLine.SetLinePlanPoints(name, curveTypes.Length, ref csiCurveTypes, ref curveTypeValues1, ref curveTypeValues2, ref curveTypeValues3, ref xCoordinates, ref yCoordinates);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
#endregion
    }
}
