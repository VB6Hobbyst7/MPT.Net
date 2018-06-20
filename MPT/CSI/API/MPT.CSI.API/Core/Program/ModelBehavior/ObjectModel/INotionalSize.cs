// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-01-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="INotionalSize.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object can have notional size properties associated with it.
    /// </summary>
    public interface INotionalSize
    {
        /// <summary>
        /// This function retrieves the method to determine the notional size of a section for the creep and shrinkage calculations.
        /// This function is currently worked for shell type area section.
        /// </summary>
        /// <param name="name">The name of an existing section property.</param>
        /// <param name="sizeType">The type to define the notional size of a section.</param>
        /// <param name="value">For <paramref name="sizeType" /> is <see cref="eNotionalSizeType.Auto" />, the Value represents for the scale factor to the program-determined notional size.
        /// For <paramref name="sizeType" /> is <see cref="eNotionalSizeType.User" />, the Value represents for the user-defined notional size [L].
        /// For <paramref name="sizeType" /> is <see cref="eNotionalSizeType.None" />, the Value will not be used and can be set to 1.</param>
        void GetNotionalSize(string name,
            ref eNotionalSizeType sizeType,
            ref double value);

        /// <summary>
        /// This function assigns the method to determine the notional size of a section for the creep and shrinkage calculations.
        /// </summary>
        /// <param name="name">The name of an existing section property.</param>
        /// <param name="sizeType">The type to define the notional size of a section.</param>
        /// <param name="value">For <paramref name="sizeType" /> is <see cref="eNotionalSizeType.Auto" />, represents for the scale factor to the program-determined notional size.
        /// For <paramref name="sizeType" /> is <see cref="eNotionalSizeType.User" />, represents for the user-defined notional size [L].
        /// For <paramref name="sizeType" /> is <see cref="eNotionalSizeType.None" />, will not be used and can be set to 1.</param>
        void SetNotionalSize(string name,
            eNotionalSizeType sizeType,
            double value);
    }
}
#endif