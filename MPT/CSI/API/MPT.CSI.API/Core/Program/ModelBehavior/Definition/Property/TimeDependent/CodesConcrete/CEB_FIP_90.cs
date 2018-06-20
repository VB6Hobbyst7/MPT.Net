// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-10-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="CEB_FIP_90.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.TimeDependent.CodesConcrete
{
    /// <summary>
    /// Represents the CEB FIP 90 method for time dependent cocnrete behavior.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.TimeDependent.CodesConcrete.TimeDependentConcreteMethod" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class CEB_FIP_90 : TimeDependentConcreteMethod
    {
        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="CEB_FIP_90" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public CEB_FIP_90(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Public

        /// <summary>
        /// This function sets the time dependent CEB FIP-90 material property data for concrete materials.
        /// </summary>
        /// <param name="name">The name of an existing concrete material property.</param>
        /// <param name="considerConcreteAge">True: Time dependence is considered for concrete compressive strength and stiffness (modulus of elasticity).</param>
        /// <param name="considerConcreteCreep">True: Time dependence is considered for concrete creep.</param>
        /// <param name="considerConcreteShrinkage">True: Time dependence is considered for concrete shrinkage.</param>
        /// <param name="CEBFIPsCoefficient">This is the cement type coefficient.
        /// This item applies only when <paramref name="considerConcreteAge" /> = True.</param>
        /// <param name="relativeHumidity">The relative humidity.
        /// This item applies only when <paramref name="considerConcreteCreep" /> = True or <paramref name="considerConcreteShrinkage" /> = True.</param>
        /// <param name="notionalSize">This is notional size of the member.
        /// This item applies only when <paramref name="considerConcreteCreep" /> = True or <paramref name="considerConcreteShrinkage" /> = True.
        /// As defined in Equation 2.1-69 of CEB_FIP Model Code 1990 the notional size is equal to two times the cross-sectional area of the member divided by the perimeter of the member in contact with the atmosphere.</param>
        /// <param name="shrinkageCoefficient">This is the shrinkage coefficient as defined in Equation 2.1-76 of CEB_FIP Model Code 1990.
        /// This item applies only when <paramref name="considerConcreteShrinkage" /> = True.</param>
        /// <param name="shrinkageStartAge">This is the shrinkage start age in days as used in Section 2.1.6.4.4 of CEB_FIP Model Code 1990.
        /// This item applies only when <paramref name="considerConcreteShrinkage" /> = True.</param>
        /// <param name="creepIntegrationType">The creep integration type.</param>
        /// <param name="numberSeriesTerms">This is the number of series terms used when integrating based on a Dirichlet series.
        /// This item applies only when <paramref name="considerConcreteCreep" /> = True and <paramref name="creepIntegrationType" /> = <see cref="eIntegrationType.DirichletSeries" />.</param>
        /// <param name="temperature">This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.
        /// This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetMethod(string name,
            ref bool considerConcreteAge,
            ref bool considerConcreteCreep,
            ref bool considerConcreteShrinkage,
            ref double CEBFIPsCoefficient,
            ref double relativeHumidity,
            ref double notionalSize,
            ref double shrinkageCoefficient,
            ref double shrinkageStartAge,
            ref eIntegrationType creepIntegrationType,
            ref int numberSeriesTerms,
            double temperature = 0)
        {
            int csiIntegrationType = 0;

            _callCode = _sapModel.PropMaterial.TimeDep.GetConcreteCEBFIP90(name, ref considerConcreteAge, ref considerConcreteCreep, ref considerConcreteShrinkage, ref CEBFIPsCoefficient, ref relativeHumidity, ref notionalSize, ref shrinkageCoefficient, ref shrinkageStartAge, ref csiIntegrationType, ref numberSeriesTerms, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            creepIntegrationType = (eIntegrationType) csiIntegrationType;
        }

        /// <summary>
        /// This function sets the time dependent CEB FIP-90 material property data for concrete materials.
        /// </summary>
        /// <param name="name">The name of an existing concrete material property.</param>
        /// <param name="considerConcreteAge">True: Time dependence is considered for concrete compressive strength and stiffness (modulus of elasticity).</param>
        /// <param name="considerConcreteCreep">True: Time dependence is considered for concrete creep.</param>
        /// <param name="considerConcreteShrinkage">True: Time dependence is considered for concrete shrinkage.</param>
        /// <param name="CEBFIPsCoefficient">This is the cement type coefficient.
        /// This item applies only when <paramref name="considerConcreteAge" /> = True.</param>
        /// <param name="relativeHumidity">The relative humidity.
        /// This item applies only when <paramref name="considerConcreteCreep" /> = True or <paramref name="considerConcreteShrinkage" /> = True.</param>
        /// <param name="notionalSize">This is notional size of the member.
        /// This item applies only when <paramref name="considerConcreteCreep" /> = True or <paramref name="considerConcreteShrinkage" /> = True.
        /// As defined in Equation 2.1-69 of CEB_FIP Model Code 1990 the notional size is equal to two times the cross-sectional area of the member divided by the perimeter of the member in contact with the atmosphere.</param>
        /// <param name="shrinkageCoefficient">This is the shrinkage coefficient as defined in Equation 2.1-76 of CEB_FIP Model Code 1990.
        /// This item applies only when <paramref name="considerConcreteShrinkage" /> = True.</param>
        /// <param name="shrinkageStartAge">This is the shrinkage start age in days as used in Section 2.1.6.4.4 of CEB_FIP Model Code 1990.
        /// This item applies only when <paramref name="considerConcreteShrinkage" /> = True.</param>
        /// <param name="creepIntegrationType">The creep integration type.</param>
        /// <param name="numberSeriesTerms">This is the number of series terms used when integrating based on a Dirichlet series.
        /// This item applies only when <paramref name="considerConcreteCreep" /> = True and <paramref name="creepIntegrationType" /> = <see cref="eIntegrationType.DirichletSeries" />.</param>
        /// <param name="temperature">This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.
        /// This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetMethod(string name,
            bool considerConcreteAge,
            bool considerConcreteCreep,
            bool considerConcreteShrinkage,
            double CEBFIPsCoefficient,
            double relativeHumidity,
            double notionalSize,
            double shrinkageCoefficient,
            double shrinkageStartAge,
            eIntegrationType creepIntegrationType,
            int numberSeriesTerms,
            double temperature = 0)
        {
            _callCode = _sapModel.PropMaterial.TimeDep.SetConcreteCEBFIP90(name, considerConcreteAge, considerConcreteCreep, considerConcreteShrinkage, CEBFIPsCoefficient, relativeHumidity, notionalSize, shrinkageCoefficient, shrinkageStartAge, (int)creepIntegrationType, numberSeriesTerms, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#endregion
    }
}

#endif