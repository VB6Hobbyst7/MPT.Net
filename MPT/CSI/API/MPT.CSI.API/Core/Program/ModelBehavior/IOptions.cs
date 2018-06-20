// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="IOptions.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Implements model and application options in the application.
    /// </summary>
    public interface IOptions
    {
        #region Methods: Public
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function retrieves the program dimension and tolerance items.
        /// </summary>
        /// <param name="cuttingPlaneTol">The tolerance for 2D view cutting planes. [L]</param>
        /// <param name="worldSpacing">The plan fine grid spacing. [L]</param>
        /// <param name="nudgeValue">The plan nudge value. [L]</param>
        /// <param name="pixelClickSize">The screen selection tolerance in pixels.</param>
        /// <param name="pixelSnapSize">The screen snap tolerance in pixels.</param>
        /// <param name="screenLineThickness">The screen line thickness in pixels.</param>
        /// <param name="printLineThickness">The printer line thickness in pixels.</param>
        /// <param name="maxFont">The maximum graphic font size in points.</param>
        /// <param name="minFont">The minimum graphic font size in points.</param>
        /// <param name="zoomStep">The auto zoom step size in percent (0 &lt; ZoomStep &lt;= 100).</param>
        /// <param name="shrinkFactor">The shrink factor in percent (0 &lt; ShrinkFact &lt;= 100).</param>
        /// <param name="textFileMaxChar">The maximum line length in the text file (ShrinkFact &gt;= 80).</param>
        void GetDimensions(ref double cuttingPlaneTol,
            ref double worldSpacing,
            ref double nudgeValue,
            ref int pixelClickSize,
            ref int pixelSnapSize,
            ref int screenLineThickness,
            ref int printLineThickness,
            ref int maxFont,
            ref int minFont,
            ref int zoomStep,
            ref int shrinkFactor,
            ref int textFileMaxChar);

        /// <summary>
        /// This function sets program dimension and tolerance items.
        /// Inputting 0 for any item means that the item will be ignored by the program; that is, its current value will not be changed.
        /// </summary>
        /// <param name="cuttingPlaneTol">The tolerance for 2D view cutting planes. [L]</param>
        /// <param name="worldSpacing">The plan fine grid spacing. [L]</param>
        /// <param name="nudgeValue">The plan nudge value. [L]</param>
        /// <param name="pixelClickSize">The screen selection tolerance in pixels.</param>
        /// <param name="pixelSnapSize">The screen snap tolerance in pixels.</param>
        /// <param name="screenLineThickness">The screen line thickness in pixels.</param>
        /// <param name="printLineThickness">The printer line thickness in pixels.</param>
        /// <param name="maxFont">The maximum graphic font size in points.</param>
        /// <param name="minFont">The minimum graphic font size in points.</param>
        /// <param name="zoomStep">The auto zoom step size in percent (0 &lt; ZoomStep &lt;= 100).</param>
        /// <param name="shrinkFactor">The shrink factor in percent (0 &lt; ShrinkFact &lt;= 100).</param>
        /// <param name="textFileMaxChar">The maximum line length in the text file (ShrinkFact &gt;= 80).</param>
        void SetDimensions(double cuttingPlaneTol,
            double worldSpacing,
            double nudgeValue,
            int pixelClickSize,
            int pixelSnapSize,
            int screenLineThickness,
            int printLineThickness,
            int maxFont,
            int minFont,
            int zoomStep,
            int shrinkFactor,
            int textFileMaxChar);
#endif

        #endregion
    }
}