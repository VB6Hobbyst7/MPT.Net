// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-21-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-28-2017
// ***********************************************************************
// <copyright file="eSectionCutItem.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_CSiBridgev18 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
namespace MPT.CSI.API.Core.Program.ModelBehavior.BridgeAdvanced.BridgeType
{
    /// <summary>
    /// Superstructure section cut items available in the application.
    /// </summary>
    public enum eSectionCutItem
    {
        /// <summary>
        /// Number of girders or webs.
        /// </summary>
        NumberOfGirdersOrWebs = 1,

        /// <summary>
        /// Design area of top slab, ASlabTop.
        /// </summary>
        DesignAreaOfTopSlab = 2,

        /// <summary>
        /// Design area of bottom slab, ASlabBot.
        /// </summary>
        DesignAreaOfBottomSlab = 3,

        /// <summary>
        /// Width of top slab, BSlabTop.
        /// </summary>
        WidthOfTopSlab = 4,

        /// <summary>
        /// Width of bottom slab, BSlabBot.
        /// </summary>
        WidthOfBottomSlab = 5,

        /// <summary>
        /// X coordinate of top slab centroid, XSlabTop.
        /// </summary>
        XCoordinateOfTopSlabCentroid = 6,

        /// <summary>
        /// X coordinate of bottom slab centroid, XSlabBot.
        /// </summary>
        XCoordinateOfBottomSlabCentroid = 7,

        /// <summary>
        /// Y coordinate of top slab centroid, YSlabTop.
        /// </summary>
        YCoordinateOfTopSlabCentroid = 8,

        /// <summary>
        /// Y coordinate of bottom slab centroid, YSlabBot.
        /// </summary>
        YCoordinateOfBottomSlabCentroid = 9,

        /// <summary>
        /// Area inside torsion circuit, ATorsion.
        /// </summary>
        AreaInsideTorsionCircuit = 10,

        /// <summary>
        /// Length of torsion circuit, LTorsion.
        /// </summary>
        LengthOfTorsionCircuit = 11,

        /// <summary>
        /// Number of tendons.
        /// </summary>
        NumberOfTendons = 12,

        /// <summary>
        /// Top outside width of torsion circuit, BTorsionTop.
        /// </summary>
        TopOutsideWidthOfTorsionCircuit = 13,

        /// <summary>
        /// Bottom outside width of torsion circuit, BTorsionBot.
        /// </summary>
        BottomOutsideWidthOfTorsionCircuit = 14,

        /// <summary>
        /// Left outside length of torsion circuit (along slope), HTorsionLeft.
        /// </summary>
        LeftOutsideLengthOfTorsionCircuit = 15,

        /// <summary>
        /// Right outside length of torsion circuit (along slope), HTorsionRight.
        /// </summary>
        RightOutsideLengthOfTorsionCircuit = 16
    }
}
#endif