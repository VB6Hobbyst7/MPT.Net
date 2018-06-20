// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="BOCA_1996.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    /// <summary>
    /// Response spectrum function from BOCA_1996.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum.ResponseSpectrumFunction" />
    public class BOCA_1996 : ResponseSpectrumFunction
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="BOCA_1996" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        protected BOCA_1996(CSiApiSeed seed) : base(seed) { }
        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves the definition of a BOCA96 response spectrum function.
        /// </summary>
        /// <param name="name">The name of a BOCA96 response spectrum function.</param>
        /// <param name="Aa">The effective peak acceleration, Aa.</param>
        /// <param name="Av">The effective peak velocity, Av.</param>
        /// <param name="S">The soil profile coefficient, S.</param>
        /// <param name="R">The response modification factor, R.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetFunction(string name,
            ref double Aa,
            ref double Av,
            ref double S,
            ref double R,
            ref double dampingRatio)
        {
            _callCode = _sapModel.Func.FuncRS.GetBOCA96(name, ref Aa, ref Av, ref S, ref R, ref dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function defines a BOCA96 response spectrum function.
        /// </summary>
        /// <param name="name">The name of a BOCA9 response spectrum function. <para />
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="Aa">The effective peak acceleration, Aa.</param>
        /// <param name="Av">The effective peak velocity, Av.</param>
        /// <param name="S">The soil profile coefficient, S.</param>
        /// <param name="R">The response modification factor, R.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetFunction(string name,
            double Aa,
            double Av,
            double S,
            double R,
            double dampingRatio)
        {
            dampingRatio = limitDampingRatio(dampingRatio);
            _callCode = _sapModel.Func.FuncRS.SetBOCA96(name, Aa, Av, S, R, dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
#endif