using System.Linq;
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.TimeDependent;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Property
{
    [TestFixture]
    public class MaterialProperties_Get : CsiGet
    {
        [Test]
        public void Count()
        {
            int numberOfObjects = _app.Model.Definitions.Properties.MaterialProperties.Count();
            Assert.That(numberOfObjects, Is.EqualTo(CSiDataMaterial.NumberExpected));
        }

        [Test]
        public void GetNameList()
        {
            string[] names;
            _app.Model.Definitions.Properties.MaterialProperties.GetNameList(out names);

            Assert.That(names.Length, Is.EqualTo(CSiDataMaterial.NumberExpected));
            Assert.That(names.Contains(CSiDataMaterial.NameSteel));
            Assert.That(names.Contains(CSiDataMaterial.NameConcrete));
            Assert.That(names.Contains(CSiDataMaterial.NameTendon));
        }


        public void GetMaterialType(string name,
            ref eMaterialPropertyType materialType,
            ref eMaterialSymmetryType symmetryType)
        {

        }


        public void GetMaterial(string name,
            ref eMaterialPropertyType materialType,
            ref int color,
            ref string notes,
            ref string GUID)
        {

        }


        public void GetDamping(string name,
            ref double modalDampingRatio,
            ref double viscousMassCoefficient,
            ref double viscousStiffnessCoefficient,
            ref double hystereticMassCoefficient,
            ref double hystereticStiffnessCoefficient,
            double temperature = 0)
        {

        }


        public void GetWeightAndMass(string name,
            ref double weightPerVolume,
            ref double massPerVolume,
            double temperature = 0)
        {

        }


        public void GetTemperature(string name,
            ref double[] temperatures)
        {

        }


        public void GetStressStrainCurve(string name,
            ref eStressStrainPointID[] pointID,
            ref double[] strain,
            ref double[] stress,
            string sectionName = "",
            double rebarArea = 0,
            double temperature = 0)
        {

        }


        public void GetMechanicalPropertiesIsotropic(string name,
            ref double modulusOfElasticity,
            ref double poissonsRatio,
            ref double thermalCoefficient,
            ref double shearModulus,
            double temperature = 0)
        {

        }


        public void GetMechanicalPropertiesUniaxial(string name,
            ref double modulusOfElasticity,
            ref double thermalCoefficient,
            double temperature = 0)
        {

        }


        public void GetMechanicalPropertiesAnisotropic(string name,
            ref double[] modulusOfElasticities,
            ref double[] poissonsRatios,
            ref double[] thermalCoefficients,
            ref double[] shearModuluses,
            double temperature = 0)
        {

        }


        public void GetMechanicalPropertiesOrthotropic(string name,
            ref double[] modulusOfElasticities,
            ref double[] poissonsRatios,
            ref double[] thermalCoefficients,
            ref double[] shearModuluses,
            double temperature = 0)
        {

        }




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

        }


        public void GetTendon(string name,
            ref double Fy,
            ref double Fu,
            ref eTendonStressStrainCurveType stressStrainCurveType,
            ref eHysteresisType stressStrainHysteresisType,
            ref double finalSlope,
            double temperature = 0)
        {

        }


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

        }



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

        }


        public void GetNoDesign(string name,
            double frictionAngle,
            double dilatationalAngle,
            double temperature = 0)
        {

        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017  

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

            
        }

        
        public void GetColdFormed(string name,
            ref double Fy,
            ref double Fu,
            ref eHysteresisType stressStrainHysteresisType,
            double temperature = 0)
        {
          
        }
#else

        public void GetMassSource(ref bool massFromElements,
            ref bool massFromMasses,
            ref bool massFromLoads,
            ref int numberLoads,
            ref string[] namesLoadPatterns,
            ref double[] scaleFactors)
        {
          
        }
#endif
    }
    
    [TestFixture]
    public class MaterialProperties_Set : CsiSet
    {
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        public void SetMaterial(string name,
            eMaterialPropertyType materialType,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
          
        }

        
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
          
        }

        
        public void SetColdFormed(string name,
            double Fy,
            double Fu,
            eHysteresisType stressStrainHysteresisType,
            double temperature = 0)
        {
          
        }
#else
  
        
        public void SetMassSource(bool massFromElements,
            bool massFromMasses,
            bool massFromLoads,
            string[] namesLoadPatterns,
            double[] scaleFactors)
        {
          
        }
#endif
        
        
        public void AddMaterial(ref string name,
            eMaterialPropertyType materialType,
            string region,
            string standardName,
            string grade,
            string userName = "")
        {
          
        }

        
        public void Delete(string name)
        {
          
        }

        
        public void ChangeName(string currentName, 
            string newName)
        {
          
        }

        
        public void SetDamping(string name,
            double modalDampingRatio,
            double viscousMassCoefficient,
            double viscousStiffnessCoefficient,
            double hystereticMassCoefficient,
            double hystereticStiffnessCoefficient,
            double temperature = 0)
        {
          
        }

        
        public void SetWeightAndMass(string name,
            eMaterialPerVolumeOption perUnitVolumeOption,
            double value,
            double temperature = 0)
        {
          
        }
        
        
        public void SetTemperature(string name,
            double[] temperatures)
        {
          
        }


        
        public void SetStressStrainCurve(string name,
            eStressStrainPointID[] pointID,
            double[] strain,
            double[] stress,
            double temperature = 0)
        {
          
        }

        
        public void SetMechanicalPropertiesIsotropic(string name,
            double modulusOfElasticity,
            double poissonsRatio,
            double thermalCoefficient,
            double temperature = 0)
        {
          
        }
        
        
        public void SetMechanicalPropertiesUniaxial(string name,
            double modulusOfElasticity,
            double thermalCoefficient,
            double temperature = 0)
        {
          
        }
        
        
        public void SetMechanicalPropertiesAnisotropic(string name,
            double[] modulusOfElasticities,
            double[] poissonsRatios,
            double[] thermalCoefficients,
            double[] shearModuluses,
            double temperature = 0)
        {
          
        }

        
        public void SetMechanicalPropertiesOrthotropic(string name,
            double[] modulusOfElasticities,
            double[] poissonsRatios,
            double[] thermalCoefficients,
            double[] shearModuluses,
            double temperature = 0)
        {
          
        }
        
        
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
          
        }

        
        public void SetTendon(string name,
            double Fy,
            double Fu,
            eTendonStressStrainCurveType stressStrainCurveType,
            eHysteresisType stressStrainHysteresisType,
            double finalSlope,
            double temperature = 0)
        {
          
        }
        
        
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
          
        }

        
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
          
        }

        
        public void SetNoDesign(string name,
            double frictionAngle = 0,
            double dilatationalAngle = 0,
            double temperature = 0)
        {
          
        }

    }
}
