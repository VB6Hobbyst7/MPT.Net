// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="AASHTO_2006.cs" company="">
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
    /// Response spectrum function from AASHTO_2006.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum.ResponseSpectrumFunction" />
    public class AASHTO_2006 : ResponseSpectrumFunction
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="AASHTO_2006" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        protected AASHTO_2006(CSiApiSeed seed) : base(seed) { }
        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves the definition of an AASHTO2006 response spectrum function.
        /// </summary>
        /// <param name="name">The name of an AASHTO2006 response spectrum function.</param>
        /// <param name="A">The acceleration coefficient, A.</param>
        /// <param name="soilProfileType">The soil profile type.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetFunction(string name,
            ref double A,
            ref eSiteClass_AASHTO_2006 soilProfileType,
            ref double dampingRatio)
        {
            int csiSoilProfileType = 0;

            _callCode = _sapModel.Func.FuncRS.GetAASHTO2006(name, ref A, ref csiSoilProfileType, ref dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            soilProfileType = (eSiteClass_AASHTO_2006)csiSoilProfileType;
        }

        /// <summary>
        /// This function defines an AASHTO2006 response spectrum function.
        /// </summary>
        /// <param name="name">The name of an AASHTO2006 response spectrum function. <para />
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="A">The acceleration coefficient, A.</param>
        /// <param name="soilProfileType">The soil profile type.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetFunction(string name,
            double A,
            eSiteClass_AASHTO_2006 soilProfileType,
            double dampingRatio)
        {
            dampingRatio = limitDampingRatio(dampingRatio);
            _callCode = _sapModel.Func.FuncRS.SetAASHTO2006(name, A, (int)soilProfileType, dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
#endif
