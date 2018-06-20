// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="Chinese_2010.cs" company="">
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
    /// Response spectrum function from Chinese_2010.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum.ResponseSpectrumFunction" />
    public class Chinese_2010 : ResponseSpectrumFunction
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="Chinese_2010" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        protected Chinese_2010(CSiApiSeed seed) : base(seed) { }
        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves the definition of a Chinese 2010 response spectrum function.
        /// </summary>
        /// <param name="name">The name of a Chinese 2010 response spectrum function.</param>
        /// <param name="alphaMax">The maximum influence factor.</param>
        /// <param name="SI">The seismic intensity.</param>
        /// <param name="Tg">The characteristic ground period, Tg &gt; 0.1. [s].</param>
        /// <param name="PTDF">The period time discount factor.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetFunction(string name,
            ref double alphaMax,
            ref eSeismicIntensity_Chinese_2010 SI,
            ref double Tg,
            ref double PTDF,
            ref double dampingRatio)
        {
            int csiSeismicIntensity = 0;

            _callCode = _sapModel.Func.FuncRS.GetChinese2010(name, ref alphaMax, ref csiSeismicIntensity, ref Tg, ref PTDF, ref dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            SI = (eSeismicIntensity_Chinese_2010)csiSeismicIntensity;
        }

        /// <summary>
        /// This function defines a Chinese 2010 response spectrum function.
        /// </summary>
        /// <param name="name">The name of a Chinese 2010 response spectrum function. <para />
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="alphaMax">The maximum influence factor.</param>
        /// <param name="SI">The seismic intensity.</param>
        /// <param name="Tg">The characteristic ground period, Tg &gt; 0.1. [s].</param>
        /// <param name="PTDF">The period time discount factor.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetFunction(string name,
            double alphaMax,
            eSeismicIntensity_Chinese_2010 SI,
            double Tg,
            double PTDF,
            double dampingRatio)
        {
            dampingRatio = limitDampingRatio(dampingRatio);
            _callCode = _sapModel.Func.FuncRS.SetChinese2010(name, alphaMax, (int)SI, Tg, PTDF, dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
#endif