// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="IGeneralizedDisplacements.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Helpers;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Implements the generalized displacements in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    public interface IGeneralizedDisplacements: IChangeableName, ICountable, IDeletable, IListableNames
    {
        #region Methods: Public        

        /// <summary>
        /// Adds a new generalized displacement with the specified name and type.
        /// The new generalized displacement must have a different name from all other generalized displacements.
        /// If the name is not unique, an error will be returned. TODO: Handle this.
        /// </summary>
        /// <param name="name">The name of a new generalized displacement.</param>
        /// <param name="type">The generalized displacement type.</param>
        void Add(string name,
            eGeneralizedDisplacementType type);

        /// <summary>
        /// This function retrieves the total number of point objects included in a specified generalized displacement.
        /// </summary>
        /// <param name="name">The name of an existing generalized displacement.</param>
        /// <param name="count">The number of point objects included in the specified generalized displacement.</param>
        void CountPoint(string name,
            ref int count);

        /// <summary>
        /// This function deletes one point object from a generalized displacement definition.
        /// </summary>
        /// <param name="nameDisplacement">The name of an existing generalized displacement.</param>
        /// <param name="namePoint">The name of a point object included in the generalized displacement that is to be deleted.</param>
        void DeletePoint(string nameDisplacement,
            string namePoint);



        // === Get/Set
        /// <summary>
        /// This function retrieves the point objects and their scale factors from a generalized displacement definition.
        /// </summary>
        /// <param name="name">The name of an existing generalized displacement.</param>
        /// <param name="numberItems">The number of point objects included in the generalized displacement definition.</param>
        /// <param name="pointNames">The names of the point objects included in the generalized displacement definition.</param>
        /// <param name="scaleFactors">The unitless scale factors for each of the displacement degrees of freedom of the associated point objects that are included in the generalized displacement definition.</param>
        void GetPoint(string name,
            ref int numberItems,
            ref string[] pointNames,
            ref Deformations[] scaleFactors);

        /// <summary>
        /// This function adds a point object and its scale factors to a generalized displacement definition. , or, i
        /// If the point object already exists in the generalized displacement definition, it modifies the scale factors.
        /// </summary>
        /// <param name="name">The name of an existing generalized displacement.</param>
        /// <param name="pointName">The name of a point object to be included in the generalized displacement definition.</param>
        /// <param name="scaleFactors">Unitless scale factors for the point object displacement degrees of freedom.</param>
        void SetPoint(string name,
            string pointName,
            Deformations scaleFactors);

        // ===

        /// <summary>
        /// This function retrieves the generalized displacement type.
        /// </summary>
        /// <param name="name">The name of an existing generalized displacement.</param>
        /// <param name="type">The generalized displacement type.</param>
        void GetTypeGeneralizedDisplacement(string name,
            ref eGeneralizedDisplacementType type);

        /// <summary>
        /// This function sets the generalized displacement type.
        /// </summary>
        /// <param name="name">The name of an existing generalized displacement.</param>
        /// <param name="type">The generalized displacement type.</param>
        void SetTypeGeneralizedDisplacement(string name,
            eGeneralizedDisplacementType type);

        #endregion
    }
}