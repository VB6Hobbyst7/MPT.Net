// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-20-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="ILocalAxesAdvanced.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has advanced local axes.
    /// </summary>
    public interface ILocalAxesAdvanced
    {
        /// <summary>
        /// This function gets the advanced local axes data for an existing object.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="isActive">True: Advanced local axes exist.</param>
        /// <param name="plane2">This is 12 or 13, indicating that the local plane determined by the plane reference vector is the 1-2 plane or the 1-3 plane.
        /// This item applies only when the <paramref name="isActive" /> = True.</param>
        /// <param name="planeVectorOption">Indicates the plane reference vector option.
        /// This item applies only when <paramref name="isActive" /> is True.</param>
        /// <param name="planeCoordinateSystem">The coordinate system used to define the plane reference vector coordinate directions and the plane user vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="planeVectorOption" /> is Coordinate Direction or User Vector.</param>
        /// <param name="planeVectorDirection">Indicates the plane reference vector primary and secondary coordinate directions, (0) and (1) respectively, taken at the object center in the specified coordinate system and used to determine the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="planeVectorOption" /> is Coordinate Direction.</param>
        /// <param name="planePoint">Indicates the labels of two joints that define the plane reference vector.
        /// Either of these joints may be specified as None to indicate the center of the specified object.
        /// If both joints are specified as None, they are not used to define the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="planeVectorOption" /> is Two Joints.</param>
        /// <param name="planeReferenceVector">Defines the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="planeVectorOption" /> is User Vector.</param>
        void GetLocalAxesAdvanced(string name,
            ref bool isActive,
            ref int plane2,
            ref eReferenceVector planeVectorOption,
            ref string planeCoordinateSystem,
            ref eReferenceVectorDirection[] planeVectorDirection,
            ref string[] planePoint,
            ref double[] planeReferenceVector);


        /// <summary>
        /// This function sets the advanced local axes data for an existing object.
        /// </summary>
        /// <param name="name">The name of an existing frame object or group depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="isActive">True: Advanced local axes exist.</param>
        /// <param name="plane2">This is 12 or 13, indicating that the local plane determined by the plane reference vector is the 1-2 plane or the 1-3 plane.
        /// This item applies only when the <paramref name="isActive" /> = True.</param>
        /// <param name="planeVectorOption">Indicates the plane reference vector option.
        /// This item applies only when <paramref name="isActive" /> is True.</param>
        /// <param name="planeCoordinateSystem">The coordinate system used to define the plane reference vector coordinate directions and the plane user vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="planeVectorOption" /> is Coordinate Direction or User Vector.</param>
        /// <param name="planeVectorDirection">Indicates the plane reference vector primary and secondary coordinate directions, (0) and (1) respectively, taken at the object center in the specified coordinate system and used to determine the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="planeVectorOption" /> is Coordinate Direction.</param>
        /// <param name="planePoint">Indicates the labels of two joints that define the plane reference vector.
        /// Either of these joints may be specified as None to indicate the center of the specified object.
        /// If both joints are specified as None, they are not used to define the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="planeVectorOption" /> is Two Joints.</param>
        /// <param name="planeReferenceVector">Defines the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="planeVectorOption" /> is User Vector.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetLocalAxesAdvanced(string name,
            bool isActive,
            int plane2,
            eReferenceVector planeVectorOption,
            string planeCoordinateSystem,
            eReferenceVectorDirection[] planeVectorDirection,
            string[] planePoint,
            double[] planeReferenceVector,
            eItemType itemType = eItemType.Object);
    }
}
#endif