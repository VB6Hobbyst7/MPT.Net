// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="IMassSource.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
    /// <summary>
    /// Implements the mass source in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    public interface IMassSource : IChangeableName, ICountable, IDeletable, IListableNames
    {
#else
    /// <summary>
    /// Implements the mass source in the application.
    /// </summary>
    public interface IMassSource
    {
#endif
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function retrieves the default mass source name.
        /// </summary>
        /// <param name="nameMassSource">The name of the mass source to be flagged as the default mass source.</param>
        void GetDefault(ref string nameMassSource);

        /// <summary>
        /// This function sets the default mass source.
        /// Only one mass source can be the default mass source so when this assignment is set all other mass sources are automatically set to have their IsDefault flag False.
        /// </summary>
        /// <param name="nameMassSource">The name of the mass source to be flagged as the default mass source.</param>
        void SetDefault(string nameMassSource);

        // ===

        /// <summary>
        /// This function gets the mass source data for an existing mass source.
        /// </summary>
        /// <param name="nameMassSource">The mass source name.</param>
        /// <param name="massFromElements">True: Element self mass is included in the mass.</param>
        /// <param name="massFromMasses">True: Assigned masses are included in the mass.</param>
        /// <param name="massFromLoads">True: Specified load patterns are included in the mass.</param>
        /// <param name="isDefault">True: Mass source is the default mass source.
        /// Only one mass source can be the default mass source so when this assignment is True all other mass sources are automatically set to have the IsDefault flag False.</param>
        /// <param name="namesLoadPatterns">This is an array of load pattern names specified for the mass source.</param>
        /// <param name="scaleFactors">This is an array of load pattern multipliers specified for the mass source.</param>
        void GetMassSource(string nameMassSource,
            ref bool massFromElements,
            ref bool massFromMasses,
            ref bool massFromLoads,
            ref bool isDefault,
            ref string[] namesLoadPatterns,
            ref double[] scaleFactors);

        /// <summary>
        /// This function adds a new mass source to the model or reinitializes an existing mass source.
        /// </summary>
        /// <param name="nameMassSource">The mass source name.
        /// If a mass source with this name already exists then the mass source is reinitialized with the new data.
        /// All previous data assigned to the mass source is lost.
        /// If a mass source with this name does not exist then a new mass source is added.</param>
        /// <param name="massFromElements">True: Element self mass is included in the mass.</param>
        /// <param name="massFromMasses">True: Assigned masses are included in the mass.</param>
        /// <param name="massFromLoads">True: Specified load patterns are included in the mass.</param>
        /// <param name="isDefault">True: Mass source is the default mass source.
        /// Only one mass source can be the default mass source so when this assignment is True all other mass sources are automatically set to have the IsDefault flag False.</param>
        /// <param name="namesLoadPatterns">This is an array of load pattern names specified for the mass source.</param>
        /// <param name="scaleFactors">This is an array of load pattern multipliers specified for the mass source.</param>
        void SetMassSource(string nameMassSource,
            bool massFromElements,
            bool massFromMasses,
            bool massFromLoads,
            bool isDefault,
            string[] namesLoadPatterns,
            double[] scaleFactors);
#endif
    }
}