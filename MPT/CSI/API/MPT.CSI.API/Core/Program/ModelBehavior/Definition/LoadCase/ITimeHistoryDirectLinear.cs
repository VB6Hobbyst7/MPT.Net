// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-19-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-28-2017
// ***********************************************************************
// <copyright file="ITimeHistoryDirectLinear.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
    /// <summary>
    /// Represents the linear time history load case in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.ISetLoadCase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.IInitialLoadCase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.ILoadTimeHistory" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.IDampingProportional" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.ITimeStep" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.ITimeIntegration" />
    public interface ITimeHistoryDirectLinear :
        ISetLoadCase, IInitialLoadCase, ILoadTimeHistory, IDampingProportional, ITimeStep, ITimeIntegration
    {
    }
#else
    /// <summary>
    /// Represents the linear time history load case in the application.
    /// </summary>
    public interface ITimeHistoryDirectLinear :
        ILoadTimeHistory
    {
    }
#endif

}
