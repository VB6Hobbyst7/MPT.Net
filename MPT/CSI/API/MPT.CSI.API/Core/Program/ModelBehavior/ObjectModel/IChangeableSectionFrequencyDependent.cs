// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-20-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="IChangeableSectionFrequencyDependent.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object can change the property assignment name for a frequency-dependent link.
    /// </summary>
    public interface IChangeableSectionFrequencyDependent
    {
        /// <summary>
        /// This function assigns the frequency dependent property assignment to a link element.
        /// </summary>
        /// <param name="name">The name of an existing link object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="propertyName">The name of the frequency dependent link property assigned to the link element.
        /// None means that no frequency dependent link property is assigned to the link object.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetSectionFrequencyDependent(string name,
            string propertyName,
            eItemType itemType = eItemType.Object);
    }
}
#endif