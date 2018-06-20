// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-03-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="IPierLabel.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction
{
    /// <summary>
    /// Object has a gettable/settable pier label.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    public interface IPierLabel : IDeletable, IChangeableName, IListableNames
    {
        /// <summary>
        /// True: The Pier Label exists.
        /// </summary>
        /// <param name="name">The name of an existing Spandrel Label.</param>
        bool GetPier(string name);

        /// <summary>
        /// Adds a new Pier Label, or modifies an existing Spandrel Label.
        /// </summary>
        /// <param name="name">The name of a Spandrel Label. 
        /// If this is the name of an existing spandrel label, that spandrel label is modified, otherwise a new spandrel label is added.</param>
        void SetPier(string name);

#if !BUILD_ETABS2015
        /// <summary>
        /// Retrieves the section properties for a specified pier.
        /// </summary>
        /// <param name="name">The name of an existing pier.</param>
        /// <param name="storyNames">The story names at which the pier exists.</param>
        /// <param name="axisAngles">The pier local axis angle at each story, defined as the angle between the global x-axis and the pier local 2-axis.</param>
        /// <param name="numberOfAreaObjects">The number of area objects in the pier at each story.</param>
        /// <param name="numberOfLineObjects">The number of line objects in the pier at each story.</param>
        /// <param name="widthBottom">The width of the pier at the bottom of each story.</param>
        /// <param name="thicknessBottom">The thickness of the pier at the bottom of each story.</param>
        /// <param name="widthTop">The width of the pier at the top of each story.</param>
        /// <param name="thicknessTop">The thickness of the pier at the top of each story.</param>
        /// <param name="materialPropertyNames">The name of the pier material property at each story.</param>
        /// <param name="centerOfGravityBottomX">The x-coordinate of the center of gravity at the bottom of each story.</param>
        /// <param name="centerOfGravityBottomY">The y-coordinate of the center of gravity at the bottom of each story.</param>
        /// <param name="centerOfGravityBotZ">The z-coordinate of the center of gravity at the bottom of each story.</param>
        /// <param name="centerOfGravityTopX">The x-coordinate of the center of gravity at the top of each story.</param>
        /// <param name="centerOfGravityTopY">The y-coordinate of the center of gravity at the top of each story.</param>
        /// <param name="centerOfGravityTopZ">The z-coordinate of the center of gravity at the top of each story.</param>
        void GetSectionProperties(string name,
            ref string[] storyNames,
            ref double[] axisAngles,
            ref int[] numberOfAreaObjects,
            ref int[] numberOfLineObjects,
            ref double[] widthBottom,
            ref double[] thicknessBottom,
            ref double[] widthTop,
            ref double[] thicknessTop,
            ref string[] materialPropertyNames,
            ref double[] centerOfGravityBottomX,
            ref double[] centerOfGravityBottomY,
            ref double[] centerOfGravityBotZ,
            ref double[] centerOfGravityTopX,
            ref double[] centerOfGravityTopY,
            ref double[] centerOfGravityTopZ);
#endif
    }
}
#endif