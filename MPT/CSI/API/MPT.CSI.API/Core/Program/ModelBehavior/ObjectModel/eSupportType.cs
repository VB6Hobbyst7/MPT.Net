// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-02-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eSupportType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
using System.ComponentModel;

namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Support types available in the application.
    /// </summary>
    public enum eSupportType
    {
        /// <summary>
        /// Point Object.
        /// </summary>
        [Description("Point Object")]
        PointObject = 1,

        /// <summary>
        /// Frame Object.
        /// </summary>
        [Description("Frame Object")]
        FrameObject = 2,

        /// <summary>
        /// Area Object.
        /// </summary>
        [Description("Area Object")]
        AreaObject = 3,

        /// <summary>
        /// Solid bject.
        /// </summary>
        [Description("Solid Object")]
        SolidObject = 6
    }
}
#endif