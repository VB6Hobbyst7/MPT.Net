// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 10-06-2017
//
// Last Modified By : Mark
// Last Modified On : 10-06-2017
// ***********************************************************************
// <copyright file="IAnalysisResultsSetup.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Helpers;

namespace MPT.CSI.API.Core.Program.ModelBehavior.AnalysisResult
{
    /// <summary>
    /// Implements the representation of the analysis results setup in the application.
    /// </summary>
    public interface IAnalysisResultsSetup
    {
        /// <summary>
        /// The function deselects all load cases and response combinations for output.
        /// </summary>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void DeselectAllCasesAndCombosForOutput();
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function selects or deselects all section cuts for output.
        /// Please note that all section cuts are, by default, selected for output when they are created.
        /// </summary>
        /// <param name="selected">if set to <c>true</c> [selected].</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void SelectAllSectionCutsForOutput(bool selected);
#endif



        /// <summary>
        /// Gets the case selected for output.
        /// </summary>
        /// <param name="name">The name of an existing load case.</param>
        /// <param name="selected">True: Specified load case is to be selected for output.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void GetCaseSelectedForOutput(string name,
            ref bool selected);

        /// <summary>
        /// Sets the case selected for output.
        /// </summary>
        /// <param name="name">The name of an existing load case.</param>
        /// <param name="selected">True: Specified load case is to be selected for output.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void SetCaseSelectedForOutput(string name,
            bool selected = true);




        /// <summary>
        /// This function checks if a load combination is selected for output.
        /// </summary>
        /// <param name="name">The name of an existing load combination.</param>
        /// <param name="selected">True: Specified load combination is to be selected for output.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void GetComboSelectedForOutput(string name,
            ref bool selected);

        /// <summary>
        /// This function sets a load combination selected for output flag.
        /// </summary>
        /// <param name="name">The name of an existing load combination.</param>
        /// <param name="selected">True: Specified load combination is to be selected for output.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void SetComboSelectedForOutput(string name,
            bool selected = true);




        /// <summary>
        /// This function retrieves the global coordinates of the location at which the base reactions are reported.
        /// </summary>
        /// <param name="coordinates">The global coordinates of the location at which the base reactions are reported.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void GetOptionBaseReactLocation(ref Coordinate3DCartesian coordinates);

        /// <summary>
        /// This function sets the global coordinates of the location at which the base reactions are reported.
        /// </summary>
        /// <param name="coordinates">The global coordinates of the location at which the base reactions are reported.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void SetOptionBaseReactLocation(Coordinate3DCartesian coordinates);




        /// <summary>
        /// This function retrieves the buckling modes for which buckling factors are reported.
        /// </summary>
        /// <param name="buckleModeStart">The first buckling mode for which the buckling factor is reported when the <paramref name="buckleModeAll" /> item is False.</param>
        /// <param name="buckleModeEnd">The last buckling mode for which the buckling factor is reported when the <paramref name="buckleModeAll" /> item is False.</param>
        /// <param name="buckleModeAll">True: Buckling factors are reported for all calculated buckling modes.
        /// False: Buckling factors are reported for the buckling modes indicated by the <paramref name="buckleModeStart" /> and <paramref name="buckleModeEnd" /> items.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void GetOptionBucklingMode(ref int buckleModeStart,
            ref int buckleModeEnd,
            ref bool buckleModeAll);

        /// <summary>
        /// This function sets the buckling modes for which buckling factors are reported.
        /// </summary>
        /// <param name="buckleModeStart">The first buckling mode for which the buckling factor is reported when the <paramref name="buckleModeAll" /> item is False.</param>
        /// <param name="buckleModeEnd">The last buckling mode for which the buckling factor is reported when the <paramref name="buckleModeAll" /> item is False.</param>
        /// <param name="buckleModeAll">True: Buckling factors are reported for all calculated buckling modes.
        /// False: Buckling factors are reported for the buckling modes indicated by the <paramref name="buckleModeStart" /> and <paramref name="buckleModeEnd" /> items.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void SetOptionBucklingMode(int buckleModeStart,
            int buckleModeEnd,
            bool buckleModeAll);




        /// <summary>
        /// This function retrieves the modes for which mode shapes are reported.
        /// </summary>
        /// <param name="modeShapeStart">The first mode for which the buckling factor is reported when the <paramref name="modeShapesAll" /> item is False.</param>
        /// <param name="modeShapeEnd">The last mode for which the buckling factor is reported when the <paramref name="modeShapesAll" /> item is False.</param>
        /// <param name="modeShapesAll">True: Results are reported for all calculated modes.
        /// False: Results are reported for the modes indicated by the <paramref name="modeShapeStart" /> and <paramref name="modeShapeEnd" /> items.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void GetOptionModeShape(ref int modeShapeStart,
            ref int modeShapeEnd,
            ref bool modeShapesAll);

        /// <summary>
        /// This function sets the modes for which mode shapes are reported.
        /// </summary>
        /// <param name="modeShapeStart">The first mode for which the buckling factor is reported when the <paramref name="modeShapesAll" /> item is False.</param>
        /// <param name="modeShapeEnd">The last mode for which the buckling factor is reported when the <paramref name="modeShapesAll" /> item is False.</param>
        /// <param name="modeShapesAll">True: Results are reported for all calculated modes.
        /// False: Results are reported for the modes indicated by the <paramref name="modeShapeStart" /> and <paramref name="modeShapeEnd" /> items.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void SetOptionModeShape(int modeShapeStart,
            int modeShapeEnd,
            bool modeShapesAll);



        /// <summary>
        /// This function retrieves the output option for direct history results.
        /// </summary>
        /// <param name="outputOption">The output option.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void GetOptionDirectHistory(ref eAnalysisMultiStepOptions outputOption);

        /// <summary>
        /// This function sets the output option for direct history results.
        /// </summary>
        /// <param name="outputOption">The output option.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void SetOptionDirectHistory(eAnalysisMultiStepOptions outputOption);



        /// <summary>
        /// This function retrieves the output option for multistep static linear results.
        /// </summary>
        /// <param name="outputOption">The output option.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void GetOptionMultiStepStatic(ref eAnalysisMultiStepOptions outputOption);

        /// <summary>
        /// This function sets the output option for multistep static linear results..
        /// </summary>
        /// <param name="outputOption">The output option.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void SetOptionMultiStepStatic(eAnalysisMultiStepOptions outputOption);




        /// <summary>
        /// This function retrieves the output option for modal history results.
        /// </summary>
        /// <param name="outputOption">The output option.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void GetOptionModalHistory(ref eAnalysisMultiStepOptions outputOption);

        /// <summary>
        /// This function sets the output option for modal history results.
        /// </summary>
        /// <param name="outputOption">The output option.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void SetOptionModalHistory(eAnalysisMultiStepOptions outputOption);



        /// <summary>
        /// This function retrieves the output option for nonlinear static results.
        /// </summary>
        /// <param name="outputOption">The output option.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void GetOptionNLStatic(ref eAnalysisMultiStepOptions outputOption);

        /// <summary>
        /// This function sets the output option for nonlinear static results.
        /// </summary>
        /// <param name="outputOption">The output option.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void SetOptionNLStatic(eAnalysisMultiStepOptions outputOption);


        /// <summary>
        /// This function retrieves the output option for multi-valued load combination results.
        /// </summary>
        /// <param name="outputOption">The output option.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void GetOptionMultiValuedCombo(ref eAnalysisMultiValuedOptions outputOption);

        /// <summary>
        /// This function sets the output option for multi-valued load combination results.
        /// </summary>
        /// <param name="outputOption">The output option.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void SetOptionMultiValuedCombo(eAnalysisMultiValuedOptions outputOption);



#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function retrieves the output option for power spectral density results.
        /// </summary>
        /// <param name="outputOption">The output option.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void GetOptionPSD(ref eAnalysisPSDOptions outputOption);

        /// <summary>
        /// This function sets the output option for power spectral density results.
        /// </summary>
        /// <param name="outputOption">The output option.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void SetOptionPSD(eAnalysisPSDOptions outputOption);




        /// <summary>
        /// This function gets the output option for steady state results.
        /// </summary>
        /// <param name="outputOption">The output option.</param>
        /// <param name="steadyStateOption">The steady state option.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void GetOptionSteadyState(ref eAnalysisSteadyStateOptions outputOption,
            ref eSteadyStateOptions steadyStateOption);

        /// <summary>
        /// This function sets the output option for steady state results.
        /// </summary>
        /// <param name="outputOption">The output option.</param>
        /// <param name="steadyStateOption">The steady state option.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        void SetOptionSteadyState(eAnalysisSteadyStateOptions outputOption,
            eSteadyStateOptions steadyStateOption);
#endif
    }
}