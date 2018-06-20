// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-03-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="PointSpring.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2016 || BUILD_ETABS2017
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Represents a point spring object in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.IPointSpring" />
    public class PointSpring : CSiApiBase, IPointSpring
    {
#region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="PointSpring"/> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public PointSpring(CSiApiSeed seed) : base(seed)
        {

        }
#endregion

#region Methods: Interface
        /// <summary>
        /// Changes the name of a defined point spring property.
        /// </summary>
        /// <param name="nameExisting">Existing name of a defined point spring property.</param>
        /// <param name="nameNew">New name for the point spring property.</param>
        public void ChangeName(string nameExisting,
            string nameNew)
        {
            _callCode = _sapModel.PropPointSpring.ChangeName(nameExisting, nameNew);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// Deletes the specified point spring property.
        /// </summary>
        /// <param name="name">The name of an existing point spring property.</param>
        public void Delete(string name)
        {
            _callCode = _sapModel.PropPointSpring.Delete(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Retrieves the names of all defined point spring property.
        /// </summary>
        /// <param name="names">The point spring property names retrieved by the program.</param>
        public void GetNameList(out string[] names)
        {
            names = new string[0];
            _callCode = _sapModel.PropPointSpring.GetNameList(ref _numberOfItems, ref names);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Retrieves an existing named point spring property.
        /// </summary>
        /// <param name="name">The name of the point spring property.</param>
        /// <param name="stiffnessSource">The source of the spring stiffness derivation.</param>
        /// <param name="effectiveStiffness">The effective stiffness.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the property is defined.</param>
        /// <param name="soilProfile">The name of an existing Soil Profile.</param>
        /// <param name="footing">The name of an existing Isolated Column Footing.</param>
        /// <param name="period">The first-mode time period. [sec].</param>
        /// <param name="color">The display color for the property specified as an integer.</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        public void GetPointSpringProperties(string name,
            ref eSpringStiffnessDerivation stiffnessSource,
            ref Stiffness effectiveStiffness,
            ref string coordinateSystem,
            ref string soilProfile,
            ref string footing,
            ref double period,
            ref int color,
            ref string notes,
            ref string GUID)
        {
            double[] k = new double[0];
            int csiStiffnessSource = 0;

            _callCode = _sapModel.PropPointSpring.GetPointSpringProp(name,
                            ref csiStiffnessSource,
                            ref k,
                            ref coordinateSystem,
                            ref soilProfile, ref footing, ref period,
                            ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            stiffnessSource = (eSpringStiffnessDerivation) csiStiffnessSource;
            effectiveStiffness.FromArray(k);
        }

        /// <summary>
        /// Creates a new named point spring property, or modifies an existing named point spring property.
        /// </summary>
        /// <param name="name">The name of the point spring property.</param>
        /// <param name="stiffnessSource">The source of the spring stiffness derivation.</param>
        /// <param name="effectiveStiffness">The effective stiffness.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the property is defined.</param>
        /// <param name="soilProfile">The name of an existing Soil Profile.</param>
        /// <param name="footing">The name of an existing Isolated Column Footing.</param>
        /// <param name="period">The first-mode time period. [sec].</param>
        /// <param name="color">The display color for the property specified as an integer.</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        public void SetPointSpringProperties(string name,
            eSpringStiffnessDerivation stiffnessSource,
            Stiffness effectiveStiffness,
            string coordinateSystem = CoordinateSystems.Global,
            string soilProfile = "",
            string footing = "",
            double period = 0,
            int color = 0,
            string notes = "",
            string GUID = "")
        {
            double[] k = effectiveStiffness.ToArray();

            _callCode = _sapModel.PropPointSpring.SetPointSpringProp(name,
                            (int)stiffnessSource,
                            ref k,
                            coordinateSystem,
                            soilProfile, footing, period, 
                            color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Retrieves the single joint links for a named point spring property.
        /// </summary>
        /// <param name="name">The name of an existing point spring property. 
        /// This spring property must be of type <see cref="eSpringStiffnessDerivation.UserSpecifiedOrLinkProperties"/>.</param>
        /// <param name="linkNames">The link names.</param>
        /// <param name="linkAxialDirections">The axial direction of their respective link properties.</param>
        /// <param name="linkAngles">The axis-2 angles of their respective link properties. [deg] .</param>
        public void GetLinks(string name,
                ref string[] linkNames,
                ref eAxialDirections[] linkAxialDirections,
                ref double[] linkAngles)
        {
            int[] csiLinkAxialDirections = new int[0];

            _callCode = _sapModel.PropPointSpring.GetLinks(name, ref _numberOfItems,
                            ref linkNames,
                            ref csiLinkAxialDirections,
                            ref linkAngles);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            linkAxialDirections = new eAxialDirections[_numberOfItems];
            for (int i = 0; i < _numberOfItems; i++)
            {
                linkAxialDirections[i] = (eAxialDirections)csiLinkAxialDirections[i];
            }
        }

        /// <summary>
        /// Sets single joint links for a named point spring property.
        /// </summary>
        /// <param name="name">The name of an existing point spring property. 
        /// This spring property must be of type <see cref="eSpringStiffnessDerivation.UserSpecifiedOrLinkProperties"/>.</param>
        /// <param name="linkNames">The link names.</param>
        /// <param name="linkAxialDirections">The axial direction of their respective link properties.</param>
        /// <param name="linkAngles">The axis-2 angles of their respective link properties. [deg] .</param>
        public void SetLinks(string name,
                ref string[] linkNames,
                ref eAxialDirections[] linkAxialDirections,
                ref double[] linkAngles)
        {
            arraysLengthMatch(nameof(linkNames), linkNames.Length, nameof(linkAxialDirections), linkAxialDirections.Length);
            arraysLengthMatch(nameof(linkNames), linkNames.Length, nameof(linkAngles), linkAngles.Length);

            int[] csiLinkAxialDirections = new int[linkAxialDirections.Length];
            for (int i = 0; i < linkAxialDirections.Length; i++)
            {
                csiLinkAxialDirections[i] = (int) linkAxialDirections[i];
            }

            _callCode = _sapModel.PropPointSpring.SetLinks(name, linkAxialDirections.Length,
                            ref linkNames,
                            ref csiLinkAxialDirections,
                            ref linkAngles);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion
    }
}
#endif