// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-10-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="LoadCases.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_SAP2000v16
using CSiProgram = SAP2000v16;
#elif BUILD_SAP2000v17
using CSiProgram = SAP2000v17;
#elif BUILD_SAP2000v18
using CSiProgram = SAP2000v18;
#elif BUILD_SAP2000v19
using CSiProgram = SAP2000v19;
#elif BUILD_SAP2000v20
using CSiProgram = SAP2000v20;
#elif BUILD_CSiBridgev16
using CSiProgram = CSiBridge16;
#elif BUILD_CSiBridgev17
using CSiProgram = CSiBridge17;
#elif BUILD_CSiBridgev18
using CSiProgram = CSiBridge18;
#elif BUILD_CSiBridgev19
using CSiProgram = CSiBridge19;
#elif BUILD_CSiBridgev20
using CSiProgram = CSiBridge20;
#elif BUILD_ETABS2013
using CSiProgram = ETABS2013;
#elif BUILD_ETABS2015
using CSiProgram = ETABS2015;
#elif BUILD_ETABS2016
using CSiProgram = ETABS2016;
#elif BUILD_ETABS2017
using CSiProgram = ETABSv17;
#endif
using MPT.Enums;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Represents the load cases in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.ILoadCases" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class LoadCases : CSiApiBase, ILoadCases
    {

        #region Fields
        /// <summary>
        /// The seed
        /// </summary>
        private readonly CSiApiSeed _seed;

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// The external results
        /// </summary>
        private ExternalResults _externalResults;
        /// <summary>
        /// The moving load
        /// </summary>
        private MovingLoad _movingLoad;
        /// <summary>
        /// The power spectral density
        /// </summary>
        private PowerSpectralDensity _powerSpectralDensity;
        /// <summary>
        /// The static linear multistep
        /// </summary>
        private StaticLinearMultistep _staticLinearMultistep;
        /// <summary>
        /// The steady state
        /// </summary>
        private SteadyState _steadyState;
#endif
        /// <summary>
        /// The buckling
        /// </summary>
        private Buckling _buckling;
#if !BUILD_ETABS2015
        /// <summary>
        /// The hyperstatic
        /// </summary>
        private Hyperstatic _hyperstatic;
        /// <summary>
        /// The modal eigen
        /// </summary>
        private ModalEigen _modalEigen;
#endif
        /// <summary>
        /// The modal ritz
        /// </summary>
        private ModalRitz _modalRitz;
        /// <summary>
        /// The response spectrum
        /// </summary>
        private ResponseSpectrum _responseSpectrum;
        /// <summary>
        /// The static linear
        /// </summary>
        private StaticLinear _staticLinear;
        /// <summary>
        /// The static nonlinear
        /// </summary>
        private StaticNonlinear _staticNonlinear;
        /// <summary>
        /// The static nonlinear staged
        /// </summary>
        private StaticNonlinearStaged _staticNonlinearStaged;
        /// <summary>
        /// The time history direct linear
        /// </summary>
        private TimeHistoryDirectLinear _timeHistoryDirectLinear;
        /// <summary>
        /// The time history direct nonlinear
        /// </summary>
        private TimeHistoryDirectNonlinear _timeHistoryDirectNonlinear;
        /// <summary>
        /// The time history modal linear
        /// </summary>
        private TimeHistoryModalLinear _timeHistoryModalLinear;
        /// <summary>
        /// The time history modal nonlinear
        /// </summary>
        private TimeHistoryModalNonlinear _timeHistoryModalNonlinear;
#endregion

#region Properties       
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Gets the external results load case.
        /// </summary>
        /// <value>The external results load case.</value>
        public ExternalResults ExternalResults => _externalResults ?? (_externalResults = new ExternalResults(_seed));

        /// <summary>
        /// Gets the moving load load case.
        /// </summary>
        /// <value>The moving load load case.</value>
        public MovingLoad MovingLoad => _movingLoad ?? (_movingLoad = new MovingLoad(_seed));

        /// <summary>
        /// Gets the power spectral density load case.
        /// </summary>
        /// <value>The power spectral density load case.</value>
        public PowerSpectralDensity PowerSpectralDensity => _powerSpectralDensity ?? (_powerSpectralDensity = new PowerSpectralDensity(_seed));

        /// <summary>
        /// Gets the static linear multistep load case.
        /// </summary>
        /// <value>The static linear multistep load case.</value>
        public StaticLinearMultistep StaticLinearMultistep => _staticLinearMultistep ?? (_staticLinearMultistep = new StaticLinearMultistep(_seed));

        /// <summary>
        /// Gets the state of the steady load case.
        /// </summary>
        /// <value>The state of the steady load case.</value>
        public SteadyState SteadyState => _steadyState ?? (_steadyState = new SteadyState(_seed));
#endif
        /// <summary>
        /// Gets the buckling load case.
        /// </summary>
        /// <value>The buckling load case.</value>
        public Buckling Buckling => _buckling ?? (_buckling = new Buckling(_seed));
#if !BUILD_ETABS2015
        /// <summary>
        /// Gets the hyperstatic load case.
        /// </summary>
        /// <value>The hyperstatic load case.</value>
        public Hyperstatic Hyperstatic => _hyperstatic ?? (_hyperstatic = new Hyperstatic(_seed));

        /// <summary>
        /// Gets the modal eigen load case.
        /// </summary>
        /// <value>The modal eigen load case.</value>
        public ModalEigen ModalEigen => _modalEigen ?? (_modalEigen = new ModalEigen(_seed));
#endif
        /// <summary>
        /// Gets the modal ritz load case.
        /// </summary>
        /// <value>The modal ritz load case.</value>
        public ModalRitz ModalRitz => _modalRitz ?? (_modalRitz = new ModalRitz(_seed));

        /// <summary>
        /// Gets the response spectrum load case.
        /// </summary>
        /// <value>The response spectrum load case.</value>
        public ResponseSpectrum ResponseSpectrum => _responseSpectrum ?? (_responseSpectrum = new ResponseSpectrum(_seed));

        /// <summary>
        /// Gets the static linear load case.
        /// </summary>
        /// <value>The static linear load case.</value>
        public StaticLinear StaticLinear => _staticLinear ?? (_staticLinear = new StaticLinear(_seed));

        /// <summary>
        /// Gets the static nonlinear load case.
        /// </summary>
        /// <value>The static nonlinear load case.</value>
        public StaticNonlinear StaticNonlinear => _staticNonlinear ?? (_staticNonlinear = new StaticNonlinear(_seed));

        /// <summary>
        /// Gets the static nonlinear staged load case.
        /// </summary>
        /// <value>The static nonlinear staged load case.</value>
        public StaticNonlinearStaged StaticNonlinearStaged => _staticNonlinearStaged ?? (_staticNonlinearStaged = new StaticNonlinearStaged(_seed));

        /// <summary>
        /// Gets the time history direct linea load caser.
        /// </summary>
        /// <value>The time history direct linear load case.</value>
        public TimeHistoryDirectLinear TimeHistoryDirectLinear => _timeHistoryDirectLinear ?? (_timeHistoryDirectLinear = new TimeHistoryDirectLinear(_seed));

        /// <summary>
        /// Gets the time history direct nonlinear load case.
        /// </summary>
        /// <value>The time history direct nonlinear load case.</value>
        public TimeHistoryDirectNonlinear TimeHistoryDirectNonlinear => _timeHistoryDirectNonlinear ?? (_timeHistoryDirectNonlinear = new TimeHistoryDirectNonlinear(_seed));

        /// <summary>
        /// Gets the time history modal linear load case.
        /// </summary>
        /// <value>The time history modal linear load case.</value>
        public TimeHistoryModalLinear TimeHistoryModalLinear => _timeHistoryModalLinear ?? (_timeHistoryModalLinear = new TimeHistoryModalLinear(_seed));

        /// <summary>
        /// Gets the time history modal nonlinear load case.
        /// </summary>
        /// <value>The time history modal nonlinear load case.</value>
        public TimeHistoryModalNonlinear TimeHistoryModalNonlinear => _timeHistoryModalNonlinear ?? (_timeHistoryModalNonlinear = new TimeHistoryModalNonlinear(_seed));

#endregion


#region Initialization        

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadCases" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public LoadCases(CSiApiSeed seed) : base(seed)
        {
            _seed = seed;
        }


#endregion

#region Methods: Public

        /// <summary>
        /// This function changes the name of an existing load case.
        /// </summary>
        /// <param name="loadCaseName">The existing name of a defined load case</param>
        /// <param name="newName">The new name for the load case.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ChangeName(string loadCaseName,
            string newName)
        {
            _callCode = _sapModel.LoadCases.ChangeName(loadCaseName, newName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function returns the total number of defined load cases in the model.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int Count()
        {
            return _sapModel.LoadCases.Count();
        }

        /// <summary>
        /// This function returns the total number of defined load cases in the model of a specified load case type.
        /// </summary>
        /// <param name="caseType">Load case type for which a count is desired.
        /// If 'all' is selected, a count is returned for all load cases in the model regardless of type.</param>
        /// <returns>System.Int32.</returns>
        public int Count(eLoadCaseType caseType)
        {
            return _sapModel.LoadCases.Count(EnumLibrary.Convert<eLoadCaseType, CSiProgram.eLoadCaseType>(caseType));
        }

        /// <summary>
        /// The name of an existing load case.
        /// </summary>
        /// <param name="loadCaseName">The name of an existing load case.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void Delete(string loadCaseName)
        {
            _callCode = _sapModel.LoadCases.Delete(loadCaseName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves the names of all defined load cases.
        /// </summary>
        /// <param name="namesOfLoadCaseType">Names of all load cases of the specified type.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetNameList(out string[] namesOfLoadCaseType)
        {
            namesOfLoadCaseType = new string[0];
            _callCode = _sapModel.LoadCases.GetNameList(ref _numberOfItems, ref namesOfLoadCaseType);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves the names of all defined load cases of the specified type.
        /// </summary>
        /// <param name="namesOfLoadCaseType">Names of all load cases of the specified type.</param>
        /// <param name="caseType">Load case type for which names are desired.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetNameList(out string[] namesOfLoadCaseType,
            eLoadCaseType caseType)
        {
            namesOfLoadCaseType = new string[0];
            _callCode = _sapModel.LoadCases.GetNameList(ref _numberOfItems, ref namesOfLoadCaseType, 
                            EnumLibrary.Convert<eLoadCaseType, CSiProgram.eLoadCaseType>(caseType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves the case type, design type, and auto flag for the specified load case.
        /// </summary>
        /// <param name="nameLoadCase">The name of an existing load case.</param>
        /// <param name="loadCaseType">Load case type corresponding to the name provided.</param>
        /// <param name="loadCaseSubType">Load case sub-type corresponding to the name provided.</param>
        /// <param name="designType">Load case design type corresponding to the name provided.</param>
        /// <param name="designTypeOption">Load case type corresponding to the name provided.</param>
        /// <param name="autoCreatedCase">This is one of the following values indicating if the load case has been automatically created.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetCaseTypes(string nameLoadCase,
            ref eLoadCaseType loadCaseType,
            ref int loadCaseSubType,
            ref eLoadPatternType designType,
            ref eSpecificationSource designTypeOption,
            ref eAutoCreatedCase autoCreatedCase)
        {
            CSiProgram.eLoadCaseType csiCaseType = CSiProgram.eLoadCaseType.Modal;
            CSiProgram.eLoadPatternType csiPatternType = CSiProgram.eLoadPatternType.Dead;

            int csiDesignTypeOption = 0;
            int csiAutoCreatedCase = 0;

            _callCode = _sapModel.LoadCases.GetTypeOAPI_1(nameLoadCase, ref csiCaseType, ref loadCaseSubType, ref csiPatternType, ref csiDesignTypeOption, ref csiAutoCreatedCase);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            loadCaseType = EnumLibrary.Convert(csiCaseType, loadCaseType);
            designType = EnumLibrary.Convert(csiPatternType, designType);
            designTypeOption = (eSpecificationSource)csiDesignTypeOption;
            autoCreatedCase = (eAutoCreatedCase)csiAutoCreatedCase;
        }

        /// <summary>
        /// This function sets the design type for the specified load case.
        /// </summary>
        /// <param name="nameLoadCase">The name of an existing load case.</param>
        /// <param name="designTypeOption">This is one of the following options for the DesignType item.</param>
        /// <param name="designType">This item only applies when the DesignTypeOption is 1 (user specified). It is one of the following items in the eLoadPatternType enumeration.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetDesignType(string nameLoadCase,
            eSpecificationSource designTypeOption,
            eLoadPatternType designType = eLoadPatternType.Dead)
        {
            int csiDesignTypeOption = (int)designTypeOption;

            _callCode = _sapModel.LoadCases.SetDesignType(nameLoadCase, csiDesignTypeOption, 
                EnumLibrary.Convert<eLoadPatternType, CSiProgram.eLoadPatternType>(designType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#endregion
    }
}
