// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-10-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-11-2017
// ***********************************************************************
// <copyright file="SectionDesigner.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.Frame
{
    /// <summary>
    /// Represents the section designer frame section in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.Frame.ISectionDesigner" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class SectionDesigner : CSiApiBase, ISectionDesigner
    {
        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="SectionDesigner" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public SectionDesigner(CSiApiSeed seed) : base(seed) { }


        #endregion

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        #region Methods: Public
        /// <summary>
        /// Deletes the specified name.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing shape in a section designer property.
        /// If the <paramref name="deleteAll" /> item is True, this item may be specified as a blank string.</param>
        /// <param name="deleteAll">True: All shapes in the section designer property specified by the Name item are deleted</param>
        /// <exception cref="CSiException">API function failed.</exception>
        public void Delete(string name,
            string nameShape,
            bool deleteAll = false)
        {
            _callCode = _sapModel.PropFrame.SDShape.Delete(name, nameShape, deleteAll);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        #endregion
#endif

        #region Methods: Get/Set Sections - Steel                
#if !BUILD_ETABS2015
        /// <summary>
        /// This function retrieves property data for a Tee shape in a section designer section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing Tee shape in the specified frame section property.</param>
        /// <param name="nameMaterial">The name of the material property for the shape.</param>
        /// <param name="fileName">This is a blank string or the name of a defined Tee property that has been imported from a section property file.
        /// If it is the name of a defined Tee property, the section dimensions are taken from that property.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="rotation">The counter clockwise rotation of the shape from its default orientation. [deg].</param>
        /// <param name="color">The fill color assigned to the shape.</param>
        /// <param name="h">The section depth. [L].</param>
        /// <param name="bf">The section width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetTee(string name,
            string nameShape,
            ref string nameMaterial,
            ref string fileName,
            ref Coordinate2DCartesian centerCoordinate,
            ref double rotation,
            ref int color,
            ref double h,
            ref double bf,
            ref double tf,
            ref double tw)
        {
            double xCenter = 0;
            double yCenter = 0;

            _callCode = _sapModel.PropFrame.SDShape.GetTee(name, nameShape, ref nameMaterial, ref fileName,
                ref color, ref xCenter, ref yCenter,
                ref h, ref bf, ref tf, ref tw, ref rotation);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            centerCoordinate.X = xCenter;
            centerCoordinate.Y = yCenter;
        }


        /// <summary>
        /// This function retrieves property data for a Angle shape in a section designer section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing Angle shape in the specified frame section property.</param>
        /// <param name="nameMaterial">The name of the material property for the shape.</param>
        /// <param name="fileName">This is a blank string or the name of a defined Angle property that has been imported from a section property file.
        /// If it is the name of a defined Angle property, the section dimensions are taken from that property.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="rotation">The counter clockwise rotation of the shape from its default orientation. [deg].</param>
        /// <param name="color">The fill color assigned to the shape.</param>
        /// <param name="h">The section depth. [L].</param>
        /// <param name="bf">The flange width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetAngle(string name,
            string nameShape,
            ref string nameMaterial,
            ref string fileName,
            ref Coordinate2DCartesian centerCoordinate,
            ref double rotation,
            ref int color,
            ref double h,
            ref double bf,
            ref double tf,
            ref double tw)
        {
            double xCenter = 0;
            double yCenter = 0;

            _callCode = _sapModel.PropFrame.SDShape.GetAngle(name, nameShape, ref nameMaterial, ref fileName,
                ref color, ref xCenter, ref yCenter,
                ref h, ref bf, ref tf, ref tw, ref rotation);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            centerCoordinate.X = xCenter;
            centerCoordinate.Y = yCenter;
        }


        /// <summary>
        /// This function retrieves property data for a I-Section shape in a section designer section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing I-Section shape in the specified frame section property.</param>
        /// <param name="nameMaterial">The name of the material property for the shape.</param>
        /// <param name="fileName">This is a blank string or the name of a defined I-Section property that has been imported from a section property file.
        /// If it is the name of a defined I-Section property, the section dimensions are taken from that property.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="rotation">The counter clockwise rotation of the shape from its default orientation. [deg].</param>
        /// <param name="color">The fill color assigned to the shape.</param>
        /// <param name="h">The section depth. [L].</param>
        /// <param name="bf">The top flange width. [L].</param>
        /// <param name="tf">The top flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="bfBottom">The bottom flange width. [L].</param>
        /// <param name="tfBottom">The bottom flange thickness. [L].&gt;</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetISection(string name,
            string nameShape,
            ref string nameMaterial,
            ref string fileName,
            ref Coordinate2DCartesian centerCoordinate,
            ref double rotation,
            ref int color,
            ref double h,
            ref double bf,
            ref double tf,
            ref double tw,
            ref double bfBottom,
            ref double tfBottom)
        {
            double xCenter = 0;
            double yCenter = 0;

            _callCode = _sapModel.PropFrame.SDShape.GetISection(name, nameShape, ref nameMaterial, ref fileName,
                ref color, ref xCenter, ref yCenter,
                ref h, ref bf, ref tf, ref tw, ref bfBottom, ref tfBottom, ref rotation);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            centerCoordinate.X = xCenter;
            centerCoordinate.Y = yCenter;
        }
#endif
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017


        /// <summary>
        /// This function adds a new Tee shape or modifies an existing shape to be a Tee shape in a section designer property.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing Tee shape in the specified frame section property.
        /// If this is an existing shape, that shape is modified; otherwise, a new shape is added.
        /// This item may be input as a blank string, in which case the program will assign a shape name to the shape and return that name in the <paramref name="nameShape" /> variable.</param>
        /// <param name="nameMaterial">The name of the material property for the shape.</param>
        /// <param name="fileName">This is a blank string or the name of a defined Tee property that has been imported from a section property file.
        /// If this item is a blank string, the section dimensions are taken from the values input in this function.
        /// If this item is the name of a defined Tee property that has been imported from a section property file, the section dimensions are taken from the specified Tee property.
        /// If this item is not blank, and the specified property name is not a Tee that was imported from a section property file, an error is returned.
        /// TODO: Handle this.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="rotation">The counter clockwise rotation of the shape from its default orientation. [deg].</param>
        /// <param name="color">The fill color assigned to the shape.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign the default fill color.</param>
        /// <param name="h">The section depth. [L].</param>
        /// <param name="bf">The section width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetTee(string name,
            ref string nameShape,
            string nameMaterial,
            string fileName,
            Coordinate2DCartesian centerCoordinate,
            double rotation,
            int color = -1,
            double h = 24,
            double bf = 24,
            double tf = 2.4,
            double tw = 2.4)
        {
            _callCode = _sapModel.PropFrame.SDShape.SetTee(name, ref nameShape, nameMaterial, fileName,
                centerCoordinate.X, centerCoordinate.Y, rotation, color,
                h, bf, tf, tw);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function adds a new Angle shape or modifies an existing shape to be a Angle shape in a section designer property.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing Angle shape in the specified frame section property.
        /// If this is an existing shape, that shape is modified; otherwise, a new shape is added.
        /// This item may be input as a blank string, in which case the program will assign a shape name to the shape and return that name in the <paramref name="nameShape" /> variable.</param>
        /// <param name="nameMaterial">The name of the material property for the shape.</param>
        /// <param name="fileName">This is a blank string or the name of a defined Angle property that has been imported from a section property file.
        /// If this item is a blank string, the section dimensions are taken from the values input in this function.
        /// If this item is the name of a defined Angle property that has been imported from a section property file, the section dimensions are taken from the specified Angle property.
        /// If this item is not blank, and the specified property name is not a Angle that was imported from a section property file, an error is returned.
        /// TODO: Handle this.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="rotation">The counter clockwise rotation of the shape from its default orientation. [deg].</param>
        /// <param name="color">The fill color assigned to the shape.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign the default fill color.</param>
        /// <param name="h">The section depth. [L].</param>
        /// <param name="bf">The flange width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetAngle(string name,
            ref string nameShape,
            string nameMaterial,
            string fileName,
            Coordinate2DCartesian centerCoordinate,
            double rotation,
            int color = -1,
            double h = 24,
            double bf = 24,
            double tf = 2.4,
            double tw = 2.4)
        {
            _callCode = _sapModel.PropFrame.SDShape.SetAngle(name, ref nameShape, nameMaterial, fileName,
                centerCoordinate.X, centerCoordinate.Y, rotation, color,
                h, bf, tf, tw);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function retrieves property data for a Channel shape in a section designer section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing Channel shape in the specified frame section property.</param>
        /// <param name="nameMaterial">The name of the material property for the shape.</param>
        /// <param name="fileName">This is a blank string or the name of a defined Channel property that has been imported from a section property file.
        /// If it is the name of a defined Channel property, the section dimensions are taken from that property.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="rotation">The counter clockwise rotation of the shape from its default orientation. [deg].</param>
        /// <param name="color">The fill color assigned to the shape.</param>
        /// <param name="h">The section depth. [L].</param>
        /// <param name="bf">The flange width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetChannel(string name,
            string nameShape,
            ref string nameMaterial,
            ref string fileName,
            ref Coordinate2DCartesian centerCoordinate,
            ref double rotation,
            ref int color,
            ref double h,
            ref double bf,
            ref double tf,
            ref double tw)
        {
            double xCenter = 0;
            double yCenter = 0;

            _callCode = _sapModel.PropFrame.SDShape.GetChannel(name, nameShape, ref nameMaterial, ref fileName,
                ref color, ref xCenter, ref yCenter,
                ref h, ref bf, ref tf, ref tw, ref rotation);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            centerCoordinate.X = xCenter;
            centerCoordinate.Y = yCenter;
        }

        /// <summary>
        /// This function adds a new Channel shape or modifies an existing shape to be a Channel shape in a section designer property.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing Channel shape in the specified frame section property.
        /// If this is an existing shape, that shape is modified; otherwise, a new shape is added.
        /// This item may be input as a blank string, in which case the program will assign a shape name to the shape and return that name in the <paramref name="nameShape" /> variable.</param>
        /// <param name="nameMaterial">The name of the material property for the shape.</param>
        /// <param name="fileName">This is a blank string or the name of a defined Channel property that has been imported from a section property file.
        /// If this item is a blank string, the section dimensions are taken from the values input in this function.
        /// If this item is the name of a defined Channel property that has been imported from a section property file, the section dimensions are taken from the specified Channel property.
        /// If this item is not blank, and the specified property name is not a Channel that was imported from a section property file, an error is returned.
        /// TODO: Handle this.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="rotation">The counter clockwise rotation of the shape from its default orientation. [deg].</param>
        /// <param name="color">The fill color assigned to the shape.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign the default fill color.</param>
        /// <param name="h">The section depth. [L].</param>
        /// <param name="bf">The flange width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetChannel(string name,
            ref string nameShape,
            string nameMaterial,
            string fileName,
            Coordinate2DCartesian centerCoordinate,
            double rotation,
            int color = -1,
            double h = 24,
            double bf = 24,
            double tf = 2.4,
            double tw = 2.4)
        {
            _callCode = _sapModel.PropFrame.SDShape.SetChannel(name, ref nameShape, nameMaterial, fileName,
                centerCoordinate.X, centerCoordinate.Y, rotation, color,
                h, bf, tf, tw);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function retrieves property data for a Double Angle shape in a section designer section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing Double Angle shape in the specified frame section property.</param>
        /// <param name="nameMaterial">The name of the material property for the shape.</param>
        /// <param name="fileName">This is a blank string or the name of a defined Double Angle property that has been imported from a section property file.
        /// If it is the name of a defined Double Angle property, the section dimensions are taken from that property.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="rotation">The counter clockwise rotation of the shape from its default orientation. [deg].</param>
        /// <param name="color">The fill color assigned to the shape.</param>
        /// <param name="h">The section depth. [L].</param>
        /// <param name="w">The flange width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="separation">Separation between the two flanges that are parallel. [L].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetDoubleAngle(string name,
            string nameShape,
            ref string nameMaterial,
            ref string fileName,
            ref Coordinate2DCartesian centerCoordinate,
            ref double rotation,
            ref int color,
            ref double h,
            ref double w,
            ref double tf,
            ref double tw,
            ref double separation)
        {
            double xCenter = 0;
            double yCenter = 0;

            _callCode = _sapModel.PropFrame.SDShape.GetDblAngle(name, nameShape, ref nameMaterial, ref fileName,
                ref color, ref xCenter, ref yCenter,
                ref h, ref w, ref tf, ref tw, ref separation, ref rotation);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            centerCoordinate.X = xCenter;
            centerCoordinate.Y = yCenter;
        }

        /// <summary>
        /// This function adds a new Double Angle shape or modifies an existing shape to be a Double Angle shape in a section designer property.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing Double Angle shape in the specified frame section property.
        /// If this is an existing shape, that shape is modified; otherwise, a new shape is added.
        /// This item may be input as a blank string, in which case the program will assign a shape name to the shape and return that name in the <paramref name="nameShape" /> variable.</param>
        /// <param name="nameMaterial">The name of the material property for the shape.</param>
        /// <param name="fileName">This is a blank string or the name of a defined Double Angle property that has been imported from a section property file.
        /// If this item is a blank string, the section dimensions are taken from the values input in this function.
        /// If this item is the name of a defined Double Angle property that has been imported from a section property file, the section dimensions are taken from the specified Double Angle property.
        /// If this item is not blank, and the specified property name is not a Double Angle that was imported from a section property file, an error is returned.
        /// TODO: Handle this.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="rotation">The counter clockwise rotation of the shape from its default orientation. [deg].</param>
        /// <param name="color">The fill color assigned to the shape.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign the default fill color.</param>
        /// <param name="h">The section depth. [L].</param>
        /// <param name="w">The flange width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="separation">Separation between the two flanges that are parallel. [L].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetDoubleAngle(string name,
            ref string nameShape,
            string nameMaterial,
            string fileName,
            Coordinate2DCartesian centerCoordinate,
            double rotation,
            int color = -1,
            double h = 24,
            double w = 24,
            double tf = 2.4,
            double tw = 2.4,
            double separation = 1.2)
        {
            _callCode = _sapModel.PropFrame.SDShape.SetDblAngle(name, ref nameShape, nameMaterial, fileName,
                centerCoordinate.X, centerCoordinate.Y, rotation, color,
                h, w, tf, tw, separation);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function adds a new I-Section shape or modifies an existing shape to be a I-Section shape in a section designer property.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing I-Section shape in the specified frame section property.
        /// If this is an existing shape, that shape is modified; otherwise, a new shape is added.
        /// This item may be input as a blank string, in which case the program will assign a shape name to the shape and return that name in the <paramref name="nameShape" /> variable.</param>
        /// <param name="nameMaterial">The name of the material property for the shape.</param>
        /// <param name="fileName">This is a blank string or the name of a defined I-Section property that has been imported from a section property file.
        /// If this item is a blank string, the section dimensions are taken from the values input in this function.
        /// If this item is the name of a defined I-Section property that has been imported from a section property file, the section dimensions are taken from the specified I-Section property.
        /// If this item is not blank, and the specified property name is not a I-Section that was imported from a section property file, an error is returned.
        /// TODO: Handle this.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="rotation">The counter clockwise rotation of the shape from its default orientation. [deg].</param>
        /// <param name="color">The fill color assigned to the shape.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign the default fill color.</param>
        /// <param name="h">The section depth. [L].</param>
        /// <param name="bf">The top flange width. [L].</param>
        /// <param name="tf">The top flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="bfBottom">The bottom flange width. [L].</param>
        /// <param name="tfBottom">The bottom flange thickness. [L].&gt;</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetISection(string name,
            ref string nameShape,
            string nameMaterial,
            string fileName,
            Coordinate2DCartesian centerCoordinate,
            double rotation,
            int color = -1,
            double h = 24,
            double bf = 24,
            double tf = 2.4,
            double tw = 2.4,
            double bfBottom = 24,
            double tfBottom = 2.4)
        {
            _callCode = _sapModel.PropFrame.SDShape.SetISection(name, ref nameShape, nameMaterial, fileName,
                centerCoordinate.X, centerCoordinate.Y, rotation, color,
                h, bf, tf, tw, bfBottom, tfBottom);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function retrieves property data for a Plate shape in a section designer section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing Plate shape in the specified frame section property.</param>
        /// <param name="nameMaterial">The name of the material property for the shape.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="rotation">The counter clockwise rotation of the shape from its default orientation. [deg].</param>
        /// <param name="color">The fill color assigned to the shape.</param>
        /// <param name="thickness">The section thickness. [L].</param>
        /// <param name="width">The section width. [L].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetPlate(string name,
            string nameShape,
            ref string nameMaterial,
            ref Coordinate2DCartesian centerCoordinate,
            ref double rotation,
            ref int color,
            ref double thickness,
            ref double width)
        {
            double xCenter = 0;
            double yCenter = 0;

            _callCode = _sapModel.PropFrame.SDShape.GetPlate(name, nameShape, ref nameMaterial, 
                ref color, ref xCenter, ref yCenter,
                ref thickness, ref width, ref rotation);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            centerCoordinate.X = xCenter;
            centerCoordinate.Y = yCenter;
        }

        /// <summary>
        /// This function adds a new Plate shape or modifies an existing shape to be a Plate shape in a section designer property.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing Plate shape in the specified frame section property.
        /// If this is an existing shape, that shape is modified; otherwise, a new shape is added.
        /// This item may be input as a blank string, in which case the program will assign a shape name to the shape and return that name in the <paramref name="nameShape" /> variable.</param>
        /// <param name="nameMaterial">The name of the material property for the shape.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="rotation">The counter clockwise rotation of the shape from its default orientation. [deg].</param>
        /// <param name="color">The fill color assigned to the shape.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign the default fill color.</param>
        /// <param name="thickness">The section thickness. [L].</param>
        /// <param name="width">The section width. [L].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetPlate(string name,
            ref string nameShape,
            string nameMaterial,
            Coordinate2DCartesian centerCoordinate,
            double rotation,
            int color = -1,
            double thickness = 2.4,
            double width = 24)
        {
            _callCode = _sapModel.PropFrame.SDShape.SetPlate(name, ref nameShape, nameMaterial,
                centerCoordinate.X, centerCoordinate.Y, rotation, color,
                thickness, width);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion

        #region Methods: Get/Set Sections - Solid        
#if !BUILD_ETABS2015
        /// <summary>
        /// This function retrieves property data for a solid circle shape in a section designer section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing solid circle shape in the specified frame section property.</param>
        /// <param name="nameMaterial">The name of the material property for the shape.</param>
        /// <param name="stressStrainOverwrite">This is a blank string, Default, or the name of a defined stress-strain curve.
        /// If this item is a blank string or Default, the shape stress-strain curve is based on the assigned material property.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="rotation">The counter clockwise rotation of the shape from its default orientation. [deg].</param>
        /// <param name="diameter">The diameter of the circle.[L].</param>
        /// <param name="color">The fill color assigned to the shape.</param>
        /// <param name="isReinforced">True: There is edge and corner reinforcing steel associated with the shape.
        /// The <paramref name="nameMaterial" /> item must refer to a concrete material for this item to be True.</param>
        /// <param name="nameMaterialRebar">The material property for the edge and corner reinforcing steel associated with the shape.
        /// This item applies only when the <paramref name="nameMaterial" /> item is a concrete material and the <paramref name="isReinforced" /> item is True.</param>
        /// <param name="numberOfBars">The number of equally spaced bars for the circular reinforcing.
        /// This item is visible only if <paramref name="isReinforced" /> = True.</param>
        /// <param name="cover">The clear cover for the specified rebar. [L].
        /// This item is visible only if <paramref name="isReinforced" /> = True.</param>
        /// <param name="barSize">The size of the reinforcing bar.
        /// This item is visible only if <paramref name="isReinforced" /> = True.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetCircle(string name,
            string nameShape,
            ref string nameMaterial,
            ref string stressStrainOverwrite,
            ref Coordinate2DCartesian centerCoordinate,
            ref double rotation,
            ref double diameter,
            ref int color,
            ref bool isReinforced,
            ref string nameMaterialRebar,
            ref int numberOfBars,
            ref double cover,
            ref string barSize)
        {
            double xCoordinate = 0;
            double yCoordinate = 0;

            _callCode = _sapModel.PropFrame.SDShape.GetSolidCircle(name, nameShape, ref nameMaterial, ref stressStrainOverwrite,
                ref color, ref xCoordinate, ref yCoordinate, ref diameter,
                ref isReinforced, ref numberOfBars, ref rotation, ref cover, ref barSize, ref nameMaterialRebar);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            centerCoordinate.X = xCoordinate;
            centerCoordinate.Y = yCoordinate;
        }

        /// <summary>
        /// This function retrieves property data for a solid rectangular shape in a section designer section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing solid rectangular shape in the specified frame section property.</param>
        /// <param name="nameMaterial">The name of the material property for the shape.</param>
        /// <param name="stressStrainOverwrite">This is a blank string, Default, or the name of a defined stress-strain curve.
        /// If this item is a blank string or Default, the shape stress-strain curve is based on the assigned material property.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="rotation">The counter clockwise rotation of the shape from its default orientation. [deg].</param>
        /// <param name="h">The section depth. [L].</param>
        /// <param name="w">The section width. [L].</param>
        /// <param name="color">The fill color assigned to the shape.</param>
        /// <param name="isReinforced">True: There is edge and corner reinforcing steel associated with the shape.
        /// The <paramref name="nameMaterial" /> item must refer to a concrete material for this item to be True.</param>
        /// <param name="nameMaterialRebar">The material property for the edge and corner reinforcing steel associated with the shape.
        /// This item applies only when the <paramref name="nameMaterial" /> item is a concrete material and the <paramref name="isReinforced" /> item is True.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetRectangle(string name,
            string nameShape,
            ref string nameMaterial,
            ref string stressStrainOverwrite,
            ref Coordinate2DCartesian centerCoordinate,
            ref double rotation,
            ref double h,
            ref double w,
            ref int color,
            ref bool isReinforced,
            ref string nameMaterialRebar)
        {
            double xCoordinate = 0;
            double yCoordinate = 0;

            _callCode = _sapModel.PropFrame.SDShape.GetSolidRect(name, nameShape, ref nameMaterial, ref stressStrainOverwrite, ref color,
                ref xCoordinate, ref yCoordinate, ref h, ref w, ref rotation, ref isReinforced, ref nameMaterialRebar);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            centerCoordinate.X = xCoordinate;
            centerCoordinate.Y = yCoordinate;
        }
#endif
#if BUILD_SAP2000v19 || BUILD_SAP2000v20 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
        /// <summary>
        /// This function retrieves property data for a polygon shape in a section designer section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing polygon shape in the specified frame section property.</param>
        /// <param name="nameMaterial">The name of the material property for the shape.</param>
        /// <param name="stressStrainOverwrite">This is a blank string, Default, or the name of a defined stress-strain curve.
        /// If this item is a blank string or Default, the shape stress-strain curve is based on the assigned material property.</param>
        /// <param name="numberOfPoints">The number of point coordinates used to define the polygon.</param>
        /// <param name="coordinates">The coordinates of the polygon points. [L].</param>
        /// <param name="radius">The radius to be applied at each of the polygon points. [L].</param>
        /// <param name="color">The fill color assigned to the shape.</param>
        /// <param name="isReinforced">True: There is edge and corner reinforcing steel associated with the shape.
        /// The <paramref name="nameMaterial" /> item must refer to a concrete material for this item to be True.</param>
        /// <param name="nameMaterialRebar">The material property for the edge and corner reinforcing steel associated with the shape.
        /// This item applies only when the <paramref name="nameMaterial" /> item is a concrete material and the <paramref name="isReinforced" /> item is True.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetPolygon(string name,
            string nameShape,
            ref string nameMaterial,
            ref string stressStrainOverwrite,
            ref int numberOfPoints,
            ref Coordinate2DCartesian[] coordinates,
            ref double[] radius,
            ref int color,
            ref bool isReinforced,
            ref string nameMaterialRebar)
        {
            double[] xCoordinates = new double[0];
            double[] yCoordinates = new double[0];

            _callCode = _sapModel.PropFrame.SDShape.GetPolygon(name, nameShape, ref nameMaterial, ref stressStrainOverwrite,
                ref numberOfPoints,  ref xCoordinates, ref yCoordinates, ref radius, 
                ref color, ref isReinforced, ref nameMaterialRebar);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            coordinates = new Coordinate2DCartesian[numberOfPoints - 1];
            for (int i = 0; i < numberOfPoints; i++)
            {
                coordinates[i].X = xCoordinates[i];
                coordinates[i].Y = yCoordinates[i];
            }
        }

        /// <summary>
        /// This function adds a new polygon shape or modifies an existing shape to be a polygon shape in a section designer property.
        /// The polygon points must be defined in order around the polygon, otherwise the shape will be created incorrectly or the creation of the shape will fail.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing polygon shape in the specified frame section property.
        /// If this is an existing shape, that shape is modified; otherwise, a new shape is added.
        /// This item may be input as a blank string, in which case the program will assign a shape name to the shape and return that name in the <paramref name="nameShape" /> variable.</param>
        /// <param name="nameMaterial">The name of the material property for the shape.</param>
        /// <param name="stressStrainOverwrite">This is a blank string, Default, or the name of a defined stress-strain curve.
        /// If this item is a blank string or Default, the shape stress-strain curve is based on the assigned material property.</param>
        /// <param name="numberOfPoints">The number of point coordinates used to define the polygon.</param>
        /// <param name="coordinates">The coordinates of the polygon points. [L].</param>
        /// <param name="radius">The radius to be applied at each of the polygon points. [L].</param>
        /// <param name="color">The fill color assigned to the shape.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign the default fill color.</param>
        /// <param name="isReinforced">True: There is edge and corner reinforcing steel associated with the shape.
        /// The <paramref name="nameMaterial" /> item must refer to a concrete material for this item to be True.</param>
        /// <param name="nameMaterialRebar">The material property for the edge and corner reinforcing steel associated with the shape.
        /// This item applies only when the <paramref name="nameMaterial" /> item is a concrete material and the <paramref name="isReinforced" /> item is True.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetPolygon(string name,
            ref string nameShape,
            string nameMaterial,
            string stressStrainOverwrite,
            int numberOfPoints,
            Coordinate2DCartesian[] coordinates,
            double[] radius,
            int color = -1,
            bool isReinforced = false,
            string nameMaterialRebar = "")
        {
            arraysLengthMatch(nameof(coordinates), coordinates.Length, nameof(radius), radius.Length);

            double[] xCoordinates = new double[numberOfPoints - 1];
            double[] yCoordinates = new double[numberOfPoints - 1];
            for (int i = 0; i < numberOfPoints; i++)
            {
                xCoordinates[i] = coordinates[i].X;
                yCoordinates[i] = coordinates[i].Y;
            }

            _callCode = _sapModel.PropFrame.SDShape.SetPolygon(name, ref nameShape, nameMaterial, stressStrainOverwrite,
                numberOfPoints, ref xCoordinates, ref yCoordinates, ref radius,
                color, isReinforced, nameMaterialRebar);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017


        /// <summary>
        /// This function retrieves property data for a solid sector shape in a section designer section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing solid sector shape in the specified frame section property.</param>
        /// <param name="nameMaterial">The name of the material property for the shape.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="rotation">The counter clockwise rotation of the shape from its default orientation. [deg].</param>
        /// <param name="angle">The angle between the two radii that define the circular sector. [deg].</param>
        /// <param name="radius">The radius of the circle defining the Sector. [L].</param>
        /// <param name="color">The fill color assigned to the shape.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetSolidSector(string name,
            string nameShape,
            ref string nameMaterial,
            ref Coordinate2DCartesian centerCoordinate,
            ref double rotation,
            ref double angle,
            ref double radius,
            ref int color)
        {
            double xCoordinate = 0;
            double yCoordinate = 0;

            _callCode = _sapModel.PropFrame.SDShape.GetSolidSector(name, nameShape, ref nameMaterial, ref color,
                ref xCoordinate, ref yCoordinate, ref angle, ref rotation, ref radius);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            centerCoordinate.X = xCoordinate;
            centerCoordinate.Y = yCoordinate;
        }

        /// <summary>
        /// This function adds a new solid sector shape or modifies an existing shape to be a solid sector shape in a section designer property.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing solid sector shape in the specified frame section property.
        /// If this is an existing shape, that shape is modified; otherwise, a new shape is added.
        /// This item may be input as a blank string, in which case the program will assign a shape name to the shape and return that name in the <paramref name="nameShape" /> variable.</param>
        /// <param name="nameMaterial">The name of the material property for the shape.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="rotation">The counter clockwise rotation of the shape from its default orientation. [deg].</param>
        /// <param name="angle">The angle between the two radii that define the circular sector. [deg].</param>
        /// <param name="radius">The radius of the circle defining the Sector. [L].</param>
        /// <param name="color">The fill color assigned to the shape.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign the default fill color.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetSolidSector(string name,
            ref string nameShape,
            string nameMaterial,
            Coordinate2DCartesian centerCoordinate,
            double rotation,
            double angle,
            double radius,
            int color = -1)
        {
            double xCoordinate = centerCoordinate.X;
            double yCoordinate = centerCoordinate.Y;

            _callCode = _sapModel.PropFrame.SDShape.SetSolidSector(name, ref nameShape, nameMaterial,
                xCoordinate, yCoordinate, angle, rotation, radius, color);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <summary>
        /// This function retrieves property data for a solid segment shape in a section designer section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing solid segment shape in the specified frame section property.</param>
        /// <param name="nameMaterial">The name of the material property for the shape.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="rotation">The counter clockwise rotation of the shape from its default orientation. [deg].</param>
        /// <param name="angle">The angle between the two radii that define the circular sector. [deg.</param>
        /// <param name="radius">The radius of the circle defining the Sector. [L].</param>
        /// <param name="color">The fill color assigned to the shape.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetSolidSegment(string name,
            string nameShape,
            ref string nameMaterial,
            ref Coordinate2DCartesian centerCoordinate,
            ref double rotation,
            ref double angle,
            ref double radius,
            ref int color)
        {
            double xCoordinate = 0;
            double yCoordinate = 0;

            _callCode = _sapModel.PropFrame.SDShape.GetSolidSegment(name, nameShape, ref nameMaterial, ref color,
                ref xCoordinate, ref yCoordinate, ref angle, ref rotation, ref radius);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            centerCoordinate.X = xCoordinate;
            centerCoordinate.Y = yCoordinate;
        }

        /// <summary>
        /// This function adds a new solid segment shape or modifies an existing shape to be a solid segment shape in a section designer property.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing solid segment shape in the specified frame section property.
        /// If this is an existing shape, that shape is modified; otherwise, a new shape is added.
        /// This item may be input as a blank string, in which case the program will assign a shape name to the shape and return that name in the <paramref name="nameShape" /> variable.</param>
        /// <param name="nameMaterial">The name of the material property for the shape.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="rotation">The counter clockwise rotation of the shape from its default orientation. [deg].</param>
        /// <param name="angle">The angle between the two radii that define the circular sector. [deg.</param>
        /// <param name="radius">The radius of the circle defining the Sector. [L].</param>
        /// <param name="color">The fill color assigned to the shape.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign the default fill color.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetSolidSegment(string name,
            ref string nameShape,
            string nameMaterial,
            Coordinate2DCartesian centerCoordinate,
            double rotation,
            double angle,
            double radius,
            int color = -1)
        {
            double xCoordinate = centerCoordinate.X;
            double yCoordinate = centerCoordinate.Y;

            _callCode = _sapModel.PropFrame.SDShape.SetSolidSegment(name, ref nameShape, nameMaterial,
                xCoordinate, yCoordinate, angle, rotation, radius, color);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function adds a new solid circle shape or modifies an existing shape to be a solid circle shape in a section designer property.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing solid circle shape in the specified frame section property.
        /// If this is an existing shape, that shape is modified; otherwise, a new shape is added.
        /// This item may be input as a blank string, in which case the program will assign a shape name to the shape and return that name in the <paramref name="nameShape" /> variable.</param>
        /// <param name="nameMaterial">The name of the material property for the shape.</param>
        /// <param name="stressStrainOverwrite">This is a blank string, Default, or the name of a defined stress-strain curve.
        /// If this item is a blank string or Default, the shape stress-strain curve is based on the assigned material property.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="rotation">The counter clockwise rotation of the shape from its default orientation. [deg].</param>
        /// <param name="diameter">The diameter of the circle.[L].</param>
        /// <param name="color">The fill color assigned to the shape.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign the default fill color.</param>
        /// <param name="isReinforced">True: There is edge and corner reinforcing steel associated with the shape.
        /// The <paramref name="nameMaterial" /> item must refer to a concrete material for this item to be True.</param>
        /// <param name="nameMaterialRebar">The material property for the edge and corner reinforcing steel associated with the shape.
        /// This item applies only when the <paramref name="nameMaterial" /> item is a concrete material and the <paramref name="isReinforced" /> item is True.</param>
        /// <param name="numberOfBars">The number of equally spaced bars for the circular reinforcing.
        /// This item is visible only if <paramref name="isReinforced" /> = True.</param>
        /// <param name="cover">The clear cover for the specified rebar. [L].
        /// This item is visible only if <paramref name="isReinforced" /> = True.</param>
        /// <param name="barSize">The size of the reinforcing bar.
        /// This item is visible only if <paramref name="isReinforced" /> = True.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetCircle(string name,
            ref string nameShape,
            string nameMaterial,
            string stressStrainOverwrite,
            Coordinate2DCartesian centerCoordinate,
            double rotation = 22.5,
            double diameter = 24,
            int color = -1,
            bool isReinforced = false,
            string nameMaterialRebar = "",
            int numberOfBars = 8,
            double cover = 2,
            string barSize = "")
        {
            double xCoordinate = centerCoordinate.X;
            double yCoordinate = centerCoordinate.Y;

            _callCode = _sapModel.PropFrame.SDShape.SetSolidCircle(name, ref nameShape, nameMaterial, stressStrainOverwrite,
                xCoordinate, yCoordinate, diameter, color,
                isReinforced, numberOfBars, rotation, cover, barSize, nameMaterialRebar);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <summary>
        /// This function retrieves property data for a Pipe shape in a section designer section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing Pipe shape in the specified frame section property.</param>
        /// <param name="nameMaterial">The name of the material property for the shape.</param>
        /// <param name="fileName">This is a blank string or the name of a defined Pipe property that has been imported from a section property file.
        /// If this item is a blank string, the section dimensions are taken from the values input in this function.
        /// If this item is the name of a defined Plate property that has been imported from a section property file, the section dimensions are taken from the specified Pipe property.
        /// If this item is not blank, and the specified property name is not a Pipe that was imported from a section property file, an error is returned.
        /// TODO: Handle this.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="diameter">The diameter of the pipe.[L].</param>
        /// <param name="thickness">The wall thickness of the Pipe. [L].</param>
        /// <param name="color">The fill color assigned to the shape.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetPipe(string name,
            string nameShape,
            ref string nameMaterial,
            ref string fileName,
            ref Coordinate2DCartesian centerCoordinate,
            ref double diameter,
            ref double thickness,
            ref int color)
        {
            double xCoordinate = 0;
            double yCoordinate = 0;

            _callCode = _sapModel.PropFrame.SDShape.GetPipe(name, nameShape, ref nameMaterial, ref fileName, ref color,
                ref xCoordinate, ref yCoordinate, ref diameter, ref thickness);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            centerCoordinate.X = xCoordinate;
            centerCoordinate.Y = yCoordinate;
        }

        /// <summary>
        /// This function adds a new Pipe shape or modifies an existing shape to be a Pipe shape in a section designer property.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing Pipe shape in the specified frame section property.
        /// If this is an existing shape, that shape is modified; otherwise, a new shape is added.
        /// This item may be input as a blank string, in which case the program will assign a shape name to the shape and return that name in the <paramref name="nameShape" /> variable.</param>
        /// <param name="nameMaterial">The name of the material property for the shape.</param>
        /// <param name="fileName">This is a blank string or the name of a defined Pipe property that has been imported from a section property file.
        /// If this item is a blank string, the section dimensions are taken from the values input in this function.
        /// If it is the name of a defined Pipe property, the section dimensions are taken from that property.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="diameter">The diameter of the pipe. [L].</param>
        /// <param name="thickness">The wall thickness of the Pipe. [L].</param>
        /// <param name="color">The fill color assigned to the shape.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign the default fill color.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetPipe(string name,
            ref string nameShape,
            string nameMaterial,
            string fileName,
            Coordinate2DCartesian centerCoordinate,
            double diameter = 24,
            double thickness = 2.4,
            int color = -1)
        {
            double xCoordinate = centerCoordinate.X;
            double yCoordinate = centerCoordinate.Y;

            _callCode = _sapModel.PropFrame.SDShape.SetPipe(name, ref nameShape, nameMaterial, fileName,
                xCoordinate, yCoordinate, color, diameter, thickness);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function adds a new solid rectangle shape or modifies an existing shape to be an solid rectangle shape in a section designer property.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing solid rectangular shape in the specified frame section property.
        /// If this is an existing shape, that shape is modified; otherwise, a new shape is added.
        /// This item may be input as a blank string, in which case the program will assign a shape name to the shape and return that name in the <paramref name="nameShape" /> variable.</param>
        /// <param name="nameMaterial">The name of the material property for the shape.</param>
        /// <param name="stressStrainOverwrite">This is a blank string, Default, or the name of a defined stress-strain curve.
        /// If this item is a blank string or Default, the shape stress-strain curve is based on the assigned material property.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="rotation">The counter clockwise rotation of the shape from its default orientation. [deg].</param>
        /// <param name="h">The section depth. [L].</param>
        /// <param name="w">The section width. [L].</param>
        /// <param name="color">The fill color assigned to the shape.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign the default fill color.</param>
        /// <param name="isReinforced">True: There is edge and corner reinforcing steel associated with the shape.
        /// The <paramref name="nameMaterial" /> item must refer to a concrete material for this item to be True.</param>
        /// <param name="nameMaterialRebar">The material property for the edge and corner reinforcing steel associated with the shape.
        /// This item applies only when the <paramref name="nameMaterial" /> item is a concrete material and the <paramref name="isReinforced" /> item is True.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetRectangle(string name,
            ref string nameShape,
            string nameMaterial,
            string stressStrainOverwrite,
            Coordinate2DCartesian centerCoordinate,
            double rotation,
            double h,
            double w,
            int color = -1,
            bool isReinforced = false,
            string nameMaterialRebar = "")
        {
            double xCoordinate = centerCoordinate.X;
            double yCoordinate = centerCoordinate.Y;

            _callCode = _sapModel.PropFrame.SDShape.SetSolidRect(name, ref nameShape, nameMaterial, stressStrainOverwrite, 
                xCoordinate, yCoordinate, h, w, rotation, color, isReinforced, nameMaterialRebar);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function retrieves property data for a Tube shape in a section designer section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing Tube shape in the specified frame section property.</param>
        /// <param name="nameMaterial">The name of the material property for the shape.</param>
        /// <param name="fileName">This is a blank string or the name of a defined Tube property that has been imported from a section property file.
        /// If it is the name of a defined Tube property, the section dimensions are taken from that property.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="rotation">The counter clockwise rotation of the shape from its default orientation. [deg].</param>
        /// <param name="h">The section height. [L].</param>
        /// <param name="w">The section width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="color">The fill color assigned to the shape.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetTube(string name,
            string nameShape,
            ref string nameMaterial,
            ref string fileName,
            ref Coordinate2DCartesian centerCoordinate,
            ref double rotation,
            ref double h,
            ref double w,
            ref double tf,
            ref double tw,
            ref int color)
        {
            double xCoordinate = 0;
            double yCoordinate = 0;

            _callCode = _sapModel.PropFrame.SDShape.GetTube(name, nameShape, ref nameMaterial, ref fileName, ref color,
                ref xCoordinate, ref yCoordinate, ref h, ref w, ref tf, ref tw, ref rotation);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            centerCoordinate.X = xCoordinate;
            centerCoordinate.Y = yCoordinate;
        }

        /// <summary>
        /// This function adds a new Tube shape or modifies an existing shape to be a Tube shape in a section designer property.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing Plate shape in the specified frame section property.
        /// If this is an existing shape, that shape is modified; otherwise, a new shape is added.
        /// This item may be input as a blank string, in which case the program will assign a shape name to the shape and return that name in the <paramref name="nameShape" /> variable.</param>
        /// <param name="nameMaterial">The name of the material property for the shape.</param>
        /// <param name="fileName">This is a blank string or the name of a defined Tube property that has been imported from a section property file.
        /// If this item is a blank string, the section dimensions are taken from the values input in this function.
        /// If this item is the name of a defined Plate property that has been imported from a section property file, the section dimensions are taken from the specified Plate property.
        /// If this item is not blank, and the specified property name is not a Plate that was imported from a section property file, an error is returned.
        /// TODO: Handle this.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="rotation">The counter clockwise rotation of the shape from its default orientation. [deg].</param>
        /// <param name="h">The section height. [L].</param>
        /// <param name="w">The section width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="color">The fill color assigned to the shape.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign the default fill color.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetTube(string name,
            ref string nameShape,
            string nameMaterial,
            string fileName,
            Coordinate2DCartesian centerCoordinate,
            double rotation,
            double h = 24,
            double w = 24,
            double tf = 2.4,
            double tw = 2.4,
            int color = -1)
        {
            double xCoordinate = centerCoordinate.X;
            double yCoordinate = centerCoordinate.Y;

            _callCode = _sapModel.PropFrame.SDShape.SetTube(name, ref nameShape, nameMaterial, fileName, 
                xCoordinate, yCoordinate, rotation, color, h, w, tf, tw);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion

        #region Methods: Get/Set Sections - Concrete: Reinforcement  
#if !BUILD_ETABS2015
        /// <summary>
        /// This function retrieves property data for a single bar reinforcing shape in a section designer section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of a single reinforcing bar shape in the section designer section.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="barSize">The size of the reinforcing bar.</param>
        /// <param name="nameMaterial">The material property for the reinforcing steel.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetReinforcementSingle(string name,
            string nameShape,
            ref Coordinate2DCartesian centerCoordinate,
            ref string barSize,
            ref string nameMaterial)
        {
            double xCenter = 0;
            double yCenter = 0;

            _callCode = _sapModel.PropFrame.SDShape.GetReinfSingle(name, nameShape,
                ref xCenter, ref yCenter, ref barSize, ref nameMaterial);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            centerCoordinate.X = xCenter;
            centerCoordinate.Y = yCenter;
        }


        /// <summary>
        /// This function retrieves corner point reinforcing data for solid rectangle, circle and polygon shapes in a section designer property.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing solid rectangle, circle or polygon shape in the specified section.</param>
        /// <param name="pointNumbers">The corner point number(s) in the shape.</param>
        /// <param name="barSizes">"None" or the name of a defined rebar, indicating the rebar assignment to the considered corner point.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetReinforcedCorner(string name,
            string nameShape,
            ref int[] pointNumbers,
            ref string[] barSizes)
        {
            _callCode = _sapModel.PropFrame.SDShape.GetReinfCorner(name, nameShape,
                            ref _numberOfItems, ref pointNumbers, ref barSizes);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function retrieves edge reinforcing data for solid rectangle, circle and polygon reinforcing shapes in a section designer property.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing solid rectangle shape in the specified frame section property.</param>
        /// <param name="edgeNumbers">The edge numbers in the shape.</param>
        /// <param name="barSizes">"None" or the name of a defined rebar, indicating the rebar assignment to the considered edge.</param>
        /// <param name="spacing">The rebar maximum center-to-center along the considered edge. [L].</param>
        /// <param name="cover">The rebar clear cover along the considered edge. [L].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetReinforcedEdge(string name,
            string nameShape,
            ref int[] edgeNumbers,
            ref string[] barSizes,
            ref double[] spacing,
            ref double[] cover)
        {
            _callCode = _sapModel.PropFrame.SDShape.GetReinfEdge(name, nameShape,
                            ref _numberOfItems, ref edgeNumbers, ref barSizes, ref spacing, ref cover);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function retrieves property data for a line reinforcing shape in a section designer section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of a line reinforcing shape in the section designer section.</param>
        /// <param name="startCoordinate">Coordinate of the first drawn end point of the line pattern reinforcing. [L].</param>
        /// <param name="endCoordinate">Coordinate of the second drawn end point of the line pattern reinforcing. [L].</param>
        /// <param name="barSize">The size of the reinforcing bars used in the line reinforcing shape.</param>
        /// <param name="barSpacing">The center-to-center spacing of the bars in the line pattern shape. [L].</param>
        /// <param name="endBarsExist">True: There are bars at the end points of the line reinforcing.</param>
        /// <param name="nameMaterial">The material property for the reinforcing steel.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetReinforcedLine(string name,
            string nameShape,
            ref Coordinate2DCartesian startCoordinate,
            ref Coordinate2DCartesian endCoordinate,
            ref string barSize,
            ref double barSpacing,
            ref bool endBarsExist,
            ref string nameMaterial)
        {
            double xStart = 0;
            double yStart = 0;
            double xEnd = 0;
            double yEnd = 0;

            _callCode = _sapModel.PropFrame.SDShape.GetReinfLine(name, nameShape,
                ref xStart, ref yStart, ref xEnd, ref yEnd,
                ref barSpacing, ref barSize, ref endBarsExist, ref nameMaterial);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            startCoordinate.X = xStart;
            startCoordinate.Y = yStart;

            endCoordinate.X = xEnd;
            endCoordinate.Y = yEnd;
        }


        /// <summary>
        /// This function retrieves property data for a circular reinforcing shape in a section designer section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of a circular reinforcing shape in the section designer section.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="barSize">The size of the reinforcing bar.</param>
        /// <param name="rotation">The counter clockwise rotation of the shape from its default orientation. [deg].</param>
        /// <param name="diameter">The diameter of the circular shape. [L].</param>
        /// <param name="numberBars">The number of equally spaced bars for the circular reinforcing.</param>
        /// <param name="nameMaterial">The material property for the reinforcing steel.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetReinforcedCircle(string name,
            string nameShape,
            ref Coordinate2DCartesian centerCoordinate,
            ref string barSize,
            ref double rotation,
            ref double diameter,
            ref int numberBars,
            ref string nameMaterial)
        {
            double xCenter = 0;
            double yCenter = 0;

            _callCode = _sapModel.PropFrame.SDShape.GetReinfCircle(name, nameShape,
                ref xCenter, ref yCenter, ref diameter, ref numberBars, ref rotation, ref barSize, ref nameMaterial);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            centerCoordinate.X = xCenter;
            centerCoordinate.Y = yCenter;
        }


        /// <summary>
        /// This function retrieves property data for a rectangular reinforcing shape in a section designer sec.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of a circular reinforcing shape in the section designer section.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="rotation">The counter clockwise rotation of the shape from its default orientation. [deg].</param>
        /// <param name="h">The section depth. [L].</param>
        /// <param name="w">The section width. [L].</param>
        /// <param name="nameMaterial">The material property for the reinforcing steel.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetReinforcedRectangular(string name,
            string nameShape,
            ref Coordinate2DCartesian centerCoordinate,
            ref double rotation,
            ref double h,
            ref double w,
            ref string nameMaterial)
        {
            double xCenter = 0;
            double yCenter = 0;

            _callCode = _sapModel.PropFrame.SDShape.GetReinfRectangular(name, nameShape,
                ref xCenter, ref yCenter, ref h, ref w,
                ref rotation, ref nameMaterial);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            centerCoordinate.X = xCenter;
            centerCoordinate.Y = yCenter;
        }
#endif
#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// This function retrieves property data for a Tee shape in a section designer section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing Tee shape in the specified frame section property.</param>
        /// <param name="nameMaterial">The name of the material property for the shape.</param>
        /// <param name="fileName">This is a blank string or the name of a defined Tee property that has been imported from a section property file. 
        /// If it is the name of a defined Tee property, the section dimensions are taken from that property.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="rotation">The counter clockwise rotation of the shape from its default orientation. [deg].</param>
        /// <param name="color">The fill color assigned to the shape.</param>
        /// <param name="h">The section depth. [L].</param>
        /// <param name="bf">The section width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="mirrorAbout3">True: Section is mirrored about the local 3-axis.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetReinforcedTee(string name,
            string nameShape,
            ref string nameMaterial,
            ref string fileName,
            ref Coordinate2DCartesian centerCoordinate,
            ref double rotation,
            ref int color,
            ref double h,
            ref double bf,
            ref double tf,
            ref double tw,
            ref bool mirrorAbout3)
        {
            double xCenter = 0;
            double yCenter = 0;

            _callCode = _sapModel.PropFrame.SDShape.GetConcreteTee(name, nameShape, ref nameMaterial, ref fileName,
                ref color, ref xCenter, ref yCenter,
                ref h, ref bf, ref tf, ref tw, ref rotation, ref mirrorAbout3);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            centerCoordinate.X = xCenter;
            centerCoordinate.Y = yCenter;
        }

        /// <summary>
        /// This function retrieves property data for a Tee shape in a section designer section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing Tee shape in the specified frame section property.</param>
        /// <param name="nameMaterial">The name of the material property for the shape.</param>
        /// <param name="fileName">This is a blank string or the name of a defined Tee property that has been imported from a section property file. 
        /// If it is the name of a defined Tee property, the section dimensions are taken from that property.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="rotation">The counter clockwise rotation of the shape from its default orientation. [deg].</param>
        /// <param name="color">The fill color assigned to the shape.</param>
        /// <param name="h">The section depth. [L].</param>
        /// <param name="bf">The section width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="mirrorAbout2">True: Section is mirrored about the local 2-axis.</param>
        /// <param name="mirrorAbout3">True: Section is mirrored about the local 3-axis.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetReinforcedL(string name,
            string nameShape,
            ref string nameMaterial,
            ref string fileName,
            ref Coordinate2DCartesian centerCoordinate,
            ref double rotation,
            ref int color,
            ref double h,
            ref double bf,
            ref double tf,
            ref double tw,
            ref bool mirrorAbout2,
            ref bool mirrorAbout3)
        {
            double xCenter = 0;
            double yCenter = 0;

            _callCode = _sapModel.PropFrame.SDShape.GetConcreteL(name, nameShape, ref nameMaterial, ref fileName,
                ref color, ref xCenter, ref yCenter,
                ref h, ref bf, ref tf, ref tw, ref rotation, ref mirrorAbout2, ref mirrorAbout3);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            centerCoordinate.X = xCenter;
            centerCoordinate.Y = yCenter;
        }
#endif

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function adds a new single bar reinforcing shape or modifies an existing shape to be a single bar reinforcing shape in a section designer property.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing or new shape in a section designer property.
        /// If this is an existing shape, that shape is modified; otherwise, a new shape is added.
        /// This item may be input as a blank string, in which case the program will assign a shape name to the shape and return that name in the <paramref name="nameShape" /> variable.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="barSize">The size of the reinforcing bar.</param>
        /// <param name="nameMaterial">The material property for the reinforcing steel.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetReinforcementSingle(string name,
            ref string nameShape,
            ref Coordinate2DCartesian centerCoordinate,
            string barSize,
            string nameMaterial = "")
        {
            _callCode = _sapModel.PropFrame.SDShape.SetReinfSingle(name, ref nameShape,
                centerCoordinate.X, centerCoordinate.Y, barSize, nameMaterial);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function specifies corner reinforcing in solid rectangle, circle and polygon shapes in a section designer property.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing solid rectangle, circle or polygon shape in the specified section.</param>
        /// <param name="pointNumber">A corner point number in the shape.
        /// This item is ignored if <paramref name="applyRebarToAllCorners" /> = True.</param>
        /// <param name="barSize">This is "None" or the name of a defined rebar, indicating the rebar assignment to the specified corner.</param>
        /// <param name="applyRebarToAllCorners">True: The specified rebar data applies to all corners in the shape.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetReinforcedCorner(string name,
            ref string nameShape,
            int pointNumber,
            string barSize,
            bool applyRebarToAllCorners = false)
        {
            _callCode = _sapModel.PropFrame.SDShape.SetReinfCorner(name, ref nameShape,
                pointNumber, barSize, applyRebarToAllCorners);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function specifies edge reinforcing in solid rectangle, circle, polygon and rectangular reinforcing shapes in a section designer property.
        /// </summary>
        /// <param name="name">The name of an existing solid rectangle, circle or polygon shape in the specified section.</param>
        /// <param name="nameShape">The name of an existing solid rectangle, circle or polygon shape in the specified section.</param>
        /// <param name="edgeNumber">An edge number in the shape. This item is ignored if <paramref name="applyRebarToAllEdges" /> = True.</param>
        /// <param name="barSize">"None" or the name of a defined rebar, indicating the rebar assignment to the considered edge.</param>
        /// <param name="spacing">The rebar maximum center-to-center along the considered edge. [L].</param>
        /// <param name="cover">The rebar clear cover along the considered edge. [L].</param>
        /// <param name="applyRebarToAllEdges">True: The specified rebar data applies to all edges in the shape</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetReinforcedEdge(string name,
            ref string nameShape,
            int edgeNumber,
            string barSize,
            double spacing,
            double cover,
            bool applyRebarToAllEdges = false)
        {
            _callCode = _sapModel.PropFrame.SDShape.SetReinfEdge(name, ref nameShape,
                edgeNumber, barSize, spacing, cover, applyRebarToAllEdges);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function adds a new line reinforcing shape or modifies an existing shape to be a line reinforcing shape in a section designer property.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing or new shape in a section designer property.
        /// If this is an existing shape, that shape is modified; otherwise, a new shape is added.
        /// This item may be input as a blank string, in which case the program will assign a shape name to the shape and return that name in the <paramref name="nameShape" /> variable.</param>
        /// <param name="startCoordinate">Coordinate of the first drawn end point of the line pattern reinforcing. [L].</param>
        /// <param name="endCoordinate">Coordinate of the second drawn end point of the line pattern reinforcing. [L].</param>
        /// <param name="barSize">The size of the reinforcing bars used in the line reinforcing shape.</param>
        /// <param name="barSpacing">The center-to-center spacing of the bars in the line pattern shape. [L].</param>
        /// <param name="endBarsExist">True: There are bars at the end points of the line reinforcing.</param>
        /// <param name="nameMaterial">The material property for the reinforcing steel.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetReinforcedLine(string name,
            ref string nameShape,
            Coordinate2DCartesian startCoordinate,
            Coordinate2DCartesian endCoordinate,
            string barSize,
            double barSpacing,
            bool endBarsExist = false,
            string nameMaterial = "")
        {
            _callCode = _sapModel.PropFrame.SDShape.SetReinfLine(name, ref nameShape,
                startCoordinate.X, startCoordinate.Y, endCoordinate.X, endCoordinate.Y,
                barSpacing, barSize, endBarsExist, nameMaterial);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function adds a new circular reinforcing shape or modifies an existing shape to be an circular reinforcing shape in a section designer property.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing or new shape in a section designer property.
        /// If this is an existing shape, that shape is modified; otherwise, a new shape is added.
        /// This item may be input as a blank string, in which case the program will assign a shape name to the shape and return that name in the <paramref name="nameShape" /> variable.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="barSize">The size of the reinforcing bar.</param>
        /// <param name="rotation">The counter clockwise rotation of the shape from its default orientation. [deg].</param>
        /// <param name="diameter">The diameter of the circular shape. [L].</param>
        /// <param name="numberBars">The number of equally spaced bars for the circular reinforcing.</param>
        /// <param name="nameMaterial">The material property for the reinforcing steel.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetReinforcedCircle(string name,
            ref string nameShape,
            Coordinate2DCartesian centerCoordinate,
            string barSize,
            double rotation,
            double diameter,
            int numberBars,
            string nameMaterial = "")
        {
            _callCode = _sapModel.PropFrame.SDShape.SetReinfCircle(name, ref nameShape,
                centerCoordinate.X, centerCoordinate.Y, diameter, numberBars, rotation, barSize, nameMaterial);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function adds a new rectangular reinforcing shape or modifies an existing shape to be a rectangular reinforcing shape in a section designer property.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing or new shape in a section designer property.
        /// If this is an existing shape, that shape is modified; otherwise, a new shape is added.
        /// This item may be input as a blank string, in which case the program will assign a shape name to the shape and return that name in the <paramref name="nameShape" /> variable.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="rotation">The counter clockwise rotation of the shape from its default orientation. [deg].</param>
        /// <param name="h">The section depth. [L].</param>
        /// <param name="w">The section width. [L].</param>
        /// <param name="nameMaterial">The material property for the reinforcing steel.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetReinforcedRectangular(string name,
            ref string nameShape,
            Coordinate2DCartesian centerCoordinate,
            double rotation,
            double h,
            double w,
            string nameMaterial = "")
        {
            _callCode = _sapModel.PropFrame.SDShape.SetReinfRectangular(name, ref nameShape,
                centerCoordinate.X, centerCoordinate.Y, h, w,
                rotation, nameMaterial);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        #region Methods: Get/Set Sections - Reference        
        /// <summary>
        /// This function retrieves property data for a reference circle shape in a section designer section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of a reference circle shape in the section designer section</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="diameter">The diameter of the circular shape. [L].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetReferenceCircle(string name,
            string nameShape,
            ref Coordinate2DCartesian centerCoordinate,
            ref double diameter)
        {
            double xCenter = 0;
            double yCenter = 0;

            _callCode = _sapModel.PropFrame.SDShape.GetRefCircle(name, nameShape,
                ref xCenter, ref yCenter, ref diameter);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            centerCoordinate.X = xCenter;
            centerCoordinate.Y = yCenter;
        }

        /// <summary>
        /// This function adds a new reference circle shape or modifies an existing shape to be a reference circle shape in a section designer property.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing or new shape in a section designer property.
        /// If this is an existing shape, that shape is modified; otherwise, a new shape is added.
        /// This item may be input as a blank string, in which case the program will assign a shape name to the shape and return that name in the <paramref name="nameShape" /> variable.</param>
        /// <param name="centerCoordinate">The coordinate of the center of the shape in the section designer coordinate system. [L].</param>
        /// <param name="diameter">The diameter of the circular shape. [L].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetReferenceCircle(string name,
            ref string nameShape,
            Coordinate2DCartesian centerCoordinate,
            double diameter)
        {
            _callCode = _sapModel.PropFrame.SDShape.SetRefCircle(name, ref nameShape,
                centerCoordinate.X, centerCoordinate.Y, diameter);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <summary>
        /// This function retrieves property data for a reference line shape in a section designer section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of a reference line shape in the section designer section</param>
        /// <param name="startCoordinate">Coordinate of the first drawn end point of the reference line. [L].</param>
        /// <param name="endCoordinate">Coordinate of the second drawn end point of the reference line. [L].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetReferenceLine(string name,
            string nameShape,
            ref Coordinate2DCartesian startCoordinate,
            ref Coordinate2DCartesian endCoordinate)
        {
            double xStart = 0;
            double yStart = 0;
            double xEnd = 0;
            double yEnd = 0;

            _callCode = _sapModel.PropFrame.SDShape.GetRefLine(name, nameShape,
                ref xStart, ref yStart, ref xEnd, ref yEnd);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            startCoordinate.X = xStart;
            startCoordinate.Y = yStart;

            endCoordinate.X = xEnd;
            endCoordinate.Y = yEnd;
        }

        /// <summary>
        /// This function adds a new reference line shape or modifies an existing shape to be a reference line shape in a section designer property.
        /// </summary>
        /// <param name="name">The name of an existing frame section property that is a section designer section.</param>
        /// <param name="nameShape">The name of an existing or new shape in a section designer property.
        /// If this is an existing shape, that shape is modified; otherwise, a new shape is added.
        /// This item may be input as a blank string, in which case the program will assign a shape name to the shape and return that name in the <paramref name="nameShape" /> variable.</param>
        /// <param name="startCoordinate">Coordinate of the first drawn end point of the reference line. [L].</param>
        /// <param name="endCoordinate">Coordinate of the second drawn end point of the reference line. [L].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetReferenceLine(string name,
            ref string nameShape,
            Coordinate2DCartesian startCoordinate,
            Coordinate2DCartesian endCoordinate)
        {
            _callCode = _sapModel.PropFrame.SDShape.SetRefLine(name, ref nameShape,
                startCoordinate.X, startCoordinate.Y, endCoordinate.X, endCoordinate.Y);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
#endif
    }
}