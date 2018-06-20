// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 06-15-2017
//
// Last Modified By : Mark
// Last Modified On : 06-15-2017
// ***********************************************************************
// <copyright file="IObservableLoadDistributed.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************


namespace MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel
{
    /// <summary>
    /// Object can return distributed load data.
    /// </summary>
    public interface IObservableLoadDistributed
    {
        /// <summary>
        /// This function retrieves the distributed load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">The total number of distributed loads retrieved for the specified elements.</param>
        /// <param name="names">The name of the element associated with each distributed load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each distributed load.</param>
        /// <param name="forceTypes">Force type for the distributed load for each load pattern.</param>
        /// <param name="coordinateSystems">Coordinated system used for each distributed load.
        /// It may be Local or the name of a defined coordinate system.</param>
        /// <param name="loadDirections">Direction that the load is applied in for each load pattern.</param>
        /// <param name="relativeDistanceStartFromI">The relative distance from the I-End of the element to the start of the distributed load.</param>
        /// <param name="relativeDistanceEndFromI">The relative distance from the I-End of the element to the end of the distributed load.</param>
        /// <param name="absoluteDistanceStartFromI">The actual distance from the I-End of the element to the start of the distributed load. [L]</param>
        /// <param name="absoluteDistanceEndFromI">The actual distance from the I-End of the element to the end of the distributed load. [L]</param>
        /// <param name="startLoadValues">The load value at the start of the distributed load.
        /// [F/L] when <paramref name="forceTypes" /> is <see cref="eLoadForceType.Force" />  and [F*L/L] when <paramref name="forceTypes" /> is <see cref="eLoadForceType.Moment" />.</param>
        /// <param name="endLoadValues">The load value at the end of the distributed load.
        /// [F/L] when <paramref name="forceTypes" /> is <see cref="eLoadForceType.Force" />  and [F*L/L] when <paramref name="forceTypes" /> is <see cref="eLoadForceType.Moment" />.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the load assignments are retrieved for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the load assignments are retrieved for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the load assignments are retrieved for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the load assignments are retrieved for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void GetLoadDistributed(string name,
            ref int numberItems,
            ref string[] names,
            ref string[] loadPatterns,
            ref eLoadForceType[] forceTypes,
            ref string[] coordinateSystems,
            ref eLoadDirection[] loadDirections,
            ref double[] relativeDistanceStartFromI,
            ref double[] relativeDistanceEndFromI,
            ref double[] absoluteDistanceStartFromI,
            ref double[] absoluteDistanceEndFromI,
            ref double[] startLoadValues,
            ref double[] endLoadValues,
            eItemTypeElement itemType = eItemTypeElement.Element);
    }
}
