// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-11-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="Functions.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Represents the functions in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.IFunctions" />
    public class Functions : CSiApiBase, IFunctions
    {


        #region Fields
        /// <summary>
        /// The seed
        /// </summary>
        private readonly CSiApiSeed _seed;
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// The power spectral density
        /// </summary>
        private PowerSpectralDensity _powerSpectralDensity;
        /// <summary>
        /// The steady state
        /// </summary>
        private SteadyState _steadyState;
#endif
        /// <summary>
        /// The response spectrum
        /// </summary>
        private ResponseSpectrum _responseSpectrum;
        /// <summary>
        /// The time history
        /// </summary>
        private TimeHistory _timeHistory;
        #endregion

        #region Properties                    

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Gets the power spectral density function.
        /// </summary>
        /// <value>The power spectral density function.</value>
        public PowerSpectralDensity PowerSpectralDensity => _powerSpectralDensity ?? (_powerSpectralDensity = new PowerSpectralDensity(_seed));

        /// <summary>
        /// Gets the state of the steady function.
        /// </summary>
        /// <value>The state of the steady function.</value>
        public SteadyState SteadyState => _steadyState ?? (_steadyState = new SteadyState(_seed));
#endif
        /// <summary>
        /// Gets the response spectrum function.
        /// </summary>
        /// <value>The response spectrum function.</value>
        public ResponseSpectrum ResponseSpectrum => _responseSpectrum ?? (_responseSpectrum = new ResponseSpectrum(_seed));

        /// <summary>
        /// Gets the time history function.
        /// </summary>
        /// <value>The time history function.</value>
        public TimeHistory TimeHistoryDirectLinear => _timeHistory ?? (_timeHistory = new TimeHistory(_seed));
        #endregion


        #region Initialization        

        /// <summary>
        /// Initializes a new instance of the <see cref="Functions" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public Functions(CSiApiSeed seed) : base(seed)
        {
            _seed = seed;
        }


        #endregion

        #region Methods: Public Inherited   

        /// <summary>
        /// This function changes the name of an existing loading function.
        /// </summary>
        /// <param name="currentName">The existing name of a defined loading function.</param>
        /// <param name="newName">The new name for the loading function.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ChangeName(string currentName, string newName)
        {
            _callCode = _sapModel.Func.ChangeName(currentName, newName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function returns the total number of defined loading functions in the model.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int Count()
        {
            return _sapModel.Func.Count();
        }

        /// <summary>
        /// The function deletes a specified loading function.
        /// It returns an error if the specified loading function can not be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        /// <param name="name">The name of an existing loading function.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void Delete(string name)
        {
            _callCode = _sapModel.Func.Delete(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves the names of all defined loading functions.
        /// </summary>
        /// <param name="names">Loading function names retrieved by the program.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetNameList(out string[] names)
        {
            names = new string[0];
            _callCode = _sapModel.Func.GetNameList(ref _numberOfItems, ref names);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        #endregion

        #region Methods: Public

        /// <summary>
        /// This function retrieves the function type for the specified function.
        /// </summary>
        /// <param name="name">The name of an existing function.</param>
        /// <param name="functionType">This is the main type of function.</param>
        /// <param name="functionSubType">This is the function subtype, which is dependent upond the function type.
        /// See enumerations for code definitions.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetType(string name,
            ref eFunctionType functionType,
            ref int functionSubType)
        {
            int csiFunctionType = 0;

            _callCode = _sapModel.Func.GetTypeOAPI(name, ref csiFunctionType, ref functionSubType);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            
            functionType = (eFunctionType)csiFunctionType;
        }

        /// <summary>
        /// This function retrieves the time and function values for any defined function.
        /// </summary>
        /// <param name="name">The name of an existing function.</param>
        /// <param name="numberItems">The number of time and function value pairs retrieved.</param>
        /// <param name="timeValue">This is an array that includes the time value for each data point.
        /// [sec] for response spectrum and time history functions, [cyc/sec] for power spectral density and steady state functions</param>
        /// <param name="functionValue">This is an array that includes the function value for each data point.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetValues(string name,
            ref int numberItems,
            ref double[] timeValue,
            ref double[] functionValue)
        {
            _callCode = _sapModel.Func.GetValues(name, ref numberItems, ref timeValue, ref functionValue);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function converts an existing function to a user defined function.
        /// An error is returned if the specified function is already a user defined function.
        /// </summary>
        /// <param name="name">The name of an existing function that is not a user defined function.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ConvertToUser(string name)
        {
            // TODO: Handle: An error is returned if the specified function is already a user defined function.
            _callCode = _sapModel.Func.ConvertToUser(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        #endregion
    }
}
