// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 06-15-2017
//
// Last Modified By : Mark
// Last Modified On : 09-28-2017
// ***********************************************************************
// <copyright file="ISolidElement.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel
{
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
    /// <summary>
    /// Represents the Solid Element in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableTransformationMatrix" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel.IObservableLocalAxes" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableObject" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservablePoints" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableSection" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableMaterialTemperature" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel.IObservableLoadGravity" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel.IObservableLoadPorePressure" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel.IObservableLoadStrain" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel.IObservableLoadSurfacePressure" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.AnalysisModel.IObservableLoadTemperature" />
    public interface ISolidElement :
        ICountable, IListableNames, IObservableTransformationMatrix, IObservableLocalAxes, IObservableObject, IObservablePoints,
        IObservableSection, IObservableMaterialTemperature,
        
        IObservableLoadGravity, 
        IObservableLoadPorePressure, 
        IObservableLoadStrain, 
        IObservableLoadSurfacePressure, 
        IObservableLoadTemperature
    {
    }
#endif
}
