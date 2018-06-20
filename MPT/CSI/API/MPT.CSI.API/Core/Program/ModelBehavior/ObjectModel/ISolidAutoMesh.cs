// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-20-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="ISolidAutoMesh.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object can be automeshed as a solid.
    /// </summary>
    public interface ISolidAutoMesh
    {
        /// <summary>
        /// This function retrieves automatic meshing assignments to objects.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <param name="meshType">The automatic mesh type for the object.</param>
        /// <param name="numberOfObjectsAlongPoint12">This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedNumber" />.
        /// It is the number of objects created along the edge of the meshed object that runs from point 1 to point 2.</param>
        /// <param name="numberOfObjectsAlongPoint13">This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedNumber" />.
        /// It is the number of objects created along the edge of the meshed object that runs from point 1 to point 3.</param>
        /// <param name="numberOfObjectsAlongPoint15">This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedNumber" />.
        /// It is the number of objects created along the edge of the meshed object that runs from point 1 to point 5.</param>
        /// <param name="maxSizeOfObjectsAlongPoint12">This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedMaxSize" />.
        /// It is the maximum size of objects created along the edge of the meshed object that runs from point 1 to point 2 [L]
        /// If this item is input as 0, the default value is used.
        /// The default value is 48 inches if the database units are English or 120 centimeters if the database units are metric..</param>
        /// <param name="maxSizeOfObjectsAlongPoint13">This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedMaxSize" />.
        /// It is the maximum size of objects created along the edge of the meshed object that runs from point 1 to point 3. [L]
        /// If this item is input as 0, the default value is used.
        /// The default value is 48 inches if the database units are English or 120 centimeters if the database units are metric.</param>
        /// <param name="maxSizeOfObjectsAlongPoint15">This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedMaxSize" />.
        /// It is the maximum size of objects created along the edge of the meshed object that runs from point 1 to point 5. [L]
        /// If this item is input as 0, the default value is used.
        /// The default value is 48 inches if the database units are English or 120 centimeters if the database units are metric.</param>
        /// <param name="restraintsOnEdge">True: If both points along an edge of the original object have the same restraint/constraint, then, if an added point on that edge and the original corner points have the same local axes definition, the program assigns the restraint/constraint to the added point.</param>
        /// <param name="restraintsOnFace">True: If all corner points on an object face have the same restraint/constraint, then, if an added point on that face and the original corner points for the face have the same local axes definition, the program assigns the restraint/constraint to the added point.</param>
        void GetAutoMesh(string name,
            ref eMeshType meshType,
            ref int numberOfObjectsAlongPoint12,
            ref int numberOfObjectsAlongPoint13,
            ref int numberOfObjectsAlongPoint15,
            ref double maxSizeOfObjectsAlongPoint12,
            ref double maxSizeOfObjectsAlongPoint13,
            ref double maxSizeOfObjectsAlongPoint15,
            ref bool restraintsOnEdge,
            ref bool restraintsOnFace);

        /// <summary>
        /// This function makes automatic meshing assignments to objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="meshType">The automatic mesh type for the object.</param>
        /// <param name="numberOfObjectsAlongPoint12">This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedNumber" />.
        /// It is the number of objects created along the edge of the meshed object that runs from point 1 to point 2.</param>
        /// <param name="numberOfObjectsAlongPoint13">This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedNumber" />.
        /// It is the number of objects created along the edge of the meshed object that runs from point 1 to point 3.</param>
        /// <param name="numberOfObjectsAlongPoint15">This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedNumber" />.
        /// It is the number of objects created along the edge of the meshed object that runs from point 1 to point 5.</param>
        /// <param name="maxSizeOfObjectsAlongPoint12">This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedMaxSize" />.
        /// It is the maximum size of objects created along the edge of the meshed object that runs from point 1 to point 2. [L]
        /// If this item is input as 0, the default value is used.
        /// The default value is 48 inches if the database units are English or 120 centimeters if the database units are metric.</param>
        /// <param name="maxSizeOfObjectsAlongPoint13">This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedMaxSize" />.
        /// It is the maximum size of objects created along the edge of the meshed object that runs from point 1 to point 3. [L]
        /// If this item is input as 0, the default value is used.
        /// The default value is 48 inches if the database units are English or 120 centimeters if the database units are metric.</param>
        /// <param name="maxSizeOfObjectsAlongPoint15">This item applies when <paramref name="meshType" /> = <see cref="eMeshType.SpecifiedMaxSize" />.
        /// It is the maximum size of objects created along the edge of the meshed object that runs from point 1 to point 5. [L]
        /// If this item is input as 0, the default value is used.
        /// The default value is 48 inches if the database units are English or 120 centimeters if the database units are metric.</param>
        /// <param name="restraintsOnEdge">True: If both points along an edge of the original object have the same restraint/constraint, then, if an added point on that edge and the original corner points have the same local axes definition, the program assigns the restraint/constraint to the added point.</param>
        /// <param name="restraintsOnFace">True: If all corner points on an object face have the same restraint/constraint, then, if an added point on that face and the original corner points for the face have the same local axes definition, the program assigns the restraint/constraint to the added point.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetAutoMesh(string name,
            eMeshType meshType,
            int numberOfObjectsAlongPoint12 = 2,
            int numberOfObjectsAlongPoint13 = 2,
            int numberOfObjectsAlongPoint15 = 2,
            double maxSizeOfObjectsAlongPoint12 = 0,
            double maxSizeOfObjectsAlongPoint13 = 0,
            double maxSizeOfObjectsAlongPoint15 = 0,
            bool restraintsOnEdge = false,
            bool restraintsOnFace = false,
            eItemType itemType = eItemType.Object);
    }
}
#endif