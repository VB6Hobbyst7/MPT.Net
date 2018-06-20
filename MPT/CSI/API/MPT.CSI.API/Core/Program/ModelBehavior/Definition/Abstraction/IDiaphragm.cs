// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-03-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-05-2017
// ***********************************************************************
// <copyright file="IDiaphragm.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction
{
    /// <summary>
    /// Object has a gettable/settable diaphragm.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    public interface IDiaphragm : IDeletable, IChangeableName, IListableNames
    {
        /// <summary>
        /// Retrieves the specified diaphragm.
        /// </summary>
        /// <param name="name">The name of an existing diaphragm.</param>
        /// <param name="semiRigid">True: Diaphragm is semi-rigid.</param>
        void GetDiaphragm(string name,
            ref bool semiRigid);

        /// <summary>
        /// Adds a new diaphragm, or modifies an existing diaphragm.
        /// </summary>
        /// <param name="name">The name of an existing diaphragm.</param>
        /// <param name="semiRigid">True: Diaphragm is semi-rigid.</param>
        void SetDiaphragm(string name,
            bool semiRigid);
    }
}
#endif