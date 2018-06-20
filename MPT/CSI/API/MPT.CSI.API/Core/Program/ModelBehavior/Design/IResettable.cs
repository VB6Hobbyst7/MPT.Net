// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 09-29-2017
//
// Last Modified By : Mark
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="IResettable.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design
{
    /// <summary>
    /// Implements an interface for resetting design settings and results.
    /// </summary>
    public interface IResettable
    {
        /// <summary>
        /// Deletes all frame design results.
        /// </summary>
        void DeleteResults();

        /// <summary>
        /// Resets all frame design overwrites to default values.
        /// </summary>
        void ResetOverwrites();
    }
}
