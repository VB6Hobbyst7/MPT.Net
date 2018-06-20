// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="AASHTO_2007.cs" company="">
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
    /// Response spectrum function from AASHTO_2007.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum.ResponseSpectrumFunction" />
    public class AASHTO_2007 : ResponseSpectrumFunction
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="AASHTO_2007" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        protected AASHTO_2007(CSiApiSeed seed) : base(seed) { }
        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves the definition of an AASHTO 2007 response spectrum function.
        /// </summary>
        /// <param name="name">The name of an AASHTO 2007 response spectrum function.</param>
        /// <param name="seismicCoefficientOption">The seismic coefficient option.</param>
        /// <param name="latitude">The latitude for which the seismic coefficients are obtained. <para />
        /// These items are used only when <paramref name="seismicCoefficientOption" /> = <see cref="eSeismicCoefficient_AASHTO_2007.SsAndS1FromUSGSLatLong" />.</param>
        /// <param name="longitude">The longitude for which the seismic coefficients are obtained. <para />
        /// These items are used only when <paramref name="seismicCoefficientOption" /> = <see cref="eSeismicCoefficient_AASHTO_2007.SsAndS1FromUSGSLatLong" />.</param>
        /// <param name="zipCode">The zip code for which the seismic coefficients are obtained. <para />
        /// This item is used only when <paramref name="seismicCoefficientOption" /> = <see cref="eSeismicCoefficient_AASHTO_2007.SsAndS1FromUSGSZipCode" />.</param>
        /// <param name="Ss">The design spectral acceleration at short periods, Ss.<para />
        /// These items are used only when <paramref name="seismicCoefficientOption" /> = <see cref="eSeismicCoefficient_AASHTO_2007.SsAndS1UserDefined" />.</param>
        /// <param name="S1">The design spectral acceleration at a one second period, S1.<para />
        /// These items are used only when <paramref name="seismicCoefficientOption" /> = <see cref="eSeismicCoefficient_AASHTO_2007.SsAndS1UserDefined" />.</param>
        /// <param name="PGA">The design spectral peak ground acceleration.<para />
        /// These items are used only when <paramref name="seismicCoefficientOption" /> = <see cref="eSeismicCoefficient_AASHTO_2007.SsAndS1UserDefined" />.</param>
        /// <param name="siteClass">The site class.</param>
        /// <param name="Fa">The site coefficients Fa. <para />
        /// These items are used only when <paramref name="siteClass" /> = <see cref="eSiteClass_AASHTO_2007.F" />.</param>
        /// <param name="Fv">The site coefficients Fv.  <para />
        /// These items are used only when <paramref name="siteClass" /> = <see cref="eSiteClass_AASHTO_2007.F" />.</param>
        /// <param name="Fpga">The site peak ground acceleration.<para />
        /// These items are used only when <paramref name="siteClass" /> = <see cref="eSiteClass_AASHTO_2007.F" />.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetFunction(string name,
            ref eSeismicCoefficient_AASHTO_2007 seismicCoefficientOption,
            ref double latitude,
            ref double longitude,
            ref string zipCode,
            ref double Ss,
            ref double S1,
            ref double PGA,
            ref eSiteClass_AASHTO_2007 siteClass,
            ref double Fa,
            ref double Fv,
            ref double Fpga,
            ref double dampingRatio)
        {
            int csiSeismicCoefficient = 0;
            int csiSiteClass = 0;

            _callCode = _sapModel.Func.FuncRS.GetAASHTO2007(name, ref csiSeismicCoefficient, ref latitude, ref longitude, ref zipCode, ref Ss, ref S1, ref PGA, ref csiSiteClass, ref Fa, ref Fv, ref Fpga, ref dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            seismicCoefficientOption = (eSeismicCoefficient_AASHTO_2007)csiSeismicCoefficient;
            siteClass = (eSiteClass_AASHTO_2007)csiSiteClass;
        }

        /// <summary>
        /// This function defines an AASHTO 2007 response spectrum function.
        /// </summary>
        /// <param name="name">The name of an AASHTO 2007 response spectrum function. <para />
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="seismicCoefficientOption">The seismic coefficient option.</param>
        /// <param name="latitude">The latitude for which the seismic coefficients are obtained. <para />
        /// These items are used only when <paramref name="seismicCoefficientOption" /> = <see cref="eSeismicCoefficient_AASHTO_2007.SsAndS1FromUSGSLatLong" />.</param>
        /// <param name="longitude">The longitude for which the seismic coefficients are obtained. <para />
        /// These items are used only when <paramref name="seismicCoefficientOption" /> = <see cref="eSeismicCoefficient_AASHTO_2007.SsAndS1FromUSGSLatLong" />.</param>
        /// <param name="zipCode">The zip code for which the seismic coefficients are obtained. <para />
        /// This item is used only when <paramref name="seismicCoefficientOption" /> = <see cref="eSeismicCoefficient_AASHTO_2007.SsAndS1FromUSGSZipCode" />.</param>
        /// <param name="Ss">The design spectral acceleration at short periods, Ss.<para />
        /// These items are used only when <paramref name="seismicCoefficientOption" /> = <see cref="eSeismicCoefficient_AASHTO_2007.SsAndS1UserDefined" />.</param>
        /// <param name="S1">The design spectral acceleration at a one second period, S1.<para />
        /// These items are used only when <paramref name="seismicCoefficientOption" /> = <see cref="eSeismicCoefficient_AASHTO_2007.SsAndS1UserDefined" />.</param>
        /// <param name="PGA">The design spectral peak ground acceleration.<para />
        /// These items are used only when <paramref name="seismicCoefficientOption" /> = <see cref="eSeismicCoefficient_AASHTO_2007.SsAndS1UserDefined" />.</param>
        /// <param name="siteClass">The site class.</param>
        /// <param name="Fa">The site coefficients Fa. <para />
        /// These items are used only when <paramref name="siteClass" /> = <see cref="eSiteClass_AASHTO_2007.F" />.</param>
        /// <param name="Fv">The site coefficients Fv.  <para />
        /// These items are used only when <paramref name="siteClass" /> = <see cref="eSiteClass_AASHTO_2007.F" />.</param>
        /// <param name="Fpga">The site peak ground acceleration.<para />
        /// These items are used only when <paramref name="siteClass" /> = <see cref="eSiteClass_AASHTO_2007.F" />.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetFunction(string name,
            eSeismicCoefficient_AASHTO_2007 seismicCoefficientOption,
            double latitude,
            double longitude,
            string zipCode,
            double Ss,
            double S1,
            double PGA,
            eSiteClass_AASHTO_2007 siteClass,
            double Fa,
            double Fv,
            double Fpga,
            double dampingRatio)
        {
            dampingRatio = limitDampingRatio(dampingRatio);
            _callCode = _sapModel.Func.FuncRS.SetAASHTO2007(name, (int)seismicCoefficientOption, latitude, longitude, zipCode, Ss, S1, PGA, (int)siteClass, Fa, Fv, Fpga, dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
#endif