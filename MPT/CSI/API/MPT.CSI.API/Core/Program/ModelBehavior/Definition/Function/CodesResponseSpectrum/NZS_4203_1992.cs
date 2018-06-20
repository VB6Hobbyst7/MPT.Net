// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="NZS_4203_1992.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    /// <summary>
    /// Response spectrum function from NZS_4203_1992.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum.ResponseSpectrumFunction" />
    public class NZS_4203_1992 : ResponseSpectrumFunction
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="NZS_4203_1992" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        protected NZS_4203_1992(CSiApiSeed seed) : base(seed) { }
        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves the definition of an NZS4203-1992 response spectrum function.
        /// </summary>
        /// <param name="name">The name of an NZS4203-1992 response spectrum function.</param>
        /// <param name="scalingFactor">The scaling factor (Sm * Sp * R * Z * L).</param>
        /// <param name="subsoilCategory">The subsoil category.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetFunction(string name,
            ref double scalingFactor,
            ref eSiteClass_NZS_4203_1992 subsoilCategory,
            ref double dampingRatio)
        {
            int csiSubsoilCategory = 0;

            _callCode = _sapModel.Func.FuncRS.GetNZS42031992(name, ref scalingFactor, ref csiSubsoilCategory, ref dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            subsoilCategory = (eSiteClass_NZS_4203_1992)csiSubsoilCategory;
        }

        /// <summary>
        /// This function defines an NZS4203-1992 response spectrum function.
        /// </summary>
        /// <param name="name">The name of an NZS4203-1992 response spectrum function. <para />
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="scalingFactor">The scaling factor (Sm * Sp * R * Z * L).</param>
        /// <param name="subsoilCategory">The subsoil category.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetFunction(string name,
            double scalingFactor,
            eSiteClass_NZS_4203_1992 subsoilCategory,
            double dampingRatio)
        {
            dampingRatio = limitDampingRatio(dampingRatio);
            _callCode = _sapModel.Func.FuncRS.SetNZS42031992(name, scalingFactor, (int)subsoilCategory, dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
#endif