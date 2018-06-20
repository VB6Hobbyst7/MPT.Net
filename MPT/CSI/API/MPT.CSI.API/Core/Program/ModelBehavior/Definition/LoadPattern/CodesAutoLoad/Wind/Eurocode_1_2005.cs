// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-10-2017
// ***********************************************************************
// <copyright file="Eurocode_1_2005.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wind
{
    /// <summary>
    /// Represents the Eurocode_1_2005 auto wind load in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wind.AutoWindLoad" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class Eurocode_1_2005 : AutoWindLoad
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="Eurocode_1_2005" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public Eurocode_1_2005(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves auto wind loading parameters for Eurocode 1 2005.
        /// </summary>
        /// <param name="name">The name of an existing Wind-type load case with an auto wind assignment.</param>
        /// <param name="exposureFrom">The source of the wind exposure.</param>
        /// <param name="directionAngle">The direction angle for the wind load.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="Cpw">The windward coefficient, Cp.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="Cpl">The leeward coefficient, Cp.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="userSpecifiedHeights">True: Top and bottom elevations of the wind load are user specified.
        /// Else, the elevations are determined by the program.</param>
        /// <param name="coordinateTopZ">Global Z-coordinate at the highest level where auto wind loads are applied. [L].</param>
        /// <param name="coordinateBottomZ">Global Z-coordinate at the lowest level where auto wind loads are applied. [L].</param>
        /// <param name="windSpeed">The basic wind speed, vb, in meters per second.</param>
        /// <param name="terrainCategory">The terrain category.</param>
        /// <param name="orography">The orography factor, Co.</param>
        /// <param name="k1">The turbulence factor, k1.</param>
        /// <param name="CsCd">The structural factor, CsCd.</param>
        /// <param name="rho">The air density in kg/m^3, Rho.</param>
        /// <param name="userSpecifiedExposure">True: Wind exposure widths are provided by the user.
        /// Else, wind exposure widths are calculated by the program from the extents of the diaphragms.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoad(string name,
            ref eWindExposure exposureFrom,
            ref double directionAngle,
            ref double Cpw,
            ref double Cpl,
            ref bool userSpecifiedHeights,
            ref double coordinateTopZ,
            ref double coordinateBottomZ,
            ref double windSpeed,
            ref eTerrainCategory terrainCategory,
            ref double orography,
            ref double k1,
            ref double CsCd,
            ref double rho,
            ref bool userSpecifiedExposure)
        {
            int csiExposureFrom = 0;
            int csiTerrainCategory = 0;

            _callCode = _sapModel.LoadPatterns.AutoWind.GetEurocode12005_1(name,
                            ref csiExposureFrom,
                            ref directionAngle,
                            ref Cpw,
                            ref Cpl,
                            ref userSpecifiedHeights,
                            ref coordinateTopZ,
                            ref coordinateBottomZ,
                            ref windSpeed,
                            ref csiTerrainCategory,
                            ref orography,
                            ref k1,
                            ref CsCd,
                            ref rho,
                            ref userSpecifiedExposure);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            exposureFrom = (eWindExposure)csiExposureFrom;
            terrainCategory = (eTerrainCategory)csiTerrainCategory;
        }


        /// <summary>
        /// This function assigns auto wind loading parameters for Eurocode 1 2005.
        /// </summary>
        /// <param name="name">The name of an existing Wind-type load case with an auto wind assignment.</param>
        /// <param name="exposureFrom">The source of the wind exposure.</param>
        /// <param name="directionAngle">The direction angle for the wind load.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="Cpw">The windward coefficient, Cp.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="Cpl">The leeward coefficient, Cp.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="userSpecifiedHeights">True: Top and bottom elevations of the wind load are user specified.
        /// Else, the elevations are determined by the program.</param>
        /// <param name="coordinateTopZ">Global Z-coordinate at the highest level where auto wind loads are applied. [L].</param>
        /// <param name="coordinateBottomZ">Global Z-coordinate at the lowest level where auto wind loads are applied. [L].</param>
        /// <param name="windSpeed">The basic wind speed, vb, in meters per second.</param>
        /// <param name="terrainCategory">The terrain category.</param>
        /// <param name="orography">The orography factor, Co.</param>
        /// <param name="k1">The turbulence factor, k1.</param>
        /// <param name="CsCd">The structural factor, CsCd.</param>
        /// <param name="rho">The air density in kg/m^3, Rho.</param>
        /// <param name="userSpecifiedExposure">True: Wind exposure widths are provided by the user.
        /// Else, wind exposure widths are calculated by the program from the extents of the diaphragms.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoad(string name,
            eWindExposure exposureFrom,
            double directionAngle,
            double Cpw,
            double Cpl,
            bool userSpecifiedHeights,
            double coordinateTopZ,
            double coordinateBottomZ,
            double windSpeed,
            eTerrainCategory terrainCategory,
            double orography,
            double k1,
            double CsCd,
            double rho,
            bool userSpecifiedExposure = false)
        {
            _callCode = _sapModel.LoadPatterns.AutoWind.SetEurocode12005_1(name,
                            (int)exposureFrom,
                            directionAngle,
                            Cpw,
                            Cpl,
                            userSpecifiedHeights,
                            coordinateTopZ,
                            coordinateBottomZ,
                            windSpeed,
                            (int)terrainCategory,
                            orography,
                            k1,
                            CsCd,
                            rho,
                            userSpecifiedExposure);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
    }
#endif