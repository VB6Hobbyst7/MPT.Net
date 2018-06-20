// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 09-29-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="GridSystems.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Represents the grid systems in the application.
    /// </summary>
    public class GridSystems : CSiApiBase, IGridSystem
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
        /// Initializes a new instance of the <see cref="GridSystems"/> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public GridSystems(CSiApiSeed seed) : base(seed) { }
#endregion

#region Methods: Interface

        /// <summary>
        /// The function returns zero if the new name is successfully applied, otherwise it returns a nonzero value.
        /// Changing the name of the Global coordinate system will fail and return an error.
        /// </summary>
        /// <param name="name">The existing name of a defined coordinate system.</param>
        /// <param name="newName">The new name for the coordinate system.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="CSiReservedNameException"></exception>
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

            _callCode = _sapModel.GridSys.ChangeName(name, newName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// The function returns the number of defined coordinate systems.
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return _sapModel.GridSys.Count();
        }

        /// <summary>
        /// The function deletes the specified coordinate system. 
        /// Attempting to delete the Global coordinate system will fail and return an error.
        /// </summary>
        /// <param name="nameCoordinateSystem">The name of an existing coordinate system.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="CSiReservedNameException"></exception>
        public void Delete(string nameCoordinateSystem)
        {
            if (Global.CompareTo(nameCoordinateSystem) == 0)
            {
                throw new CSiReservedNameException("Cannot delete global grid system " + Global);
            }

            _callCode = _sapModel.GridSys.Delete(nameCoordinateSystem);
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
            _callCode = _sapModel.GridSys.GetNameList(ref _numberOfItems, ref coordinateSystemNames);
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
            _callCode = _sapModel.GridSys.GetTransformationMatrix(nameObject, ref directionCosines);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#endregion

#region Methods: Public
#if !BUILD_ETABS2015
        //TODO: Consider enum for eGridSystemType        
        /// <summary>
        /// Gets the type of the grid system.
        /// </summary>
        /// <param name="name">The name of the grid system.</param>
        /// <param name="gridSystemType">Type of the grid system.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetGridSystemType(string name,
            ref string gridSystemType)
        {
            _callCode = _sapModel.GridSys.GetGridSysType(name, ref gridSystemType);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        // === Get/Set ===

        /// <summary>
        /// Retrieves the translation and rotation of the specified coordinate system origin and axes in relation to the global coordinate system.
        /// </summary>
        /// <param name="nameCoordinateSystem">The name of an existing coordinate system.</param>
        /// <param name="xCoordinateOrigin">The global X coordinate of the origin of the coordinate system. [L]</param>
        /// <param name="yCoordinateOrigin">The global Y coordinate of the origin of the coordinate system. [L]</param>
        /// <param name="rzCoordinateOrigin">The rotation of an axis of the new coordinate system relative to the global coordinate system is defined as follows: 
        /// (1) Rotate the coordinate system about the positive global Z-axis as defined by the RZ item. [deg].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetGridSystem(string nameCoordinateSystem,
            ref double xCoordinateOrigin,
            ref double yCoordinateOrigin,
            ref double rzCoordinateOrigin)
        {
            _callCode = _sapModel.GridSys.GetGridSys(nameCoordinateSystem, ref xCoordinateOrigin, ref yCoordinateOrigin, ref rzCoordinateOrigin);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        //TODO: Consider enum for eGridSystemType     
        /// <summary>
        /// Sets the translation and rotation of the specified coordinate system origin and axes in relation to the global coordinate system.
        /// Modifying the Global coordinate system will fail and return an error.
        /// </summary>
        /// <param name="nameCoordinateSystem">The name of an existing coordinate system.</param>
        /// <param name="xCoordinateOrigin">The global X coordinate of the origin of the coordinate system. [L]</param>
        /// <param name="yCoordinateOrigin">The global Y coordinate of the origin of the coordinate system. [L]</param>
        /// <param name="rzCoordinateOrigin">The rotation of an axis of the new coordinate system relative to the global coordinate system is defined as follows: 
        /// (1) Rotate the coordinate system about the positive global Z-axis as defined by the RZ item. [deg].</param>
        /// <param name="gridSystemType">Type of the grid system.</param>
        /// <param name="numberOfXLines">Number of grid lines along the X-axis.</param>
        /// <param name="numberOfYLines">Number of grid lines along the Y-axis.</param>
        /// <param name="gridLineIdX">The ID of each grid line along the X-axis.</param>
        /// <param name="gridLineIdY">The ID of each grid line along the Y-axis.</param>
        /// <param name="ordinateX">The location of each grid line along the X-axis. [L]</param>
        /// <param name="ordinateY">The location of each grid line along the Y-axis. [L]</param>
        /// <param name="visibleX">The visibility of each grid line along the X-axis.</param>
        /// <param name="visibleY">The visibility of each grid line along the Y-axis.</param>
        /// <param name="bubbleLocationX">The bubble location of each grid line along the X-axis.</param>
        /// <param name="bubbleLocationY">The bubble location of each grid line along the Y-axis.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetGridSystem(string nameCoordinateSystem,
            ref double xCoordinateOrigin,
            ref double yCoordinateOrigin,
            ref double rzCoordinateOrigin,
            ref string gridSystemType,
            ref int numberOfXLines,
            ref string[] gridLineIdX,
            ref double[] ordinateX,
            ref bool[] visibleX,
            ref string[] bubbleLocationX,
            ref int numberOfYLines,
            ref string[] gridLineIdY,
            ref double[] ordinateY,
            ref bool[] visibleY,
            ref string[] bubbleLocationY)
        {
            _callCode = _sapModel.GridSys.GetGridSys_2(nameCoordinateSystem, ref xCoordinateOrigin, ref yCoordinateOrigin, ref rzCoordinateOrigin,
                            ref gridSystemType,
                            ref numberOfXLines, ref numberOfYLines,
                            ref gridLineIdX, ref gridLineIdY,
                            ref ordinateX, ref ordinateY,
                            ref visibleX, ref visibleY,
                            ref bubbleLocationX, ref bubbleLocationY);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Retrieves the translation and rotation of the specified coordinate system origin and axes in relation to the global coordinate system.
        /// </summary>
        /// <param name="nameCoordinateSystem">The name of an existing coordinate system.</param>
        /// <param name="xCoordinateOrigin">The global X coordinate of the origin of the coordinate system. [L]</param>
        /// <param name="yCoordinateOrigin">The global Y coordinate of the origin of the coordinate system. [L]</param>
        /// <param name="rzCoordinateOrigin">The rotation of an axis of the new coordinate system relative to the global coordinate system is defined as follows: 
        /// (1) Rotate the coordinate system about the positive global Z-axis as defined by the RZ item. [deg].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetGridSystem(string nameCoordinateSystem,
            ref double xCoordinateOrigin,
            ref double yCoordinateOrigin,
            ref double rzCoordinateOrigin)
        {
            _callCode = _sapModel.GridSys.SetGridSys(nameCoordinateSystem, xCoordinateOrigin, yCoordinateOrigin, rzCoordinateOrigin);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#if !BUILD_ETABS2015
        /// <summary>
        /// Sets the translation and rotation of the specified coordinate system origin and axes in relation to the global coordinate system.
        /// Modifying the Global coordinate system will fail and return an error.
        /// </summary>
        /// <param name="nameCoordinateSystem">The name of an existing coordinate system.</param>
        /// <param name="xCoordinateOrigin">The global X coordinate of the origin of the coordinate system. [L]</param>
        /// <param name="yCoordinateOrigin">The global Y coordinate of the origin of the coordinate system. [L]</param>
        /// <param name="rzCoordinateOrigin">The rotation of an axis of the new coordinate system relative to the global coordinate system is defined as follows: 
        /// (1) Rotate the coordinate system about the positive global Z-axis as defined by the RZ item. [deg].</param>
        /// <param name="storyRangeIsDefault"></param>
        /// <param name="topStory">Name of the top story.</param>
        /// <param name="bottomStory">Name of the bottom story.</param>
        /// <param name="bubbleSize">Size of the bubbles displayed. [L]</param>
        /// <param name="gridColor">Color of the grid lines.</param>
        /// <param name="numberOfXLines">Number of grid lines along the X-axis.</param>
        /// <param name="numberOfYLines">Number of grid lines along the Y-axis.</param>
        /// <param name="gridLineIdX">The ID of each grid line along the X-axis.</param>
        /// <param name="gridLineIdY">The ID of each grid line along the Y-axis.</param>
        /// <param name="ordinateX">The location of each grid line along the X-axis. [L]</param>
        /// <param name="ordinateY">The location of each grid line along the Y-axis. [L]</param>
        /// <param name="visibleX">The visibility of each grid line along the X-axis.</param>
        /// <param name="visibleY">The visibility of each grid line along the Y-axis.</param>
        /// <param name="bubbleLocationX">The bubble location of each grid line along the X-axis.</param>
        /// <param name="bubbleLocationY">The bubble location of each grid line along the Y-axis.</param>
        /// <param name="numberOfGeneralLines"></param>
        /// <param name="gridLineIdGeneral">Number of grid lines in the general system.</param>
        /// <param name="ordinateX1General">The x-axis coordinate of the start of a general system gridline.</param>
        /// <param name="ordinateY1General">The y-axis coordinate of the start of a general system gridline.</param>
        /// <param name="ordinateX2General">The x-axis coordinate of the end of a general system gridline.</param>
        /// <param name="ordinateY2General">The y-axis coordinate of the end of a general system gridline.</param>
        /// <param name="visibleGeneral">The visibility of each grid line in the general system.</param>
        /// <param name="bubbleLocationGeneral">The bubble location for each line in the general system.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetGridSystemCartesian(string nameCoordinateSystem,
            ref double xCoordinateOrigin,
            ref double yCoordinateOrigin,
            ref double rzCoordinateOrigin,
            ref bool storyRangeIsDefault,
            ref string topStory,
            ref string bottomStory,
            ref double bubbleSize,
            ref int gridColor,
            ref int numberOfXLines,
            ref string[] gridLineIdX,
            ref double[] ordinateX,
            ref bool[] visibleX,
            ref string[] bubbleLocationX,
            ref int numberOfYLines,
            ref string[] gridLineIdY,
            ref double[] ordinateY,
            ref bool[] visibleY,
            ref string[] bubbleLocationY,
            ref int numberOfGeneralLines,
            ref string[] gridLineIdGeneral,
            ref double[] ordinateX1General,
            ref double[] ordinateY1General,
            ref double[] ordinateX2General,
            ref double[] ordinateY2General,
            ref bool[] visibleGeneral,
            ref string[] bubbleLocationGeneral)
        {
            _callCode = _sapModel.GridSys.GetGridSysCartesian(nameCoordinateSystem, ref xCoordinateOrigin, ref yCoordinateOrigin, ref rzCoordinateOrigin,
                            ref storyRangeIsDefault, ref topStory, ref bottomStory,
                            ref bubbleSize, ref gridColor,
                            ref numberOfXLines, ref gridLineIdX, ref ordinateX, ref visibleX, ref bubbleLocationX,
                            ref numberOfYLines, ref gridLineIdY, ref ordinateY, ref visibleY, ref bubbleLocationY,
                            ref numberOfGeneralLines, ref gridLineIdGeneral, ref ordinateX1General, ref ordinateY1General, ref ordinateX2General, ref ordinateY2General, ref visibleGeneral, ref bubbleLocationGeneral);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Sets the translation and rotation of the specified coordinate system origin and axes in relation to the global coordinate system.
        /// Modifying the Global coordinate system will fail and return an error.
        /// </summary>
        /// <param name="nameCoordinateSystem">The name of an existing coordinate system.</param>
        /// <param name="xCoordinateOrigin">The global X coordinate of the origin of the coordinate system. [L]</param>
        /// <param name="yCoordinateOrigin">The global Y coordinate of the origin of the coordinate system. [L]</param>
        /// <param name="rzCoordinateOrigin">The rotation of an axis of the new coordinate system relative to the global coordinate system is defined as follows: 
        /// (1) Rotate the coordinate system about the positive global Z-axis as defined by the RZ item. [deg].</param>
        /// <param name="storyRangeIsDefault"></param>
        /// <param name="topStory">Name of the top story.</param>
        /// <param name="bottomStory">Name of the bottom story.</param>
        /// <param name="bubbleSize">Size of the bubbles displayed. [L]</param>
        /// <param name="gridColor">Color of the grid lines.</param>
        /// <param name="numberOfRadialLines">Number of grid lines along the radial-axis.</param>
        /// <param name="numberOfThetaLines">Number of grid lines along the theta-axis.</param>
        /// <param name="gridLineIdRadial">The ID of each grid line along the radial-axis.</param>
        /// <param name="gridLineIdTheta">The ID of each grid line along the theta-axis.</param>
        /// <param name="ordinateRadial">The location of each grid line along the radial-axis. [L]</param>
        /// <param name="ordinateTheta">The location of each grid line along the theta-axis. [L]</param>
        /// <param name="visibleRadial">The visibility of each grid line along the radial-axis.</param>
        /// <param name="visibleTheta">The visibility of each grid line along the theta-axis.</param>
        /// <param name="bubbleLocationRadial">The bubble location of each grid line along the radial-axis.</param>
        /// <param name="bubbleLocationTheta">The bubble location of each grid line along the theta-axis.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetGridSystemCylindrical(string nameCoordinateSystem,
            ref double xCoordinateOrigin,
            ref double yCoordinateOrigin,
            ref double rzCoordinateOrigin,
            ref bool storyRangeIsDefault,
            ref string topStory,
            ref string bottomStory,
            ref double bubbleSize,
            ref int gridColor,
            ref int numberOfRadialLines,
            ref string[] gridLineIdRadial,
            ref double[] ordinateRadial,
            ref bool[] visibleRadial,
            ref string[] bubbleLocationRadial,
            ref int numberOfThetaLines,
            ref string[] gridLineIdTheta,
            ref double[] ordinateTheta,
            ref bool[] visibleTheta,
            ref string[] bubbleLocationTheta)
        {
            _callCode = _sapModel.GridSys.GetGridSysCylindrical(nameCoordinateSystem, ref xCoordinateOrigin, ref yCoordinateOrigin, ref rzCoordinateOrigin,
                            ref storyRangeIsDefault, ref topStory, ref bottomStory,
                            ref bubbleSize, ref gridColor,
                            ref numberOfRadialLines, ref gridLineIdRadial, ref ordinateRadial, ref visibleRadial, ref bubbleLocationRadial,
                            ref numberOfThetaLines, ref gridLineIdTheta, ref ordinateTheta, ref visibleTheta, ref bubbleLocationTheta);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Returns the names and types of all grid systems.
        /// </summary>
        /// <param name="gridSystemNames">Name of each grid system.</param>
        /// <param name="gridSystemTypes">Type of each grid system.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetNameTypeList(ref string[] gridSystemNames,
            ref string[] gridSystemTypes
            )
        {
            _callCode = _sapModel.GridSys.GetNameTypeList(ref _numberOfItems, ref gridSystemNames, ref gridSystemTypes);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
#endregion
    }
}
#endif