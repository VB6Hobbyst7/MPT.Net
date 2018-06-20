// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 07-22-2017
//
// Last Modified By : Mark
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="ePreferences_AA_LRFD_2000.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_SAP2000v18 || BUILD_SAP2000v19 || BUILD_SAP2000v20
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Aluminum
{
    /// <summary>
    /// Preferences available for <see cref="AA_LRFD_2000" /> aluminum design in the application.
    /// </summary>
    public enum ePreferences_AA_LRFD_2000
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
        /// The resistance factor, phi y.
        /// </summary>
        ResistanceFactor_Phi_y = 3,

        /// <summary>
        /// The resistance factor, phi b.
        /// </summary>
        ResistanceFactor_Phi_b = 4,

        /// <summary>
        /// The resistance factor, phi c.
        /// </summary>
        ResistanceFactor_Phi_c = 5,

        /// <summary>
        /// The resistance factor, phi u.
        /// </summary>
        ResistanceFactor_Phi_u = 6,

        /// <summary>
        /// The resistance factor, phi cc.
        /// </summary>
        ResistanceFactor_Phi_cc = 7,

        /// <summary>
        /// The resistance factor, phi cp.
        /// </summary>
        ResistanceFactor_Phi_cp = 8,

        /// <summary>
        /// The resistance factor, phi v.
        /// </summary>
        ResistanceFactor_Phi_v = 9,

        /// <summary>
        /// The resistance factor, phi vp.
        /// </summary>
        ResistanceFactor_Phi_vp = 10,

        /// <summary>
        /// The resistance factor, phi w.
        /// </summary>
        ResistanceFactor_Phi_w = 11,

        /// <summary>
        /// The time history design.
        /// </summary>
        TimeHistoryDesign = 12
    }
}
#endif