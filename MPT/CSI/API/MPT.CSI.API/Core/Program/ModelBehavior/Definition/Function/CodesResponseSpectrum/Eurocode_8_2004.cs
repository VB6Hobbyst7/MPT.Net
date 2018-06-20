// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="Eurocode_8_2004.cs" company="">
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
    /// Response spectrum function from Eurocode_8_2004.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum.ResponseSpectrumFunction" />
    public class Eurocode_8_2004 : ResponseSpectrumFunction
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="Eurocode_8_2004" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        protected Eurocode_8_2004(CSiApiSeed seed) : base(seed) { }
        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves the definition of a Eurocode 8 2004 response spectrum function.
        /// </summary>
        /// <param name="name">The name of a Eurocode 8 2004 response spectrum function.</param>
        /// <param name="country">The country for which the Nationally Determined Parameters (NDPs) are specified.</param>
        /// <param name="direction">The ground motion direction.</param>
        /// <param name="spectrumType">The spectrum type.</param>
        /// <param name="groundType">The ground type. <para />
        /// This item only applies when the <paramref name="direction" /> = <see cref="eSpectrumDirection_Eurocode_8_2004.Horizontal" />.</param>
        /// <param name="ag">The design ground acceleration in g, ag.</param>
        /// <param name="S">The soil factor, S.<para />
        /// This item only applies when the <paramref name="direction" /> = <see cref="eSpectrumDirection_Eurocode_8_2004.Horizontal" />.</param>
        /// <param name="AvgOverAg">The vertical ground acceleration divided by the horizontal ground acceleration, Avg/Ag.<para />
        /// This item only applies when the <paramref name="direction" /> = <see cref="eSpectrumDirection_Eurocode_8_2004.Vertical" />.</param>
        /// <param name="Tb">The lower limit of period of the constant spectral acceleration branch, Tb.</param>
        /// <param name="Tc">The upper limit of period of the constant spectral acceleration branch, Tc.</param>
        /// <param name="Td">The period defining the start of the constant displacement range, Td.</param>
        /// <param name="beta">The lower bound factor, Beta.</param>
        /// <param name="q">The behavior factor, q.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetFunction(string name,
            ref eParameters_Eurocode_8_2004 country,
            ref eSpectrumDirection_Eurocode_8_2004 direction,
            ref eSpectrumType_Eurocode_8_2004 spectrumType,
            ref eSiteClass_Eurocode_8_2004 groundType,
            ref double ag,
            ref double S,
            ref double AvgOverAg,
            ref double Tb,
            ref double Tc,
            ref double Td,
            ref double beta,
            ref double q,
            ref double dampingRatio)
        {
            int csiCountry = 0;
            int csiDirection = 0;
            int csiSpectrumType = 0;
            int csiGroundType = 0;

            _callCode = _sapModel.Func.FuncRS.GetEurocode82004_1(name, ref csiCountry, ref csiDirection, ref csiSpectrumType, ref csiGroundType, ref ag, ref S, ref AvgOverAg, ref Tb, ref Tc, ref Td, ref beta, ref q, ref dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            country = (eParameters_Eurocode_8_2004)csiCountry;
            direction = (eSpectrumDirection_Eurocode_8_2004)csiDirection;
            spectrumType = (eSpectrumType_Eurocode_8_2004)csiSpectrumType;
            groundType = (eSiteClass_Eurocode_8_2004)csiGroundType;
        }

        /// <summary>
        /// This function defines a Eurocode 8 2004 response spectrum function.
        /// </summary>
        /// <param name="name">The name of a Eurocode 8 2004 response spectrum function. <para />
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="country">The country for which the Nationally Determined Parameters (NDPs) are specified.</param>
        /// <param name="direction">The ground motion direction.</param>
        /// <param name="spectrumType">The spectrum type.</param>
        /// <param name="groundType">The ground type. <para />
        /// This item only applies when the <paramref name="direction" /> = <see cref="eSpectrumDirection_Eurocode_8_2004.Horizontal" />.</param>
        /// <param name="ag">The design ground acceleration in g, ag.</param>
        /// <param name="S">The soil factor, S.<para />
        /// This item only applies when the <paramref name="direction" /> = <see cref="eSpectrumDirection_Eurocode_8_2004.Horizontal" />.</param>
        /// <param name="AvgOverAg">The vertical ground acceleration divided by the horizontal ground acceleration, Avg/Ag.<para />
        /// This item only applies when the <paramref name="direction" /> = <see cref="eSpectrumDirection_Eurocode_8_2004.Vertical" />.</param>
        /// <param name="Tb">The lower limit of period of the constant spectral acceleration branch, Tb.</param>
        /// <param name="Tc">The upper limit of period of the constant spectral acceleration branch, Tc.</param>
        /// <param name="Td">The period defining the start of the constant displacement range, Td.</param>
        /// <param name="beta">The lower bound factor, Beta.</param>
        /// <param name="q">The behavior factor, q.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetFunction(string name,
            eParameters_Eurocode_8_2004 country,
            eSpectrumDirection_Eurocode_8_2004 direction,
            eSpectrumType_Eurocode_8_2004 spectrumType,
            eSiteClass_Eurocode_8_2004 groundType,
            double ag,
            double S,
            double AvgOverAg,
            double Tb,
            double Tc,
            double Td,
            double beta,
            double q,
            double dampingRatio)
        {
            dampingRatio = limitDampingRatio(dampingRatio);
            _callCode = _sapModel.Func.FuncRS.SetEurocode82004_1(name, (int)country, (int)direction, (int)spectrumType, (int)groundType, ag, S, AvgOverAg, Tb, Tc, Td, beta, q, dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
#endif