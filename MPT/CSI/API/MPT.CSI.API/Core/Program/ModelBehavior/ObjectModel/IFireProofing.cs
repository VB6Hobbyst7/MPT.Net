// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="IFireProofing.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has CRUDable fireproofing.
    /// </summary>
    public interface IFireProofing
    {
        /// <summary>
        /// This function gets the fireproofing assignment to an existing frame object.
        /// </summary>
        /// <param name="name">The name of an existing frame object.</param>
        /// <param name="type">Type of fireproofing assigned.</param>
        /// <param name="thickness">When <paramref name="type" /> = <see cref="eFireProofing.SprayedOnProgramPerimeterCalc" /> or <see cref="eFireProofing.SprayedOnUserPerimeterDefine" /> this is the thickness of the sprayed on fireproofing.
        /// When <paramref name="type" /> = <see cref="eFireProofing.ConcreteEncased" /> this is the concrete cover dimension. [L]</param>
        /// <param name="perimeter">This item applies only when <paramref name="type" /> = <see cref="eFireProofing.SprayedOnUserPerimeterDefine" />.
        /// It is the length of fireproofing applied measured around the perimeter of the frame object cross-section. [L]</param>
        /// <param name="density">This is the weight per unit volume of the fireproofing material. [F/L^3]</param>
        /// <param name="appliedToTopFlange">True: Fireproofing is assumed to be applied to the top flange of the section.
        /// False: Program assumes no fireproofing is applied to the section top flange.
        /// This flag applies for I, channel and double channel sections only when <paramref name="type" /> = <see cref="eFireProofing.SprayedOnProgramPerimeterCalc" /> or <see cref="eFireProofing.ConcreteEncased" />.</param>
        /// <param name="includeInSelfWeight">True: Fireproofing is included in the structure self weight.</param>
        /// <param name="includeInGravityLoads">True: Fireproofing is included gravity loads applied in the X, Y and Z directions</param>
        /// <param name="includedLoadPattern">This item is either None or the name of an existing load pattern.
        /// If it is the name of a load pattern then the weight of the fireproofing is applied as a distributed load in the global Z direction in the load pattern.</param>
        void GetFireProofing(string name,
            ref eFireProofing type,
            ref double thickness,
            ref double perimeter,
            ref double density,
            ref bool appliedToTopFlange,
            ref bool includeInSelfWeight,
            ref bool includeInGravityLoads,
            ref string includedLoadPattern);

        /// <summary>
        /// This function sets the fireproofing assignments to existing frame objects.
        /// </summary>
        /// <param name="name">The name of an existing frame object.</param>
        /// <param name="type">Type of fireproofing assigned.</param>
        /// <param name="thickness">When <paramref name="type" /> = <see cref="eFireProofing.SprayedOnProgramPerimeterCalc" /> or <see cref="eFireProofing.SprayedOnUserPerimeterDefine" /> this is the thickness of the sprayed on fireproofing.
        /// When <paramref name="type" /> = <see cref="eFireProofing.ConcreteEncased" /> this is the concrete cover dimension. [L]</param>
        /// <param name="perimeter">This item applies only when <paramref name="type" /> = <see cref="eFireProofing.SprayedOnUserPerimeterDefine" />.
        /// It is the length of fireproofing applied measured around the perimeter of the frame object cross-section. [L]</param>
        /// <param name="density">This is the weight per unit volume of the fireproofing material. [F/L^3]</param>
        /// <param name="appliedToTopFlange">True: Fireproofing is assumed to be applied to the top flange of the section.
        /// False: Program assumes no fireproofing is applied to the section top flange.
        /// This flag applies for I, channel and double channel sections only when <paramref name="type" /> = <see cref="eFireProofing.SprayedOnProgramPerimeterCalc" /> or <see cref="eFireProofing.ConcreteEncased" />.</param>
        /// <param name="includeInSelfWeight">True: Fireproofing is included in the structure self weight.</param>
        /// <param name="includeInGravityLoads">True: Fireproofing is included gravity loads applied in the X, Y and Z directions</param>
        /// <param name="includedLoadPattern">This item is either None or the name of an existing load pattern.
        /// If it is the name of a load pattern then the weight of the fireproofing is applied as a distributed load in the global Z direction in the load pattern.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetFireProofing(string name,
            eFireProofing type,
            double thickness,
            double perimeter,
            double density,
            bool appliedToTopFlange,
            bool includeInSelfWeight,
            bool includeInGravityLoads,
            string includedLoadPattern,
            eItemType itemType = eItemType.Object);

        /// <summary>
        /// This function deletes the fireproofing assignments to the specified objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void DeleteFireProofing(string name,
            eItemType itemType = eItemType.Object);
    }
}
#endif