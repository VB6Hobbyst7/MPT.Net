// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 07-22-2017
//
// Last Modified By : Mark
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="ePreferences_AA_ASD_2000.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_SAP2000v18 || BUILD_SAP2000v19 || BUILD_SAP2000v20
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Aluminum
{
    /// <summary>
    /// Preferences available for <see cref="AA_ASD_2000" /> aluminum design in the application.
    /// </summary>
    public enum ePreferences_AA_ASD_2000
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
        /// The lateral factor.
        /// </summary>
        LateralFactor = 3,

        /// <summary>
        /// Use lateral factor?
        /// </summary>
        UseLateralFactor = 4,

        /// <summary>
        /// The bridge type structure.
        /// </summary>
        BridgeTypeStructure = 5,

        /// <summary>
        /// The time history design.
        /// </summary>
        TimeHistoryDesign = 6
    }
}
#endif