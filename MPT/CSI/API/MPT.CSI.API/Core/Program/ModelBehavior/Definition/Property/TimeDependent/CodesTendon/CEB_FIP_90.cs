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

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.TimeDependent.CodesTendon
{
    /// <summary>
    /// Represents the CEB FIP 90 method for time dependent cocnrete behavior.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.TimeDependent.CodesTendon.TimeDependentTendonMethod" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class CEB_FIP_90 : TimeDependentTendonMethod
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
        /// This function retrieves the time dependent CEB FIP-90 material property data for tendon materials.
        /// The function returns an error if the specified material is not tendon.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="name">The name of an existing tendon material property.</param>
        /// <param name="considerSteelRelaxation">True: Time dependence is considered for tendon steel relaxation.</param>
        /// <param name="classification">The CEB FIP-90 class.
        /// This item applies only when <paramref name="considerSteelRelaxation" /> = True.</param>
        /// <param name="integrationType">The steel relaxation integration type.</param>
        /// <param name="numberSeriesTerms">This is the number of series terms used when integrating based on a Dirichlet series.
        /// This item applies only when <paramref name="considerSteelRelaxation" /> = True and <paramref name="integrationType" /> = <see cref="eIntegrationType.DirichletSeries" />.</param>
        /// <param name="temperature">This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.
        /// This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetMethod(string name,
            ref bool considerSteelRelaxation,
            ref eCEBFIP90Class classification,
            ref eIntegrationType integrationType,
            ref int numberSeriesTerms,
            double temperature = 0)
        {
            int csiClass = 0;
            int csiIntegrationType = 0;

            _callCode = _sapModel.PropMaterial.TimeDep.GetTendonCEBFIP90(name, ref considerSteelRelaxation, ref csiClass, ref csiIntegrationType, ref numberSeriesTerms, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            classification = (eCEBFIP90Class) csiClass;
            integrationType = (eIntegrationType)csiIntegrationType;
        }

        /// <summary>
        /// This function sets the time dependent CEB FIP-90 material property data for tendon materials.
        /// </summary>
        /// <param name="name">The name of an existing tendon material property.</param>
        /// <param name="considerSteelRelaxation">True: Time dependence is considered for tendon steel relaxation.</param>
        /// <param name="classification">The CEB FIP-90 class.
        /// This item applies only when <paramref name="considerSteelRelaxation" /> = True.</param>
        /// <param name="integrationType">The steel relaxation integration type.</param>
        /// <param name="numberSeriesTerms">This is the number of series terms used when integrating based on a Dirichlet series.
        /// This item applies only when <paramref name="considerSteelRelaxation" /> = True and <paramref name="integrationType" /> = <see cref="eIntegrationType.DirichletSeries" />.</param>
        /// <param name="temperature">This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.
        /// This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetMethod(string name,
            bool considerSteelRelaxation,
            eCEBFIP90Class classification,
            eIntegrationType integrationType,
            int numberSeriesTerms,
            double temperature = 0)
        {
            _callCode = _sapModel.PropMaterial.TimeDep.SetTendonCEBFIP90(name, considerSteelRelaxation, (int)classification, (int)integrationType, numberSeriesTerms, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        
#endregion
    }
}
#endif