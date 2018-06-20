// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-05-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-05-2017
// ***********************************************************************
// <copyright file="eSpringStiffnessDerivation.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Methods in the application by which spring stiffness is derived.
    /// </summary>
    public enum eSpringStiffnessDerivation
    {
        /// <summary>
        /// Based on the user specified or link properties.
        /// </summary>
        UserSpecifiedOrLinkProperties = 1,

        /// <summary>
        /// Based on soil profile and footing dimensions.
        /// </summary>
        SoilProfileAndFootingDimensions = 2
    }
}
#endif