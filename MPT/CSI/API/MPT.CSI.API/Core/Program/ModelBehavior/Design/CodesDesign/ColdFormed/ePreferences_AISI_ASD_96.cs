// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 07-22-2017
//
// Last Modified By : Mark
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="ePreferences_AISI_ASD_96.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_SAP2000v18 || BUILD_SAP2000v19 || BUILD_SAP2000v20
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.ColdFormed
{
    /// <summary>
    /// Preferences available for <see cref="AISI_ASD_96" /> cold-formed steel design in the application.
    /// </summary>
    public enum ePreferences_AISI_ASD_96
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
        /// The bending stiffened Omega.
        /// </summary>
        BendingStiffened_Omega = 3,

        /// <summary>
        /// The bending unstiffened Omega.
        /// </summary>
        BendingUnstiffened_Omega = 4,

        /// <summary>
        /// The bending lateral torsional buckling Omega.
        /// </summary>
        BendingLateralTorsionalBuckling_Omega = 5,

        /// <summary>
        /// The shear slender Omega.
        /// </summary>
        ShearSlender_Omega = 6,

        /// <summary>
        /// The shear nonslender Omega.
        /// </summary>
        ShearNonslender_Omega = 7,

        /// <summary>
        /// The axial tension Omega.
        /// </summary>
        AxialTension_Omega = 8,

        /// <summary>
        /// The axial compression Omega.
        /// </summary>
        AxialCompression_Omega = 9,

        /// <summary>
        /// The time history design.
        /// </summary>
        TimeHistoryDesign = 10
    }
}
#endif