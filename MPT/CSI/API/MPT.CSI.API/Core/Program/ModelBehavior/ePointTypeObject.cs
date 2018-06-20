// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-13-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-13-2017
// ***********************************************************************
// <copyright file="ePointTypeObject.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Point object types available in the application.
    /// </summary>
    public enum ePointTypeObject
    {
        /// <summary>
        /// Object is the point object corresponding to the specified point element.
        /// </summary>
        Point = 1,

        /// <summary>
        /// Object is a line object that is internally meshed by the program to create the specified point element.
        /// </summary>
        LineInternallyMeshed = 2,

        /// <summary>
        /// Object is an area object that is internally meshed by the program to create the specified point element.
        /// </summary>
        AreaInternallyMeshed = 3,

        /// <summary>
        /// Object is a solid object that is internally meshed by the program to create the specified point element.
        /// </summary>
        SolidInternallyMeshed = 6,

        /// <summary>
        /// Object is a point object that has a panel zone assignment.
        /// The specified point element is internally added by the program at the point object (panel zone) location to model the panel zone.
        /// The specified point element does not directly correspond to the point object returned; it is an added point at the same location as the point object.
        /// </summary>
        PanelZone = 9,

        /// <summary>
        /// Object is a defined diaphragm constraint.
        /// The specified point element was internally added by the program for application of auto wind and auto seismic loads.
        /// </summary>
        DiaphragmConstraint = 21
    }
}
