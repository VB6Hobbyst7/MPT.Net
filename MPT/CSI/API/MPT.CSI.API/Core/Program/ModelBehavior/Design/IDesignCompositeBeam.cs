// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-03-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="IDesignCompositeBeam.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design
{
    /// <summary>
    /// Implements a design interface for all composite beam-based frame elements.
    /// </summary>
    public interface IDesignCompositeBeam : IDesignCode, IResettable, IComboStrength, IComboDeflection, IAutoSection
    {
        /// <summary>
        /// Retrieves summary results for frame design.
        /// </summary>
        /// <param name="name">The name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <param name="designSections">The design sections.</param>
        /// <param name="beamFy">The beam steel yield strengths, Fy. [F/L^2].</param>
        /// <param name="studDiameters">The stud diameters. [L].</param>
        /// <param name="studLayouts">The stud layouts.</param>
        /// <param name="isBeamShored">True: The is beam shored.</param>
        /// <param name="beamCambers">The beam cambers. [L]</param>
        /// <param name="passFail">The pass fail design status.</param>
        /// <param name="reactionsLeft">The left support reactions.</param>
        /// <param name="reactionsRight">The right support reactions.</param>
        /// <param name="MMaxNegative">The maximum negative moment.</param>
        /// <param name="MMaxPositive">The maximum positive moment.</param>
        /// <param name="percentCompositeConnection">The percent composite connection (PCC).</param>
        /// <param name="overallRatios">The overall ratios.</param>
        /// <param name="studRatios">The stud ratios.</param>
        /// <param name="strengthRatiosPM">The strength ratios considering PM (Axial &amp; Bending).</param>
        /// <param name="constructionRatiosPM">The construction ratios considering PM (Axial &amp; Bending).</param>
        /// <param name="strengthShearRatios">The strength shear ratios.</param>
        /// <param name="constructionShearRatios">The construction shear ratios.</param>
        /// <param name="deflectionRatiosPostConcreteDL">The deflection ratios post-concrete, DL (Dead Load).</param>
        /// <param name="deflectionRatiosSDL">The deflection ratios, SDL (Specified Dead Load).</param>
        /// <param name="deflectionRatiosLL">The deflection ratios, LL (Live Load).</param>
        /// <param name="deflectionRatiosTotalCamber">The deflection ratios from total camber.</param>
        /// <param name="frequencyRatios">The walking frequency ratios.</param>
        /// <param name="MDampingRatios">The M damping ratios.</param>
        /// <param name="itemType">This is one of the following items in the eItemType enumeration: Object = 0, Group = 1, SelectedObjects = 2
        /// If this item is Object, the design results are retrieved for the frame object specified by the Name item.
        /// If this item is Group, the design results are retrieved for all frame objects in the group specified by the Name item.
        /// If this item is SelectedObjects, the design results are retrieved for all selected frame objects, and the Name item is ignored.</param>
        void GetSummaryResults(string name,
            ref string[] designSections,
            ref double[] beamFy,
            ref double[] studDiameters,
            ref string[] studLayouts,
            ref bool[] isBeamShored,
            ref double[] beamCambers,
            ref string[] passFail,
            ref double[] reactionsLeft,
            ref double[] reactionsRight,
            ref double[] MMaxNegative,
            ref double[] MMaxPositive,
            ref double[] percentCompositeConnection,
            ref double[] overallRatios,
            ref double[] studRatios,
            ref double[] strengthRatiosPM,
            ref double[] constructionRatiosPM,
            ref double[] strengthShearRatios,
            ref double[] constructionShearRatios,
            ref double[] deflectionRatiosPostConcreteDL,
            ref double[] deflectionRatiosSDL,
            ref double[] deflectionRatiosLL,
            ref double[] deflectionRatiosTotalCamber,
            ref double[] frequencyRatios,
            ref double[] MDampingRatios,
            eItemType itemType = eItemType.Object);
    }
}
#endif