// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-15-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-11-2017
// ***********************************************************************
// <copyright file="IPointObject.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
    /// <summary>
    /// Represents the Point Object in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILocalAxesAdvancedWithPoints" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableMerge" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeableMerge" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservablePattern" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeablePattern" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IDeletablePattern" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IConstraint" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IAddableCoordinate" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadForceWithGUID" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IGroupAssignable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ISelectable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IObservableLocalAxes" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IGUID" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IMassLumped" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableTransformationMatrix" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableElement" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableConnectivity" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IObservableCommonTo" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableCoordinates" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IPanelZone" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ISpecialPoint" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableRestraint" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeableRestraint" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IDeletableRestraint" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservablePointSpring" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeablePointSpring" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletableSpring" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ICountablePanelZone" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadForce" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountableLoadForce" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadDisplacement" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountableLoadDisplacement" />
#else
    /// <summary>
    /// Represents the Point Object in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IGroupAssignable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ISelectable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IObservableLocalAxes" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IGUID" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IMassLumped" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableTransformationMatrix" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableElement" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableConnectivity" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IObservableCommonTo" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableCoordinates" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IPanelZone" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ISpecialPoint" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableRestraint" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeableRestraint" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IDeletableRestraint" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservablePointSpring" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeablePointSpring" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletableSpring" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ICountablePanelZone" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadForce" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountableLoadForce" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadDisplacement" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountableLoadDisplacement" />
#endif
    public interface IPointObject:
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        ILocalAxesAdvancedWithPoints,

        IObservableMerge, IChangeableMerge,
        IObservablePattern, IChangeablePattern, IDeletablePattern,

        IConstraint, 
        IAddableCoordinate,

        ILoadForceWithGUID, 
#endif
#if BUILD_ETABS2016 || BUILD_ETABS2017
        ILabel,
        IDiaphragmPoints,
        ISpringAssignment,
#endif
        ICountable, IListableNames, IChangeableName, IGroupAssignable, ISelectable, IDeletable,
        IObservableLocalAxes,  IGUID, IMassLumped,
        IObservableTransformationMatrix, IObservableElement,
        IObservableConnectivity, IObservableCommonTo,

       
        IObservableCoordinates, 
        
        IPanelZone, ISpecialPoint,
        IObservableRestraint, IChangeableRestraint, IDeletableRestraint,
        IObservablePointSpring, IChangeablePointSpring, IDeletableSpring,
        ICountablePanelZone,

        // Loads
        ILoadForce, ICountableLoadForce,
        ILoadDisplacement, ICountableLoadDisplacement
    {
    }
}
