// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-09-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-10-2017
// ***********************************************************************
// <copyright file="AS_NZS_1170_2_2002.cs" company="">
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
    public class AS_NZS_1170_2_2002 : AutoWindLoad
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="AS_NZS_1170_2_2002" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public AS_NZS_1170_2_2002(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves auto wind loading parameters for AS/NZS 1170.2:2002.
        /// </summary>
        /// <param name="name">The name of an existing Wind-type load case with an auto wind assignment.</param>
        /// <param name="exposureFrom">The source of the wind exposure.</param>
        /// <param name="directionAngle">The direction angle for the wind load.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="Cpw">The windward coefficient, Cp.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="Cpl">The leeward coefficient, Cp.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="Ka">The area reduction factor, Ka.</param>
        /// <param name="Kc">The combination factor, Kc.</param>
        /// <param name="Kl">The local pressure factor, Kl.</param>
        /// <param name="Kp">The porous cladding factor, Kp.</param>
        /// <param name="userSpecifiedHeights">True: Top and bottom elevations of the wind load are user specified.
        /// Else, the elevations are determined by the program.</param>
        /// <param name="coordinateTopZ">Global Z-coordinate at the highest level where auto wind loads are applied. [L].</param>
        /// <param name="coordinateBottomZ">Global Z-coordinate at the lowest level where auto wind loads are applied. [L].</param>
        /// <param name="windSpeed">Design reference wind speed, Vr, in m/s.</param>
        /// <param name="terrainCategory">The terrain category.</param>
        /// <param name="cycloneRegion">True: The structure is in a cyclone region.</param>
        /// <param name="Md">The directional multiplier, Md.</param>
        /// <param name="Ms">The shielding multiplier, Ms.</param>
        /// <param name="Mt">The topographic multiplier, Mt.</param>
        /// <param name="Cdyn">The dynamic response factor, Cdyn.</param>
        /// <param name="userSpecifiedExposure">True: Wind exposure widths are provided by the user.
        /// Else, wind exposure widths are calculated by the program from the extents of the diaphragms.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoad(string name,
            ref eWindExposure exposureFrom,
            ref double directionAngle,
            ref double Cpw,
            ref double Cpl,
            ref double Ka,
            ref double Kc,
            ref double Kl,
            ref double Kp,
            ref bool userSpecifiedHeights,
            ref double coordinateTopZ,
            ref double coordinateBottomZ,
            ref double windSpeed,
            ref int terrainCategory,
            ref bool cycloneRegion,
            ref double Md,
            ref double Ms,
            ref double Mt,
            ref double Cdyn,
            ref bool userSpecifiedExposure)
        {
            int csiExposureFrom = 0;

            _callCode = _sapModel.LoadPatterns.AutoWind.GetASNZS117022002(name,
                            ref csiExposureFrom,
                            ref directionAngle,
                            ref Cpw,
                            ref Cpl,
                            ref Ka,
                            ref Kc,
                            ref Kl,
                            ref Kp,
                            ref userSpecifiedHeights,
                            ref coordinateTopZ,
                            ref coordinateBottomZ,
                            ref windSpeed,
                            ref terrainCategory,
                            ref cycloneRegion,
                            ref Md,
                            ref Ms,
                            ref Mt,
                            ref Cdyn,
                            ref userSpecifiedExposure);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            exposureFrom = (eWindExposure)csiExposureFrom;
        }


        /// <summary>
        /// This function assigns auto wind loading parameters for AS/NZS 1170.2:2002.
        /// </summary>
        /// <param name="name">The name of an existing Wind-type load case with an auto wind assignment.</param>
        /// <param name="exposureFrom">The source of the wind exposure.</param>
        /// <param name="directionAngle">The direction angle for the wind load.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="Cpw">The windward coefficient, Cp.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="Cpl">The leeward coefficient, Cp.
        /// This item applies only when <paramref name="exposureFrom" /> = <see cref="eWindExposure.ExtentsOfRigidDiaphragms" />.</param>
        /// <param name="Ka">The area reduction factor, Ka.</param>
        /// <param name="Kc">The combination factor, Kc.</param>
        /// <param name="Kl">The local pressure factor, Kl.</param>
        /// <param name="Kp">The porous cladding factor, Kp.</param>
        /// <param name="userSpecifiedHeights">True: Top and bottom elevations of the wind load are user specified.
        /// Else, the elevations are determined by the program.</param>
        /// <param name="coordinateTopZ">Global Z-coordinate at the highest level where auto wind loads are applied. [L].</param>
        /// <param name="coordinateBottomZ">Global Z-coordinate at the lowest level where auto wind loads are applied. [L].</param>
        /// <param name="windSpeed">Design reference wind speed, Vr, in m/s.</param>
        /// <param name="terrainCategory">The terrain category.</param>
        /// <param name="cycloneRegion">True: The structure is in a cyclone region.</param>
        /// <param name="Md">The directional multiplier, Md.</param>
        /// <param name="Ms">The shielding multiplier, Ms.</param>
        /// <param name="Mt">The topographic multiplier, Mt.</param>
        /// <param name="Cdyn">The dynamic response factor, Cdyn.</param>
        /// <param name="userSpecifiedExposure">True: Wind exposure widths are provided by the user.
        /// Else, wind exposure widths are calculated by the program from the extents of the diaphragms.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoad(string name,
            eWindExposure exposureFrom,
            double directionAngle,
            double Cpw,
            double Cpl,
            double Ka,
            double Kc,
            double Kl,
            double Kp,
            bool userSpecifiedHeights,
            double coordinateTopZ,
            double coordinateBottomZ,
            double windSpeed,
            int terrainCategory,
            bool cycloneRegion,
            double Md,
            double Ms,
            double Mt,
            double Cdyn,
            bool userSpecifiedExposure = false)
        {
            _callCode = _sapModel.LoadPatterns.AutoWind.SetASNZS117022002(name,
                            (int)exposureFrom,
                            directionAngle,
                            Cpw,
                            Cpl,
                            Ka,
                            Kc,
                            Kl,
                            Kp,
                            userSpecifiedHeights,
                            coordinateTopZ,
                            coordinateBottomZ,
                            windSpeed,
                            terrainCategory,
                            cycloneRegion,
                            Md,
                            Ms,
                            Mt,
                            Cdyn,
                            userSpecifiedExposure);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
#endif