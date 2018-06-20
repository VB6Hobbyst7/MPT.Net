// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-15-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-11-2017
// ***********************************************************************
// <copyright file="IFrameObject.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;

namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
    /// <summary>
    /// Represents the Frame Object in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILabel" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ISpringAssignment" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IPier" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ISpandrel" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IColumnSpliceOverwrite" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IAddableObject" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IGroupAssignable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ISelectable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IObservableLocalAxes" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeableLocalAxes" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IGUID" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IMass" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IDeletableMass" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IOutputStations" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IFrameInsertionPoint" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IEndLengthOffset" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IObservableHinges" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletableSpring" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IDesignProcedure" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILateralBracing" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableTransformationMatrix" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservablePoints" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableElement" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IObservableElementFrame" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableSection" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeableSection" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IFrameSection" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableReleases" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableReleases" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IDeletableModifiers" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableMaterialOverwrite" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeableMaterialOverwrite" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableTensionCompressionLimits" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeableTensionCompressionLimits" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadTemperature" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadDistributed" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadPoint" />
#else
    /// <summary>
    /// Represents the Frame Object in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IAddableObject" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IGroupAssignable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ISelectable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IObservableLocalAxes" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeableLocalAxes" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IGUID" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IMass" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IDeletableMass" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IOutputStations" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IFrameInsertionPoint" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IEndLengthOffset" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IObservableHinges" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletableSpring" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILocalAxesAdvanced" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.INotionalSize" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IObservableDirectAnalysisModifiers" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IFrameAutoMesh" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IFrameCurved" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IFrameEndSkew" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IFrameSectionTrapezoidal" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IFireProofing" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ILineSpring" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IDesignProcedure" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILateralBracing" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableTransformationMatrix" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservablePoints" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableElement" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IObservableElementFrame" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableSection" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeableSection" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IFrameSection" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableReleases" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableReleases" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableFrameModifiers" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableFrameModifiers" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IDeletableModifiers" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableMaterialOverwrite" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeableMaterialOverwrite" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableTensionCompressionLimits" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeableTensionCompressionLimits" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableMaterialTemperature" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeableMaterialTemperature" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservablePDeltaForces" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeablePDeltaForces" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletablePDeltaForces" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadGravity" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadDeformation" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadTargetForce" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadStrain" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadDistributedWithGUID" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadPointWithGUID" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadTransfer" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadTemperature" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadDistributed" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadPoint" />
#endif
    public interface IFrameObject:
        IAddableObject,
        ICountable, IListableNames, IChangeableName, IGroupAssignable, ISelectable, IDeletable,
        IObservableLocalAxes, IChangeableLocalAxes,
        IGUID, IMass, IDeletableMass, 
        IOutputStations, IFrameInsertionPoint, IEndLengthOffset, 
        IObservableHinges, // How is this set?
        IDeletableSpring,
#if BUILD_ETABS2016 || BUILD_ETABS2017
        ISpringAssignment,
        IColumnSpliceOverwrite,
#endif
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        ILabel, 
        IPier, ISpandrel,
#else
        ILocalAxesAdvanced,
        INotionalSize, 

        IObservableDirectAnalysisModifiers, // How is this set?

        IFrameAutoMesh, IFrameCurved, IFrameEndSkew, IFrameSectionTrapezoidal,
        IFireProofing, 
        ILineSpring,
#endif
        IDesignProcedure, ILateralBracing, 

        IObservableTransformationMatrix, IObservableTransformationMatrixObject,
        IObservablePoints, IObservableElement, IObservableElementFrame,
        
        IObservableSection, IChangeableSection, IFrameSection, 
        IObservableReleases, IChangeableReleases,
        
        IObservableFrameModifiers, IChangeableFrameModifiers, IDeletableModifiers,
        IObservableMaterialOverwrite, IChangeableMaterialOverwrite,
        IObservableTensionCompressionLimits, IChangeableTensionCompressionLimits,
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        IObservableMaterialTemperature, IChangeableMaterialTemperature,
        IObservablePDeltaForces, IChangeablePDeltaForces, IDeletablePDeltaForces,
#endif


        // Loads
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        ILoadGravity, 
        ILoadDeformation, 
        ILoadTargetForce, 
        
        ILoadStrain, 

        ILoadDistributedWithGUID,
        ILoadPointWithGUID,

        ILoadTransfer,
#endif

        ILoadTemperature,

        ILoadDistributed,        
        ILoadPoint
    {
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the design orientation of a frame object.
        /// </summary>
        /// <param name="name">The name of a defined frame object.</param>
        /// <param name="designOrientation">The design orientation.</param>
        void GetDesignOrientation(string name,
            ref eFrameDesignOrientation designOrientation);

        /// <summary>
        /// Retrieves select data for all area objects in the model .
        /// </summary>
        /// <param name="frameNames">The name of each frame.</param>
        /// <param name="sectionNames">The names of the sections assigned to each frame.</param>
        /// <param name="storyNames">The story name associated with each frame</param>
        /// <param name="pointINames">Point I associated with each frame</param>
        /// <param name="pointJNames">Point J associated with each frame</param>
        /// <param name="pointICoordinates">Coordinates for point I of each frame. [L].</param>
        /// <param name="pointJCoordinates">Coordinates for point J of each frame. [L].</param>
        /// <param name="angles">Angle of rotation of the local axis about the local-1 axis. [deg].</param>
        /// <param name="pointIOffsets">Three joint offset distances for point I of each frame. [L].</param>
        /// <param name="pointJOffsets">Three joint offset distances for point J of each frame. [L].</param>
        /// <param name="cardinalInsertionPoints">The cardinal point specifies the relative position of the frame section on the line representing the frame object.</param>
        /// <param name="coordinateSystem">The name of the coordinate system in which the coordinates are returned.</param>
        void GetAllFrames(ref string[] frameNames,
            ref string[] sectionNames,
            ref string[] storyNames,
            ref string[] pointINames,
            ref string[] pointJNames,
            ref Coordinate3DCartesian[] pointICoordinates,
            ref Coordinate3DCartesian[] pointJCoordinates,
            ref double[] angles,
            ref Displacements[] pointIOffsets,
            ref Displacements[] pointJOffsets,
            ref eCardinalInsertionPoint[] cardinalInsertionPoints,
            string coordinateSystem = CoordinateSystems.Global);


        /// <summary>
        /// Retrieves support data for a given frame beam object.
        /// </summary>
        /// <param name="name">The name of an existing frame beam object.</param>
        /// <param name="supportName1">The name of the column frame object, beam frame object or wall area object which supports the beam at its start node.</param>
        /// <param name="supportType1">The type of support at the start node.</param>
        /// <param name="supportName2">The name of the column frame object, beam frame object or wall area object which supports the beam at its end node.</param>
        /// <param name="supportType2">The type of support at the end node.</param>
        void GetSupports(string name,
            ref string supportName1,
            ref eSupportType supportType1,
            ref string supportName2,
            ref eSupportType supportType2);
#endif
    }
}
