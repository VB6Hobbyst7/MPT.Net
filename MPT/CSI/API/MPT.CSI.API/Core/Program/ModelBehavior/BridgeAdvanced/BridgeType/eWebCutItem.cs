// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-21-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-28-2017
// ***********************************************************************
// <copyright file="eWebCutItem.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_CSiBridgev18 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
namespace MPT.CSI.API.Core.Program.ModelBehavior.BridgeAdvanced.BridgeType
{
    /// <summary>
    /// Superstructure web cut items available in the application.
    /// </summary>
    public enum eWebCutItem
    {
        /// <summary>
        /// Angle from vertical (Y) axis, clockwise is positive.
        /// </summary>
        AngleFromVertical = 1,

        /// <summary>
        /// Minimum horizontal (X) web thickness.
        /// </summary>
        MinHorizontalWebThickness = 2,

        /// <summary>
        /// Minimum top slab thickness above cell to left of web.
        /// </summary>
        MinTopSlabThickness = 3,

        /// <summary>
        /// Minimum bottom slab thickness above cell to left of web.
        /// </summary>
        MinBottomSlabThickness = 4,

        /// <summary>
        /// Top width of cell to left of web measured from centerline of girders on each side of cell.
        /// </summary>
        TopWidthOfCell = 5,

        /// <summary>
        /// Bottom width of cell to left of web measured from centerline of girders on each side of cell.
        /// </summary>
        BottomWidthOfCell = 6
    }
}
#endif