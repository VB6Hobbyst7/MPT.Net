// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="IAutoWind.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wind;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern
{
    /// <summary>
    /// Implements the auto wind load pattern in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.IAutoLoad" />
    public interface IAutoWind : IAutoLoad
    {
        /// <summary>
        /// Auto wind load pattern according to user definitions.
        /// </summary>
        /// <value>The user.</value>
        User User { get; }

        /// <summary>
        /// Auto wind load pattern.
        /// </summary>
        /// <value>The automatic wind load.</value>
        AutoWindLoad AutoWindLoad { get; }

        /// <summary>
        /// Sets the auto wind load code used by the class.
        /// </summary>
        /// <param name="autoWindLoad">The auto wind load for the class to use.</param>
        void SetAutoCode(AutoWindLoad autoWindLoad);

#if BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_CSiBridgev16 || BUILD_CSiBridgev17
        /// <summary>
        /// This function retrieves exposure parameters for auto wind loads determined from extents of rigid diaphragms.
        /// This function does not apply for User-type auto wind loads.
        /// </summary>
        /// <param name="patternName">The name of an existing Wind-type load pattern that has an auto wind load assigned.</param>
        /// <param name="diaphragmNames">This is an array that includes the names of the diaphragms that have eccentricity overrides.</param>
        /// <param name="xCoordinates">This is an array that includes the global X-coordinate of the point where the wind force load is applied to the diaphragm. [L]</param>
        /// <param name="yCoordinates">This is an array that includes the global Y-coordinate of the point where the wind force load is applied to the diaphragm. [L]</param>
        /// <param name="diaphragmWidth">This is an array that includes the exposure width for the wind load applied to the specified diaphragm. [L]</param>
        /// <param name="diaphragmHeight">This is an array that includes the exposure height for the wind load applied to the specified diaphragm. [L]</param>
        void GetExposure(string patternName,
            ref string[] diaphragmNames,
            ref double[] xCoordinates,
            ref double[] yCoordinates,
            ref double[] diaphragmWidth,
            ref double[] diaphragmHeight);

        /// <summary>
        /// This function assigns exposure parameters for auto wind loads determined from extents of rigid diaphragms.
        /// This function does not apply for User-type auto wind loads.
        /// </summary>
        /// <param name="patternName">The name of an existing Wind-type load pattern that has an auto wind load assigned.</param>
        /// <param name="diaphragmName">The name of an existing special rigid diaphragm constraint, that is, a diaphragm constraint with the following features:
        /// 1. The constraint type is CONSTRAINT_DIAPHRAGM = 2.
        /// 2. The constraint coordinate system is Global.
        /// 3. The constraint axis is Z.</param>
        /// <param name="xCoordinate">The global X-coordinate of the point where the wind force is applied. [L]</param>
        /// <param name="yCoordinate">The global Y-coordinate of the point where the wind force is applied. [L]</param>
        /// <param name="diaphragmWidth">The exposure width for the wind load applied to the specified diaphragm. [L]</param>
        /// <param name="diaphragmHeight">The exposure height for the wind load applied to the specified diaphragm. [L]</param>
        void SetExposure(string patternName,
            string diaphragmName,
            double xCoordinate,
            double yCoordinate,
            double diaphragmWidth,
            double diaphragmHeight);
#else
        /// <summary>
        /// This function retrieves exposure parameters for auto wind loads determined from extents of rigid diaphragms.
        /// This function does not apply for User-type auto wind loads.
        /// </summary>
        /// <param name="patternName">The name of an existing Wind-type load pattern that has an auto wind load assigned.</param>
        /// <param name="diaphragmNames">This is an array that includes the names of the diaphragms that have eccentricity overrides.</param>
        /// <param name="xCoordinates">This is an array that includes the global X-coordinate of the point where the wind force load is applied to the diaphragm. [L]</param>
        /// <param name="yCoordinates">This is an array that includes the global Y-coordinate of the point where the wind force load is applied to the diaphragm. [L]</param>
        /// <param name="diaphragmWidth">This is an array that includes the exposure width for the wind load applied to the specified diaphragm. [L]</param>
        /// <param name="diaphragmDepth">This is an array that includes the exposure depth for the wind load applied to the specified diaphragm. [L]</param>
        /// <param name="diaphragmHeight">This is an array that includes the exposure height for the wind load applied to the specified diaphragm. [L]</param>
        void GetExposure(string patternName,
            ref string[] diaphragmNames,
            ref double[] xCoordinates,
            ref double[] yCoordinates,
            ref double[] diaphragmWidth,
            ref double[] diaphragmDepth,
            ref double[] diaphragmHeight);

        /// <summary>
        /// This function assigns exposure parameters for auto wind loads determined from extents of rigid diaphragms.
        /// This function does not apply for User-type auto wind loads.
        /// </summary>
        /// <param name="patternName">The name of an existing Wind-type load pattern that has an auto wind load assigned.</param>
        /// <param name="diaphragmName">The name of an existing special rigid diaphragm constraint, that is, a diaphragm constraint with the following features:
        /// 1. The constraint type is CONSTRAINT_DIAPHRAGM = 2.
        /// 2. The constraint coordinate system is Global.
        /// 3. The constraint axis is Z.</param>
        /// <param name="xCoordinate">The global X-coordinate of the point where the wind force is applied. [L]</param>
        /// <param name="yCoordinate">The global Y-coordinate of the point where the wind force is applied. [L]</param>
        /// <param name="diaphragmWidth">The exposure width for the wind load applied to the specified diaphragm. [L]</param>
        /// <param name="diaphragmDepth">The exposure depth for the wind load applied to the specified diaphragm. [L]</param>
        /// <param name="diaphragmHeight">The exposure height for the wind load applied to the specified diaphragm. [L]</param>
        void SetExposure(string patternName,
            string diaphragmName,
            double xCoordinate,
            double yCoordinate,
            double diaphragmWidth,
            double diaphragmDepth,
            double diaphragmHeight);
#endif
    }
}
#endif