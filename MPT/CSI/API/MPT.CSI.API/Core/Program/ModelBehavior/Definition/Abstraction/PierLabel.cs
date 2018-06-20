// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-03-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="PierLabel.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction
{
    /// <summary>
    /// Represents a pier label object in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction.IPierLabel" />
    public class PierLabel : CSiApiBase, IPierLabel
    {
#region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="PierLabel"/> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public PierLabel(CSiApiSeed seed) : base(seed)
        {

        }
#endregion

#region Methods: Interface
        /// <summary>
        /// Changes the name of a defined pier label property.
        /// </summary>
        /// <param name="nameExisting">Existing name of a defined pier label property.</param>
        /// <param name="nameNew">New name for the pier label property.</param>
        public void ChangeName(string nameExisting,
            string nameNew)
        {
            _callCode = _sapModel.PierLabel.ChangeName(nameExisting, nameNew);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// Deletes the specified pier label property.
        /// </summary>
        /// <param name="name">The name of an existing pier label property.</param>
        public void Delete(string name)
        {
            _callCode = _sapModel.PierLabel.Delete(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Retrieves the names of all defined pier label property.
        /// </summary>
        /// <param name="names">The pier label property names retrieved by the program.</param>
        public void GetNameList(out string[] names)
        {
            names = new string[0];
            _callCode = _sapModel.PierLabel.GetNameList(ref _numberOfItems, ref names);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// True: The Pier Label exists.
        /// </summary>
        /// <param name="name">The name of an existing Pier Label.</param>
        public bool GetPier(string name)
        {
            _callCode = _sapModel.PierLabel.GetPier(name);
            if (_callCode == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds a new Pier Label, or modifies an existing Pier Label.
        /// </summary>
        /// <param name="name">The name of a Pier Label. 
        /// If this is the name of an existing Pier label, that Pier label is modified, otherwise a new spandrel label is added.</param>
        public void SetPier(string name)
        {
            _callCode = _sapModel.PierLabel.SetPier(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
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
        public void GetSectionProperties(string name,
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
            ref double[] centerOfGravityTopZ)
        {
            _callCode = _sapModel.PierLabel.GetSectionProperties(name,
                                    ref _numberOfItems,
                                    ref storyNames,
                                    ref axisAngles,
                                    ref numberOfAreaObjects,
                                    ref numberOfLineObjects,
                                    ref widthBottom,
                                    ref thicknessBottom,
                                    ref widthTop,
                                    ref thicknessTop,
                                    ref materialPropertyNames,
                                    ref centerOfGravityBottomX,
                                    ref centerOfGravityBottomY,
                                    ref centerOfGravityBotZ,
                                    ref centerOfGravityTopX,
                                    ref centerOfGravityTopY,
                                    ref centerOfGravityTopZ);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
#endregion
    }
}
#endif