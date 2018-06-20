// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-11-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-11-2017
// ***********************************************************************
// <copyright file="MaterialProperties.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
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
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.TimeDependent;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Represents the material properties in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.IMaterialProperties" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class MaterialProperties : CSiApiBase, IMaterialProperties
    {
        #region Fields
        /// <summary>
        /// The seed
        /// </summary>
        private readonly CSiApiSeed _seed;

        /// <summary>
        /// The time dependent concrete
        /// </summary>
        private TimeDependentConcrete _timeDependentConcrete;
        /// <summary>
        /// The time dependen tendon
        /// </summary>
        private TimeDependentTendon _timeDependenTendon;
        #endregion

        #region Properties                              
        /// <summary>
        /// Gets the time dependent concrete.
        /// </summary>
        /// <value>The time dependent concrete.</value>
        public TimeDependentConcrete TimeDependentConcrete => _timeDependentConcrete ?? (_timeDependentConcrete = new TimeDependentConcrete(_seed));

        /// <summary>
        /// Gets the time dependent tendon.
        /// </summary>
        /// <value>The time dependent tendon.</value>
        public TimeDependentTendon TimeDependentTendon => _timeDependenTendon ?? (_timeDependenTendon = new TimeDependentTendon(_seed));
        #endregion

        #region Initialization        

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialProperties" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public MaterialProperties(CSiApiSeed seed) : base(seed)
        {
            _seed = seed;
        }

        #endregion

        #region Methods: Query

        /// <summary>
        /// This function returns the total number of defined material properties in the model.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int Count()
        {
            return _sapModel.PropMaterial.Count();
        }



        /// <summary>
        /// This function retrieves the names of all defined material properties.
        /// </summary>
        /// <param name="names">Material property names retrieved by the program.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetNameList(out string[] names)
        {
            names = new string[0];
            _callCode = _sapModel.PropMaterial.GetNameList(ref _numberOfItems, ref names);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves the material type for the specified material.
        /// </summary>
        /// <param name="name">The name of an existing material propert.</param>
        /// <param name="materialType">Type of the material.</param>
        /// <param name="symmetryType">Type of the material directional symmetry.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetMaterialType(string name,
            ref eMaterialPropertyType materialType,
            ref eMaterialSymmetryType symmetryType)
        {
            CSiProgram.eMatType csiMaterialType = CSiProgram.eMatType.NoDesign;
            int csiSymmetryType = 0;
            
            _callCode = _sapModel.PropMaterial.GetTypeOAPI(name, ref csiMaterialType, ref csiSymmetryType);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            materialType = EnumLibrary.Convert(csiMaterialType, materialType);
            symmetryType = (eMaterialSymmetryType)csiSymmetryType;
        }
        #endregion

        #region Methods: Creation        
        /// <summary>
        /// Adds the material.
        /// </summary>
        /// <param name="name">This item is returned by the program.
        /// It is the name that the program ultimately assigns for the material property.
        /// If no <paramref name="userName" /> is specified, the program assigns a default name to the material property.
        /// If a <paramref name="userName" /> is specified and that name is not used for another material property, the <paramref name="userName" /> is assigned to the material prope.</param>
        /// <param name="materialType">Type of the material.</param>
        /// <param name="region">The region name of the material property that is user-predefined in the file "CSiMaterialLibrary*.xml" located in subfolder "Property Libraries" under the installation.</param>
        /// <param name="standardName">The <paramref name="standardName" /> name of the material property with the specified <paramref name="materialType" /> within the specified region.</param>
        /// <param name="grade">The Grade name of the material property with the specified <paramref name="materialType" /> within the specified region and <paramref name="standardName" />.</param>
        /// <param name="userName">This is an optional user specified name for the material property.
        /// If a <paramref name="userName" /> is specified and that name is already used for another material property, the program ignores the <paramref name="userName" />.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void AddMaterial(ref string name,
            eMaterialPropertyType materialType,
            string region,
            string standardName,
            string grade,
            string userName = "")
        {
            _callCode = _sapModel.PropMaterial.AddMaterial(ref name, 
                EnumLibrary.Convert<eMaterialPropertyType, CSiProgram.eMatType>(materialType), 
                region, standardName, grade, userName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// The function deletes a specified material property.
        /// It returns an error if the specified property can not be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        /// <param name="name">The name of an existing material property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void Delete(string name)
        {
            _callCode = _sapModel.PropMaterial.Delete(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function changes the name of an existing material property.
        /// </summary>
        /// <param name="currentName">The existing name of a defined material property.</param>
        /// <param name="newName">The new name for the material property.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ChangeName(string currentName, 
            string newName)
        {
            _callCode = _sapModel.PropMaterial.ChangeName(currentName, newName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function retrieves some basic material property data.
        /// </summary>
        /// <param name="name">The name of an existing material property.</param>
        /// <param name="materialType">Type of the material.</param>
        /// <param name="color">The display color assigned to the material.</param>
        /// <param name="notes">The notes, if any, assigned to the material.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the material.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetMaterial(string name,
            ref eMaterialPropertyType materialType,
            ref int color,
            ref string notes,
            ref string GUID)
        {
            CSiProgram.eMatType csiMaterialType = CSiProgram.eMatType.NoDesign;

            _callCode = _sapModel.PropMaterial.GetMaterial(name, ref csiMaterialType, ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            materialType = EnumLibrary.Convert(csiMaterialType, materialType);
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        // Deprecated. Use AddMaterial instead
        /// <summary>
        /// Sets the material.
        /// </summary>
        /// <param name="name">The name of an existing or new material property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added</param>
        /// <param name="materialType">Type of the material.</param>
        /// <param name="color">The display color assigned to the material.</param>
        /// <param name="notes">The notes, if any, assigned to the material.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the material.
        /// If this item is input as Default, the program assigns a GUID to the material</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetMaterial(string name,
            eMaterialPropertyType materialType,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropMaterial.SetMaterial(name, 
                EnumLibrary.Convert<eMaterialPropertyType, CSiProgram.eMatType>(materialType), 
                color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif

        /// <summary>
        /// This function retrieves the  additional material damping data for the material.
        /// </summary>
        /// <param name="name">The name of an existing material property.</param>
        /// <param name="modalDampingRatio">The modal damping ratio.</param>
        /// <param name="viscousMassCoefficient">The mass coefficient for viscous proportional damping.</param>
        /// <param name="viscousStiffnessCoefficient">The stiffness coefficient for viscous proportional damping.</param>
        /// <param name="hystereticMassCoefficient">The mass coefficient for hysteretic proportional damping.</param>
        /// <param name="hystereticStiffnessCoefficient">The stiffness coefficient for hysteretic proportional damping.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetDamping(string name,
            ref double modalDampingRatio,
            ref double viscousMassCoefficient,
            ref double viscousStiffnessCoefficient,
            ref double hystereticMassCoefficient,
            ref double hystereticStiffnessCoefficient,
            double temperature = 0)
        {
            _callCode = _sapModel.PropMaterial.GetDamping(name, ref modalDampingRatio, ref viscousMassCoefficient, ref viscousStiffnessCoefficient, ref hystereticMassCoefficient, ref hystereticStiffnessCoefficient, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the additional material damping data for the material.
        /// </summary>
        /// <param name="name">The name of an existing material property.</param>
        /// <param name="modalDampingRatio">The modal damping ratio.</param>
        /// <param name="viscousMassCoefficient">The mass coefficient for viscous proportional damping.</param>
        /// <param name="viscousStiffnessCoefficient">The stiffness coefficient for viscous proportional damping.</param>
        /// <param name="hystereticMassCoefficient">The mass coefficient for hysteretic proportional damping.</param>
        /// <param name="hystereticStiffnessCoefficient">The stiffness coefficient for hysteretic proportional damping.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetDamping(string name,
            double modalDampingRatio,
            double viscousMassCoefficient,
            double viscousStiffnessCoefficient,
            double hystereticMassCoefficient,
            double hystereticStiffnessCoefficient,
            double temperature = 0)
        {
            _callCode = _sapModel.PropMaterial.SetDamping(name, modalDampingRatio, viscousMassCoefficient, viscousStiffnessCoefficient, hystereticMassCoefficient, hystereticStiffnessCoefficient, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function retrieves the weight per unit volume and mass per unit volume of the material.
        /// </summary>
        /// <param name="name">The name of an existing material property.</param>
        /// <param name="weightPerVolume">The weight per unit volume for the material. [F/L^3].</param>
        /// <param name="massPerVolume">The mass per unit volume for the material. [M/L^3].</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetWeightAndMass(string name,
            ref double weightPerVolume,
            ref double massPerVolume,
            double temperature = 0)
        {
            _callCode = _sapModel.PropMaterial.GetWeightAndMass(name, ref weightPerVolume, ref massPerVolume, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function assigns weight per unit volume or mass per unit volume to a material property.
        /// </summary>
        /// <param name="name">The name of an existing material property.</param>
        /// <param name="perUnitVolumeOption">The per unit volume option.
        /// If the weight is specified, the corresponding mass is program calculated based on the specified weight.
        /// Similarly, if the mass is specified, the corresponding weight is program calculated based on the specified mass.</param>
        /// <param name="value">This is either the weight per unit volume or the mass per unit volume, depending on the value of the <paramref name="perUnitVolumeOption" /> item.
        /// [F/L^3] for <paramref name="perUnitVolumeOption" /> = 1 (weight), and [M/L^3] for <paramref name="perUnitVolumeOption" /> = 2 (mass).</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetWeightAndMass(string name,
            eMaterialPerVolumeOption perUnitVolumeOption,
            double value,
            double temperature = 0)
        {
            _callCode = _sapModel.PropMaterial.SetWeightAndMass(name, (int)perUnitVolumeOption, value, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <summary>
        /// This function retrieves the temperatures at which properties are specified for a material.
        /// </summary>
        /// <param name="name">The name of an existing material pro.</param>
        /// <param name="temperatures">The different temperatures at which properties are specified for the material.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetTemperature(string name,
            ref double[] temperatures)
        {
            _callCode = _sapModel.PropMaterial.GetTemp(name, ref _numberOfItems, ref temperatures);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function assigns the temperatures at which properties are specified for a material.
        /// This data is required only for materials whose properties are temperature dependent..
        /// </summary>
        /// <param name="name">The name of an existing material pro.</param>
        /// <param name="temperatures">The different temperatures at which properties are specified for the material.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetTemperature(string name,
            double[] temperatures)
        {
            _callCode = _sapModel.PropMaterial.SetTemp(name, temperatures.Length, ref temperatures);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function retrieves the material stress-strain curve.
        /// </summary>
        /// <param name="name">The name of an existing material property.</param>
        /// <param name="pointID">The point identifier.
        /// The point ID controls the color that will be displayed for hinges in a deformed shape plot.</param>
        /// <param name="strain">The strain at each point on the stress strain curve.</param>
        /// <param name="stress">The stress at each point on the stress strain curve. [F/L^2].</param>
        /// <param name="sectionName">This item applies only if the specified material is concrete with a Mander concrete type.
        /// This is the frame section property for which the Mander stress-strain curve is retrieved.
        /// The section must be round or rectangular.</param>
        /// <param name="rebarArea">This item applies only if the specified material is rebar, which does not have a user-defined stress-strain curve and is specified to use Caltrans default controlling strain values, which are bar size dependent.
        /// This is the area of the rebar for which the stress-strain curve is retrieved.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetStressStrainCurve(string name,
            out eStressStrainPointID[] pointID,
            out double[] strain,
            out double[] stress,
            string sectionName = "",
            double rebarArea = 0,
            double temperature = 0)
        {
            int[] csiPointID = new int[0];
            strain = new double[0];
            stress = new double[0];

            _callCode = _sapModel.PropMaterial.GetSSCurve(name, ref _numberOfItems, ref csiPointID, ref strain, ref stress, sectionName, rebarArea, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            pointID = csiPointID.Cast<eStressStrainPointID>().ToArray();
        }


        /// <summary>
        /// This function sets the material stress-strain curve for materials that are specified to have user-defined stress-strain curves.
        /// TODO: Make parameter object for stress-strain curve that prevents or checks for the errors. Overloaded method skips the checks. Object stores values in tuples.
        /// </summary>
        /// <param name="name">The name of an existing material property.</param>
        /// <param name="pointID">The point identifier.
        /// The point ID controls the color that will be displayed for hinges in a deformed shape plot.
        /// The point IDs must be input in numerically increasing order, except that 0 (None) values are allowed anywhere.
        /// TODO: No duplicate values are allowed excepth for 0 (None).
        /// At least three points must be specified.</param>
        /// <param name="strain">The strain at each point on the stress strain curve.
        /// TODO: The strains must increase monotonically.
        /// At least three points must be specified.</param>
        /// <param name="stress">The stress at each point on the stress strain curve. [F/L^2].
        /// TODO: Points that have a negative strain must have a zero or negative stress.
        /// TODO: Similarly, points that have a positive strain must have a zero or positive stress.
        /// At least three points must be specified.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="CSiException">At least 3 points must be specifed for a stress-strain curve, but only " +
        /// pointID.Length + " were specified.
        /// or 
        /// API_DEFAULT_ERROR_CODE</exception>
        public void SetStressStrainCurve(string name,
            eStressStrainPointID[] pointID,
            double[] strain,
            double[] stress,
            double temperature = 0)
        {
            if (pointID.Length < 3)
            {
                throw new CSiException("At least 3 points must be specifed for a stress-strain curve, but only " +
                                       pointID.Length + " were specified.");
            }
            arraysLengthMatch(nameof(pointID), pointID.Length, nameof(strain), strain.Length);
            arraysLengthMatch(nameof(pointID), pointID.Length, nameof(stress), stress.Length);

            int[] csiPointID = pointID.Cast<int>().ToArray();

            _callCode = _sapModel.PropMaterial.SetSSCurve(name, pointID.Length, ref csiPointID, ref strain, ref stress, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion

        #region Methods: Mechanical Properties           

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// This function gets the mass source data for an existing mass source.
        /// </summary>
        /// <param name="massFromElements">True: Element self mass is included in the mass.</param>
        /// <param name="massFromMasses">True: Assigned masses are included in the mass.</param>
        /// <param name="massFromLoads">True: Specified load patterns are included in the mass.</param>
        /// <param name="numberLoads">The number of load patterns specified for the mass source.  
        /// This item is only applicable when the MassFromLoads item is True.</param>
        /// <param name="namesLoadPatterns">This is an array of load pattern names specified for the mass source.</param>
        /// <param name="scaleFactors">This is an array of load pattern multipliers specified for the mass source.</param>
        public void GetMassSource(ref bool massFromElements,
            ref bool massFromMasses,
            ref bool massFromLoads,
            ref int numberLoads,
            ref string[] namesLoadPatterns,
            ref double[] scaleFactors)
        {
            _callCode = _sapModel.PropMaterial.GetMassSource_1(ref massFromElements, ref massFromMasses, ref massFromLoads, ref numberLoads, ref namesLoadPatterns, ref scaleFactors);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function adds a new mass source to the model or reinitializes an existing mass source.
        /// </summary>
        /// <param name="massFromElements">True: Element self mass is included in the mass.</param>
        /// <param name="massFromMasses">True: Assigned masses are included in the mass.</param>
        /// <param name="massFromLoads">True: Specified load patterns are included in the mass.</param>
        /// <param name="namesLoadPatterns">This is an array of load pattern names specified for the mass source when <paramref name="massFromLoads"/> = true.</param>
        /// <param name="scaleFactors">This is an array of load pattern multipliers specified for the mass source when <paramref name="massFromLoads"/> = true.</param>
        public void SetMassSource(bool massFromElements,
            bool massFromMasses,
            bool massFromLoads,
            string[] namesLoadPatterns,
            double[] scaleFactors)
        {
            arraysLengthMatch(nameof(namesLoadPatterns), namesLoadPatterns.Length, nameof(scaleFactors), scaleFactors.Length);

            _callCode = _sapModel.PropMaterial.SetMassSource_1(ref massFromElements, 
                            ref massFromMasses, ref massFromLoads, 
                            namesLoadPatterns.Length, ref namesLoadPatterns, ref scaleFactors);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif

        /// <summary>
        /// This function retrieves the mechanical properties for a material with an isotropic directional symmetry type.
        /// The function returns an error if the symmetry type of the specified material is not isotropic.
        /// TODO: Handle this
        /// </summary>
        /// <param name="name">The name of an existing material property.</param>
        /// <param name="modulusOfElasticity">The modulus of elasticity.</param>
        /// <param name="poissonsRatio">The poisson's ratio.</param>
        /// <param name="thermalCoefficient">The thermal coefficient.</param>
        /// <param name="shearModulus">The shear modulus.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetMechanicalPropertiesIsotropic(string name,
            ref double modulusOfElasticity,
            ref double poissonsRatio,
            ref double thermalCoefficient,
            ref double shearModulus,
            double temperature = 0)
        {
            _callCode = _sapModel.PropMaterial.GetMPIsotropic(name, ref modulusOfElasticity, ref poissonsRatio, ref thermalCoefficient, ref shearModulus, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the material directional symmetry type to isotropic, and assigns the isotropic mechanical properties.
        /// </summary>
        /// <param name="name">The name of an existing material property.</param>
        /// <param name="modulusOfElasticity">The modulus of elasticity.</param>
        /// <param name="poissonsRatio">The poisson's ratio.</param>
        /// <param name="thermalCoefficient">The thermal coefficient.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetMechanicalPropertiesIsotropic(string name,
            double modulusOfElasticity,
            double poissonsRatio,
            double thermalCoefficient,
            double temperature = 0)
        {
            _callCode = _sapModel.PropMaterial.SetMPIsotropic(name, modulusOfElasticity, poissonsRatio, thermalCoefficient, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function retrieves the mechanical properties for a material with a uniaxial directional symmetry type.
        /// The function returns an error if the symmetry type of the specified material is not uniaxial.
        /// TODO: Handle this
        /// </summary>
        /// <param name="name">The name of an existing material property.</param>
        /// <param name="modulusOfElasticity">The modulus of elasticity.</param>
        /// <param name="thermalCoefficient">The thermal coefficient.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetMechanicalPropertiesUniaxial(string name,
            ref double modulusOfElasticity,
            ref double thermalCoefficient,
            double temperature = 0)
        {
            _callCode = _sapModel.PropMaterial.GetMPUniaxial(name, ref modulusOfElasticity, ref thermalCoefficient, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the material directional symmetry type to uniaxial, and assigns the uniaxial mechanical properties.
        /// </summary>
        /// <param name="name">The name of an existing material property.</param>
        /// <param name="modulusOfElasticity">The modulus of elasticity.</param>
        /// <param name="thermalCoefficient">The thermal coefficient.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetMechanicalPropertiesUniaxial(string name,
            double modulusOfElasticity,
            double thermalCoefficient,
            double temperature = 0)
        {
            _callCode = _sapModel.PropMaterial.SetMPUniaxial(name, modulusOfElasticity, thermalCoefficient, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }





        /// <summary>
        /// This function retrieves the mechanical properties for a material with an anisotropic directional symmetry type.
        /// The function returns an error if the symmetry type of the specified material is not anisotropic.
        /// TODO: Handle this
        /// </summary>
        /// <param name="name">The name of an existing material property.</param>
        /// <param name="modulusOfElasticities">The modulus of elasticities.</param>
        /// <param name="poissonsRatios">The poisson's ratios.</param>
        /// <param name="thermalCoefficients">The thermal coefficients.</param>
        /// <param name="shearModuluses">The shear moduluses.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetMechanicalPropertiesAnisotropic(string name,
            ref double[] modulusOfElasticities,
            ref double [] poissonsRatios,
            ref double[] thermalCoefficients,
            ref double[] shearModuluses,
            double temperature = 0)
        {
            _callCode = _sapModel.PropMaterial.GetMPAnisotropic(name, ref modulusOfElasticities, ref poissonsRatios, ref thermalCoefficients, ref shearModuluses, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the material directional symmetry type to anisotropic, and assigns the anisotropic mechanical properties.
        /// </summary>
        /// <param name="name">The name of an existing material property.</param>
        /// <param name="modulusOfElasticities">The modulus of elasticities.</param>
        /// <param name="poissonsRatios">The poisson's ratios.</param>
        /// <param name="thermalCoefficients">The thermal coefficients.</param>
        /// <param name="shearModuluses">The shear moduluses.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetMechanicalPropertiesAnisotropic(string name,
            double[] modulusOfElasticities,
            double[] poissonsRatios,
            double[] thermalCoefficients,
            double[] shearModuluses,
            double temperature = 0)
        {
            arraysLengthMatch(nameof(modulusOfElasticities), modulusOfElasticities.Length, nameof(poissonsRatios), poissonsRatios.Length);
            arraysLengthMatch(nameof(modulusOfElasticities), modulusOfElasticities.Length, nameof(thermalCoefficients), thermalCoefficients.Length);
            arraysLengthMatch(nameof(modulusOfElasticities), modulusOfElasticities.Length, nameof(shearModuluses), shearModuluses.Length);

            _callCode = _sapModel.PropMaterial.SetMPAnisotropic(name, ref modulusOfElasticities, ref poissonsRatios, ref thermalCoefficients, ref shearModuluses, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function retrieves the mechanical properties for a material with an anisotropic directional symmetry type.
        /// The function returns an error if the symmetry type of the specified material is not orthotropic.
        /// TODO: Handle this
        /// </summary>
        /// <param name="name">The name of an existing material property.</param>
        /// <param name="modulusOfElasticities">The modulus of elasticities.</param>
        /// <param name="poissonsRatios">The poisson's ratios.</param>
        /// <param name="thermalCoefficients">The thermal coefficients.</param>
        /// <param name="shearModuluses">The shear moduluses.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetMechanicalPropertiesOrthotropic(string name,
            ref double[] modulusOfElasticities,
            ref double[] poissonsRatios,
            ref double[] thermalCoefficients,
            ref double[] shearModuluses,
            double temperature = 0)
        {
            _callCode = _sapModel.PropMaterial.GetMPOrthotropic(name, ref modulusOfElasticities, ref poissonsRatios, ref thermalCoefficients, ref shearModuluses, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the material directional symmetry type to anisotropic, and assigns the anisotropic mechanical properties.
        /// </summary>
        /// <param name="name">The name of an existing material property.</param>
        /// <param name="modulusOfElasticities">The modulus of elasticities.</param>
        /// <param name="poissonsRatios">The poisson's ratios.</param>
        /// <param name="thermalCoefficients">The thermal coefficients.</param>
        /// <param name="shearModuluses">The shear moduluses.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetMechanicalPropertiesOrthotropic(string name,
            double[] modulusOfElasticities,
            double[] poissonsRatios,
            double[] thermalCoefficients,
            double[] shearModuluses,
            double temperature = 0)
        {
            arraysLengthMatch(nameof(modulusOfElasticities), modulusOfElasticities.Length, nameof(poissonsRatios), poissonsRatios.Length);
            arraysLengthMatch(nameof(modulusOfElasticities), modulusOfElasticities.Length, nameof(thermalCoefficients), thermalCoefficients.Length);
            arraysLengthMatch(nameof(modulusOfElasticities), modulusOfElasticities.Length, nameof(shearModuluses), shearModuluses.Length);

            _callCode = _sapModel.PropMaterial.SetMPOrthotropic(name, ref modulusOfElasticities, ref poissonsRatios, ref thermalCoefficients, ref shearModuluses, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion

        #region Methods: Basic Types 
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// This function retrieves the other material property data for aluminum materials.
        /// The function returns an error if the specified material is not aluminum.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="name">The name of an existing aluminum material property.</param>
        /// <param name="aluminumType">The type of aluminum.</param>
        /// <param name="alloy">The Alloy designation for the aluminum, for example, 2014-T6 for wrought or 356.0-T7 for cast (mold or sand) aluminum.</param>
        /// <param name="Fcy">The compressive yield strength of aluminum. [F/L^2].</param>
        /// <param name="Fty">The tensile yield strength of aluminum. [F/L^2].</param>
        /// <param name="Ftu">The tensile ultimate strength of aluminum. [F/L^2].</param>
        /// <param name="Fsu">The shear ultimate strength of aluminum. [F/L^2].</param>
        /// <param name="stressStrainHysteresisType">The stress-strain hysteresis type.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetAluminum(string name,
            ref eAluminumType aluminumType,
            ref string alloy,
            ref double Fcy,
            ref double Fty,
            ref double Ftu,
            ref double Fsu,
            ref eHysteresisType stressStrainHysteresisType,
            double temperature = 0)
        {
            int csiAluminumType = 0;
            int csiStressStrainHysteresisType = 0;

            _callCode = _sapModel.PropMaterial.GetOAluminum(name, ref csiAluminumType, ref alloy, ref Fcy, ref Fty, ref Ftu, ref Fsu, ref csiStressStrainHysteresisType, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            aluminumType = (eAluminumType)csiAluminumType;
            stressStrainHysteresisType = (eHysteresisType)csiStressStrainHysteresisType;
        }

        /// <summary>
        /// This function sets the other material property data for aluminum materials.
        /// </summary>
        /// <param name="name">The name of an existing aluminum material property.</param>
        /// <param name="aluminumType">The type of aluminum.</param>
        /// <param name="alloy">The Alloy designation for the aluminum, for example, 2014-T6 for wrought or 356.0-T7 for cast (mold or sand) aluminum.</param>
        /// <param name="Fcy">The compressive yield strength of aluminum. [F/L^2].</param>
        /// <param name="Fty">The tensile yield strength of aluminum. [F/L^2].</param>
        /// <param name="Ftu">The tensile ultimate strength of aluminum. [F/L^2].</param>
        /// <param name="Fsu">The shear ultimate strength of aluminum. [F/L^2].</param>
        /// <param name="stressStrainHysteresisType">The stress-strain hysteresis type.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetAluminum(string name,
            eAluminumType aluminumType,
            string alloy,
            double Fcy,
            double Fty,
            double Ftu,
            double Fsu,
            eHysteresisType stressStrainHysteresisType,
            double temperature = 0)
        {
            _callCode = _sapModel.PropMaterial.SetOAluminum(name, (int)aluminumType, alloy, Fcy, Fty, Ftu, Fsu, (int)stressStrainHysteresisType, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function retrieves the other material property data for cold formed materials.
        /// The function returns an error if the specified material is not cold formed.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="name">The name of an existing cold formed material property.</param>
        /// <param name="Fy">The minimum yield stress. [F/L^2].</param>
        /// <param name="Fu">The minimum tensile stress. [F/L^2].</param>
        /// <param name="stressStrainHysteresisType">The stress-strain hysteresis type.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetColdFormed(string name,
            ref double Fy,
            ref double Fu,
            ref eHysteresisType stressStrainHysteresisType,
            double temperature = 0)
        {
            int csiStressStrainHysteresisType = 0;

            _callCode = _sapModel.PropMaterial.GetOColdFormed(name, ref Fy, ref Fu, ref csiStressStrainHysteresisType, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            stressStrainHysteresisType = (eHysteresisType)csiStressStrainHysteresisType;
        }

        /// <summary>
        /// This function sets the other material property data for cold formed materials.
        /// </summary>
        /// <param name="name">The name of an existing cold formed material property.</param>
        /// <param name="Fy">The minimum yield stress. [F/L^2].</param>
        /// <param name="Fu">The minimum tensile stress. [F/L^2].</param>
        /// <param name="stressStrainHysteresisType">The stress-strain hysteresis type.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetColdFormed(string name,
            double Fy,
            double Fu,
            eHysteresisType stressStrainHysteresisType,
            double temperature = 0)
        {
            _callCode = _sapModel.PropMaterial.SetOColdFormed(name, Fy, Fu, (int)stressStrainHysteresisType, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif

        /// <summary>
        /// This function retrieves the other material property data for steel materials.
        /// The function returns an error if the specified material is not steel.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="name">The name of an existing rebar material property.</param>
        /// <param name="Fy">The minimum yield stress. [F/L^2].</param>
        /// <param name="Fu">The minimum tensile stress. [F/L^2].</param>
        /// <param name="expectedFy">The expected yield stress. [F/L^2].</param>
        /// <param name="expectedFu">The expected tensile stress. [F/L^2].</param>
        /// <param name="stressStrainCurveType">Type of the stress-strain curve.</param>
        /// <param name="stressStrainHysteresisType">The stress-strain hysteresis type.</param>
        /// <param name="strainAtHardening">The strain at the onset of strain hardening.
        /// This item applies only to parametric stress-strain curves.</param>
        /// <param name="strainAtMaxStress">The strain at maximum stress.
        /// This item applies only to parametric stress-strain curves.</param>
        /// <param name="strainAtRupture">The strain at rupture.
        /// This item applies only to parametric stress-strain curves.</param>
        /// <param name="finalSlope">This item applies only to parametric stress-strain curves.
        /// It is a multiplier on the material modulus of elasticity, E.
        /// This value multiplied times E gives the final slope of the curve.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetSteel(string name,
            ref double Fy,
            ref double Fu,
            ref double expectedFy,
            ref double expectedFu,
            ref eSteelStressStrainCurveType stressStrainCurveType,
            ref eHysteresisType stressStrainHysteresisType,
            ref double strainAtHardening,
            ref double strainAtMaxStress,
            ref double strainAtRupture,
            ref double finalSlope,
            double temperature = 0)
        {
            int csiStressStrainCurveType = 0;
            int csiStressStrainHysteresisType = 0;

            _callCode = _sapModel.PropMaterial.GetOSteel_1(name, ref Fy, ref Fu, ref expectedFy, ref expectedFu, ref csiStressStrainCurveType, ref csiStressStrainHysteresisType, ref strainAtHardening, ref strainAtMaxStress, ref strainAtRupture, ref finalSlope, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            stressStrainCurveType = (eSteelStressStrainCurveType)csiStressStrainCurveType;
            stressStrainHysteresisType = (eHysteresisType)csiStressStrainHysteresisType;
        }

        /// <summary>
        /// This function sets the other material property data for steel materials.
        /// </summary>
        /// <param name="name">The name of an existing rebar material property.</param>
        /// <param name="Fy">The minimum yield stress. [F/L^2].</param>
        /// <param name="Fu">The minimum tensile stress. [F/L^2].</param>
        /// <param name="expectedFy">The expected yield stress. [F/L^2].</param>
        /// <param name="expectedFu">The expected tensile stress. [F/L^2].</param>
        /// <param name="stressStrainCurveType">Type of the stress-strain curve.</param>
        /// <param name="stressStrainHysteresisType">The stress-strain hysteresis type.</param>
        /// <param name="strainAtHardening">The strain at the onset of strain hardening.
        /// This item applies only to parametric stress-strain curves.</param>
        /// <param name="strainAtMaxStress">The strain at maximum stress.
        /// This item applies only to parametric stress-strain curves.</param>
        /// <param name="strainAtRupture">The strain at rupture.
        /// This item applies only to parametric stress-strain curves.</param>
        /// <param name="finalSlope">This item applies only to parametric stress-strain curves.
        /// It is a multiplier on the material modulus of elasticity, E.
        /// This value multiplied times E gives the final slope of the curve.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetSteel(string name,
            double Fy,
            double Fu,
            double expectedFy,
            double expectedFu,
            eSteelStressStrainCurveType stressStrainCurveType,
            eHysteresisType stressStrainHysteresisType,
            double strainAtHardening,
            double strainAtMaxStress,
            double strainAtRupture,
            double finalSlope,
            double temperature = 0)
        {
            _callCode = _sapModel.PropMaterial.SetOSteel_1(name, Fy, Fu, expectedFy, expectedFu, (int)stressStrainCurveType, (int)stressStrainHysteresisType, strainAtHardening, strainAtMaxStress, strainAtRupture, finalSlope, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <summary>
        /// This function retrieves the other material property data for tendon materials.
        /// The function returns an error if the specified material is not tendon.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="name">The name of an existing tendon material property.</param>
        /// <param name="Fy">The minimum yield stress. [F/L^2].</param>
        /// <param name="Fu">The minimum tensile stress. [F/L^2].</param>
        /// <param name="stressStrainCurveType">Type of the stress-strain curve.</param>
        /// <param name="stressStrainHysteresisType">The stress-strain hysteresis type.</param>
        /// <param name="finalSlope">This item applies only to parametric stress-strain curves.
        /// It is a multiplier on the material modulus of elasticity, E.
        /// This value multiplied times E gives the final slope of the curve.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetTendon(string name,
            ref double Fy,
            ref double Fu,
            ref eTendonStressStrainCurveType stressStrainCurveType,
            ref eHysteresisType stressStrainHysteresisType,
            ref double finalSlope,
            double temperature = 0)
        {
            int csiStressStrainCurveType = 0;
            int csiStressStrainHysteresisType = 0;

            _callCode = _sapModel.PropMaterial.GetOTendon_1(name, ref Fy, ref Fu, ref csiStressStrainCurveType, ref csiStressStrainHysteresisType, ref finalSlope, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            stressStrainCurveType = (eTendonStressStrainCurveType)csiStressStrainCurveType;
            stressStrainHysteresisType = (eHysteresisType)csiStressStrainHysteresisType;
        }

        /// <summary>
        /// This function sets the other material property data for tendon materials.
        /// </summary>
        /// <param name="name">The name of an existing tendon material property.</param>
        /// <param name="Fy">The minimum yield stress. [F/L^2].</param>
        /// <param name="Fu">The minimum tensile stress. [F/L^2].</param>
        /// <param name="stressStrainCurveType">Type of the stress-strain curve.</param>
        /// <param name="stressStrainHysteresisType">The stress-strain hysteresis type.</param>
        /// <param name="finalSlope">This item applies only to parametric stress-strain curves.
        /// It is a multiplier on the material modulus of elasticity, E.
        /// This value multiplied times E gives the final slope of the curve.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetTendon(string name,
            double Fy,
            double Fu,
            eTendonStressStrainCurveType stressStrainCurveType,
            eHysteresisType stressStrainHysteresisType,
            double finalSlope,
            double temperature = 0)
        {
            _callCode = _sapModel.PropMaterial.SetOTendon_1(name, Fy, Fu, (int)stressStrainCurveType, (int)stressStrainHysteresisType, finalSlope, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function retrieves the other material property data for rebar materials.
        /// The function returns an error if the specified material is not rebar.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="name">The name of an existing rebar material property.</param>
        /// <param name="Fy">The minimum yield stress. [F/L^2].</param>
        /// <param name="Fu">The minimum tensile stress. [F/L^2].</param>
        /// <param name="expectedFy">The expected yield stress. [F/L^2].</param>
        /// <param name="expectedFu">The expected tensile stress. [F/L^2].</param>
        /// <param name="stressStrainCurveType">Type of the stress-strain curve.</param>
        /// <param name="stressStrainHysteresisType">The stress-strain hysteresis type.</param>
        /// <param name="strainAtHardening">The strain at the onset of strain hardening.
        /// This item applies only when parametric stress-strain curves are used and when <paramref name="useCaltransStressStrainDefaults" /> is False.</param>
        /// <param name="strainUltimate">The ultimate strain capacity.
        /// This item must be larger than the <paramref name="strainAtHardening" /> item.
        /// This item applies only when parametric stress-strain curves are used and when <paramref name="useCaltransStressStrainDefaults" /> is False.</param>
        /// <param name="finalSlope">This item applies only to parametric stress-strain curves.
        /// It is a multiplier on the material modulus of elasticity, E.
        /// This value multiplied times E gives the final slope of the curve.</param>
        /// <param name="useCaltransStressStrainDefaults">True: Program uses Caltrans default controlling strain values, which are bar size dependent.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetRebar(string name,
            ref double Fy,
            ref double Fu,
            ref double expectedFy,
            ref double expectedFu,
            ref eRebarStressStrainCurveType stressStrainCurveType,
            ref eHysteresisType stressStrainHysteresisType,
            ref double strainAtHardening,
            ref double strainUltimate,
            ref double finalSlope,
            ref bool useCaltransStressStrainDefaults,
            double temperature = 0)
        {
            int csiStressStrainCurveType = 0;
            int csiStressStrainHysteresisType = 0;

            _callCode = _sapModel.PropMaterial.GetORebar_1(name, ref Fy, ref Fu, ref expectedFy, ref expectedFu, ref csiStressStrainCurveType, ref csiStressStrainHysteresisType, ref strainAtHardening, ref strainUltimate, ref finalSlope, ref useCaltransStressStrainDefaults, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            stressStrainCurveType = (eRebarStressStrainCurveType)csiStressStrainCurveType;
            stressStrainHysteresisType = (eHysteresisType)csiStressStrainHysteresisType;
        }

        /// <summary>
        /// This function sets the other material property data for rebar materials.
        /// </summary>
        /// <param name="name">The name of an existing rebar material property.</param>
        /// <param name="Fy">The minimum yield stress. [F/L^2].</param>
        /// <param name="Fu">The minimum tensile stress. [F/L^2].</param>
        /// <param name="expectedFy">The expected yield stress. [F/L^2].</param>
        /// <param name="expectedFu">The expected tensile stress. [F/L^2].</param>
        /// <param name="stressStrainCurveType">Type of the stress-strain curve.</param>
        /// <param name="stressStrainHysteresisType">The stress-strain hysteresis type.</param>
        /// <param name="strainAtHardening">The strain at the onset of strain hardening.
        /// This item applies only when parametric stress-strain curves are used and when <paramref name="useCaltransStressStrainDefaults" /> is False.</param>
        /// <param name="strainUltimate">The ultimate strain capacity.
        /// This item must be larger than the <paramref name="strainAtHardening" /> item.
        /// This item applies only when parametric stress-strain curves are used and when <paramref name="useCaltransStressStrainDefaults" /> is False.</param>
        /// <param name="finalSlope">This item applies only to parametric stress-strain curves.
        /// It is a multiplier on the material modulus of elasticity, E.
        /// This value multiplied times E gives the final slope of the curve.</param>
        /// <param name="useCaltransStressStrainDefaults">True: Program uses Caltrans default controlling strain values, which are bar size dependent.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetRebar(string name,
            double Fy,
            double Fu,
            double expectedFy,
            double expectedFu,
            eRebarStressStrainCurveType stressStrainCurveType,
            eHysteresisType stressStrainHysteresisType,
            double strainAtHardening,
            double strainUltimate,
            double finalSlope,
            bool useCaltransStressStrainDefaults,
            double temperature = 0)
        {
            _callCode = _sapModel.PropMaterial.SetORebar_1(name, Fy, Fu, expectedFy, expectedFu, (int)stressStrainCurveType, (int)stressStrainHysteresisType, strainAtHardening, strainUltimate, finalSlope, useCaltransStressStrainDefaults, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function retrieves the other material property data for concrete materials.
        /// The function returns an error if the specified material is not concrete.
        /// TODO: Handle
        /// </summary>
        /// <param name="name">The name of an existing concrete material property.</param>
        /// <param name="fc">The concrete compressive strength. [F/L^2].</param>
        /// <param name="isLightweight">True: The concrete is assumed to be lightweight concrete.</param>
        /// <param name="shearStrengthReductionFactor">The shear strength reduction factor for lightweight concrete.</param>
        /// <param name="stressStrainCurveType">Type of the stress-strain curve.</param>
        /// <param name="stressStrainHysteresisType">The stress-strain hysteresis type.</param>
        /// <param name="strainUnconfinedCompressive">The strain at the unconfined compressive strength.
        /// This item applies only to parametric stress-strain curves.</param>
        /// <param name="strainUltimate">The ultimate unconfined strain capacity.
        /// This item applies only to parametric stress-strain curves.</param>
        /// <param name="finalSlope">This item applies only to parametric stress-strain curves.
        /// It is a multiplier on the material modulus of elasticity, E.
        /// This value multiplied times E gives the final slope of the curve.</param>
        /// <param name="frictionAngle">The Drucker-Prager friction angle, 0 &lt;= <paramref name="frictionAngle" /> &lt; 90. [deg].</param>
        /// <param name="dilatationalAngle">The Drucker-Prager dilatational angle, 0 &lt;= <paramref name="dilatationalAngle" /> &lt; 90. [deg].</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetConcrete(string name,
            ref double fc,
            ref bool isLightweight,
            double shearStrengthReductionFactor,
            ref eConcreteStressStrainCurveType stressStrainCurveType,
            ref eHysteresisType stressStrainHysteresisType,
            ref double strainUnconfinedCompressive,
            ref double strainUltimate,
            ref double finalSlope,
            ref double frictionAngle,
            ref double dilatationalAngle,
            double temperature = 0)
        {
            int csiStressStrainCurveType = 0;
            int csiStressStrainHysteresisType = 0;

            _callCode = _sapModel.PropMaterial.GetOConcrete_1(name, ref fc, ref isLightweight, ref shearStrengthReductionFactor, ref csiStressStrainCurveType, ref csiStressStrainHysteresisType, ref strainUnconfinedCompressive, ref strainUltimate, ref finalSlope, ref frictionAngle, ref dilatationalAngle, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            stressStrainCurveType = (eConcreteStressStrainCurveType)csiStressStrainCurveType;
            stressStrainHysteresisType = (eHysteresisType)csiStressStrainHysteresisType;
        }

        /// <summary>
        /// This function sets the other material property data for concrete materials.
        /// </summary>
        /// <param name="name">The name of an existing concrete material property.</param>
        /// <param name="fc">The concrete compressive strength. [F/L^2].</param>
        /// <param name="isLightweight">True: The concrete is assumed to be lightweight concrete.</param>
        /// <param name="shearStrengthReductionFactor">The shear strength reduction factor for lightweight concrete.</param>
        /// <param name="stressStrainCurveType">Type of the stress-strain curve.</param>
        /// <param name="stressStrainHysteresisType">The stress-strain hysteresis type.</param>
        /// <param name="strainUnconfinedCompressive">The strain at the unconfined compressive strength.
        /// This item applies only to parametric stress-strain curves.</param>
        /// <param name="strainUltimate">The ultimate unconfined strain capacity.
        /// This item applies only to parametric stress-strain curves.</param>
        /// <param name="finalSlope">This item applies only to parametric stress-strain curves.
        /// It is a multiplier on the material modulus of elasticity, E.
        /// This value multiplied times E gives the final slope of the curve.</param>
        /// <param name="frictionAngle">The Drucker-Prager friction angle, 0 &lt;= <paramref name="frictionAngle" /> &lt; 90. [deg].</param>
        /// <param name="dilatationalAngle">The Drucker-Prager dilatational angle, 0 &lt;= <paramref name="dilatationalAngle" /> &lt; 90. [deg].</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetConcrete(string name,
            double fc,
            bool isLightweight,
            double shearStrengthReductionFactor,
            eConcreteStressStrainCurveType stressStrainCurveType,
            eHysteresisType stressStrainHysteresisType,
            double strainUnconfinedCompressive,
            double strainUltimate,
            double finalSlope,
            double frictionAngle = 0,
            double dilatationalAngle = 0,
            double temperature = 0)
        {
            _callCode = _sapModel.PropMaterial.SetOConcrete_1(name, fc, isLightweight, shearStrengthReductionFactor, (int)stressStrainCurveType, (int)stressStrainHysteresisType, strainUnconfinedCompressive, strainUltimate, finalSlope, frictionAngle, dilatationalAngle, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function retrieves the other material property data for no design type materials.
        /// The function returns an error if the specified material is not concrete.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="name">The name of an existing concrete material property.</param>
        /// <param name="frictionAngle">The Drucker-Prager friction angle, 0 &lt;= <paramref name="frictionAngle" /> &lt; 90. [deg].</param>
        /// <param name="dilatationalAngle">The Drucker-Prager dilatational angle, 0 &lt;= <paramref name="dilatationalAngle" /> &lt; 90. [deg].</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetNoDesign(string name,
            double frictionAngle,
            double dilatationalAngle,
            double temperature = 0)
        {
            _callCode = _sapModel.PropMaterial.GetONoDesign(name, ref frictionAngle, ref dilatationalAngle, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the other material property data for no design type materials.
        /// </summary>
        /// <param name="name">The name of an existing concrete material property.</param>
        /// <param name="frictionAngle">The Drucker-Prager friction angle, 0 &lt;= <paramref name="frictionAngle" /> &lt; 90. [deg].</param>
        /// <param name="dilatationalAngle">The Drucker-Prager dilatational angle, 0 &lt;= <paramref name="dilatationalAngle" /> &lt; 90. [deg].</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent.
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved.
        /// The temperature must have been previously defined for the material.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetNoDesign(string name,
            double frictionAngle = 0,
            double dilatationalAngle = 0,
            double temperature = 0)
        {
            _callCode = _sapModel.PropMaterial.SetONoDesign(name, frictionAngle, dilatationalAngle, temperature);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
       
        #endregion
    }
}
