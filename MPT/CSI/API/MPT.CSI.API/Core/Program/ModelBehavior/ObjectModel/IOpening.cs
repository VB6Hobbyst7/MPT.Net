// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-02-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="IOpening.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object can be classified as an opening rather than a solid object.
    /// </summary>
    public interface IOpening
    {
        /// <summary>
        /// Retrieves whether the specified area object is an opening.
        /// </summary>
        /// <param name="name">The name of an existing area object.</param>
        /// <param name="isOpening">True: Specified area object is an opening.</param>
        void GetOpening(string name,
            ref bool isOpening);

        /// <summary>
        /// Designates an area object(s) as an opening.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType"/> item.</param>
        /// <param name="isOpening">True: Specified area object is an opening.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object"/>, the assignments are made for the object specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.Group"/>, the assignments are made for the objects included in the group specified by the <paramref name="name"/> item.
        /// If this item is <see cref="eItemType.SelectedObjects"/>, the assignments are made for all selected objects, and the <paramref name="name"/> item is ignored.</param>
        void SetOpening(string name,
            bool isOpening,
            eItemType itemType = eItemType.Object);
    }
}
#endif