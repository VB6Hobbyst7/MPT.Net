// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-07-2017
// ***********************************************************************
// <copyright file="ISpecialPoint.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has CRUDable special point.
    /// </summary>
    public interface ISpecialPoint
    {
        /// <summary>
        /// This function retrieves the special point status for a point object.
        /// Special points are allowed to exist in the model even if no objects (line, area, solid, link) are connected to them.
        /// Points that are not special are automatically deleted if no objects connect to them.
        /// </summary>
        /// <param name="name">The name of an existing point object.</param>
        /// <param name="isSpecialPoint">True: The point object is specified as a special point.</param>
        void GetSpecialPoint(string name,
            ref bool isSpecialPoint);

        /// <summary>
        /// This function sets the special point status for a point object.
        /// Special points are allowed to exist in the model even if no objects (line, area, solid, link) are connected to them.
        /// Points that are not special are automatically deleted if no objects connect to them.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="isSpecialPoint">True: The point object is specified as a special point.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetSpecialPoint(string name,
            bool isSpecialPoint,
            eItemType itemType = eItemType.Object);

        /// <summary>
        /// The function deletes special point objects that have no other objects connected to them.
        /// Point objects can be deleted only if they have no other objects(e.g., frame, cable, tendon, area, solid link) connected to them.
        /// If a point object is not specified to be a Special Point, the program automatically deletes that point object when it has no other objects connected to it.
        /// If a point object is specified to be a Special Point, to delete it, first delete all other objects connected to the point and then call this function to delete the point.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void DeleteSpecialPoint(string name,
            eItemType itemType = eItemType.Object);
    }
}