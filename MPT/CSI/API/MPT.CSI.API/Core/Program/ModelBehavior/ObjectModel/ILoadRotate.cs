// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-15-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="ILoadRotate.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has a CRUDable rotational load.
    /// </summary>
    public interface ILoadRotate
    {
        /// <summary>
        /// This function retrieves the rotational load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">The total number of rotate loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each rotate load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each rotate load.</param>
        /// <param name="rotateLoadValues">Rotate load values, angular velocity. [Cyc/T]</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the load assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the load assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the load assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void GetLoadRotate(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref double[] rotateLoadValues,
            eItemType itemType = eItemType.Object);

        /// <summary>
        /// This function assigns rotational loads to objects.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the rotational load.</param>
        /// <param name="rotateLoadValue">Rotate load value, angular velocity. [Cyc/T]</param>
        void SetLoadRotate(string name,
            string loadPattern,
            double rotateLoadValue);

        /// <summary>
        /// This function deletes the rotation load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void DeleteLoadRotate(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object);
    }
}
#endif