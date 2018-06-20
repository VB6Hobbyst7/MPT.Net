// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-03-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="IPointSpring.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2016 || BUILD_ETABS2017
using MPT.CSI.API.Core.Helpers;
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Object has a gettable/settable point spring property.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    public interface IPointSpring : IDeletable, IChangeableName, IListableNames
    {
        /// <summary>
        /// Retrieves the single joint links for a named point spring property.
        /// </summary>
        /// <param name="name">The name of an existing point spring property. 
        /// This spring property must be of type <see cref="eSpringStiffnessDerivation.UserSpecifiedOrLinkProperties"/>.</param>
        /// <param name="linkNames">The link names.</param>
        /// <param name="linkAxialDirections">The axial direction of their respective link properties.</param>
        /// <param name="linkAngles">The axis-2 angles of their respective link properties. [deg] .</param>
        void GetLinks(string name,
                ref string[] linkNames,
                ref eAxialDirections[] linkAxialDirections,
                ref double[] linkAngles);

        /// <summary>
        /// Sets single joint links for a named point spring property.
        /// </summary>
        /// <param name="name">The name of an existing point spring property. 
        /// This spring property must be of type <see cref="eSpringStiffnessDerivation.UserSpecifiedOrLinkProperties"/>.</param>
        /// <param name="linkNames">The link names.</param>
        /// <param name="linkAxialDirections">The axial direction of their respective link properties.</param>
        /// <param name="linkAngles">The axis-2 angles of their respective link properties. [deg] .</param>
        void SetLinks(string name,
                ref string[] linkNames,
                ref eAxialDirections[] linkAxialDirections,
                ref double[] linkAngles);

    
        /// <summary>
        /// Retrieves an existing named point spring property.
        /// </summary>
        /// <param name="name">The name of the point spring property.</param>
        /// <param name="stiffnessSource">The source of the spring stiffness derivation.</param>
        /// <param name="effectiveStiffness">The effective stiffness.
        /// This argument only applies when <paramref name="stiffnessSource"/> = <see cref="eSpringStiffnessDerivation.UserSpecifiedOrLinkProperties"/>.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the property is defined.
        /// This argument only applies when <paramref name="stiffnessSource"/> = <see cref="eSpringStiffnessDerivation.UserSpecifiedOrLinkProperties"/>.</param>
        /// <param name="soilProfile">The name of an existing Soil Profile.
        /// This argument only applies when <paramref name="stiffnessSource"/> = <see cref="eSpringStiffnessDerivation.SoilProfileAndFootingDimensions"/>.</param>
        /// <param name="footing">The name of an existing Isolated Column Footing.
        /// This argument only applies when <paramref name="stiffnessSource"/> = <see cref="eSpringStiffnessDerivation.SoilProfileAndFootingDimensions"/>.</param>
        /// <param name="period">The first-mode time period. [sec].
        /// This argument only applies when <paramref name="stiffnessSource"/> = <see cref="eSpringStiffnessDerivation.SoilProfileAndFootingDimensions"/>.</param>
        /// <param name="color">The display color for the property specified as an integer.</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        void GetPointSpringProperties(string name,
            ref eSpringStiffnessDerivation stiffnessSource,
            ref Stiffness effectiveStiffness,
            ref string coordinateSystem,
            ref string soilProfile,
            ref string footing,
            ref double period,
            ref int color,
            ref string notes,
            ref string GUID);

        /// <summary>
        /// Creates a new named point spring property, or modifies an existing named point spring property.
        /// </summary>
        /// <param name="name">The name of the point spring property.</param>
        /// <param name="stiffnessSource">The source of the spring stiffness derivation.</param>
        /// <param name="effectiveStiffness">The effective stiffness.
        /// This argument only applies when <paramref name="stiffnessSource"/> = <see cref="eSpringStiffnessDerivation.UserSpecifiedOrLinkProperties"/>.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the property is defined.
        /// This argument only applies when <paramref name="stiffnessSource"/> = <see cref="eSpringStiffnessDerivation.UserSpecifiedOrLinkProperties"/>.</param>
        /// <param name="soilProfile">The name of an existing Soil Profile.
        /// This argument only applies when <paramref name="stiffnessSource"/> = <see cref="eSpringStiffnessDerivation.SoilProfileAndFootingDimensions"/>.</param>
        /// <param name="footing">The name of an existing Isolated Column Footing.
        /// This argument only applies when <paramref name="stiffnessSource"/> = <see cref="eSpringStiffnessDerivation.SoilProfileAndFootingDimensions"/>.</param>
        /// <param name="period">The first-mode time period. [sec].
        /// This argument only applies when <paramref name="stiffnessSource"/> = <see cref="eSpringStiffnessDerivation.SoilProfileAndFootingDimensions"/>.</param>
        /// <param name="color">The display color for the property specified as an integer.</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        void SetPointSpringProperties(string name,
            eSpringStiffnessDerivation stiffnessSource,
            Stiffness effectiveStiffness,
            string coordinateSystem = CoordinateSystems.Global,
            string soilProfile = "",
            string footing = "",
            double period = 0,
            int color = 0,
            string notes = "",
            string GUID = "");
    }
}
#endif