// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-20-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 09-28-2017
// ***********************************************************************
// <copyright file="IModalCombination.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Interface IModalCombination
    /// </summary>
    public interface IModalCombination
    {
        /// <summary>
        /// This function retrieves the modal combination option assigned to the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing load case.</param>
        /// <param name="modalCombination">The modal combination option.</param>
        /// <param name="gmcF1">The GMC f1 factor [cyc/s].
        /// This item does not apply when <paramref name="modalCombination" /> = <see cref="eModalCombination.ABS" />.</param>
        /// <param name="gmcF2">The GMC f2 factor [cyc/s].
        /// This item does not apply when <paramref name="modalCombination" /> = <see cref="eModalCombination.ABS" />.</param>
        /// <param name="periodicPlusRigidModalCombination">The periodic plus rigid modal combination option.</param>
        /// <param name="td">The factor td [s].
        /// This item applies only when <paramref name="modalCombination" /> = <see cref="eModalCombination.DoubleSum" />.</param>
        void GetModalCombination(string name,
            ref eModalCombination modalCombination,
            ref double gmcF1,
            ref double gmcF2,
            ref ePeriodicPlusRigidModalCombination periodicPlusRigidModalCombination,
            ref double td);
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function retrieves the modal combination option assigned to the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing load case.</param>
        /// <param name="modalCombination">The modal combination option.</param>
        /// <param name="gmcF1">The GMC f1 factor [cyc/s].
        /// This item does not apply when <paramref name="modalCombination" /> = <see cref="eModalCombination.ABS" />.</param>
        /// <param name="gmcF2">The GMC f2 factor [cyc/s].
        /// This item does not apply when <paramref name="modalCombination" /> = <see cref="eModalCombination.ABS" />.</param>
        /// <param name="periodicPlusRigidModalCombination">The periodic plus rigid modal combination option.</param>
        /// <param name="td">The factor td [s].
        /// This item applies only when <paramref name="modalCombination" /> = <see cref="eModalCombination.DoubleSum" />.</param>
        void SetModalCombination(string name,
            eModalCombination modalCombination,
            double gmcF1,
            double gmcF2,
            ePeriodicPlusRigidModalCombination periodicPlusRigidModalCombination,
            double td);
#endif
    }
}