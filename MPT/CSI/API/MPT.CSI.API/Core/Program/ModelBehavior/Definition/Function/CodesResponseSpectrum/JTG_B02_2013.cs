// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="JTG_B02_2013.cs" company="">
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
    /// Response spectrum function from JTG_B02_2013.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum.ResponseSpectrumFunction" />
    public class JTG_B02_2013 : ResponseSpectrumFunction
    {
#region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="JTG_B02_2013" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        protected JTG_B02_2013(CSiApiSeed seed) : base(seed) { }
#endregion

#region Methods: Public
        /// <summary>
        /// This function retrieves the definition of a JTG B02-2013 response spectrum function.
        /// </summary>
        /// <param name="name">The name of a JTG B02-2013 response spectrum function.</param>
        /// <param name="direction">The response spectrum direction.</param>
        /// <param name="peakAcceleration">The peak acceleration, A.</param>
        /// <param name="Tg">The characteristic ground period, Tg &gt; 0.1. [s].</param>
        /// <param name="Ci">The importance coefficient.</param>
        /// <param name="Cs">The site soil coefficient.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetFunction(string name,
            ref eSpectrumDirection_JTG_B02_2013 direction,
            ref double peakAcceleration,
            ref double Tg,
            ref double Ci,
            ref double Cs,
            ref double dampingRatio)
        {
            int csiDirection = 0;

            _callCode = _sapModel.Func.FuncRS.GetJTGB022013(name, ref csiDirection, ref peakAcceleration, ref Tg, ref Ci, ref Cs, ref dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            direction = (eSpectrumDirection_JTG_B02_2013)csiDirection;
        }

        /// <summary>
        /// This function defines a JTG B02-2013 response spectrum function.
        /// </summary>
        /// <param name="name">The name of a JTG B02-2013 response spectrum function. <para />
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="direction">The response spectrum direction.</param>
        /// <param name="peakAcceleration">The peak acceleration, A.</param>
        /// <param name="Tg">The characteristic ground period, Tg &gt; 0.1. [s].</param>
        /// <param name="Ci">The importance coefficient.</param>
        /// <param name="Cs">The site soil coefficient.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetFunction(string name,
            eSpectrumDirection_JTG_B02_2013 direction,
            double peakAcceleration,
            double Tg,
            double Ci,
            double Cs,
            double dampingRatio)
        {
            dampingRatio = limitDampingRatio(dampingRatio);
            _callCode = _sapModel.Func.FuncRS.SetJTGB022013(name, (int)direction, peakAcceleration, Tg, Ci, Cs, dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion
    }
}
#endif