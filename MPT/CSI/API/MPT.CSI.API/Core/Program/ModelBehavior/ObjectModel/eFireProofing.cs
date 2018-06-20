// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-01-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eFireProofing.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Fireproofing types available in the application.
    /// </summary>
    public enum eFireProofing
    {
        /// <summary>
        /// Sprayed on - program calculate section perimeter.
        /// </summary>
        SprayedOnProgramPerimeterCalc = 1,


        /// <summary>
        /// Sprayed on - user provides section perimeter.
        /// </summary>
        SprayedOnUserPerimeterDefine = 2,

        /// <summary>
        /// Concrete encased.
        /// </summary>
        ConcreteEncased = 3
    }
}
#endif