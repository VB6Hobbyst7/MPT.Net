// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="NBCC_1995.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    /// <summary>
    /// Response spectrum function from NBCC_1995.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum.ResponseSpectrumFunction" />
    public class NBCC_1995 : ResponseSpectrumFunction
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="NBCC_1995" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        protected NBCC_1995(CSiApiSeed seed) : base(seed) { }
        #endregion

        #region Methods: Public
        /// <summary>
        /// This function defines an NBCC95 response spectrum function.
        /// </summary>
        /// <param name="name">The name of an NBCC95 response spectrum function.</param>
        /// <param name="ZVR">The zonal velocity ratio.</param>
        /// <param name="ZA">The acceleration-related seismic zone.</param>
        /// <param name="Zv">The velocity-related seismic zone.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetFunction(string name,
            ref double ZVR,
            ref int ZA,
            ref int Zv,
            ref double dampingRatio)
        {
            _callCode = _sapModel.Func.FuncRS.GetNBCC95(name, ref ZVR, ref ZA, ref Zv, ref dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function defines an NBCC95 response spectrum function.
        /// </summary>
        /// <param name="name">The name of an NBCC95 response spectrum function. <para />
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="ZVR">The zonal velocity ratio.</param>
        /// <param name="ZA">The acceleration-related seismic zone.</param>
        /// <param name="Zv">The velocity-related seismic zone.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetFunction(string name,
            double ZVR,
            int ZA,
            int Zv,
            double dampingRatio)
        {
            dampingRatio = limitDampingRatio(dampingRatio);
            _callCode = _sapModel.Func.FuncRS.SetNBCC95(name, ZVR, ZA, Zv, dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
#endif