// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-20-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="ITendonObject.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
#endif

namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
    /// <summary>
    /// Represents the Tendon Object in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IAddableObject" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IGroupAssignable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IDiscretizable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IGroupLoadable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IObservableLocalAxes" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IGUID" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableTransformationMatrix" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservablePoints" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableElement" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableSection" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeableSection" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableMaterialTemperature" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeableMaterialTemperature" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableTensionCompressionLimits" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeableTensionCompressionLimits" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadGravity" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadDeformationUniaxial" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadStrainUniaxial" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadTemperatureConstant" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadForceStress" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ISelectable" />
#else
    /// <summary>
    /// Represents the Tendon Object in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IAddableObject" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IGroupAssignable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IObservableLocalAxes" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IGUID" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableTransformationMatrix" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservablePoints" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableElement" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableSection" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeableSection" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableTensionCompressionLimits" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeableTensionCompressionLimits" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ISelectable" />
#endif
    public interface ITendonObject:
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        IAddableObject,
        ICountable, IChangeableName, IGroupAssignable, 

        IDeletable, IDiscretizable, IGroupLoadable,

        IObservableLocalAxes, IGUID,
        
        IObservableTransformationMatrix, IObservableTransformationMatrixObject,
        IObservablePoints, IObservableElement, IObservableElementFrame,


        IObservableSection, IChangeableSection, 
        IObservableMaterialTemperature, IChangeableMaterialTemperature,
        IObservableTensionCompressionLimits, IChangeableTensionCompressionLimits,

        // Loads
        ILoadGravity,
        ILoadDeformationUniaxial,

        ILoadStrainUniaxial, 
        ILoadTemperatureConstant, 
        
        ILoadForceStress,
#endif
        IListableNames, ISelectable
    {
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function assigns the tendon geometric definition parameters to a tendon object.
        /// </summary>
        /// <param name="name">The name of a defined tendon object.</param>
        /// <param name="numberPoints">The number of points defining the tendon geometry.</param>
        /// <param name="tendonGeometryDefinitions">The tendon geometry definition parameter for the specified point.
        /// The first point should always have a MyType value of 1. If it is not equal to 1, the program uses 1 anyway.
        /// MyType of 6 through 9 is based on using three points to calculate a parabolic or circular arc.
        /// MyType 6 and 8 use the specified point and the two previous points as the three points.
        /// MyType 7 and 9 use the specified point and the points just before and after the specified point as the three points.</param>
        /// <param name="coordinates">Coordinates of the considered point on the tendon in the coordinate system specified by the <paramref name="coordinateSystem" /> item. [L]</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the x, y and z coordinates are to be reported.
        /// It is Local or the name of a defined coordinate system.</param>
        void GetTendonData(string name,
            ref int numberPoints,
            ref eTendonGeometryDefinition[] tendonGeometryDefinitions,
            ref Coordinate3DCartesian[] coordinates,
            string coordinateSystem = CoordinateSystems.Global);

        /// <summary>
        /// This function assigns the tendon geometric definition parameters to a tendon object.
        /// </summary>
        /// <param name="name">The name of a defined tendon object.</param>
        /// <param name="numberPoints">The number of points defining the tendon geometry.</param>
        /// <param name="tendonGeometryDefinitions">The tendon geometry definition parameter for the specified point.
        /// The first point should always have a MyType value of 1. If it is not equal to 1, the program uses 1 anyway.
        /// MyType of 6 through 9 is based on using three points to calculate a parabolic or circular arc.
        /// MyType 6 and 8 use the specified point and the two previous points as the three points.
        /// MyType 7 and 9 use the specified point and the points just before and after the specified point as the three points.</param>
        /// <param name="coordinates">Coordinates of the considered point on the tendon in the coordinate system specified by the <paramref name="coordinateSystem" /> item. [L]</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the x, y and z coordinates are to be reported.
        /// It is Local or the name of a defined coordinate system.</param>
        void SetTendonData(string name,
            int numberPoints,
            eTendonGeometryDefinition[] tendonGeometryDefinitions,
            Coordinate3DCartesian[] coordinates,
            string coordinateSystem = CoordinateSystems.Global);




        /// <summary>
        /// This function retrieves geometric data for a specified tendon object.
        /// </summary>
        /// <param name="name">The name of a defined tendon object.</param>
        /// <param name="numberPoints">The number of points defining the tendon geometry.</param>
        /// <param name="coordinates">Coordinates of the considered point on the tendon in the coordinate system specified by the <paramref name="coordinateSystem" /> item. [L]</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the x, y and z coordinates are to be reported.
        /// It is Local or the name of a defined coordinate system.</param>
        void GetTendonGeometry(string name,
            ref int numberPoints,
            ref Coordinate3DCartesian[] coordinates,
            string coordinateSystem = CoordinateSystems.Global);
#endif
    }
}
