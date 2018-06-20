// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-02-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="IPier.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has a gettable/settable wall pier.
    /// </summary>
    public interface IPier
    {
        /// <summary>
        /// Retrieves the pier label assignments of an object.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="namePier">The name of the pier assignment, if any, or "None".</param>
        void GetPier(string name,
            ref string namePier);

        /// <summary>
        /// Sets the pier label assignment of one or more objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType"/> item.</param>
        /// <param name="namePier">The name of the pier assignment, if any, or "None".</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are made for the object specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are made for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are made for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        void SetPier(string name,
            string namePier,
            eItemType itemType = eItemType.Object);
    }
}
#endif