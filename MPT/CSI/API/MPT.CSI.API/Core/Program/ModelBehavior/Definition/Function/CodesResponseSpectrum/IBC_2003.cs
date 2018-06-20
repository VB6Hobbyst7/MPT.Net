// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="IBC_2003.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    /// <summary>
    /// Response spectrum function from IBC_2003.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum.ResponseSpectrumFunction" />
    public class IBC_2003 : ResponseSpectrumFunction
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="IBC_2003" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        protected IBC_2003(CSiApiSeed seed) : base(seed) { }
        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves the definition of an IBC2003 response spectrum function.
        /// </summary>
        /// <param name="name">The name of an IBC2003 response spectrum function.</param>
        /// <param name="Sds">The design spectral acceleration at short periods, Sds.</param>
        /// <param name="Sd1">The design spectral acceleration at a one second period, Sd1.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetFunction(string name,
            ref double Sds,
            ref double Sd1,
            ref double dampingRatio)
        {
            _callCode = _sapModel.Func.FuncRS.GetIBC2003(name, ref Sds, ref Sd1, ref dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function defines an IBC2003 response spectrum function.
        /// </summary>
        /// <param name="name">The name of an IBC2003 response spectrum function. <para />
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="Sds">The design spectral acceleration at short periods, Sds.</param>
        /// <param name="Sd1">The design spectral acceleration at a one second period, Sd1.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetFunction(string name,
            double Sds,
            double Sd1,
            double dampingRatio)
        {
            dampingRatio = limitDampingRatio(dampingRatio);
            _callCode = _sapModel.Func.FuncRS.SetIBC2003(name, Sds, Sd1, dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
#endif