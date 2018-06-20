// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 09-28-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="WaveLoadGeneric.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wave
{
    /// <summary>
    /// Represents a generic auto wave load in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wave.AutoWaveLoad" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class WaveLoadGeneric : AutoWaveLoad
    {

        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="WaveLoadGeneric" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public WaveLoadGeneric(CSiApiSeed seed) : base(seed)
        {
        }


        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves auto seastate loading parameters.
        /// </summary>
        /// <param name="name">The name of an existing seastate-type load pattern.</param>
        /// <param name="loadMethod">This is one of the following three options defining what parameters are being specified.</param>
        /// <param name="coordinateSystem">The coordinate system used as a reference for specifying the center of rotation location and the inertia load parameters.</param>
        /// <param name="adjustGravityLateral">This item only applies when using LoadMethod 1 or 2.
        /// It is True if generated lateral loads should include the effects of the rotated structure, otherwise it is False.</param>
        /// <param name="adjustGravityLateralFactor">This item only applies when using LoadMethod 1 or 2.
        /// This is a scale factor on the lateral effects generated as a result of the rotated structure.</param>
        /// <param name="adjustGravityVertical">This item only applies when using LoadMethod 1 or 2.
        /// It is True if generated vetical loads should include the effects of the rotated structure, otherwise it is False.</param>
        /// <param name="adjustGravityVerticalFactor">This item only applies when using LoadMethod 1 or 2.
        /// This is a scale factor on the vertical effects generated as a result of the rotated structure.</param>
        /// <param name="centerRotation">The coordinates of the center of rotation, with respect to the selected coordinate system.</param>
        /// <param name="parameters">This is an array of the inertia load parameters, based on the specified LoadMethod, as described below. For all cases, the array contains the amplitude, period, &amp; phase for each DOF:
        /// Load Method = 1: UZ, RX, RY, Load Method = 2: UX, UY, UZ, RX, RY, RZ, Load Method = 3: UX, UY, UZ, RX, RY, RZ</param>
        /// <param name="ignorePhase">This item only applies when using LoadMethod 1 or 2.
        /// It is True if the input phases should be ignored, otherwise it is False.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoad(string name,
            ref eLoadMethod loadMethod,
            ref string coordinateSystem,
            ref bool adjustGravityLateral,
            ref double adjustGravityLateralFactor,
            ref bool adjustGravityVertical,
            ref double adjustGravityVerticalFactor,
            ref Coordinate3DCartesian centerRotation,
            ref double[] parameters,
            ref bool ignorePhase)
        {
            double[] csiCenterRotation = new double[3];
            int csiLoadMethod = 0;

            _callCode = _sapModel.LoadPatterns.AutoSeastate.GetAuto(name, ref csiLoadMethod, ref coordinateSystem, 
                            ref adjustGravityLateral, ref adjustGravityLateralFactor, 
                            ref adjustGravityVertical, ref adjustGravityVerticalFactor, 
                            ref csiCenterRotation, 
                            ref parameters, 
                            ref ignorePhase);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            centerRotation.X = csiCenterRotation[0];
            centerRotation.Y = csiCenterRotation[1];
            centerRotation.Z = csiCenterRotation[2];

            loadMethod = (eLoadMethod) csiLoadMethod;
        }

        /// <summary>
        /// This function retrieves auto seastate loading parameters.
        /// </summary>
        /// <param name="name">The name of an existing seastate-type load pattern.</param>
        /// <param name="loadMethod">This is one of the following three options defining what parameters are being specified.</param>
        /// <param name="coordinateSystem">The coordinate system used as a reference for specifying the center of rotation location and the inertia load parameters.</param>
        /// <param name="adjustGravityLateral">This item only applies when using LoadMethod 1 or 2.
        /// It is True if generated lateral loads should include the effects of the rotated structure, otherwise it is False.</param>
        /// <param name="adjustGravityLateralFactor">This item only applies when using LoadMethod 1 or 2.
        /// This is a scale factor on the lateral effects generated as a result of the rotated structure.</param>
        /// <param name="adjustGravityVertical">This item only applies when using LoadMethod 1 or 2.
        /// It is True if generated vetical loads should include the effects of the rotated structure, otherwise it is False.</param>
        /// <param name="adjustGravityVerticalFactor">This item only applies when using LoadMethod 1 or 2.
        /// This is a scale factor on the vertical effects generated as a result of the rotated structure.</param>
        /// <param name="centerRotation">The coordinates of the center of rotation, with respect to the selected coordinate system.</param>
        /// <param name="parameters">This is an array of the inertia load parameters, based on the specified LoadMethod, as described below.
        /// For all cases, the array contains the amplitude, period, &amp; phase for each DOF:
        /// Load Method = 1: UZ, RX, RY, Load Method = 2: UX, UY, UZ, RX, RY, RZ, Load Method = 3: UX, UY, UZ, RX, RY, RZ</param>
        /// <param name="ignorePhase">This item only applies when using LoadMethod 1 or 2.
        /// It is True if the input phases should be ignored, otherwise it is False.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoad(string name,
            eLoadMethod loadMethod,
            string coordinateSystem,
            bool adjustGravityLateral,
            double adjustGravityLateralFactor,
            bool adjustGravityVertical,
            double adjustGravityVerticalFactor,
            Coordinate3DCartesian centerRotation,
            double[] parameters,
            bool ignorePhase)
        {
            double[] csiCenterRotation = new double[3];
            csiCenterRotation[0] = centerRotation.X;
            csiCenterRotation[1] = centerRotation.Y;
            csiCenterRotation[2] = centerRotation.Z;

            _callCode = _sapModel.LoadPatterns.AutoSeastate.SetAuto(name, (int)loadMethod, coordinateSystem, 
                            adjustGravityLateral, adjustGravityLateralFactor, 
                            adjustGravityVertical, adjustGravityVerticalFactor,
                            csiCenterRotation, 
                            parameters, 
                            ignorePhase);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        #endregion
    }
}
#endif
