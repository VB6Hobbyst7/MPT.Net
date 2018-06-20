// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 07-07-2017
//
// Last Modified By : Mark
// Last Modified On : 07-07-2017
// ***********************************************************************
// <copyright file="IObservableConstraint.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel
{
    /// <summary>
    /// Object can return point constraint data.
    /// </summary>
    public interface IObservableConstraint
    {
        /// <summary>
        /// If the <paramref name="name" /> item is provided, the function returns the total number of constraint assignments made to the specified point element.
        /// If the <paramref name="name" /> item is not specified or is specified as an empty string, the function returns the total number of constraint assignments to all point elements in the model.
        /// If the <paramref name="name" /> item is specified but it is not recognized by the program as a valid point element, an error is returned.
        /// TODO: Handle last case.
        /// </summary>
        /// <param name="name">The name of an existing point element.</param>
        /// <returns>System.Int32.</returns>
        int CountConstraint(string name = "");

        /// <summary>
        /// This function returns a list of constraint assignments made to one or more specified point elements.
        /// </summary>
        /// <param name="name">The name of an existing point object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">This is the total number of constraint assignments returned.</param>
        /// <param name="pointNames">The name of the point element to which the specified constraint assignment applies.</param>
        /// <param name="constraintNames">The name of the constraint that is assigned to the point element specified by the <paramref name="pointNames" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the load assignments are retrieved for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the load assignments are retrieved for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the load assignments are retrieved for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the load assignments are retrieved for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void GetConstraint(string name,
            ref int numberItems,
            ref string[] pointNames,
            ref string[] constraintNames,
            eItemTypeElement itemType = eItemTypeElement.Element);
    }
}