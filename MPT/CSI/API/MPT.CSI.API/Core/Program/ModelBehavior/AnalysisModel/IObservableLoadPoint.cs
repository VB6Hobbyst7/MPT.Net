// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 06-15-2017
//
// Last Modified By : Mark
// Last Modified On : 06-15-2017
// ***********************************************************************
// <copyright file="IObservableLoadPoint.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel
{
    /// <summary>
    /// Object can return distributed load data.
    /// </summary>
    public interface IObservableLoadPoint
    {
        /// <summary>
        /// This function retrieves the distributed load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">The total number of point loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each point load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each point load.</param>
        /// <param name="forceTypes">Force type for the point load for each load pattern.</param>
        /// <param name="coordinateSystems">Coordinated system used for each point load.
        /// It may be Local or the name of a defined coordinate system.</param>
        /// <param name="loadDirections">Direction that the load is applied in for each load pattern.</param>
        /// <param name="relativeDistanceFromI">The relative distance from the I-End of the element to the location of the point load.</param>
        /// <param name="absoluteDistanceFromI">The actual distance from the I-End of the element to the location of the point load. [L]</param>
        /// <param name="pointLoadValues">The load value of the point loads.
        /// [F] when <paramref name="forceTypes" /> is <see cref="eLoadForceType.Force" />  and [F*L] when <paramref name="forceTypes" /> is <see cref="eLoadForceType.Moment" />.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the load assignments are retrieved for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the load assignments are retrieved for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the load assignments are retrieved for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the load assignments are retrieved for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void GetLoadPoint(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref eLoadForceType[] forceTypes,
            ref string[] coordinateSystems,
            ref eLoadDirection[] loadDirections,
            ref double[] relativeDistanceFromI,
            ref double[] absoluteDistanceFromI,
            ref double[] pointLoadValues,
            eItemTypeElement itemType = eItemTypeElement.Element);
    }
}
