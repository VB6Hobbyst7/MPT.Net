Option Explicit On
Option Strict On

Public Enum eLoadPatternType
    None = 0
    Dead = 1
    SuperDead
    Live
    LiveReducible
    Quake
    Wind
    Snow
    Other
    Move
    Temperature
    LiveRoof
    Notional
    LivePattern
    Wave
    Braking
    Centrifugal
    Friction
    Ice
    WindOnLiveLoad
    EarthPressureHorizontal
    EarthPressureVertical
    EarthSurcharge
    DownDrag
    VehicleCollision
    VesselCollision
    TemperatureGradient
    Settlement
    Shrinkage
    Creep
    WaterLoadPressure
    LiveLoadSurcharge
    LockedInForces
    LiveLoadPedestrian
    PreStress
    HyperStatic
    Bouyancy
    StreamFlow
    Impact
    Construction
End Enum
