// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="NBCC_2010.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_SAP2000v19 || BUILD_SAP2000v20 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum
{
    /// <summary>
    /// Response spectrum function from NBCC_2010.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum.ResponseSpectrumFunction" />
    public class NBCC_2010 : ResponseSpectrumFunction
    {
#region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="NBCC_2010" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        protected NBCC_2010(CSiApiSeed seed) : base(seed) { }
#endregion

#region Methods: Public
        /// <summary>
        /// This function defines an NBCC2010 response spectrum function.
        /// </summary>
        /// <param name="name">The name of an NBCC2010 response spectrum function.</param>
        /// <param name="PGA">The peak ground acceleration.</param>
        /// <param name="S02">The spectral acceleration at a 0.2 second period.</param>
        /// <param name="S05">The spectral acceleration at a 0.5 second period.</param>
        /// <param name="S1">The spectral acceleration at a 1 second period.</param>
        /// <param name="S2">The spectral acceleration at a 2 second period.</param>
        /// <param name="siteClass">The site class.</param>
        /// <param name="Fa">The site coefficient at a 0.2 second period.<para />
        /// These items are used only when <paramref name="siteClass" /> = <see cref="eSiteClass_NBCC_2010.F" />.</param>
        /// <param name="Fv">The site coefficient at a 0.5 second period.<para />
        /// These items are used only when <paramref name="siteClass" /> = <see cref="eSiteClass_NBCC_2010.F" />.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetFunction(string name,
            ref double PGA,
            ref double S02,
            ref double S05,
            ref double S1,
            ref double S2,
            ref eSiteClass_NBCC_2010 siteClass,
            ref double Fa,
            ref double Fv,
            ref double dampingRatio)
        {
            int csiSiteClass = 0;

            _callCode = _sapModel.Func.FuncRS.GetNBCC2010(name, ref PGA, ref S02, ref S05, ref S1, ref S2, ref csiSiteClass, ref Fa, ref Fv, ref dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            siteClass = (eSiteClass_NBCC_2010)csiSiteClass;
        }

        /// <summary>
        /// This function defines an NBCC2010 response spectrum function.
        /// </summary>
        /// <param name="name">The name of an NBCC2010 response spectrum function. <para />
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="PGA">The peak ground acceleration.</param>
        /// <param name="S02">The spectral acceleration at a 0.2 second period.</param>
        /// <param name="S05">The spectral acceleration at a 0.5 second period.</param>
        /// <param name="S1">The spectral acceleration at a 1 second period.</param>
        /// <param name="S2">The spectral acceleration at a 2 second period.</param>
        /// <param name="siteClass">The site class.</param>
        /// <param name="Fa">The site coefficient at a 0.2 second period.<para />
        /// These items are used only when <paramref name="siteClass" /> = <see cref="eSiteClass_NBCC_2010.F" />.</param>
        /// <param name="Fv">The site coefficient at a 0.5 second period.<para />
        /// These items are used only when <paramref name="siteClass" /> = <see cref="eSiteClass_NBCC_2010.F" />.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetFunction(string name,
            double PGA,
            double S02,
            double S05,
            double S1,
            double S2,
            eSiteClass_NBCC_2010 siteClass,
            double Fa,
            double Fv,
            double dampingRatio)
        {
            dampingRatio = limitDampingRatio(dampingRatio);
            _callCode = _sapModel.Func.FuncRS.SetNBCC2010(name, PGA, S02, S05, S1, S2, (int)siteClass, Fa, Fv, dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion
    }
}
#endif