// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-10-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="TimeDependentConcrete.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.TimeDependent.CodesConcrete;
#endif
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.TimeDependent
{
    /// <summary>
    /// Represents time dependent concrete in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.TimeDependent.ITimeDependentConcrete" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class TimeDependentConcrete : CSiApiBase, ITimeDependentConcrete
    {
        #region Properties                            
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Gets the time dependent concrete method.
        /// </summary>
        /// <value>The time dependent concrete method.</value>
        public TimeDependentConcreteMethod Method { get; private set; }
#endif
        #endregion


        #region Initialization        

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeDependentConcrete" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public TimeDependentConcrete(CSiApiSeed seed) : base(seed)
        {}

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeDependentConcrete" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        /// <param name="method">The time dependent method for the class to use.</param>
        public TimeDependentConcrete(CSiApiSeed seed, TimeDependentConcreteMethod method) : base(seed)
        {
            Method = method;
        }
#endif
        #endregion

        #region Methods: Public
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Sets the time dependent method used by the class.
        /// </summary>
        /// <param name="method">The time dependent method for the class to use.</param>
        public void SetMethod(TimeDependentConcreteMethod method)
        {
            Method = method;
        }

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
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetScaleFactors(string name,
            double scaleFactorAge,
            double scaleFactorCreep,
            double scaleFactorShrinkage,
            double temperature = 0)
        {
            _callCode = _sapModel.PropMaterial.TimeDep.GetConcreteScaleFactors(name, ref scaleFactorAge, ref scaleFactorCreep, ref scaleFactorShrinkage, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

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
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetScaleFactors(string name,
            double scaleFactorAge,
            double scaleFactorCreep,
            double scaleFactorShrinkage,
            double temperature = 0)
        {
            _callCode = _sapModel.PropMaterial.TimeDep.SetConcreteScaleFactors(name, scaleFactorAge, scaleFactorCreep, scaleFactorShrinkage, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
#endregion
    }
}
