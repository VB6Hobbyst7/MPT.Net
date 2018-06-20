// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-19-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-28-2017
// ***********************************************************************
// <copyright file="ITimeHistoryDirectNonlinear.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
    /// <summary>
    /// Represents the nonlinear time history load case in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.ITimeHistoryDirectLinear" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.IMassSource" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.IGeometricNonlinearity" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.INLTHSolutionControlParameters" />
    public interface ITimeHistoryDirectNonlinear:
        ITimeHistoryDirectLinear, IMassSource, IGeometricNonlinearity, INLTHSolutionControlParameters
    {
        
    }
#else
    /// <summary>
    /// Represents the nonlinear time history load case in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.ITimeHistoryDirectLinear" />
    public interface ITimeHistoryDirectNonlinear :
        ITimeHistoryDirectLinear
    {

    }
#endif
}
