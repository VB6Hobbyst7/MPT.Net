// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-13-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="ePlaneType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Plane types available in the application.
    /// </summary>
    public enum ePlaneType
    {
        /// <summary>
        /// Plane-stress.
        /// </summary>
        PlaneStress = 1,

        /// <summary>
        /// Plane-strain.
        /// </summary>
        PlaneStrain = 2
    }
}
#endif
