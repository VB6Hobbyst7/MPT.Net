// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="IGridSystem.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Implements the grid systems in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableTransformationMatrix" />
    public interface IGridSystem: IChangeableName, ICountable, IDeletable, IListableNames, IObservableTransformationMatrix
    {
#region Methods: Public
#if !BUILD_ETABS2015
        //TODO: Consider enum for eGridSystemType        
        /// <summary>
        /// Gets the type of the grid system.
        /// </summary>
        /// <param name="name">The name of the grid system.</param>
        /// <param name="gridSystemType">Type of the grid system.</param>
        void GetGridSystemType(string name,
            ref string gridSystemType);
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
        void GetGridSystem(string nameCoordinateSystem,
            ref double xCoordinateOrigin,
            ref double yCoordinateOrigin,
            ref double rzCoordinateOrigin);

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
        void GetGridSystem(string nameCoordinateSystem,
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
            ref string[] bubbleLocationY);


        /// <summary>
        /// Retrieves the translation and rotation of the specified coordinate system origin and axes in relation to the global coordinate system.
        /// </summary>
        /// <param name="nameCoordinateSystem">The name of an existing coordinate system.</param>
        /// <param name="xCoordinateOrigin">The global X coordinate of the origin of the coordinate system. [L]</param>
        /// <param name="yCoordinateOrigin">The global Y coordinate of the origin of the coordinate system. [L]</param>
        /// <param name="rzCoordinateOrigin">The rotation of an axis of the new coordinate system relative to the global coordinate system is defined as follows: 
        /// (1) Rotate the coordinate system about the positive global Z-axis as defined by the RZ item. [deg].</param>
        void SetGridSystem(string nameCoordinateSystem,
            ref double xCoordinateOrigin,
            ref double yCoordinateOrigin,
            ref double rzCoordinateOrigin);

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
        void GetGridSystemCartesian(string nameCoordinateSystem,
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
            ref string[] bubbleLocationGeneral);


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
        void GetGridSystemCylindrical(string nameCoordinateSystem,
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
            ref string[] bubbleLocationTheta);

        /// <summary>
        /// Returns the names and types of all grid systems.
        /// </summary>
        /// <param name="gridSystemNames">Name of each grid system.</param>
        /// <param name="gridSystemTypes">Type of each grid system.</param>
        void GetNameTypeList(ref string[] gridSystemNames,
            ref string[] gridSystemTypes
        );
#endif
        #endregion
    }
}
#endif