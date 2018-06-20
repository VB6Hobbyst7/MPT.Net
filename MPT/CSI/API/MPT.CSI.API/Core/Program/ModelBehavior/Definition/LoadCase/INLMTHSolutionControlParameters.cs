// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-20-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-28-2017
// ***********************************************************************
// <copyright file="INLMTHSolutionControlParameters.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Load case uses solution control parameters for nonlinear modal time history analysis.
    /// </summary>
    public interface INLMTHSolutionControlParameters
    {
        /// <summary>
        /// This function retrieves the solution control parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing nonlinear load case.</param>
        /// <param name="staticPeriod">The static period.</param>
        /// <param name="maxSubstepSize">The maximum substep size.</param>
        /// <param name="minSubstepSize">The minimum substep size.</param>
        /// <param name="relativeForceConvergenceTolerance">The relative force convergence tolerance.</param>
        /// <param name="relativeEnergyConvergenceTolerance">The relative energy convergence tolerance.</param>
        /// <param name="maxIterationLimit">The maximum iteration limit.</param>
        /// <param name="minIterationLimit">The minimum iteration limit.</param>
        /// <param name="convergenceFactor">The convergence factor.</param>
        void GetSolutionControlParameters(string name,
            ref double staticPeriod,
            ref double maxSubstepSize,
            ref double minSubstepSize,
            ref double relativeForceConvergenceTolerance,
            ref double relativeEnergyConvergenceTolerance,
            ref int maxIterationLimit,
            ref int minIterationLimit,
            ref double convergenceFactor);

        /// <summary>
        /// This function sets the solution control parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing nonlinear load case.</param>
        /// <param name="staticPeriod">The static period.</param>
        /// <param name="maxSubstepSize">The maximum substep size.</param>
        /// <param name="minSubstepSize">The minimum substep size.</param>
        /// <param name="relativeForceConvergenceTolerance">The relative force convergence tolerance.</param>
        /// <param name="relativeEnergyConvergenceTolerance">The relative energy convergence tolerance.</param>
        /// <param name="maxIterationLimit">The maximum iteration limit.</param>
        /// <param name="minIterationLimit">The minimum iteration limit.</param>
        /// <param name="convergenceFactor">The convergence factor.</param>
        void SetSolutionControlParameters(string name,
            double staticPeriod,
            double maxSubstepSize,
            double minSubstepSize,
            double relativeForceConvergenceTolerance,
            double relativeEnergyConvergenceTolerance,
            int maxIterationLimit,
            int minIterationLimit,
            double convergenceFactor);
    }
}
#endif