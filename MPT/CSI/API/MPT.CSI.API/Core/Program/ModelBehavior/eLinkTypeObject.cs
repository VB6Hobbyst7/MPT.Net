// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-13-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eLinkTypeObject.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Link object types available in the application.
    /// </summary>
    public enum eLinkTypeObject
    {
        /// <summary>
        /// Object is a line object that is has a line spring assignment.
        /// The springs are modeled using link elements.
        /// </summary>
        LineSpring = 2,

        /// <summary>
        /// Object is a area object that is has an area spring assignment.
        /// The springs are modeled using link elements.
        /// </summary>
        AreaSpring = 3,

        /// <summary>
        /// Object is a solid object that is has a surface spring assignment.
        /// The springs are modeled using link elements.
        /// </summary>
        SurfaceSpring = 4,

        /// <summary>
        /// Object is a point object that has a panel zone assignment.
        /// The specified link element is internally added by the program at the point object (panel zone) location to model the panel zone.
        /// </summary>
        PanelZone = 9
    }
}
#endif