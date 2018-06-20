// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-02-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eFrameDesignOrientation.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Design types available for areas in the application.
    /// </summary>
    public enum eFrameDesignOrientation
    {
        /// <summary>
        /// Design frame as a column.
        /// </summary>
        Column = 1,

        /// <summary>
        /// Design frame as a beam.
        /// </summary>
        Beam = 2,

        /// <summary>
        /// Design frame as a brace.
        /// </summary>
        Brace = 3,

        /// <summary>
        /// Frame is a null element, not to be designed.
        /// </summary>
        Null = 4,

        /// <summary>
        /// Frame is of another type.
        /// </summary>
        Other = 5 
    }
}
#endif