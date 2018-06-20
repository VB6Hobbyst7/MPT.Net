// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="AS_1170_2007.cs" company="">
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
    /// Response spectrum function from AS_1170_2007.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum.ResponseSpectrumFunction" />
    public class AS_1170_2007 : ResponseSpectrumFunction
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="AS_1170_2007" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        protected AS_1170_2007(CSiApiSeed seed) : base(seed) { }
        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves the definition of an AS 1170 2007 response spectrum function.
        /// </summary>
        /// <param name="name">The name of an AS 1170 2007 response spectrum function.</param>
        /// <param name="siteClass">The site class.</param>
        /// <param name="kp">The probability factor, kp.</param>
        /// <param name="Z">The hazard factor, Z.</param>
        /// <param name="Sp">The structural performance factor, Sp.</param>
        /// <param name="u">The structural ductility factor, u.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetFunction(string name,
            ref eSiteClass_AS_1170_2007 siteClass,
            ref double kp,
            ref double Z,
            ref double Sp,
            ref double u,
            ref double dampingRatio)
        {
            int csiSiteClass = 0;

            _callCode = _sapModel.Func.FuncRS.GetAS11702007(name, ref csiSiteClass, ref kp, ref Z, ref Sp, ref u, ref dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            siteClass = (eSiteClass_AS_1170_2007)csiSiteClass;
        }

        /// <summary>
        /// This function defines an AS 1170 2007 response spectrum function.
        /// </summary>
        /// <param name="name">The name of an AS 1170 2007 response spectrum function. <para />
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="siteClass">The site class.</param>
        /// <param name="kp">The probability factor, kp.</param>
        /// <param name="Z">The hazard factor, Z.</param>
        /// <param name="Sp">The structural performance factor, Sp.</param>
        /// <param name="u">The structural ductility factor, u.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetFunction(string name,
            eSiteClass_AS_1170_2007 siteClass,
            double kp,
            double Z,
            double Sp,
            double u,
            double dampingRatio)
        {
            dampingRatio = limitDampingRatio(dampingRatio);
            _callCode = _sapModel.Func.FuncRS.SetAS11702007(name, (int)siteClass, kp, Z, Sp, u, dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
#endif