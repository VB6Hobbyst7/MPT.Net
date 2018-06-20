// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="ITimeDependentTendon.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.TimeDependent.CodesTendon;
#endif
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.TimeDependent
{
    /// <summary>
    /// Implements time dependent tendon material in the application.
    /// </summary>
    public interface ITimeDependentTendon
    {
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        #region Properties                            
        /// <summary>
        /// Gets the time dependent concrete method.
        /// </summary>
        /// <value>The time dependent concrete method.</value>
        TimeDependentTendonMethod Method { get; }
        #endregion

        #region Methods: Public
        /// <summary>
        /// Sets the time dependent method used by the class.
        /// </summary>
        /// <param name="method">The time dependent method for the class to use.</param>
        void SetMethod(TimeDependentTendonMethod method);

        /// <summary>
        /// This function retrieves the scale factors for the time-dependent material property data for tendon materials.
        /// The function returns an error if the specified material is not tendon.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="name">The name of an existing tendon material property.</param>
        /// <param name="scaleFactorRelaxation">This value multiplies the relaxation coefficient, and hence the relaxation strain, computed for the material during a time-dependent analysis.
        /// It has no effect for load cases that do not consider time-dependent effects, or for materials that do not consider relaxation effects.
        /// The default value is unity, and the specified value must be positive.</param>
        /// <param name="temperature">This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.
        /// This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.</param>
        void GetScaleFactors(string name,
            ref double scaleFactorRelaxation,
            double temperature = 0);

        /// <summary>
        /// This function sets scale factors for the time-dependent material property data for tendon materials.
        /// If this function is not called, default values of unity are assumed for all scale factors.
        /// </summary>
        /// <param name="name">The name of an existing tendon material property.</param>
        /// <param name="scaleFactorRelaxation">This value multiplies the relaxation coefficient, and hence the relaxation strain, computed for the material during a time-dependent analysis.
        /// It has no effect for load cases that do not consider time-dependent effects, or for materials that do not consider relaxation effects.
        /// The default value is unity, and the specified value must be positive.</param>
        /// <param name="temperature">This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.
        /// This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.</param>
        void SetScaleFactors(string name,
            double scaleFactorRelaxation,
            double temperature = 0);

#endregion

#endif
    }
}