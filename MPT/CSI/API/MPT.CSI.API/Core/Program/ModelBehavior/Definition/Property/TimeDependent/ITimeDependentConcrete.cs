// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="ITimeDependentConcrete.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.TimeDependent.CodesConcrete;
#endif

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.TimeDependent
{
    /// <summary>
    /// Implements time dependent concrete in the application.
    /// </summary>
    public interface ITimeDependentConcrete
    {
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        #region Properties                            
        /// <summary>
        /// Gets the time dependent concrete method.
        /// </summary>
        /// <value>The time dependent concrete method.</value>
        TimeDependentConcreteMethod Method { get;}
        #endregion

        #region Methods: Public

        /// <summary>
        /// Sets the time dependent method used by the class.
        /// </summary>
        /// <param name="method">The time dependent method for the class to use.</param>
        void SetMethod(TimeDependentConcreteMethod method);

        /// <summary>
        /// This function retrieves the scale factors for the time-dependent material property data for concrete materials.
        /// </summary>
        /// <param name="name">The name of an existing concrete material property.</param>
        /// <param name="scaleFactorAge">This value multiplies the stiffness (modulus of elasticity) computed with age for the material during a time-dependent analysis.
        /// It has no effect for load cases that do not consider time-dependent effects, or for materials that do not consider time-dependent age effects.
        /// The default value is unity, and the specified value must be positive.</param>
        /// <param name="scaleFactorCreep">This value multiplies the creep coefficient, and hence the creep strain, computed for the material during a time-dependent analysis.
        /// It has no effect for load cases that do not consider time-dependent effects, or for materials that do not consider creep effects.
        /// The default value is unity, and the specified value must be positive.</param>
        /// <param name="scaleFactorShrinkage">This value multiplies the shrinkage strain computed for the material during a time-dependent analysis.
        /// It has no effect for load cases that do not consider time-dependent effects, or for materials that do not consider shrinkage effects.
        /// The default value is unity, and the specified value must be positive.</param>
        /// <param name="temperature">This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.
        /// This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.</param>
        void GetScaleFactors(string name,
            double scaleFactorAge,
            double scaleFactorCreep,
            double scaleFactorShrinkage,
            double temperature = 0);

        /// <summary>
        /// This function sets scale factors for the time-dependent material property data for concrete materials.
        /// If this function is not called, default values of unity are assumed for all scale factors.
        /// </summary>
        /// <param name="name">The name of an existing concrete material property.</param>
        /// <param name="scaleFactorAge">This value multiplies the stiffness (modulus of elasticity) computed with age for the material during a time-dependent analysis.
        /// It has no effect for load cases that do not consider time-dependent effects, or for materials that do not consider time-dependent age effects.
        /// The default value is unity, and the specified value must be positive.</param>
        /// <param name="scaleFactorCreep">This value multiplies the creep coefficient, and hence the creep strain, computed for the material during a time-dependent analysis.
        /// It has no effect for load cases that do not consider time-dependent effects, or for materials that do not consider creep effects.
        /// The default value is unity, and the specified value must be positive.</param>
        /// <param name="scaleFactorShrinkage">This value multiplies the shrinkage strain computed for the material during a time-dependent analysis.
        /// It has no effect for load cases that do not consider time-dependent effects, or for materials that do not consider shrinkage effects.
        /// The default value is unity, and the specified value must be positive.</param>
        /// <param name="temperature">This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.
        /// This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.</param>
        void SetScaleFactors(string name,
            double scaleFactorAge,
            double scaleFactorCreep,
            double scaleFactorShrinkage,
            double temperature = 0);

#endregion

#endif
    }
}