using System.Collections.Generic;
using NUnit.Framework;

namespace MPT.GIS.Tests
{
    [TestFixture]
    public class RegionTests
    {
        public const string NAME_REGION = "FooRegion";
        public const string NAME_FORMATION = "FooFormation";
        public const string NAME_SUBFORMATION = "BarFormation";

        /// <summary>
        /// A 5-point star that spans from pole-to-pole.
        /// </summary>
        public static List<Coordinate> StarRegion = new List<Coordinate>()
        {
            new Coordinate(90, 0),
            new Coordinate(24, 20),
            new Coordinate(24, 90),
            new Coordinate(22, 90), // To create short vertical segment
            new Coordinate(-20, 40),
            new Coordinate(-90, 60),
            new Coordinate(-45, 0),
            new Coordinate(-90, -60),
            new Coordinate(-20, -40),
            new Coordinate(24, -90),
            new Coordinate(24, -20),
            new Coordinate(90, 0),
        };

        [Test]
        public void Region_Initialize()
        {
            Region region = new Region(NAME_REGION, StarRegion);

            Assert.That(region.Name, Is.EqualTo(NAME_REGION));

            Extents extents = new Extents(region.Extents);
            Assert.That(extents.MaxLatitude, Is.EqualTo(90));
            Assert.That(extents.MinLatitude, Is.EqualTo(-90));
            Assert.That(extents.MaxLongitude, Is.EqualTo(90));
            Assert.That(extents.MinLongitude, Is.EqualTo(-90));

            // First boundary vertex
            Assert.That(region.Boundary[0].Latitude, Is.EqualTo(StarRegion[0].Latitude));
            Assert.That(region.Boundary[0].Longitude, Is.EqualTo(StarRegion[0].Longitude));

            // Random boundary vertex
            Assert.That(region.Boundary[5].Latitude, Is.EqualTo(StarRegion[5].Latitude));
            Assert.That(region.Boundary[5].Longitude, Is.EqualTo(StarRegion[5].Longitude));

            // Last boundary vertex
            Assert.That(region.Boundary[StarRegion.Count - 1].Latitude, Is.EqualTo(StarRegion[StarRegion.Count - 1].Latitude));
            Assert.That(region.Boundary[StarRegion.Count - 1].Longitude, Is.EqualTo(StarRegion[StarRegion.Count - 1].Longitude));

            Assert.That(region.Formations.Count, Is.EqualTo(0));
            Assert.That(region.FormationsBase.Count, Is.EqualTo(0));
        }

        [Test]
        public void AddFormationByCoordinates_Not_Within_Extents_Or_Shape_Does_Not_Add()
        {
            Coordinate coordinateOutside = new Coordinate(120, 5);
            Formation formationOutside = new Formation(NAME_FORMATION, coordinateOutside);
            Region region = new Region(NAME_REGION, StarRegion);

            Assert.That(region.FormationsBase, Is.Empty);

            region.AddFormationByCoordinates(formationOutside);

            Assert.That(region.FormationsBase, Is.Empty);
        }

        [Test]
        public void AddFormationByCoordinates_Within_Extents_But_Not_Shape_Does_Not_Add()
        {
            Coordinate coordinatePartiallyOutside = new Coordinate(25, 21);
            Formation formationPartiallyOutside = new Formation(NAME_FORMATION, coordinatePartiallyOutside);
            Region region = new Region(NAME_REGION, StarRegion);

            Assert.That(region.FormationsBase, Is.Empty);

            region.AddFormationByCoordinates(formationPartiallyOutside);

            Assert.That(region.FormationsBase, Is.Empty);
        }

        [Test]
        public void AddFormationByCoordinates_Within_Shape_Adds()
        {
            Coordinate coordinateInside = new Coordinate(20, 20);
            Formation formationInside = new Formation(NAME_FORMATION, coordinateInside);
            Region region = new Region(NAME_REGION, StarRegion);

            Assert.That(region.FormationsBase, Is.Empty);

            Assert.IsTrue(region.AddFormationByCoordinates(formationInside));

            Assert.That(region.FormationsBase.Count, Is.EqualTo(1));
            Assert.That(region.FormationsBase[0].Name, Is.EqualTo(NAME_FORMATION));
        }

        [Test]
        public void AddFormationByCoordinates_Within_Shape_Intersects_Vertical_Segment_Adds()
        {
            Coordinate coordinateInside = new Coordinate(23, 89);
            Formation formationInside = new Formation(NAME_FORMATION, coordinateInside);
            Region region = new Region(NAME_REGION, StarRegion);

            Assert.That(region.FormationsBase, Is.Empty);

            Assert.IsTrue(region.AddFormationByCoordinates(formationInside));

            Assert.That(region.FormationsBase.Count, Is.EqualTo(1));
        }

        [Test]
        public void AddFormationByCoordinates_On_Shape_Vertex_Adds()
        {
            Formation formationOnVertex = new Formation(NAME_FORMATION, StarRegion[0]);
            Region region = new Region(NAME_REGION, StarRegion);

            Assert.That(region.FormationsBase, Is.Empty);

            Assert.IsTrue(region.AddFormationByCoordinates(formationOnVertex));

            Assert.That(region.FormationsBase.Count, Is.EqualTo(1));
        }

        [Test]
        public void AddFormationByCoordinates_On_Shape_Segment_Adds()
        {
            Coordinate coordinateOnShapeSegment = new Coordinate(24, 45);
            Formation formationOnShapeSegment = new Formation(NAME_FORMATION, coordinateOnShapeSegment);
            Region region = new Region(NAME_REGION, StarRegion);

            Assert.That(region.FormationsBase, Is.Empty);

            Assert.IsTrue(region.AddFormationByCoordinates(formationOnShapeSegment));

            Assert.That(region.FormationsBase.Count, Is.EqualTo(1));
        }

        [Test]
        public void AddFormationsByCoordinates()
        {
            List<Formation> formations = new List<Formation>();

            Coordinate coordinateOutside = new Coordinate(120, 5);
            Formation formationOutside = new Formation(NAME_FORMATION + "Outside", coordinateOutside);
            formations.Add(formationOutside);

            Coordinate coordinateInside = new Coordinate(23, 90);
            Formation formationInside = new Formation(NAME_FORMATION + "Inside", coordinateInside);
            formations.Add(formationInside);

            Coordinate coordinatePartiallyOutside = new Coordinate(25, 21);
            Formation formationPartiallyOutside = new Formation(NAME_FORMATION + "PartiallyOutside", coordinatePartiallyOutside);
            formations.Add(formationPartiallyOutside);

            Formation formationOnVertex = new Formation(NAME_FORMATION + "OnVertex", StarRegion[0]);
            formations.Add(formationOnVertex);

            Coordinate coordinateOnShapeSegment = new Coordinate(24, 45);
            Formation formationOnShapeSegment = new Formation(NAME_FORMATION + "OnSegment", coordinateOnShapeSegment);
            formations.Add(formationOnShapeSegment);


            Region region = new Region(NAME_REGION, StarRegion);

            Assert.That(region.FormationsBase, Is.Empty);

            region.AddFormationsByCoordinates(formations);

            Assert.That(region.FormationsBase.Count, Is.EqualTo(3));
            Assert.That(region.FormationsBase[0].Name, Is.EqualTo(NAME_FORMATION + "Inside"));
            Assert.That(region.FormationsBase[1].Name, Is.EqualTo(NAME_FORMATION + "OnVertex"));
            Assert.That(region.FormationsBase[2].Name, Is.EqualTo(NAME_FORMATION + "OnSegment"));
        }

        [Test]
        public void AddFormationByRegionName_Nonmatching_Region_Name_Does_Not_Add()
        {
            Region region = new Region(NAME_REGION, StarRegion);
            FormationMatcher nonMatchingFormationMatcher = new FormationMatcher(NAME_REGION+"NonMatching", NAME_FORMATION, NAME_SUBFORMATION);
            region.AddFormationByRegionName(nonMatchingFormationMatcher);

            Assert.That(region.Formations, Is.Empty);
        }
        
        [Test]
        public void AddFormationByRegionName_Matching_Region_Name_Adds()
        {
            Region region = new Region(NAME_REGION, StarRegion);
            FormationMatcher matchingFormationMatcher = new FormationMatcher(NAME_REGION, NAME_FORMATION);
            region.AddFormationByRegionName(matchingFormationMatcher);

            Assert.That(region.Formations.Count, Is.EqualTo(1));
            Assert.That(region.Formations[0].FormationName, Is.EqualTo(NAME_FORMATION));
        }

        [Test]
        public void AddFormationsByRegionName()
        {
            List<FormationMatcher> formations = new List<FormationMatcher>();
            FormationMatcher nonMatchingFormationMatcher = new FormationMatcher(NAME_REGION + "NonMatching", NAME_FORMATION, NAME_SUBFORMATION);
            formations.Add(nonMatchingFormationMatcher);
            FormationMatcher matchingFormationMatcher = new FormationMatcher(NAME_REGION, NAME_FORMATION);
            formations.Add(matchingFormationMatcher);

            Region region = new Region(NAME_REGION, StarRegion);
            region.AddFormationsByRegionName(formations);

            Assert.That(region.Formations.Count, Is.EqualTo(1));
            Assert.That(region.Formations[0].FormationName, Is.EqualTo(NAME_FORMATION));
        }

        [Test]
        public void MergeFormations()
        {
            List<FormationMatcher> formationMatchers = new List<FormationMatcher>();

            FormationMatcher nonMatchingFormationMatcher = new FormationMatcher(NAME_REGION + "NonMatching", NAME_FORMATION, NAME_SUBFORMATION);
            formationMatchers.Add(nonMatchingFormationMatcher);
            FormationMatcher matchingFormationMatcher = new FormationMatcher(NAME_REGION, NAME_FORMATION);
            formationMatchers.Add(matchingFormationMatcher);


            List<Formation> formations = new List<Formation>();

            Coordinate coordinateOutside = new Coordinate(120, 5);
            Formation formationOutside = new Formation(NAME_FORMATION + "Outside", coordinateOutside);
            formations.Add(formationOutside);

            Coordinate coordinateInside = new Coordinate(23, 90);
            Formation formationInside = new Formation(NAME_FORMATION + "Inside", coordinateInside);
            formations.Add(formationInside);

            Coordinate coordinatePartiallyOutside = new Coordinate(25, 21);
            Formation formationPartiallyOutside = new Formation(NAME_FORMATION + "PartiallyOutside", coordinatePartiallyOutside);
            formations.Add(formationPartiallyOutside);

            Formation formationOnVertex = new Formation(NAME_FORMATION + "OnVertex", StarRegion[0]);
            formations.Add(formationOnVertex);

            Coordinate coordinateOnShapeSegment = new Coordinate(24, 45);
            Formation formationOnShapeSegment = new Formation(NAME_FORMATION + "OnSegment", coordinateOnShapeSegment);
            formations.Add(formationOnShapeSegment);


            Region region = new Region(NAME_REGION, StarRegion);
            region.AddFormationsByRegionName(formationMatchers);
            region.AddFormationsByCoordinates(formations);
            region.MergeFormations();

            Assert.That(region.Formations.Count, Is.EqualTo(1));
            Assert.That(region.Formations[0].MatchedFormation, Is.Null);
            Assert.That(region.Formations[0].PossibleFormations.Count, Is.EqualTo(3));
            Assert.That(region.Formations[0].PossibleFormations[0].Name, Is.EqualTo(NAME_FORMATION + "Inside"));
            Assert.That(region.Formations[0].PossibleFormations[1].Name, Is.EqualTo(NAME_FORMATION + "OnVertex"));
            Assert.That(region.Formations[0].PossibleFormations[2].Name, Is.EqualTo(NAME_FORMATION + "OnSegment"));
        }

        [Test]
        public void MergeFormations_Where_No_Formation_Matcher_Is_Present()
        {
            List<Formation> formations = new List<Formation>();

            Coordinate coordinateOutside = new Coordinate(120, 5);
            Formation formationOutside = new Formation(NAME_FORMATION + "Outside", coordinateOutside);
            formations.Add(formationOutside);

            Coordinate coordinateInside = new Coordinate(23, 90);
            Formation formationInside = new Formation(NAME_FORMATION + "Inside", coordinateInside);
            formations.Add(formationInside);

            Coordinate coordinatePartiallyOutside = new Coordinate(25, 21);
            Formation formationPartiallyOutside = new Formation(NAME_FORMATION + "PartiallyOutside", coordinatePartiallyOutside);
            formations.Add(formationPartiallyOutside);

            Formation formationOnVertex = new Formation(NAME_FORMATION + "OnVertex", StarRegion[0]);
            formations.Add(formationOnVertex);

            Coordinate coordinateOnShapeSegment = new Coordinate(24, 45);
            Formation formationOnShapeSegment = new Formation(NAME_FORMATION + "OnSegment", coordinateOnShapeSegment);
            formations.Add(formationOnShapeSegment);


            Region region = new Region(NAME_REGION, StarRegion);
            region.AddFormationsByCoordinates(formations);
            region.MergeFormations();

            Assert.That(region.Formations.Count, Is.EqualTo(1));
            Assert.That(region.Formations[0].MatchedFormation, Is.Null);
            Assert.That(region.Formations[0].PossibleFormations.Count, Is.EqualTo(1));
            Assert.That(region.Formations[0].PossibleFormations[0].Name, Is.EqualTo(NAME_FORMATION + "Inside"));
        }

        [Test]
        public void CondensePotentialMatches()
        {

            List<FormationMatcher> formationMatchers = new List<FormationMatcher>();

            FormationMatcher nonMatchingFormationMatcher = new FormationMatcher(NAME_REGION + "NonMatching", NAME_FORMATION, NAME_SUBFORMATION);
            formationMatchers.Add(nonMatchingFormationMatcher);
            FormationMatcher matchingFormationMatcher = new FormationMatcher(NAME_REGION, NAME_FORMATION);
            formationMatchers.Add(matchingFormationMatcher);


            List<Formation> formations = new List<Formation>();

            Coordinate coordinateOutside = new Coordinate(120, 5);
            Formation formationOutside = new Formation(NAME_FORMATION + "Outside", coordinateOutside);
            formations.Add(formationOutside);

            Coordinate coordinateInside = new Coordinate(23, 90);
            Formation formationInside = new Formation(NAME_FORMATION + "Inside", coordinateInside);
            formations.Add(formationInside);

            Coordinate coordinatePartiallyOutside = new Coordinate(25, 21);
            Formation formationPartiallyOutside = new Formation(NAME_FORMATION + "PartiallyOutside", coordinatePartiallyOutside);
            formations.Add(formationPartiallyOutside);

            Formation formationOnVertex = new Formation(NAME_FORMATION + "OnVertex", StarRegion[0]);
            formations.Add(formationOnVertex);

            Coordinate coordinateOnShapeSegment = new Coordinate(24, 45);
            Formation formationOnShapeSegment = new Formation(NAME_FORMATION + "OnSegment", coordinateOnShapeSegment);
            formations.Add(formationOnShapeSegment);

            Coordinate coordinateMatchingFormation = new Coordinate(1, 2);
            Formation formationMatching = new Formation(NAME_FORMATION, coordinateMatchingFormation);
            formations.Add(formationMatching);


            Region region = new Region(NAME_REGION, StarRegion);
            region.AddFormationsByRegionName(formationMatchers);
            region.AddFormationsByCoordinates(formations);
            region.MergeFormations();

            Assert.That(region.Formations.Count, Is.EqualTo(1));
            Assert.That(region.Formations[0].MatchedFormation, Is.Null);
            Assert.That(region.Formations[0].PossibleFormations.Count, Is.EqualTo(4));
            Assert.That(region.Formations[0].PossibleFormations[0].Name, Is.EqualTo(NAME_FORMATION + "Inside"));
            Assert.That(region.Formations[0].PossibleFormations[1].Name, Is.EqualTo(NAME_FORMATION + "OnVertex"));
            Assert.That(region.Formations[0].PossibleFormations[2].Name, Is.EqualTo(NAME_FORMATION + "OnSegment"));
            Assert.That(region.Formations[0].PossibleFormations[3].Name, Is.EqualTo(NAME_FORMATION ));

            region.CondensePotentialMatches();

            Assert.That(region.Formations[0].MatchedFormation, Is.Not.Null);
            int indexMatching = formations.IndexOf(formationMatching);
            Assert.That(region.Formations[0].MatchedFormation.Name, Is.EqualTo(formations[indexMatching].Name));
            Assert.That(region.Formations[0].MatchedFormation.OtherName, Is.EqualTo(formations[indexMatching].OtherName));
            Assert.That(region.Formations[0].MatchedFormation.SubFormationName, Is.EqualTo(formations[indexMatching].SubFormationName));
        }

        [Test]
        public void CondensePotentialMatches_Ericcson_Crags_And_Crag_And_Mountain()
        {
            string regionName = "Collegiate Area";
            string baseName = "Ericsson";
            string formationMtEricsson = $"Mount {baseName}";
            string formationEricssonCrags = $"{baseName} Crags";
            string formationEricssonCrag1 = $"{baseName} Crag 1";
            string formationEricssonCrag2 = $"{baseName} Crag 2";
            string formationEricssonCrag3 = $"{baseName} Crag #3";
            string formationEricssonCrag4 = $"{baseName} Crag Number 4";

            List<FormationMatcher> formationMatchers = new List<FormationMatcher>();
            FormationMatcher formationMatcher1 = new FormationMatcher(regionName, formationEricssonCrags, formationEricssonCrag3);
            formationMatchers.Add(formationMatcher1);
            FormationMatcher formationMatcher2 = new FormationMatcher(regionName, formationMtEricsson);
            formationMatchers.Add(formationMatcher2);

            List<Formation> formations = new List<Formation>();
            Formation formation1 = new Formation(formationEricssonCrags, 1, 2, subformationName: formationEricssonCrag1);
            formations.Add(formation1);
            Formation formation2 = new Formation(formationEricssonCrags, 3, 4, subformationName: formationEricssonCrag2);
            formations.Add(formation2);
            Formation formation3 = new Formation(formationEricssonCrags, 5, 6, subformationName: formationEricssonCrag3);
            formations.Add(formation3);
            Formation formation4 = new Formation(formationEricssonCrags, 7, 8, subformationName: formationEricssonCrag4);
            formations.Add(formation4);
            Formation formationCrags = new Formation(formationEricssonCrags, 9, 10);
            formations.Add(formationCrags);
            Formation formationMtn = new Formation(formationMtEricsson, 11, 12);
            formations.Add(formationMtn);
            
            Region region = new Region(regionName, StarRegion);
            region.AddFormationsByRegionName(formationMatchers);
            region.AddFormationsByCoordinates(formations);
            region.MergeFormations();

            Assert.That(region.Formations.Count, Is.EqualTo(2));

            Assert.That(region.Formations[0].MatchedFormation, Is.Null);
            Assert.That(region.Formations[0].PossibleFormations.Count, Is.EqualTo(5));
            Assert.That(region.Formations[0].PossibleFormations[0].SubFormationName, Is.EqualTo(formationEricssonCrag1));
            Assert.That(region.Formations[0].PossibleFormations[1].SubFormationName, Is.EqualTo(formationEricssonCrag2));
            Assert.That(region.Formations[0].PossibleFormations[2].SubFormationName, Is.EqualTo(formationEricssonCrag3));
            Assert.That(region.Formations[0].PossibleFormations[3].SubFormationName, Is.EqualTo(formationEricssonCrag4));
            Assert.That(region.Formations[0].PossibleFormations[4].Name, Is.EqualTo(formationEricssonCrags));

            Assert.That(region.Formations[1].MatchedFormation, Is.Null);
            Assert.That(region.Formations[1].PossibleFormations.Count, Is.EqualTo(1));
            Assert.That(region.Formations[1].PossibleFormations[0].Name, Is.EqualTo(formationMtEricsson));

            region.CondensePotentialMatches();

            Assert.That(region.Formations[0].MatchedFormation, Is.Not.Null);
            int indexCrag = formations.IndexOf(formation3);
            Assert.That(region.Formations[0].MatchedFormation.Name, Is.EqualTo(formations[indexCrag].Name));
            Assert.That(region.Formations[0].MatchedFormation.OtherName, Is.EqualTo(formations[indexCrag].OtherName));
            Assert.That(region.Formations[0].MatchedFormation.SubFormationName, Is.EqualTo(formations[indexCrag].SubFormationName));

            Assert.That(region.Formations[1].MatchedFormation, Is.Not.Null);
            int indexMtn = formations.IndexOf(formationMtn);
            Assert.That(region.Formations[1].MatchedFormation.Name, Is.EqualTo(formations[indexMtn].Name));
            Assert.That(region.Formations[1].MatchedFormation.OtherName, Is.EqualTo(formations[indexMtn].OtherName));
            Assert.That(region.Formations[1].MatchedFormation.SubFormationName, Is.EqualTo(formations[indexMtn].SubFormationName));
        }


        [Test]
        public void CondensePotentialMatches_Cathedral_Peak_And_Eichorns_Pinnacle()
        {
            string regionName = "Cathedral Area";
            string formationCathedralPk = "Cathedral Peak";
            string formationEichornsPinnacle = "Eichorn''s Pinnacle";

            List<FormationMatcher> formationMatchers = new List<FormationMatcher>();
            FormationMatcher formationMatcher1 = new FormationMatcher(regionName, formationCathedralPk);
            formationMatchers.Add(formationMatcher1);
            FormationMatcher formationMatcher2 = new FormationMatcher(regionName, formationEichornsPinnacle);
            formationMatchers.Add(formationMatcher2);

            List<Formation> formations = new List<Formation>();
            Formation formation1 = new Formation(formationCathedralPk, 1, 2);
            formations.Add(formation1);
            Formation formation2 = new Formation(formationCathedralPk, 3, 4, subformationName: formationEichornsPinnacle);
            formations.Add(formation2);

            Region region = new Region(regionName, StarRegion);
            region.AddFormationsByRegionName(formationMatchers);
            region.AddFormationsByCoordinates(formations);
            region.MergeFormations();

            Assert.That(region.Formations.Count, Is.EqualTo(2));

            Assert.That(region.Formations[0].MatchedFormation, Is.Null);
            Assert.That(region.Formations[0].PossibleFormations.Count, Is.EqualTo(2));
            Assert.That(region.Formations[0].PossibleFormations[0].Name, Is.EqualTo(formationCathedralPk));
            Assert.That(region.Formations[0].PossibleFormations[0].SubFormationName, Is.Empty);
            Assert.That(region.Formations[0].PossibleFormations[1].Name, Is.EqualTo(formationCathedralPk));
            Assert.That(region.Formations[0].PossibleFormations[1].SubFormationName, Is.EqualTo(formationEichornsPinnacle));

            Assert.That(region.Formations[1].MatchedFormation, Is.Null);
            Assert.That(region.Formations[1].PossibleFormations.Count, Is.EqualTo(1));
            Assert.That(region.Formations[1].PossibleFormations[0].Name, Is.EqualTo(formationCathedralPk));

            region.CondensePotentialMatches();

            Assert.That(region.Formations[0].MatchedFormation, Is.Not.Null);
            int indexCathedral = formations.LastIndexOf(formation1);
            Assert.That(region.Formations[0].MatchedFormation.Name, Is.EqualTo(formations[indexCathedral].Name));
            Assert.That(region.Formations[0].MatchedFormation.OtherName, Is.EqualTo(formations[indexCathedral].OtherName));
            Assert.That(region.Formations[0].MatchedFormation.SubFormationName, Is.EqualTo(formations[indexCathedral].SubFormationName));

            Assert.That(region.Formations[1].MatchedFormation, Is.Not.Null);
            int indexEichorn = formations.LastIndexOf(formation2);
            Assert.That(region.Formations[1].MatchedFormation.Name, Is.EqualTo(formations[indexEichorn].Name));
            Assert.That(region.Formations[1].MatchedFormation.OtherName, Is.EqualTo(formations[indexEichorn].OtherName));
            Assert.That(region.Formations[1].MatchedFormation.SubFormationName, Is.EqualTo(formations[indexEichorn].SubFormationName));
        }

        [Test]
        public void Split_Polyline_Is_Outside_Does_Not_Split()
        {

        }

        [Test]
        public void Split_Polyline_Only_Touches_Vertices_Does_Not_Split()
        {

        }

        [Test]
        public void Split_Poyline_Is_Null_or_Empty_Does_Not_Split()
        {

        }

        [Test]
        public void Split_Polyine_Intersects_Vertex_Splits()
        {

        }

        [Test]
        public void Split_Polyline_Intersects_Segment_Splits()
        {

        }

        [Test]
        public void Split_Polyline_Ends_Inside_Shape_Splits_By_Extrapolated_Tangent()
        {

        }

        [Test]
        public void Split_Polyline_Starts_Inside_Shape_Splits_By_Extrapolated_Tangent()
        {

        }

        [Test]
        public void Split_Polyline_Starts_And_Ends_Inside_Shape_Splits_By_Non_Crossing_Extrapolated_Tangent()
        {

        }

        [Test]
        public void Split_Polyline_Starts_And_Ends_Inside_Shape_Splits_By_Crossing_Extrapolated_Tangent()
        {

        }

        [Test]
        public void Split_Polyline_Forms_Interior_Shape_Splits()
        {

        }

        [Test]
        public void Override_ToString_Empty_Returns_Default_ToString()
        {
            Region region = new Region("", StarRegion);
            Assert.That(region.ToString(), Is.EqualTo("MPT.GIS.Region"));
        }

        [Test]
        public void Override_ToString_With_Name()
        {
            Region region = new Region(NAME_REGION, StarRegion);
            Assert.That(region.ToString(), Is.EqualTo(NAME_REGION));
        }
    }
}
