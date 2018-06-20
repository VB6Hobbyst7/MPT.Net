// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="IAnalyzer.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Helpers;

namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Implements the analysis controls in the application.
    /// </summary>
    public interface IAnalyzer
    {
        #region Methods: Public

        /// <summary>
        /// Creates the analysis model.
        /// If the analysis model is already created and current, nothing is done.
        /// It is not necessary to call this function before running an analysis.
        /// The analysis model is automatically created, if necessary, when the model is run.
        /// </summary>
        void CreateAnalysisModel();

        /// <summary>
        /// Runs the analysis.
        /// The analysis model is automatically created as part of this function.
        /// </summary>
        /// <param name="filePath">Path to the model file.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool RunAnalysis(string filePath);

        /// <summary>
        /// Deletes results for the specified load case.
        /// </summary>
        /// <param name="nameLoadCase">The name of an existing load case that is to have its results deleted.
        /// This item is ignored when the 'All' item is 'True'.</param>
        /// <param name="deleteAll">True: Results are deleted for all load cases, and the Name item is ignored.</param>
        void DeleteResults(string nameLoadCase,
            bool deleteAll = false);

        /// <summary>
        /// Modifies the undeformed geometry based on displacements obtained from a specified load case.
        /// </summary>
        /// <param name="caseName">The name of the static load case from which displacements are obtained.</param>
        /// <param name="scaleFactor">The scale factor applied to the displacements.</param>
        /// <param name="stageOfStageConstruction">This item applies only when the specified load case is a staged construction load case.
        /// It is the stage number from which the displacements are obtained.
        /// Specifying a -1 for this item means to use the last run stage.</param>
        /// <param name="resetToOriginal">True: All other input items in this function are ignored and the original undeformed geometry data is reinstated.</param>
        void ModifyUnDeformedGeometry(string caseName,
            double scaleFactor,
            int stageOfStageConstruction = -1,
            bool resetToOriginal = false);

        /// <summary>
        /// This function modifies the undeformed geometry based on the shape of a specified mode from a specified modal or buckling load case.
        /// </summary>
        /// <param name="caseName">The name of a modal or buckling load case.</param>
        /// <param name="mode">The mode shape to consider.</param>
        /// <param name="maxDisplacement">The maximum displacement to which the mode shape will be scaled.</param>
        /// <param name="direction">The direction in which to apply the geometry modification.</param>
        /// <param name="resetToOriginal">True: All other input items in this function are ignored and the original undeformed geometry data is reinstated.</param>
        void ModifyUndeformedGeometryModeShape(string caseName,
            int mode,
            double maxDisplacement,
            int direction,
            bool resetToOriginal = false);

        // === Get ===

        /// <summary>
        /// Retrieves the status for all load cases.
        /// </summary>
        /// <param name="caseName">This is an array that includes the name of each analysis case for which the status is reported.</param>
        /// <param name="caseStatus">This is an array of that includes 1, 2, 3 or 4, indicating the load case status.</param>
        void GetCaseStatus(ref string[] caseName,
            ref eCaseStatus[] caseStatus);

        // === Get/Set ===


        /// <summary>
        /// Retrieves the model global degrees of freedom.
        /// </summary>
        /// <param name="activeDOFs">Boolean indications of which degrees of freedom are active.</param>
        void GetActiveDOF(ref DegreesOfFreedomGlobal activeDOFs);

        /// <summary>
        /// Sets the model global degrees of freedom.
        /// </summary>
        /// <param name="activeDOFs">Boolean indications of which degrees of freedom are active.</param>
        void SetActiveDOF(DegreesOfFreedomGlobal activeDOFs);

        // ===

        /// <summary>
        /// Retrieves the model solver options.
        /// </summary>
        /// <param name="solverType">Solver type used for the analysis.</param>
        /// <param name="solverProcessType">Process type used for the analysis.</param>
        /// <param name="force32BitSolver">True: The analysis is always run using 32-bit, even on 64-bit computers.</param>
        /// <param name="stiffCase">The name of the load case used when outputting the mass and stiffness matrices to text files.
        /// If this item is blank, no matrices are output.</param>
        void GetSolverOption_1(ref eSolverType solverType,
            ref eSolverProcessType solverProcessType,
            ref bool force32BitSolver,
            ref string stiffCase);

        /// <summary>
        /// Sets the model solver options.
        /// </summary>
        /// <param name="solverType">Solver type used for the analysis.</param>
        /// <param name="solverProcessType">Process type used for the analysis.</param>
        /// <param name="force32BitSolver">True: The analysis is always run using 32-bit, even on 64-bit computers.</param>
        /// <param name="stiffCase">The name of the load case used when outputting the mass and stiffness matrices to text files.
        /// If this item is blank, no matrices are output.</param>
        void SetSolverOption_1(eSolverType solverType,
            eSolverProcessType solverProcessType,
            bool force32BitSolver,
            string stiffCase = "");

        // ===

        /// <summary>
        /// Retrieves the run flags for all analysis cases.
        /// </summary>
        /// <param name="caseName">This is an array that includes the name of each analysis case for which the run flag is reported.</param>
        /// <param name="caseSetToRun">This is an array of boolean values indicating if the specified load case is to be run.</param>
        void GetRunCaseFlag(ref string[] caseName,
            ref bool[] caseSetToRun);

        /// <summary>
        /// Sets the run flag for all of the existing load cases.
        /// </summary>
        /// <param name="caseName">The name of an existing load case that is to have its flag set.</param>
        /// <param name="caseSetToRun">&gt;True: The specified load case is to be run.</param>
        /// <param name="applyToAll">True: The run flag is set as specified by the Run item for all load cases, and the Name item is ignored.</param>
        void SetRunCaseFlag(string caseName,
            bool caseSetToRun,
            bool applyToAll = false);

        #endregion
    }
}