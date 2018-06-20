// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 10-03-2017
//
// Last Modified By : Mark
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="IDesignSteel.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design
{
    /// <summary>
    /// Implements a design interface for all steel frame elements.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.IDesignMetal" />
    public interface IDesignSteel : IDesignMetal
    {
        #region Properties
        /// <summary>
        /// Gets the steel design code.
        /// </summary>
        /// <value>The steel design code.</value>
        DesignSteel Code { get;}
        #endregion

        #region Methods
        /// <summary>
        /// Sets the design code used by the class.
        /// </summary>
        /// <param name="code">The design code for the class to use.</param>
        void SetCode(DesignSteel code);
        #endregion
    }
}