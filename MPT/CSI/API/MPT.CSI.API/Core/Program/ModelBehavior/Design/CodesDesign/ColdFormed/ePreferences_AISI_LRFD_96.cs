// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 07-22-2017
//
// Last Modified By : Mark
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="ePreferences_AISI_LRFD_96.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_SAP2000v18 || BUILD_SAP2000v19 || BUILD_SAP2000v20
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.ColdFormed
{
    /// <summary>
    /// Preferences available for <see cref="AISI_LRFD_96" /> cold-formed steel design in the application.
    /// </summary>
    public enum ePreferences_AISI_LRFD_96
    {
        /// <summary>
        /// The framing type.
        /// </summary>
        FramingType = 1,

        /// <summary>
        /// The demand/capacity ratio limit.
        /// </summary>
        DemandCapacityRatioLimit = 2,

        /// <summary>
        /// The bending stiffened Phi.
        /// </summary>
        BendingStiffened_Phi = 3,

        /// <summary>
        /// The bending unstiffened Phi.
        /// </summary>
        BendingUnstiffened_Phi = 4,

        /// <summary>
        /// The bending lateral torsional buckling Phi.
        /// </summary>
        BendingLateralTorsionalBuckling_Phi = 5,

        /// <summary>
        /// The shear slender Phi.
        /// </summary>
        ShearSlender_Phi = 6,

        /// <summary>
        /// The shear nonslender Phi.
        /// </summary>
        ShearNonslender_Phi = 7,

        /// <summary>
        /// The axial tension Phi.
        /// </summary>
        AxialTension_Phi = 8,

        /// <summary>
        /// The axial compression Phi.
        /// </summary>
        AxialCompression_Phi = 9,

        /// <summary>
        /// The time history design.
        /// </summary>
        TimeHistoryDesign = 10
    }
}
#endif