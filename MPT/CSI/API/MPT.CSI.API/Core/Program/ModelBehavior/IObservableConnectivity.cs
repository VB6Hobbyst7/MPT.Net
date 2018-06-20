// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-07-2017
// ***********************************************************************
// <copyright file="IObservableConnectivity.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Object returns connectivity data.
    /// </summary>
    public interface IObservableConnectivity
    {
        /// <summary>
        /// This function returns a list of elements connected to a specified point element/object.
        /// </summary>
        /// <param name="name">The name of an existing point element/object.</param>
        /// <param name="numberItems">This is the total number of elements/objects connected to the specified point element/object.</param>
        /// <param name="objectTypes">The element/object type of each element/object connected to the specified point element/object.</param>
        /// <param name="objectNames">The element/object name of each element/object connected to the specified point element/object.</param>
        /// <param name="pointNumbers">The point number within the considered element/object that corresponds to the specified point element/object.</param>
        void GetConnectivity(string name,
            ref int numberItems,
            ref eObjectType[] objectTypes,
            ref string[] objectNames,
            ref int[] pointNumbers);
    }
}
