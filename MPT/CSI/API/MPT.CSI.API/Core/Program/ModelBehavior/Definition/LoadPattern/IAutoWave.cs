// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="IAutoWave.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wave;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern
{
    /// <summary>
    /// Implements the auto wave load pattern in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.IAutoLoad" />
    public interface IAutoWave : IAutoLoad
    {
        /// <summary>
        /// Auto wave load pattern according to generic definitions.
        /// </summary>
        /// <value>The wave load generic.</value>
        WaveLoadGeneric WaveLoadGeneric { get; }

        /// <summary>
        /// Auto wave load pattern.
        /// </summary>
        /// <value>The automatic wave load.</value>
        AutoWaveLoad AutoWaveLoad { get; }

        /// <summary>
        /// Sets the auto wave load code used by the class.
        /// </summary>
        /// <param name="autoWaveLoad">The auto wave load for the class to use.</param>
        void SetAutoCode(AutoWaveLoad autoWaveLoad);
        
    }
}
#endif