// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="NZS_1170_2004.cs" company="">
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
    /// Response spectrum function from NZS_1170_2004.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum.ResponseSpectrumFunction" />
    public class NZS_1170_2004 : ResponseSpectrumFunction
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="NZS_1170_2004" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        protected NZS_1170_2004(CSiApiSeed seed) : base(seed) { }
        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves the definition of an NZS 1170 2004 response spectrum function.
        /// </summary>
        /// <param name="name">The name of an NZS 1170 2004 response spectrum function.</param>
        /// <param name="siteClass">The site class.</param>
        /// <param name="Z">The hazard factor, Z.</param>
        /// <param name="R">The return period factor, R.</param>
        /// <param name="distanceToFault">Distance to the fault in km, used to calculate the near fault factor.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetFunction(string name,
            ref eSiteClass_NZS_1170_2004 siteClass,
            ref double Z,
            ref double R,
            ref double distanceToFault,
            ref double dampingRatio)
        {
            int csiSiteClass = 0;

            _callCode = _sapModel.Func.FuncRS.GetNZS11702004(name, ref csiSiteClass, ref Z, ref R, ref distanceToFault, ref dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            siteClass = (eSiteClass_NZS_1170_2004)csiSiteClass;
        }

        /// <summary>
        /// This function defines an NZS 1170 2004 response spectrum function.
        /// </summary>
        /// <param name="name">The name of an NZS 1170 2004 response spectrum function. <para />
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="siteClass">The site class.</param>
        /// <param name="Z">The hazard factor, Z.</param>
        /// <param name="R">The return period factor, R.</param>
        /// <param name="distanceToFault">Distance to the fault in km, used to calculate the near fault factor.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetFunction(string name,
            eSiteClass_NZS_1170_2004 siteClass,
            double Z,
            double R,
            double distanceToFault,
            double dampingRatio)
        {
            dampingRatio = limitDampingRatio(dampingRatio);
            _callCode = _sapModel.Func.FuncRS.SetNZS11702004(name, (int)siteClass, Z, R, distanceToFault, dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
#endif