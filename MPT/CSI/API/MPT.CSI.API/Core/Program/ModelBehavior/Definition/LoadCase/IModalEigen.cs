// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-19-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="IModalEigen.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Represents the Eigen modal load case in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.IModal" />
    public interface IModalEigen: IModal
    {
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function retrieves the load data for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing modal Eigen load case.</param>
        /// <param name="loadTypes">The load types.</param>
        /// <param name="loadNames">This is an array that includes the name of each load assigned to the load case.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadTypeModal.Load" />, this item is the name of a defined load pattern.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadTypeModal.Accel" />, this item is UX, UY, UZ, RX, RY or RZ, indicating the direction of the load.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadTypeModal.Link" />, this item is not used.</param>
        /// <param name="targetMassParticipationRatios">The target mass participation ratios.</param>
        /// <param name="isStaticCorrectionModeCalculated">True: Static correction modes are to be calculated.</param>
        void GetLoads(string name,
            ref eLoadTypeModal[] loadTypes,
            ref string[] loadNames,
            ref double[] targetMassParticipationRatios,
            ref bool[] isStaticCorrectionModeCalculated);

        /// <summary>
        /// This function sets the load data for the specified analysis case.
        /// </summary>
        /// <param name="name">The name of an existing modal Eigen load case.</param>
        /// <param name="loadTypes">The load types.</param>
        /// <param name="loadNames">This is an array that includes the name of each load assigned to the load case.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadTypeModal.Load" />, this item is the name of a defined load pattern.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadTypeModal.Accel" />, this item is UX, UY, UZ, RX, RY or RZ, indicating the direction of the load.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadTypeModal.Link" />, this item is not used.</param>
        /// <param name="targetMassParticipationRatios">The target mass participation ratios.</param>
        /// <param name="isStaticCorrectionModeCalculated">True: Static correction modes are to be calculated.</param>
        void SetLoads(string name,
            eLoadTypeModal[] loadTypes,
            string[] loadNames,
            double[] targetMassParticipationRatios,
            bool[] isStaticCorrectionModeCalculated);




        /// <summary>
        /// This function retrieves various parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing modal eigen load case.</param>
        /// <param name="eigenvalueShiftFrequency">The eigenvalue shift frequency. [cyc/s].</param>
        /// <param name="cutoffFrequencyRadius">The eigencutoff frequency radius. [cyc/s].</param>
        /// <param name="convergenceTolerance">The relative convergence tolerance for eigenvalues.</param>
        /// <param name="allowAutoFrequencyShifting">True: Automatic frequency shifting is allowed</param>
        void GetParameters(string name,
            ref double eigenvalueShiftFrequency,
            ref double cutoffFrequencyRadius,
            ref double convergenceTolerance,
            ref bool allowAutoFrequencyShifting);

        /// <summary>
        /// This function sets various parameters for the specified modal eigen load case.
        /// </summary>
        /// <param name="name">The name of an existing modal eigen load case.</param>
        /// <param name="eigenvalueShiftFrequency">The eigenvalue shift frequency. [cyc/s].</param>
        /// <param name="cutoffFrequencyRadius">The eigencutoff frequency radius. [cyc/s].</param>
        /// <param name="convergenceTolerance">The relative convergence tolerance for eigenvalues.</param>
        /// <param name="allowAutoFrequencyShifting">True: Automatic frequency shifting is allowed</param>
        void SetParameters(string name,
            double eigenvalueShiftFrequency,
            double cutoffFrequencyRadius,
            double convergenceTolerance,
            bool allowAutoFrequencyShifting);
#endif
    }
}
#endif