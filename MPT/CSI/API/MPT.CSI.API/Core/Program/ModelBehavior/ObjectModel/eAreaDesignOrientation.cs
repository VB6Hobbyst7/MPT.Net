// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-02-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eAreaDesignOrientation.cs" company="">
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
    public enum eAreaDesignOrientation
    {
        /// <summary>
        /// Design area as a wall.
        /// </summary>
        Wall = 1,

        /// <summary>
        /// Design area as a floor.
        /// </summary>
        Floor = 2,

        ///// <summary>
        ///// Design area as a ramp.
        ///// </summary>
        //Ramp_DO_NOT_USE = 3,

        /// <summary>
        /// Area is a null element, not to be designed.
        /// </summary>
        Null = 4,

        /// <summary>
        /// Area is of another type.
        /// </summary>
        Other = 5
    }
}
#endif