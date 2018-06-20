// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 06-15-2017
//
// Last Modified By : Mark
// Last Modified On : 09-28-2017
// ***********************************************************************
// <copyright file="IPointElement.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel
{
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
    /// <summary>
    /// Represents the Point Element in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableTransformationMatrix" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel.IObservableLocalAxes" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableObject" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableCoordinates" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableConnectivity" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservablePattern" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableMerge" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel.IObservableConstraint" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableRestraint" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservablePointSpring" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel.IObservableLoadForce" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountableLoadForce" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel.IObservableLoadDisplacement" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountableLoadDisplacement" />
#else
    /// <summary>
    /// Represents the Point Element in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableTransformationMatrix" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel.IObservableLocalAxes" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableObject" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableCoordinates" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableConnectivity" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservablePattern" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel.IObservableConstraint" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableRestraint" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservablePointSpring" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel.IObservableLoadForce" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountableLoadForce" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel.IObservableLoadDisplacement" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountableLoadDisplacement" />
#endif
    public interface IPointElement:
        ICountable, IListableNames, IObservableTransformationMatrix, IObservableLocalAxes, IObservableObject,
        IObservableCoordinates, IObservableConnectivity, IObservablePattern,
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        IObservableMerge, 
#endif
        IObservableConstraint, 
        IObservableRestraint, 
        IObservablePointSpring,
        
        // Loads
        IObservableLoadForce, ICountableLoadForce,
        IObservableLoadDisplacement, ICountableLoadDisplacement
    {
    }
}
