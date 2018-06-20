// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-03-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="ISpandrelLabel.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction
{
    /// <summary>
    /// Object has a gettable/settable spandrel label.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    public interface ISpandrelLabel : IDeletable, IChangeableName, IListableNames
    {
        /// <summary>
        /// Retrieves the names of all defined spandrel label property.
        /// </summary>
        /// <param name="names">The spandrel label property names retrieved by the program.</param>
        /// <param name="isMultiStory">True: Spandrel Label spans multiple story levels .</param>
        void GetNameList(ref string[] names,
            ref bool[] isMultiStory);

        /// <summary>
        /// True: The Spandrel Label exists.
        /// </summary>
        /// <param name="name">The name of an existing Spandrel Label.</param>
        /// <param name="isMultiStory">True: Spandrel Label spans multiple story levels .</param>
        bool GetSpandrel(string name,
            ref bool isMultiStory);

        /// <summary>
        /// Adds a new Spandrel Label, or modifies an existing Spandrel Label.
        /// </summary>
        /// <param name="name">The name of a Spandrel Label. 
        /// If this is the name of an existing spandrel label, that spandrel label is modified, otherwise a new spandrel label is added.</param>
        /// <param name="isMultiStory">True: Spandrel Label spans multiple story levels .</param>
        void SetSpandrel(string name,
            bool isMultiStory);
    }
}
#endif