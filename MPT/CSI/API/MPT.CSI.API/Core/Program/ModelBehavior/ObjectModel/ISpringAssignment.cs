// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-02-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="ISpringAssignment.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object can get/set an existing spring assignment.
    /// </summary>
    public interface ISpringAssignment
    {
        /// <summary>
        /// Retrieves the named spring property assignment for an object.</summary>
        /// <param name="name">The name of an existing object .</param>
        /// <param name="nameSpring">The name of an existing point spring property.</param>
        void GetSpringAssignment(string name,
            ref string nameSpring);

        /// <summary>
        /// Assigns an existing named spring property to objects.</summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType"/> item.</param>
        /// <param name="nameSpring">The name of an existing point spring property.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are made for the object specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are made for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are made for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        void SetSpringAssignment(string name,
            string nameSpring,
            eItemType itemType = eItemType.Object);
    }
}
#endif