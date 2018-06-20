// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="Eurocode_8.cs" company="">
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
    /// Response spectrum function from Eurocode_8.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum.ResponseSpectrumFunction" />
    public class Eurocode_8 : ResponseSpectrumFunction
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="Eurocode_8" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        protected Eurocode_8(CSiApiSeed seed) : base(seed) { }
        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves the definition of a EuroCode8 response spectrum function.
        /// </summary>
        /// <param name="name">The name of a EuroCode8 response spectrum function.</param>
        /// <param name="AG">The design ground acceleration, Ag.</param>
        /// <param name="S">The subsoil class.</param>
        /// <param name="n">The damping correction factor, n, where n &gt;= 0.7.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetFunction(string name,
            ref double AG,
            ref eSiteClass_Eurocode_8 S,
            ref double n,
            ref double dampingRatio)
        {
            int csiSubsoilClass = 0;

            _callCode = _sapModel.Func.FuncRS.GetEuroCode8(name, ref AG, ref csiSubsoilClass, ref n, ref dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            S = (eSiteClass_Eurocode_8)csiSubsoilClass;
        }

        /// <summary>
        /// This function defines a EuroCode8 response spectrum function.
        /// </summary>
        /// <param name="name">The name of a EuroCode8 response spectrum function. <para />
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="AG">The design ground acceleration, Ag.</param>
        /// <param name="S">The subsoil class.</param>
        /// <param name="n">The damping correction factor, n, where n &gt;= 0.7.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetFunction(string name,
            double AG,
            eSiteClass_Eurocode_8 S,
            double n,
            double dampingRatio)
        {
            if (n < 0.7) { n = 0.7; }
            dampingRatio = limitDampingRatio(dampingRatio);

            _callCode = _sapModel.Func.FuncRS.SetEuroCode8(name, AG, (int)S, n, dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
#endif