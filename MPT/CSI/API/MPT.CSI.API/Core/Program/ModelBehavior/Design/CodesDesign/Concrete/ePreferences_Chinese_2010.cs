// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 07-24-2017
//
// Last Modified By : Mark
// Last Modified On : 09-29-2017
// ***********************************************************************
// <copyright file="ePreferences_Chinese_2010.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_CSiBridgev18 && !BUILD_CSiBridgev19 && !BUILD_CSiBridgev20
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Concrete
{
    /// <summary>
    /// Preferences available for <see cref="Chinese_2010" /> concrete design in the application.
    /// </summary>
    public enum ePreferences_Chinese_2010
    {
        /// <summary>
        /// The number of interaction curves.
        /// </summary>
        NumberOfInteractionCurves = 1,

        /// <summary>
        /// The number of interaction points.
        /// </summary>
        NumberOfInteractionPoints = 2,

        /// <summary>
        /// Importance factor, gamma0.
        /// </summary>
        ImportanceFactor_Gamma0 = 3,

        /// <summary>
        /// Column design procedure.
        /// </summary>
        ColumnDesignProcedure = 4,

        /// <summary>
        /// SeismicDesignGrade.
        /// </summary>
        SeismicDesignGrade = 5,

        /// <summary>
        /// The pattern live load factor.
        /// </summary>
        PatternLiveLoadFactor = 6,

        /// <summary>
        /// The utilization factor limit.
        /// </summary>
        UtilizationFactorLimit = 7,

        /// <summary>
        /// The multi-response case design.
        /// </summary>
        MultiResponseCaseDesign = 8,

        /// <summary>
        /// Structural system.
        /// </summary>
        StructuralSystem = 9,

        /// <summary>
        /// Is the building a tall building?
        /// </summary>
        IsTallBuilding = 10,

        /// <summary>
        /// Seismic field type.
        /// </summary>
        SeismicFieldType = 11
    }
}
#endif