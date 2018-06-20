// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-19-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-28-2017
// ***********************************************************************
// <copyright file="ITimeIntegration.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Load case uses time step integration procedures.
    /// </summary>
    public interface ITimeIntegration
    {
        /// <summary>
        /// This function retrieves the time integration data assigned to the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing integration time history load case.</param>
        /// <param name="integrationType">The time integration type.</param>
        /// <param name="alpha">The alphafactor (-1/3 &lt;= <paramref name="alpha" /> &lt;= 0).
        /// This item applies only when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.HilberHughesTaylor" /> or <see cref="eTimeIntegrationType.ChungHulbert" />.</param>
        /// <param name="beta">The beta factor (<paramref name="beta" /> &gt;= 0.5).
        /// This item applies only when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.Newmark" />, <see cref="eTimeIntegrationType.Collocation" /> or <see cref="eTimeIntegrationType.ChungHulbert" />.
        /// It is returned for informational purposes when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.HilberHughesTaylor" />.</param>
        /// <param name="gamma">The gamma factor (<paramref name="gamma" /> &gt;= 0.5).
        /// This item applies only when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.Newmark" />, <see cref="eTimeIntegrationType.Collocation" /> or <see cref="eTimeIntegrationType.ChungHulbert" />.
        /// It is returned for informational purposes when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.HilberHughesTaylor" />.</param>
        /// <param name="theta">The theta factor (<paramref name="theta" /> &gt; 0).
        /// This item applies only when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.Wilson" /> or <see cref="eTimeIntegrationType.Collocation" />.</param>
        /// <param name="alphaM">The alpha-m factor.
        /// This item only applies when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.ChungHulbert" />.</param>
        void GetTimeIntegration(string name,
            ref eTimeIntegrationType integrationType,
            ref double alpha,
            ref double beta,
            ref double gamma,
            ref double theta,
            ref double alphaM);


        /// <summary>
        /// This function sets time integration data for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing integration time history load case.</param>
        /// <param name="integrationType">The time integration type.</param>
        /// <param name="alpha">The alphafactor (-1/3 &lt;= <paramref name="alpha" /> &lt;= 0).
        /// This item applies only when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.HilberHughesTaylor" /> or <see cref="eTimeIntegrationType.ChungHulbert" />.</param>
        /// <param name="beta">The beta factor (<paramref name="beta" /> &gt;= 0.5).
        /// This item applies only when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.Newmark" />, <see cref="eTimeIntegrationType.Collocation" /> or <see cref="eTimeIntegrationType.ChungHulbert" />.
        /// It is returned for informational purposes when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.HilberHughesTaylor" />.</param>
        /// <param name="gamma">The gamma factor (<paramref name="gamma" /> &gt;= 0.5).
        /// This item applies only when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.Newmark" />, <see cref="eTimeIntegrationType.Collocation" /> or <see cref="eTimeIntegrationType.ChungHulbert" />.
        /// It is returned for informational purposes when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.HilberHughesTaylor" />.</param>
        /// <param name="theta">The theta factor (<paramref name="theta" /> &gt; 0).
        /// This item applies only when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.Wilson" /> or <see cref="eTimeIntegrationType.Collocation" />.</param>
        /// <param name="alphaM">The alpha-m factor.
        /// This item only applies when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.ChungHulbert" />.</param>
        void SetTimeIntegration(string name,
            eTimeIntegrationType integrationType,
            double alpha,
            double beta,
            double gamma,
            double theta,
            double alphaM);
    }
}
#endif