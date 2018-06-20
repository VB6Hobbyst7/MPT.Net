// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-04-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="Analyzer.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
using IO = System.IO;

using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Represents the analysis controls in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IAnalyzer" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class Analyzer : CSiApiBase, IAnalyzer
    {
        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="Analyzer" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public Analyzer(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Public
        /// <summary>
        /// Creates the analysis model.
        /// If the analysis model is already created and current, nothing is done.
        /// It is not necessary to call this function before running an analysis.
        /// The analysis model is automatically created, if necessary, when the model is run.
        /// </summary>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void CreateAnalysisModel()
        {
            _callCode = _sapModel.Analyze.CreateAnalysisModel();
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Runs the analysis.
        /// The analysis model is automatically created as part of this function.
        /// </summary>
        /// <param name="filePath">Path to the model file.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public bool RunAnalysis(string filePath)
        {
            if (!IO.File.Exists(filePath)) return false;
            // TODO: Why is this here? Check if necessary.

            // run model (this will create the analysis model)
            _callCode = _sapModel.Analyze.RunAnalysis();
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return true;
        }

        /// <summary>
        /// Deletes results for the specified load case.
        /// </summary>
        /// <param name="nameLoadCase">The name of an existing load case that is to have its results deleted.
        /// This item is ignored when the 'All' item is 'True'.</param>
        /// <param name="deleteAll">True: Results are deleted for all load cases, and the Name item is ignored.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void DeleteResults(string nameLoadCase,
            bool deleteAll = false)
        {
            _callCode = _sapModel.Analyze.DeleteResults(nameLoadCase, deleteAll);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Modifies the undeformed geometry based on displacements obtained from a specified load case.
        /// </summary>
        /// <param name="caseName">The name of the static load case from which displacements are obtained.</param>
        /// <param name="scaleFactor">The scale factor applied to the displacements.</param>
        /// <param name="stageOfStageConstruction">This item applies only when the specified load case is a staged construction load case.
        /// It is the stage number from which the displacements are obtained.
        /// Specifying a -1 for this item means to use the last run stage.</param>
        /// <param name="resetToOriginal">True: All other input items in this function are ignored and the original undeformed geometry data is reinstated.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void ModifyUnDeformedGeometry(string caseName,
            double scaleFactor,
            int stageOfStageConstruction = -1,
            bool resetToOriginal = false)
        {
            _callCode = _sapModel.Analyze.ModifyUndeformedGeometry(caseName, scaleFactor, stageOfStageConstruction, resetToOriginal);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function modifies the undeformed geometry based on the shape of a specified mode from a specified modal or buckling load case.
        /// </summary>
        /// <param name="caseName">The name of a modal or buckling load case.</param>
        /// <param name="mode">The mode shape to consider.</param>
        /// <param name="maxDisplacement">The maximum displacement to which the mode shape will be scaled.</param>
        /// <param name="direction">The direction in which to apply the geometry modification.</param>
        /// <param name="resetToOriginal">True: All other input items in this function are ignored and the original undeformed geometry data is reinstated.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void ModifyUndeformedGeometryModeShape(string caseName,
            int mode,
            double maxDisplacement,
            int direction,
            bool resetToOriginal = false)
        {
            _callCode = _sapModel.Analyze.ModifyUndeformedGeometryModeShape(caseName, mode, maxDisplacement, direction, resetToOriginal);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // === Get ===

        /// <summary>
        /// Retrieves the status for all load cases.
        /// </summary>
        /// <param name="caseName">This is an array that includes the name of each analysis case for which the status is reported.</param>
        /// <param name="caseStatus">This is an array of that includes 1, 2, 3 or 4, indicating the load case status.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetCaseStatus(ref string[] caseName,
            ref eCaseStatus[] caseStatus)
        {
            int[] csiCaseStatus = new int[0];

            _callCode = _sapModel.Analyze.GetCaseStatus(ref _numberOfItems, ref caseName, ref csiCaseStatus);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            caseStatus = csiCaseStatus.Cast< eCaseStatus>().ToArray();
        }

        // === Get/Set ===


        /// <summary>
        /// Retrieves the model global degrees of freedom.
        /// </summary>
        /// <param name="activeDOFs">Boolean indications of which degrees of freedom are active.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetActiveDOF(ref DegreesOfFreedomGlobal activeDOFs)
        {
            bool[] dofArray = new bool[0];

            _callCode = _sapModel.Analyze.GetActiveDOF(ref dofArray);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            activeDOFs.FromArray(dofArray);
        }

        /// <summary>
        /// Sets the model global degrees of freedom.
        /// </summary>
        /// <param name="activeDOFs">Boolean indications of which degrees of freedom are active.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetActiveDOF(DegreesOfFreedomGlobal activeDOFs)
        {
            bool[] dofArray = activeDOFs.ToArray();

            _callCode = _sapModel.Analyze.SetActiveDOF(ref dofArray);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Retrieves the model solver options.
        /// </summary>
        /// <param name="solverType">Solver type used for the analysis.</param>
        /// <param name="solverProcessType">Process type used for the analysis.</param>
        /// <param name="force32BitSolver">True: The analysis is always run using 32-bit, even on 64-bit computers.</param>
        /// <param name="stiffCase">The name of the load case used when outputting the mass and stiffness matrices to text files.
        /// If this item is blank, no matrices are output.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetSolverOption_1(ref eSolverType solverType,
            ref eSolverProcessType solverProcessType,
            ref bool force32BitSolver,
            ref string stiffCase)
        {
            int csiSolverType = 0;
            int csiSolverProcessType = 0;

            _callCode = _sapModel.Analyze.GetSolverOption_1(ref csiSolverType, ref csiSolverProcessType, ref force32BitSolver, ref stiffCase);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            solverType = (eSolverType)csiSolverType;
            solverProcessType = (eSolverProcessType)csiSolverProcessType;
        }

        /// <summary>
        /// Sets the model solver options.
        /// </summary>
        /// <param name="solverType">Solver type used for the analysis.</param>
        /// <param name="solverProcessType">Process type used for the analysis.</param>
        /// <param name="force32BitSolver">True: The analysis is always run using 32-bit, even on 64-bit computers.</param>
        /// <param name="stiffCase">The name of the load case used when outputting the mass and stiffness matrices to text files.
        /// If this item is blank, no matrices are output.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetSolverOption_1(eSolverType solverType,
            eSolverProcessType solverProcessType,
            bool force32BitSolver,
            string stiffCase = "")
        {
            _callCode = _sapModel.Analyze.SetSolverOption_1((int)solverType, (int)solverProcessType, force32BitSolver, stiffCase);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// Retrieves the run flags for all analysis cases.
        /// </summary>
        /// <param name="caseName">This is an array that includes the name of each analysis case for which the run flag is reported.</param>
        /// <param name="caseSetToRun">This is an array of boolean values indicating if the specified load case is to be run.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetRunCaseFlag(ref string[] caseName,
            ref bool[] caseSetToRun)
        {
            _callCode = _sapModel.Analyze.GetRunCaseFlag(ref _numberOfItems, ref caseName, ref caseSetToRun);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Sets the run flag for all of the existing load cases.
        /// </summary>
        /// <param name="caseName">The name of an existing load case that is to have its flag set.</param>
        /// <param name="caseSetToRun">&gt;True: The specified load case is to be run.</param>
        /// <param name="applyToAll">True: The run flag is set as specified by the Run item for all load cases, and the Name item is ignored.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetRunCaseFlag(string caseName,
            bool caseSetToRun,
            bool applyToAll = false)
        {
            _callCode = _sapModel.Analyze.SetRunCaseFlag(caseName, caseSetToRun, applyToAll);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
