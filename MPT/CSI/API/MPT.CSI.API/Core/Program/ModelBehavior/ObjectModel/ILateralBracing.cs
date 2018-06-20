// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-06-2017
// ***********************************************************************
// <copyright file="ILateralBracing.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has CRUDable lateral bracing.
    /// </summary>
    public interface ILateralBracing
    {
        /// <summary>
        /// This function retrieves the lateral bracing location assignments for frame objects.
        /// </summary>
        /// <param name="name">The name of an existing frame object.</param>
        /// <param name="numberItems">The total number of bracing assignments retrieved for the specified frame objects.</param>
        /// <param name="frameNames">The name of the frame object associated with each bracing assignment.</param>
        /// <param name="bracingTypes">The bracing types assigned for each bracing assignment.</param>
        /// <param name="bracingLocations">The bracing locations for each bracing assignment along the depth of the cross section.</param>
        /// <param name="relativeDistanceStartBracing">The relative location of the point bracing or start of the distributed bracing.</param>
        /// <param name="relativeDistanceEndBracing">The relative location of the end of the uniform bracing.
        /// This item does not apply when <paramref name="bracingTypes" /> = <see cref="eBracingType.Point" />.</param>
        /// <param name="actuaDistanceStartBracing">The absolute location of the point bracing or start of the distributed bracing.</param>
        /// <param name="actualDistanceEndBracing">The absolute location of the end of the uniform bracing.
        /// This item does not apply when <paramref name="bracingTypes" /> = <see cref="eBracingType.Point" />.</param>
        void GetLateralBracing(string name,
            ref int numberItems,
            ref string[] frameNames,
            ref eBracingType[] bracingTypes,
            ref eBracingLocation[] bracingLocations,
            ref double[] relativeDistanceStartBracing,
            ref double[] relativeDistanceEndBracing,
            ref double[] actuaDistanceStartBracing,
            ref double[] actualDistanceEndBracing);

        /// <summary>
        /// This function assigns a lateral bracing location to frame objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="bracingType">The bracing type to assign.</param>
        /// <param name="bracingLocation">The bracing location along the depth of the cross section.</param>
        /// <param name="distanceStartBracing">The location of the point bracing or start of the distributed bracing.</param>
        /// <param name="distanceEndBracing">The location of the end of the uniform bracing.
        /// This item does not apply when <paramref name="bracingType" /> = <see cref="eBracingType.Point" />.</param>
        /// <param name="relativeDistance">True: <parameref name="distanceStartBracing" /> and <parameref name="distanceEndBracing" /> are relative distances.
        /// Otherwise, distances are absolute.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetLateralBracing(string name,
            eBracingType bracingType,
            eBracingLocation bracingLocation,
            double distanceStartBracing,
            double distanceEndBracing,
            bool relativeDistance = true,
            eItemType itemType = eItemType.Object);

        /// <summary>
        /// This function deletes the lateral bracing assignments to the specified objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="bracingType">Indicates the bracing type to be deleted.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void DeleteLateralBracing(string name,
            eBracingType bracingType,
            eItemType itemType = eItemType.Object);
    }
}