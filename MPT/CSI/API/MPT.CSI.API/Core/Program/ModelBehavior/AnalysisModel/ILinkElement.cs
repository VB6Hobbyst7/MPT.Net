// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 06-15-2017
//
// Last Modified By : Mark
// Last Modified On : 09-28-2017
// ***********************************************************************
// <copyright file="ILinkElement.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel
{
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
    /// <summary>
    /// Represents the Link Element in the application.
    /// </summary>
    public interface ILinkElement
#endif
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
    /// <summary>
    /// Represents the Link Element in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableTransformationMatrix" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel.IObservableLocalAxes" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableObject" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservablePoints" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableSection" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableSectionFrequencyDependent" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel.IObservableLoadDeformation" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel.IObservableLoadGravity" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel.IObservableLoadTargetForce" />
    public interface ILinkElement:
        ICountable, IListableNames, IObservableTransformationMatrix, IObservableLocalAxes, IObservableObject, IObservablePoints,
        IObservableSection, IObservableSectionFrequencyDependent,
        
        IObservableLoadDeformation, 
        IObservableLoadGravity, 
        IObservableLoadTargetForce
#endif
    {

    }
}
