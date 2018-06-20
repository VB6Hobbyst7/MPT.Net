// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="IGroupLoadable.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object can be loaded by group.
    /// </summary>
    public interface IGroupLoadable
    {
        /// <summary>
        /// This function retrieves the loaded group for tendon objects.
        /// A tendon object transfers its load to any object that is in the specified group.
        /// </summary>
        /// <param name="name">The name of an existing tendon object.</param>
        /// <param name="groupName">This is the name of an existing group.
        /// All objects in the specified group can be loaded by the tendon.</param>
        void GetLoadedGroup(string name,
            ref string groupName);

        /// <summary>
        /// This function makes the loaded group assignment to tendon objects.
        /// A tendon object transfers its load to any object that is in the specified group.
        /// </summary>
        /// <param name="name">The name of an existing tendon object.</param>
        /// <param name="groupName">This is the name of an existing group.
        /// All objects in the specified group can be loaded by the tendon.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetLoadedGroup(string name,
            string groupName,
            eItemType itemType = eItemType.Object);
    }
}
#endif