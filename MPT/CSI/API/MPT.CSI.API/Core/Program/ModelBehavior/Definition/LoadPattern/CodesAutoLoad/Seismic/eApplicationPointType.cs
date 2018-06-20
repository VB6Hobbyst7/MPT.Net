// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-10-2017
// ***********************************************************************
// <copyright file="eApplicationPointType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic
{
    /// <summary>
    /// Application point type for the user load.
    /// </summary>
    public enum eApplicationPointType
    {
        /// <summary>
        /// User specified application point.
        /// </summary>
        UserSpecifiedApplicationPoint = 1,

        /// <summary>
        /// At center of mass with optional additional eccentricity.
        /// </summary>
        CenterOfMassWithEccentricity = 2
    }
}
#endif
