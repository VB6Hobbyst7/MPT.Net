// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-20-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="ILoadForceStress.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has a CRUDable force/stress load.
    /// </summary>
    public interface ILoadForceStress
    {
        /// <summary>
        /// This function retrieves the force stress load assignments to elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="numberItems">The total number of uniform loads retrieved for the specified elements.</param>
        /// <param name="tendonNames">The name of the element associated with each force stress load.</param>
        /// <param name="loadPatterns">The name of the load pattern associated with each force stress load.</param>
        /// <param name="jackedFrom">Indicates how the tendon is jacked.</param>
        /// <param name="loadTypes">Indicates the type of load.</param>
        /// <param name="values">The load value. [F] when <paramref name="loadTypes" /> = <see cref="eTendonLoadType.Force" />, and [F/L^2] when <paramref name="loadTypes" /> = <see cref="eTendonLoadType.Stress" />.</param>
        /// <param name="curvatureCoefficients">The curvature coefficient used when calculating friction losses.</param>
        /// <param name="wobbleCoefficients">The wobble coefficient used when calculating friction losses. [1/L]</param>
        /// <param name="lossAnchorages">The anchorage set slip. [L]</param>
        /// <param name="lossShortenings">The tendon stress loss due to elastic shortening. [F/L^2]</param>
        /// <param name="lossCreep">The tendon stress loss due to creep. [F/L^2]</param>
        /// <param name="lossShrinkages">The tendon stress loss due to shrinkage. [F/L^2]</param>
        /// <param name="lossSteelRelax">The tendon stress loss due to tendon steel relaxation. [F/L^2]</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are retrieved for the objects specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void GetLoadForceStress(string name,
            ref int numberItems,
            ref string[] tendonNames,
            ref string[] loadPatterns,
            ref eTendonJack[] jackedFrom,
            ref eTendonLoadType[] loadTypes,
            ref double[] values,
            ref double[] curvatureCoefficients,
            ref double[] wobbleCoefficients,
            ref double[] lossAnchorages,
            ref double[] lossShortenings,
            ref double[] lossCreep,
            ref double[] lossShrinkages,
            ref double[] lossSteelRelax,
            eItemType itemType = eItemType.Object);

        /// <summary>
        /// This function assigns force stress loads to objects.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the force stress load.</param>
        /// <param name="jackedFrom">Indicates how the tendon is jacked.</param>
        /// <param name="loadType">Indicates the type of load.</param>
        /// <param name="value">The load value. [F] when <paramref name="loadType" /> = <see cref="eTendonLoadType.Force" />, and [F/L^2] when <paramref name="loadType" /> = <see cref="eTendonLoadType.Stress" />.</param>
        /// <param name="curvatureCoefficient">The curvature coefficient used when calculating friction losses.</param>
        /// <param name="wobbleCoefficient">The wobble coefficient used when calculating friction losses. [1/L]</param>
        /// <param name="lossAnchorage">The anchorage set slip. [L]</param>
        /// <param name="lossShortening">The tendon stress loss due to elastic shortening. [F/L^2]</param>
        /// <param name="lossCreep">The tendon stress loss due to creep. [F/L^2]</param>
        /// <param name="lossShrinkage">The tendon stress loss due to shrinkage. [F/L^2]</param>
        /// <param name="lossSteelRelax">The tendon stress loss due to tendon steel relaxation. [F/L^2]</param>
        /// <param name="replace">True: All previous force stress loads, if any, assigned to the specified object(s), in the specified load pattern, are deleted before making the new assignment.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetLoadForceStress(string name,
            string loadPattern,
            eTendonJack jackedFrom,
            eTendonLoadType loadType,
            double value,
            double curvatureCoefficient,
            double wobbleCoefficient,
            double lossAnchorage,
            double lossShortening,
            double lossCreep,
            double lossShrinkage,
            double lossSteelRelax,
            bool replace = true,
            eItemType itemType = eItemType.Object);

        /// <summary>
        /// This function deletes the force stress load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are deleted for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are deleted for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are deleted for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void DeleteLoadForceStress(string name,
            string loadPattern,
            eItemType itemType = eItemType.Object);
    }
}
#endif