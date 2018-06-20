// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-15-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-01-2017
// ***********************************************************************
// <copyright file="ILinkObject.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
    /// <summary>
    /// Represents the Link Object in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableSectionFrequencyDependent" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeableSectionFrequencyDependent" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadGravity" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadDeformation" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadTargetForce" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IAddableObject" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IAddableLinkObject" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IGroupAssignable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ISelectable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IObservableLocalAxes" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeableLocalAxes" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILocalAxesAdvancedWithPoints" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IGUID" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableTransformationMatrix" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservablePoints" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableElement" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableSection" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeableSection" />
#else
    /// <summary>
    /// Represents the Link Object in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IAddableObject" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IAddableLinkObject" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IGroupAssignable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ISelectable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IObservableLocalAxes" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeableLocalAxes" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILocalAxesAdvancedWithPoints" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IGUID" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableTransformationMatrix" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservablePoints" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableElement" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableSection" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeableSection" />
#endif
    public interface ILinkObject:
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        IObservableSectionFrequencyDependent, IChangeableSectionFrequencyDependent,

         // Loads
        ILoadGravity, 
        ILoadDeformation, 
        ILoadTargetForce,
#endif
        IAddableObject, IAddableLinkObject,
        ICountable, IListableNames, IChangeableName, IGroupAssignable, ISelectable, IDeletable,
        IObservableLocalAxes, IChangeableLocalAxes,
        ILocalAxesAdvancedWithPoints, IGUID,

        IObservableTransformationMatrix, IObservableTransformationMatrixObject,
        IObservablePoints, IObservableElement,
        
        IObservableSection, IChangeableSection
    {
       
    }
}
