// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-13-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-11-2017
// ***********************************************************************
// <copyright file="eFrameSectionType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Types of frame sections available in the application.
    /// </summary>
    public enum eFrameSectionType
    {
        /// <summary>
        /// All frame types.
        /// </summary>
        All = 0,

        /// <summary>
        /// I-section.
        /// </summary>
        ISection = 1,

        /// <summary>
        /// Channel section.
        /// </summary>
        Channel = 2,

        /// <summary>
        /// T-section.
        /// </summary>
        TSection = 3,

        /// <summary>
        /// Angle section.
        /// </summary>
        Angle = 4,

        /// <summary>
        /// Double angle section.
        /// </summary>
        DoubleAngle = 5,

        /// <summary>
        /// Box section.
        /// </summary>
        Box = 6,

        /// <summary>
        /// Pipe section.
        /// </summary>
        Pipe = 7,

        /// <summary>
        /// Rectangular section.
        /// </summary>
        Rectangular = 8,

        /// <summary>
        /// Circular section.
        /// </summary>
        Circle = 9,

        /// <summary>
        /// General section.
        /// </summary>
        General = 10,

        /// <summary>
        /// Double channel section.
        /// </summary>
        DoubleChannel = 11,

        /// <summary>
        /// Auto section.
        /// </summary>
        Auto = 12,

        /// <summary>
        /// Section Designer (SD) section.
        /// </summary>
        SectionDesigner = 13,

        /// <summary>
        /// Variable section.
        /// </summary>
        Variable = 14,

        /// <summary>
        /// Joist section.
        /// </summary>
        Joist = 15,

        /// <summary>
        /// Bridge section.
        /// </summary>
        Bridge = 16,

        /// <summary>
        /// Cold-formed steel C-section.
        /// </summary>
        ColdC = 17,

        /// <summary>
        /// Cold-formed steel double C-section.
        /// </summary>
        ColdDoubleC = 18,

        /// <summary>
        /// Cold-formed steel  Z-section.
        /// </summary>
        ColdZ = 19,

        /// <summary>
        /// Cold-formed steel L-section.
        /// </summary>
        ColdL = 20,

        /// <summary>
        /// Cold-formed steel double L-section.
        /// </summary>
        ColdDoubleL = 21,

        /// <summary>
        /// Cold-formed steel hat section.
        /// </summary>
        ColdHat = 22,

        /// <summary>
        /// Built-up cover plate I-section.
        /// </summary>
        BuiltUpICoverPlate = 23,

        /// <summary>
        /// Pre-Cast concrete girder I-section.
        /// </summary>
        PreCastConcreteGirderI = 24,

        /// <summary>
        /// Pre-Cast concrete girder U-section.
        /// </summary>
        PreCastConcreteGirderU = 25,

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Built-up hybrid I-section.
        /// </summary>
        BuiltUpIHybrid = 26,

        /// <summary>
        /// Built-up hybrid U-section.
        /// </summary>
        BuiltUpUHybrid = 27,

        /// <summary>
        /// Concrete L-section.
        /// </summary>
        ConcreteL = 28,

        /// <summary>
        /// Filled tube section.
        /// </summary>
        FilledTube = 29,

        /// <summary>
        /// Filled pipe section.
        /// </summary>
        FilledPipe = 30,

        /// <summary>
        /// Encased rectangle section.
        /// </summary>
        EncasedRectangle = 31,

        /// <summary>
        /// Encased circle section.
        /// </summary>
        EncasedCircle = 32,

        /// <summary>
        /// Buckling restrained brace section.
        /// </summary>
        BucklingRestrainedBrace = 33,

        /// <summary>
        /// Buckling restrained core brace section.
        /// </summary>
        CoreBraceBRB = 34,

        /// <summary>
        /// Concrete tee section.
        /// </summary>
        ConcreteTee = 35,

        /// <summary>
        /// Concrete box section.
        /// </summary>
        ConcreteBox = 36,

        /// <summary>
        /// Concrete pipe section.
        /// </summary>
        ConcretePipe = 37,

        /// <summary>
        /// Concrete cross section.
        /// </summary>
        ConcreteCross = 38,

        /// <summary>
        /// Steel plate section.
        /// </summary>
        SteelPlate = 39,

        /// <summary>
        /// Steel rod section.
        /// </summary>
        SteelRod = 40,
#endif
    }
}
