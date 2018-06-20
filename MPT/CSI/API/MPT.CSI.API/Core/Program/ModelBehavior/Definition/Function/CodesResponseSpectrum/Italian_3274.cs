// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="Italian_3274.cs" company="">
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
    /// Response spectrum function from Italian_3274.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum.ResponseSpectrumFunction" />
    public class Italian_3274 : ResponseSpectrumFunction
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="Italian_3274" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        protected Italian_3274(CSiApiSeed seed) : base(seed) { }
        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves the definition of an Italian 3274 response spectrum function4.
        /// </summary>
        /// <param name="name">The name of an Italian 3274 response spectrum function.</param>
        /// <param name="g">The peak ground acceleration.</param>
        /// <param name="seismicIntensity">The seismic intensity.</param>
        /// <param name="q">The structure factor.</param>
        /// <param name="level">The spectral level, direction and building type.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetFunction(string name,
            ref double g,
            ref eSiteClass_Italian_3274 seismicIntensity,
            ref double q,
            ref eLevel_Italian_3274 level,
            ref double dampingRatio)
        {
            int csiSeismicIntensity = 0;
            double csiLevel = 0;

            _callCode = _sapModel.Func.FuncRS.GetItalian3274(name, ref g, ref csiSeismicIntensity, ref q, ref csiLevel, ref dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            seismicIntensity = (eSiteClass_Italian_3274)csiSeismicIntensity;
            level = (eLevel_Italian_3274)csiLevel;
        }

        /// <summary>
        /// This function defines an Italian 3274 response spectrum function.
        /// </summary>
        /// <param name="name">The name of an Italian 3274 response spectrum function. <para />
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="g">The peak ground acceleration.</param>
        /// <param name="seismicIntensity">The seismic intensity.</param>
        /// <param name="q">The structure factor.</param>
        /// <param name="level">The spectral level, direction and building type.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetFunction(string name,
            double g,
            eSiteClass_Italian_3274 seismicIntensity,
            double q,
            eLevel_Italian_3274 level,
            double dampingRatio)
        {
            dampingRatio = limitDampingRatio(dampingRatio);
            _callCode = _sapModel.Func.FuncRS.SetItalian3274(name, g, (int)seismicIntensity, q, (double)level, dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
#endif