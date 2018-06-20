// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-02-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eWallPierRebarLayerType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Wall pier rebar layer types available in the application.
    /// </summary>
    public enum eWallPierRebarLayerType
    {
        /// <summary>
        /// Vertical, distributed, in the middle zone of each face.
        /// </summary>
        VerticalDistributedMiddleZoneEachface = 1,

        /// <summary>
        /// Horizontal, distributed, in the middle zone of each face.
        /// </summary>
        HorizontalDistributedMiddleZoneEachface = 2,

        /// <summary>
        /// Vertical, distributed, total rebar in the end zone I.
        /// </summary>
        VerticalDistributedEndZoneITotal = 3,

        /// <summary>
        /// Vertical, distributed, total rebar in the end zone J.
        /// </summary>
        VerticalDistributedEndZoneJTotal = 4,

        /// <summary>
        /// The confinement in end zone I.
        /// </summary>
        ConfinementEndZoneI = 5,

        /// <summary>
        /// The confinement in end zone J.
        /// </summary>
        ConfinementEndZoneJ = 6,

        /// <summary>
        /// Each rebar is diagonal.
        /// </summary>
        DiagonalEach = 7
    }
}
#endif