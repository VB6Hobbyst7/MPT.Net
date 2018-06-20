// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-11-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="LinkProperties.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Linq;
#if BUILD_SAP2000v16
using CSiProgram = SAP2000v16;
#elif BUILD_SAP2000v17
using CSiProgram = SAP2000v17;
#elif BUILD_SAP2000v18
using CSiProgram = SAP2000v18;
#elif BUILD_SAP2000v19
using CSiProgram = SAP2000v19;
#elif BUILD_SAP2000v20
using CSiProgram = SAP2000v20;
#elif BUILD_CSiBridgev16
using CSiProgram = CSiBridge16;
#elif BUILD_CSiBridgev17
using CSiProgram = CSiBridge17;
#elif BUILD_CSiBridgev18
using CSiProgram = CSiBridge18;
#elif BUILD_CSiBridgev19
using CSiProgram = CSiBridge19;
#elif BUILD_CSiBridgev20
using CSiProgram = CSiBridge20;
#elif BUILD_ETABS2013
using CSiProgram = ETABS2013;
#elif BUILD_ETABS2015
using CSiProgram = ETABS2015;
#elif BUILD_ETABS2016
using CSiProgram = ETABS2016;
#elif BUILD_ETABS2017
using CSiProgram = ETABSv17;
#endif
using MPT.Enums;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Represents the link properties in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.ILinkProperties" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class LinkProperties : CSiApiBase, ILinkProperties
    {
        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkProperties" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public LinkProperties(CSiApiSeed seed) : base(seed)
        {
        }

        #endregion

        #region Methods: Interface

        /// <summary>
        /// This function changes the name of an existing link property.
        /// </summary>
        /// <param name="currentName">The existing name of a defined link property.</param>
        /// <param name="newName">The new name for the link property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ChangeName(string currentName,
            string newName)
        {
            _callCode = _sapModel.PropLink.ChangeName(currentName, newName);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }
        }

        /// <summary>
        /// This function returns the total number of defined link properties in the model.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int Count()
        {
            return _sapModel.PropLink.Count();
        }

        /// <summary>
        /// The function deletes a specified link property.
        /// It returns an error if the specified property can not be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        /// <param name="name">The name of an existing link property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void Delete(string name)
        {
            _callCode = _sapModel.PropLink.Delete(name);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }
        }

        /// <summary>
        /// This function retrieves the names of all defined link properties.
        /// </summary>
        /// <param name="names">Link property names retrieved by the program.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetNameList(out string[] names)
        {
            names = new string[0];
            _callCode = _sapModel.PropLink.GetNameList(ref _numberOfItems, ref names);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }
        }

        /// <summary>
        /// This function retrieves the names of all defined link properties of the specified type.
        /// </summary>
        /// <param name="names">Link property names retrieved by the program.</param>
        /// <param name="linkType">The link type to filter the name list by.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetNameList(out string[] names,
            eLinkPropertyType linkType)
        {
            names = new string[0];
            _callCode = _sapModel.PropLink.GetNameList(ref _numberOfItems, ref names,
                EnumLibrary.Convert<eLinkPropertyType, CSiProgram.eLinkPropType>(linkType));
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }
        }

        #endregion

        #region Methods: Public        

        /// <summary>
        /// This function retrieves the property type for the specified link property.
        /// </summary>
        /// <param name="name">The name of an existing link property.</param>
        /// <param name="linkType">Type of the link.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetLinkType(string name,
            ref eLinkPropertyType linkType)
        {
            CSiProgram.eLinkPropType csiLinkType = CSiProgram.eLinkPropType.Damper;

            _callCode = _sapModel.PropLink.GetTypeOAPI(name, ref csiLinkType);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }

            linkType = EnumLibrary.Convert(csiLinkType, linkType);
        }




        /// <summary>
        /// This function retrieves weight and mass data for a link property.
        /// </summary>
        /// <param name="name">The name of an existing link property.</param>
        /// <param name="weight">The weight of the link. [F].</param>
        /// <param name="massTranslational">The translational mass of the link. [M].</param>
        /// <param name="massR1">The rotational inertia of the link about its local 1 axis. [M*L^2].</param>
        /// <param name="massR2">The rotational inertia of the link about its local 2 axis. [M*L^2].</param>
        /// <param name="massR3">The rotational inertia of the link about its local 3 axis. [M*L^2].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetWeightAndMass(string name,
            ref double weight,
            ref double massTranslational,
            ref double massR1,
            ref double massR2,
            ref double massR3)
        {
            _callCode = _sapModel.PropLink.GetWeightAndMass(name, ref weight, ref massTranslational, ref massR1,
                ref massR2, ref massR3);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }
        }

        /// <summary>
        /// This function assigns weight and mass values to a link property.
        /// </summary>
        /// <param name="name">The name of an existing link property.</param>
        /// <param name="weight">The weight of the link. [F].</param>
        /// <param name="massTranslational">The translational mass of the link. [M].</param>
        /// <param name="massR1">The rotational inertia of the link about its local 1 axis. [M*L^2].</param>
        /// <param name="massR2">The rotational inertia of the link about its local 2 axis. [M*L^2].</param>
        /// <param name="massR3">The rotational inertia of the link about its local 3 axis. [M*L^2].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetWeightAndMass(string name,
            double weight,
            double massTranslational,
            double massR1,
            double massR2,
            double massR3)
        {
            _callCode = _sapModel.PropLink.SetWeightAndMass(name, weight, massTranslational, massR1, massR2, massR3);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }
        }




        /// <summary>
        /// This function retrieves P-delta parameters for a link property.
        /// </summary>
        /// <param name="name">The name of an existing link property.</param>
        /// <param name="pDeltaParameters">The P-delta moment parameters at each end of the link.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetPDelta(string name,
            ref PDeltaParameters pDeltaParameters)
        {
            double[] csiPDeltaParameters = new double[0];

            _callCode = _sapModel.PropLink.GetPDelta(name, ref csiPDeltaParameters);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }

            pDeltaParameters.FromArray(csiPDeltaParameters);
        }

        /// <summary>
        /// This function assigns P-delta parameters to a link property.
        /// </summary>
        /// <param name="name">The name of an existing link property.</param>
        /// <param name="pDeltaParameters">The P-delta moment parameters at each end of the link</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetPDelta(string name,
            PDeltaParameters pDeltaParameters)
        {
            double[] csiPDeltaParameters = pDeltaParameters.ToArray();
            _callCode = _sapModel.PropLink.SetPDelta(name, ref csiPDeltaParameters);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }
        }



        /// <summary>
        /// This function retrieves length and area values for a link property that are used if the link property is specified in line and area spring assignments.
        /// </summary>
        /// <param name="name">The name of an existing link prope.</param>
        /// <param name="lengthDefined">The link property is defined for this length in a line (frame) spring. [L].</param>
        /// <param name="areaDefined">The link property is defined for this area in an area spring. [L^2].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetSpringData(string name,
            ref double lengthDefined,
            ref double areaDefined)
        {
            _callCode = _sapModel.PropLink.GetSpringData(name, ref lengthDefined, ref areaDefined);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }
        }

        /// <summary>
        /// This function assigns length and area values to a link property that are used if the link property is specified in line and area spring assignments.
        /// </summary>
        /// <param name="name">The name of an existing link prope.</param>
        /// <param name="lengthDefined">The link property is defined for this length in a line (frame) spring. [L].</param>
        /// <param name="areaDefined">The link property is defined for this area in an area spring. [L^2].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetSpringData(string name,
            double lengthDefined,
            double areaDefined)
        {
            _callCode = _sapModel.PropLink.SetSpringData(name, lengthDefined, areaDefined);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }
        }

        #endregion

        #region Methods: Get/Set Properties        

        /// <summary>
        /// This function retrieves link property data for an exponential damper-type link property.
        /// </summary>
        /// <param name="name">The name of an existing exponential damper-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property.
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property.
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property.
        /// The initial stiffness applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="nonLinearDamping">Nonlinear damping coefficient terms for the link property.
        /// The nonlinear damping coefficient applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="nonLinearDampingExponent">Nonlinear damping exponent terms.
        /// The nonlinear damping exponent applies for nonlinear analyses.
        /// It is applied to the velocity across the damper in the equation of motion.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U2" /> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U3" /> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetDamper(string name,
            ref DegreesOfFreedomLocal degreesOfFreedom,
            ref DegreesOfFreedomLocal fixity,
            ref DegreesOfFreedomLocal nonLinear,
            ref Stiffness effectiveStiffness,
            ref Stiffness effectiveDamping,
            ref Stiffness initialStiffness,
            ref Stiffness nonLinearDamping,
            ref Stiffness nonLinearDampingExponent,
            ref double distanceFromJEndToU2Spring,
            ref double distanceFromJEndToU3Spring,
            ref string notes,
            ref string GUID)
        {
            bool[] csiDegreesOfFreedom = new bool[0];
            bool[] csiFixity = new bool[0];
            bool[] csiNonLinear = new bool[0];
            double[] csiEffectiveStiffness = new double[0];
            double[] csiEffectiveDamping = new double[0];
            double[] csiInitialStiffness = new double[0];
            double[] csiNonLinearDamping = new double[0];
            double[] csiNonLinearDampingExponent = new double[0];

            _callCode = _sapModel.PropLink.GetDamper(name,
                ref csiDegreesOfFreedom, ref csiFixity, ref csiNonLinear,
                ref csiEffectiveStiffness, ref csiEffectiveDamping, ref csiInitialStiffness, ref csiNonLinearDamping,
                ref csiNonLinearDampingExponent,
                ref distanceFromJEndToU2Spring, ref distanceFromJEndToU3Spring, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }

            degreesOfFreedom.FromArray(csiDegreesOfFreedom);
            fixity.FromArray(csiFixity);
            nonLinear.FromArray(csiNonLinear);
            effectiveStiffness.FromArray(csiEffectiveStiffness);
            effectiveDamping.FromArray(csiEffectiveDamping);
            initialStiffness.FromArray(csiInitialStiffness);
            nonLinearDamping.FromArray(csiNonLinearDamping);
            nonLinearDampingExponent.FromArray(csiNonLinearDampingExponent);
        }

        /// <summary>
        /// This function initializes an exponential damper-type link property.
        /// If this function is called for an existing link property, all items for the property are reset to their default values.
        /// </summary>
        /// <param name="name">The name of an existing exponential damper-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property.
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property.
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property.
        /// The initial stiffness applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="nonLinearDamping">Nonlinear damping coefficient terms for the link property.
        /// The nonlinear damping coefficient applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="nonLinearDampingExponent">Nonlinear damping exponent terms.
        /// The nonlinear damping exponent applies for nonlinear analyses.
        /// It is applied to the velocity across the damper in the equation of motion.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U2" /> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U3" /> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetDamper(string name,
            DegreesOfFreedomLocal degreesOfFreedom,
            DegreesOfFreedomLocal fixity,
            DegreesOfFreedomLocal nonLinear,
            Stiffness effectiveStiffness,
            Stiffness effectiveDamping,
            Stiffness initialStiffness,
            Stiffness nonLinearDamping,
            Stiffness nonLinearDampingExponent,
            double distanceFromJEndToU2Spring,
            double distanceFromJEndToU3Spring,
            string notes = "",
            string GUID = "")
        {
            bool[] csiDegreesOfFreedom = degreesOfFreedom.ToArray();
            bool[] csiFixity = fixity.ToArray();
            bool[] csiNonLinear = nonLinear.ToArray();
            double[] csiEffectiveStiffness = effectiveStiffness.ToArray();
            double[] csiEffectiveDamping = effectiveDamping.ToArray();
            double[] csiInitialStiffness = initialStiffness.ToArray();
            double[] csiNonLinearDamping = nonLinearDamping.ToArray();
            double[] csiNonLinearDampingExponent = nonLinearDampingExponent.ToArray();

            _callCode = _sapModel.PropLink.SetDamper(name,
                ref csiDegreesOfFreedom, ref csiFixity, ref csiNonLinear,
                ref csiEffectiveStiffness, ref csiEffectiveDamping, ref csiInitialStiffness, ref csiNonLinearDamping,
                ref csiNonLinearDampingExponent,
                distanceFromJEndToU2Spring, distanceFromJEndToU3Spring, notes, GUID);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }
        }



        /// <summary>
        /// This function retrieves link property data for a bilinear damper-type link property.
        /// </summary>
        /// <param name="name">The name of an existing bilinear damper-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property.
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property.
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property.
        /// The initial stiffness applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="nonLinearInitialDamping">Nonlinear initial damping coefficient terms for the link property.
        /// The nonlinear initial damping coefficient applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="nonLinearYieldedDamping">Nonlinear yielded damping exponent terms.
        /// The nonlinear yielded damping exponent applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="nonLinearForceLimit">Nonlinear linear force limit terms for the link property.
        /// The linear force limit applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U2" /> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U3" /> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetDamperBilinear(string name,
            ref DegreesOfFreedomLocal degreesOfFreedom,
            ref DegreesOfFreedomLocal fixity,
            ref DegreesOfFreedomLocal nonLinear,
            ref Stiffness effectiveStiffness,
            ref Stiffness effectiveDamping,
            ref Stiffness initialStiffness,
            ref Stiffness nonLinearInitialDamping,
            ref Stiffness nonLinearYieldedDamping,
            ref Deformations nonLinearForceLimit,
            ref double distanceFromJEndToU2Spring,
            ref double distanceFromJEndToU3Spring,
            ref string notes,
            ref string GUID)
        {
            bool[] csiDegreesOfFreedom = new bool[0];
            bool[] csiFixity = new bool[0];
            bool[] csiNonLinear = new bool[0];
            double[] csiEffectiveStiffness = new double[0];
            double[] csiEffectiveDamping = new double[0];
            double[] csiInitialStiffness = new double[0];
            double[] csiNonLinearInitialDamping = new double[0];
            double[] csiNonLinearYieldedDamping = new double[0];
            double[] csiNonLinearForceLimit = new double[0];

            _callCode = _sapModel.PropLink.GetDamperBilinear(name,
                ref csiDegreesOfFreedom, ref csiFixity, ref csiNonLinear,
                ref csiEffectiveStiffness, ref csiEffectiveDamping, ref csiInitialStiffness,
                ref csiNonLinearInitialDamping, ref csiNonLinearYieldedDamping, ref csiNonLinearForceLimit,
                ref distanceFromJEndToU2Spring, ref distanceFromJEndToU3Spring, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }

            degreesOfFreedom.FromArray(csiDegreesOfFreedom);
            fixity.FromArray(csiFixity);
            nonLinear.FromArray(csiNonLinear);
            effectiveStiffness.FromArray(csiEffectiveStiffness);
            effectiveDamping.FromArray(csiEffectiveDamping);
            initialStiffness.FromArray(csiInitialStiffness);
            nonLinearInitialDamping.FromArray(csiNonLinearInitialDamping);
            nonLinearYieldedDamping.FromArray(csiNonLinearYieldedDamping);
            nonLinearForceLimit.FromArray(csiNonLinearForceLimit);
        }

        /// <summary>
        /// This function initializes a bilinear damper-type link property.
        /// If this function is called for an existing link property, all items for the property are reset to their default values.
        /// </summary>
        /// <param name="name">The name of an existing bilinear damper-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property.
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property.
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property.
        /// The initial stiffness applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="nonLinearInitialDamping">Nonlinear initial damping coefficient terms for the link property.
        /// The nonlinear initial damping coefficient applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="nonLinearYieldedDamping">Nonlinear yielded damping exponent terms.
        /// The nonlinear yielded damping exponent applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="nonLinearForceLimit">Nonlinear linear force limit terms for the link property.
        /// The linear force limit applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U2" /> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U3" /> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetDamperBilinear(string name,
            DegreesOfFreedomLocal degreesOfFreedom,
            DegreesOfFreedomLocal fixity,
            DegreesOfFreedomLocal nonLinear,
            Stiffness effectiveStiffness,
            Stiffness effectiveDamping,
            Stiffness initialStiffness,
            Stiffness nonLinearInitialDamping,
            Stiffness nonLinearYieldedDamping,
            Deformations nonLinearForceLimit,
            double distanceFromJEndToU2Spring,
            double distanceFromJEndToU3Spring,
            string notes = "",
            string GUID = "")
        {
            bool[] csiDegreesOfFreedom = degreesOfFreedom.ToArray();
            bool[] csiFixity = fixity.ToArray();
            bool[] csiNonLinear = nonLinear.ToArray();
            double[] csiEffectiveStiffness = effectiveStiffness.ToArray();
            double[] csiEffectiveDamping = effectiveDamping.ToArray();
            double[] csiInitialStiffness = initialStiffness.ToArray();
            double[] csiNonLinearInitialDamping = nonLinearInitialDamping.ToArray();
            double[] csiNonLinearYieldedDamping = nonLinearYieldedDamping.ToArray();
            double[] csiNonLinearForceLimit = nonLinearForceLimit.ToArray();

            _callCode = _sapModel.PropLink.SetDamperBilinear(name,
                ref csiDegreesOfFreedom, ref csiFixity, ref csiNonLinear,
                ref csiEffectiveStiffness, ref csiEffectiveDamping, ref csiInitialStiffness,
                ref csiNonLinearInitialDamping, ref csiNonLinearYieldedDamping, ref csiNonLinearForceLimit,
                distanceFromJEndToU2Spring, distanceFromJEndToU3Spring, notes, GUID);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }
        }



        /// <summary>
        /// This function retrieves link property data for a friction spring damper-type link property.
        /// </summary>
        /// <param name="name">The name of an existing friction spring damper-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property.
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property.
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property.
        /// The initial stiffness applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="slippingStiffnessLoading">Slipping stiffness when loading terms for the link property.
        /// The slipping stiffness when loading applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="slippingStiffnessUnloading">Slipping stiffness when unloading terms for the link property.
        /// The slipping stiffness when unloading applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="preCompressionDisplacement">Precompression displacement terms for the link property.
        /// The nonlinear precompression displacement applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="stopDisplacement">Stop displacement terms for the link property.
        /// The nonlinear stop displacement applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="direction">The direction for which the force can be applied for deach degree-of-freedom.
        /// The direction applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U2" /> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U3" /> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetDamperFrictionSpring(string name,
            ref DegreesOfFreedomLocal degreesOfFreedom,
            ref DegreesOfFreedomLocal fixity,
            ref DegreesOfFreedomLocal nonLinear,
            ref Stiffness effectiveStiffness,
            ref Stiffness effectiveDamping,
            ref Stiffness initialStiffness,
            ref Stiffness slippingStiffnessLoading,
            ref Stiffness slippingStiffnessUnloading,
            ref Deformations preCompressionDisplacement,
            ref Deformations stopDisplacement,
            ref eLinkDirection[] direction,
            ref double distanceFromJEndToU2Spring,
            ref double distanceFromJEndToU3Spring,
            ref string notes,
            ref string GUID)
        {
            bool[] csiDegreesOfFreedom = new bool[0];
            bool[] csiFixity = new bool[0];
            bool[] csiNonLinear = new bool[0];
            double[] csiEffectiveStiffness = new double[0];
            double[] csiEffectiveDamping = new double[0];
            double[] csiInitialStiffness = new double[0];
            double[] csiSlippingStiffnessLoading = new double[0];
            double[] csiSlippingStiffessUnloading = new double[0];
            double[] csiPreCompressionDisplacement = new double[0];
            double[] csiStopDisplacement = new double[0];
            int[] csiDirection = new int[0];

            _callCode = _sapModel.PropLink.GetDamperFrictionSpring(name,
                ref csiDegreesOfFreedom, ref csiFixity, ref csiNonLinear,
                ref csiEffectiveStiffness, ref csiEffectiveDamping, ref csiInitialStiffness,
                ref csiSlippingStiffnessLoading, ref csiSlippingStiffessUnloading, ref csiPreCompressionDisplacement,
                ref csiStopDisplacement,
                ref csiDirection, ref distanceFromJEndToU2Spring, ref distanceFromJEndToU3Spring, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }

            direction = csiDirection.Cast<eLinkDirection>().ToArray();

            degreesOfFreedom.FromArray(csiDegreesOfFreedom);
            fixity.FromArray(csiFixity);
            nonLinear.FromArray(csiNonLinear);
            effectiveStiffness.FromArray(csiEffectiveStiffness);
            effectiveDamping.FromArray(csiEffectiveDamping);
            initialStiffness.FromArray(csiInitialStiffness);
            slippingStiffnessLoading.FromArray(csiSlippingStiffnessLoading);
            slippingStiffnessUnloading.FromArray(csiSlippingStiffessUnloading);
            preCompressionDisplacement.FromArray(csiPreCompressionDisplacement);
            stopDisplacement.FromArray(csiStopDisplacement);
        }

        /// <summary>
        /// This function initializes a friction spring damper-type link property.
        /// If this function is called for an existing link property, all items for the property are reset to their default values.
        /// </summary>
        /// <param name="name">The name of an existing friction spring damper-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property.
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property.
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property.
        /// The initial stiffness applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="slippingStiffnessLoading">Slipping stiffness when loading terms for the link property.
        /// The slipping stiffness when loading applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="slippingStiffnessUnloading">Slipping stiffness when unloading terms for the link property.
        /// The slipping stiffness when unloading applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="preCompressionDisplacement">Precompression displacement terms for the link property.
        /// The nonlinear precompression displacement applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="stopDisplacement">Stop displacement terms for the link property.
        /// The nonlinear stop displacement applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="direction">The direction for which the force can be applied for deach degree-of-freedom.
        /// The direction applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U2" /> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U3" /> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetDamperFrictionSpring(string name,
            DegreesOfFreedomLocal degreesOfFreedom,
            DegreesOfFreedomLocal fixity,
            DegreesOfFreedomLocal nonLinear,
            Stiffness effectiveStiffness,
            Stiffness effectiveDamping,
            Stiffness initialStiffness,
            Stiffness slippingStiffnessLoading,
            Stiffness slippingStiffnessUnloading,
            Deformations preCompressionDisplacement,
            Deformations stopDisplacement,
            eLinkDirection[] direction,
            double distanceFromJEndToU2Spring,
            double distanceFromJEndToU3Spring,
            string notes = "",
            string GUID = "")
        {
            bool[] csiDegreesOfFreedom = degreesOfFreedom.ToArray();
            bool[] csiFixity = fixity.ToArray();
            bool[] csiNonLinear = nonLinear.ToArray();
            double[] csiEffectiveStiffness = effectiveStiffness.ToArray();
            double[] csiEffectiveDamping = effectiveDamping.ToArray();
            double[] csiInitialStiffness = initialStiffness.ToArray();
            double[] csiSlippingStiffnessLoading = slippingStiffnessLoading.ToArray();
            double[] csiSlippingStiffessUnloading = slippingStiffnessUnloading.ToArray();
            double[] csiPreCompressionDisplacement = preCompressionDisplacement.ToArray();
            double[] csiStopDisplacement = stopDisplacement.ToArray();

            int[] csiDirection = direction.Cast<int>().ToArray();

            _callCode = _sapModel.PropLink.SetDamperFrictionSpring(name,
                ref csiDegreesOfFreedom, ref csiFixity, ref csiNonLinear,
                ref csiEffectiveStiffness, ref csiEffectiveDamping, ref csiInitialStiffness,
                ref csiSlippingStiffnessLoading, ref csiSlippingStiffessUnloading, ref csiPreCompressionDisplacement,
                ref csiStopDisplacement,
                ref csiDirection, distanceFromJEndToU2Spring, distanceFromJEndToU3Spring, notes, GUID);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }
        }


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function retrieves link property data for a linear exponential damper-type link property.
        /// </summary>
        /// <param name="name">The name of an existing exponential damper-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property.
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property.
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property.
        /// The initial stiffness applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="nonLinearDamping">Nonlinear damping coefficient terms for the link property.
        /// The nonlinear damping coefficient applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="nonLinearDampingExponent">Nonlinear damping exponent terms.
        /// The nonlinear damping exponent applies for nonlinear analyses.
        /// It is applied to the velocity across the damper in the equation of motion.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="nonLinearForceLimit">Nonlinear linear force limit terms for the link property.
        /// The linear force limit applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U2" /> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U3" /> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetDamperLinearExponential(string name,
            ref DegreesOfFreedomLocal degreesOfFreedom,
            ref DegreesOfFreedomLocal fixity,
            ref DegreesOfFreedomLocal nonLinear,
            ref Stiffness effectiveStiffness,
            ref Stiffness effectiveDamping,
            ref Stiffness initialStiffness,
            ref Stiffness nonLinearDamping,
            ref Stiffness nonLinearDampingExponent,
            ref Deformations nonLinearForceLimit,
            ref double distanceFromJEndToU2Spring,
            ref double distanceFromJEndToU3Spring,
            ref string notes,
            ref string GUID)
        {
            bool[] csiDegreesOfFreedom = new bool[0];
            bool[] csiFixity = new bool[0];
            bool[] csiNonLinear = new bool[0];
            double[] csiEffectiveStiffness = new double[0];
            double[] csiEffectiveDamping = new double[0];
            double[] csiInitialStiffness = new double[0];
            double[] csiNonLinearDamping = new double[0];
            double[] csiNonLinearDampingExponent = new double[0];
            double[] csiNonLinearForceLimit = new double[0];

            _callCode = _sapModel.PropLink.GetDamperLinearExponential(name,
                ref csiDegreesOfFreedom, ref csiFixity, ref csiNonLinear,
                ref csiEffectiveStiffness, ref csiEffectiveDamping, ref csiInitialStiffness, ref csiNonLinearDamping,
                ref csiNonLinearDampingExponent, ref csiNonLinearForceLimit,
                ref distanceFromJEndToU2Spring, ref distanceFromJEndToU3Spring, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }

            degreesOfFreedom.FromArray(csiDegreesOfFreedom);
            fixity.FromArray(csiFixity);
            nonLinear.FromArray(csiNonLinear);
            effectiveStiffness.FromArray(csiEffectiveStiffness);
            effectiveDamping.FromArray(csiEffectiveDamping);
            initialStiffness.FromArray(csiInitialStiffness);
            nonLinearDamping.FromArray(csiNonLinearDamping);
            nonLinearDampingExponent.FromArray(csiNonLinearDampingExponent);
            nonLinearForceLimit.FromArray(csiNonLinearForceLimit);
        }

        /// <summary>
        /// This function initializes a linear exponential damper-type link property.
        /// If this function is called for an existing link property, all items for the property are reset to their default values.
        /// </summary>
        /// <param name="name">The name of an existing exponential damper-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property.
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property.
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property.
        /// The initial stiffness applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="nonLinearDamping">Nonlinear damping coefficient terms for the link property.
        /// The nonlinear damping coefficient applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="nonLinearDampingExponent">Nonlinear damping exponent terms.
        /// The nonlinear damping exponent applies for nonlinear analyses.
        /// It is applied to the velocity across the damper in the equation of motion.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="nonLinearForceLimit">Nonlinear linear force limit terms for the link property.
        /// The linear force limit applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U2" /> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U3" /> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetDamperLinearExponential(string name,
            DegreesOfFreedomLocal degreesOfFreedom,
            DegreesOfFreedomLocal fixity,
            DegreesOfFreedomLocal nonLinear,
            Stiffness effectiveStiffness,
            Stiffness effectiveDamping,
            Stiffness initialStiffness,
            Stiffness nonLinearDamping,
            Stiffness nonLinearDampingExponent,
            Deformations nonLinearForceLimit,
            double distanceFromJEndToU2Spring,
            double distanceFromJEndToU3Spring,
            string notes = "",
            string GUID = "")
        {
            bool[] csiDegreesOfFreedom = degreesOfFreedom.ToArray();
            bool[] csiFixity = fixity.ToArray();
            bool[] csiNonLinear = nonLinear.ToArray();
            double[] csiEffectiveStiffness = effectiveStiffness.ToArray();
            double[] csiEffectiveDamping = effectiveDamping.ToArray();
            double[] csiInitialStiffness = initialStiffness.ToArray();
            double[] csiNonLinearDamping = nonLinearDamping.ToArray();
            double[] csiNonLinearDampingExponent = nonLinearDampingExponent.ToArray();
            double[] csiNonLinearForceLimit = nonLinearForceLimit.ToArray();

            _callCode = _sapModel.PropLink.SetDamperLinearExponential(name,
                ref csiDegreesOfFreedom, ref csiFixity, ref csiNonLinear,
                ref csiEffectiveStiffness, ref csiEffectiveDamping, ref csiInitialStiffness, ref csiNonLinearDamping,
                ref csiNonLinearDampingExponent, ref csiNonLinearForceLimit,
                distanceFromJEndToU2Spring, distanceFromJEndToU3Spring, notes, GUID);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }
        }
#endif


        /// <summary>
        /// This function retrieves link property data for a friction isolator-type link property.
        /// </summary>
        /// <param name="name">The name of an existing friction isolator-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property.
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property.
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property.
        /// The initial stiffness applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U1, U2 and U3 [F/L].
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="frictionCoefficientSlow">The friction coefficient at zero velocity terms for the link property.
        /// The friction coefficient applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="frictionCoefficientFast">The friction coefficient at fast velocity terms for the link property.
        /// The friction coefficient applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="slidingRate">The inverse of the characteristic sliding velocity terms for the link property.
        /// The sliding rate applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3 [s/L].
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="radiusOfContactSurface">The radius of the sliding contact surface terms for the link property.
        /// Inputting 0 means there is an infinite radius, that is, the slider is flat.
        /// The radius of contact surface applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3 [L].
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="damping">This is the nonlinear damping coefficient used for the axial translational degree of freedom, U1.
        /// This item applies for nonlinear analyses. [F/L].</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U2" /> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U3" /> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetFrictionIsolator(string name,
            ref DegreesOfFreedomLocal degreesOfFreedom,
            ref DegreesOfFreedomLocal fixity,
            ref DegreesOfFreedomLocal nonLinear,
            ref Stiffness effectiveStiffness,
            ref Stiffness effectiveDamping,
            ref Stiffness initialStiffness,
            ref Deformations frictionCoefficientSlow,
            ref Deformations frictionCoefficientFast,
            ref Deformations slidingRate,
            ref Deformations radiusOfContactSurface,
            ref double damping,
            ref double distanceFromJEndToU2Spring,
            ref double distanceFromJEndToU3Spring,
            ref string notes,
            ref string GUID)
        {
            bool[] csiDegreesOfFreedom = new bool[0];
            bool[] csiFixity = new bool[0];
            bool[] csiNonLinear = new bool[0];
            double[] csiEffectiveStiffness = new double[0];
            double[] csiEffectiveDamping = new double[0];
            double[] csiInitialStiffness = new double[0];
            double[] csiFrictionCoefficientSlow = new double[0];
            double[] csiFrictionCoefficientFast = new double[0];
            double[] csiSlidingRate = new double[0];
            double[] csiRadiusOfContactSurface = new double[0];

            _callCode = _sapModel.PropLink.GetFrictionIsolator(name,
                ref csiDegreesOfFreedom, ref csiFixity, ref csiNonLinear,
                ref csiEffectiveStiffness, ref csiEffectiveDamping, ref csiInitialStiffness,
                ref csiFrictionCoefficientSlow, ref csiFrictionCoefficientFast, ref csiSlidingRate,
                ref csiRadiusOfContactSurface,
                ref damping, ref distanceFromJEndToU2Spring, ref distanceFromJEndToU3Spring, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }

            degreesOfFreedom.FromArray(csiDegreesOfFreedom);
            fixity.FromArray(csiFixity);
            nonLinear.FromArray(csiNonLinear);
            effectiveStiffness.FromArray(csiEffectiveStiffness);
            effectiveDamping.FromArray(csiEffectiveDamping);
            initialStiffness.FromArray(csiInitialStiffness);
            frictionCoefficientSlow.FromArray(csiFrictionCoefficientSlow);
            frictionCoefficientFast.FromArray(csiFrictionCoefficientFast);
            slidingRate.FromArray(csiSlidingRate);
            radiusOfContactSurface.FromArray(csiRadiusOfContactSurface);
        }

        /// <summary>
        /// This function initializes a friction isolator-type link property.
        /// If this function is called for an existing link property, all items for the property are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing friction isolator-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property.
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property.
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property.
        /// The initial stiffness applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U1, U2 and U3 [F/L].
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="frictionCoefficientSlow">The friction coefficient at zero velocity terms for the link property.
        /// The friction coefficient applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="frictionCoefficientFast">The friction coefficient at fast velocity terms for the link property.
        /// The friction coefficient applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="slidingRate">The inverse of the characteristic sliding velocity terms for the link property.
        /// The sliding rate applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3 [s/L].
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="radiusOfContactSurface">The radius of the sliding contact surface terms for the link property.
        /// Inputting 0 means there is an infinite radius, that is, the slider is flat.
        /// The radius of contact surface applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3 [L].
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="damping">This is the nonlinear damping coefficient used for the axial translational degree of freedom, U1.
        /// This item applies for nonlinear analyses. [F/L].</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U2" /> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U3" /> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetFrictionIsolator(string name,
            DegreesOfFreedomLocal degreesOfFreedom,
            DegreesOfFreedomLocal fixity,
            DegreesOfFreedomLocal nonLinear,
            Stiffness effectiveStiffness,
            Stiffness effectiveDamping,
            Stiffness initialStiffness,
            Deformations frictionCoefficientSlow,
            Deformations frictionCoefficientFast,
            Deformations slidingRate,
            Deformations radiusOfContactSurface,
            double damping,
            double distanceFromJEndToU2Spring,
            double distanceFromJEndToU3Spring,
            string notes = "",
            string GUID = "")
        {
            bool[] csiDegreesOfFreedom = degreesOfFreedom.ToArray();
            bool[] csiFixity = fixity.ToArray();
            bool[] csiNonLinear = nonLinear.ToArray();
            double[] csiEffectiveStiffness = effectiveStiffness.ToArray();
            double[] csiEffectiveDamping = effectiveDamping.ToArray();
            double[] csiInitialStiffness = initialStiffness.ToArray();
            double[] csiFrictionCoefficientSlow = frictionCoefficientSlow.ToArray();
            double[] csiFrictionCoefficientFast = frictionCoefficientFast.ToArray();
            double[] csiSlidingRate = slidingRate.ToArray();
            double[] csiRadiusOfContactSurface = radiusOfContactSurface.ToArray();

            _callCode = _sapModel.PropLink.SetFrictionIsolator(name,
                ref csiDegreesOfFreedom, ref csiFixity, ref csiNonLinear,
                ref csiEffectiveStiffness, ref csiEffectiveDamping, ref csiInitialStiffness,
                ref csiFrictionCoefficientSlow, ref csiFrictionCoefficientFast, ref csiSlidingRate,
                ref csiRadiusOfContactSurface,
                damping, distanceFromJEndToU2Spring, distanceFromJEndToU3Spring, notes, GUID);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }
        }



        /// <summary>
        /// This function retrieves link property data for a gap-type link property.
        /// </summary>
        /// <param name="name">The name of an existing gap-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property.
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property.
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property.
        /// The initial stiffness applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="initialGap">Initial gap opening terms for the link property
        /// The initial gap applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U2" /> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U3" /> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetGap(string name,
            ref DegreesOfFreedomLocal degreesOfFreedom,
            ref DegreesOfFreedomLocal fixity,
            ref DegreesOfFreedomLocal nonLinear,
            ref Stiffness effectiveStiffness,
            ref Stiffness effectiveDamping,
            ref Stiffness initialStiffness,
            ref Deformations initialGap,
            ref double distanceFromJEndToU2Spring,
            ref double distanceFromJEndToU3Spring,
            ref string notes,
            ref string GUID)
        {
            bool[] csiDegreesOfFreedom = new bool[0];
            bool[] csiFixity = new bool[0];
            bool[] csiNonLinear = new bool[0];
            double[] csiEffectiveStiffness = new double[0];
            double[] csiEffectiveDamping = new double[0];
            double[] csiInitialStiffness = new double[0];
            double[] csiInitialGap = new double[0];

            _callCode = _sapModel.PropLink.GetGap(name,
                ref csiDegreesOfFreedom, ref csiFixity, ref csiNonLinear,
                ref csiEffectiveStiffness, ref csiEffectiveDamping, ref csiInitialStiffness, ref csiInitialGap,
                ref distanceFromJEndToU2Spring, ref distanceFromJEndToU3Spring, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }

            degreesOfFreedom.FromArray(csiDegreesOfFreedom);
            fixity.FromArray(csiFixity);
            nonLinear.FromArray(csiNonLinear);
            effectiveStiffness.FromArray(csiEffectiveStiffness);
            effectiveDamping.FromArray(csiEffectiveDamping);
            initialStiffness.FromArray(csiInitialStiffness);
            initialGap.FromArray(csiInitialGap);
        }

        /// <summary>
        /// This function initializes a gap-type link property.
        /// If this function is called for an existing link property, all items for the property are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing gap-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property.
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property.
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property.
        /// The initial stiffness applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="initialGap">Initial gap opening terms for the link property
        /// The initial gap applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U2" /> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U3" /> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetGap(string name,
            DegreesOfFreedomLocal degreesOfFreedom,
            DegreesOfFreedomLocal fixity,
            DegreesOfFreedomLocal nonLinear,
            Stiffness effectiveStiffness,
            Stiffness effectiveDamping,
            Stiffness initialStiffness,
            Deformations initialGap,
            double distanceFromJEndToU2Spring,
            double distanceFromJEndToU3Spring,
            string notes = "",
            string GUID = "")
        {
            bool[] csiDegreesOfFreedom = degreesOfFreedom.ToArray();
            bool[] csiFixity = fixity.ToArray();
            bool[] csiNonLinear = nonLinear.ToArray();
            double[] csiEffectiveStiffness = effectiveStiffness.ToArray();
            double[] csiEffectiveDamping = effectiveDamping.ToArray();
            double[] csiInitialStiffness = initialStiffness.ToArray();
            double[] csiInitialGap = initialGap.ToArray();

            _callCode = _sapModel.PropLink.SetGap(name,
                ref csiDegreesOfFreedom, ref csiFixity, ref csiNonLinear,
                ref csiEffectiveStiffness, ref csiEffectiveDamping, ref csiInitialStiffness, ref csiInitialGap,
                distanceFromJEndToU2Spring, distanceFromJEndToU3Spring, notes, GUID);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }
        }



        /// <summary>
        /// This function retrieves link property data for a hook-type link property.
        /// </summary>
        /// <param name="name">The name of an existing exponential hook-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property.
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property.
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property.
        /// The initial stiffness applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="initialGap">Initial hook opening terms for the link property
        /// The initial hook opening applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U2" /> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U3" /> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetHook(string name,
            ref DegreesOfFreedomLocal degreesOfFreedom,
            ref DegreesOfFreedomLocal fixity,
            ref DegreesOfFreedomLocal nonLinear,
            ref Stiffness effectiveStiffness,
            ref Stiffness effectiveDamping,
            ref Stiffness initialStiffness,
            ref Deformations initialGap,
            ref double distanceFromJEndToU2Spring,
            ref double distanceFromJEndToU3Spring,
            ref string notes,
            ref string GUID)
        {
            bool[] csiDegreesOfFreedom = new bool[0];
            bool[] csiFixity = new bool[0];
            bool[] csiNonLinear = new bool[0];
            double[] csiEffectiveStiffness = new double[0];
            double[] csiEffectiveDamping = new double[0];
            double[] csiInitialStiffness = new double[0];
            double[] csiInitialGap = new double[0];

            _callCode = _sapModel.PropLink.GetHook(name,
                ref csiDegreesOfFreedom, ref csiFixity, ref csiNonLinear,
                ref csiEffectiveStiffness, ref csiEffectiveDamping, ref csiInitialStiffness, ref csiInitialGap,
                ref distanceFromJEndToU2Spring, ref distanceFromJEndToU3Spring, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }

            degreesOfFreedom.FromArray(csiDegreesOfFreedom);
            fixity.FromArray(csiFixity);
            nonLinear.FromArray(csiNonLinear);
            effectiveStiffness.FromArray(csiEffectiveStiffness);
            effectiveDamping.FromArray(csiEffectiveDamping);
            initialStiffness.FromArray(csiInitialStiffness);
            initialGap.FromArray(csiInitialGap);
        }

        /// <summary>
        /// This function initializes a hook-type link property.
        /// If this function is called for an existing link property, all items for the property are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing hook-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property.
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property.
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property.
        /// The initial stiffness applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="initialGap">Initial hook opening terms for the link property
        /// The initial hook opening applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U2" /> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U3" /> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetHook(string name,
            DegreesOfFreedomLocal degreesOfFreedom,
            DegreesOfFreedomLocal fixity,
            DegreesOfFreedomLocal nonLinear,
            Stiffness effectiveStiffness,
            Stiffness effectiveDamping,
            Stiffness initialStiffness,
            Deformations initialGap,
            double distanceFromJEndToU2Spring,
            double distanceFromJEndToU3Spring,
            string notes = "",
            string GUID = "")
        {
            bool[] csiDegreesOfFreedom = degreesOfFreedom.ToArray();
            bool[] csiFixity = fixity.ToArray();
            bool[] csiNonLinear = nonLinear.ToArray();
            double[] csiEffectiveStiffness = effectiveStiffness.ToArray();
            double[] csiEffectiveDamping = effectiveDamping.ToArray();
            double[] csiInitialStiffness = initialStiffness.ToArray();
            double[] csiInitialGap = initialGap.ToArray();

            _callCode = _sapModel.PropLink.SetHook(name,
                ref csiDegreesOfFreedom, ref csiFixity, ref csiNonLinear,
                ref csiEffectiveStiffness, ref csiEffectiveDamping, ref csiInitialStiffness, ref csiInitialGap,
                distanceFromJEndToU2Spring, distanceFromJEndToU3Spring, notes, GUID);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }
        }



        /// <summary>
        /// This function retrieves link property data for a linear-type link property.
        /// </summary>
        /// <param name="name">The name of an existing linear-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property when <paramref name="isStiffnessCoupled" /> = False.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property when <paramref name="isDampingCoupled" /> = False.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveStiffnessCoupled">Effective coupled stiffness terms for the link property when <paramref name="isStiffnessCoupled" /> = True.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveDampingCoupled">Effective coupled damping terms for the link property when <paramref name="isDampingCoupled" /> = True.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="isStiffnessCoupled">True: Link effective stiffness, Ke, is coupled.
        /// Use the <paramref name="effectiveDampingCoupled" /> parameter.
        /// False: Use the <paramref name="effectiveStiffness" /> parameter.</param>
        /// <param name="isDampingCoupled">True: Link effective damping, Ce, is coupled.
        /// Use the <paramref name="effectiveStiffnessCoupled" /> parameter.
        /// False: Use the <paramref name="effectiveDamping" /> parameter.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U2" /> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U3" /> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLinear(string name,
            ref DegreesOfFreedomLocal degreesOfFreedom,
            ref DegreesOfFreedomLocal fixity,
            ref Stiffness effectiveStiffness,
            ref Stiffness effectiveDamping,
            ref StiffnessCoupled effectiveStiffnessCoupled,
            ref StiffnessCoupled effectiveDampingCoupled,
            ref bool isStiffnessCoupled,
            ref bool isDampingCoupled,
            ref double distanceFromJEndToU2Spring,
            ref double distanceFromJEndToU3Spring,
            ref string notes,
            ref string GUID)
        {
            bool[] csiDegreesOfFreedom = new bool[0];
            bool[] csiFixity = new bool[0];
            double[] csiEffectiveStiffness = new double[0];
            double[] csiEffectiveDamping = new double[0];

            _callCode = _sapModel.PropLink.GetLinear(name,
                ref csiDegreesOfFreedom, ref csiFixity,
                ref csiEffectiveStiffness, ref csiEffectiveDamping,
                ref distanceFromJEndToU2Spring, ref distanceFromJEndToU3Spring, ref isStiffnessCoupled,
                ref isDampingCoupled, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }

            degreesOfFreedom.FromArray(csiDegreesOfFreedom);
            fixity.FromArray(csiFixity);
            if (isStiffnessCoupled)
            {
                effectiveStiffness.FromArray(csiEffectiveStiffness);
            }
            else
            {
                effectiveStiffnessCoupled.FromArray(csiEffectiveStiffness);
            }

            if (isDampingCoupled)
            {
                effectiveDamping.FromArray(csiEffectiveDamping);
            }
            else
            {
                effectiveDampingCoupled.FromArray(csiEffectiveDamping);
            }
        }


        /// <summary>
        /// This function initializes a linear-type link property.
        /// If this function is called for an existing link property, all items for the property are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing linear-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property when <paramref name="isStiffnessCoupled" /> = False.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property when <paramref name="isDampingCoupled" /> = False.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveStiffnessCoupled">Effective coupled stiffness terms for the link property when <paramref name="isStiffnessCoupled" /> = True.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveDampingCoupled">Effective coupled damping terms for the link property when <paramref name="isDampingCoupled" /> = True.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="isStiffnessCoupled">True: Link effective stiffness, Ke, is coupled.
        /// Use the <paramref name="effectiveDampingCoupled" /> parameter.
        /// False: Use the <paramref name="effectiveStiffness" /> parameter.</param>
        /// <param name="isDampingCoupled">True: Link effective damping, Ce, is coupled.
        /// Use the <paramref name="effectiveStiffnessCoupled" /> parameter.
        /// False: Use the <paramref name="effectiveDamping" /> parameter.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U2" /> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U3" /> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLinear(string name,
            DegreesOfFreedomLocal degreesOfFreedom,
            DegreesOfFreedomLocal fixity,
            Stiffness effectiveStiffness,
            Stiffness effectiveDamping,
            StiffnessCoupled effectiveStiffnessCoupled,
            StiffnessCoupled effectiveDampingCoupled,
            bool isStiffnessCoupled,
            bool isDampingCoupled,
            double distanceFromJEndToU2Spring,
            double distanceFromJEndToU3Spring,
            string notes = "",
            string GUID = "")
        {
            bool[] csiDegreesOfFreedom = degreesOfFreedom.ToArray();
            bool[] csiFixity = fixity.ToArray();

            double[] csiEffectiveStiffness;
            csiEffectiveStiffness = isStiffnessCoupled
                ? effectiveStiffness.ToArray()
                : effectiveStiffnessCoupled.ToArray();

            double[] csiEffectiveDamping = effectiveDamping.ToArray();
            csiEffectiveDamping = isDampingCoupled ? effectiveDamping.ToArray() : effectiveDampingCoupled.ToArray();

            _callCode = _sapModel.PropLink.SetLinear(name,
                ref csiDegreesOfFreedom, ref csiFixity,
                ref csiEffectiveStiffness, ref csiEffectiveDamping,
                distanceFromJEndToU2Spring, distanceFromJEndToU3Spring, isStiffnessCoupled, isDampingCoupled, notes,
                GUID);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }
        }





        /// <summary>
        /// This function retrieves link property data for a multilinear elastic-type link property
        /// </summary>
        /// <param name="name">The name of an existing exponential damper-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property.
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property.
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U2" /> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U3" /> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetMultiLinearElastic(string name,
            out DegreesOfFreedomLocal degreesOfFreedom,
            out DegreesOfFreedomLocal fixity,
            out DegreesOfFreedomLocal nonLinear,
            out Stiffness effectiveStiffness,
            out Stiffness effectiveDamping,
            out double distanceFromJEndToU2Spring,
            out double distanceFromJEndToU3Spring,
            out string notes,
            out string GUID)
        {
            bool[] csiDegreesOfFreedom = new bool[0];
            bool[] csiFixity = new bool[0];
            bool[] csiNonLinear = new bool[0];
            double[] csiEffectiveStiffness = new double[0];
            double[] csiEffectiveDamping = new double[0];
            distanceFromJEndToU2Spring = 0;
            distanceFromJEndToU3Spring = 0;
            notes = "";
            GUID = "";

            _callCode = _sapModel.PropLink.GetMultiLinearElastic(name,
                ref csiDegreesOfFreedom, ref csiFixity, ref csiNonLinear,
                ref csiEffectiveStiffness, ref csiEffectiveDamping,
                ref distanceFromJEndToU2Spring, ref distanceFromJEndToU3Spring, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }

            degreesOfFreedom = new DegreesOfFreedomLocal();
            degreesOfFreedom.FromArray(csiDegreesOfFreedom);

            fixity = new DegreesOfFreedomLocal();
            fixity.FromArray(csiFixity);

            nonLinear = new DegreesOfFreedomLocal();
            nonLinear.FromArray(csiNonLinear);

            effectiveStiffness = new Stiffness();
            effectiveStiffness.FromArray(csiEffectiveStiffness);

            effectiveDamping = new Stiffness();
            effectiveDamping.FromArray(csiEffectiveDamping);
        }

        /// <summary>
        /// This function initializes a multilinear elastic-type link property.
        /// If this function is called for an existing link property, all items for the property are reset to their default value
        /// </summary>
        /// <param name="name">The name of an existing exponential damper-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property.
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property.
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U2" /> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U3" /> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetMultiLinearElastic(string name,
            DegreesOfFreedomLocal degreesOfFreedom,
            DegreesOfFreedomLocal fixity,
            DegreesOfFreedomLocal nonLinear,
            Stiffness effectiveStiffness,
            Stiffness effectiveDamping,
            double distanceFromJEndToU2Spring,
            double distanceFromJEndToU3Spring,
            string notes = "",
            string GUID = "")
        {
            bool[] csiDegreesOfFreedom = degreesOfFreedom.ToArray();
            bool[] csiFixity = fixity.ToArray();
            bool[] csiNonLinear = nonLinear.ToArray();
            double[] csiEffectiveStiffness = effectiveStiffness.ToArray();
            double[] csiEffectiveDamping = effectiveDamping.ToArray();

            _callCode = _sapModel.PropLink.SetMultiLinearElastic(name,
                ref csiDegreesOfFreedom, ref csiFixity, ref csiNonLinear,
                ref csiEffectiveStiffness, ref csiEffectiveDamping,
                distanceFromJEndToU2Spring, distanceFromJEndToU3Spring, notes, GUID);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }
        }



        /// <summary>
        /// This function retrieves link property data for a multilinear plastic-type link property
        /// </summary>
        /// <param name="name">The name of an existing exponential damper-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property.
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property.
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U2" /> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U3" /> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetMultiLinearPlastic(string name,
            out DegreesOfFreedomLocal degreesOfFreedom,
            out DegreesOfFreedomLocal fixity,
            out DegreesOfFreedomLocal nonLinear,
            out Stiffness effectiveStiffness,
            out Stiffness effectiveDamping,
            out double distanceFromJEndToU2Spring,
            out double distanceFromJEndToU3Spring,
            out string notes,
            out string GUID)
        {
            bool[] csiDegreesOfFreedom = new bool[0];
            bool[] csiFixity = new bool[0];
            bool[] csiNonLinear = new bool[0];
            double[] csiEffectiveStiffness = new double[0];
            double[] csiEffectiveDamping = new double[0];
            distanceFromJEndToU2Spring = 0;
            distanceFromJEndToU3Spring = 0;
            notes = "";
            GUID = "";

            _callCode = _sapModel.PropLink.GetMultiLinearPlastic(name,
                ref csiDegreesOfFreedom, ref csiFixity, ref csiNonLinear,
                ref csiEffectiveStiffness, ref csiEffectiveDamping,
                ref distanceFromJEndToU2Spring, ref distanceFromJEndToU3Spring, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }

            degreesOfFreedom = new DegreesOfFreedomLocal();
            degreesOfFreedom.FromArray(csiDegreesOfFreedom);

            fixity = new DegreesOfFreedomLocal();
            fixity.FromArray(csiFixity);

            nonLinear = new DegreesOfFreedomLocal();
            nonLinear.FromArray(csiNonLinear);

            effectiveStiffness = new Stiffness();
            effectiveStiffness.FromArray(csiEffectiveStiffness);

            effectiveDamping = new Stiffness();
            effectiveDamping.FromArray(csiEffectiveDamping);
        }

        /// <summary>
        /// This function initializes a multilinear plastic-type link property.
        /// If this function is called for an existing link property, all items for the property are reset to their default values
        /// </summary>
        /// <param name="name">The name of an existing exponential damper-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property.
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property.
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U2" /> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U3" /> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetMultiLinearPlastic(string name,
            DegreesOfFreedomLocal degreesOfFreedom,
            DegreesOfFreedomLocal fixity,
            DegreesOfFreedomLocal nonLinear,
            Stiffness effectiveStiffness,
            Stiffness effectiveDamping,
            double distanceFromJEndToU2Spring,
            double distanceFromJEndToU3Spring,
            string notes = "",
            string GUID = "")
        {
            bool[] csiDegreesOfFreedom = degreesOfFreedom.ToArray();
            bool[] csiFixity = fixity.ToArray();
            bool[] csiNonLinear = nonLinear.ToArray();
            double[] csiEffectiveStiffness = effectiveStiffness.ToArray();
            double[] csiEffectiveDamping = effectiveDamping.ToArray();

            _callCode = _sapModel.PropLink.SetMultiLinearPlastic(name,
                ref csiDegreesOfFreedom, ref csiFixity, ref csiNonLinear,
                ref csiEffectiveStiffness, ref csiEffectiveDamping,
                distanceFromJEndToU2Spring, distanceFromJEndToU3Spring, notes, GUID);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }
        }



        /// <summary>
        /// This function retrieves the force-deformation data for a specified degree of freedom in multilinear elastic and multilinear plastic link properties.
        /// To successfully retrieve this data from the indicated link property, the following conditions must be met:
        /// 1.The link property must be multilinear elastic or multilinear plastic. TODO: OOAPI optional check GetMultilinearElastic, GetMultilinearPlastic.
        /// 2.The specified DOF must be active. TODO: OOAPI optional check GetMultilinearElastic, GetMultilinearPlastic.
        /// 3.The specified DOF must not be fixed. TODO: OOAPI optional check GetMultilinearElastic, GetMultilinearPlastic.
        /// 4.The specified DOF must be nonlinear. TODO: OOAPI optional check GetMultilinearElastic, GetMultilinearPlastic.
        /// </summary>
        /// <param name="name">The name of an existing multilinear elastic or multilinear plastic link property.</param>
        /// <param name="degreeOfFreedom">The degree of freedom to which the multilinear points apply.</param>
        /// <param name="forces">The force at each point.
        /// When <paramref name="degreeOfFreedom" /> is <see cref="eDegreeOfFreedom.U1" />, <see cref="eDegreeOfFreedom.U2" /> or <see cref="eDegreeOfFreedom.U3" />, this is a force [F].
        /// When <paramref name="degreeOfFreedom" /> is <see cref="eDegreeOfFreedom.R1" />, <see cref="eDegreeOfFreedom.R2" /> or <see cref="eDegreeOfFreedom.R3" />, this is a moment. [F*L]</param>
        /// <param name="displacements">The displacement at each point.
        /// When <paramref name="degreeOfFreedom" /> is <see cref="eDegreeOfFreedom.U1" />, <see cref="eDegreeOfFreedom.U2" /> or <see cref="eDegreeOfFreedom.U3" />, this is a translation [L].
        /// When <paramref name="degreeOfFreedom" /> is <see cref="eDegreeOfFreedom.R1" />, <see cref="eDegreeOfFreedom.R2" /> or <see cref="eDegreeOfFreedom.R3" />, this is a roration. [rad]</param>
        /// <param name="linkHysteresisType">Type of the link hysteresis.</param>
        /// <param name="alpha1">The Alpha1 hysteresis parameter.
        /// This item only applies to multilinear plastic link properties that have a <paramref name="linkHysteresisType" /> = <see cref="eLinkHysteresisType.Pivot" />.</param>
        /// <param name="alpha2">The Alpha2 hysteresis parameter.
        /// This item only applies to multilinear plastic link properties that have a <paramref name="linkHysteresisType" /> = <see cref="eLinkHysteresisType.Pivot" />.</param>
        /// <param name="beta1">The Beta1 hysteresis parameter.
        /// This item only applies to multilinear plastic link properties that have a <paramref name="linkHysteresisType" /> = <see cref="eLinkHysteresisType.Pivot" />.</param>
        /// <param name="beta2">The Beta2 hysteresis parameter.
        /// This item only applies to multilinear plastic link properties that have a <paramref name="linkHysteresisType" /> = <see cref="eLinkHysteresisType.Pivot" />.</param>
        /// <param name="eta">The Eta hysteresis parameter.
        /// This item only applies to multilinear plastic link properties that have a <paramref name="linkHysteresisType" /> = <see cref="eLinkHysteresisType.Pivot" />.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetMultiLinearPoints(string name,
            eDegreeOfFreedom degreeOfFreedom,
            out double[] forces,
            out double[] displacements,
            out eLinkHysteresisType linkHysteresisType,
            out double alpha1,
            out double alpha2,
            out double beta1,
            out double beta2,
            out double eta)
        {
            int csiDegreeOfFreedom = (int) degreeOfFreedom;
            int csiLinkHysteresisType = 0;
            forces = new double[0];
            displacements = new double[0];
            alpha1 = 0;
            alpha2 = 0;
            beta1 = 0;
            beta2 = 0;
            eta = 0;

            _callCode = _sapModel.PropLink.GetMultiLinearPoints(name,
                csiDegreeOfFreedom,
                ref _numberOfItems,
                ref forces,
                ref displacements,
                ref csiLinkHysteresisType,
                ref alpha1,
                ref alpha2,
                ref beta1,
                ref beta2,
                ref eta);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }

            linkHysteresisType = (eLinkHysteresisType) csiLinkHysteresisType;
        }

        /// <summary>
        /// This function sets the force-deformation data for a specified degree of freedom in multilinear elastic and multilinear plastic link properties.
        /// To successfully apply this data to the indicated link property, the following conditions must be met:
        /// 1.The link property must be multilinear elastic or multilinear plastic. TODO: OOAPI optional check GetMultilinearElastic, GetMultilinearPlastic.
        /// 2.The specified DOF must be active. TODO: OOAPI optional check GetMultilinearElastic, GetMultilinearPlastic.
        /// 3.The specified DOF must not be fixed. TODO: OOAPI optional check GetMultilinearElastic, GetMultilinearPlastic.
        /// 4.The specified DOF must be nonlinear. TODO: OOAPI optional check GetMultilinearElastic, GetMultilinearPlastic.
        /// TODO: Make parameter object for force-displacement curve that prevents or checks for the errors. Overloaded method skips the checks. Object stores values in tuples.
        /// TODO: Make parameter object for hysteresis types. Pivot type inherits and has extra values.
        /// </summary>
        /// <param name="name">The name of an existing multilinear elastic or multilinear plastic link property.</param>
        /// <param name="degreeOfFreedom">The degree of freedom to which the multilinear points apply.</param>
        /// <param name="forces">The force at each point.
        /// When <paramref name="degreeOfFreedom" /> is <see cref="eDegreeOfFreedom.U1" />, <see cref="eDegreeOfFreedom.U2" /> or <see cref="eDegreeOfFreedom.U3" />, this is a force [F].
        /// When <paramref name="degreeOfFreedom" /> is <see cref="eDegreeOfFreedom.R1" />, <see cref="eDegreeOfFreedom.R2" /> or <see cref="eDegreeOfFreedom.R3" />, this is a moment. [F*L].
        /// Forces must have at least 3 entries, with a 0 value corresponding with a displacement value of 0.</param>
        /// <param name="displacements">The displacement at each point.
        /// When <paramref name="degreeOfFreedom" /> is <see cref="eDegreeOfFreedom.U1" />, <see cref="eDegreeOfFreedom.U2" /> or <see cref="eDegreeOfFreedom.U3" />, this is a translation [L].
        /// When <paramref name="degreeOfFreedom" /> is <see cref="eDegreeOfFreedom.R1" />, <see cref="eDegreeOfFreedom.R2" /> or <see cref="eDegreeOfFreedom.R3" />, this is a roration. [rad].
        /// There must be at least 3 deformations with at least one negative, one 0, and one positive value.
        /// Deformations listed must be unique and ordered.</param>
        /// <param name="linkHysteresisType">Type of the link hysteresis.</param>
        /// <param name="alpha1">The Alpha1 hysteresis parameter.
        /// This item only applies to multilinear plastic link properties that have a <paramref name="linkHysteresisType" /> = <see cref="eLinkHysteresisType.Pivot" />.</param>
        /// <param name="alpha2">The Alpha2 hysteresis parameter.
        /// This item only applies to multilinear plastic link properties that have a <paramref name="linkHysteresisType" /> = <see cref="eLinkHysteresisType.Pivot" />.</param>
        /// <param name="beta1">The Beta1 hysteresis parameter.
        /// This item only applies to multilinear plastic link properties that have a <paramref name="linkHysteresisType" /> = <see cref="eLinkHysteresisType.Pivot" />.</param>
        /// <param name="beta2">The Beta2 hysteresis parameter.
        /// This item only applies to multilinear plastic link properties that have a <paramref name="linkHysteresisType" /> = <see cref="eLinkHysteresisType.Pivot" />.</param>
        /// <param name="eta">The Eta hysteresis parameter.
        /// This item only applies to multilinear plastic link properties that have a <paramref name="linkHysteresisType" /> = <see cref="eLinkHysteresisType.Pivot" />.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetMultiLinearPoints(string name,
            eDegreeOfFreedom degreeOfFreedom,
            double[] forces,
            double[] displacements,
            eLinkHysteresisType linkHysteresisType = eLinkHysteresisType.Kinematic,
            double alpha1 = 0,
            double alpha2 = 0,
            double beta1 = 0,
            double beta2 = 0,
            double eta = 0)
        {
            // Validation
            arraysLengthMatch(nameof(forces), forces.Length, nameof(displacements), displacements.Length);

            if (displacements.Length < 3)
                throw new CSiException($"Displacements has {forces.Length} entries, but must have at least 3.");
            int origin = Array.IndexOf(displacements, 0);
            if (origin < 0)
                throw new CSiException("Displacements must have at least one value of 0.");
            if (forces[origin] != 0)    // Note: In CSi application forms, it appears that there is no rounding tolerance enforced, so none is applied here for consistency.
                throw new CSiException("Forces must have a value of 0 where displacement is 0.");
            if (origin == 0 ||
                origin == (displacements.Length - 1) ||
                Math.Sign(displacements[origin - 1]) == Math.Sign(displacements[origin + 1]))
                throw new CSiException("Displacements must have at least one negative and one positive value.");
            if (!Validation.IsMonotonic(ref displacements))
                throw new CSiException("Displacements must have unique entries and be ordered.");


            _callCode = _sapModel.PropLink.SetMultiLinearPoints(name, 
                (int)degreeOfFreedom, 
                forces.Length, 
                ref forces, 
                ref displacements, 
                (int)linkHysteresisType, 
                alpha1, 
                alpha2, 
                beta1, 
                beta2, 
                eta);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function retrieves link property data for a plastic Wen-type link property.
        /// </summary>
        /// <param name="name">The name of an existing plastic Wen-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property.
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property.
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property.
        /// The initial stiffness applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="yieldForce">The yield force terms for the link property.
        /// The yield force applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="postYieldStiffnessRatio">post-yield stiffness ratio terms for the link property.
        /// The post-yield stiffness ratio applies for nonlinear analyses.
        /// It is the post-yield stiffness divided by the initial stiffness.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="yieldExponentTerm">The yield exponent terms for the link property.
        /// The yield exponent applies for nonlinear analyses.
        /// The yielding exponent that controls the sharpness of the transition from the initial stiffness to the yielded stiffness.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U2" /> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U3" /> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetPlasticWen(string name,
            ref DegreesOfFreedomLocal degreesOfFreedom,
            ref DegreesOfFreedomLocal fixity,
            ref DegreesOfFreedomLocal nonLinear,
            ref Stiffness effectiveStiffness,
            ref Stiffness effectiveDamping,
            ref Stiffness initialStiffness,
            ref Deformations yieldForce,
            ref Stiffness postYieldStiffnessRatio,
            ref Deformations yieldExponentTerm,
            ref double distanceFromJEndToU2Spring,
            ref double distanceFromJEndToU3Spring,
            ref string notes,
            ref string GUID)
        {
            bool[] csiDegreesOfFreedom = new bool[0];
            bool[] csiFixity = new bool[0];
            bool[] csiNonLinear = new bool[0];
            double[] csiEffectiveStiffness = new double[0];
            double[] csiEffectiveDamping = new double[0];
            double[] csiInitialStiffness = new double[0];
            double[] csiYieldForce = new double[0];
            double[] csiPostYieldStiffnessRatio = new double[0];
            double[] csiYieldExponentTerm = new double[0];

            _callCode = _sapModel.PropLink.GetPlasticWen(name,
                ref csiDegreesOfFreedom, ref csiFixity, ref csiNonLinear,
                ref csiEffectiveStiffness, ref csiEffectiveDamping, ref csiInitialStiffness, ref csiYieldForce, ref csiPostYieldStiffnessRatio, ref csiYieldExponentTerm,
                ref distanceFromJEndToU2Spring, ref distanceFromJEndToU3Spring, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            degreesOfFreedom.FromArray(csiDegreesOfFreedom);
            fixity.FromArray(csiFixity);
            nonLinear.FromArray(csiNonLinear);
            effectiveStiffness.FromArray(csiEffectiveStiffness);
            effectiveDamping.FromArray(csiEffectiveDamping);
            initialStiffness.FromArray(csiInitialStiffness);
            yieldForce.FromArray(csiYieldForce);
            postYieldStiffnessRatio.FromArray(csiPostYieldStiffnessRatio);
            yieldExponentTerm.FromArray(csiYieldExponentTerm);
        }

        /// <summary>
        /// This function initializes a plastic Wen-type link property.
        /// If this function is called for an existing link property, all items for the property are reset to their default values.
        /// </summary>
        /// <param name="name">The name of an existing plastic Wen-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property.
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property.
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property.
        /// The initial stiffness applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="yieldForce">The yield force terms for the link property.
        /// The yield force applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="postYieldStiffnessRatio">post-yield stiffness ratio terms for the link property.
        /// The post-yield stiffness ratio applies for nonlinear analyses.
        /// It is the post-yield stiffness divided by the initial stiffness.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="yieldExponentTerm">The yield exponent terms for the link property.
        /// The yield exponent applies for nonlinear analyses.
        /// The yielding exponent that controls the sharpness of the transition from the initial stiffness to the yielded stiffness.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U2" /> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U3" /> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetPlasticWen(string name,
            DegreesOfFreedomLocal degreesOfFreedom,
            DegreesOfFreedomLocal fixity,
            DegreesOfFreedomLocal nonLinear,
            Stiffness effectiveStiffness,
            Stiffness effectiveDamping,
            Stiffness initialStiffness,
            Deformations yieldForce,
            Stiffness postYieldStiffnessRatio,
            Deformations yieldExponentTerm,
            double distanceFromJEndToU2Spring,
            double distanceFromJEndToU3Spring,
            string notes = "",
            string GUID = "")
        {
            bool[] csiDegreesOfFreedom = degreesOfFreedom.ToArray();
            bool[] csiFixity = fixity.ToArray();
            bool[] csiNonLinear = nonLinear.ToArray();
            double[] csiEffectiveStiffness = effectiveStiffness.ToArray();
            double[] csiEffectiveDamping = effectiveDamping.ToArray();
            double[] csiInitialStiffness = initialStiffness.ToArray();
            double[] csiYieldForce = yieldForce.ToArray();
            double[] csiPostYieldStiffnessRatio = postYieldStiffnessRatio.ToArray();
            double[] csiYieldExponentTerm = yieldExponentTerm.ToArray();

            _callCode = _sapModel.PropLink.SetPlasticWen(name,
                ref csiDegreesOfFreedom, ref csiFixity, ref csiNonLinear,
                ref csiEffectiveStiffness, ref csiEffectiveDamping, ref csiInitialStiffness, ref csiYieldForce, ref csiPostYieldStiffnessRatio, ref csiYieldExponentTerm,
                distanceFromJEndToU2Spring, distanceFromJEndToU3Spring, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <summary>
        /// This function retrieves link property data for a rubber isolator-type link property.
        /// </summary>
        /// <param name="name">The name of an existing rubber isolator-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property.
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property.
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property.
        /// The initial stiffness applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="yieldForce">The yield force terms for the link property.
        /// The yield force applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="postYieldStiffnessRatio">The post-yield stiffness ratio terms for the link property.
        /// The post-yield stiffness ratio applies for nonlinear analyses.
        /// It is the post-yield stiffness divided by the initial stiffness.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U2" /> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U3" /> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetRubberIsolator(string name,
            ref DegreesOfFreedomLocal degreesOfFreedom,
            ref DegreesOfFreedomLocal fixity,
            ref DegreesOfFreedomLocal nonLinear,
            ref Stiffness effectiveStiffness,
            ref Stiffness effectiveDamping,
            ref Stiffness initialStiffness,
            ref Deformations yieldForce,
            ref Stiffness postYieldStiffnessRatio,
            ref double distanceFromJEndToU2Spring,
            ref double distanceFromJEndToU3Spring,
            ref string notes,
            ref string GUID)
        {
            bool[] csiDegreesOfFreedom = new bool[0];
            bool[] csiFixity = new bool[0];
            bool[] csiNonLinear = new bool[0];
            double[] csiEffectiveStiffness = new double[0];
            double[] csiEffectiveDamping = new double[0];
            double[] csiInitialStiffness = new double[0];
            double[] csiYieldForce = new double[0];
            double[] csiPostYieldStiffnessRatio = new double[0];
            double[] csiYieldExponentTerm = new double[0];

            _callCode = _sapModel.PropLink.GetRubberIsolator(name,
                ref csiDegreesOfFreedom, ref csiFixity, ref csiNonLinear,
                ref csiEffectiveStiffness, ref csiEffectiveDamping, ref csiInitialStiffness, ref csiYieldForce, ref csiPostYieldStiffnessRatio, 
                ref distanceFromJEndToU2Spring, ref distanceFromJEndToU3Spring, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            degreesOfFreedom.FromArray(csiDegreesOfFreedom);
            fixity.FromArray(csiFixity);
            nonLinear.FromArray(csiNonLinear);
            effectiveStiffness.FromArray(csiEffectiveStiffness);
            effectiveDamping.FromArray(csiEffectiveDamping);
            initialStiffness.FromArray(csiInitialStiffness);
            yieldForce.FromArray(csiYieldForce);
            postYieldStiffnessRatio.FromArray(csiPostYieldStiffnessRatio);
        }

        /// <summary>
        /// This function initializes a rubber isolator-type link property.
        /// If this function is called for an existing link property, all items for the property are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing rubber isolator-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property.
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property.
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property.
        /// The initial stiffness applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="yieldForce">The yield force terms for the link property.
        /// The yield force applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="postYieldStiffnessRatio">The post-yield stiffness ratio terms for the link property.
        /// The post-yield stiffness ratio applies for nonlinear analyses.
        /// It is the post-yield stiffness divided by the initial stiffness.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U2" /> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U3" /> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetRubberIsolator(string name,
            ref DegreesOfFreedomLocal degreesOfFreedom,
            ref DegreesOfFreedomLocal fixity,
            ref DegreesOfFreedomLocal nonLinear,
            ref Stiffness effectiveStiffness,
            ref Stiffness effectiveDamping,
            ref Stiffness initialStiffness,
            ref Deformations yieldForce,
            ref Stiffness postYieldStiffnessRatio,
            ref double distanceFromJEndToU2Spring,
            ref double distanceFromJEndToU3Spring,
            ref string notes,
            ref string GUID)
        {
            bool[] csiDegreesOfFreedom = degreesOfFreedom.ToArray();
            bool[] csiFixity = fixity.ToArray();
            bool[] csiNonLinear = nonLinear.ToArray();
            double[] csiEffectiveStiffness = effectiveStiffness.ToArray();
            double[] csiEffectiveDamping = effectiveDamping.ToArray();
            double[] csiInitialStiffness = initialStiffness.ToArray();
            double[] csiYieldForce = yieldForce.ToArray();
            double[] csiPostYieldStiffnessRatio = postYieldStiffnessRatio.ToArray();

            _callCode = _sapModel.PropLink.SetRubberIsolator(name,
                ref csiDegreesOfFreedom, ref csiFixity, ref csiNonLinear,
                ref csiEffectiveStiffness, ref csiEffectiveDamping, ref csiInitialStiffness, ref csiYieldForce, ref csiPostYieldStiffnessRatio, 
                distanceFromJEndToU2Spring, distanceFromJEndToU3Spring, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }





        /// <summary>
        /// This function retrieves link property data for a T/C friction isolator-type link property.
        /// </summary>
        /// <param name="name">The name of an existing T/C friction isolator-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property.
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property.
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property.
        /// The initial stiffness applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U1, U2 and U3 [F/L].
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="frictionCoefficientSlow">The friction coefficient at zero velocity terms for the link property.
        /// The friction coefficient applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="frictionCoefficientFast">The friction coefficient at fast velocity terms for the link property.
        /// The friction coefficient applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="slidingRate">The inverse of the characteristic sliding velocity terms for the link property.
        /// The sliding rate applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3 [s/L].
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="radiusOfContactSurface">The radius of the sliding contact surface terms for the link property.
        /// Inputting 0 means there is an infinite radius, that is, the slider is flat.
        /// The radius of contact surface applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3 [L].
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="frictionCoefficientSlowTension">The friction coefficient at zero velocity terms when U1 is in tension for the link property.
        /// The friction coefficient applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="frictionCoefficientFastTension">The friction coefficient at fast velocity terms when U1 is in tension for the link property.
        /// The friction coefficient applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="slidingRateTension">The inverse of the characteristic sliding velocity terms for when U1 is in tension the link property.
        /// The sliding rate applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3 [s/L].
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="axialTranslationTensionStiffness">The axial translational tension stiffness for the U1 degree of freedom.
        /// This item applies for nonlinear analyses. [F/L].</param>
        /// <param name="gapCompression">The U1 degree of freedom gap opening for compression.
        /// This item applies for nonlinear analyses. [L].</param>
        /// <param name="gapTension">The U1 degree of freedom gap opening for tension.
        /// This item applies for nonlinear analyses. [L].</param>
        /// <param name="damping">This is the nonlinear damping coefficient used for the axial translational degree of freedom, U1.
        /// This item applies for nonlinear analyses. [F/L].</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U2" /> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U3" /> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetTensionCompressionFrictionIsolator(string name,
            ref DegreesOfFreedomLocal degreesOfFreedom,
            ref DegreesOfFreedomLocal fixity,
            ref DegreesOfFreedomLocal nonLinear,
            ref Stiffness effectiveStiffness,
            ref Stiffness effectiveDamping,
            ref Stiffness initialStiffness,
            ref Deformations frictionCoefficientSlow,
            ref Deformations frictionCoefficientFast,
            ref Deformations slidingRate,
            ref Deformations radiusOfContactSurface,
            ref Deformations frictionCoefficientSlowTension,
            ref Deformations frictionCoefficientFastTension,
            ref Deformations slidingRateTension,
            ref double axialTranslationTensionStiffness,
            ref double gapCompression,
            ref double gapTension,
            ref double damping,
            ref double distanceFromJEndToU2Spring,
            ref double distanceFromJEndToU3Spring,
            ref string notes,
            ref string GUID)
        {
            bool[] csiDegreesOfFreedom = new bool[0];
            bool[] csiFixity = new bool[0];
            bool[] csiNonLinear = new bool[0];
            double[] csiEffectiveStiffness = new double[0];
            double[] csiEffectiveDamping = new double[0];
            double[] csiInitialStiffness = new double[0];
            double[] csiFrictionCoefficientSlow = new double[0];
            double[] csiFrictionCoefficientFast = new double[0];
            double[] csiSlidingRate = new double[0];
            double[] csiRadiusOfContactSurface = new double[0];
            double[] csiFrictionCoefficientSlowTension = new double[0];
            double[] csiFrictionCoefficientFastTension = new double[0];
            double[] csiSlidingRateTension = new double[0];

            _callCode = _sapModel.PropLink.GetTCFrictionIsolator(name,
                ref csiDegreesOfFreedom, ref csiFixity, ref csiNonLinear,
                ref csiEffectiveStiffness, ref csiEffectiveDamping, ref csiInitialStiffness,
                ref csiFrictionCoefficientSlow, ref csiFrictionCoefficientFast, ref csiSlidingRate, ref csiRadiusOfContactSurface,
                ref csiFrictionCoefficientSlowTension, ref csiFrictionCoefficientFastTension, ref csiSlidingRateTension,
                ref axialTranslationTensionStiffness, ref gapCompression, ref gapTension, ref damping, ref distanceFromJEndToU2Spring, ref distanceFromJEndToU3Spring, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            degreesOfFreedom.FromArray(csiDegreesOfFreedom);
            fixity.FromArray(csiFixity);
            nonLinear.FromArray(csiNonLinear);
            effectiveStiffness.FromArray(csiEffectiveStiffness);
            effectiveDamping.FromArray(csiEffectiveDamping);
            initialStiffness.FromArray(csiInitialStiffness);
            frictionCoefficientSlow.FromArray(csiFrictionCoefficientSlow);
            frictionCoefficientFast.FromArray(csiFrictionCoefficientFast);
            slidingRate.FromArray(csiSlidingRate);
            radiusOfContactSurface.FromArray(csiRadiusOfContactSurface);
            frictionCoefficientSlowTension.FromArray(csiFrictionCoefficientSlowTension);
            frictionCoefficientFastTension.FromArray(csiFrictionCoefficientFastTension);
            slidingRateTension.FromArray(csiSlidingRateTension);
        }

        /// <summary>
        /// This function initializes a T/C friction isolator-type link property.
        /// If this function is called for an existing link property, all items for the property are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing T/C friction isolator-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property.
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property.
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property.
        /// The initial stiffness applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U1, U2 and U3 [F/L].
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="frictionCoefficientSlow">The friction coefficient at zero velocity terms for the link property.
        /// The friction coefficient applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="frictionCoefficientFast">The friction coefficient at fast velocity terms for the link property.
        /// The friction coefficient applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="slidingRate">The inverse of the characteristic sliding velocity terms for the link property.
        /// The sliding rate applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3 [s/L].
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="radiusOfContactSurface">The radius of the sliding contact surface terms for the link property.
        /// Inputting 0 means there is an infinite radius, that is, the slider is flat.
        /// The radius of contact surface applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3 [L].
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="frictionCoefficientSlowTension">The friction coefficient at zero velocity terms when U1 is in tension for the link property.
        /// The friction coefficient applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="frictionCoefficientFastTension">The friction coefficient at fast velocity terms when U1 is in tension for the link property.
        /// The friction coefficient applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="slidingRateTension">The inverse of the characteristic sliding velocity terms for when U1 is in tension the link property.
        /// The sliding rate applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3 [s/L].
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="axialTranslationTensionStiffness">The axial translational tension stiffness for the U1 degree of freedom.
        /// This item applies for nonlinear analyses. [F/L].</param>
        /// <param name="gapCompression">The U1 degree of freedom gap opening for compression.
        /// This item applies for nonlinear analyses. [L].</param>
        /// <param name="gapTension">The U1 degree of freedom gap opening for tension.
        /// This item applies for nonlinear analyses. [L].</param>
        /// <param name="damping">This is the nonlinear damping coefficient used for the axial translational degree of freedom, U1.
        /// This item applies for nonlinear analyses. [F/L].</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U2" /> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U3" /> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetTensionCompressionFrictionIsolator(string name,
            DegreesOfFreedomLocal degreesOfFreedom,
            DegreesOfFreedomLocal fixity,
            DegreesOfFreedomLocal nonLinear,
            Stiffness effectiveStiffness,
            Stiffness effectiveDamping,
            Stiffness initialStiffness,
            Deformations frictionCoefficientSlow,
            Deformations frictionCoefficientFast,
            Deformations slidingRate,
            Deformations radiusOfContactSurface,
            Deformations frictionCoefficientSlowTension,
            Deformations frictionCoefficientFastTension,
            Deformations slidingRateTension,
            double axialTranslationTensionStiffness,
            double gapCompression,
            double gapTension,
            double damping,
            double distanceFromJEndToU2Spring,
            double distanceFromJEndToU3Spring,
            string notes = "",
            string GUID = "")
        {
            bool[] csiDegreesOfFreedom = degreesOfFreedom.ToArray();
            bool[] csiFixity = fixity.ToArray();
            bool[] csiNonLinear = nonLinear.ToArray();
            double[] csiEffectiveStiffness = effectiveStiffness.ToArray();
            double[] csiEffectiveDamping = effectiveDamping.ToArray();
            double[] csiInitialStiffness = initialStiffness.ToArray();
            double[] csiFrictionCoefficientSlow = frictionCoefficientSlow.ToArray();
            double[] csiFrictionCoefficientFast = frictionCoefficientFast.ToArray();
            double[] csiSlidingRate = slidingRate.ToArray();
            double[] csiRadiusOfContactSurface = radiusOfContactSurface.ToArray();
            double[] csiFrictionCoefficientSlowTension = frictionCoefficientSlowTension.ToArray();
            double[] csiFrictionCoefficientFastTension = frictionCoefficientFastTension.ToArray();
            double[] csiSlidingRateTension = slidingRateTension.ToArray();

            _callCode = _sapModel.PropLink.SetTCFrictionIsolator(name,
                ref csiDegreesOfFreedom, ref csiFixity, ref csiNonLinear,
                ref csiEffectiveStiffness, ref csiEffectiveDamping, ref csiInitialStiffness, 
                ref csiFrictionCoefficientSlow, ref csiFrictionCoefficientFast, ref csiSlidingRate, ref csiRadiusOfContactSurface,
                ref csiFrictionCoefficientSlowTension, ref csiFrictionCoefficientFastTension, ref csiSlidingRateTension,
                axialTranslationTensionStiffness, gapCompression, gapTension, damping, distanceFromJEndToU2Spring, distanceFromJEndToU3Spring, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function retrieves link property data for a Triple Pendulum Isolator type link property.
        /// </summary>
        /// <param name="name">The name of an existing or new link property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property.
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property.
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="k1">This is the axial compression stiffness for the U1 degree of freedom.
        /// This item applies for nonlinear analyses. [F/L].</param>
        /// <param name="damping">This is the nonlinear damping coefficient for the axial degree of freedom, U1, when it is in compression.
        /// This item applies for nonlinear analyses. [F/L].</param>
        /// <param name="initialNonlinearStiffness">The initial nonlinear stiffness (before sliding) for each sliding surface.
        /// Note that this item is applicable for degrees of freedom U2 and U3 only.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="frictionCoefficientSlow">The friction coefficient at zero velocity for each sliding surface when U1 is in compression.
        /// Note that this item is applicable for degrees of freedom U2 and U3 only.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="frictionCoefficientFast">The friction coefficient at fast velocity for each sliding surface when U1 is in compression.
        /// Note that this item is applicable for degrees of freedom U2 and U3 only.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="slidingRate">The inverse of the characteristic sliding velocity for the <paramref name="frictionCoefficientSlow" /> and <paramref name="frictionCoefficientFast" /> friction coefficients for each sliding surface. [s/L].
        /// This item applies for nonlinear analyses.
        /// Note that this item is applicable for degrees of freedom U2 and U3 only.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="radiusOfContactSurface">The radius for each sliding surface. [L].
        /// Inputting 0 means there is an infinite radius, that is, the slider is flat.
        /// This item applies for nonlinear analyses.
        /// Note that this item is applicable for degrees of freedom U2 and U3 only.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="maxSlidingDistance">The amount of displacement allowed before hitting a stiff limit for each sliding surface. [L].
        /// Inputting 0 means there is no stop.
        /// This item applies for nonlinear analyses.
        /// Note that this item is applicable for degrees of freedom U2 and U3 only.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="heightOuterSurface">The height (distance) between the outer sliding surfaces at zero displacement. [L].
        /// Note that this item is applicable for degrees of freedom U2 and U3 only.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="heightInnerSurface">The height (distance) between the inner sliding surfaces. [L].
        /// Note that this item is applicable for degrees of freedom U2 and U3 only.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U2" /> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U3" /> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetTriplePendulumIsolator(string name,
            ref DegreesOfFreedomLocal degreesOfFreedom,
            ref DegreesOfFreedomLocal fixity,
            ref DegreesOfFreedomLocal nonLinear,
            ref Stiffness effectiveStiffness,
            ref Stiffness effectiveDamping,
            ref double k1,
            ref double damping,
            ref StiffnessPendulum initialNonlinearStiffness,
            ref StiffnessPendulum frictionCoefficientSlow,
            ref StiffnessPendulum frictionCoefficientFast,
            ref StiffnessPendulum slidingRate,
            ref StiffnessPendulum radiusOfContactSurface,
            ref StiffnessPendulum maxSlidingDistance,
            ref double heightOuterSurface,
            ref double heightInnerSurface,
            ref double distanceFromJEndToU2Spring,
            ref double distanceFromJEndToU3Spring,
            ref string notes,
            ref string GUID)
        {
            bool[] csiDegreesOfFreedom = new bool[0];
            bool[] csiFixity = new bool[0];
            bool[] csiNonLinear = new bool[0];
            double[] csiEffectiveStiffness = new double[0];
            double[] csiEffectiveDamping = new double[0];
            double[] csiInitialNonlinearStiffness = new double[0];
            double[] csiFrictionCoefficientSlow = new double[0];
            double[] csiFrictionCoefficientFast = new double[0];
            double[] csiSlidingRate = new double[0];
            double[] csiRadiusOfContactSurface = new double[0];
            double[] csiMaxSlidingDistance = new double[0];

            _callCode = _sapModel.PropLink.GetTriplePendulumIsolator(name,
                ref csiDegreesOfFreedom, ref csiFixity, ref csiNonLinear,
                ref csiEffectiveStiffness, ref csiEffectiveDamping, ref k1, ref damping,
                ref csiInitialNonlinearStiffness, ref csiFrictionCoefficientSlow, ref csiFrictionCoefficientFast,
                ref csiSlidingRate, ref csiRadiusOfContactSurface, ref csiMaxSlidingDistance,
                ref heightOuterSurface, ref heightInnerSurface, ref distanceFromJEndToU2Spring,
                ref distanceFromJEndToU3Spring, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)){throw new CSiException(API_DEFAULT_ERROR_CODE);}

            degreesOfFreedom.FromArray(csiDegreesOfFreedom);
            fixity.FromArray(csiFixity);
            nonLinear.FromArray(csiNonLinear);
            effectiveStiffness.FromArray(csiEffectiveStiffness);
            effectiveDamping.FromArray(csiEffectiveDamping);
            initialNonlinearStiffness.FromArray(csiInitialNonlinearStiffness);
            frictionCoefficientSlow.FromArray(csiFrictionCoefficientSlow);
            frictionCoefficientFast.FromArray(csiFrictionCoefficientFast);
            slidingRate.FromArray(csiSlidingRate);
            radiusOfContactSurface.FromArray(csiRadiusOfContactSurface);
            maxSlidingDistance.FromArray(csiMaxSlidingDistance);
        }

        /// <summary>
        /// This function initializes a Triple Pendulum Isolator type link property.
        /// If this function is called for an existing link property, then all items for the property are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new link property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property.
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property.
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True and the corresponding <paramref name="fixity" /> = False.</param>
        /// <param name="k1">This is the axial compression stiffness for the U1 degree of freedom.
        /// This item applies for nonlinear analyses. [F/L].</param>
        /// <param name="damping">This is the nonlinear damping coefficient for the axial degree of freedom, U1, when it is in compression.
        /// This item applies for nonlinear analyses. [F/L].</param>
        /// <param name="initialNonlinearStiffness">The initial nonlinear stiffness (before sliding) for each sliding surface.
        /// Note that this item is applicable for degrees of freedom U2 and U3 only.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="frictionCoefficientSlow">The friction coefficient at zero velocity for each sliding surface when U1 is in compression.
        /// Note that this item is applicable for degrees of freedom U2 and U3 only.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="frictionCoefficientFast">The friction coefficient at fast velocity for each sliding surface when U1 is in compression.
        /// Note that this item is applicable for degrees of freedom U2 and U3 only.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="slidingRate">The inverse of the characteristic sliding velocity for the <paramref name="frictionCoefficientSlow" /> and <paramref name="frictionCoefficientFast" /> friction coefficients for each sliding surface. [s/L].
        /// This item applies for nonlinear analyses.
        /// Note that this item is applicable for degrees of freedom U2 and U3 only.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="radiusOfContactSurface">The radius for each sliding surface. [L].
        /// Inputting 0 means there is an infinite radius, that is, the slider is flat.
        /// This item applies for nonlinear analyses.
        /// Note that this item is applicable for degrees of freedom U2 and U3 only.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="maxSlidingDistance">The amount of displacement allowed before hitting a stiff limit for each sliding surface. [L].
        /// Inputting 0 means there is no stop.
        /// This item applies for nonlinear analyses.
        /// Note that this item is applicable for degrees of freedom U2 and U3 only.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="heightOuterSurface">The height (distance) between the outer sliding surfaces at zero displacement. [L].
        /// Note that this item is applicable for degrees of freedom U2 and U3 only.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="heightInnerSurface">The height (distance) between the inner sliding surfaces. [L].
        /// Note that this item is applicable for degrees of freedom U2 and U3 only.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom" /> = True, the corresponding <paramref name="fixity" /> = False, and the corresponding <paramref name="nonLinear" /> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U2" /> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring.
        /// This item applies only when <paramref name="degreesOfFreedom.U3" /> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetTriplePendulumIsolator(string name,
            DegreesOfFreedomLocal degreesOfFreedom,
            DegreesOfFreedomLocal fixity,
            DegreesOfFreedomLocal nonLinear,
            Stiffness effectiveStiffness,
            Stiffness effectiveDamping,
            double k1,
            double damping,
            StiffnessPendulum initialNonlinearStiffness,
            StiffnessPendulum frictionCoefficientSlow,
            StiffnessPendulum frictionCoefficientFast,
            StiffnessPendulum slidingRate,
            StiffnessPendulum radiusOfContactSurface,
            StiffnessPendulum maxSlidingDistance,
            double heightOuterSurface,
            double heightInnerSurface,
            double distanceFromJEndToU2Spring,
            double distanceFromJEndToU3Spring,
            string notes = "",
            string GUID = "")
        {
            bool[] csiDegreesOfFreedom = degreesOfFreedom.ToArray();
            bool[] csiFixity = fixity.ToArray();
            bool[] csiNonLinear = nonLinear.ToArray();
            double[] csiEffectiveStiffness = effectiveStiffness.ToArray();
            double[] csiEffectiveDamping = effectiveDamping.ToArray();
            double[] csiInitialNonlinearStiffness = initialNonlinearStiffness.ToArray();
            double[] csiFrictionCoefficientSlow = frictionCoefficientSlow.ToArray();
            double[] csiFrictionCoefficientFast = frictionCoefficientFast.ToArray();
            double[] csiSlidingRate = slidingRate.ToArray();
            double[] csiRadiusOfContactSurface = radiusOfContactSurface.ToArray();
            double[] csiMaxSlidingDistance = maxSlidingDistance.ToArray();


            _callCode = _sapModel.PropLink.SetTriplePendulumIsolator(name,
                ref csiDegreesOfFreedom, ref csiFixity, ref csiNonLinear,
                ref csiEffectiveStiffness, ref csiEffectiveDamping, k1, damping,
                ref csiInitialNonlinearStiffness, ref csiFrictionCoefficientSlow, ref csiFrictionCoefficientFast, ref csiSlidingRate, ref csiRadiusOfContactSurface, ref csiMaxSlidingDistance,
                heightOuterSurface, heightInnerSurface, distanceFromJEndToU2Spring, distanceFromJEndToU3Spring, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion
    }
}

