// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-15-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="ILoadSurfacePressure.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has a CRUDable surface pressure load.
    /// </summary>
    public interface ILoadSurfacePressure
    {
        /// <summary>
        /// This function retrieves the surface pressure assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">The total number of surface pressure loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each surface pressure load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each surface pressure load.</param>
        /// <param name="faceApplied">The element face to which the specified load assignment applies.
        /// Note that edge face n is from plane element point n to plane element point n + 1. For example, edge face 2 is from plane element point 2 to plane element point 3.</param>
        /// <param name="surfacePressureLoadValues">The surface pressure values. [F/L^2]</param>
        /// <param name="jointPatternNames">The joint pattern name, if any, used to specify each surface pressure load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the load assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the load assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the load assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void GetLoadSurfacePressure(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref eFace[] faceApplied,
            ref double[] surfacePressureLoadValues,
            ref string[] jointPatternNames,
            eItemType itemType = eItemType.Object);

        /// <summary>
        /// This function assigns surface pressure loads to objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the surface pressure load.</param>
        /// <param name="faceApplied">The element face to which the specified load assignment applies.
        /// Note that edge face n is from plane element point n to plane element point n + 1. For example, edge face 2 is from plane element point 2 to plane element point 3.</param>
        /// <param name="surfacePressureLoadValue">The surface pressure values. [F/L^2]</param>
        /// <param name="jointPatternName">The joint pattern name, if any, used to specify the surface pressure load.</param>
        /// <param name="replace">True: All previous uniform loads, if any, assigned to the specified object(s), in the specified load pattern, are deleted before making the new assignment.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetLoadSurfacePressure(string name,
            string loadPattern,
            eFace faceApplied,
            double surfacePressureLoadValue,
            string jointPatternName,
            bool replace = true,
            eItemType itemType = eItemType.Object);

        /// <summary>
        /// This function deletes the surface pressure load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void DeleteLoadSurfacePressure(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object);
    }
}
#endif