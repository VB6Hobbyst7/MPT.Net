// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="IS_1893_2002.cs" company="">
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
    /// Response spectrum function from IS_1893_2002.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum.ResponseSpectrumFunction" />
    public class IS_1893_2002 : ResponseSpectrumFunction
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="IS_1893_2002" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        protected IS_1893_2002(CSiApiSeed seed) : base(seed) { }
        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves the definition of an IS1893-2002 response spectrum function.
        /// </summary>
        /// <param name="name">The name of an IS1893-2002 response spectrum function.</param>
        /// <param name="Z">The seismic zone factor, Z.</param>
        /// <param name="S">The soil type.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetFunction(string name,
            ref double Z,
            ref eSiteClass_IS_1893_2002 S,
            ref double dampingRatio)
        {
            int csiSoilType = 0;

            _callCode = _sapModel.Func.FuncRS.GetIS18932002(name, ref Z, ref csiSoilType, ref dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            S = (eSiteClass_IS_1893_2002)csiSoilType;
        }

        /// <summary>
        /// This function defines an IS1893-2002 response spectrum function.
        /// </summary>
        /// <param name="name">The name of an IS1893-2002 response spectrum function. <para />
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="Z">The seismic zone factor, Z.</param>
        /// <param name="S">The soil type.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetFunction(string name,
            double Z,
            eSiteClass_IS_1893_2002 S,
            double dampingRatio)
        {
            dampingRatio = limitDampingRatio(dampingRatio);
            _callCode = _sapModel.Func.FuncRS.SetIS18932002(name, Z, (int)S, dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
#endif