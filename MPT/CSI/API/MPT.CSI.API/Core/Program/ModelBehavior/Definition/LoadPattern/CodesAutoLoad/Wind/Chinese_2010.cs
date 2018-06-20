// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-10-2017
// ***********************************************************************
// <copyright file="Chinese_2010.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wind
{
    /// <summary>
    /// Represents the UBC_94 auto wind load in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wind.AutoWindLoad" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class Chinese_2010 : AutoWindLoad
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="Chinese_2010" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public Chinese_2010(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves auto wind loading parameters for Chinese 2010.
        /// </summary>
        /// <param name="name">The name of an existing Wind-type load case with an auto wind assignment.</param>
        /// <param name="exposureFrom">The source of the wind exposure.</param>
        /// <param name="directionAngle">The direction angle for the wind load.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="buildingWidth">The building width. [L].</param>
        /// <param name="Us">The shape coefficient.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="uniformTaper">True: A correction is to be applied to the wind load for a uniform taper.</param>
        /// <param name="BhOverB0">The taper ratio, Bh/B0.
        /// This item applies only when <paramref name="uniformTaper" /> = True.</param>
        /// <param name="userSpecifiedHeights">True: Top and bottom elevations of the wind load are user specified.
        /// Else, the elevations are determined by the program.</param>
        /// <param name="coordinateTopZ">Global Z-coordinate at the highest level where auto wind loads are applied. [L].</param>
        /// <param name="coordinateBottomZ">Global Z-coordinate at the lowest level where auto wind loads are applied. [L].</param>
        /// <param name="wzero">The basic wind pressure in kN/m^2.</param>
        /// <param name="Rt">The ground roughness, Rt.</param>
        /// <param name="phiZSource">The Phi Z source.</param>
        /// <param name="T1Source">The T1 source.</param>
        /// <param name="userT">It is the user defined T1 period. [s].
        /// This item applies only when <paramref name="T1Source" /> = <see cref="eT1Source.UserDefined" /></param>
        /// <param name="dampingRatio">The damping ratio.</param>
        /// <param name="userSpecifiedExposure">True: Wind exposure widths are provided by the user.
        /// Else, wind exposure widths are calculated by the program from the extents of the diaphragms.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoad(string name,
            ref eWindExposure exposureFrom,
            ref double directionAngle,
            ref double buildingWidth,
            ref double Us,
            ref bool uniformTaper,
            ref double BhOverB0,
            ref bool userSpecifiedHeights,
            ref double coordinateTopZ,
            ref double coordinateBottomZ,
            ref double wzero,
            ref eRoughnessBToD Rt,
            ref ePhiZSource phiZSource,
            ref eT1Source T1Source,
            ref double userT,
            ref double dampingRatio,
            ref bool userSpecifiedExposure)
        {
            int csiExposureFrom = 0;
            int csiRt = 0;
            int csiPhiZSource = 0;
            int csiT1Source = 0;

            _callCode = _sapModel.LoadPatterns.AutoWind.GetChinese2010(name,
                            ref csiExposureFrom,
                            ref directionAngle,
                            ref buildingWidth,
                            ref Us,
                            ref uniformTaper,
                            ref BhOverB0,
                            ref userSpecifiedHeights,
                            ref coordinateTopZ,
                            ref coordinateBottomZ,
                            ref wzero,
                            ref csiRt,
                            ref csiPhiZSource,
                            ref csiT1Source,
                            ref userT,
                            ref dampingRatio,
                            ref userSpecifiedExposure);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            exposureFrom = (eWindExposure)csiExposureFrom;
            Rt = (eRoughnessBToD)csiRt;
            phiZSource = (ePhiZSource)csiPhiZSource;
            T1Source = (eT1Source)csiT1Source;
        }


        /// <summary>
        /// This function assigns auto wind loading parameters for Chinese 2010.
        /// </summary>
        /// <param name="name">The name of an existing Wind-type load case with an auto wind assignment.</param>
        /// <param name="exposureFrom">The source of the wind exposure.</param>
        /// <param name="directionAngle">The direction angle for the wind load.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="buildingWidth">The building width. [L].</param>
        /// <param name="Us">The shape coefficient.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="uniformTaper">True: A correction is to be applied to the wind load for a uniform taper.</param>
        /// <param name="BhOverB0">The taper ratio, Bh/B0.
        /// This item applies only when <paramref name="uniformTaper" /> = True.</param>
        /// <param name="userSpecifiedHeights">True: Top and bottom elevations of the wind load are user specified.
        /// Else, the elevations are determined by the program.</param>
        /// <param name="coordinateTopZ">Global Z-coordinate at the highest level where auto wind loads are applied. [L].</param>
        /// <param name="coordinateBottomZ">Global Z-coordinate at the lowest level where auto wind loads are applied. [L].</param>
        /// <param name="wzero">The basic wind pressure in kN/m^2.</param>
        /// <param name="Rt">The ground roughness, Rt.</param>
        /// <param name="phiZSource">The Phi Z source.</param>
        /// <param name="T1Source">The T1 source.</param>
        /// <param name="userT">It is the user defined T1 period. [s].
        /// This item applies only when <paramref name="T1Source" /> = <see cref="eT1Source.UserDefined" /></param>
        /// <param name="dampingRatio">The damping ratio.</param>
        /// <param name="userSpecifiedExposure">True: Wind exposure widths are provided by the user.
        /// Else, wind exposure widths are calculated by the program from the extents of the diaphragms.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoad(string name,
            eWindExposure exposureFrom,
            double directionAngle,
            double buildingWidth,
            double Us,
            bool uniformTaper,
            double BhOverB0,
            bool userSpecifiedHeights,
            double coordinateTopZ,
            double coordinateBottomZ,
            double wzero,
            eRoughnessBToD Rt,
            ePhiZSource phiZSource,
            eT1Source T1Source,
            double userT,
            double dampingRatio,
            bool userSpecifiedExposure = false)
        {
            _callCode = _sapModel.LoadPatterns.AutoWind.SetChinese2010(name,
                            (int)exposureFrom,
                            directionAngle,
                            buildingWidth,
                            Us,
                            uniformTaper,
                            BhOverB0,
                            userSpecifiedHeights,
                            coordinateTopZ,
                            coordinateBottomZ,
                            wzero,
                            (int)Rt,
                            (int)phiZSource,
                            (int)T1Source,
                            userT,
                            dampingRatio,
                            userSpecifiedExposure);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
#endif