using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Implements the link properties in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    public interface ILinkProperties: IChangeableName, ICountable, IDeletable, IListableNames
    {
        /// <summary>
        /// This function retrieves the names of all defined link properties of the specified type.
        /// </summary>
        /// <param name="names">Link property names retrieved by the program.</param>
        /// <param name="linkType">The link type to filter the name list by.</param>
        void GetNameList(out string[] names,
            eLinkPropertyType linkType);

        /// <summary>
        /// This function retrieves the property type for the specified link property.
        /// </summary>
        /// <param name="name">The name of an existing link property.</param>
        /// <param name="linkType">Type of the link.</param>
        void GetLinkType(string name,
            ref eLinkPropertyType linkType);




        /// <summary>
        /// This function retrieves weight and mass data for a link property.
        /// </summary>
        /// <param name="name">The name of an existing link property.</param>
        /// <param name="weight">The weight of the link. [F].</param>
        /// <param name="massTranslational">The translational mass of the link. [M].</param>
        /// <param name="massR1">The rotational inertia of the link about its local 1 axis. [M*L^2].</param>
        /// <param name="massR2">The rotational inertia of the link about its local 2 axis. [M*L^2].</param>
        /// <param name="massR3">The rotational inertia of the link about its local 3 axis. [M*L^2].</param>
        void GetWeightAndMass(string name,
            ref double weight,
            ref double massTranslational,
            ref double massR1,
            ref double massR2,
            ref double massR3);

        /// <summary>
        /// This function assigns weight and mass values to a link property.
        /// </summary>
        /// <param name="name">The name of an existing link property.</param>
        /// <param name="weight">The weight of the link. [F].</param>
        /// <param name="massTranslational">The translational mass of the link. [M].</param>
        /// <param name="massR1">The rotational inertia of the link about its local 1 axis. [M*L^2].</param>
        /// <param name="massR2">The rotational inertia of the link about its local 2 axis. [M*L^2].</param>
        /// <param name="massR3">The rotational inertia of the link about its local 3 axis. [M*L^2].</param>
        void SetWeightAndMass(string name,
            double weight,
            double massTranslational,
            double massR1,
            double massR2,
            double massR3);




        /// <summary>
        /// This function retrieves P-delta parameters for a link property.
        /// </summary>
        /// <param name="name">The name of an existing link property.</param>
        /// <param name="pDeltaParameters">The P-delta moment parameters at each end of the link.</param>
        void GetPDelta(string name,
            ref PDeltaParameters pDeltaParameters);

        /// <summary>
        /// This function assigns P-delta parameters to a link property.
        /// </summary>
        /// <param name="name">The name of an existing link property.</param>
        /// <param name="pDeltaParameters">The P-delta moment parameters at each end of the link</param>
        void SetPDelta(string name,
            PDeltaParameters pDeltaParameters);



        /// <summary>
        /// This function retrieves length and area values for a link property that are used if the link property is specified in line and area spring assignments.
        /// </summary>
        /// <param name="name">The name of an existing link prope.</param>
        /// <param name="lengthDefined">The link property is defined for this length in a line (frame) spring. [L].</param>
        /// <param name="areaDefined">The link property is defined for this area in an area spring. [L^2].</param>
        void GetSpringData(string name,
            ref double lengthDefined,
            ref double areaDefined);

        /// <summary>
        /// This function assigns length and area values to a link property that are used if the link property is specified in line and area spring assignments.
        /// </summary>
        /// <param name="name">The name of an existing link prope.</param>
        /// <param name="lengthDefined">The link property is defined for this length in a line (frame) spring. [L].</param>
        /// <param name="areaDefined">The link property is defined for this area in an area spring. [L^2].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        void SetSpringData(string name,
            double lengthDefined,
            double areaDefined);

        /// <summary>
        /// This function retrieves link property data for an exponential damper-type link property.
        /// </summary>
        /// <param name="name">The name of an existing exponential damper-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property. 
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property. 
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property. 
        /// The initial stiffness applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="nonLinearDamping">Nonlinear damping coefficient terms for the link property. 
        /// The nonlinear damping coefficient applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="nonLinearDampingExponent">Nonlinear damping exponent terms. 
        /// The nonlinear damping exponent applies for nonlinear analyses. 
        /// It is applied to the velocity across the damper in the equation of motion.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U2"/> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U3"/> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        void GetDamper(string name,
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
            ref string GUID);

        /// <summary>
        /// This function initializes an exponential damper-type link property. 
        /// If this function is called for an existing link property, all items for the property are reset to their default values.
        /// </summary>
        /// <param name="name">The name of an existing exponential damper-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property. 
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property. 
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property. 
        /// The initial stiffness applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="nonLinearDamping">Nonlinear damping coefficient terms for the link property. 
        /// The nonlinear damping coefficient applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="nonLinearDampingExponent">Nonlinear damping exponent terms. 
        /// The nonlinear damping exponent applies for nonlinear analyses. 
        /// It is applied to the velocity across the damper in the equation of motion.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U2"/> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U3"/> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        void SetDamper(string name,
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
            string GUID = "");



        /// <summary>
        /// This function retrieves link property data for a bilinear damper-type link property.
        /// </summary>
        /// <param name="name">The name of an existing bilinear damper-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property. 
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property. 
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property. 
        /// The initial stiffness applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="nonLinearInitialDamping">Nonlinear initial damping coefficient terms for the link property. 
        /// The nonlinear initial damping coefficient applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="nonLinearYieldedDamping">Nonlinear yielded damping exponent terms. 
        /// The nonlinear yielded damping exponent applies for nonlinear analyses. 
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="nonLinearForceLimit">Nonlinear linear force limit terms for the link property. 
        /// The linear force limit applies for nonlinear analyses. 
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U2"/> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U3"/> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        void GetDamperBilinear(string name,
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
            ref string GUID);

        /// <summary>
        /// This function initializes a bilinear damper-type link property. 
        /// If this function is called for an existing link property, all items for the property are reset to their default values.
        /// </summary>
        /// <param name="name">The name of an existing bilinear damper-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property. 
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property. 
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property. 
        /// The initial stiffness applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="nonLinearInitialDamping">Nonlinear initial damping coefficient terms for the link property. 
        /// The nonlinear initial damping coefficient applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="nonLinearYieldedDamping">Nonlinear yielded damping exponent terms. 
        /// The nonlinear yielded damping exponent applies for nonlinear analyses. 
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="nonLinearForceLimit">Nonlinear linear force limit terms for the link property. 
        /// The linear force limit applies for nonlinear analyses. 
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U2"/> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U3"/> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        void SetDamperBilinear(string name,
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
            string GUID = "");



        /// <summary>
        /// This function retrieves link property data for a friction spring damper-type link property.
        /// </summary>
        /// <param name="name">The name of an existing friction spring damper-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property. 
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property. 
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property. 
        /// The initial stiffness applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="slippingStiffnessLoading">Slipping stiffness when loading terms for the link property. 
        /// The slipping stiffness when loading applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="slippingStiffnessUnloading">Slipping stiffness when unloading terms for the link property. 
        /// The slipping stiffness when unloading applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="preCompressionDisplacement">Precompression displacement terms for the link property. 
        /// The nonlinear precompression displacement applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="stopDisplacement">Stop displacement terms for the link property. 
        /// The nonlinear stop displacement applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="direction">The direction for which the force can be applied for deach degree-of-freedom.
        /// The direction applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U2"/> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U3"/> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        void GetDamperFrictionSpring(string name,
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
            ref string GUID);

        /// <summary>
        /// This function initializes a friction spring damper-type link property. 
        /// If this function is called for an existing link property, all items for the property are reset to their default values.
        /// </summary>
        /// <param name="name">The name of an existing friction spring damper-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property. 
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property. 
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property. 
        /// The initial stiffness applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="slippingStiffnessLoading">Slipping stiffness when loading terms for the link property. 
        /// The slipping stiffness when loading applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="slippingStiffnessUnloading">Slipping stiffness when unloading terms for the link property. 
        /// The slipping stiffness when unloading applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="preCompressionDisplacement">Precompression displacement terms for the link property. 
        /// The nonlinear precompression displacement applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="stopDisplacement">Stop displacement terms for the link property. 
        /// The nonlinear stop displacement applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="direction">The direction for which the force can be applied for deach degree-of-freedom.
        /// The direction applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U2"/> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U3"/> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        void SetDamperFrictionSpring(string name,
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
            string GUID = "");


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function retrieves link property data for a linear exponential damper-type link property.
        /// </summary>
        /// <param name="name">The name of an existing exponential damper-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property. 
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property. 
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property. 
        /// The initial stiffness applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="nonLinearDamping">Nonlinear damping coefficient terms for the link property. 
        /// The nonlinear damping coefficient applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="nonLinearDampingExponent">Nonlinear damping exponent terms. 
        /// The nonlinear damping exponent applies for nonlinear analyses. 
        /// It is applied to the velocity across the damper in the equation of motion.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="nonLinearForceLimit">Nonlinear linear force limit terms for the link property. 
        /// The linear force limit applies for nonlinear analyses. 
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U2"/> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U3"/> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        void GetDamperLinearExponential(string name,
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
            ref string GUID);

        /// <summary>
        /// This function initializes a linear exponential damper-type link property. 
        /// If this function is called for an existing link property, all items for the property are reset to their default values.
        /// </summary>
        /// <param name="name">The name of an existing exponential damper-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property. 
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property. 
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property. 
        /// The initial stiffness applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="nonLinearDamping">Nonlinear damping coefficient terms for the link property. 
        /// The nonlinear damping coefficient applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="nonLinearDampingExponent">Nonlinear damping exponent terms. 
        /// The nonlinear damping exponent applies for nonlinear analyses. 
        /// It is applied to the velocity across the damper in the equation of motion.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="nonLinearForceLimit">Nonlinear linear force limit terms for the link property. 
        /// The linear force limit applies for nonlinear analyses. 
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U2"/> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U3"/> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        void SetDamperLinearExponential(string name,
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
            string GUID = "");
#endif


        /// <summary>
        /// This function retrieves link property data for a friction isolator-type link property.
        /// </summary>
        /// <param name="name">The name of an existing friction isolator-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property. 
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property. 
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property. 
        /// The initial stiffness applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U1, U2 and U3 [F/L].
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="frictionCoefficientSlow">The friction coefficient at zero velocity terms for the link property.
        /// The friction coefficient applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="frictionCoefficientFast">The friction coefficient at fast velocity terms for the link property.
        /// The friction coefficient applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="slidingRate">The inverse of the characteristic sliding velocity terms for the link property.
        /// The sliding rate applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3 [s/L].
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="radiusOfContactSurface">The radius of the sliding contact surface terms for the link property. 
        /// Inputting 0 means there is an infinite radius, that is, the slider is flat.
        /// The radius of contact surface applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3 [L].
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="damping">This is the nonlinear damping coefficient used for the axial translational degree of freedom, U1. 
        /// This item applies for nonlinear analyses. [F/L].</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U2"/> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U3"/> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        void GetFrictionIsolator(string name,
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
            ref string GUID);

        /// <summary>
        /// This function initializes a friction isolator-type link property. 
        /// If this function is called for an existing link property, all items for the property are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing friction isolator-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property. 
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property. 
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property. 
        /// The initial stiffness applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U1, U2 and U3 [F/L].
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="frictionCoefficientSlow">The friction coefficient at zero velocity terms for the link property.
        /// The friction coefficient applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="frictionCoefficientFast">The friction coefficient at fast velocity terms for the link property.
        /// The friction coefficient applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="slidingRate">The inverse of the characteristic sliding velocity terms for the link property.
        /// The sliding rate applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3 [s/L].
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="radiusOfContactSurface">The radius of the sliding contact surface terms for the link property. 
        /// Inputting 0 means there is an infinite radius, that is, the slider is flat.
        /// The radius of contact surface applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3 [L].
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="damping">This is the nonlinear damping coefficient used for the axial translational degree of freedom, U1. 
        /// This item applies for nonlinear analyses. [F/L].</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U2"/> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U3"/> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        void SetFrictionIsolator(string name,
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
            string GUID = "");



        /// <summary>
        /// This function retrieves link property data for a gap-type link property.
        /// </summary>
        /// <param name="name">The name of an existing gap-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property. 
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property. 
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property. 
        /// The initial stiffness applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="initialGap">Initial gap opening terms for the link property
        /// The initial gap applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U2"/> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U3"/> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        void GetGap(string name,
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
            ref string GUID);

        /// <summary>
        /// This function initializes a gap-type link property. 
        /// If this function is called for an existing link property, all items for the property are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing gap-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property. 
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property. 
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property. 
        /// The initial stiffness applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="initialGap">Initial gap opening terms for the link property
        /// The initial gap applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U2"/> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U3"/> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        void SetGap(string name,
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
            string GUID = "");



        /// <summary>
        /// This function retrieves link property data for a hook-type link property.
        /// </summary>
        /// <param name="name">The name of an existing exponential hook-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property. 
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property. 
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property. 
        /// The initial stiffness applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="initialGap">Initial hook opening terms for the link property
        /// The initial hook opening applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U2"/> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U3"/> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        void GetHook(string name,
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
            ref string GUID);

        /// <summary>
        /// This function initializes a hook-type link property. 
        /// If this function is called for an existing link property, all items for the property are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing hook-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property. 
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property. 
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property. 
        /// The initial stiffness applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="initialGap">Initial hook opening terms for the link property
        /// The initial hook opening applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U2"/> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U3"/> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        void SetHook(string name,
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
            string GUID = "");



        /// <summary>
        /// This function retrieves link property data for a linear-type link property.
        /// </summary>
        /// <param name="name">The name of an existing linear-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property when <paramref name="isStiffnessCoupled"/> = False.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property when <paramref name="isDampingCoupled"/> = False.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveStiffnessCoupled">Effective coupled stiffness terms for the link property when <paramref name="isStiffnessCoupled"/> = True.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveDampingCoupled">Effective coupled damping terms for the link property when <paramref name="isDampingCoupled"/> = True.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="isStiffnessCoupled">True: Link effective stiffness, Ke, is coupled.
        /// Use the <paramref name="effectiveDampingCoupled"/> parameter.
        /// False: Use the <paramref name="effectiveStiffness"/> parameter.</param>
        /// <param name="isDampingCoupled">True: Link effective damping, Ce, is coupled.
        /// Use the <paramref name="effectiveStiffnessCoupled"/> parameter.
        /// False: Use the <paramref name="effectiveDamping"/> parameter.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U2"/> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U3"/> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        void GetLinear(string name,
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
            ref string GUID);


        /// <summary>
        /// This function initializes a linear-type link property. 
        /// If this function is called for an existing link property, all items for the property are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing linear-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property when <paramref name="isStiffnessCoupled"/> = False.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property when <paramref name="isDampingCoupled"/> = False.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveStiffnessCoupled">Effective coupled stiffness terms for the link property when <paramref name="isStiffnessCoupled"/> = True.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveDampingCoupled">Effective coupled damping terms for the link property when <paramref name="isDampingCoupled"/> = True.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="isStiffnessCoupled">True: Link effective stiffness, Ke, is coupled.
        /// Use the <paramref name="effectiveDampingCoupled"/> parameter.
        /// False: Use the <paramref name="effectiveStiffness"/> parameter.</param>
        /// <param name="isDampingCoupled">True: Link effective damping, Ce, is coupled.
        /// Use the <paramref name="effectiveStiffnessCoupled"/> parameter.
        /// False: Use the <paramref name="effectiveDamping"/> parameter.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U2"/> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U3"/> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        void SetLinear(string name,
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
            string GUID = "");




        /// <summary>
        /// This function retrieves link property data for a multilinear elastic-type link property
        /// </summary>
        /// <param name="name">The name of an existing exponential damper-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property. 
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property. 
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U2"/> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U3"/> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        void GetMultiLinearElastic(string name,
            out DegreesOfFreedomLocal degreesOfFreedom,
            out DegreesOfFreedomLocal fixity,
            out DegreesOfFreedomLocal nonLinear,
            out Stiffness effectiveStiffness,
            out Stiffness effectiveDamping,
            out double distanceFromJEndToU2Spring,
            out double distanceFromJEndToU3Spring,
            out string notes,
            out string GUID);


        /// <summary>
        /// This function initializes a multilinear elastic-type link property. 
        /// If this function is called for an existing link property, all items for the property are reset to their default value
        /// </summary>
        /// <param name="name">The name of an existing exponential damper-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property. 
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property. 
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U2"/> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U3"/> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        void SetMultiLinearElastic(string name,
            DegreesOfFreedomLocal degreesOfFreedom,
            DegreesOfFreedomLocal fixity,
            DegreesOfFreedomLocal nonLinear,
            Stiffness effectiveStiffness,
            Stiffness effectiveDamping,
            double distanceFromJEndToU2Spring,
            double distanceFromJEndToU3Spring,
            string notes = "",
            string GUID = "");



        /// <summary>
        /// This function retrieves link property data for a multilinear plastic-type link property
        /// </summary>
        /// <param name="name">The name of an existing exponential damper-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property. 
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property. 
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U2"/> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U3"/> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        void GetMultiLinearPlastic(string name,
            out DegreesOfFreedomLocal degreesOfFreedom,
            out DegreesOfFreedomLocal fixity,
            out DegreesOfFreedomLocal nonLinear,
            out Stiffness effectiveStiffness,
            out Stiffness effectiveDamping,
            out double distanceFromJEndToU2Spring,
            out double distanceFromJEndToU3Spring,
            out string notes,
            out string GUID);

        /// <summary>
        /// This function initializes a multilinear plastic-type link property. 
        /// If this function is called for an existing link property, all items for the property are reset to their default values
        /// </summary>
        /// <param name="name">The name of an existing exponential damper-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property. 
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property. 
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U2"/> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U3"/> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        void SetMultiLinearPlastic(string name,
            DegreesOfFreedomLocal degreesOfFreedom,
            DegreesOfFreedomLocal fixity,
            DegreesOfFreedomLocal nonLinear,
            Stiffness effectiveStiffness,
            Stiffness effectiveDamping,
            double distanceFromJEndToU2Spring,
            double distanceFromJEndToU3Spring,
            string notes = "",
            string GUID = "");



        /// <summary>
        /// This function retrieves the force-deformation data for a specified degree of freedom in multilinear elastic and multilinear plastic link properties.
        /// To successfully retrieve this data from the indicated link property, the following conditions must be met:
        ///   1.The link property must be multilinear elastic or multilinear plastic.
        ///   2.The specified DOF must be active.
        ///   3.The specified DOF must not be fixed.
        ///   4.The specified DOF must be nonlinear.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="name">The name of an existing multilinear elastic or multilinear plastic link property.</param>
        /// <param name="degreeOfFreedom">The degree of freedom to which the multilinear points apply.</param>
        /// <param name="forces">The force at each point. 
        /// When <paramref name="degreeOfFreedom"/> is <see cref="eDegreeOfFreedom.U1"/>, <see cref="eDegreeOfFreedom.U2"/> or <see cref="eDegreeOfFreedom.U3"/>, this is a force [F]. 
        /// When <paramref name="degreeOfFreedom"/> is <see cref="eDegreeOfFreedom.R1"/>, <see cref="eDegreeOfFreedom.R2"/> or <see cref="eDegreeOfFreedom.R3"/>, this is a moment. [F*L] </param>
        /// <param name="displacements">The displacement at each point. 
        /// When <paramref name="degreeOfFreedom"/> is <see cref="eDegreeOfFreedom.U1"/>, <see cref="eDegreeOfFreedom.U2"/> or <see cref="eDegreeOfFreedom.U3"/>, this is a translation [L]. 
        /// When <paramref name="degreeOfFreedom"/> is <see cref="eDegreeOfFreedom.R1"/>, <see cref="eDegreeOfFreedom.R2"/> or <see cref="eDegreeOfFreedom.R3"/>, this is a roration. [rad]</param>
        /// <param name="linkHysteresisType">Type of the link hysteresis.</param>
        /// <param name="alpha1">The Alpha1 hysteresis parameter.
        /// This item only applies to multilinear plastic link properties that have a <paramref name="linkHysteresisType"/> = <see cref="eLinkHysteresisType.Pivot"/>.</param>
        /// <param name="alpha2">The Alpha2 hysteresis parameter.
        /// This item only applies to multilinear plastic link properties that have a <paramref name="linkHysteresisType"/> = <see cref="eLinkHysteresisType.Pivot"/>.</param>
        /// <param name="beta1">The Beta1 hysteresis parameter.
        /// This item only applies to multilinear plastic link properties that have a <paramref name="linkHysteresisType"/> = <see cref="eLinkHysteresisType.Pivot"/>.</param>
        /// <param name="beta2">The Beta2 hysteresis parameter.
        /// This item only applies to multilinear plastic link properties that have a <paramref name="linkHysteresisType"/> = <see cref="eLinkHysteresisType.Pivot"/>.</param>
        /// <param name="eta">The Eta hysteresis parameter.
        /// This item only applies to multilinear plastic link properties that have a <paramref name="linkHysteresisType"/> = <see cref="eLinkHysteresisType.Pivot"/>.</param>
        void GetMultiLinearPoints(string name,
            eDegreeOfFreedom degreeOfFreedom,
            out double[] forces,
            out double[] displacements,
            out eLinkHysteresisType linkHysteresisType,
            out double alpha1,
            out double alpha2,
            out double beta1,
            out double beta2,
            out double eta);

        /// <summary>
        /// This function sets the force-deformation data for a specified degree of freedom in multilinear elastic and multilinear plastic link properties.
        /// To successfully apply this data to the indicated link property, the following conditions must be met:
        ///   1.The link property must be multilinear elastic or multilinear plastic.
        ///   2.The specified DOF must be active.
        ///   3.The specified DOF must not be fixed.
        ///   4.The specified DOF must be nonlinear.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="name">The name of an existing multilinear elastic or multilinear plastic link property.</param>
        /// <param name="degreeOfFreedom">The degree of freedom to which the multilinear points apply.</param>
        /// <param name="forces">The force at each point. 
        /// When <paramref name="degreeOfFreedom"/> is <see cref="eDegreeOfFreedom.U1"/>, <see cref="eDegreeOfFreedom.U2"/> or <see cref="eDegreeOfFreedom.U3"/>, this is a force [F]. 
        /// When <paramref name="degreeOfFreedom"/> is <see cref="eDegreeOfFreedom.R1"/>, <see cref="eDegreeOfFreedom.R2"/> or <see cref="eDegreeOfFreedom.R3"/>, this is a moment. [F*L] </param>
        /// <param name="displacements">The displacement at each point. 
        /// When <paramref name="degreeOfFreedom"/> is <see cref="eDegreeOfFreedom.U1"/>, <see cref="eDegreeOfFreedom.U2"/> or <see cref="eDegreeOfFreedom.U3"/>, this is a translation [L]. 
        /// When <paramref name="degreeOfFreedom"/> is <see cref="eDegreeOfFreedom.R1"/>, <see cref="eDegreeOfFreedom.R2"/> or <see cref="eDegreeOfFreedom.R3"/>, this is a roration. [rad]</param>
        /// <param name="linkHysteresisType">Type of the link hysteresis.</param>
        /// <param name="alpha1">The Alpha1 hysteresis parameter.
        /// This item only applies to multilinear plastic link properties that have a <paramref name="linkHysteresisType"/> = <see cref="eLinkHysteresisType.Pivot"/>.</param>
        /// <param name="alpha2">The Alpha2 hysteresis parameter.
        /// This item only applies to multilinear plastic link properties that have a <paramref name="linkHysteresisType"/> = <see cref="eLinkHysteresisType.Pivot"/>.</param>
        /// <param name="beta1">The Beta1 hysteresis parameter.
        /// This item only applies to multilinear plastic link properties that have a <paramref name="linkHysteresisType"/> = <see cref="eLinkHysteresisType.Pivot"/>.</param>
        /// <param name="beta2">The Beta2 hysteresis parameter.
        /// This item only applies to multilinear plastic link properties that have a <paramref name="linkHysteresisType"/> = <see cref="eLinkHysteresisType.Pivot"/>.</param>
        /// <param name="eta">The Eta hysteresis parameter.
        /// This item only applies to multilinear plastic link properties that have a <paramref name="linkHysteresisType"/> = <see cref="eLinkHysteresisType.Pivot"/>.</param>
        void SetMultiLinearPoints(string name,
            eDegreeOfFreedom degreeOfFreedom,
            double[] forces,
            double[] displacements,
            eLinkHysteresisType linkHysteresisType = eLinkHysteresisType.Kinematic,
            double alpha1 = 0,
            double alpha2 = 0,
            double beta1 = 0,
            double beta2 = 0,
            double eta = 0);




        /// <summary>
        /// This function retrieves link property data for a plastic Wen-type link property.
        /// </summary>
        /// <param name="name">The name of an existing plastic Wen-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property. 
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property. 
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property. 
        /// The initial stiffness applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="yieldForce">The yield force terms for the link property. 
        /// The yield force applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="postYieldStiffnessRatio">post-yield stiffness ratio terms for the link property. 
        /// The post-yield stiffness ratio applies for nonlinear analyses. 
        /// It is the post-yield stiffness divided by the initial stiffness.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="yieldExponentTerm">The yield exponent terms for the link property. 
        /// The yield exponent applies for nonlinear analyses. 
        /// The yielding exponent that controls the sharpness of the transition from the initial stiffness to the yielded stiffness.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U2"/> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U3"/> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        void GetPlasticWen(string name,
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
            ref string GUID);

        /// <summary>
        /// This function initializes a plastic Wen-type link property. 
        /// If this function is called for an existing link property, all items for the property are reset to their default values.
        /// </summary>
        /// <param name="name">The name of an existing plastic Wen-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property. 
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property. 
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property. 
        /// The initial stiffness applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="yieldForce">The yield force terms for the link property. 
        /// The yield force applies for nonlinear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="postYieldStiffnessRatio">post-yield stiffness ratio terms for the link property. 
        /// The post-yield stiffness ratio applies for nonlinear analyses. 
        /// It is the post-yield stiffness divided by the initial stiffness.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="yieldExponentTerm">The yield exponent terms for the link property. 
        /// The yield exponent applies for nonlinear analyses. 
        /// The yielding exponent that controls the sharpness of the transition from the initial stiffness to the yielded stiffness.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U2"/> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U3"/> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        void SetPlasticWen(string name,
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
            string GUID = "");



        /// <summary>
        /// This function retrieves link property data for a rubber isolator-type link property.
        /// </summary>
        /// <param name="name">The name of an existing rubber isolator-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property. 
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property. 
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property. 
        /// The initial stiffness applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="yieldForce">The yield force terms for the link property. 
        /// The yield force applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="postYieldStiffnessRatio">The post-yield stiffness ratio terms for the link property. 
        /// The post-yield stiffness ratio applies for nonlinear analyses. 
        /// It is the post-yield stiffness divided by the initial stiffness.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U2"/> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U3"/> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        void GetRubberIsolator(string name,
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
            ref string GUID);

        /// <summary>
        /// This function initializes a rubber isolator-type link property. 
        /// If this function is called for an existing link property, all items for the property are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing rubber isolator-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property. 
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property. 
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property. 
        /// The initial stiffness applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="yieldForce">The yield force terms for the link property. 
        /// The yield force applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="postYieldStiffnessRatio">The post-yield stiffness ratio terms for the link property. 
        /// The post-yield stiffness ratio applies for nonlinear analyses. 
        /// It is the post-yield stiffness divided by the initial stiffness.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U2"/> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U3"/> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        void SetRubberIsolator(string name,
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
            ref string GUID);





        /// <summary>
        /// This function retrieves link property data for a T/C friction isolator-type link property.
        /// </summary>
        /// <param name="name">The name of an existing T/C friction isolator-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property. 
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property. 
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property. 
        /// The initial stiffness applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U1, U2 and U3 [F/L].
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="frictionCoefficientSlow">The friction coefficient at zero velocity terms for the link property.
        /// The friction coefficient applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="frictionCoefficientFast">The friction coefficient at fast velocity terms for the link property.
        /// The friction coefficient applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="slidingRate">The inverse of the characteristic sliding velocity terms for the link property.
        /// The sliding rate applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3 [s/L].
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="radiusOfContactSurface">The radius of the sliding contact surface terms for the link property. 
        /// Inputting 0 means there is an infinite radius, that is, the slider is flat.
        /// The radius of contact surface applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3 [L].
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="frictionCoefficientSlowTension">The friction coefficient at zero velocity terms when U1 is in tension for the link property.
        /// The friction coefficient applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="frictionCoefficientFastTension">The friction coefficient at fast velocity terms when U1 is in tension for the link property.
        /// The friction coefficient applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="slidingRateTension">The inverse of the characteristic sliding velocity terms for when U1 is in tension the link property.
        /// The sliding rate applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3 [s/L].
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="axialTranslationTensionStiffness">The axial translational tension stiffness for the U1 degree of freedom. 
        /// This item applies for nonlinear analyses. [F/L].</param>
        /// <param name="gapCompression">The U1 degree of freedom gap opening for compression. 
        /// This item applies for nonlinear analyses. [L].</param>
        /// <param name="gapTension">The U1 degree of freedom gap opening for tension. 
        /// This item applies for nonlinear analyses. [L].</param>
        /// <param name="damping">This is the nonlinear damping coefficient used for the axial translational degree of freedom, U1. 
        /// This item applies for nonlinear analyses. [F/L].</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U2"/> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U3"/> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        void GetTensionCompressionFrictionIsolator(string name,
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
            ref string GUID);

        /// <summary>
        /// This function initializes a T/C friction isolator-type link property. 
        /// If this function is called for an existing link property, all items for the property are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing T/C friction isolator-type link property.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property. 
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property. 
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="initialStiffness">Initial stiffness terms for the link property. 
        /// The initial stiffness applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U1, U2 and U3 [F/L].
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="frictionCoefficientSlow">The friction coefficient at zero velocity terms for the link property.
        /// The friction coefficient applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="frictionCoefficientFast">The friction coefficient at fast velocity terms for the link property.
        /// The friction coefficient applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="slidingRate">The inverse of the characteristic sliding velocity terms for the link property.
        /// The sliding rate applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3 [s/L].
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="radiusOfContactSurface">The radius of the sliding contact surface terms for the link property. 
        /// Inputting 0 means there is an infinite radius, that is, the slider is flat.
        /// The radius of contact surface applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3 [L].
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="frictionCoefficientSlowTension">The friction coefficient at zero velocity terms when U1 is in tension for the link property.
        /// The friction coefficient applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="frictionCoefficientFastTension">The friction coefficient at fast velocity terms when U1 is in tension for the link property.
        /// The friction coefficient applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="slidingRateTension">The inverse of the characteristic sliding velocity terms for when U1 is in tension the link property.
        /// The sliding rate applies for nonlinear analyses.
        /// Note that this item is applicable only for degrees of freedom U2 and U3 [s/L].
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="axialTranslationTensionStiffness">The axial translational tension stiffness for the U1 degree of freedom. 
        /// This item applies for nonlinear analyses. [F/L].</param>
        /// <param name="gapCompression">The U1 degree of freedom gap opening for compression. 
        /// This item applies for nonlinear analyses. [L].</param>
        /// <param name="gapTension">The U1 degree of freedom gap opening for tension. 
        /// This item applies for nonlinear analyses. [L].</param>
        /// <param name="damping">This is the nonlinear damping coefficient used for the axial translational degree of freedom, U1. 
        /// This item applies for nonlinear analyses. [F/L].</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U2"/> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U3"/> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        void SetTensionCompressionFrictionIsolator(string name,
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
            string GUID = "");


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function retrieves link property data for a Triple Pendulum Isolator type link property.
        /// </summary>
        /// <param name="name">The name of an existing or new link property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property. 
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property. 
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="k1">This is the axial compression stiffness for the U1 degree of freedom. 
        /// This item applies for nonlinear analyses. [F/L].</param>
        /// <param name="damping">This is the nonlinear damping coefficient for the axial degree of freedom, U1, when it is in compression. 
        /// This item applies for nonlinear analyses. [F/L].</param>
        /// <param name="initialNonlinearStiffness">The initial nonlinear stiffness (before sliding) for each sliding surface.
        /// Note that this item is applicable for degrees of freedom U2 and U3 only. 
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="frictionCoefficientSlow">The friction coefficient at zero velocity for each sliding surface when U1 is in compression.
        /// Note that this item is applicable for degrees of freedom U2 and U3 only. 
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="frictionCoefficientFast">The friction coefficient at fast velocity for each sliding surface when U1 is in compression.
        /// Note that this item is applicable for degrees of freedom U2 and U3 only. 
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="slidingRate">The inverse of the characteristic sliding velocity for the <paramref name="frictionCoefficientSlow"/> and <paramref name="frictionCoefficientFast"/> friction coefficients for each sliding surface. [s/L]. 
        /// This item applies for nonlinear analyses.
        /// Note that this item is applicable for degrees of freedom U2 and U3 only. 
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="radiusOfContactSurface">The radius for each sliding surface. [L]. 
        /// Inputting 0 means there is an infinite radius, that is, the slider is flat. 
        /// This item applies for nonlinear analyses.
        /// Note that this item is applicable for degrees of freedom U2 and U3 only. 
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="maxSlidingDistance">The amount of displacement allowed before hitting a stiff limit for each sliding surface. [L]. 
        /// Inputting 0 means there is no stop. 
        /// This item applies for nonlinear analyses.
        /// Note that this item is applicable for degrees of freedom U2 and U3 only. 
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="heightOuterSurface">The height (distance) between the outer sliding surfaces at zero displacement. [L].
        /// Note that this item is applicable for degrees of freedom U2 and U3 only. 
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="heightInnerSurface">The height (distance) between the inner sliding surfaces. [L].
        /// Note that this item is applicable for degrees of freedom U2 and U3 only. 
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U2"/> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U3"/> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.</param>
        void GetTriplePendulumIsolator(string name,
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
            ref string GUID);

        /// <summary>
        /// This function initializes a Triple Pendulum Isolator type link property. 
        /// If this function is called for an existing link property, then all items for the property are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new link property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="degreesOfFreedom">Indicates if properties exist for a specified degree of freedom.</param>
        /// <param name="fixity">Indicates if the specified degree of freedom is fixed (restrained).
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True.</param>
        /// <param name="nonLinear">Indicates if nonlinear properties exist for a specified degree of freedom.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveStiffness">Effective stiffness terms for the link property. 
        /// The effective stiffness applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="effectiveDamping">Effective damping terms for the link property. 
        /// The effective damping applies for linear analyses.
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True and the corresponding <paramref name="fixity"/> = False.</param>
        /// <param name="k1">This is the axial compression stiffness for the U1 degree of freedom. 
        /// This item applies for nonlinear analyses. [F/L].</param>
        /// <param name="damping">This is the nonlinear damping coefficient for the axial degree of freedom, U1, when it is in compression. 
        /// This item applies for nonlinear analyses. [F/L].</param>
        /// <param name="initialNonlinearStiffness">The initial nonlinear stiffness (before sliding) for each sliding surface.
        /// Note that this item is applicable for degrees of freedom U2 and U3 only. 
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="frictionCoefficientSlow">The friction coefficient at zero velocity for each sliding surface when U1 is in compression.
        /// Note that this item is applicable for degrees of freedom U2 and U3 only. 
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="frictionCoefficientFast">The friction coefficient at fast velocity for each sliding surface when U1 is in compression.
        /// Note that this item is applicable for degrees of freedom U2 and U3 only. 
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="slidingRate">The inverse of the characteristic sliding velocity for the <paramref name="frictionCoefficientSlow"/> and <paramref name="frictionCoefficientFast"/> friction coefficients for each sliding surface. [s/L]. 
        /// This item applies for nonlinear analyses.
        /// Note that this item is applicable for degrees of freedom U2 and U3 only. 
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="radiusOfContactSurface">The radius for each sliding surface. [L]. 
        /// Inputting 0 means there is an infinite radius, that is, the slider is flat. 
        /// This item applies for nonlinear analyses.
        /// Note that this item is applicable for degrees of freedom U2 and U3 only. 
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="maxSlidingDistance">The amount of displacement allowed before hitting a stiff limit for each sliding surface. [L]. 
        /// Inputting 0 means there is no stop. 
        /// This item applies for nonlinear analyses.
        /// Note that this item is applicable for degrees of freedom U2 and U3 only. 
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="heightOuterSurface">The height (distance) between the outer sliding surfaces at zero displacement. [L].
        /// Note that this item is applicable for degrees of freedom U2 and U3 only. 
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="heightInnerSurface">The height (distance) between the inner sliding surfaces. [L].
        /// Note that this item is applicable for degrees of freedom U2 and U3 only. 
        /// A given property only applies when the corresponding <paramref name="degreesOfFreedom"/> = True, the corresponding <paramref name="fixity"/> = False, and the corresponding <paramref name="nonLinear"/> = True.</param>
        /// <param name="distanceFromJEndToU2Spring">The distance from the J-End of the link to the U2 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U2"/> = True. [L]</param>
        /// <param name="distanceFromJEndToU3Spring">The distance from the J-End of the link to the U3 shear spring. 
        /// This item applies only when <paramref name="degreesOfFreedom.U3"/> = True. [L]</param>
        /// <param name="notes">The notes, if any, assigned to the property.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the property.
        /// If this item is input as Default, the program assigns a GUID to the property.</param>
        void SetTriplePendulumIsolator(string name,
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
            string GUID = "");
#endif
    }
}