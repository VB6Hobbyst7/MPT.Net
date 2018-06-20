// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-19-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-28-2017
// ***********************************************************************
// <copyright file="INLTHSolutionControlParameters.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Load case uses solution control parameters for nonlinear time history analysis.
    /// </summary>
    public interface INLTHSolutionControlParameters
    {
        /// <summary>
        /// This function retrieves the solution control parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing nonlinear load case.</param>
        /// <param name="maxSubstepSize">Maximum size of the substep.</param>
        /// <param name="minSubstepSize">Minimum size of the substep.</param>
        /// <param name="maxConstantStiffnessIterationsPerStep">The maximum constant stiffness iterations per step.</param>
        /// <param name="maxNewtonRaphsonIterationsPerStep">The maximum Newton-Raphson iterations per step.</param>
        /// <param name="relativeIterationConvergenceTolerance">The relative iteration convergence tolerance.</param>
        /// <param name="useEventStepping">True: Event-to-event stepping is used.</param>
        /// <param name="relativeEventLumpingTolerance">The relative event lumping tolerance.</param>
        /// <param name="maxNumberLineSearches">The maximum number of line-searches per iteration.</param>
        /// <param name="relativeLineSearchAcceptanceTolerance">The relative line-search acceptance tolerance.</param>
        /// <param name="lineSearchStepFactor">The line-search step factor.</param>
        void GetSolutionControlParameters(string name,
            ref double maxSubstepSize,
            ref double minSubstepSize,
            ref int maxConstantStiffnessIterationsPerStep,
            ref int maxNewtonRaphsonIterationsPerStep,
            ref double relativeIterationConvergenceTolerance,
            ref bool useEventStepping,
            ref double relativeEventLumpingTolerance,
            ref int maxNumberLineSearches,
            ref double relativeLineSearchAcceptanceTolerance,
            ref double lineSearchStepFactor);

        /// <summary>
        /// This function sets the solution control parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing nonlinear load case.</param>
        /// <param name="maxSubstepSize">Maximum size of the substep.</param>
        /// <param name="minSubstepSize">Minimum size of the substep.</param>
        /// <param name="maxConstantStiffnessIterationsPerStep">The maximum constant stiffness iterations per step.</param>
        /// <param name="maxNewtonRaphsonIterationsPerStep">The maximum Newton-Raphson iterations per step.</param>
        /// <param name="relativeIterationConvergenceTolerance">The relative iteration convergence tolerance.</param>
        /// <param name="useEventStepping">True: Event-to-event stepping is used.</param>
        /// <param name="relativeEventLumpingTolerance">The relative event lumping tolerance.</param>
        /// <param name="maxNumberLineSearches">The maximum number of line-searches per iteration.</param>
        /// <param name="relativeLineSearchAcceptanceTolerance">The relative line-search acceptance tolerance.</param>
        /// <param name="lineSearchStepFactor">The line-search step factor.</param>
        void SetSolutionControlParameters(string name,
            double maxSubstepSize,
            double minSubstepSize,
            int maxConstantStiffnessIterationsPerStep,
            int maxNewtonRaphsonIterationsPerStep,
            double relativeIterationConvergenceTolerance,
            bool useEventStepping,
            double relativeEventLumpingTolerance,
            int maxNumberLineSearches,
            double relativeLineSearchAcceptanceTolerance,
            double lineSearchStepFactor);
    }
}
#endif