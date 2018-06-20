// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 06-10-2017
//
// Last Modified By : Mark
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="eLoadPatternType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Load pattern types available in the application.
    /// </summary>
    public enum eLoadPatternType
    {
        /// <summary>
        /// The none
        /// </summary>
        None = 0,
        /// <summary>
        /// The dead
        /// </summary>
        Dead = 1,
        /// <summary>
        /// The super dead
        /// </summary>
        SuperDead,
        /// <summary>
        /// The live
        /// </summary>
        Live,
        /// <summary>
        /// The live reducible
        /// </summary>
        LiveReducible,
        /// <summary>
        /// The quake
        /// </summary>
        Quake,
        /// <summary>
        /// The wind
        /// </summary>
        Wind,
        /// <summary>
        /// The snow
        /// </summary>
        Snow,
        /// <summary>
        /// The other
        /// </summary>
        Other,
        /// <summary>
        /// The move
        /// </summary>
        Move,
        /// <summary>
        /// The temperature
        /// </summary>
        Temperature,
        /// <summary>
        /// The live roof
        /// </summary>
        LiveRoof,
        /// <summary>
        /// The notional
        /// </summary>
        Notional,
        /// <summary>
        /// The live pattern
        /// </summary>
        LivePattern,
        /// <summary>
        /// The wave
        /// </summary>
        Wave,
        /// <summary>
        /// The braking
        /// </summary>
        Braking,
        /// <summary>
        /// The centrifugal
        /// </summary>
        Centrifugal,
        /// <summary>
        /// The friction
        /// </summary>
        Friction,
        /// <summary>
        /// The ice
        /// </summary>
        Ice,
        /// <summary>
        /// The wind on live load
        /// </summary>
        WindOnLiveLoad,
        /// <summary>
        /// The earth pressure horizontal
        /// </summary>
        EarthPressureHorizontal,
        /// <summary>
        /// The earth pressure vertical
        /// </summary>
        EarthPressureVertical,
        /// <summary>
        /// The earth surcharge
        /// </summary>
        EarthSurcharge,
        /// <summary>
        /// Down drag
        /// </summary>
        DownDrag,
        /// <summary>
        /// The vehicle collision
        /// </summary>
        VehicleCollision,
        /// <summary>
        /// The vessel collision
        /// </summary>
        VesselCollision,
        /// <summary>
        /// The temperature gradient
        /// </summary>
        TemperatureGradient,
        /// <summary>
        /// The settlement
        /// </summary>
        Settlement,
        /// <summary>
        /// The shrinkage
        /// </summary>
        Shrinkage,
        /// <summary>
        /// The creep
        /// </summary>
        Creep,
        /// <summary>
        /// The water load pressure
        /// </summary>
        WaterLoadPressure,
        /// <summary>
        /// The live load surcharge
        /// </summary>
        LiveLoadSurcharge,
        /// <summary>
        /// The locked in forces
        /// </summary>
        LockedInForces,
        /// <summary>
        /// The live load pedestrian
        /// </summary>
        LiveLoadPedestrian,
        /// <summary>
        /// The pre stress
        /// </summary>
        PreStress,
        /// <summary>
        /// The hyper static
        /// </summary>
        HyperStatic,
        /// <summary>
        /// The bouyancy
        /// </summary>
        Bouyancy,
        /// <summary>
        /// The stream flow
        /// </summary>
        StreamFlow,
        /// <summary>
        /// The impact
        /// </summary>
        Impact,
        /// <summary>
        /// The construction
        /// </summary>
        Construction
    }
}
