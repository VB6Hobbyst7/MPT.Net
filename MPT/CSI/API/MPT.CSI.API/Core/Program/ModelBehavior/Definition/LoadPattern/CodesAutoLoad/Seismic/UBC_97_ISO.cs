// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-10-2017
// ***********************************************************************
// <copyright file="UBC_97_ISO.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadLateralCode.Seismic;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic
{
    /// <summary>
    /// Represents the UBC_94 auto seismic load in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic.AutoSeismicLoad" />
    /// <seealso cref="AutoSeismicLoad" />
    public class UBC_97_ISO : AutoSeismicLoad
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="UBC_97_ISO" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public UBC_97_ISO(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves auto seismic loading parameters for seismically isolated buildings using the 1997 UBC code.
        /// </summary>
        /// <param name="name">The name of an existing Quake-type load pattern with a corresponding auto seismic load assignment.</param>
        /// <param name="loadDirection">The seismic load direction.</param>
        /// <param name="eccentricity">The eccentricity ratio that applies to all diaphragms.</param>
        /// <param name="userSpecifiedHeights">True: Top and bottom elevations of the seismic load are user specified.
        /// Else, the program determines the elevations.</param>
        /// <param name="coordinateTopZ">Global Z-coordinate at the highest level where auto seismic loads are applied. [L].
        /// This only applies when <paramref name="userSpecifiedHeights" /> = True.</param>
        /// <param name="coordinateBottomZ">Global Z-coordinate at the lowest level where auto seismic loads are applied. [L].
        /// This only applies when <paramref name="userSpecifiedHeights" /> = True.</param>
        /// <param name="soilType">Type of the soil.
        /// This item is applicable only when <paramref name="seismicCoefficientSource" /> = <see cref="eSource.PerCode" /></param>
        /// <param name="Z">This is 0.075, 0.15, 0.2, 0.3 or 0.4, indicating the seismic zone factor.
        /// This item is applicable only when <paramref name="seismicCoefficientSource" /> = <see cref="eSource.PerCode" /></param>
        /// <param name="seismicCoefficientSource">The seismic coefficient source.</param>
        /// <param name="Cv">The seismic coefficient, Cv.</param>
        /// <param name="nearSourceSource">Indicates the source  of the near source factor coefficients Na and Nv.
        /// This item is applicable only when <paramref name="seismicCoefficientSource" /> = <see cref="eSource.PerCode" /> and <paramref name="Z" /> = 0.4.</param>
        /// <param name="seismicSourceType">Type of the seismic source.
        /// This item is applicable only when <paramref name="seismicCoefficientSource" /> = <see cref="eSource.PerCode" /> and <paramref name="Z" /> = 0.4 and <paramref name="nearSourceSource" /> = <see cref="eSource.PerCode" />.</param>
        /// <param name="distanceToSeismicSource">This is the distance to the seismic source in kilometers.
        /// This item is applicable only when <paramref name="seismicCoefficientSource" /> = <see cref="eSource.PerCode" /> and <paramref name="Z" /> = 0.4 and <paramref name="nearSourceSource" /> = <see cref="eSource.PerCode" />.</param>
        /// <param name="Nv">The near source factor coefficient, Nv.
        /// This item is applicable only when <paramref name="seismicCoefficientSource" /> = <see cref="eSource.UserDefined" />.</param>
        /// <param name="Ri">The overstrength factor, Ri.</param>
        /// <param name="Bd">The coefficient for damping.</param>
        /// <param name="KDmax">The maximum effective stiffness of the isolation system. [F/L].</param>
        /// <param name="KDmin">The minimum effective stiffness of the isolation system. [F/L].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoad(string name,
            ref eSeismicLoadDirection loadDirection,
            ref double eccentricity,
            ref bool userSpecifiedHeights,
            ref double coordinateTopZ,
            ref double coordinateBottomZ,
            ref eSiteClass_UBC_97 soilType,
            ref double Z,
            ref eSource seismicCoefficientSource,
            ref double Cv,
            ref eSource nearSourceSource,
            ref eSourceType_UBC_97 seismicSourceType,
            ref double distanceToSeismicSource,
            ref double Nv,
            ref double Ri,
            ref double Bd,
            ref double KDmax,
            ref double KDmin)
        {
            int csiLoadDirection = 0;
            int csiSoilType = 0;
            int csiSeismicCoefficientSource = 0;
            int csiNearSourceSource = 0;
            int csiSeismicSourceType = 0;

            _callCode = _sapModel.LoadPatterns.AutoSeismic.GetUBC97Iso(name,
                            ref csiLoadDirection,
                            ref eccentricity,
                            ref userSpecifiedHeights,
                            ref coordinateTopZ,
                            ref coordinateBottomZ,
                            ref csiSeismicCoefficientSource,
                            ref csiSoilType,
                            ref Z,
                            ref Cv,
                            ref csiNearSourceSource,
                            ref csiSeismicSourceType,
                            ref distanceToSeismicSource,
                            ref Nv,
                            ref Ri,
                            ref Bd,
                            ref KDmax,
                            ref KDmin);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            loadDirection = (eSeismicLoadDirection)csiLoadDirection;
            soilType = (eSiteClass_UBC_97)csiSoilType;
            seismicCoefficientSource = (eSource)csiSeismicCoefficientSource;
            nearSourceSource = (eSource)csiNearSourceSource;
            seismicSourceType = (eSourceType_UBC_97)csiSeismicSourceType;
        }

        /// <summary>
        /// This function assigns auto seismic loading parameters for seismically isolated buildings using the 1997 UBC code.
        /// </summary>
        /// <param name="name">The name of an existing Quake-type load pattern with a corresponding auto seismic load assignment.</param>
        /// <param name="loadDirection">The seismic load direction.</param>
        /// <param name="eccentricity">The eccentricity ratio that applies to all diaphragms.</param>
        /// <param name="userSpecifiedHeights">True: Top and bottom elevations of the seismic load are user specified.
        /// Else, the program determines the elevations.</param>
        /// <param name="coordinateTopZ">Global Z-coordinate at the highest level where auto seismic loads are applied. [L].
        /// This only applies when <paramnamef name="userSpecifiedHeights" /> = True.</param>
        /// <param name="coordinateBottomZ">Global Z-coordinate at the lowest level where auto seismic loads are applied. [L].
        /// This only applies when <paramnamef name="userSpecifiedHeights" /> = True.</param>
        /// <param name="soilType">Type of the soil.
        /// This item is applicable only when <paramnamef name="seismicCoefficientSource" /> = <see cref="eSource.PerCode" /></param>
        /// <param name="Z">This is 0.075, 0.15, 0.2, 0.3 or 0.4, indicating the seismic zone factor.
        /// This item is applicable only when <paramnamef name="seismicCoefficientSource" /> = <see cref="eSource.PerCode" /></param>
        /// <param name="seismicCoefficientSource">The seismic coefficient source.</param>
        /// <param name="Cv">The seismic coefficient, Cv.</param>
        /// <param name="nearSourceSource">Indicates the source  of the near source factor coefficients Na and Nv.
        /// This item is applicable only when <paramnamef name="seismicCoefficientSource" /> = <see cref="eSource.PerCode" /> and <paramnamef name="Z" /> = 0.4.</param>
        /// <param name="seismicSourceType">Type of the seismic source.
        /// This item is applicable only when <paramnamef name="seismicCoefficientSource" /> = <see cref="eSource.PerCode" /> and <paramnamef name="Z" /> = 0.4 and <paramnamef name="nearSourceSource" /> = <see cref="eSource.PerCode" />.</param>
        /// <param name="distanceToSeismicSource">This is the distance to the seismic source in kilometers.
        /// This item is applicable only when <paramnamef name="seismicCoefficientSource" /> = <see cref="eSource.PerCode" /> and <paramnamef name="Z" /> = 0.4 and <paramnamef name="nearSourceSource" /> = <see cref="eSource.PerCode" />.</param>
        /// <param name="Nv">The near source factor coefficient, Nv.
        /// This item is applicable only when <paramnamef name="seismicCoefficientSource" /> = <see cref="eSource.UserDefined" />.</param>
        /// <param name="Ri">The overstrength factor, Ri.</param>
        /// <param name="Bd">The coefficient for damping.</param>
        /// <param name="KDmax">The maximum effective stiffness of the isolation system. [F/L].</param>
        /// <param name="KDmin">The minimum effective stiffness of the isolation system. [F/L].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoad(string name,
            eSeismicLoadDirection loadDirection,
            double eccentricity,
            bool userSpecifiedHeights,
            double coordinateTopZ,
            double coordinateBottomZ,
            eSiteClass_UBC_97 soilType,
            double Z,
            eSource seismicCoefficientSource,
            double Cv,
            eSource nearSourceSource,
            eSourceType_UBC_97 seismicSourceType,
            double distanceToSeismicSource,
            double Nv,
            double Ri,
            double Bd,
            double KDmax,
            double KDmin)
        {
            _callCode = _sapModel.LoadPatterns.AutoSeismic.SetUBC97Iso(name,
                            (int)loadDirection,
                            eccentricity,
                            userSpecifiedHeights,
                            coordinateTopZ,
                            coordinateBottomZ,
                            (int)seismicCoefficientSource,
                            (int)soilType,
                            Z,
                            Cv,
                            (int)nearSourceSource,
                            (int)seismicSourceType,
                            distanceToSeismicSource,
                            Nv,
                            Ri,
                            Bd,
                            KDmax,
                            KDmin);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
#endif
