
namespace MPT.CSI.API.Core
{
    /// <summary>
    /// Steel overwrite items allowed in the application for steel design code:
    /// AISC360_05_IBC2006
    /// </summary>
    public enum eSteelOverwriteItem
    {
        /// <summary>
        /// Framing type.
        /// </summary>
        FramingType = 1,

        /// <summary>
        /// Omega0.
        /// </summary>
        Omega0 = 2,

        /// <summary>
        /// Consider deflection.
        /// </summary>
        ConsiderDeflection = 3,

        /// <summary>
        /// Deflection check type.
        /// </summary>
        DeflectionCheckType = 4,

        /// <summary>
        /// DL deflection limit, L/Value.
        /// </summary>
        DeflectionLimitRelativeDL = 5,

        /// <summary>
        /// SDL + LL deflection limit, L/Value.
        /// </summary>
        DeflectionLimitRelativeSDL_LL = 6,

        /// <summary>
        /// LL deflection limit, L/Value.
        /// </summary>
        DeflectionLimitRelativeLL = 7,

        /// <summary>
        /// Total load deflection limit, L/Value.
        /// </summary>
        DeflectionLimitRelativeTotal = 8,

        /// <summary>
        /// Total camber limit, L/Value.
        /// </summary>
        CamberLimitRelativeTotal = 9,

        /// <summary>
        /// DL deflection limit, absolute.
        /// </summary>
        DeflectionLimitAbsoluteDL = 10,

        /// <summary>
        /// SDL + LL deflection limit, absolute.
        /// </summary>
        DeflectionLimitAbsoluteSDL_LL = 11,

        /// <summary>
        /// LL deflection limit, absolute.
        /// </summary>
        DeflectionLimitAbsoluteLL = 12,

        /// <summary>
        /// Total load deflection limit, absolute.
        /// </summary>
        DeflectionLimitAbsoluteTotal = 13,

        /// <summary>
        /// Total camber limit, absolute.
        /// </summary>
        CamberLimitAbsoluteTotal = 14,

        /// <summary>
        /// Specified camber.
        /// </summary>
        CamberSpecified = 15,

        /// <summary>
        /// Net area to total area ratio.
        /// </summary>
        NetAreaToTotalAreaRatio = 16,

        /// <summary>
        /// Live load reduction factor.
        /// </summary>
        LiveLoadReductionFactor = 17,

        /// <summary>
        /// Unbraced length ratio, Major.
        /// </summary>
        UnbracedLengthRatioMajor = 18,

        /// <summary>
        /// Unbraced length ratio, Minor.
        /// </summary>
        UnbracedLengthRatioMinor = 19,

        /// <summary>
        /// Unbraced length ratio, Lateral Torsional Buckling.
        /// </summary>
        UnbracedLengthRatioLTB = 20,

        /// <summary>
        /// Effective length factor, K1 Major.
        /// </summary>
        EffectiveLengthFactorK1Major = 21,

        /// <summary>
        /// Effective length factor, K1 Minor.
        /// </summary>
        EffectiveLengthFactorK1Minor = 22,

        /// <summary>
        /// Effective length factor, K2 Major.
        /// </summary>
        EffectiveLengthFactorK2Major = 23,

        /// <summary>
        /// Effective length factor, K2 Minor.
        /// </summary>
        EffectiveLengthFactorK2Minor = 24,

        /// <summary>
        /// Effective length factor, K Lateral Torsional Buckling.
        /// </summary>
        EffectiveLengthFactorKLTB = 25,

        /// <summary>
        /// Moment coefficient, Cm Major.
        /// </summary>
        MomentCoefficientCmMajor = 26,

        /// <summary>
        /// Moment coefficient, Cm Minor.
        /// </summary>
        MomentCoefficientCmMinor = 27,

        /// <summary>
        /// Bending coefficient, Cb.
        /// </summary>
        BendingCoefficientCb = 28,

        /// <summary>
        /// Nonsway moment factor, B1 Major.
        /// </summary>
        NonswayMomentFactorB1Major = 29,

        /// <summary>
        /// Nonsway moment factor, B1 Minor.
        /// </summary>
        NonswayMomentFactorB1Minor = 30,

        /// <summary>
        /// Sway moment factor, B2 Major.
        /// </summary>
        SwayMomentFactorB2Major = 31,

        /// <summary>
        /// Sway moment factor, B2 Minor.
        /// </summary>
        SwayMomentFactorB2Minor = 32,

        /// <summary>
        /// Reduce HSS thickness.
        /// </summary>
        ReduceHSSThickness = 33,

        /// <summary>
        /// HSS welding type.
        /// </summary>
        HSSWeldingType = 34,

        /// <summary>
        /// Yield stress, Fy.
        /// </summary>
        YieldStressFy = 35,

        /// <summary>
        /// Expected to specified Fy ratio, Ry.
        /// </summary>
        ExpectedToSpecifiedFyRatioRy = 36,

        /// <summary>
        /// Compressive capacity, Pnc.
        /// </summary>
        CompressiveCapacityPnc = 37,

        /// <summary>
        /// Tensile capacity, Pnt.
        /// </summary>
        TensileCapacityPnt = 38,

        /// <summary>
        /// Major bending capacity, Mn3.
        /// </summary>
        MajorBendingCapacityMn3 = 39,

        /// <summary>
        /// Minor bending capacity, Mn2.
        /// </summary>
        MinorBendingCapacityMn2 = 40,

        /// <summary>
        /// Major shear capacity, Vn2.
        /// </summary>
        MajorShearCapacityVn2 = 41,

        /// <summary>
        /// Minor shear capacity, Vn3.
        /// </summary>
        MinorShearCapacityVn3 = 42,

        /// <summary>
        /// Demand/capacity ratio limit.
        /// </summary>
        DemandCapacityRatioLimit = 43
    }
}
