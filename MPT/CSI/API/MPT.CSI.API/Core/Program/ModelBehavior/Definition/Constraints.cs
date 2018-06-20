// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-11-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="Constraints.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_SAP2000v16
using CSiProgram = SAP2000v16;
#elif BUILD_SAP2000v17
using CSiProgram = SAP2000v17;
#elif BUILD_SAP2000v18
using CSiProgram = SAP2000v18;
#elif BUILD_SAP2000v19
using CSiProgram = SAP2000v19;
#elif BUILD_SAP2000v20
using CSiProgram = SAP2000v20;
#elif BUILD_CSiBridgev16
using CSiProgram = CSiBridge16;
#elif BUILD_CSiBridgev17
using CSiProgram = CSiBridge17;
#elif BUILD_CSiBridgev18
using CSiProgram = CSiBridge18;
#elif BUILD_CSiBridgev19
using CSiProgram = CSiBridge19;
#elif BUILD_CSiBridgev20
using CSiProgram = CSiBridge20;
#elif BUILD_ETABS2013
using CSiProgram = ETABS2013;
#elif BUILD_ETABS2015
using CSiProgram = ETABS2015;
#elif BUILD_ETABS2016
using CSiProgram = ETABS2016;
#elif BUILD_ETABS2017
using CSiProgram = ETABSv17;
#endif
using MPT.Enums;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Represents the constraints in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.IConstraints" />
    public class Constraints : CSiApiBase, IConstraints
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="Constraints" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public Constraints(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Public
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Changes the name of the specified constraint.
        /// </summary>
        /// <param name="nameConstraint">The existing name of a defined constraint.</param>
        /// <param name="newName">The new name for the constraint.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ChangeName(string nameConstraint,
            string newName)
        {
            _callCode = _sapModel.ConstraintDef.ChangeName(nameConstraint, newName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Returns the total number of defined constraints without regard for type.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int Count()
        {
            return _sapModel.ConstraintDef.Count();
        }

        /// <summary>
        /// Returns the number of defined constraints of the specified type.
        /// </summary>
        /// <param name="constraintType">Type of constraint to return a count for.</param>
        /// <returns>System.Int32.</returns>
        public int Count(eConstraintType constraintType)
        {
            return _sapModel.ConstraintDef.Count(EnumLibrary.Convert<eConstraintType, CSiProgram.eConstraintType>(constraintType));
        }
#endif

        /// <summary>
        /// The function deletes the specified constraint.
        /// All constraint assignments for that constraint are also deleted.
        /// </summary>
        /// <param name="nameConstraint">The name of an existing constraint.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void Delete(string nameConstraint)
        {
            _callCode = _sapModel.ConstraintDef.Delete(nameConstraint);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // === Get

        /// <summary>
        /// This function retrieves the names of all defined joint constraints.
        /// </summary>
        /// <param name="namesConstraint">Joint constraint names retrieved by the program.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetNameList(out string[] namesConstraint)
        {
            namesConstraint = new string[0];
            _callCode = _sapModel.ConstraintDef.GetNameList(ref _numberOfItems, ref namesConstraint);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// The function returns the constraint type for the specified constraint.
        /// </summary>
        /// <param name="nameConstraint">The name of an existing constraint.</param>
        /// <param name="constraintType">Constraint type assigned to the named cosntraint.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetConstraintType(string nameConstraint,
            ref eConstraintType constraintType)
        {
            CSiProgram.eConstraintType csiConstraintType = CSiProgram.eConstraintType.Beam;
            _callCode = _sapModel.ConstraintDef.GetConstraintType(nameConstraint, ref csiConstraintType);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            constraintType = (eConstraintType) csiConstraintType;
        }

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
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetSpecialRigidDiaphragmList(ref string[] diaphragms)
        {
            _callCode = _sapModel.ConstraintDef.GetSpecialRigidDiaphragmList(ref _numberOfItems, ref diaphragms);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif




        // === Get/Set
        /// <summary>
        /// The function returns the definition for the specified Diaphragm constraint.
        /// </summary>
        /// <param name="nameConstraint">The name of a constraint.</param>
        /// <param name="axis">Specifies the axis in the specified coordinate system that is perpendicular to the plane of the constraint.</param>
        /// <param name="nameCoordinateSystem">The name of the coordinate system in which the constraint is defined.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetDiaphragm(string nameConstraint,
            ref eConstraintAxis axis,
            ref string nameCoordinateSystem)
        {
            CSiProgram.eConstraintAxis csiAxis = CSiProgram.eConstraintAxis.AutoAxis;

            _callCode = _sapModel.ConstraintDef.GetDiaphragm(nameConstraint, ref csiAxis, ref nameCoordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            axis = EnumLibrary.Convert(csiAxis, axis);
        }

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
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetDiaphragm(string nameConstraint,
            eConstraintAxis axis = eConstraintAxis.AutoAxis,
            string nameCoordinateSystem = CoordinateSystems.Global)
        {
            _callCode = _sapModel.ConstraintDef.SetDiaphragm(nameConstraint, 
                            EnumLibrary.Convert<eConstraintAxis, CSiProgram.eConstraintAxis>(axis), 
                            nameCoordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// The function returns the definition for the specified Beam constraint.
        /// </summary>
        /// <param name="nameConstraint">The name of an existing constraint.</param>
        /// <param name="axis">Specifies the axis in the specified coordinate system that is parallel to the axis of the constraint.
        /// If AutoAxis is specified, the axis of the constraint is automatically determined from the joints assigned to the constraint.</param>
        /// <param name="nameCoordinateSystem">The name of the coordinate system in which the constraint is defined.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetBeam(string nameConstraint, 
            ref eConstraintAxis axis,
            ref string nameCoordinateSystem)
        {
            CSiProgram.eConstraintAxis csiAxis = CSiProgram.eConstraintAxis.AutoAxis;
            _callCode = _sapModel.ConstraintDef.GetBeam(nameConstraint, ref csiAxis, ref nameCoordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            axis = (eConstraintAxis)csiAxis;
        }

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
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetBeam(string nameConstraint,
            eConstraintAxis axis,
            string nameCoordinateSystem)
        {
            // TODO: Handle this: If the specified name is already used for some constraint that is not a [xxxx] constraint, an error is returned.
            _callCode = _sapModel.ConstraintDef.SetBeam(nameConstraint, (CSiProgram.eConstraintAxis)axis, nameCoordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===

        /// <summary>
        /// The function returns the definition for the specified Body constraint
        /// </summary>
        /// <param name="nameConstraint">The name of an existing constraint.</param>
        /// <param name="degreesOfFreedom">Indicates which joint degrees of freedom are included in the constraint.</param>
        /// <param name="nameCoordinateSystem">The name of the coordinate system in which the constraint is defined.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetBody(string nameConstraint, 
            ref DegreesOfFreedomGlobal degreesOfFreedom,
            ref string nameCoordinateSystem)
        {
            bool[] csiDegreesOfFreedom = new bool[0];

            _callCode = _sapModel.ConstraintDef.GetBody(nameConstraint, ref csiDegreesOfFreedom, ref nameCoordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            degreesOfFreedom.FromArray(csiDegreesOfFreedom);
        }

        /// <summary>
        /// This function defines a Body constraint.
        /// If the specified name is not used for a constraint, a new constraint is defined using the specified name.
        /// If the specified name is already used for another Body constraint, the definition of that constraint is modified.
        /// If the specified name is already used for some constraint that is not a Body constraint, an error is returned.
        /// </summary>
        /// <param name="nameConstraint">The name of a constraint.</param>
        /// <param name="degreesOfFreedom">Indicates which joint degrees of freedom are included in the constraint.</param>
        /// <param name="nameCoordinateSystem">The name of the coordinate system in which the constraint is defined.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetBody(string nameConstraint,
            DegreesOfFreedomGlobal degreesOfFreedom,
            string nameCoordinateSystem)
        {
            // TODO: Handle this: If the specified name is already used for some constraint that is not a [xxxx] constraint, an error is returned.
            bool[] csiDegreesOfFreedom = degreesOfFreedom.ToArray();

            _callCode = _sapModel.ConstraintDef.SetBody(nameConstraint, ref csiDegreesOfFreedom, nameCoordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===

        /// <summary>
        /// The function returns the definition for the specified Equal constraint.
        /// </summary>
        /// <param name="nameConstraint">The name of an existing constraint.</param>
        /// <param name="degreesOfFreedom">Indicates which joint degrees of freedom are included in the constraint.</param>
        /// <param name="nameCoordinateSystem">The name of the coordinate system in which the constraint is defined.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetEqual(string nameConstraint,
            ref DegreesOfFreedomGlobal degreesOfFreedom,
            ref string nameCoordinateSystem)
        {
            bool[] csiDegreesOfFreedom = new bool[0];

            _callCode = _sapModel.ConstraintDef.GetEqual(nameConstraint, ref csiDegreesOfFreedom, ref nameCoordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            degreesOfFreedom.FromArray(csiDegreesOfFreedom);
        }

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
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetEqual(string nameConstraint,
            DegreesOfFreedomGlobal degreesOfFreedom,
            string nameCoordinateSystem)
        {
            bool[] csiDegreesOfFreedom = degreesOfFreedom.ToArray();

            _callCode = _sapModel.ConstraintDef.SetEqual(nameConstraint, ref csiDegreesOfFreedom, nameCoordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===

        /// <summary>
        /// The function returns the definition for the specified Line constraint.
        /// </summary>
        /// <param name="nameConstraint">The name of an existing constraint.</param>
        /// <param name="degreesOfFreedom">Indicates which joint degrees of freedom are included in the constraint.</param>
        /// <param name="nameCoordinateSystem">The name of the coordinate system in which the constraint is defined.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLine(string nameConstraint,
            ref DegreesOfFreedomGlobal degreesOfFreedom,
            ref string nameCoordinateSystem)
        {
            bool[] csiDegreesOfFreedom = new bool[0];

            _callCode = _sapModel.ConstraintDef.GetLine(nameConstraint, ref csiDegreesOfFreedom, ref nameCoordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            degreesOfFreedom.FromArray(csiDegreesOfFreedom);
        }

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
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLine(string nameConstraint,
            DegreesOfFreedomGlobal degreesOfFreedom,
            string nameCoordinateSystem)
        {
            bool[] csiDegreesOfFreedom = degreesOfFreedom.ToArray();

            _callCode = _sapModel.ConstraintDef.SetLine(nameConstraint, ref csiDegreesOfFreedom, nameCoordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===

        /// <summary>
        /// The function returns the definition for the specified Local constraint.
        /// </summary>
        /// <param name="nameConstraint">The name of an existing constraint.</param>
        /// <param name="degreesOfFreedom">Indicates which joint degrees of freedom are included in the constraint.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLocal(string nameConstraint,
            ref DegreesOfFreedomGlobal degreesOfFreedom)
        {
            bool[] csiDegreesOfFreedom = new bool[0];

            _callCode = _sapModel.ConstraintDef.GetLocal(nameConstraint, ref csiDegreesOfFreedom);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            degreesOfFreedom.FromArray(csiDegreesOfFreedom);
        }

        /// <summary>
        /// This function defines a Local constraint.
        /// If the specified name is not used for a constraint, a new constraint is defined using the specified name.
        /// If the specified name is already used for another Local constraint, the definition of that constraint is modified.
        /// If the specified name is already used for some constraint that is not a Local constraint, an error is returned.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="nameConstraint">The name of a constraint.</param>
        /// <param name="degreesOfFreedom">Indicates which joint degrees of freedom are included in the constraint.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLocal(string nameConstraint,
            DegreesOfFreedomGlobal degreesOfFreedom)
        {
            bool[] csiDegreesOfFreedom = degreesOfFreedom.ToArray();

            _callCode = _sapModel.ConstraintDef.SetLocal(nameConstraint, ref csiDegreesOfFreedom);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===

        /// <summary>
        /// The function returns the definition for the specified Plate constraint.
        /// </summary>
        /// <param name="nameConstraint">The name of an existing constraint.</param>
        /// <param name="axis">Specifies the axis in the specified coordinate system that is perpendicular to the plane of the constraint.</param>
        /// <param name="nameCoordinateSystem">The name of the coordinate system in which the constraint is defined.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetPlate(string nameConstraint,
            ref eConstraintAxis axis,
            ref string nameCoordinateSystem)
        {
            CSiProgram.eConstraintAxis csiAxis = CSiProgram.eConstraintAxis.AutoAxis;

            _callCode = _sapModel.ConstraintDef.GetPlate(nameConstraint, ref csiAxis, ref nameCoordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            axis = EnumLibrary.Convert(csiAxis, axis);
        }

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
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetPlate(string nameConstraint,
            eConstraintAxis axis = eConstraintAxis.AutoAxis,
            string nameCoordinateSystem = CoordinateSystems.Global)
        {
            _callCode = _sapModel.ConstraintDef.SetPlate(nameConstraint, 
                EnumLibrary.Convert<eConstraintAxis, CSiProgram.eConstraintAxis>(axis), 
                nameCoordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===

        /// <summary>
        /// The function returns the definition for the specified Rod constraint.
        /// </summary>
        /// <param name="nameConstraint">The name of an existing constraint.</param>
        /// <param name="axis">Specifies the axis in the specified coordinate system that is perpendicular to the plane of the constraint.</param>
        /// <param name="nameCoordinateSystem">The name of the coordinate system in which the constraint is defined.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetRod(string nameConstraint,
            ref eConstraintAxis axis,
            ref string nameCoordinateSystem)
        {
            CSiProgram.eConstraintAxis csiAxis = CSiProgram.eConstraintAxis.AutoAxis;

            _callCode = _sapModel.ConstraintDef.GetRod(nameConstraint, ref csiAxis, ref nameCoordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            axis = EnumLibrary.Convert(csiAxis, axis);
        }

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
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetRod(string nameConstraint,
            eConstraintAxis axis = eConstraintAxis.AutoAxis,
            string nameCoordinateSystem = CoordinateSystems.Global)
        {
            _callCode = _sapModel.ConstraintDef.SetRod(nameConstraint, 
                            EnumLibrary.Convert<eConstraintAxis, CSiProgram.eConstraintAxis>(axis), 
                            nameCoordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===

        /// <summary>
        /// The function returns the definition for the specified Weld constraint.
        /// </summary>
        /// <param name="nameConstraint">The name of a constraint.</param>
        /// <param name="degreesOfFreedom">Indicates which joint degrees of freedom are included in the constraint.</param>
        /// <param name="tolerance">Joints within this distance of each other are constrained together.</param>
        /// <param name="nameCoordinateSystem">The name of the coordinate system in which the constraint is defined.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetWeld(string nameConstraint,
            ref DegreesOfFreedomGlobal degreesOfFreedom,
            ref double tolerance,
            ref string nameCoordinateSystem)
        {
            bool[] csiDegreesOfFreedom = new bool[0];

            _callCode = _sapModel.ConstraintDef.GetWeld(nameConstraint, ref csiDegreesOfFreedom, ref tolerance, ref nameCoordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            degreesOfFreedom.FromArray(csiDegreesOfFreedom);
        }

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
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetWeld(string nameConstraint,
            DegreesOfFreedomGlobal degreesOfFreedom,
            double tolerance,
            string nameCoordinateSystem)
        {
            bool[] csiDegreesOfFreedom = degreesOfFreedom.ToArray();

            _callCode = _sapModel.ConstraintDef.SetWeld(nameConstraint, ref csiDegreesOfFreedom, tolerance, nameCoordinateSystem);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // ===
#endif
        #endregion
    }
}
