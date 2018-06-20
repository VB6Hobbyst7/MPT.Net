// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="IAutoLoad.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern
{
    /// <summary>
    /// Implements a design interface for all auto load types.
    /// </summary>
    public interface IAutoLoad
    {
        #region Methods: Public
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function retrieves the code name used for auto parameters in auto load patterns.
        /// An error is returned if the specified load pattern is not an auto load pattern.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="name">The name of an existing auto load pattern.</param>
        /// <param name="codeName">This is either blank or the name code used for the auto parameters.
        /// Blank means no auto load is specified for the auto load pattern.</param>
        void GetAutoCode(string name,
                            ref string codeName);

        /// <summary>
        /// Sets the auto load code used by the class.
        /// </summary>
        /// <param name="autoLoad">The auto load for the class to use.</param>
        void SetAutoCode(AutoLoad autoLoad);

        /// <summary>
        /// This function sets the auto loading type for the specified load pattern to None.
        /// </summary>
        /// <param name="name">The name of an existing auto load pattern.</param>
        void SetNone(string name);
#endif
        #endregion
    }
}
