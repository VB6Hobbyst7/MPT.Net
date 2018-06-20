// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-06-2017
// ***********************************************************************
// <copyright file="IDesigner.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.Design;

namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Implements design of structural members.
    /// </summary>
    public interface IDesigner
    {
        /// <summary>
        /// Gets the design concrete.
        /// </summary>
        /// <value>The design concrete.</value>
        DesignConcrete DesignConcrete { get; }

        /// <summary>
        /// Gets the design steel.
        /// </summary>
        /// <value>The design steel.</value>
        DesignSteel DesignSteel { get; }

#if BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_SAP2000v18 || BUILD_SAP2000v19 || BUILD_SAP2000v20
        /// <summary>
        /// Gets the design aluminum.
        /// </summary>
        /// <value>The design aluminum.</value>
        DesignAluminum DesignAluminum { get; }

        /// <summary>
        /// Gets the design cold formed.
        /// </summary>
        /// <value>The design cold formed.</value>
        DesignColdFormed DesignColdFormed { get; }
#endif
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Gets the design composite beam.
        /// </summary>
        /// <value>The design composite beam.</value>
        DesignCompositeBeam DesignCompositeBeam { get; }
#endif
#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Gets the design detailing.
        /// </summary>
        /// <value>The design detailing.</value>
        DesignDetailing DesignDetailing { get; }
#endif
    }
}