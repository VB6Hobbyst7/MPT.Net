// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="NBCC_2015.cs" company="">
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
    /// Response spectrum function from NBCC_2015.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum.ResponseSpectrumFunction" />
    public class NBCC_2015 : ResponseSpectrumFunction
    {
#region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="NBCC_2015" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        protected NBCC_2015(CSiApiSeed seed) : base(seed) { }
#endregion

#region Methods: Public
        /// <summary>
        /// This function retrieves the definition of an NBCC2015 response spectrum function.
        /// </summary>
        /// <param name="name">The name of an NBCC2015 response spectrum function.</param>
        /// <param name="PGA">The peak ground acceleration.</param>
        /// <param name="S02">The spectral acceleration at a 0.2 second period.</param>
        /// <param name="S05">The spectral acceleration at a 0.5 second period.</param>
        /// <param name="S1">The spectral acceleration at a 1 second period.</param>
        /// <param name="S2">The spectral acceleration at a 2 second period.</param>
        /// <param name="S5">The spectral acceleration at a 5 second period.</param>
        /// <param name="S10">The spectral acceleration at a 10 second period.</param>
        /// <param name="siteClass">The site class.</param>
        /// <param name="F02">The site coefficient at a 0.2 second period.<para />
        /// These items are used only when <paramref name="siteClass" /> = <see cref="eSiteClass_NBCC_2015.F" />.</param>
        /// <param name="F05">The site coefficient at a 0.5 second period.<para />
        /// These items are used only when <paramref name="siteClass" /> = <see cref="eSiteClass_NBCC_2015.F" />.</param>
        /// <param name="F1">The site coefficient at a 1 second period.<para />
        /// These items are used only when <paramref name="siteClass" /> = <see cref="eSiteClass_NBCC_2015.F" />.</param>
        /// <param name="F2">The site coefficient at a 2 second period.<para />
        /// These items are used only when <paramref name="siteClass" /> = <see cref="eSiteClass_NBCC_2015.F" />.</param>
        /// <param name="F5">The site coefficient at a 5 second period.<para />
        /// These items are used only when <paramref name="siteClass" /> = <see cref="eSiteClass_NBCC_2015.F" />.</param>
        /// <param name="F10">The site coefficient at a 10 second period.<para />
        /// These items are used only when <paramref name="siteClass" /> = <see cref="eSiteClass_NBCC_2015.F" />.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetFunction(string name,
            ref double PGA,
            ref double S02,
            ref double S05,
            ref double S1,
            ref double S2,
            ref double S5,
            ref double S10,
            ref eSiteClass_NBCC_2015 siteClass,
            ref double F02,
            ref double F05,
            ref double F1,
            ref double F2,
            ref double F5,
            ref double F10,
            ref double dampingRatio)
        {
            int csiSiteClass = 0;

            _callCode = _sapModel.Func.FuncRS.GetNBCC2015(name, ref PGA, ref S02, ref S05, ref S1, ref S2, ref S5, ref S10, ref csiSiteClass, ref F02, ref F05, ref F1, ref F2, ref F5, ref F10, ref dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            siteClass = (eSiteClass_NBCC_2015)csiSiteClass;
        }

        /// <summary>
        /// This function retrieves the definition of an NBCC2015 response spectrum function.
        /// </summary>
        /// <param name="name">The name of an NBCC2015 response spectrum function. <para />
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="PGA">The peak ground acceleration.</param>
        /// <param name="S02">The spectral acceleration at a 0.2 second period.</param>
        /// <param name="S05">The spectral acceleration at a 0.5 second period.</param>
        /// <param name="S1">The spectral acceleration at a 1 second period.</param>
        /// <param name="S2">The spectral acceleration at a 2 second period.</param>
        /// <param name="S5">The spectral acceleration at a 5 second period.</param>
        /// <param name="S10">The spectral acceleration at a 10 second period.</param>
        /// <param name="siteClass">The site class.</param>
        /// <param name="F02">The site coefficient at a 0.2 second period.<para />
        /// These items are used only when <paramref name="siteClass" /> = <see cref="eSiteClass_NBCC_2015.F" />.</param>
        /// <param name="F05">The site coefficient at a 0.5 second period.<para />
        /// These items are used only when <paramref name="siteClass" /> = <see cref="eSiteClass_NBCC_2015.F" />.</param>
        /// <param name="F1">The site coefficient at a 1 second period.<para />
        /// These items are used only when <paramref name="siteClass" /> = <see cref="eSiteClass_NBCC_2015.F" />.</param>
        /// <param name="F2">The site coefficient at a 2 second period.<para />
        /// These items are used only when <paramref name="siteClass" /> = <see cref="eSiteClass_NBCC_2015.F" />.</param>
        /// <param name="F5">The site coefficient at a 5 second period.<para />
        /// These items are used only when <paramref name="siteClass" /> = <see cref="eSiteClass_NBCC_2015.F" />.</param>
        /// <param name="F10">The site coefficient at a 10 second period.<para />
        /// These items are used only when <paramref name="siteClass" /> = <see cref="eSiteClass_NBCC_2015.F" />.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetFunction(string name,
            double PGA,
            double S02,
            double S05,
            double S1,
            double S2,
            double S5,
            double S10,
            eSiteClass_NBCC_2015 siteClass,
            double F02,
            double F05,
            double F1,
            double F2,
            double F5,
            double F10,
            double dampingRatio)
        {
            dampingRatio = limitDampingRatio(dampingRatio);
            _callCode = _sapModel.Func.FuncRS.SetNBCC2015(name, PGA, S02, S05, S1, S2, S5, S10, (int)siteClass, F02, F05, F1, F2, F5, F10, dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion
    }
}
#endif