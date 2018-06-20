// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-10-2017
// ***********************************************************************
// <copyright file="IAutoSeismic.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern
{
    /// <summary>
    /// Implements the auto seismic load pattern in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.IAutoLoad" />
    public interface IAutoSeismic : IAutoLoad
    {
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Auto seismic load pattern according to user definitions.
        /// </summary>
        /// <value>The user.</value>
        User User { get; }
#endif
        /// <summary>
        /// Auto seismic load pattern.
        /// </summary>
        /// <value>The automatic seismic load.</value>
        AutoSeismicLoad AutoSeismicLoad { get; }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Sets the auto seismic load code used by the class.
        /// </summary>
        /// <param name="autoSeismicLoad">The auto seismic load for the class to use.</param>
        void SetAutoCode(AutoSeismicLoad autoSeismicLoad);

        /// <summary>
        /// This function retrieves diaphragm eccentricity overrides for auto seismic loads.
        /// This function does not apply for User Load type auto seismic loads.
        /// </summary>
        /// <param name="patternName">The name of an existing Quake-type load pattern that has an auto seismic load assigned.</param>
        /// <param name="diaphragmNames">This is an array that includes the names of the diaphragms which have eccentricity overrides.</param>
        /// <param name="eccentricities">This is an array that includes the eccentricity applied to each diaphragm. [L]</param>
        void GetDiaphragmEccentricityOverride(string patternName,
            ref string[] diaphragmNames,
            ref double[] eccentricities);

        /// <summary>
        /// This function assigns diaphragm eccentricity overrides for auto seismic loads.
        /// This function does not apply for User Load type auto seismic loads.
        /// </summary>
        /// <param name="patternName">The name of an existing Quake-type load pattern that has an auto seismic load assigned.</param>
        /// <param name="diaphragmName">The name of an existing special rigid diaphragm constraint, that is, a diaphragm constraint with the following features:
        /// 1. The constraint type is CONSTRAINT_DIAPHRAGM = 2.
        /// 2. The constraint coordinate system is Global.
        /// 3. The constraint axis is Z.</param>
        /// <param name="eccentricity">The eccentricity applied to the specified diaphragm. [L</param>
        /// <param name="delete">True: Eccentricity override for the specified diaphragm is deleted.</param>
        void SetDiaphragmEccentricityOverride(string patternName,
            string diaphragmName,
            double eccentricity,
            bool delete = false);
#endif
    }
}