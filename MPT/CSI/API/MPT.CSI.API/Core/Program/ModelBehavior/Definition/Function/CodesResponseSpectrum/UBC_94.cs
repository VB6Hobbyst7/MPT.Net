// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="UBC_94.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    /// <summary>
    /// Response spectrum function from UBC_94.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum.ResponseSpectrumFunction" />
    public class UBC_94 : ResponseSpectrumFunction
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="UBC_94" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        protected UBC_94(CSiApiSeed seed) : base(seed) { }
        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves the definition of a UBC94 response spectrum function.
        /// </summary>
        /// <param name="name">The name of a UBC94 response spectrum function.</param>
        /// <param name="Z">The seismic zone factor, Z.</param>
        /// <param name="S">This is 1, 2 or 3, indicating the soil type.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetFunction(string name,
            double Z,
            int S,
            double dampingRatio)
        {
            _callCode = _sapModel.Func.FuncRS.GetUBC94(name, ref Z, ref S, ref dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function defines a UBC94 response spectrum function.
        /// </summary>
        /// <param name="name">The name of a UBC94 response spectrum function. <para />
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="Z">The seismic zone factor, Z.</param>
        /// <param name="S">This is 1, 2 or 3, indicating the soil type.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetFunction(string name,
            double Z,
            int S,
            double dampingRatio)
        {
            if (S < 0) {S = 1;}
            if (S > 3) {S = 3;}
            dampingRatio = limitDampingRatio(dampingRatio);

            _callCode = _sapModel.Func.FuncRS.SetUBC94(name, Z, S, dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
#endif