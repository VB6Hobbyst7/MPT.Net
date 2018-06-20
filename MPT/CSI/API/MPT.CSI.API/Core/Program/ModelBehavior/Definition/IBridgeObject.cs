// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="IBridgeObject.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_CSiBridgev18 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Implements the bridge objects in the application.
    /// </summary>
    public interface IBridgeObject
    {
        /// <summary>
        /// This function returns a flag indicating if the specified bridge object is currently linked to existing objects in the model. <para/>
        /// If the bridge object is linked, it returns the model type (spine, area or solid) and meshing data used when the linked bridge model was updated.
        /// </summary>
        /// <param name="name">The name of an existing bridge object.</param>
        /// <param name="linkedModelExists">True: a linked bridge model exists for the specified bridge object.</param>
        /// <param name="modelType">Indicates the linked bridge model type. <para/>
        /// This only applies when <paramref name="linkedModelExists"/> is true.</param>
        /// <param name="maxLengthDeck">The maximum length for the deck objects in the linked bridge model. [L]<para/>
        /// This only applies when <paramref name="linkedModelExists"/> is true.</param>
        /// <param name="maxLengthCapBeam">The maximum length for the cap beam objects in the linked bridge model. [L]<para/>
        /// This only applies when <paramref name="linkedModelExists"/> is true.</param>
        /// <param name="maxLengthColumn">The maximum length for the column objects in the linked bridge model. [L]<para/>
        /// This only applies when <paramref name="linkedModelExists"/> is true.</param>
        /// <param name="subMeshSize">The maximum submesh size for area and solid objects in the linked bridge model. [L]<para/>
        /// This only applies when <paramref name="linkedModelExists"/> is true and <paramref name="modelType"/> is an area or solid model.</param>
        void GetBridgeUpdateData(string name,
            ref bool linkedModelExists,
            ref eBridgeLinkModelType modelType,
            ref double maxLengthDeck,
            ref double maxLengthCapBeam,
            ref double maxLengthColumn,
            ref double subMeshSize);

        /// <summary>
        /// This function updates a linked bridge model, clears all objects from a linked bridge model, or converts a linked bridge model to an unlinked model.
        /// </summary>
        /// <param name="name">The name of an existing bridge object.</param>
        /// <param name="action">The action to be taken to the linked bridge object.</param>
        /// <param name="modelType">Indicates the linked bridge model type. <para/>
        /// This only applies when <paramref name="action"/> is set to update.</param>
        /// <param name="maxLengthDeck">The maximum length for the deck objects in the linked bridge model. [L]<para/>
        /// This only applies when <paramref name="action"/> is set to update.</param>
        /// <param name="maxLengthCapBeam">The maximum length for the cap beam objects in the linked bridge model. [L]<para/>
        /// This only applies when <paramref name="action"/> is set to update.</param>
        /// <param name="maxLengthColumn">The maximum length for the column objects in the linked bridge model. [L]<para/>
        /// This only applies when <paramref name="action"/> is set to update.</param>
        /// <param name="subMeshSize">The maximum submesh size for area and solid objects in the linked bridge model. [L]<para/>
        /// This only applies when <paramref name="action"/> is set to update and <paramref name="modelType"/> is an area or solid model.</param>
        void SetBridgeUpdateData(string name,
            eBridgeLinkAction action,
            eBridgeLinkModelType modelType,
            double maxLengthDeck,
            double maxLengthCapBeam,
            double maxLengthColumn,
            double subMeshSize);

        // ===

        /// <summary>
        /// Determines whether or not bridge objects are to be automatically updated before analysis, if needed.
        /// </summary>
        /// <param name="updateBridgeObjects">True: Program automatically updates bridge objects before running an analysis if it detects anything has been changed that might affect the bridge analysis.</param>
        void GetBridgeUpdateForAnalysisFlag(ref bool updateBridgeObjects);

        /// <summary>
        /// Sets whether or not bridge objects are to be automatically updated before analysis, if needed.
        /// </summary>
        /// <param name="updateBridgeObjects">True: Program automatically updates bridge objects before running an analysis if it detects anything has been changed that might affect the bridge analysis.</param>
        void SetBridgeUpdateForAnalysisFlag(bool updateBridgeObjects);
    }
}
#endif