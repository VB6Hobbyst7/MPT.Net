// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="IConstraints.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Helpers;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
    /// <summary>
    /// Implements the constraints in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    public interface IConstraints : IChangeableName, ICountable, IDeletable, IListableNames
    {
#else
    /// <summary>
    /// Implements the constraints in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    public interface IConstraints : IDeletable, IListableNames
    {
#endif
        #region Methods: Public
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns the number of defined constraints of the specified type.
        /// </summary>
        /// <param name="constraintType">Type of constraint to return a count for.</param>
        /// <returns>System.Int32.</returns>
        int Count(eConstraintType constraintType);

        /// <summary>
        /// The function returns the constraint type for the specified constraint.
        /// </summary>
        /// <param name="nameConstraint">The name of an existing constraint.</param>
        /// <param name="constraintType">Constraint type assigned to the named cosntraint.</param>
        void GetConstraintType(string nameConstraint,
            ref eConstraintType constraintType);

        /// <summary>
        /// This function retrieves the list of the names of each special rigid diaphragm constraint.
        /// A special rigid diaphragm constraint is required for assignment of auto seismic load diaphragm eccentricity overwrites.
        /// It is also required for calculation of auto wind loads whose exposure widths are determined from the extents of rigid diaphragms.
        /// A special rigid diaphragm constraint is a constraint with the following features:
        /// 1. The constraint type is Diaphragm.
        /// 2. The constraint coordinate system is Global.
        /// 3. The constraint axis is Z.
        /// </summary>
        /// <param name="diaphragms">The name of each special rigid diaphragm constraint.</param>
        void GetSpecialRigidDiaphragmList(ref string[] diaphragms);
#endif

        // === Get/Set
        /// <summary>
        /// The function returns the definition for the specified Diaphragm constraint.
        /// </summary>
        /// <param name="nameConstraint">The name of a constraint.</param>
        /// <param name="axis">Specifies the axis in the specified coordinate system that is perpendicular to the plane of the constraint.</param>
        /// <param name="nameCoordinateSystem">The name of the coordinate system in which the constraint is defined.</param>
        void GetDiaphragm(string nameConstraint,
            ref eConstraintAxis axis,
            ref string nameCoordinateSystem);

        /// <summary>
        /// This function defines a Diaphragm constraint.
        /// If the specified name is not used for a constraint, a new constraint is defined using the specified name.
        /// If the specified name is already used for another Diaphragm constraint, the definition of that constraint is modified.
        /// If the specified name is already used for some constraint that is not a Diaphragm constraint, an error is returned.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="nameConstraint">The name of a constraint.</param>
        /// <param name="axis">Specifies the axis in the specified coordinate system that is perpendicular to the plane of the constraint.</param>
        /// <param name="nameCoordinateSystem">The name of the coordinate system in which the constraint is defined.</param>
        void SetDiaphragm(string nameConstraint,
            eConstraintAxis axis = eConstraintAxis.AutoAxis,
            string nameCoordinateSystem = CoordinateSystems.Global);

        // ===
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// The function returns the definition for the specified Beam constraint.
        /// </summary>
        /// <param name="nameConstraint">The name of an existing constraint.</param>
        /// <param name="axis">Specifies the axis in the specified coordinate system that is parallel to the axis of the constraint.
        /// If AutoAxis is specified, the axis of the constraint is automatically determined from the joints assigned to the constraint.</param>
        /// <param name="nameCoordinateSystem">The name of the coordinate system in which the constraint is defined.</param>
        void GetBeam(string nameConstraint,
            ref eConstraintAxis axis,
            ref string nameCoordinateSystem);

        /// <summary>
        /// This function defines a Beam constraint.
        /// If the specified name is not used for a constraint, a new constraint is defined using the specified name.
        /// If the specified name is already used for another Beam constraint, the definition of that constraint is modified.
        /// If the specified name is already used for some constraint that is not a Beam constraint, an error is returned.
        /// </summary>
        /// <param name="nameConstraint">The name of an existing constraint.</param>
        /// <param name="axis">Specifies the axis in the specified coordinate system that is parallel to the axis of the constraint.
        /// If AutoAxis is specified, the axis of the constraint is automatically determined from the joints assigned to the constraint.</param>
        /// <param name="nameCoordinateSystem">The name of the coordinate system in which the constraint is defined.</param>
        void SetBeam(string nameConstraint,
            eConstraintAxis axis,
            string nameCoordinateSystem);

        // ===

        /// <summary>
        /// The function returns the definition for the specified Body constraint
        /// </summary>
        /// <param name="nameConstraint">The name of an existing constraint.</param>
        /// <param name="degreesOfFreedom">Indicates which joint degrees of freedom are included in the constraint.</param>
        /// <param name="nameCoordinateSystem">The name of the coordinate system in which the constraint is defined.</param>
        void GetBody(string nameConstraint,
            ref DegreesOfFreedomGlobal degreesOfFreedom,
            ref string nameCoordinateSystem);

        /// <summary>
        /// This function defines a Body constraint.
        /// If the specified name is not used for a constraint, a new constraint is defined using the specified name.
        /// If the specified name is already used for another Body constraint, the definition of that constraint is modified.
        /// If the specified name is already used for some constraint that is not a Body constraint, an error is returned.
        /// </summary>
        /// <param name="nameConstraint">The name of a constraint.</param>
        /// <param name="degreesOfFreedom">Indicates which joint degrees of freedom are included in the constraint.</param>
        /// <param name="nameCoordinateSystem">The name of the coordinate system in which the constraint is defined.</param>
        void SetBody(string nameConstraint,
            DegreesOfFreedomGlobal degreesOfFreedom,
            string nameCoordinateSystem);

        // ===

        /// <summary>
        /// The function returns the definition for the specified Equal constraint.
        /// </summary>
        /// <param name="nameConstraint">The name of an existing constraint.</param>
        /// <param name="degreesOfFreedom">Indicates which joint degrees of freedom are included in the constraint.</param>
        /// <param name="nameCoordinateSystem">The name of the coordinate system in which the constraint is defined.</param>
        void GetEqual(string nameConstraint,
            ref DegreesOfFreedomGlobal degreesOfFreedom,
            ref string nameCoordinateSystem);

        /// <summary>
        /// This function defines an Equal constraint.
        /// If the specified name is not used for a constraint, a new constraint is defined using the specified name.
        /// If the specified name is already used for another Equal constraint, the definition of that constraint is modified.
        /// If the specified name is already used for some constraint that is not an Equal constraint, an error is returned.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="nameConstraint">The name of a constraint.</param>
        /// <param name="degreesOfFreedom">Indicates which joint degrees of freedom are included in the constraint.</param>
        /// <param name="nameCoordinateSystem">The name of the coordinate system in which the constraint is defined.</param>
        void SetEqual(string nameConstraint,
            DegreesOfFreedomGlobal degreesOfFreedom,
            string nameCoordinateSystem);

        // ===

        /// <summary>
        /// The function returns the definition for the specified Line constraint.
        /// </summary>
        /// <param name="nameConstraint">The name of an existing constraint.</param>
        /// <param name="degreesOfFreedom">Indicates which joint degrees of freedom are included in the constraint.</param>
        /// <param name="nameCoordinateSystem">The name of the coordinate system in which the constraint is defined.</param>
        void GetLine(string nameConstraint,
            ref DegreesOfFreedomGlobal degreesOfFreedom,
            ref string nameCoordinateSystem);

        /// <summary>
        /// This function defines a Line constraint.
        /// If the specified name is not used for a constraint, a new constraint is defined using the specified name.
        /// If the specified name is already used for another Line constraint, the definition of that constraint is modified.
        /// If the specified name is already used for some constraint that is not a Line constraint, an error is returned.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="nameConstraint">The name of a constraint.</param>
        /// <param name="degreesOfFreedom">Indicates which joint degrees of freedom are included in the constraint.</param>
        /// <param name="nameCoordinateSystem">The name of the coordinate system in which the constraint is defined.</param>
        void SetLine(string nameConstraint,
            DegreesOfFreedomGlobal degreesOfFreedom,
            string nameCoordinateSystem);

        // ===

        /// <summary>
        /// The function returns the definition for the specified Local constraint.
        /// </summary>
        /// <param name="nameConstraint">The name of an existing constraint.</param>
        /// <param name="degreesOfFreedom">Indicates which joint degrees of freedom are included in the constraint.</param>
        void GetLocal(string nameConstraint,
            ref DegreesOfFreedomGlobal degreesOfFreedom);

        /// <summary>
        /// This function defines a Local constraint.
        /// If the specified name is not used for a constraint, a new constraint is defined using the specified name.
        /// If the specified name is already used for another Local constraint, the definition of that constraint is modified.
        /// If the specified name is already used for some constraint that is not a Local constraint, an error is returned.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="nameConstraint">The name of a constraint.</param>
        /// <param name="degreesOfFreedom">Indicates which joint degrees of freedom are included in the constraint.</param>
        void SetLocal(string nameConstraint,
            DegreesOfFreedomGlobal degreesOfFreedom);

        // ===

        /// <summary>
        /// The function returns the definition for the specified Plate constraint.
        /// </summary>
        /// <param name="nameConstraint">The name of an existing constraint.</param>
        /// <param name="axis">Specifies the axis in the specified coordinate system that is perpendicular to the plane of the constraint.</param>
        /// <param name="nameCoordinateSystem">The name of the coordinate system in which the constraint is defined.</param>
        void GetPlate(string nameConstraint,
            ref eConstraintAxis axis,
            ref string nameCoordinateSystem);

        /// <summary>
        /// This function defines a Plate constraint.
        /// If the specified name is not used for a constraint, a new constraint is defined using the specified name.
        /// If the specified name is already used for another Plate constraint, the definition of that constraint is modified.
        /// If the specified name is already used for some constraint that is not a Plate constraint, an error is returned.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="nameConstraint">The name of a constraint.</param>
        /// <param name="axis">Specifies the axis in the specified coordinate system that is perpendicular to the plane of the constraint.</param>
        /// <param name="nameCoordinateSystem">The name of the coordinate system in which the constraint is defined.</param>
        void SetPlate(string nameConstraint,
            eConstraintAxis axis = eConstraintAxis.AutoAxis,
            string nameCoordinateSystem = CoordinateSystems.Global);

        // ===

        /// <summary>
        /// The function returns the definition for the specified Rod constraint.
        /// </summary>
        /// <param name="nameConstraint">The name of an existing constraint.</param>
        /// <param name="axis">Specifies the axis in the specified coordinate system that is perpendicular to the plane of the constraint.</param>
        /// <param name="nameCoordinateSystem">The name of the coordinate system in which the constraint is defined.</param>
        void GetRod(string nameConstraint,
            ref eConstraintAxis axis,
            ref string nameCoordinateSystem);

        /// <summary>
        /// This function defines a Rod constraint.
        /// If the specified name is not used for a constraint, a new constraint is defined using the specified name.
        /// If the specified name is already used for another Rod constraint, the definition of that constraint is modified.
        /// If the specified name is already used for some constraint that is not a Rod constraint, an error is returned.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="nameConstraint">The name of a constraint.</param>
        /// <param name="axis">Specifies the axis in the specified coordinate system that is perpendicular to the plane of the constraint.</param>
        /// <param name="nameCoordinateSystem">The name of the coordinate system in which the constraint is defined.</param>
        void SetRod(string nameConstraint,
            eConstraintAxis axis = eConstraintAxis.AutoAxis,
            string nameCoordinateSystem = CoordinateSystems.Global);

        // ===

        /// <summary>
        /// The function returns the definition for the specified Weld constraint.
        /// </summary>
        /// <param name="nameConstraint">The name of a constraint.</param>
        /// <param name="degreesOfFreedom">Indicates which joint degrees of freedom are included in the constraint.</param>
        /// <param name="tolerance">Joints within this distance of each other are constrained together.</param>
        /// <param name="nameCoordinateSystem">The name of the coordinate system in which the constraint is defined.</param>
        void GetWeld(string nameConstraint,
            ref DegreesOfFreedomGlobal degreesOfFreedom,
            ref double tolerance,
            ref string nameCoordinateSystem);

        /// <summary>
        /// This function defines a Weld constraint.
        /// If the specified name is not used for a constraint, a new constraint is defined using the specified name.
        /// If the specified name is already used for another Weld constraint, the definition of that constraint is modified.
        /// If the specified name is already used for some constraint that is not a Weld constraint, an error is returned.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="nameConstraint">The name of an existing constraint.</param>
        /// <param name="degreesOfFreedom">Indicates which joint degrees of freedom are included in the constraint.</param>
        /// <param name="tolerance">Joints within this distance of each other are constrained together.</param>
        /// <param name="nameCoordinateSystem">The name of the coordinate system in which the constraint is defined.</param>
        void SetWeld(string nameConstraint,
            DegreesOfFreedomGlobal degreesOfFreedom,
            double tolerance,
            string nameCoordinateSystem);
#endif
        #endregion
    }
}