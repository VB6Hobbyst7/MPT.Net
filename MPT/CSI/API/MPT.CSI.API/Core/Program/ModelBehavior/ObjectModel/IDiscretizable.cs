// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="IDiscretizable.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has gettable/settable discretization attributes.
    /// </summary>
    public interface IDiscretizable
    {
        /// <summary>
        /// This function retrieves the maximum discretization length assignment for tendon objects.
        /// </summary>
        /// <param name="name">The name of an existing tendon object.</param>
        /// <param name="maxDiscretizationLength">The maximum discretization length for the tendon. [L]</param>
        void GetDiscretization(string name,
            ref double maxDiscretizationLength);

        /// <summary>
        /// This function assigns a maximum discretization length to tendon objects.
        /// </summary>
        /// <param name="name">The name of an existing tendon object.</param>
        /// <param name="maxDiscretizationLength">The maximum discretization length for the tendon. [L]</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetDiscretization(string name,
            double maxDiscretizationLength,
            eItemType itemType = eItemType.Object);
    }
}
#endif