// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 06-15-2017
//
// Last Modified By : Mark
// Last Modified On : 09-28-2017
// ***********************************************************************
// <copyright file="IObservableLoadSurfacePressure.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel
{
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
    /// <summary>
    /// Object can return surface pressure data.
    /// </summary>
    public interface IObservableLoadSurfacePressure
    {
        /// <summary>
        /// This function retrieves the surface pressure assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">The total number of surface pressure loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each surface pressure load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each surface pressure load.</param>
        /// <param name="faceApplied">The element face to which each specified load assignment applies.
        /// Note that edge face n is from plane element point n to plane element point n + 1. For example, edge face 2 is from plane element point 2 to plane element point 3.</param>
        /// <param name="surfacePressureLoadValues">The surface pressure values. [F/L^2]</param>
        /// <param name="jointPatternNames">The joint pattern name, if any, used to specify each surface pressure load.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the load assignments are retrieved for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the load assignments are retrieved for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the load assignments are retrieved for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the load assignments are retrieved for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void GetLoadSurfacePressure(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref eFace[] faceApplied,
            ref double[] surfacePressureLoadValues,
            ref string[] jointPatternNames,
            eItemTypeElement itemType = eItemTypeElement.Element);
    }
#endif
}
