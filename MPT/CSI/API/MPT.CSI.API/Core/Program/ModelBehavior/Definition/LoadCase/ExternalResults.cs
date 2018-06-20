// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-10-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-10-2017
// ***********************************************************************
// <copyright file="ExternalResults.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Represents external results in the application.
    /// This enables users to subject frame objects to internal forces and moments which result from an external source.
    /// All other response quantities (displacements, reactions, shell stresses, etc.) will be reported as zero for these load cases.
    /// See <see href="http://wiki.csiamerica.com/display/kb/External+Results+load+case">CSi Knowledge Base</see> for more information.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.IExternalResults" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ExternalAnalysisResults" />
    public class ExternalResults : CSiApiBase, IExternalResults
    {

        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalResults" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public ExternalResults(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Public        
        /// <summary>
        /// This function initializes an external results load case.
        /// If this function is called for an existing load case, all items for the case are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new load case for which user-supplied external analysis results are available for some frame objects.
        /// If this is an existing case, that case is modified; otherwise, a new case is add.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetCase(string name)
        {
            _callCode = _sapModel.LoadCases.ExternalResults.SetCase(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// In the absence of a call to this function, the default values are <paramref name="firstStep" /> = 1 and <paramref name="lastStep" /> = 1.
        /// The number of steps available for this load case will be <paramref name="lastStep" /> – <paramref name="firstStep" /> + 1.
        /// </summary>
        /// <param name="name">The name of an existing external results load case.</param>
        /// <param name="firstStep">The number of the first step for which external results are to be subsequently provided for frame objects in conjunction with this load case.
        /// The value may be 0 or 1.
        /// A value of zero is typically used for cases that include the initial conditions, such as time-history cases.</param>
        /// <param name="lastStep">The number of the last step for which external results are to be subsequently provided for frame objects in conjunction with this load case.
        /// The value must be greater than or equal to <paramref name="firstStep" />.
        /// TODO: Handle this.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetNumberSteps(string name,
            int firstStep,
            int lastStep)
        {
            _callCode = _sapModel.LoadCases.ExternalResults.SetNumberSteps(name, firstStep, lastStep);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        #endregion
    }
}
#endif
