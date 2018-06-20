// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-08-2017
// ***********************************************************************
// <copyright file="IPanelZone.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has CRUDable panel zones.
    /// </summary>
    public interface IPanelZone
    {
        /// <summary>
        /// This function retrieves the panel zone assignment data for a point object.
        /// If no panel zone assignment is made to the point object, an error is returned.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="name">The name of an existing point object.</param>
        /// <param name="propertyType">Method by which properties are determined for panel zones.</param>
        /// <param name="thickness">The thickness of the doubler plate.
        /// This item applies only when <paramref name="propertyType" /> = <see cref="ePanelZonePropertyType.ElasticFromColumnAndDoublerPlate" />. [L]</param>
        /// <param name="k1">The spring stiffness for major axis bending (about the local 3 axis of the column and panel zone).
        /// This item applies only when <paramref name="propertyType" /> = <see cref="ePanelZonePropertyType.FromSpringStiffness" />. [FL/rad]</param>
        /// <param name="k2">The spring stiffness for minor axis bending (about the local 2 axis of the column and panel zone).
        /// This item applies only when <paramref name="propertyType" /> = <see cref="ePanelZonePropertyType.FromSpringStiffness" />. [FL/rad]</param>
        /// <param name="linkProperty">The name of the link property used to define the panel zone.
        /// This item applies only when <paramref name="propertyType" /> = <see cref="ePanelZonePropertyType.FromLink" />.</param>
        /// <param name="connectivity">Panel zone connection types.</param>
        /// <param name="localAxisFrom">Method by which the local axis is defined.
        /// The <paramref name="localAxisFrom" /> item can be <see cref="ePanelZoneLocalAxis.UserDefined" /> only when the <paramref name="propertyType" /> item is <see cref="ePanelZonePropertyType.FromLink" />.</param>
        /// <param name="localAxisAngle">This item applies only when <paramref name="propertyType" /> = <see cref="ePanelZonePropertyType.FromLink" /> and <paramref name="localAxisFrom" /> = <see cref="ePanelZoneLocalAxis.UserDefined" />.
        /// It is the angle measured counter clockwise from the positive global X-axis to the local 2-axis of the panel zone. [deg]</param>
        void GetPanelZone(string name,
            ref ePanelZonePropertyType propertyType,
            ref double thickness,
            ref double k1,
            ref double k2,
            ref string linkProperty,
            ref ePanelZoneConnectivity connectivity,
            ref ePanelZoneLocalAxis localAxisFrom,
            ref double localAxisAngle);

        /// <summary>
        /// This function makes panel zone assignments to point objects. Any existing panel zone assignments are replaced by the new assignments.
        /// </summary>
        /// <param name="name">The name of an existing point object, element or group of objects, depending on the value of <paramref name="itemType" />.</param>
        /// <param name="propertyType">Method by which properties are determined for panel zones.</param>
        /// <param name="thickness">The thickness of the doubler plate.
        /// This item applies only when <paramref name="propertyType" /> = <see cref="ePanelZonePropertyType.ElasticFromColumnAndDoublerPlate" />. [L]</param>
        /// <param name="k1">The spring stiffness for major axis bending (about the local 3 axis of the column and panel zone).
        /// This item applies only when <paramref name="propertyType" /> = <see cref="ePanelZonePropertyType.FromSpringStiffness" />. [FL/rad]</param>
        /// <param name="k2">The spring stiffness for minor axis bending (about the local 2 axis of the column and panel zone).
        /// This item applies only when <paramref name="propertyType" /> = <see cref="ePanelZonePropertyType.FromSpringStiffness" />. [FL/rad]</param>
        /// <param name="linkProperty">The name of the link property used to define the panel zone.
        /// This item applies only when <paramref name="propertyType" /> = <see cref="ePanelZonePropertyType.FromLink" />.</param>
        /// <param name="connectivity">Panel zone connection types.</param>
        /// <param name="localAxisFrom">Method by which the local axis is defined.
        /// The <paramref name="localAxisFrom" /> item can be <see cref="ePanelZoneLocalAxis.UserDefined" /> only when the <paramref name="propertyType" /> item is <see cref="ePanelZonePropertyType.FromLink" />.</param>
        /// <param name="localAxisAngle">This item applies only when <paramref name="propertyType" /> = <see cref="ePanelZonePropertyType.FromLink" /> and <paramref name="localAxisFrom" /> = <see cref="ePanelZoneLocalAxis.UserDefined" />.
        /// It is the angle measured counter clockwise from the positive global X-axis to the local 2-axis of the panel zone. [deg]</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are retrieved for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are retrieved for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are retrieved for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetPanelZone(string name,
            ePanelZonePropertyType propertyType,
            double thickness,
            double k1,
            double k2,
            string linkProperty,
            ePanelZoneConnectivity connectivity,
            ePanelZoneLocalAxis localAxisFrom,
            double localAxisAngle,
            eItemType itemType = eItemType.Object);


        /// <summary>
        /// This function deletes all panel zone assignments from the specified point object(s).
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void DeletePanelZone(string name,
            eItemType itemType = eItemType.Object);
    }
}