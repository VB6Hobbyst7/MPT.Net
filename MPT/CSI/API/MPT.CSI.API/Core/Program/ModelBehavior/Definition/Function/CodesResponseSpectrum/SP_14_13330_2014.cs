// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-09-2017
// ***********************************************************************
// <copyright file="SP_14_13330_2014.cs" company="">
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
    /// Response spectrum function from SP_14_13330_2014.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Function.CodesResponseSpectrum.ResponseSpectrumFunction" />
    public class SP_14_13330_2014 : ResponseSpectrumFunction
    {
#region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="SP_14_13330_2014" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        protected SP_14_13330_2014(CSiApiSeed seed) : base(seed) { }
#endregion

#region Methods: Public
        /// <summary>
        /// This function retrieves the definition of a SP 14.13330.2014 response spectrum function.
        /// </summary>
        /// <param name="name">The name of an SP 14.13330.2014 response spectrum function.</param>
        /// <param name="direction">The direction and structure type for which the response spectrum is generated.</param>
        /// <param name="regionSeismicity">The region seismicity of the construction site.</param>
        /// <param name="soilCategory">The soil category.</param>
        /// <param name="K0Factor">The K0Factor, 0 &lt; K0 &lt;= 2.0.
        /// This is only applicable when <paramref name="direction" /> = <see cref="eSpectrumDirection_SP_14_13330_2014.BuildingHorizontal" /> or <see cref="eSpectrumDirection_SP_14_13330_2014.BridgeHorizontal" />.</param>
        /// <param name="K1Factor">The K1Factor, 0 &lt; K0 &lt;= 1.0.</param>
        /// <param name="KPsiFactor">The KPsiFactor, 0.5 &lt; K0 &lt;= 1.5.
        /// This is only applicable when <paramref name="direction" /> = <see cref="eSpectrumDirection_SP_14_13330_2014.BuildingHorizontal" /> or <see cref="eSpectrumDirection_SP_14_13330_2014.BridgeHorizontal" />.</param>
        /// <param name="isSoilNonlinear">True: Nonlinear soil deformation should be accounted for.
        /// This is only applicable when <paramref name="direction" /> = <see cref="eSpectrumDirection_SP_14_13330_2014.BuildingHorizontal" /> or <see cref="eSpectrumDirection_SP_14_13330_2014.BuildingVertical" /> for buildings and <paramref name="soilCategory" /> = <see cref="eSiteClass_SP_14_13330_2014.III" /> or <see cref="eSiteClass_SP_14_13330_2014.IV" />.</param>
        /// <param name="ASoil">The nonlinear soil deformation factor, 0 &lt; a_soil &lt;= 1.0.
        /// This is only applicable when <paramref name="isSoilNonlinear" /> = True, <paramref name="direction" /> = <see cref="eSpectrumDirection_SP_14_13330_2014.BuildingHorizontal" /> or <see cref="eSpectrumDirection_SP_14_13330_2014.BuildingVertical" /> for buildings and <paramref name="soilCategory" /> = <see cref="eSiteClass_SP_14_13330_2014.III" /> or <see cref="eSiteClass_SP_14_13330_2014.IV" />.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetFunction(string name,
            ref eSpectrumDirection_SP_14_13330_2014 direction,
            ref eSeismicIntensity_SP_14_13330_2014 regionSeismicity,
            ref eSiteClass_SP_14_13330_2014 soilCategory,
            ref double K0Factor,
            ref double K1Factor,
            ref double KPsiFactor,
            ref bool isSoilNonlinear,
            ref double ASoil,
            ref double dampingRatio)
        {
            int csiDirection = 0;
            int csiRegionSeismicity = 0;
            int csiSoilCategory = 0;

            _callCode = _sapModel.Func.FuncRS.GetSP14133302014(name, ref csiDirection, ref csiRegionSeismicity, ref csiSoilCategory, ref K0Factor, ref K1Factor, ref KPsiFactor, ref isSoilNonlinear, ref ASoil, ref dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            direction = (eSpectrumDirection_SP_14_13330_2014)csiDirection;
            regionSeismicity = (eSeismicIntensity_SP_14_13330_2014)csiRegionSeismicity;
            soilCategory = (eSiteClass_SP_14_13330_2014)csiSoilCategory;
        }

        /// <summary>
        /// This function retrieves the definition of a SP 14.13330.2014 response spectrum function.
        /// </summary>
        /// <param name="name">The name of an SP 14.13330.2014 response spectrum function. <para />
        /// If this is an existing function, that function is modified; otherwise, a new function is added.</param>
        /// <param name="direction">The direction and structure type for which the response spectrum is generated.</param>
        /// <param name="regionSeismicity">The region seismicity of the construction site.</param>
        /// <param name="soilCategory">The soil category.</param>
        /// <param name="K0Factor">The K0Factor, 0 &lt; K0 &lt;= 2.0.
        /// This is only applicable when <paramref name="direction" /> = <see cref="eSpectrumDirection_SP_14_13330_2014.BuildingHorizontal" /> or <see cref="eSpectrumDirection_SP_14_13330_2014.BridgeHorizontal" />.</param>
        /// <param name="K1Factor">The K1Factor, 0 &lt; K0 &lt;= 1.0.</param>
        /// <param name="KPsiFactor">The KPsiFactor, 0.5 &lt; K0 &lt;= 1.5.
        /// This is only applicable when <paramref name="direction" /> = <see cref="eSpectrumDirection_SP_14_13330_2014.BuildingHorizontal" /> or <see cref="eSpectrumDirection_SP_14_13330_2014.BridgeHorizontal" />.</param>
        /// <param name="isSoilNonlinear">True: Nonlinear soil deformation should be accounted for.
        /// This is only applicable when <paramref name="direction" /> = <see cref="eSpectrumDirection_SP_14_13330_2014.BuildingHorizontal" /> or <see cref="eSpectrumDirection_SP_14_13330_2014.BuildingVertical" /> for buildings and <paramref name="soilCategory" /> = <see cref="eSiteClass_SP_14_13330_2014.III" /> or <see cref="eSiteClass_SP_14_13330_2014.IV" />.</param>
        /// <param name="ASoil">The nonlinear soil deformation factor, 0 &lt; a_soil &lt;= 1.0.
        /// This is only applicable when <paramref name="isSoilNonlinear" /> = True, <paramref name="direction" /> = <see cref="eSpectrumDirection_SP_14_13330_2014.BuildingHorizontal" /> or <see cref="eSpectrumDirection_SP_14_13330_2014.BuildingVertical" /> for buildings and <paramref name="soilCategory" /> = <see cref="eSiteClass_SP_14_13330_2014.III" /> or <see cref="eSiteClass_SP_14_13330_2014.IV" />.</param>
        /// <param name="dampingRatio">The damping ratio for the function, 0 &lt;= <paramref name="dampingRatio" /> &lt; 1.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetFunction(string name,
            eSpectrumDirection_SP_14_13330_2014 direction,
            eSeismicIntensity_SP_14_13330_2014 regionSeismicity,
            eSiteClass_SP_14_13330_2014 soilCategory,
            double K0Factor,
            double K1Factor,
            double KPsiFactor,
            bool isSoilNonlinear,
            double ASoil,
            double dampingRatio)
        {
            if (K0Factor < 0) { K0Factor = 0; }
            if (K0Factor > 2) { K0Factor = 2; }

            if (K1Factor < 0) { K1Factor = 0; }
            if (K1Factor > 1) { K1Factor = 1; }

            if (KPsiFactor < 0) { KPsiFactor = 0; }
            if (KPsiFactor > 1.5) { KPsiFactor = 1.5; }

            if (ASoil < 0) { ASoil = 0; }
            if (ASoil > 1) { ASoil = 1; }
            dampingRatio = limitDampingRatio(dampingRatio);

            _callCode = _sapModel.Func.FuncRS.SetSP14133302014(name, (int)direction, (int)regionSeismicity, (int)soilCategory, K0Factor, K1Factor, KPsiFactor, isSoilNonlinear, ASoil, dampingRatio);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion
    }
}
#endif