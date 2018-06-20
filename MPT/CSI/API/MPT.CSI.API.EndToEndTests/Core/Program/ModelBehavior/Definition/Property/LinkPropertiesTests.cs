using System;
using System.Linq;
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.API.Core.Support;
using MPT.CSI.API.EndToEndTests.Core;

//SAP2000 API: SetMultiLinearPoints does not allow first force value to be >= 0
//API function PropLink.SetMultiLinearPoints resets to default if forces in both points are zero


namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Property
{
    [TestFixture]
    public class LinkProperties_Get : CsiGet
    {
        private const string _nameMultiLinearElastic = "MultiLinearElastic";
        private const string _nameMultiLinearPlastic = "MultiLinearPlastic";

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

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
          
        }
        

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
          
        }

#endif
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
          
        }
        
        
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
          
        }

        
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
          
        }

        
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
          
        }


        [Test]
        public void GetMultiLinearElastic()
        {
            string name = _nameMultiLinearElastic;
            DegreesOfFreedomLocal degreesOfFreedom;
            DegreesOfFreedomLocal fixity;
            DegreesOfFreedomLocal nonLinear;
            Stiffness effectiveStiffness;
            Stiffness effectiveDamping;
            double distanceFromJEndToU2Spring;
            double distanceFromJEndToU3Spring;
            string notes;
            string GUID;

            _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearElastic(name,
                out degreesOfFreedom,
                out fixity,
                out nonLinear,
                out effectiveStiffness,
                out effectiveDamping,
                out distanceFromJEndToU2Spring,
                out distanceFromJEndToU3Spring,
                out notes,
                out GUID);

            // Check output
            string oldNotes = name;
            Assert.That(notes, Is.EqualTo(oldNotes));
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
            Assert.That(!string.IsNullOrEmpty(GUID));
#else
            Assert.That(GUID, Is.EqualTo(CSiData.OldGUID));
#endif
            Assert.That(distanceFromJEndToU2Spring, Is.EqualTo(CSiData.oldDistanceFromJEndToU2Spring).Within(0.001));
            Assert.That(distanceFromJEndToU3Spring, Is.EqualTo(CSiData.oldDistanceFromJEndToU3Spring).Within(0.001));

            // U1
            Assert.That(degreesOfFreedom.U1, Is.EqualTo(degreesOfFreedom.U1));
            Assert.That(fixity.U1, Is.EqualTo(CSiData.oldFixity.U1));
            Assert.That(nonLinear.U1, Is.EqualTo(CSiData.oldNonlinear.U1));
            Assert.That(effectiveStiffness.U1, Is.EqualTo(CSiData.oldEffectiveStiffness.U1).Within(0.001));
            Assert.That(effectiveDamping.U1, Is.EqualTo(CSiData.oldEffectiveDamping.U1).Within(0.001));

            // U2
            Assert.That(degreesOfFreedom.U2, Is.EqualTo(degreesOfFreedom.U2));
            Assert.That(fixity.U2, Is.EqualTo(CSiData.oldFixity.U2));
            Assert.That(nonLinear.U2, Is.EqualTo(CSiData.oldNonlinear.U2));
            Assert.That(effectiveStiffness.U2, Is.EqualTo(CSiData.oldEffectiveStiffness.U2).Within(0.001));
            Assert.That(effectiveDamping.U2, Is.EqualTo(CSiData.oldEffectiveDamping.U2).Within(0.001));

            // U3
            Assert.That(degreesOfFreedom.U3, Is.EqualTo(degreesOfFreedom.U3));
            Assert.That(fixity.U3, Is.EqualTo(CSiData.oldFixity.U3));
            Assert.That(nonLinear.U3, Is.EqualTo(CSiData.oldNonlinear.U3));
            Assert.That(effectiveStiffness.U3, Is.EqualTo(CSiData.oldEffectiveStiffness.U3).Within(0.001));
            Assert.That(effectiveDamping.U3, Is.EqualTo(CSiData.oldEffectiveDamping.U3).Within(0.001));

            // R1
            Assert.That(degreesOfFreedom.R1, Is.EqualTo(degreesOfFreedom.R1));
            Assert.That(fixity.R1, Is.EqualTo(CSiData.oldFixity.R1));
            Assert.That(nonLinear.R1, Is.EqualTo(CSiData.oldNonlinear.R1));
            Assert.That(effectiveStiffness.R1, Is.EqualTo(CSiData.oldEffectiveStiffness.R1).Within(0.001));
            Assert.That(effectiveDamping.R1, Is.EqualTo(CSiData.oldEffectiveDamping.R1).Within(0.001));

            // R2
            Assert.That(degreesOfFreedom.R2, Is.EqualTo(degreesOfFreedom.R2));
            Assert.That(fixity.R2, Is.EqualTo(CSiData.oldFixity.R2));
            Assert.That(nonLinear.R2, Is.EqualTo(CSiData.oldNonlinear.R2));
            Assert.That(effectiveStiffness.R2, Is.EqualTo(CSiData.oldEffectiveStiffness.R2).Within(0.001));
            Assert.That(effectiveDamping.R2, Is.EqualTo(CSiData.oldEffectiveDamping.R2).Within(0.001));

            // R3
            Assert.That(degreesOfFreedom.R3, Is.EqualTo(degreesOfFreedom.R3));
            Assert.That(fixity.R3, Is.EqualTo(CSiData.oldFixity.R3));
            Assert.That(nonLinear.R3, Is.EqualTo(CSiData.oldNonlinear.R3));
            Assert.That(effectiveStiffness.R3, Is.EqualTo(CSiData.oldEffectiveStiffness.R3).Within(0.001));
            Assert.That(effectiveDamping.R3, Is.EqualTo(CSiData.oldEffectiveDamping.R3).Within(0.001));
        }

        [Test]
        public void GetMultiLinearPlastic()
        {
            string name = _nameMultiLinearPlastic;
            DegreesOfFreedomLocal degreesOfFreedom;
            DegreesOfFreedomLocal fixity;
            DegreesOfFreedomLocal nonLinear;
            Stiffness effectiveStiffness;
            Stiffness effectiveDamping;
            double distanceFromJEndToU2Spring;
            double distanceFromJEndToU3Spring;
            string notes;
            string GUID;

            _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPlastic(name,
                out degreesOfFreedom,
                out fixity,
                out nonLinear,
                out effectiveStiffness,
                out effectiveDamping,
                out distanceFromJEndToU2Spring,
                out distanceFromJEndToU3Spring,
                out notes,
                out GUID);

            // Check output
            string oldNotes = name;
            Assert.That(notes, Is.EqualTo(oldNotes));
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
            Assert.That(!string.IsNullOrEmpty(GUID));
#else
            Assert.That(GUID, Is.EqualTo(CSiData.OldGUID));
#endif
            Assert.That(distanceFromJEndToU2Spring, Is.EqualTo(CSiData.oldDistanceFromJEndToU2Spring).Within(0.001));
            Assert.That(distanceFromJEndToU3Spring, Is.EqualTo(CSiData.oldDistanceFromJEndToU3Spring).Within(0.001));

            // U1
            Assert.That(degreesOfFreedom.U1, Is.EqualTo(degreesOfFreedom.U1));
            Assert.That(fixity.U1, Is.EqualTo(CSiData.oldFixity.U1));
            Assert.That(nonLinear.U1, Is.EqualTo(CSiData.oldNonlinear.U1));
            Assert.That(effectiveStiffness.U1, Is.EqualTo(CSiData.oldEffectiveStiffness.U1).Within(0.001));
            Assert.That(effectiveDamping.U1, Is.EqualTo(CSiData.oldEffectiveDamping.U1).Within(0.001));

            // U2
            Assert.That(degreesOfFreedom.U2, Is.EqualTo(degreesOfFreedom.U2));
            Assert.That(fixity.U2, Is.EqualTo(CSiData.oldFixity.U2));
            Assert.That(nonLinear.U2, Is.EqualTo(CSiData.oldNonlinear.U2));
            Assert.That(effectiveStiffness.U2, Is.EqualTo(CSiData.oldEffectiveStiffness.U2).Within(0.001));
            Assert.That(effectiveDamping.U2, Is.EqualTo(CSiData.oldEffectiveDamping.U2).Within(0.001));

            // U3
            Assert.That(degreesOfFreedom.U3, Is.EqualTo(degreesOfFreedom.U3));
            Assert.That(fixity.U3, Is.EqualTo(CSiData.oldFixity.U3));
            Assert.That(nonLinear.U3, Is.EqualTo(CSiData.oldNonlinear.U3));
            Assert.That(effectiveStiffness.U3, Is.EqualTo(CSiData.oldEffectiveStiffness.U3).Within(0.001));
            Assert.That(effectiveDamping.U3, Is.EqualTo(CSiData.oldEffectiveDamping.U3).Within(0.001));

            // R1
            Assert.That(degreesOfFreedom.R1, Is.EqualTo(degreesOfFreedom.R1));
            Assert.That(fixity.R1, Is.EqualTo(CSiData.oldFixity.R1));
            Assert.That(nonLinear.R1, Is.EqualTo(CSiData.oldNonlinear.R1));
            Assert.That(effectiveStiffness.R1, Is.EqualTo(CSiData.oldEffectiveStiffness.R1).Within(0.001));
            Assert.That(effectiveDamping.R1, Is.EqualTo(CSiData.oldEffectiveDamping.R1).Within(0.001));

            // R2
            Assert.That(degreesOfFreedom.R2, Is.EqualTo(degreesOfFreedom.R2));
            Assert.That(fixity.R2, Is.EqualTo(CSiData.oldFixity.R2));
            Assert.That(nonLinear.R2, Is.EqualTo(CSiData.oldNonlinear.R2));
            Assert.That(effectiveStiffness.R2, Is.EqualTo(CSiData.oldEffectiveStiffness.R2).Within(0.001));
            Assert.That(effectiveDamping.R2, Is.EqualTo(CSiData.oldEffectiveDamping.R2).Within(0.001));

            // R3
            Assert.That(degreesOfFreedom.R3, Is.EqualTo(degreesOfFreedom.R3));
            Assert.That(fixity.R3, Is.EqualTo(CSiData.oldFixity.R3));
            Assert.That(nonLinear.R3, Is.EqualTo(CSiData.oldNonlinear.R3));
            Assert.That(effectiveStiffness.R3, Is.EqualTo(CSiData.oldEffectiveStiffness.R3).Within(0.001));
            Assert.That(effectiveDamping.R3, Is.EqualTo(CSiData.oldEffectiveDamping.R3).Within(0.001));
        }

        [Test]
        public void GetMultiLinearPoints_Elastic_Linear_DOF_Throws_CSiException()
        {
            string name = _nameMultiLinearElastic;

            double[] forces;
            double[] displacements;
            eLinkHysteresisType linkHysteresisType;
            double alpha1;
            double alpha2;
            double beta1;
            double beta2;
            double eta;


            // U1
            eDegreeOfFreedom degreeOfFreedom = eDegreeOfFreedom.U1;
            // In model, U1 is not linear
            Assert.Throws<CSiException>(() =>
            {
                _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPoints(name,
                degreeOfFreedom,
                out forces,
                out displacements,
                out linkHysteresisType,
                out alpha1,
                out alpha2,
                out beta1,
                out beta2,
                out eta);
            });
        }

        [Test]
        public void GetMultiLinearPoints_Elastic_Fixed_DOF_Throws_CSiException()
        {
            string name = _nameMultiLinearElastic;

            double[] forces;
            double[] displacements;
            eLinkHysteresisType linkHysteresisType;
            double alpha1;
            double alpha2;
            double beta1;
            double beta2;
            double eta;

            // R1
            // In model, R2 is fixed
            eDegreeOfFreedom degreeOfFreedom = eDegreeOfFreedom.R1;
            Assert.Throws<CSiException>(() =>
            {
                _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPoints(name,
                degreeOfFreedom,
                out forces,
                out displacements,
                out linkHysteresisType,
                out alpha1,
                out alpha2,
                out beta1,
                out beta2,
                out eta);
            });

        }

        [Test]
        public void GetMultiLinearPoints_Elastic_Inactive_DOF_Throws_CSiException()
        {
            string name = _nameMultiLinearElastic;

            double[] forces;
            double[] displacements;
            eLinkHysteresisType linkHysteresisType;
            double alpha1;
            double alpha2;
            double beta1;
            double beta2;
            double eta;

            // R2
            // In model, R2 is not active
            eDegreeOfFreedom degreeOfFreedom = eDegreeOfFreedom.R2;
            Assert.Throws<CSiException>(() =>
            {
                _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPoints(name,
                degreeOfFreedom,
                out forces,
                out displacements,
                out linkHysteresisType,
                out alpha1,
                out alpha2,
                out beta1,
                out beta2,
                out eta);
            });
        }

        [Test]
        public void GetMultiLinearPoints_Elastic()
        {
            string name = _nameMultiLinearElastic;
            
            double[] forces;
            double[] displacements;
            eLinkHysteresisType linkHysteresisType;
            double alpha1;
            double alpha2;
            double beta1;
            double beta2;
            double eta;
            
            // U2
            eDegreeOfFreedom degreeOfFreedom = eDegreeOfFreedom.U2;
            _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPoints(name,
                degreeOfFreedom,
                out forces,
                out displacements,
                out linkHysteresisType,
                out alpha1,
                out alpha2,
                out beta1,
                out beta2,
                out eta);



            Assert.That(linkHysteresisType, Is.EqualTo(eLinkHysteresisType.NA));
            Assert.That(alpha1, Is.EqualTo(0));
            Assert.That(alpha2, Is.EqualTo(0));
            Assert.That(beta1, Is.EqualTo(0));
            Assert.That(beta2, Is.EqualTo(0));
            Assert.That(eta, Is.EqualTo(0));

            Assert.That(forces[0], Is.EqualTo(CSiData.oldForces[0]));
            Assert.That(forces[1], Is.EqualTo(CSiData.oldForces[1]));
            Assert.That(forces[2], Is.EqualTo(CSiData.oldForces[2]));
            Assert.That(forces[3], Is.EqualTo(CSiData.oldForces[3]));
            Assert.That(forces[4], Is.EqualTo(CSiData.oldForces[4]));

            Assert.That(displacements[0], Is.EqualTo(CSiData.oldDisplacements[0]));
            Assert.That(displacements[1], Is.EqualTo(CSiData.oldDisplacements[1]));
            Assert.That(displacements[2], Is.EqualTo(CSiData.oldDisplacements[2]));
            Assert.That(displacements[3], Is.EqualTo(CSiData.oldDisplacements[3]));
            Assert.That(displacements[4], Is.EqualTo(CSiData.oldDisplacements[4]));

            // U3
            degreeOfFreedom = eDegreeOfFreedom.U3;
            _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPoints(name,
                degreeOfFreedom,
                out forces,
                out displacements,
                out linkHysteresisType,
                out alpha1,
                out alpha2,
                out beta1,
                out beta2,
                out eta);

            Assert.That(linkHysteresisType, Is.EqualTo(eLinkHysteresisType.NA));
            Assert.That(alpha1, Is.EqualTo(0));
            Assert.That(alpha2, Is.EqualTo(0));
            Assert.That(beta1, Is.EqualTo(0));
            Assert.That(beta2, Is.EqualTo(0));
            Assert.That(eta, Is.EqualTo(0));

            Assert.That(forces[0], Is.EqualTo(CSiData.oldForces[0]));
            Assert.That(forces[1], Is.EqualTo(CSiData.oldForces[1]));
            Assert.That(forces[2], Is.EqualTo(CSiData.oldForces[2]));
            Assert.That(forces[3], Is.EqualTo(CSiData.oldForces[3]));
            Assert.That(forces[4], Is.EqualTo(CSiData.oldForces[4]));

            Assert.That(displacements[0], Is.EqualTo(CSiData.oldDisplacements[0]));
            Assert.That(displacements[1], Is.EqualTo(CSiData.oldDisplacements[1]));
            Assert.That(displacements[2], Is.EqualTo(CSiData.oldDisplacements[2]));
            Assert.That(displacements[3], Is.EqualTo(CSiData.oldDisplacements[3]));
            Assert.That(displacements[4], Is.EqualTo(CSiData.oldDisplacements[4]));


            // R3
            degreeOfFreedom = eDegreeOfFreedom.R3;
            _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPoints(name,
                degreeOfFreedom,
                out forces,
                out displacements,
                out linkHysteresisType,
                out alpha1,
                out alpha2,
                out beta1,
                out beta2,
                out eta);

            Assert.That(linkHysteresisType, Is.EqualTo(eLinkHysteresisType.NA));
            Assert.That(alpha1, Is.EqualTo(0));
            Assert.That(alpha2, Is.EqualTo(0));
            Assert.That(beta1, Is.EqualTo(0));
            Assert.That(beta2, Is.EqualTo(0));
            Assert.That(eta, Is.EqualTo(0));

            Assert.That(forces[0], Is.EqualTo(CSiData.oldForces[0]));
            Assert.That(forces[1], Is.EqualTo(CSiData.oldForces[1]));
            Assert.That(forces[2], Is.EqualTo(CSiData.oldForces[2]));
            Assert.That(forces[3], Is.EqualTo(CSiData.oldForces[3]));
            Assert.That(forces[4], Is.EqualTo(CSiData.oldForces[4]));

            Assert.That(displacements[0], Is.EqualTo(CSiData.oldDisplacements[0]));
            Assert.That(displacements[1], Is.EqualTo(CSiData.oldDisplacements[1]));
            Assert.That(displacements[2], Is.EqualTo(CSiData.oldDisplacements[2]));
            Assert.That(displacements[3], Is.EqualTo(CSiData.oldDisplacements[3]));
            Assert.That(displacements[4], Is.EqualTo(CSiData.oldDisplacements[4]));
        }

        [Test]
        public void GetMultiLinearPoints_Plastic_Linear_DOF_Throws_CSiException()
        {
            string name = _nameMultiLinearPlastic;

            double[] forces;
            double[] displacements;
            eLinkHysteresisType linkHysteresisType;
            double alpha1;
            double alpha2;
            double beta1;
            double beta2;
            double eta;

            // U1
            eDegreeOfFreedom degreeOfFreedom = eDegreeOfFreedom.U1;
            // In model, U1 is linear
            Assert.Throws<CSiException>(() =>
            {
                _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPoints(name,
                    degreeOfFreedom,
                    out forces,
                    out displacements,
                    out linkHysteresisType,
                    out alpha1,
                    out alpha2,
                    out beta1,
                    out beta2,
                    out eta);
            });
        }

        [Test]
        public void GetMultiLinearPoints_Plastic_Fixed_DOF_Throws_CSiException()
        {
            string name = _nameMultiLinearPlastic;

            double[] forces;
            double[] displacements;
            eLinkHysteresisType linkHysteresisType;
            double alpha1;
            double alpha2;
            double beta1;
            double beta2;
            double eta;

            // R1
            eDegreeOfFreedom degreeOfFreedom = eDegreeOfFreedom.R1;
            // In model, R1 is fixed
            Assert.Throws<CSiException>(() =>
            {
                _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPoints(name,
                degreeOfFreedom,
                out forces,
                out displacements,
                out linkHysteresisType,
                out alpha1,
                out alpha2,
                out beta1,
                out beta2,
                out eta);
            });
        }

        [Test]
        public void GetMultiLinearPoints_Plastic_Inactive_DOF_Throws_CSiException()
        {
            string name = _nameMultiLinearPlastic;

            double[] forces;
            double[] displacements;
            eLinkHysteresisType linkHysteresisType;
            double alpha1;
            double alpha2;
            double beta1;
            double beta2;
            double eta;

            // R2
            // In model, R2 is not active
            eDegreeOfFreedom degreeOfFreedom = eDegreeOfFreedom.R2;
            Assert.Throws<CSiException>(() =>
            {
                _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPoints(name,
                degreeOfFreedom,
                out forces,
                out displacements,
                out linkHysteresisType,
                out alpha1,
                out alpha2,
                out beta1,
                out beta2,
                out eta);
            });
        }

        [Test]
        public void GetMultiLinearPoints_Plastic()
        {
            string name = _nameMultiLinearPlastic;

            double[] forces;
            double[] displacements;
            eLinkHysteresisType linkHysteresisType;
            double alpha1;
            double alpha2;
            double beta1;
            double beta2;
            double eta;

            // U2
            eDegreeOfFreedom degreeOfFreedom = eDegreeOfFreedom.U2;
            _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPoints(name,
                degreeOfFreedom,
                out forces,
                out displacements,
                out linkHysteresisType,
                out alpha1,
                out alpha2,
                out beta1,
                out beta2,
                out eta);

            Assert.That(linkHysteresisType, Is.EqualTo(eLinkHysteresisType.Kinematic));
            Assert.That(alpha1, Is.EqualTo(0));
            Assert.That(alpha2, Is.EqualTo(0));
            Assert.That(beta1, Is.EqualTo(0));
            Assert.That(beta2, Is.EqualTo(0));
            Assert.That(eta, Is.EqualTo(0));

            Assert.That(forces[0], Is.EqualTo(CSiData.oldForces[0]));
            Assert.That(forces[1], Is.EqualTo(CSiData.oldForces[1]));
            Assert.That(forces[2], Is.EqualTo(CSiData.oldForces[2]));
            Assert.That(forces[3], Is.EqualTo(CSiData.oldForces[3]));
            Assert.That(forces[4], Is.EqualTo(CSiData.oldForces[4]));

            Assert.That(displacements[0], Is.EqualTo(CSiData.oldDisplacements[0]));
            Assert.That(displacements[1], Is.EqualTo(CSiData.oldDisplacements[1]));
            Assert.That(displacements[2], Is.EqualTo(CSiData.oldDisplacements[2]));
            Assert.That(displacements[3], Is.EqualTo(CSiData.oldDisplacements[3]));
            Assert.That(displacements[4], Is.EqualTo(CSiData.oldDisplacements[4]));

            // U3
            degreeOfFreedom = eDegreeOfFreedom.U3;
            _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPoints(name,
                degreeOfFreedom,
                out forces,
                out displacements,
                out linkHysteresisType,
                out alpha1,
                out alpha2,
                out beta1,
                out beta2,
                out eta);

            Assert.That(linkHysteresisType, Is.EqualTo(eLinkHysteresisType.Pivot));
            Assert.That(alpha1, Is.EqualTo(CSiData.oldPivotAlpha1));
            Assert.That(alpha2, Is.EqualTo(CSiData.oldPivotAlpha2));
            Assert.That(beta1, Is.EqualTo(CSiData.oldPivotBeta1));
            Assert.That(beta2, Is.EqualTo(CSiData.oldPivotBeta2));
            Assert.That(eta, Is.EqualTo(CSiData.oldPivotEta));

            Assert.That(forces[0], Is.EqualTo(CSiData.oldForces[0]));
            Assert.That(forces[1], Is.EqualTo(CSiData.oldForces[1]));
            Assert.That(forces[2], Is.EqualTo(CSiData.oldForces[2]));
            Assert.That(forces[3], Is.EqualTo(CSiData.oldForces[3]));
            Assert.That(forces[4], Is.EqualTo(CSiData.oldForces[4]));

            Assert.That(displacements[0], Is.EqualTo(CSiData.oldDisplacements[0]));
            Assert.That(displacements[1], Is.EqualTo(CSiData.oldDisplacements[1]));
            Assert.That(displacements[2], Is.EqualTo(CSiData.oldDisplacements[2]));
            Assert.That(displacements[3], Is.EqualTo(CSiData.oldDisplacements[3]));
            Assert.That(displacements[4], Is.EqualTo(CSiData.oldDisplacements[4]));

            // R3
            degreeOfFreedom = eDegreeOfFreedom.R3;
            _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPoints(name,
                degreeOfFreedom,
                out forces,
                out displacements,
                out linkHysteresisType,
                out alpha1,
                out alpha2,
                out beta1,
                out beta2,
                out eta);

            Assert.That(linkHysteresisType, Is.EqualTo(eLinkHysteresisType.Takeda));
            Assert.That(alpha1, Is.EqualTo(0));
            Assert.That(alpha2, Is.EqualTo(0));
            Assert.That(beta1, Is.EqualTo(0));
            Assert.That(beta2, Is.EqualTo(0));
            Assert.That(eta, Is.EqualTo(0));

            Assert.That(forces[0], Is.EqualTo(CSiData.oldForces[0]));
            Assert.That(forces[1], Is.EqualTo(CSiData.oldForces[1]));
            Assert.That(forces[2], Is.EqualTo(CSiData.oldForces[2]));
            Assert.That(forces[3], Is.EqualTo(CSiData.oldForces[3]));
            Assert.That(forces[4], Is.EqualTo(CSiData.oldForces[4]));

            Assert.That(displacements[0], Is.EqualTo(CSiData.oldDisplacements[0]));
            Assert.That(displacements[1], Is.EqualTo(CSiData.oldDisplacements[1]));
            Assert.That(displacements[2], Is.EqualTo(CSiData.oldDisplacements[2]));
            Assert.That(displacements[3], Is.EqualTo(CSiData.oldDisplacements[3]));
            Assert.That(displacements[4], Is.EqualTo(CSiData.oldDisplacements[4]));
        }


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

        }


        
        public void Count()
        {
          
        }

        
        public void GetNameList(ref string[] names)
        {
          
        }

        
        public void GetNameList(ref string[] names,
            eLinkPropertyType linkType)
        {
          
        }

        
        public void GetLinkType(string name,
            ref eLinkPropertyType linkType)
        {
          
        }

        
        public void GetWeightAndMass(string name,
            ref double weight,
            ref double massTranslational,
            ref double massR1,
            ref double massR2,
            ref double massR3)
        {
          
        }

        
        public void GetPDelta(string name,
            ref PDeltaParameters pDeltaParameters)
        {
          
        }

        
        public void GetSpringData(string name,
            ref double lengthDefined,
            ref double areaDefined)
        {
          
        }

        
        
        
        
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
          
        }

        
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
          
        }
        
        
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
          
        }


        
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
          
        }


        
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
          
        }
        
        
       

    }
    
    [TestFixture]
    public class LinkProperties_Set : CsiSet
    {
        private const string _nameMultiLinearElastic = "MultiLinearElastic";
        private const string _nameMultiLinearPlastic = "MultiLinearPlastic";

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

       
       
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
          
        }

        
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
          
        }
#endif

        public void ChangeName(string currentName, 
            string newName)
        {
          
        }

        
        public void Delete(string name)
        {
          
        }


        
        public void SetWeightAndMass(string name,
            double weight,
            double massTranslational,
            double massR1,
            double massR2,
            double massR3)
        {
          
        }

        
        public void SetPDelta(string name,
            PDeltaParameters pDeltaParameters)
        {
          
        }public void SetSpringData(string name,
            double lengthDefined,
            double areaDefined)
        {
          
        }

        
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
          
        }

        
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
          
        }

        
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
          
        }
        
        
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
          
        }

        
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
          
        }

        
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
          
        }


        
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
          
        }

        [Test]
        public void SetMultiLinearElastic()
        {
            string name = _nameMultiLinearElastic;
            DegreesOfFreedomLocal degreesOfFreedom;
            DegreesOfFreedomLocal fixity;
            DegreesOfFreedomLocal nonLinear;
            Stiffness effectiveStiffness;
            Stiffness effectiveDamping;
            double distanceFromJEndToU2Spring;
            double distanceFromJEndToU3Spring;
            string notes;
            string GUID;

            // Get current model state
            _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearElastic(name,
                out degreesOfFreedom,
                out fixity,
                out nonLinear,
                out effectiveStiffness,
                out effectiveDamping,
                out distanceFromJEndToU2Spring,
                out distanceFromJEndToU3Spring,
                out notes,
                out GUID);

            // Check output
            string oldNotes = name;
            Assert.That(notes, Is.EqualTo(oldNotes));
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
            Assert.That(!string.IsNullOrEmpty(GUID));
#else
            Assert.That(GUID, Is.EqualTo(CSiData.OldGUID));
#endif
            Assert.That(distanceFromJEndToU2Spring, Is.EqualTo(CSiData.oldDistanceFromJEndToU2Spring).Within(0.001));
            Assert.That(distanceFromJEndToU3Spring, Is.EqualTo(CSiData.oldDistanceFromJEndToU3Spring).Within(0.001));

            // U1
            Assert.That(degreesOfFreedom.U1, Is.EqualTo(degreesOfFreedom.U1));
            Assert.That(fixity.U1, Is.EqualTo(CSiData.oldFixity.U1));
            Assert.That(nonLinear.U1, Is.EqualTo(CSiData.oldNonlinear.U1));
            Assert.That(effectiveStiffness.U1, Is.EqualTo(CSiData.oldEffectiveStiffness.U1).Within(0.001));
            Assert.That(effectiveDamping.U1, Is.EqualTo(CSiData.oldEffectiveDamping.U1).Within(0.001));

            // U2
            Assert.That(degreesOfFreedom.U2, Is.EqualTo(degreesOfFreedom.U2));
            Assert.That(fixity.U2, Is.EqualTo(CSiData.oldFixity.U2));
            Assert.That(nonLinear.U2, Is.EqualTo(CSiData.oldNonlinear.U2));
            Assert.That(effectiveStiffness.U2, Is.EqualTo(CSiData.oldEffectiveStiffness.U2).Within(0.001));
            Assert.That(effectiveDamping.U2, Is.EqualTo(CSiData.oldEffectiveDamping.U2).Within(0.001));

            // U3
            Assert.That(degreesOfFreedom.U3, Is.EqualTo(degreesOfFreedom.U3));
            Assert.That(fixity.U3, Is.EqualTo(CSiData.oldFixity.U3));
            Assert.That(nonLinear.U3, Is.EqualTo(CSiData.oldNonlinear.U3));
            Assert.That(effectiveStiffness.U3, Is.EqualTo(CSiData.oldEffectiveStiffness.U3).Within(0.001));
            Assert.That(effectiveDamping.U3, Is.EqualTo(CSiData.oldEffectiveDamping.U3).Within(0.001));

            // R1
            Assert.That(degreesOfFreedom.R1, Is.EqualTo(degreesOfFreedom.R1));
            Assert.That(fixity.R1, Is.EqualTo(CSiData.oldFixity.R1));
            Assert.That(nonLinear.R1, Is.EqualTo(CSiData.oldNonlinear.R1));
            Assert.That(effectiveStiffness.R1, Is.EqualTo(CSiData.oldEffectiveStiffness.R1).Within(0.001));
            Assert.That(effectiveDamping.R1, Is.EqualTo(CSiData.oldEffectiveDamping.R1).Within(0.001));

            // R2
            Assert.That(degreesOfFreedom.R2, Is.EqualTo(degreesOfFreedom.R2));
            Assert.That(fixity.R2, Is.EqualTo(CSiData.oldFixity.R2));
            Assert.That(nonLinear.R2, Is.EqualTo(CSiData.oldNonlinear.R2));
            Assert.That(effectiveStiffness.R2, Is.EqualTo(CSiData.oldEffectiveStiffness.R2).Within(0.001));
            Assert.That(effectiveDamping.R2, Is.EqualTo(CSiData.oldEffectiveDamping.R2).Within(0.001));

            // R3
            Assert.That(degreesOfFreedom.R3, Is.EqualTo(degreesOfFreedom.R3));
            Assert.That(fixity.R3, Is.EqualTo(CSiData.oldFixity.R3));
            Assert.That(nonLinear.R3, Is.EqualTo(CSiData.oldNonlinear.R3));
            Assert.That(effectiveStiffness.R3, Is.EqualTo(CSiData.oldEffectiveStiffness.R3).Within(0.001));
            Assert.That(effectiveDamping.R3, Is.EqualTo(CSiData.oldEffectiveDamping.R3).Within(0.001));

            // Change model state
            string newNotes = oldNotes + CSiData.NewNotes;

            _app.Model.Definitions.Properties.LinkProperties.SetMultiLinearElastic(name,
                CSiData.newDegreesOfFreedom,
                CSiData.newFixitySet,
                CSiData.newNonlinearSet,
                CSiData.newEffectiveStiffness,
                CSiData.newEffectiveDamping,
                CSiData.newDistanceFromJEndToU2Spring,
                CSiData.newDistanceFromJEndToU3Spring,
                newNotes,
                CSiData.NewGUID);

            // Get new model state
            _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearElastic(name,
                out degreesOfFreedom,
                out fixity,
                out nonLinear,
                out effectiveStiffness,
                out effectiveDamping,
                out distanceFromJEndToU2Spring,
                out distanceFromJEndToU3Spring,
                out notes,
                out GUID);

            // Check output
            Assert.That(notes, Is.EqualTo(newNotes));
            Assert.That(GUID, Is.EqualTo(CSiData.NewGUID));
            Assert.That(effectiveStiffness.U1, Is.EqualTo(CSiData.newEffectiveStiffness.U1).Within(0.001));
            Assert.That(effectiveDamping.U1, Is.EqualTo(CSiData.newEffectiveDamping.U1).Within(0.001));

            // U1
            Assert.That(degreesOfFreedom.U1, Is.EqualTo(degreesOfFreedom.U1));
            Assert.That(fixity.U1, Is.EqualTo(CSiData.newFixityGet.U1));
            Assert.That(nonLinear.U1, Is.EqualTo(CSiData.newNonlinearGet.U1));
            Assert.That(effectiveStiffness.U1, Is.EqualTo(CSiData.newEffectiveStiffness.U1).Within(0.001));
            Assert.That(effectiveDamping.U1, Is.EqualTo(CSiData.newEffectiveDamping.U1).Within(0.001));

            // U2
            Assert.That(degreesOfFreedom.U2, Is.EqualTo(degreesOfFreedom.U2));
            Assert.That(fixity.U2, Is.EqualTo(CSiData.newFixityGet.U2));
            Assert.That(nonLinear.U2, Is.EqualTo(CSiData.newNonlinearGet.U2));
            Assert.That(effectiveStiffness.U2, Is.EqualTo(CSiData.newEffectiveStiffness.U2).Within(0.001));
            Assert.That(effectiveDamping.U2, Is.EqualTo(CSiData.newEffectiveDamping.U2).Within(0.001));

            // U3
            Assert.That(degreesOfFreedom.U3, Is.EqualTo(degreesOfFreedom.U3));
            Assert.That(fixity.U3, Is.EqualTo(CSiData.newFixityGet.U3));
            Assert.That(nonLinear.U3, Is.EqualTo(CSiData.newNonlinearGet.U3));
            Assert.That(effectiveStiffness.U3, Is.EqualTo(CSiData.newEffectiveStiffness.U3).Within(0.001));
            Assert.That(effectiveDamping.U3, Is.EqualTo(CSiData.newEffectiveDamping.U3).Within(0.001));

            // R1
            Assert.That(degreesOfFreedom.R1, Is.EqualTo(degreesOfFreedom.R1));
            Assert.That(fixity.R1, Is.EqualTo(CSiData.newFixityGet.R1));
            Assert.That(nonLinear.R1, Is.EqualTo(CSiData.newNonlinearGet.R1));
            Assert.That(effectiveStiffness.R1, Is.EqualTo(CSiData.newEffectiveStiffness.R1).Within(0.001));
            Assert.That(effectiveDamping.R1, Is.EqualTo(CSiData.newEffectiveDamping.R1).Within(0.001));

            // R2
            Assert.That(degreesOfFreedom.R2, Is.EqualTo(degreesOfFreedom.R2));
            Assert.That(fixity.R2, Is.EqualTo(CSiData.newFixityGet.R2));
            Assert.That(nonLinear.R2, Is.EqualTo(CSiData.newNonlinearGet.R2));
            Assert.That(effectiveStiffness.R2, Is.EqualTo(CSiData.newEffectiveStiffness.R2).Within(0.001));
            Assert.That(effectiveDamping.R2, Is.EqualTo(CSiData.newEffectiveDamping.R2).Within(0.001));

            // R3
            Assert.That(degreesOfFreedom.R3, Is.EqualTo(degreesOfFreedom.R3));
            Assert.That(fixity.R3, Is.EqualTo(CSiData.newFixityGet.R3));
            Assert.That(nonLinear.R3, Is.EqualTo(CSiData.newNonlinearGet.R3));
            Assert.That(effectiveStiffness.R3, Is.EqualTo(CSiData.newEffectiveStiffness.R3).Within(0.001));
            Assert.That(effectiveDamping.R3, Is.EqualTo(CSiData.newEffectiveDamping.R3).Within(0.001));
        }

        [Test]
        public void SetMultiLinearPlastic()
        {
            string name = _nameMultiLinearPlastic;
            DegreesOfFreedomLocal degreesOfFreedom;
            DegreesOfFreedomLocal fixity;
            DegreesOfFreedomLocal nonLinear;
            Stiffness effectiveStiffness;
            Stiffness effectiveDamping;
            double distanceFromJEndToU2Spring;
            double distanceFromJEndToU3Spring;
            string notes;
            string GUID;

            // Get current model state
            _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPlastic(name,
                out degreesOfFreedom,
                out fixity,
                out nonLinear,
                out effectiveStiffness,
                out effectiveDamping,
                out distanceFromJEndToU2Spring,
                out distanceFromJEndToU3Spring,
                out notes,
                out GUID);

            // Check output
            string oldNotes = name;
            Assert.That(notes, Is.EqualTo(oldNotes));
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
            Assert.That(!string.IsNullOrEmpty(GUID));
#else
            Assert.That(GUID, Is.EqualTo(CSiData.OldGUID));
#endif
            Assert.That(distanceFromJEndToU2Spring, Is.EqualTo(CSiData.oldDistanceFromJEndToU2Spring).Within(0.001));
            Assert.That(distanceFromJEndToU3Spring, Is.EqualTo(CSiData.oldDistanceFromJEndToU3Spring).Within(0.001));

            // U1
            Assert.That(degreesOfFreedom.U1, Is.EqualTo(degreesOfFreedom.U1));
            Assert.That(fixity.U1, Is.EqualTo(CSiData.oldFixity.U1));
            Assert.That(nonLinear.U1, Is.EqualTo(CSiData.oldNonlinear.U1));
            Assert.That(effectiveStiffness.U1, Is.EqualTo(CSiData.oldEffectiveStiffness.U1).Within(0.001));
            Assert.That(effectiveDamping.U1, Is.EqualTo(CSiData.oldEffectiveDamping.U1).Within(0.001));

            // U2
            Assert.That(degreesOfFreedom.U2, Is.EqualTo(degreesOfFreedom.U2));
            Assert.That(fixity.U2, Is.EqualTo(CSiData.oldFixity.U2));
            Assert.That(nonLinear.U2, Is.EqualTo(CSiData.oldNonlinear.U2));
            Assert.That(effectiveStiffness.U2, Is.EqualTo(CSiData.oldEffectiveStiffness.U2).Within(0.001));
            Assert.That(effectiveDamping.U2, Is.EqualTo(CSiData.oldEffectiveDamping.U2).Within(0.001));

            // U3
            Assert.That(degreesOfFreedom.U3, Is.EqualTo(degreesOfFreedom.U3));
            Assert.That(fixity.U3, Is.EqualTo(CSiData.oldFixity.U3));
            Assert.That(nonLinear.U3, Is.EqualTo(CSiData.oldNonlinear.U3));
            Assert.That(effectiveStiffness.U3, Is.EqualTo(CSiData.oldEffectiveStiffness.U3).Within(0.001));
            Assert.That(effectiveDamping.U3, Is.EqualTo(CSiData.oldEffectiveDamping.U3).Within(0.001));

            // R1
            Assert.That(degreesOfFreedom.R1, Is.EqualTo(degreesOfFreedom.R1));
            Assert.That(fixity.R1, Is.EqualTo(CSiData.oldFixity.R1));
            Assert.That(nonLinear.R1, Is.EqualTo(CSiData.oldNonlinear.R1));
            Assert.That(effectiveStiffness.R1, Is.EqualTo(CSiData.oldEffectiveStiffness.R1).Within(0.001));
            Assert.That(effectiveDamping.R1, Is.EqualTo(CSiData.oldEffectiveDamping.R1).Within(0.001));

            // R2
            Assert.That(degreesOfFreedom.R2, Is.EqualTo(degreesOfFreedom.R2));
            Assert.That(fixity.R2, Is.EqualTo(CSiData.oldFixity.R2));
            Assert.That(nonLinear.R2, Is.EqualTo(CSiData.oldNonlinear.R2));
            Assert.That(effectiveStiffness.R2, Is.EqualTo(CSiData.oldEffectiveStiffness.R2).Within(0.001));
            Assert.That(effectiveDamping.R2, Is.EqualTo(CSiData.oldEffectiveDamping.R2).Within(0.001));

            // R3
            Assert.That(degreesOfFreedom.R3, Is.EqualTo(degreesOfFreedom.R3));
            Assert.That(fixity.R3, Is.EqualTo(CSiData.oldFixity.R3));
            Assert.That(nonLinear.R3, Is.EqualTo(CSiData.oldNonlinear.R3));
            Assert.That(effectiveStiffness.R3, Is.EqualTo(CSiData.oldEffectiveStiffness.R3).Within(0.001));
            Assert.That(effectiveDamping.R3, Is.EqualTo(CSiData.oldEffectiveDamping.R3).Within(0.001));

            // Change model state
            string newNotes = oldNotes + CSiData.NewNotes;

            _app.Model.Definitions.Properties.LinkProperties.SetMultiLinearPlastic(name,
                CSiData.newDegreesOfFreedom,
                CSiData.newFixitySet,
                CSiData.newNonlinearSet,
                CSiData.newEffectiveStiffness,
                CSiData.newEffectiveDamping,
                CSiData.newDistanceFromJEndToU2Spring,
                CSiData.newDistanceFromJEndToU3Spring,
                newNotes,
                CSiData.NewGUID);

            // Get new model state
            _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPlastic(name,
                out degreesOfFreedom,
                out fixity,
                out nonLinear,
                out effectiveStiffness,
                out effectiveDamping,
                out distanceFromJEndToU2Spring,
                out distanceFromJEndToU3Spring,
                out notes,
                out GUID);

            // Check output
            Assert.That(notes, Is.EqualTo(newNotes));
            Assert.That(GUID, Is.EqualTo(CSiData.NewGUID));
            Assert.That(effectiveStiffness.U1, Is.EqualTo(CSiData.newEffectiveStiffness.U1).Within(0.001));
            Assert.That(effectiveDamping.U1, Is.EqualTo(CSiData.newEffectiveDamping.U1).Within(0.001));

            // U1
            Assert.That(degreesOfFreedom.U1, Is.EqualTo(degreesOfFreedom.U1));
            Assert.That(fixity.U1, Is.EqualTo(CSiData.newFixityGet.U1));
            Assert.That(nonLinear.U1, Is.EqualTo(CSiData.newNonlinearGet.U1));
            Assert.That(effectiveStiffness.U1, Is.EqualTo(CSiData.newEffectiveStiffness.U1).Within(0.001));
            Assert.That(effectiveDamping.U1, Is.EqualTo(CSiData.newEffectiveDamping.U1).Within(0.001));

            // U2
            Assert.That(degreesOfFreedom.U2, Is.EqualTo(degreesOfFreedom.U2));
            Assert.That(fixity.U2, Is.EqualTo(CSiData.newFixityGet.U2));
            Assert.That(nonLinear.U2, Is.EqualTo(CSiData.newNonlinearGet.U2));
            Assert.That(effectiveStiffness.U2, Is.EqualTo(CSiData.newEffectiveStiffness.U2).Within(0.001));
            Assert.That(effectiveDamping.U2, Is.EqualTo(CSiData.newEffectiveDamping.U2).Within(0.001));

            // U3
            Assert.That(degreesOfFreedom.U3, Is.EqualTo(degreesOfFreedom.U3));
            Assert.That(fixity.U3, Is.EqualTo(CSiData.newFixityGet.U3));
            Assert.That(nonLinear.U3, Is.EqualTo(CSiData.newNonlinearGet.U3));
            Assert.That(effectiveStiffness.U3, Is.EqualTo(CSiData.newEffectiveStiffness.U3).Within(0.001));
            Assert.That(effectiveDamping.U3, Is.EqualTo(CSiData.newEffectiveDamping.U3).Within(0.001));

            // R1
            Assert.That(degreesOfFreedom.R1, Is.EqualTo(degreesOfFreedom.R1));
            Assert.That(fixity.R1, Is.EqualTo(CSiData.newFixityGet.R1));
            Assert.That(nonLinear.R1, Is.EqualTo(CSiData.newNonlinearGet.R1));
            Assert.That(effectiveStiffness.R1, Is.EqualTo(CSiData.newEffectiveStiffness.R1).Within(0.001));
            Assert.That(effectiveDamping.R1, Is.EqualTo(CSiData.newEffectiveDamping.R1).Within(0.001));

            // R2
            Assert.That(degreesOfFreedom.R2, Is.EqualTo(degreesOfFreedom.R2));
            Assert.That(fixity.R2, Is.EqualTo(CSiData.newFixityGet.R2));
            Assert.That(nonLinear.R2, Is.EqualTo(CSiData.newNonlinearGet.R2));
            Assert.That(effectiveStiffness.R2, Is.EqualTo(CSiData.newEffectiveStiffness.R2).Within(0.001));
            Assert.That(effectiveDamping.R2, Is.EqualTo(CSiData.newEffectiveDamping.R2).Within(0.001));

            // R3
            Assert.That(degreesOfFreedom.R3, Is.EqualTo(degreesOfFreedom.R3));
            Assert.That(fixity.R3, Is.EqualTo(CSiData.newFixityGet.R3));
            Assert.That(nonLinear.R3, Is.EqualTo(CSiData.newNonlinearGet.R3));
            Assert.That(effectiveStiffness.R3, Is.EqualTo(CSiData.newEffectiveStiffness.R3).Within(0.001));
            Assert.That(effectiveDamping.R3, Is.EqualTo(CSiData.newEffectiveDamping.R3).Within(0.001));
        }

        [Test]
        public void SetMultiLinearPoints_Elastic()
        {
            string name = _nameMultiLinearElastic;

            double[] forces;
            double[] displacements;
            eLinkHysteresisType linkHysteresisType;
            double alpha1;
            double alpha2;
            double beta1;
            double beta2;
            double eta;

            eDegreeOfFreedom degreeOfFreedom = eDegreeOfFreedom.U2;

            // Get current model state
            _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPoints(name,
                degreeOfFreedom,
                out forces,
                out displacements,
                out linkHysteresisType,
                out alpha1,
                out alpha2,
                out beta1,
                out beta2,
                out eta);
            
            // Check output
            Assert.That(linkHysteresisType, Is.EqualTo(eLinkHysteresisType.NA));
            Assert.That(alpha1, Is.EqualTo(0));
            Assert.That(alpha2, Is.EqualTo(0));
            Assert.That(beta1, Is.EqualTo(0));
            Assert.That(beta2, Is.EqualTo(0));
            Assert.That(eta, Is.EqualTo(0));

            Assert.That(forces[0], Is.EqualTo(CSiData.oldForces[0]));
            Assert.That(forces[1], Is.EqualTo(CSiData.oldForces[1]));
            Assert.That(forces[2], Is.EqualTo(CSiData.oldForces[2]));
            Assert.That(forces[3], Is.EqualTo(CSiData.oldForces[3]));
            Assert.That(forces[4], Is.EqualTo(CSiData.oldForces[4]));

            Assert.That(displacements[0], Is.EqualTo(CSiData.oldDisplacements[0]));
            Assert.That(displacements[1], Is.EqualTo(CSiData.oldDisplacements[1]));
            Assert.That(displacements[2], Is.EqualTo(CSiData.oldDisplacements[2]));
            Assert.That(displacements[3], Is.EqualTo(CSiData.oldDisplacements[3]));
            Assert.That(displacements[4], Is.EqualTo(CSiData.oldDisplacements[4]));

            // Change model state
            _app.Model.Definitions.Properties.LinkProperties.SetMultiLinearPoints(name,
                degreeOfFreedom,
                CSiData.newForces,
                CSiData.newDisplacements,
                eLinkHysteresisType.Kinematic,
                CSiData.newPivotAlpha1,
                CSiData.newPivotAlpha2,
                CSiData.newPivotBeta1,
                CSiData.newPivotBeta2,
                CSiData.newPivotEta);

            // Get new model state
            _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPoints(name,
                degreeOfFreedom,
                out forces,
                out displacements,
                out linkHysteresisType,
                out alpha1,
                out alpha2,
                out beta1,
                out beta2,
                out eta);

            // Check output
            Assert.That(linkHysteresisType, Is.EqualTo(eLinkHysteresisType.NA));
            Assert.That(alpha1, Is.EqualTo(0));
            Assert.That(alpha2, Is.EqualTo(0));
            Assert.That(beta1, Is.EqualTo(0));
            Assert.That(beta2, Is.EqualTo(0));
            Assert.That(eta, Is.EqualTo(0));

            Assert.That(forces[0], Is.EqualTo(CSiData.newForces[0]));
            Assert.That(forces[1], Is.EqualTo(CSiData.newForces[1]));
            Assert.That(forces[2], Is.EqualTo(CSiData.newForces[2]));
            Assert.That(forces[3], Is.EqualTo(CSiData.newForces[3]));
            Assert.That(forces[4], Is.EqualTo(CSiData.newForces[4]));

            Assert.That(displacements[0], Is.EqualTo(CSiData.newDisplacements[0]));
            Assert.That(displacements[1], Is.EqualTo(CSiData.newDisplacements[1]));
            Assert.That(displacements[2], Is.EqualTo(CSiData.newDisplacements[2]));
            Assert.That(displacements[3], Is.EqualTo(CSiData.newDisplacements[3]));
            Assert.That(displacements[4], Is.EqualTo(CSiData.newDisplacements[4]));
        }

        [Test]
        public void SetMultiLinearPoints_Plastic()
        {
            string name = _nameMultiLinearPlastic;

            double[] forces;
            double[] displacements;
            eLinkHysteresisType linkHysteresisType;
            double alpha1;
            double alpha2;
            double beta1;
            double beta2;
            double eta;

            eDegreeOfFreedom degreeOfFreedom = eDegreeOfFreedom.U3;

            // Get current model state
            _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPoints(name,
                degreeOfFreedom,
                out forces,
                out displacements,
                out linkHysteresisType,
                out alpha1,
                out alpha2,
                out beta1,
                out beta2,
                out eta);

            // Check output
            Assert.That(linkHysteresisType, Is.EqualTo(eLinkHysteresisType.Pivot));
            Assert.That(alpha1, Is.EqualTo(CSiData.oldPivotAlpha1));
            Assert.That(alpha2, Is.EqualTo(CSiData.oldPivotAlpha2));
            Assert.That(beta1, Is.EqualTo(CSiData.oldPivotBeta1));
            Assert.That(beta2, Is.EqualTo(CSiData.oldPivotBeta2));
            Assert.That(eta, Is.EqualTo(CSiData.oldPivotEta));

            Assert.That(forces[0], Is.EqualTo(CSiData.oldForces[0]));
            Assert.That(forces[1], Is.EqualTo(CSiData.oldForces[1]));
            Assert.That(forces[2], Is.EqualTo(CSiData.oldForces[2]));
            Assert.That(forces[3], Is.EqualTo(CSiData.oldForces[3]));
            Assert.That(forces[4], Is.EqualTo(CSiData.oldForces[4]));

            Assert.That(displacements[0], Is.EqualTo(CSiData.oldDisplacements[0]));
            Assert.That(displacements[1], Is.EqualTo(CSiData.oldDisplacements[1]));
            Assert.That(displacements[2], Is.EqualTo(CSiData.oldDisplacements[2]));
            Assert.That(displacements[3], Is.EqualTo(CSiData.oldDisplacements[3]));
            Assert.That(displacements[4], Is.EqualTo(CSiData.oldDisplacements[4]));

            // Change model state
            _app.Model.Definitions.Properties.LinkProperties.SetMultiLinearPoints(name,
                degreeOfFreedom,
                CSiData.newForces,
                CSiData.newDisplacements,
                eLinkHysteresisType.Pivot,
                CSiData.newPivotAlpha1,
                CSiData.newPivotAlpha2,
                CSiData.newPivotBeta1,
                CSiData.newPivotBeta2,
                CSiData.newPivotEta);

            // Get new model state
            _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPoints(name,
                degreeOfFreedom,
                out forces,
                out displacements,
                out linkHysteresisType,
                out alpha1,
                out alpha2,
                out beta1,
                out beta2,
                out eta);

            // Check output
            Assert.That(linkHysteresisType, Is.EqualTo(eLinkHysteresisType.Pivot));
            Assert.That(alpha1, Is.EqualTo(CSiData.newPivotAlpha1));
            Assert.That(alpha2, Is.EqualTo(CSiData.newPivotAlpha2));
            Assert.That(beta1, Is.EqualTo(CSiData.newPivotBeta1));
            Assert.That(beta2, Is.EqualTo(CSiData.newPivotBeta2));
            Assert.That(eta, Is.EqualTo(CSiData.newPivotEta));

            Assert.That(forces[0], Is.EqualTo(CSiData.newForces[0]));
            Assert.That(forces[1], Is.EqualTo(CSiData.newForces[1]));
            Assert.That(forces[2], Is.EqualTo(CSiData.newForces[2]));
            Assert.That(forces[3], Is.EqualTo(CSiData.newForces[3]));
            Assert.That(forces[4], Is.EqualTo(CSiData.newForces[4]));

            Assert.That(displacements[0], Is.EqualTo(CSiData.newDisplacements[0]));
            Assert.That(displacements[1], Is.EqualTo(CSiData.newDisplacements[1]));
            Assert.That(displacements[2], Is.EqualTo(CSiData.newDisplacements[2]));
            Assert.That(displacements[3], Is.EqualTo(CSiData.newDisplacements[3]));
            Assert.That(displacements[4], Is.EqualTo(CSiData.newDisplacements[4]));
        }

        [Test]
        public void SetMultiLinearPoints_Elastic_ToPivot_AndFactors()
        {
            string name = _nameMultiLinearElastic;

            double[] forces;
            double[] displacements;
            eLinkHysteresisType linkHysteresisType;
            double alpha1;
            double alpha2;
            double beta1;
            double beta2;
            double eta;

            eDegreeOfFreedom degreeOfFreedom = eDegreeOfFreedom.U2;

            // Get current model state
            _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPoints(name,
                degreeOfFreedom,
                out forces,
                out displacements,
                out linkHysteresisType,
                out alpha1,
                out alpha2,
                out beta1,
                out beta2,
                out eta);

            // Check output
            Assert.That(linkHysteresisType, Is.EqualTo(eLinkHysteresisType.NA));
            Assert.That(alpha1, Is.EqualTo(0));
            Assert.That(alpha2, Is.EqualTo(0));
            Assert.That(beta1, Is.EqualTo(0));
            Assert.That(beta2, Is.EqualTo(0));
            Assert.That(eta, Is.EqualTo(0));
            
            // Change model state
            _app.Model.Definitions.Properties.LinkProperties.SetMultiLinearPoints(name,
                degreeOfFreedom,
                CSiData.newForces,
                CSiData.newDisplacements,
                eLinkHysteresisType.Pivot,
                CSiData.newPivotAlpha1,
                CSiData.newPivotAlpha2,
                CSiData.newPivotBeta1,
                CSiData.newPivotBeta2,
                CSiData.newPivotEta);

            // Get new model state
            _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPoints(name,
                degreeOfFreedom,
                out forces,
                out displacements,
                out linkHysteresisType,
                out alpha1,
                out alpha2,
                out beta1,
                out beta2,
                out eta);

            // Check output
            Assert.That(linkHysteresisType, Is.EqualTo(eLinkHysteresisType.NA));
            Assert.That(alpha1, Is.EqualTo(0));
            Assert.That(alpha2, Is.EqualTo(0));
            Assert.That(beta1, Is.EqualTo(0));
            Assert.That(beta2, Is.EqualTo(0));
            Assert.That(eta, Is.EqualTo(0));
        }

        [Test]
        public void SetMultiLinearPoints_Plastic_ToPivot_AndFactors()
        {
            string name = _nameMultiLinearPlastic;

            double[] forces;
            double[] displacements;
            eLinkHysteresisType linkHysteresisType;
            double alpha1;
            double alpha2;
            double beta1;
            double beta2;
            double eta;

            eDegreeOfFreedom degreeOfFreedom = eDegreeOfFreedom.U2;

            // Get current model state
            _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPoints(name,
                degreeOfFreedom,
                out forces,
                out displacements,
                out linkHysteresisType,
                out alpha1,
                out alpha2,
                out beta1,
                out beta2,
                out eta);

            // Check output
            Assert.That(linkHysteresisType, Is.EqualTo(eLinkHysteresisType.Kinematic));
            Assert.That(alpha1, Is.EqualTo(0));
            Assert.That(alpha2, Is.EqualTo(0));
            Assert.That(beta1, Is.EqualTo(0));
            Assert.That(beta2, Is.EqualTo(0));
            Assert.That(eta, Is.EqualTo(0));
            
            // Change model state
            _app.Model.Definitions.Properties.LinkProperties.SetMultiLinearPoints(name,
                degreeOfFreedom,
                CSiData.newForces,
                CSiData.newDisplacements,
                eLinkHysteresisType.Pivot,
                CSiData.newPivotAlpha1,
                CSiData.newPivotAlpha2,
                CSiData.newPivotBeta1,
                CSiData.newPivotBeta2,
                CSiData.newPivotEta);

            // Get new model state
            _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPoints(name,
                degreeOfFreedom,
                out forces,
                out displacements,
                out linkHysteresisType,
                out alpha1,
                out alpha2,
                out beta1,
                out beta2,
                out eta);

            // Check output
            Assert.That(linkHysteresisType, Is.EqualTo(eLinkHysteresisType.Pivot));
            Assert.That(alpha1, Is.EqualTo(CSiData.newPivotAlpha1));
            Assert.That(alpha2, Is.EqualTo(CSiData.newPivotAlpha2));
            Assert.That(beta1, Is.EqualTo(CSiData.newPivotBeta1));
            Assert.That(beta2, Is.EqualTo(CSiData.newPivotBeta2));
            Assert.That(eta, Is.EqualTo(CSiData.newPivotEta));
        }

        [Test]
        public void SetMultiLinearPoints_Elastic_FromPivot()
        {
            string name = _nameMultiLinearElastic;

            double[] forces;
            double[] displacements;
            eLinkHysteresisType linkHysteresisType;
            double alpha1;
            double alpha2;
            double beta1;
            double beta2;
            double eta;

            eDegreeOfFreedom degreeOfFreedom = eDegreeOfFreedom.U3;

            // Get current model state
            _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPoints(name,
                degreeOfFreedom,
                out forces,
                out displacements,
                out linkHysteresisType,
                out alpha1,
                out alpha2,
                out beta1,
                out beta2,
                out eta);

            // Check output
            Assert.That(linkHysteresisType, Is.EqualTo(eLinkHysteresisType.NA));
            Assert.That(alpha1, Is.EqualTo(0));
            Assert.That(alpha2, Is.EqualTo(0));
            Assert.That(beta1, Is.EqualTo(0));
            Assert.That(beta2, Is.EqualTo(0));
            Assert.That(eta, Is.EqualTo(0));

            // Change model state
            _app.Model.Definitions.Properties.LinkProperties.SetMultiLinearPoints(name,
                degreeOfFreedom,
                CSiData.newForces,
                CSiData.newDisplacements,
                eLinkHysteresisType.Kinematic);

            // Get new model state
            _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPoints(name,
                degreeOfFreedom,
                out forces,
                out displacements,
                out linkHysteresisType,
                out alpha1,
                out alpha2,
                out beta1,
                out beta2,
                out eta);

            // Check output
            Assert.That(linkHysteresisType, Is.EqualTo(eLinkHysteresisType.NA));
            Assert.That(alpha1, Is.EqualTo(0));
            Assert.That(alpha2, Is.EqualTo(0));
            Assert.That(beta1, Is.EqualTo(0));
            Assert.That(beta2, Is.EqualTo(0));
            Assert.That(eta, Is.EqualTo(0));
        }

        [Test]
        public void SetMultiLinearPoints_Plastic_FromPivot()
        {
            string name = _nameMultiLinearPlastic;

            double[] forces;
            double[] displacements;
            eLinkHysteresisType linkHysteresisType;
            double alpha1;
            double alpha2;
            double beta1;
            double beta2;
            double eta;

            eDegreeOfFreedom degreeOfFreedom = eDegreeOfFreedom.U3;

            // Get current model state
            _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPoints(name,
                degreeOfFreedom,
                out forces,
                out displacements,
                out linkHysteresisType,
                out alpha1,
                out alpha2,
                out beta1,
                out beta2,
                out eta);

            // Check output
            Assert.That(linkHysteresisType, Is.EqualTo(eLinkHysteresisType.Pivot));
            Assert.That(alpha1, Is.EqualTo(CSiData.oldPivotAlpha1));
            Assert.That(alpha2, Is.EqualTo(CSiData.oldPivotAlpha2));
            Assert.That(beta1, Is.EqualTo(CSiData.oldPivotBeta1));
            Assert.That(beta2, Is.EqualTo(CSiData.oldPivotBeta2));
            Assert.That(eta, Is.EqualTo(CSiData.oldPivotEta));

            // Change model state
            _app.Model.Definitions.Properties.LinkProperties.SetMultiLinearPoints(name,
                degreeOfFreedom,
                CSiData.newForces,
                CSiData.newDisplacements,
                eLinkHysteresisType.Takeda);

            // Get new model state
            _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPoints(name,
                degreeOfFreedom,
                out forces,
                out displacements,
                out linkHysteresisType,
                out alpha1,
                out alpha2,
                out beta1,
                out beta2,
                out eta);

            // Check output
            Assert.That(linkHysteresisType, Is.EqualTo(eLinkHysteresisType.Takeda));
            Assert.That(alpha1, Is.EqualTo(0));
            Assert.That(alpha2, Is.EqualTo(0));
            Assert.That(beta1, Is.EqualTo(0));
            Assert.That(beta2, Is.EqualTo(0));
            Assert.That(eta, Is.EqualTo(0));
        }

        [TestCase(_nameMultiLinearElastic)]
        [TestCase(_nameMultiLinearPlastic)]
        public void SetMultiLinearPoints_Insufficient_Points_Throws_CSiException(string name)
        {
            eDegreeOfFreedom degreeOfFreedom = eDegreeOfFreedom.U2;

            // Change model state
            double[] insufficientForces = {0, 1};
            double[] insufficientDisplacements = {0, 1};
            Assert.That(() =>
            {
                _app.Model.Definitions.Properties.LinkProperties.SetMultiLinearPoints(name,
                degreeOfFreedom,
                insufficientForces,
                insufficientDisplacements);
            },
            Throws.Exception.TypeOf<CSiException>());
        }

        [TestCase(_nameMultiLinearElastic)]
        [TestCase(_nameMultiLinearPlastic)]
        public void SetMultiLinearPoints_Displacements_Not_Monotonic_Throws_CSiException(string name)
        {
            eDegreeOfFreedom degreeOfFreedom = eDegreeOfFreedom.U2;
            
            // Change model state
            double[] nonMonotonicDisplacements = { -1, -2, 0, 1 };
            double[] correspondingForces = { -1, -1, 0, 1 };

            Assert.That(() =>
            {
                _app.Model.Definitions.Properties.LinkProperties.SetMultiLinearPoints(name,
                degreeOfFreedom,
                correspondingForces,
                nonMonotonicDisplacements);
            },
            Throws.Exception.TypeOf<CSiException>());
        }

        [TestCase(_nameMultiLinearElastic)]
        [TestCase(_nameMultiLinearPlastic)]
        public void SetMultiLinearPoints_No_Negative_Displacements_Throws_CSiException(string name)
        {
            eDegreeOfFreedom degreeOfFreedom = eDegreeOfFreedom.U2;

            // Change model state
            double[] allPositiveDisplacements = { 0, 1, 10 };
            double[] correspondingForces = { 0, 1, 1 };

            Assert.That(() =>
            {
                _app.Model.Definitions.Properties.LinkProperties.SetMultiLinearPoints(name,
                degreeOfFreedom,
                correspondingForces,
                allPositiveDisplacements);
            },
            Throws.Exception.TypeOf<CSiException>());
        }

        [TestCase(_nameMultiLinearElastic)]
        [TestCase(_nameMultiLinearPlastic)]
        public void SetMultiLinearPoints_No_Positive_Displacements_Throws_CSiException(string name)
        {
            eDegreeOfFreedom degreeOfFreedom = eDegreeOfFreedom.U2;

            // Change model state
            double[] allNegativeDisplacements = { -10, -1,  0 };
            double[] correspondingForces = { -1, -1, 0 };

            Assert.That(() =>
            {
                _app.Model.Definitions.Properties.LinkProperties.SetMultiLinearPoints(name,
                degreeOfFreedom,
                correspondingForces,
                allNegativeDisplacements);
            },
            Throws.Exception.TypeOf<CSiException>());
        }

        [TestCase(_nameMultiLinearElastic)]
        [TestCase(_nameMultiLinearPlastic)]
        public void SetMultiLinearPoints_No_Zero_Displacement_Throws_CSiException(string name)
        {
            eDegreeOfFreedom degreeOfFreedom = eDegreeOfFreedom.U2;

            // Change model state
            double[] noZeroDisplacements = { -10, -1, 1, 2, 10 };
            double[] correspondingForces = { -1, -1, 0, 1, 1 };

            Assert.That(() =>
            {
                _app.Model.Definitions.Properties.LinkProperties.SetMultiLinearPoints(name,
                degreeOfFreedom,
                correspondingForces,
                noZeroDisplacements);
            },
            Throws.Exception.TypeOf<CSiException>());
        }

        [TestCase(_nameMultiLinearElastic)]
        [TestCase(_nameMultiLinearPlastic)]
        public void SetMultiLinearPoints_No_Origin_Displacement_Throws_CSiException(string name)
        {
            eDegreeOfFreedom degreeOfFreedom = eDegreeOfFreedom.U2;
            
            // Change model state
            double[] noZeroForces = {-1, -1, 1, 1};
            double[] correspondingDisplacements = {-10, -1, 0, 10};
            
            Assert.That(() =>
            {
                _app.Model.Definitions.Properties.LinkProperties.SetMultiLinearPoints(name,
                degreeOfFreedom,
                noZeroForces,
                correspondingDisplacements);
            },
            Throws.Exception.TypeOf<CSiException>());
        }

        [TestCase(_nameMultiLinearElastic)]
        [TestCase(_nameMultiLinearPlastic)]
        public void SetMultiLinearPoints_All_Positive_Forces(string name) // Verification Incident 15090, 15091
        {
            double[] forces;
            double[] displacements;
            eLinkHysteresisType linkHysteresisType;
            double alpha1;
            double alpha2;
            double beta1;
            double beta2;
            double eta;

            eDegreeOfFreedom degreeOfFreedom = eDegreeOfFreedom.U2;

            // Get current model state
            _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPoints(name,
                degreeOfFreedom,
                out forces,
                out displacements,
                out linkHysteresisType,
                out alpha1,
                out alpha2,
                out beta1,
                out beta2,
                out eta);

            // Check output
            Assert.That(forces[0], Is.EqualTo(CSiData.oldForces[0]));
            Assert.That(forces[1], Is.EqualTo(CSiData.oldForces[1]));
            Assert.That(forces[2], Is.EqualTo(CSiData.oldForces[2]));
            Assert.That(forces[3], Is.EqualTo(CSiData.oldForces[3]));
            Assert.That(forces[4], Is.EqualTo(CSiData.oldForces[4]));

            Assert.That(displacements[0], Is.EqualTo(CSiData.oldDisplacements[0]));
            Assert.That(displacements[1], Is.EqualTo(CSiData.oldDisplacements[1]));
            Assert.That(displacements[2], Is.EqualTo(CSiData.oldDisplacements[2]));
            Assert.That(displacements[3], Is.EqualTo(CSiData.oldDisplacements[3]));
            Assert.That(displacements[4], Is.EqualTo(CSiData.oldDisplacements[4]));

            // Change model state
            double[] allPositiveForces = { 1, 1, 0, 1, 1 };
            double[] correspondingDisplacements = {-2, -1, 0, 1, 2};

            _app.Model.Definitions.Properties.LinkProperties.SetMultiLinearPoints(name,
                degreeOfFreedom,
                allPositiveForces,
                correspondingDisplacements);

            // Get new model state
            _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPoints(name,
                degreeOfFreedom,
                out forces,
                out displacements,
                out linkHysteresisType,
                out alpha1,
                out alpha2,
                out beta1,
                out beta2,
                out eta);

            // Check output
            Assert.That(forces[0], Is.EqualTo(allPositiveForces[0]));
            Assert.That(forces[1], Is.EqualTo(allPositiveForces[1]));
            Assert.That(forces[2], Is.EqualTo(allPositiveForces[2]));
            Assert.That(forces[3], Is.EqualTo(allPositiveForces[3]));
            Assert.That(forces[4], Is.EqualTo(allPositiveForces[4]));

            Assert.That(displacements[0], Is.EqualTo(correspondingDisplacements[0]));
            Assert.That(displacements[1], Is.EqualTo(correspondingDisplacements[1]));
            Assert.That(displacements[2], Is.EqualTo(correspondingDisplacements[2]));
            Assert.That(displacements[3], Is.EqualTo(correspondingDisplacements[3]));
            Assert.That(displacements[4], Is.EqualTo(correspondingDisplacements[4]));
        }

        [TestCase(_nameMultiLinearElastic)]
        [TestCase(_nameMultiLinearPlastic)]
        public void SetMultiLinearPoints_All_Negative_Forces(string name) // Verification Incident 15090, 15091
        {
            double[] forces;
            double[] displacements;
            eLinkHysteresisType linkHysteresisType;
            double alpha1;
            double alpha2;
            double beta1;
            double beta2;
            double eta;

            eDegreeOfFreedom degreeOfFreedom = eDegreeOfFreedom.U2;

            // Get current model state
            _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPoints(name,
                degreeOfFreedom,
                out forces,
                out displacements,
                out linkHysteresisType,
                out alpha1,
                out alpha2,
                out beta1,
                out beta2,
                out eta);

            // Check output
            Assert.That(forces[0], Is.EqualTo(CSiData.oldForces[0]));
            Assert.That(forces[1], Is.EqualTo(CSiData.oldForces[1]));
            Assert.That(forces[2], Is.EqualTo(CSiData.oldForces[2]));
            Assert.That(forces[3], Is.EqualTo(CSiData.oldForces[3]));
            Assert.That(forces[4], Is.EqualTo(CSiData.oldForces[4]));

            Assert.That(displacements[0], Is.EqualTo(CSiData.oldDisplacements[0]));
            Assert.That(displacements[1], Is.EqualTo(CSiData.oldDisplacements[1]));
            Assert.That(displacements[2], Is.EqualTo(CSiData.oldDisplacements[2]));
            Assert.That(displacements[3], Is.EqualTo(CSiData.oldDisplacements[3]));
            Assert.That(displacements[4], Is.EqualTo(CSiData.oldDisplacements[4]));

            // Change model state
            double[] allNegativeForces = { -1, -1, 0, -1, -1 };
            double[] correspondingDisplacements = { -2, -1, 0, 1, 2 };

            _app.Model.Definitions.Properties.LinkProperties.SetMultiLinearPoints(name,
                    degreeOfFreedom,
                    allNegativeForces,
                    correspondingDisplacements);

            // Get new model state
            _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPoints(name,
                degreeOfFreedom,
                out forces,
                out displacements,
                out linkHysteresisType,
                out alpha1,
                out alpha2,
                out beta1,
                out beta2,
                out eta);

            // Check output
            Assert.That(forces[0], Is.EqualTo(allNegativeForces[0]));
            Assert.That(forces[1], Is.EqualTo(allNegativeForces[1]));
            Assert.That(forces[2], Is.EqualTo(allNegativeForces[2]));
            Assert.That(forces[3], Is.EqualTo(allNegativeForces[3]));
            Assert.That(forces[4], Is.EqualTo(allNegativeForces[4]));

            Assert.That(displacements[0], Is.EqualTo(correspondingDisplacements[0]));
            Assert.That(displacements[1], Is.EqualTo(correspondingDisplacements[1]));
            Assert.That(displacements[2], Is.EqualTo(correspondingDisplacements[2]));
            Assert.That(displacements[3], Is.EqualTo(correspondingDisplacements[3]));
            Assert.That(displacements[4], Is.EqualTo(correspondingDisplacements[4]));
        }

        [TestCase(_nameMultiLinearElastic)]
        [TestCase(_nameMultiLinearPlastic)]
        public void SetMultiLinearPoints_Duplicate_Zero_Forces(string name) // Verification Incident 15090, 15091
        {
            double[] forces;
            double[] displacements;
            eLinkHysteresisType linkHysteresisType;
            double alpha1;
            double alpha2;
            double beta1;
            double beta2;
            double eta;

            eDegreeOfFreedom degreeOfFreedom = eDegreeOfFreedom.U2;

            // Get current model state
            _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPoints(name,
                degreeOfFreedom,
                out forces,
                out displacements,
                out linkHysteresisType,
                out alpha1,
                out alpha2,
                out beta1,
                out beta2,
                out eta);

            // Check output
            Assert.That(forces[0], Is.EqualTo(CSiData.oldForces[0]));
            Assert.That(forces[1], Is.EqualTo(CSiData.oldForces[1]));
            Assert.That(forces[2], Is.EqualTo(CSiData.oldForces[2]));
            Assert.That(forces[3], Is.EqualTo(CSiData.oldForces[3]));
            Assert.That(forces[4], Is.EqualTo(CSiData.oldForces[4]));

            Assert.That(displacements[0], Is.EqualTo(CSiData.oldDisplacements[0]));
            Assert.That(displacements[1], Is.EqualTo(CSiData.oldDisplacements[1]));
            Assert.That(displacements[2], Is.EqualTo(CSiData.oldDisplacements[2]));
            Assert.That(displacements[3], Is.EqualTo(CSiData.oldDisplacements[3]));
            Assert.That(displacements[4], Is.EqualTo(CSiData.oldDisplacements[4]));

            // Change model state
            double[] duplicateZeroForces = { 0, -1, 0, 0, 1, 0 };
            double[] correspondingDisplacements = { -11, -3, 0, 4, 9 , 12};

            _app.Model.Definitions.Properties.LinkProperties.SetMultiLinearPoints(name,
                degreeOfFreedom,
                duplicateZeroForces,
                correspondingDisplacements);

            // Get new model state
            _app.Model.Definitions.Properties.LinkProperties.GetMultiLinearPoints(name,
                degreeOfFreedom,
                out forces,
                out displacements,
                out linkHysteresisType,
                out alpha1,
                out alpha2,
                out beta1,
                out beta2,
                out eta);

            // Check output
            Assert.That(forces[0], Is.EqualTo(duplicateZeroForces[0]));
            Assert.That(forces[1], Is.EqualTo(duplicateZeroForces[1]));
            Assert.That(forces[2], Is.EqualTo(duplicateZeroForces[2]));
            Assert.That(forces[3], Is.EqualTo(duplicateZeroForces[3]));
            Assert.That(forces[4], Is.EqualTo(duplicateZeroForces[4]));
            Assert.That(forces[5], Is.EqualTo(duplicateZeroForces[5]));

            Assert.That(displacements[0], Is.EqualTo(correspondingDisplacements[0]));
            Assert.That(displacements[1], Is.EqualTo(correspondingDisplacements[1]));
            Assert.That(displacements[2], Is.EqualTo(correspondingDisplacements[2]));
            Assert.That(displacements[3], Is.EqualTo(correspondingDisplacements[3]));
            Assert.That(displacements[4], Is.EqualTo(correspondingDisplacements[4]));
            Assert.That(displacements[5], Is.EqualTo(correspondingDisplacements[5]));
        }

    
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
            
        }



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
          
        }
        
        
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
          
        }

    }

}

