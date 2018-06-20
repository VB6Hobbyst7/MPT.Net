// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="CJJ_166_2011.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17 && !BUILD_CSiBridgev16 && !BUILD_CSiBridgev17
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    /// <summary>
    /// Response spectrum function from CJJ_166_2011.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum.ResponseSpectrumFunction" />
    public class CJJ_166_2011 : ResponseSpectrumFunction
    {
#region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="CJJ_166_2011" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        protected CJJ_166_2011(CSiApiSeed seed) : base(seed) { }
#endregion

#region Methods: Public

        /// <summary>
        /// This function retrieves the definition of a CJJ 166-2011 response spectrum function.
        /// </summary>
        /// <param name="name">The name of a CJJ 166-2011 response spectrum function.</param>
        /// <param name="direction">The response spectrum direction.</param>
        /// <param name="peakAcceleration">The peak acceleration, A.</param>
        /// <param name="Tg">The characteristic ground period , Tg &gt; 0.1. [s].</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetFunction(string name,
            ref eSpectrumDirection_CJJ_166_2011 direction,
            ref double peakAcceleration,
            ref double Tg,
            ref double dampingRatio)
        {
            int csiDirection = 0;

            _callCode = _sapModel.Func.FuncRS.GetCJJ1662011(name, ref csiDirection, ref peakAcceleration, ref Tg, ref dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            direction = (eSpectrumDirection_CJJ_166_2011)csiDirection;
        }

        /// <summary>
        /// This function defines a CJJ 166-2011 response spectrum function.
        /// </summary>
        /// <param name="name">The name of a CJJ 166-2011 response spectrum function. <para />
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="direction">The response spectrum direction.</param>
        /// <param name="peakAcceleration">The peak acceleration, A.</param>
        /// <param name="Tg">The characteristic ground period , Tg &gt; 0.1. [s].</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetFunction(string name,
            eSpectrumDirection_CJJ_166_2011 direction,
            double peakAcceleration,
            double Tg,
            double dampingRatio)
        {
            dampingRatio = limitDampingRatio(dampingRatio);
            _callCode = _sapModel.Func.FuncRS.SetCJJ1662011(name, (int)direction, peakAcceleration, Tg, dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion
    }
}
#endif