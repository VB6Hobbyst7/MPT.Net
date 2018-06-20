// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-06-2017
// ***********************************************************************
// <copyright file="IAbstraction.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Implements abstraction objects.
    /// </summary>
    public interface IAbstraction
    {
        /// <summary>
        /// Gets the diaphragm.
        /// </summary>
        /// <value>The diaphragm.</value>
        Diaphragm Diaphragm { get; }

        /// <summary>
        /// Gets the story.
        /// </summary>
        /// <value>The story.</value>
        Story Story { get; }

        /// <summary>
        /// Gets the tower.
        /// </summary>
        /// <value>The tower.</value>
        Tower Tower { get; }

        /// <summary>
        /// Gets the pier label.
        /// </summary>
        /// <value>The pier label.</value>
        PierLabel PierLabel { get; }

        /// <summary>
        /// Gets the spandrel label.
        /// </summary>
        /// <value>The spandrel label.</value>
        SpandrelLabel SpandrelLabel { get; }
    }
}
#endif