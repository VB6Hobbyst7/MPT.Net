// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-21-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-28-2017
// ***********************************************************************
// <copyright file="eCutStatus.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_CSiBridgev18 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
namespace MPT.CSI.API.Core.Program.ModelBehavior.BridgeAdvanced.BridgeType
{
    /// <summary>
    /// Status available for bridge superstructure cuts.
    /// </summary>
    public enum eCutStatus
    {
        /// <summary>
        /// No portion of the section is cut.
        /// </summary>
        NoCut = 0,

        /// <summary>
        /// Only the section is cut; no interior cell is cut.
        /// </summary>
        OnlySectionCut = 1,

        /// <summary>
        /// The section and an interior cell are cut.
        /// </summary>
        SectionAndInteriorCellCut = 2
    }
}
#endif