// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-21-2017
// ***********************************************************************
// <copyright file="IAddableLinkObject.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;

namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object can have a link object addded.
    /// </summary>
    public interface IAddableLinkObject
    {
        /// <summary>
        /// This function adds a new link object whose corner points are at the specified coordinates.
        /// </summary>
        /// <param name="coordinates">Coordinates for the corner points of the object.
        /// The coordinates are in the coordinate system defined by the <paramref name="coordinateSystem" /> item.
        /// Two coordinates are required.</param>
        /// <param name="name">This is the name that the program ultimately assigns for the object.
        /// If no <paramref name="userName" /> is specified, the program assigns a default name to the object.
        /// If a <paramref name="userName" /> is specified and that name is not used for another object, the <paramref name="userName" /> is assigned to the object; otherwise a default name is assigned to the object.</param>
        /// <param name="isSingleJoint">True: A one-joint link is added.
        /// False: A two-joint link is added.</param>
        /// <param name="nameProperty">This is either Default or the name of a defined solid property.
        /// If it is Default, the program assigns a default solid property to the solid object.
        /// If it is the name of a defined property, that property is assigned to the object.</param>
        /// <param name="userName">This is an optional user specified name for the object.
        /// If a <paramref name="userName" /> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="userName" />.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the object point coordinates are defined.</param>
        void AddByCoordinate(ref Coordinate3DCartesian[] coordinates,
            ref string name,
            bool isSingleJoint,
            string nameProperty = "Default",
            string userName = "",
            string coordinateSystem = CoordinateSystems.Global);

        /// <summary>
        /// This function adds a new link object whose corner points are specified by name.
        /// </summary>
        /// <param name="pointNames">Names of the point objects that define the corner points of the added object.
        /// Two points are required.</param>
        /// <param name="name">This is the name that the program ultimately assigns for the object.
        /// If no <paramref name="userName" /> is specified, the program assigns a default name to the object.
        /// If a <paramref name="userName" /> is specified and that name is not used for another object, the <paramref name="userName" /> is assigned to the object; otherwise a default name is assigned to the object.</param>
        /// <param name="isSingleJoint">True: A one-joint link is added.
        /// False: A two-joint link is added.</param>
        /// <param name="nameProperty">This is either Default or the name of a defined solid property.
        /// If it is Default, the program assigns a default solid property to the solid object.
        /// If it is the name of a defined property, that property is assigned to the object.</param>
        /// <param name="userName">This is an optional user specified name for the object.
        /// If a <paramref name="userName" /> is specified and that name is already used for another object of the same type, the program ignores the <paramref name="userName" />.</param>
        void AddByPoint(string[] pointNames,
            ref string name,
            bool isSingleJoint,
            string nameProperty = "Default",
            string userName = "");
    }
}