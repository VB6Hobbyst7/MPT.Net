// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-11-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-06-2017
// ***********************************************************************
// <copyright file="PointEditor.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Edit
{
    /// <summary>
    /// Represents the point editor in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Edit.IPointEditor" />
    public class PointEditor : CSiApiBase, IPointEditor
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="PointEditor" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public PointEditor(CSiApiSeed seed) : base(seed) { }


        #endregion

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        #region Methods: Public
        /// <summary>
        /// This function aligns selected point objects.
        /// </summary>
        /// <param name="alignmentOption">Method by which a point is aligned.</param>
        /// <param name="ordinate">The X, Y or Z ordinate that applies if MyType is 1, 2 or 3, respectively. [L]</param>
        /// <param name="numberPoints">The number of point objects that are in a new location after the alignment is complete</param>
        /// <param name="pointNames">The name of each point object that is in a new location after the alignment is complete.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void Align(ePointAlignOption alignmentOption,
            double ordinate,
            ref int numberPoints,
            ref string[] pointNames)
        {
            _callCode = _sapModel.EditPoint.Align((int)alignmentOption, ordinate, ref numberPoints, ref pointNames);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function connects objects that have been disconnected using the <see cref="Disconnect" /> function.
        /// If two or more objects have different end points objects that are at the same location, all of those objects can be connected together by selecting the objects, and selecting their end points, and calling the <see cref="Connect" /> function.
        /// The result will be that all of the objects are connected at a single point.
        /// </summary>
        /// <param name="numberPoints">The number of the point objects that remain at locations where connections were made.</param>
        /// <param name="pointNames">The name of each point object that remains at locations where connections were made.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void Connect(ref int numberPoints,
            ref string[] pointNames)
        {
            _callCode = _sapModel.EditPoint.Connect(ref numberPoints, ref pointNames);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function disconnects selected point objects.
        /// It creates a separate point for each object that frames into the selected point object.
        /// </summary>
        /// <param name="numberPoints">The number of the point objects (including the original selected point objects) that are created by the disconnect action.</param>
        /// <param name="pointNames">The name of each point object (including the original selected point objects) that is created by the disconnect action.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void Disconnect(ref int numberPoints,
            ref string[] pointNames)
        {
            _callCode = _sapModel.EditPoint.Disconnect(ref numberPoints, ref pointNames);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function merges selected point objects that are within a specified distance of one another.
        /// </summary>
        /// <param name="tolerance">Point objects within this distance of one another are merged into one point object. [L]</param>
        /// <param name="numberPoints">The number of the selected point objects that still exist after the merge is complete.</param>
        /// <param name="pointNames">The name of each selected point object that still exists after the merge is complete.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void Merge(double tolerance,
            ref int numberPoints,
            ref string[] pointNames)
        {
            _callCode = _sapModel.EditPoint.Merge(tolerance, ref numberPoints, ref pointNames);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function changes the coordinates of a specified point object.
        /// </summary>
        /// <param name="name">The name of an existing point object.</param>
        /// <param name="coordinate">Coordinate for the new point location.</param>
        /// <param name="noWindoRefresh">True: The model display window is not refreshed after the point object is moved.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void Move(string name,
            Coordinate3DCartesian coordinate,
            bool noWindoRefresh = false)
        {
            _callCode = _sapModel.EditPoint.ChangeCoordinates_1(name, coordinate.X, coordinate.Y, coordinate.Z, noWindoRefresh);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
#endif
    }
}
