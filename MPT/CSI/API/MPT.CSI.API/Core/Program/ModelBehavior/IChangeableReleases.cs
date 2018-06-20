// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-20-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 06-20-2017
// ***********************************************************************
// <copyright file="IChangeableReleases.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Helpers;

namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Object can have releases set.
    /// </summary>
    public interface IChangeableReleases
    {
        /// <summary>
        /// This function defines a named frame end release.
        /// </summary>
        /// <param name="name">The name of a new or existing frame end release.</param>
        /// <param name="iEndRelease">Booleans indicating the I-End releases.</param>
        /// <param name="jEndRelease">Booleans indicating the J-End releases.</param>
        /// <param name="iEndFixity">Values indicating the I-End partial fixity springs.
        /// The value will be inactive if there is not a released specified for the corresponding degree of freedom.</param>
        /// <param name="jEndFixity">Values indicating the J-End partial fixity springs.
        /// The value will be inactive if there is not a released specified for the corresponding degree of freedom.</param>
        void SetReleases(string name,
            DegreesOfFreedomLocal iEndRelease,
            DegreesOfFreedomLocal jEndRelease,
            Fixity iEndFixity,
            Fixity jEndFixity);
    }
}
