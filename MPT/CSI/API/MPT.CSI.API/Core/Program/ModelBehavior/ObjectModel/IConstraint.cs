// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="IConstraint.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has readbale/writeable constraint data.
    /// </summary>
    public interface IConstraint
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
        /// <param name="name">The name of an existing point object or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">This is the total number of constraint assignments returned.</param>
        /// <param name="pointNames">The name of the point element to which the specified constraint assignment applies.</param>
        /// <param name="constraintNames">The name of the constraint that is assigned to the point element specified by the <paramref name="pointNames" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are retrieved for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void GetConstraint(string name,
            ref int numberItems,
            ref string[] pointNames,
            ref string[] constraintNames,
            eItemType itemType = eItemType.Group);

        /// <summary>
        /// This function makes joint constraint assignments to point objects.
        /// </summary>
        /// <param name="name">The name of an existing point object or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="constraintName">The name of the existing joint constraint.</param>
        /// <param name="replace">True: All existing point assignments to the specified point object(s) are deleted prior to making the assignment.
        /// False: Mass assignments are added to any existing assignments.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are retrieved for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetConstraint(string name,
            string constraintName,
            bool replace = true,
            eItemType itemType = eItemType.Group);

        /// <summary>
        /// This function deletes all constraint assignments from the specified point object(s).
        /// </summary>
        /// <param name="name">The name of an existing point object or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are retrieved for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void DeleteConstraint(string name,
            eItemType itemType = eItemType.Object);
    }
}
#endif