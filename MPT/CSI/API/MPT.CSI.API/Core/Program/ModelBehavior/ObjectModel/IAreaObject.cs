// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-15-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="IAreaObject.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
using MPT.CSI.API.Core.Helpers;
#endif


namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
    /// <summary>
    /// Represents the Area Object in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IAddableObject" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IGroupAssignable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ISelectable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ISelectableEdge" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IObservableLocalAxes" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeableLocalAxes" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IGUID" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IMass" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IDeletableMass" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IEdgeConstraints" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletableSpring" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILocalAxesAdvanced" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IAreaAutoMesh" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.INotionalSize" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ISurfaceSpring" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableTransformationMatrix" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservablePoints" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableElement" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableAreaModifiers" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableAreaModifiers" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IDeletableModifiers" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableMaterialOverwrite" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeableMaterialOverwrite" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableSection" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeableSection" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableAreaOffset" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeableAreaOffset" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableThickness" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeableThickness" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableMaterialTemperature" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeableMaterialTemperature" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadGravity" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadPorePressure" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadStrain" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadSurfacePressure" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadRotate" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadTemperature" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadUniform" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadUniformToFrame" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IDeletableLoadUniform" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadWindPressure" />
#else
    /// <summary>
    /// Represents the Area Object in the application.
    /// </summary>
#endif
    public interface IAreaObject :
        IAddableObject,
        ICountable, IListableNames, IChangeableName, IGroupAssignable, ISelectable, ISelectableEdge, IDeletable,
        IObservableLocalAxes, IChangeableLocalAxes,
        IGUID, IMass, IDeletableMass,  IEdgeConstraints,
        IDeletableSpring,
#if BUILD_ETABS2016 || BUILD_ETABS2017
        ISpringAssignment,
#endif  
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        ILabel,
        IDiaphragmAreas,
        IPier, ISpandrel, IRebarDataPierSpandrel,
        IOpening,
#else
        ILocalAxesAdvanced,
        IAreaAutoMesh, INotionalSize,
        ISurfaceSpring, 
#endif
        IObservableTransformationMatrix, IObservableTransformationMatrixObject,
        IObservablePoints, IObservableElement,
        IObservableAreaModifiers, IChangeableAreaModifiers, IDeletableModifiers,
        IObservableMaterialOverwrite, IChangeableMaterialOverwrite,

        IObservableSection, IChangeableSection,
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        IObservableAreaOffset, IChangeableAreaOffset,
        IObservableThickness, IChangeableThickness,
        IObservableMaterialTemperature, IChangeableMaterialTemperature,
#endif


        // Loads
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        ILoadGravity,
        ILoadPorePressure, 
        ILoadStrain,
        ILoadSurfacePressure,
        
        ILoadRotate,
#endif
        ILoadTemperature,

        ILoadUniform,
        ILoadUniformToFrame,
        IDeletableLoadUniform,

        ILoadWindPressure
    {
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the design orientation of an area object.
        /// </summary>
        /// <param name="name">The name of a defined area object.</param>
        /// <param name="designOrientation">The design orientation.</param>
        void GetDesignOrientation(string name,
            ref eAreaDesignOrientation designOrientation);

        /// <summary>
        /// Retrieves select data for all area objects in the model .
        /// </summary>
        /// <param name="areaNames">The area names.</param>
        /// <param name="designOrientations">The design orientations.</param>
        /// <param name="numberOfBoundaryPts">The number of boundary points.</param>
        /// <param name="pointDelimiters">The point delimiters.</param>
        /// <param name="pointNames">The point names for each area.</param>
        /// <param name="coordinates">The coordinates for each point for each area. [L].</param>
        void GetAllAreas(ref string[] areaNames,
            ref eAreaDesignOrientation[] designOrientations,
            ref int numberOfBoundaryPts,
            ref int[] pointDelimiters,
            ref string[] pointNames,
            ref Coordinate3DCartesian[] coordinates);
#endif
    }
}
