// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="ILoadCases.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Implements the load cases in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    public interface ILoadCases: IChangeableName, ICountable, IDeletable, IListableNames
    {
        #region Properties       
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Gets the external results load case.
        /// </summary>
        /// <value>The external results load case.</value>
        ExternalResults ExternalResults { get; }

        /// <summary>
        /// Gets the moving load load case.
        /// </summary>
        /// <value>The moving load load case.</value>
        MovingLoad MovingLoad { get; }

        /// <summary>
        /// Gets the power spectral density load case.
        /// </summary>
        /// <value>The power spectral density load case.</value>
        PowerSpectralDensity PowerSpectralDensity { get; }

        /// <summary>
        /// Gets the static linear multistep load case.
        /// </summary>
        /// <value>The static linear multistep load case.</value>
        StaticLinearMultistep StaticLinearMultistep { get; }

        /// <summary>
        /// Gets the state of the steady load case.
        /// </summary>
        /// <value>The state of the steady load case.</value>
        SteadyState SteadyState { get; }
#endif
#if !BUILD_ETABS2015
        /// <summary>
        /// Gets the hyperstatic load case.
        /// </summary>
        /// <value>The hyperstatic load case.</value>
        Hyperstatic Hyperstatic { get; }

        /// <summary>
        /// Gets the modal eigen load case.
        /// </summary>
        /// <value>The modal eigen load case.</value>
        ModalEigen ModalEigen { get; }
#endif
        /// <summary>
        /// Gets the buckling load case.
        /// </summary>
        /// <value>The buckling load case.</value>
        Buckling Buckling { get; }
        
        /// <summary>
        /// Gets the modal ritz load case.
        /// </summary>
        /// <value>The modal ritz load case.</value>
        ModalRitz ModalRitz { get; }

        /// <summary>
        /// Gets the response spectrum load case.
        /// </summary>
        /// <value>The response spectrum load case.</value>
        ResponseSpectrum ResponseSpectrum { get; }

        /// <summary>
        /// Gets the static linear load case.
        /// </summary>
        /// <value>The static linear load case.</value>
        StaticLinear StaticLinear { get; }

        /// <summary>
        /// Gets the static nonlinear load case.
        /// </summary>
        /// <value>The static nonlinear load case.</value>
        StaticNonlinear StaticNonlinear { get; }

        /// <summary>
        /// Gets the static nonlinear staged load case.
        /// </summary>
        /// <value>The static nonlinear staged load case.</value>
        StaticNonlinearStaged StaticNonlinearStaged { get; }

        /// <summary>
        /// Gets the time history direct linea load caser.
        /// </summary>
        /// <value>The time history direct linear load case.</value>
        TimeHistoryDirectLinear TimeHistoryDirectLinear { get; }

        /// <summary>
        /// Gets the time history direct nonlinear load case.
        /// </summary>
        /// <value>The time history direct nonlinear load case.</value>
        TimeHistoryDirectNonlinear TimeHistoryDirectNonlinear { get; }

        /// <summary>
        /// Gets the time history modal linear load case.
        /// </summary>
        /// <value>The time history modal linear load case.</value>
        TimeHistoryModalLinear TimeHistoryModalLinear { get; }

        /// <summary>
        /// Gets the time history modal nonlinear load case.
        /// </summary>
        /// <value>The time history modal nonlinear load case.</value>
        TimeHistoryModalNonlinear TimeHistoryModalNonlinear { get; }

#endregion

#region Methods: Public

        /// <summary>
        /// This function returns the total number of defined load cases in the model of a specified load case type.
        /// </summary>
        /// <param name="caseType">Load case type for which a count is desired.
        /// If 'all' is selected, a count is returned for all load cases in the model regardless of type.</param>
        /// <returns>System.Int32.</returns>
        int Count(eLoadCaseType caseType);


        /// <summary>
        /// This function retrieves the names of all defined load cases of the specified type.
        /// </summary>
        /// <param name="namesOfLoadCaseType">Names of all load cases of the specified type.</param>
        /// <param name="caseType">Load case type for which names are desired.</param>
        void GetNameList(out string[] namesOfLoadCaseType,
            eLoadCaseType caseType);

        /// <summary>
        /// This function retrieves the case type, design type, and auto flag for the specified load case.
        /// </summary>
        /// <param name="nameLoadCase">The name of an existing load case.</param>
        /// <param name="loadCaseType">Load case type corresponding to the name provided.</param>
        /// <param name="loadCaseSubType">Load case sub-type corresponding to the name provided.</param>
        /// <param name="designType">Load case design type corresponding to the name provided.</param>
        /// <param name="designTypeOption">Load case type corresponding to the name provided.</param>
        /// <param name="autoCreatedCase">This is one of the following values indicating if the load case has been automatically created.</param>
        void GetCaseTypes(string nameLoadCase,
            ref eLoadCaseType loadCaseType,
            ref int loadCaseSubType,
            ref eLoadPatternType designType,
            ref eSpecificationSource designTypeOption,
            ref eAutoCreatedCase autoCreatedCase);

        /// <summary>
        /// This function sets the design type for the specified load case.
        /// </summary>
        /// <param name="nameLoadCase">The name of an existing load case.</param>
        /// <param name="designTypeOption">This is one of the following options for the DesignType item.</param>
        /// <param name="designType">This item only applies when the DesignTypeOption is 1 (user specified). It is one of the following items in the eLoadPatternType enumeration.</param>
        void SetDesignType(string nameLoadCase,
            eSpecificationSource designTypeOption,
            eLoadPatternType designType = eLoadPatternType.Dead);

#endregion
    }
}