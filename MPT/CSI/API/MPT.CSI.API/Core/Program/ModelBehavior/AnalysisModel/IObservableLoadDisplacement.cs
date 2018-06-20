// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 07-07-2017
//
// Last Modified By : Mark
// Last Modified On : 07-07-2017
// ***********************************************************************
// <copyright file="IObservableLoadDisplacement.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Helpers;

namespace MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel
{
    /// <summary>
    /// Object can return point displacement data.
    /// </summary>
    public interface IObservableLoadDisplacement
    {


        /// <summary>
        /// This function returns a list of ground displacement load assignments made to one or more specified point elements.
        /// </summary>
        /// <param name="name">The name of an existing point object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">This is the total number of ground displacement load assignments returned.</param>
        /// <param name="pointNames">The name of the point element to which the specified ground displacement load assignment applies.</param>
        /// <param name="loadPatterns">The name of the load pattern for each load.</param>
        /// <param name="loadPatternSteps">The load pattern step for each load.
        /// In most cases this item does not apply and will be returned as 0.</param>
        /// <param name="coordinateSystem">The name of the coordinate system for the ground displacement.
        /// This is either Local or the name of a defined coordinate system.</param>
        /// <param name="displacements">The ground displacements assigned along the local or global axis direction, depending on the specified <paramref name="coordinateSystem" />.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the load assignments are retrieved for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the load assignments are retrieved for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the load assignments are retrieved for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the load assignments are retrieved for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void GetLoadDisplacement(string name,
            ref int numberItems,
            ref string[] pointNames,
            ref string[] loadPatterns,
            ref int[] loadPatternSteps,
            ref string[] coordinateSystem,
            ref Displacements[] displacements,
            eItemTypeElement itemType = eItemTypeElement.Element);
    }
}