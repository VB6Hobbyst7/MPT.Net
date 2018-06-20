using System.Collections.Generic;
using MPT.String.Word;
using NUnit.Framework;

namespace MPT.GIS.Tests
{
    [TestFixture]
    public class FormationMatcherTests
    {
        public const string NAME_REGION = "FooRegion";

        public const string NAME_MATCHER_FORMATION = "FooMatcherFormation";
        public const string NAME_MATCHER_SUBFORMATION = "BarMatcherFormation";

        public const string NAME_MATCHER_FORMATION_ELEVATION = "Point 13749";
        public const string NAME_MATCHER_SUBFORMATION_ELEVATION = "Point 13149";

        public const string NAME_FORMATION = "FooFormation";
        public const string NAME_OTHER_FORMATION = "OtherFormation";
        public const string NAME_SUBFORMATION = "BarFormation";


        [Test]
        public void FormationMatcher_Initialize_Partial_Initializes_Partial()
        {
            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, NAME_MATCHER_FORMATION);

            Assert.That(formationMatcher.RegionName, Is.EqualTo(NAME_REGION));
            Assert.That(formationMatcher.FormationName, Is.EqualTo(NAME_MATCHER_FORMATION));
            Assert.That(formationMatcher.MatchedFormation, Is.Null);
            Assert.That(formationMatcher.PossibleFormations, Is.Empty);
        }

        [Test]
        public void FormationMatcher_Initialize_Full_Initializes_Full()
        {
            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, NAME_MATCHER_FORMATION, NAME_MATCHER_SUBFORMATION);

            Assert.That(formationMatcher.RegionName, Is.EqualTo(NAME_REGION));
            Assert.That(formationMatcher.FormationName, Is.EqualTo(NAME_MATCHER_FORMATION));
            Assert.That(formationMatcher.SubFormationName, Is.EqualTo(NAME_MATCHER_SUBFORMATION));
            Assert.That(formationMatcher.MatchedFormation, Is.Null);
            Assert.That(formationMatcher.PossibleFormations, Is.Empty);
        }

        [Test]
        public void AddIfPotentialMatchByName_Non_Potential_Match_Does_Not_Add()
        {
            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, NAME_MATCHER_FORMATION, NAME_MATCHER_SUBFORMATION);
            Formation formation = new Formation("Moo", 90, 90, "Nar", 0, "Car");
            formationMatcher.AddIfPotentialMatchByName(formation);

            Assert.That(formationMatcher.PossibleFormations, Is.Empty);
        }

        [TestCase(NAME_MATCHER_FORMATION)] // Perfect match
        [TestCase(NAME_MATCHER_FORMATION + "Extra")] // Contains
        [TestCase(NAME_MATCHER_FORMATION + " Extra")] // Contains
        [TestCase("Foo")] // Is contained within
        [TestCase(NAME_MATCHER_FORMATION + "s")] // Perfect match
        [TestCase(NAME_MATCHER_FORMATION + "s" + "Extra")] // Contains
        [TestCase(NAME_MATCHER_FORMATION + "s" + " Extra")] // Contains
        [TestCase("Foos")] // Is contained within
        [TestCase(NAME_MATCHER_FORMATION + "'s")] // Perfect match
        [TestCase(NAME_MATCHER_FORMATION + "'s" + "Extra")] // Contains
        [TestCase(NAME_MATCHER_FORMATION + "'s" + " Extra")] // Contains
        [TestCase("Foo's")] // Is contained within
        [TestCase(NAME_MATCHER_FORMATION + "s'")] // Perfect match
        [TestCase(NAME_MATCHER_FORMATION + "s'" + "Extra")] // Contains
        [TestCase(NAME_MATCHER_FORMATION + "s'" + " Extra")] // Contains
        [TestCase("Foos'")] // Is contained within
        public void AddIfPotentialMatchByName_Potential_Match_By_FormationName_To_Name_Adds(string formationName)
        {
            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, NAME_MATCHER_FORMATION, NAME_MATCHER_SUBFORMATION);
            Formation formation = new Formation(formationName, 90, 90);
            formationMatcher.AddIfPotentialMatchByName(formation);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(1));
            Assert.That(formationMatcher.PossibleFormations[0].Name, Is.EqualTo(formationName));
        }

        [TestCase(NAME_MATCHER_SUBFORMATION)] // Perfect match
        [TestCase(NAME_MATCHER_SUBFORMATION + "Extra")] // Contains
        [TestCase(NAME_MATCHER_SUBFORMATION + " Extra")] // Contains
        [TestCase("Bar")] // Is contained within
        [TestCase(NAME_MATCHER_SUBFORMATION + "s")] // Perfect match
        [TestCase(NAME_MATCHER_SUBFORMATION + "s" + "Extra")] // Contains
        [TestCase(NAME_MATCHER_SUBFORMATION + "s" + " Extra")] // Contains
        [TestCase("Bars")] // Is contained within
        [TestCase(NAME_MATCHER_SUBFORMATION + "'s")] // Perfect match
        [TestCase(NAME_MATCHER_SUBFORMATION + "'s" + "Extra")] // Contains
        [TestCase(NAME_MATCHER_SUBFORMATION + "'s" + " Extra")] // Contains
        [TestCase("Bar's")] // Is contained within
        [TestCase(NAME_MATCHER_SUBFORMATION + "s'")] // Perfect match
        [TestCase(NAME_MATCHER_SUBFORMATION + "s'" + "Extra")] // Contains
        [TestCase(NAME_MATCHER_SUBFORMATION + "s'" + " Extra")] // Contains
        [TestCase("Bars'")] // Is contained within
        public void AddIfPotentialMatchByName_Potential_Match_By_FormationName_To_SubformationName_Adds(string formationName)
        {
            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, NAME_MATCHER_FORMATION, NAME_MATCHER_SUBFORMATION);
            Formation formation = new Formation(formationName, 90, 90);
            formationMatcher.AddIfPotentialMatchByName(formation);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(1));
            Assert.That(formationMatcher.PossibleFormations[0].Name, Is.EqualTo(formationName));
        }

        [TestCase(NAME_MATCHER_FORMATION)] // Perfect match
        [TestCase(NAME_MATCHER_FORMATION + "Extra")] // Contains
        [TestCase(NAME_MATCHER_FORMATION + " Extra")] // Contains
        [TestCase("Foo")] // Is contained within
        [TestCase(NAME_MATCHER_FORMATION + "s")] // Perfect match
        [TestCase(NAME_MATCHER_FORMATION + "s" + "Extra")] // Contains
        [TestCase(NAME_MATCHER_FORMATION + "s" + " Extra")] // Contains
        [TestCase("Foos")] // Is contained within
        [TestCase(NAME_MATCHER_FORMATION + "'s")] // Perfect match
        [TestCase(NAME_MATCHER_FORMATION + "'s" + "Extra")] // Contains
        [TestCase(NAME_MATCHER_FORMATION + "'s" + " Extra")] // Contains
        [TestCase("Foo's")] // Is contained within
        [TestCase(NAME_MATCHER_FORMATION + "s'")] // Perfect match
        [TestCase(NAME_MATCHER_FORMATION + "s'" + "Extra")] // Contains
        [TestCase(NAME_MATCHER_FORMATION + "s'" + " Extra")] // Contains
        [TestCase("Foos'")] // Is contained within
        public void AddIfPotentialMatchByName_Potential_Match_By_OtherName_To_Name_Adds(string otherName)
        {
            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, NAME_MATCHER_FORMATION, NAME_MATCHER_SUBFORMATION);
            Formation formation = new Formation("Moo", 90, 90, otherName);
            formationMatcher.AddIfPotentialMatchByName(formation);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(1));
            Assert.That(formationMatcher.PossibleFormations[0].OtherName, Is.EqualTo(otherName));
        }

        [TestCase(NAME_MATCHER_SUBFORMATION)] // Perfect match
        [TestCase(NAME_MATCHER_SUBFORMATION + "Extra")] // Contains
        [TestCase(NAME_MATCHER_SUBFORMATION + " Extra")] // Contains
        [TestCase("Bar")] // Is contained within
        [TestCase(NAME_MATCHER_SUBFORMATION + "s")] // Perfect match
        [TestCase(NAME_MATCHER_SUBFORMATION + "s" + "Extra")] // Contains
        [TestCase(NAME_MATCHER_SUBFORMATION + "s" + " Extra")] // Contains
        [TestCase("Bars")] // Is contained within
        [TestCase(NAME_MATCHER_SUBFORMATION + "'s")] // Perfect match
        [TestCase(NAME_MATCHER_SUBFORMATION + "'s" + "Extra")] // Contains
        [TestCase(NAME_MATCHER_SUBFORMATION + "'s" + " Extra")] // Contains
        [TestCase("Bar's")] // Is contained within
        [TestCase(NAME_MATCHER_SUBFORMATION + "s'")] // Perfect match
        [TestCase(NAME_MATCHER_SUBFORMATION + "s'" + "Extra")] // Contains
        [TestCase(NAME_MATCHER_SUBFORMATION + "s'" + " Extra")] // Contains
        [TestCase("Bars'")] // Is contained within
        public void AddIfPotentialMatchByName_Potential_Match_By_OtherName_To_SubformationName_Adds(string otherName)
        {
            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, NAME_MATCHER_FORMATION, NAME_MATCHER_SUBFORMATION);
            Formation formation = new Formation("Moo", 90, 90, otherName);
            formationMatcher.AddIfPotentialMatchByName(formation);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(1));
            Assert.That(formationMatcher.PossibleFormations[0].OtherName, Is.EqualTo(otherName));
        }

        [TestCase(NAME_MATCHER_FORMATION)] // Perfect match
        [TestCase(NAME_MATCHER_FORMATION + "Extra")] // Contains
        [TestCase(NAME_MATCHER_FORMATION + " Extra")] // Contains
        [TestCase("Foo")] // Is contained within
        [TestCase(NAME_MATCHER_FORMATION + "s")] // Perfect match
        [TestCase(NAME_MATCHER_FORMATION + "s" + "Extra")] // Contains
        [TestCase(NAME_MATCHER_FORMATION + "s" + " Extra")] // Contains
        [TestCase("Foos")] // Is contained within
        [TestCase(NAME_MATCHER_FORMATION + "'s")] // Perfect match
        [TestCase(NAME_MATCHER_FORMATION + "'s" + "Extra")] // Contains
        [TestCase(NAME_MATCHER_FORMATION + "'s" + " Extra")] // Contains
        [TestCase("Foo's")] // Is contained within
        [TestCase(NAME_MATCHER_FORMATION + "s'")] // Perfect match
        [TestCase(NAME_MATCHER_FORMATION + "s'" + "Extra")] // Contains
        [TestCase(NAME_MATCHER_FORMATION + "s'" + " Extra")] // Contains
        [TestCase("Foos'")] // Is contained within
        public void AddIfPotentialMatchByName_Potential_Match_By_SubformationName_To_Name_Adds(string subformationName)
        {
            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, NAME_MATCHER_FORMATION, NAME_MATCHER_SUBFORMATION);
            Formation formation = new Formation("Moo", 90, 90, "Nar", 0, subformationName);
            formationMatcher.AddIfPotentialMatchByName(formation);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(1));
            Assert.That(formationMatcher.PossibleFormations[0].SubFormationName, Is.EqualTo(subformationName));
        }

        [TestCase(NAME_MATCHER_SUBFORMATION)] // Perfect match
        [TestCase(NAME_MATCHER_SUBFORMATION + "Extra")] // Contains
        [TestCase(NAME_MATCHER_SUBFORMATION + " Extra")] // Contains
        [TestCase("Bar")] // Is contained within
        [TestCase(NAME_MATCHER_SUBFORMATION + "s")] // Perfect match
        [TestCase(NAME_MATCHER_SUBFORMATION + "s" + "Extra")] // Contains
        [TestCase(NAME_MATCHER_SUBFORMATION + "s" + " Extra")] // Contains
        [TestCase("Bars")] // Is contained within
        [TestCase(NAME_MATCHER_SUBFORMATION + "'s")] // Perfect match
        [TestCase(NAME_MATCHER_SUBFORMATION + "'s" + "Extra")] // Contains
        [TestCase(NAME_MATCHER_SUBFORMATION + "'s" + " Extra")] // Contains
        [TestCase("Bar's")] // Is contained within
        [TestCase(NAME_MATCHER_SUBFORMATION + "s'")] // Perfect match
        [TestCase(NAME_MATCHER_SUBFORMATION + "s'" + "Extra")] // Contains
        [TestCase(NAME_MATCHER_SUBFORMATION + "s'" + " Extra")] // Contains
        [TestCase("Bars'")] // Is contained within
        public void AddIfPotentialMatchByName_Potential_Match_By_SubformationName_To_SubformationName_Adds(string subformationName)
        {
            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, NAME_MATCHER_FORMATION, NAME_MATCHER_SUBFORMATION);
            Formation formation = new Formation("Moo", 90, 90, "Nar", 0, subformationName);
            formationMatcher.AddIfPotentialMatchByName(formation);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(1));
            Assert.That(formationMatcher.PossibleFormations[0].SubFormationName, Is.EqualTo(subformationName));
        }

        
        [TestCase("Pt. 13749")]
        [TestCase("Pt. 13,749")]
        [TestCase("Pt. 13749'")]
        public void AddIfPotentialMatchByName_PotentialMatch_By_FormationName_To_Name_Elevation_Adds(string formationName)
        {
            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, NAME_MATCHER_FORMATION_ELEVATION, NAME_MATCHER_SUBFORMATION);
            Formation formation = new Formation(formationName, 90, 90);
            formationMatcher.AddIfPotentialMatchByName(formation);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(1));
            Assert.That(formationMatcher.PossibleFormations[0].Name, Is.EqualTo(formationName));
        }

        [TestCase("Pt. 13149")]
        [TestCase("Pt. 13,149")]
        [TestCase("Pt. 13149'")]
        public void AddIfPotentialMatchByName_Potential_Match_By_FormationName_To_SubformationName_Elevation_Adds(string formationName)
        {
            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, NAME_MATCHER_FORMATION, NAME_MATCHER_SUBFORMATION_ELEVATION);
            Formation formation = new Formation(formationName, 90, 90);
            formationMatcher.AddIfPotentialMatchByName(formation);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(1));
            Assert.That(formationMatcher.PossibleFormations[0].Name, Is.EqualTo(formationName));
        }

        [TestCase("Pt. 13749")]
        [TestCase("Pt. 13,749")]
        [TestCase("Pt. 13749'")]
        public void AddIfPotentialMatchByName_Potential_Match_By_OtherName_To_Name_Elevation_Adds(string otherName)
        {
            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, NAME_MATCHER_FORMATION_ELEVATION, NAME_MATCHER_SUBFORMATION);
            Formation formation = new Formation("Moo", 90, 90, otherName);
            formationMatcher.AddIfPotentialMatchByName(formation);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(1));
            Assert.That(formationMatcher.PossibleFormations[0].OtherName, Is.EqualTo(otherName));
        }

        [TestCase("Pt. 13149")]
        [TestCase("Pt. 13,149")]
        [TestCase("Pt. 13149'")]
        public void AddIfPotentialMatchByName_PotentialMatch_By_OtherName_To_SubformationName_Elevation_Adds(string otherName)
        {
            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, NAME_MATCHER_FORMATION, NAME_MATCHER_SUBFORMATION_ELEVATION);
            Formation formation = new Formation("Moo", 90, 90, otherName);
            formationMatcher.AddIfPotentialMatchByName(formation);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(1));
            Assert.That(formationMatcher.PossibleFormations[0].OtherName, Is.EqualTo(otherName));
        }

        [TestCase("Pt. 13749")]
        [TestCase("Pt. 13,749")]
        [TestCase("Pt. 13749'")]
        public void AddIfPotentialMatchByName_Potential_Match_By_SubformationName_To_Name_Elevation_Adds(string subformationName)
        {
            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, NAME_MATCHER_FORMATION_ELEVATION, NAME_MATCHER_SUBFORMATION);
            Formation formation = new Formation("Moo", 90, 90, "Nar", 0, subformationName);
            formationMatcher.AddIfPotentialMatchByName(formation);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(1));
            Assert.That(formationMatcher.PossibleFormations[0].SubFormationName, Is.EqualTo(subformationName));
        }

        [TestCase("Pt. 13149")]
        [TestCase("Pt. 13,149")]
        [TestCase("Pt. 13149'")]
        public void AddIfPotentialMatchByName_Potential_Match_By_SubformationName_To_SubformationName_Elevation_Adds(string subformationName)
        {
            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, NAME_MATCHER_FORMATION, NAME_MATCHER_SUBFORMATION_ELEVATION);
            Formation formation = new Formation("Moo", 90, 90, "Nar", 0, subformationName);
            formationMatcher.AddIfPotentialMatchByName(formation);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(1));
            Assert.That(formationMatcher.PossibleFormations[0].SubFormationName, Is.EqualTo(subformationName));
        }



        [Test]
        public void AddIfPotentialMatchByName_For_IEnumerble()
        {
            List<Formation> formations = new List<Formation>();
            Formation formation1 = new Formation(NAME_FORMATION + "1", 90, 90, NAME_OTHER_FORMATION + "1", 0, NAME_SUBFORMATION + "1");
            formations.Add(formation1);
            Formation formation2 = new Formation(NAME_FORMATION + "2", 90, 90, NAME_OTHER_FORMATION + "2", 0, NAME_SUBFORMATION + "2");
            formations.Add(formation2);
            Formation formation3 = new Formation("Moo" + "3", 90, 90, "Nar" + "3", 0, "Car" + "3");
            formations.Add(formation3);
            Formation formation4 = new Formation(NAME_FORMATION + "3", 90, 90, NAME_OTHER_FORMATION + "3", 0, NAME_SUBFORMATION + "3");
            formations.Add(formation4);

            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, NAME_FORMATION, NAME_MATCHER_SUBFORMATION);
            formationMatcher.AddIfPotentialMatchByName(formations);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(3));
            Assert.That(formationMatcher.PossibleFormations[1].Name, Is.EqualTo(NAME_FORMATION + "2"));
        }

        [Test]
        public void ChooseMatch_Chooses_Match()
        {
            List<Formation> formations = new List<Formation>();
            Formation formation1 = new Formation(NAME_FORMATION + "1", 90, 90, NAME_OTHER_FORMATION + "1", 0, NAME_SUBFORMATION + "1");
            formations.Add(formation1);
            Formation formation2 = new Formation(NAME_FORMATION + "2", 90, 90, NAME_OTHER_FORMATION + "2", 0, NAME_SUBFORMATION + "2");
            formations.Add(formation2);
            Formation formation3 = new Formation(NAME_FORMATION + "3", 90, 90, NAME_OTHER_FORMATION + "3", 0, NAME_SUBFORMATION + "3");
            formations.Add(formation3);

            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, NAME_FORMATION, NAME_MATCHER_SUBFORMATION);
            formationMatcher.AddIfPotentialMatchByName(formations);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(3));

            int matchIndex = 1;
            formationMatcher.ChooseMatch(matchIndex);

            Assert.That(formationMatcher.MatchedFormation, Is.Not.Null);
            Assert.That(formationMatcher.MatchedFormation.Name, Is.EqualTo(formations[matchIndex].Name));
            Assert.That(formationMatcher.MatchedFormation.OtherName, Is.EqualTo(formations[matchIndex].OtherName));
            Assert.That(formationMatcher.MatchedFormation.SubFormationName, Is.EqualTo(formations[matchIndex].SubFormationName));
        }

        [Test]
        public void CondensePotentialMatches_No_Potential_Matches_Does_Not_Match()
        {
            List<Formation> formations = new List<Formation>();
            Formation formation1 = new Formation("NonMatching1", 90, 90, "NonMatching1", 0, "NonMatching1");
            formations.Add(formation1);
            Formation formation2 = new Formation("NonMatching2", 90, 90, "NonMatching2", 0, "NonMatching2");
            formations.Add(formation2);

            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, NAME_FORMATION, NAME_MATCHER_SUBFORMATION);
            formationMatcher.AddIfPotentialMatchByName(formations);

            Assert.That(formationMatcher.PossibleFormations, Is.Empty);

            formationMatcher.CondensePotentialMatches();

            Assert.That(formationMatcher.MatchedFormation, Is.Null);
        }

        [Test]
        public void CondensePotentialMatches_Single_Potential_Match_Matches()
        {
            List<Formation> formations = new List<Formation>();
            Formation formation1 = new Formation(NAME_FORMATION + "1", 90, 90, NAME_OTHER_FORMATION + "1", 0, NAME_SUBFORMATION + "1");
            formations.Add(formation1);
            Formation formation2 = new Formation("NonMatching2", 90, 90, "NonMatching2", 0, "NonMatching2");
            formations.Add(formation2);

            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, NAME_FORMATION, NAME_MATCHER_SUBFORMATION);
            formationMatcher.AddIfPotentialMatchByName(formations);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(1));

            formationMatcher.CondensePotentialMatches();

            Assert.That(formationMatcher.MatchedFormation, Is.Not.Null);
            Assert.That(formationMatcher.MatchedFormation.Name, Is.EqualTo(formations[0].Name));
            Assert.That(formationMatcher.MatchedFormation.OtherName, Is.EqualTo(formations[0].OtherName));
            Assert.That(formationMatcher.MatchedFormation.SubFormationName, Is.EqualTo(formations[0].SubFormationName));
        }

        [Test]
        public void CondensePotentialMatches_Multiple_Potential_Matches_Does_Not_Match()
        {

            List<Formation> formations = new List<Formation>();
            Formation formation1 = new Formation(NAME_FORMATION + "1", 90, 90, NAME_OTHER_FORMATION + "1", 0, NAME_SUBFORMATION + "1");
            formations.Add(formation1);
            Formation formation2 = new Formation(NAME_FORMATION + "2", 90, 90, NAME_OTHER_FORMATION + "2", 0, NAME_SUBFORMATION + "2");
            formations.Add(formation2);
            Formation formation3 = new Formation(NAME_FORMATION + "3", 90, 90, NAME_OTHER_FORMATION + "3", 0, NAME_SUBFORMATION + "3");
            formations.Add(formation3);

            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, NAME_FORMATION, NAME_MATCHER_SUBFORMATION);
            formationMatcher.AddIfPotentialMatchByName(formations);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(3));

            formationMatcher.CondensePotentialMatches();

            Assert.That(formationMatcher.MatchedFormation, Is.Null);
        }

        [TestCase("FOO", ExpectedResult = "foo")]
        [TestCase("Mt Foo", ExpectedResult = "mount foo")]
        [TestCase("Foo Mtn", ExpectedResult = "foo mountain")]
        [TestCase("Foo Mtns", ExpectedResult = "foo mountains")]
        [TestCase("Foo PK", ExpectedResult = "foo peak")]
        [TestCase("Foo pks", ExpectedResult = "foo peaks")]
        [TestCase("Pt Foo", ExpectedResult = "point foo")]
        [TestCase("Foo Pts", ExpectedResult = "foo points")]
        [TestCase("Foo Pts.", ExpectedResult = "foo points")]
        [TestCase("Foo Pinnacle", ExpectedResult = "foo spire")]
        [TestCase("Foo Tower", ExpectedResult = "foo spire")]
        [TestCase("Foo Needle", ExpectedResult = "foo spire")]
        [TestCase("Foo Spire", ExpectedResult = "foo spire")]
        [TestCase("Foo #3", ExpectedResult = "foo 3")]
        [TestCase("Foo Number 3", ExpectedResult = "foo 3")]
        [TestCase("N Foo", ExpectedResult = "north foo")]
        [TestCase("S Foo", ExpectedResult = "south foo")]
        [TestCase("E. Foo", ExpectedResult = "east foo")]
        [TestCase("W. Foo", ExpectedResult = "west foo")]
        [TestCase("Pt. 13,149", ExpectedResult = "point 13149")]
        [TestCase("Pt. 13,149'", ExpectedResult = "point 13149")]
        [TestCase("Foo's Pinnacle", ExpectedResult = "foo spire")]
        [TestCase("Mountain of Foos", ExpectedResult = "mountain of foo")]
        [TestCase("Foo Mountains", ExpectedResult = "foo mountains")]
        [TestCase("Foo Peaks", ExpectedResult = "foo peaks")]
        [TestCase("Foo Points", ExpectedResult = "foo points")]
        [TestCase("Foo Pinnacles", ExpectedResult = "foo spires")]
        [TestCase("Foo Towers", ExpectedResult = "foo spires")]
        [TestCase("Foo Needles", ExpectedResult = "foo spires")]
        [TestCase("Foo Spires", ExpectedResult = "foo spires")]
        public string NormalizeGeographicName(string name)
        {
            return name.ApplyToIndividualWords(FormationMatcher.NormalizeGeographicName).ToLower();
        }


        [TestCase("foo", "FOO")]
        [TestCase("Mt Foo", "Mount Foo")]
        [TestCase("Foo Mtn", "Foo Mountain")]
        [TestCase("Foo Mtns", "Foo Mountains")]
        [TestCase("Foo PK", "Foo Peak")]
        [TestCase("Foo pks", "Foo Peaks")]
        [TestCase("Pt Foo", "Point Foo")]
        [TestCase("Foo Pts", "Foo Points")]
        [TestCase("Foo Pts.", "Foo Points")]
        [TestCase("Foo Pinnacle", "Foo Spire")]
        [TestCase("Foo Tower", "Foo Spire")]
        [TestCase("Foo Needle", "Foo Spire")]
        [TestCase("Foo Spire", "Foo Spire")]
        [TestCase("Foo #3", "Foo 3")]
        [TestCase("Foo Number 3", "Foo 3")]
        [TestCase("N Foo", "North Foo")]
        [TestCase("S Foo", "South Foo")]
        [TestCase("E. Foo", "East Foo")]
        [TestCase("W. Foo", "West Foo")]
        [TestCase("Pt. 13,149", "Point 13149")]
        [TestCase("Pt. 13,149'", "Point 13149")]
        [TestCase("Foo's Pinnacle", "Foos Pinnacle")]
        [TestCase("Mountain of Foos", "Mountain of Foo")]
        public void AddIfPotentialMatchByName_Potential_Match_By_FormationName_To_Name(string formationName, string name)
        {
            List<Formation> formations = new List<Formation>();
            Formation formation1 = new Formation(formationName + " 1", 90, 90, "Other" + formationName + "1", 0, "Sub" + formationName + "1");
            formations.Add(formation1);
            Formation formation2 = new Formation("NonMatching2", 90, 90, "NonMatching2", 0, "NonMatching2");
            formations.Add(formation2);
            Formation formation = new Formation(formationName, 90, 90, "Other" + formationName, 0, NAME_SUBFORMATION);
            formations.Add(formation);
            Formation formation3 = new Formation(formationName + " 3", 90, 90, "Other" + formationName + "3", 0, "Sub" + formationName + "3");
            formations.Add(formation3);

            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, name, NAME_MATCHER_SUBFORMATION);
            formationMatcher.AddIfPotentialMatchByName(formations);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(3));
        }



        [TestCase("foo", "FOO")]
        [TestCase("Mt Foo", "Mount Foo")]
        [TestCase("Foo Mtn", "Foo Mountain")]
        [TestCase("Foo Mtns", "Foo Mountains")]
        [TestCase("Foo PK", "Foo Peak")]
        [TestCase("Foo pks", "Foo Peaks")]
        [TestCase("Pt Foo", "Point Foo")]
        [TestCase("Foo Pts", "Foo Points")]
        [TestCase("Foo Pts.", "Foo Points")]
        [TestCase("Foo Pinnacle", "Foo Spire")]
        [TestCase("Foo Tower", "Foo Spire")]
        [TestCase("Foo Needle", "Foo Spire")]
        [TestCase("Foo Spire", "Foo Spire")]
        [TestCase("Foo #3", "Foo 3")]
        [TestCase("Foo Number 3", "Foo 3")]
        [TestCase("N Foo", "North Foo")]
        [TestCase("S Foo", "South Foo")]
        [TestCase("E. Foo", "East Foo")]
        [TestCase("W. Foo", "West Foo")]
        [TestCase("Pt. 13,149", "Point 13149")]
        [TestCase("Pt. 13,149'", "Point 13149")]
        [TestCase("Foo's Pinnacle", "Foos Pinnacle")]
        [TestCase("Mountain of Foos", "Mountain of Foo")]
        public void AddIfPotentialMatchByName_Potential_Match_By_FormationName_To_SubformationName(string formationName, string subFormationName)
        {
            List<Formation> formations = new List<Formation>();
            Formation formation1 = new Formation(formationName + " 1", 90, 90, NAME_OTHER_FORMATION + "1", 0, NAME_SUBFORMATION + "1");
            formations.Add(formation1);
            Formation formation2 = new Formation("NonMatching2", 90, 90, "NonMatching2", 0, "NonMatching2");
            formations.Add(formation2);
            Formation formation = new Formation(formationName, 90, 90);
            formations.Add(formation);
            Formation formation3 = new Formation(formationName + " 3", 90, 90, NAME_OTHER_FORMATION + "3", 0, NAME_SUBFORMATION + "3");
            formations.Add(formation3);

            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, NAME_MATCHER_FORMATION, subFormationName);
            formationMatcher.AddIfPotentialMatchByName(formations);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(3));
        }

        [TestCase("foo", "FOO")]
        [TestCase("Mt Foo", "Mount Foo")]
        [TestCase("Foo Mtn", "Foo Mountain")]
        [TestCase("Foo Mtns", "Foo Mountains")]
        [TestCase("Foo PK", "Foo Peak")]
        [TestCase("Foo pks", "Foo Peaks")]
        [TestCase("Pt Foo", "Point Foo")]
        [TestCase("Foo Pts", "Foo Points")]
        [TestCase("Foo Pts.", "Foo Points")]
        [TestCase("Foo Pinnacle", "Foo Spire")]
        [TestCase("Foo Tower", "Foo Spire")]
        [TestCase("Foo Needle", "Foo Spire")]
        [TestCase("Foo Spire", "Foo Spire")]
        [TestCase("Foo #3", "Foo 3")]
        [TestCase("Foo Number 3", "Foo 3")]
        [TestCase("N Foo", "North Foo")]
        [TestCase("S Foo", "South Foo")]
        [TestCase("E. Foo", "East Foo")]
        [TestCase("W. Foo", "West Foo")]
        [TestCase("Pt. 13,149", "Point 13149")]
        [TestCase("Pt. 13,149'", "Point 13149")]
        [TestCase("Foo's Pinnacle", "Foos Pinnacle")]
        [TestCase("Mountain of Foos", "Mountain of Foo")]
        public void AddIfPotentialMatchByName_Potential_Match_By_OtherName_To_Name(string otherName, string name)
        {
            List<Formation> formations = new List<Formation>();
            Formation formation1 = new Formation(NAME_FORMATION + "1", 90, 90, otherName + " 1", 0, NAME_SUBFORMATION + "1");
            formations.Add(formation1);
            Formation formation2 = new Formation("NonMatching2", 90, 90, "NonMatching2", 0, "NonMatching2");
            formations.Add(formation2);
            Formation formation = new Formation("Mt Ericsson", 90, 90, otherName);
            formations.Add(formation);
            Formation formation3 = new Formation(NAME_FORMATION + "3", 90, 90, otherName + " 3", 0, NAME_SUBFORMATION + "3");
            formations.Add(formation3);

            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, name, NAME_MATCHER_SUBFORMATION);
            formationMatcher.AddIfPotentialMatchByName(formations);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(3));
            
        }

        [TestCase("foo", "FOO")]
        [TestCase("Mt Foo", "Mount Foo")]
        [TestCase("Foo Mtn", "Foo Mountain")]
        [TestCase("Foo Mtns", "Foo Mountains")]
        [TestCase("Foo PK", "Foo Peak")]
        [TestCase("Foo pks", "Foo Peaks")]
        [TestCase("Pt Foo", "Point Foo")]
        [TestCase("Foo Pts", "Foo Points")]
        [TestCase("Foo Pts.", "Foo Points")]
        [TestCase("Foo Pinnacle", "Foo Spire")]
        [TestCase("Foo Tower", "Foo Spire")]
        [TestCase("Foo Needle", "Foo Spire")]
        [TestCase("Foo Spire", "Foo Spire")]
        [TestCase("Foo #3", "Foo 3")]
        [TestCase("Foo Number 3", "Foo 3")]
        [TestCase("N Foo", "North Foo")]
        [TestCase("S Foo", "South Foo")]
        [TestCase("E. Foo", "East Foo")]
        [TestCase("W. Foo", "West Foo")]
        [TestCase("Pt. 13,149", "Point 13149")]
        [TestCase("Pt. 13,149'", "Point 13149")]
        [TestCase("Foo's Pinnacle", "Foos Pinnacle")]
        [TestCase("Mountain of Foos", "Mountain of Foo")]
        public void AddIfPotentialMatchByName_Potential_Match_By_OtherName_To_SubformationName(string otherName, string subFormationName)
        {
            List<Formation> formations = new List<Formation>();
            Formation formation1 = new Formation(NAME_FORMATION + "1", 90, 90, otherName + " 1", 0, NAME_SUBFORMATION + "1");
            formations.Add(formation1);
            Formation formation2 = new Formation("NonMatching2", 90, 90, "NonMatching2", 0, "NonMatching2");
            formations.Add(formation2);
            Formation formation = new Formation("Mt Ericsson", 90, 90, otherName);
            formations.Add(formation);
            Formation formation3 = new Formation(NAME_FORMATION + "3", 90, 90, otherName + " 3", 0, NAME_SUBFORMATION + "3");
            formations.Add(formation3);

            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, NAME_MATCHER_FORMATION, subFormationName);
            formationMatcher.AddIfPotentialMatchByName(formations);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(3));
        }

        [TestCase("foo", "FOO")]
        [TestCase("Mt Foo", "Mount Foo")]
        [TestCase("Foo Mtn", "Foo Mountain")]
        [TestCase("Foo Mtns", "Foo Mountains")]
        [TestCase("Foo PK", "Foo Peak")]
        [TestCase("Foo pks", "Foo Peaks")]
        [TestCase("Pt Foo", "Point Foo")]
        [TestCase("Foo Pts", "Foo Points")]
        [TestCase("Foo Pts.", "Foo Points")]
        [TestCase("Foo Pinnacle", "Foo Spire")]
        [TestCase("Foo Tower", "Foo Spire")]
        [TestCase("Foo Needle", "Foo Spire")]
        [TestCase("Foo Spire", "Foo Spire")]
        [TestCase("Foo #3", "Foo 3")]
        [TestCase("Foo Number 3", "Foo 3")]
        [TestCase("N Foo", "North Foo")]
        [TestCase("S Foo", "South Foo")]
        [TestCase("E. Foo", "East Foo")]
        [TestCase("W. Foo", "West Foo")]
        [TestCase("Pt. 13,149", "Point 13149")]
        [TestCase("Pt. 13,149'", "Point 13149")]
        [TestCase("Foo's Pinnacle", "Foos Pinnacle")]
        [TestCase("Mountain of Foos", "Mountain of Foo")]
        public void AddIfPotentialMatchByName_Potential_Match_By_SubformationName_To_Name(string formationSubformationName, string name)
        {
            List<Formation> formations = new List<Formation>();
            Formation formation1 = new Formation(NAME_FORMATION + "1", 90, 90, NAME_OTHER_FORMATION + "1", 0, formationSubformationName + " 1");
            formations.Add(formation1);
            Formation formation2 = new Formation("NonMatching2", 90, 90, "NonMatching2", 0, "NonMatching2");
            formations.Add(formation2);
            Formation formation = new Formation("Mt Ericsson", 90, 90, "Ericsson Crags", 0, formationSubformationName);
            formations.Add(formation);
            Formation formation3 = new Formation(NAME_FORMATION + "3", 90, 90, NAME_OTHER_FORMATION + "3", 0, formationSubformationName + " 3");
            formations.Add(formation3);

            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, name, NAME_MATCHER_SUBFORMATION);
            formationMatcher.AddIfPotentialMatchByName(formations);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(3));
        }

        [TestCase("foo", "FOO")]
        [TestCase("Mt Foo", "Mount Foo")]
        [TestCase("Foo Mtn", "Foo Mountain")]
        [TestCase("Foo Mtns", "Foo Mountains")]
        [TestCase("Foo PK", "Foo Peak")]
        [TestCase("Foo pks", "Foo Peaks")]
        [TestCase("Pt Foo", "Point Foo")]
        [TestCase("Foo Pts", "Foo Points")]
        [TestCase("Foo Pts.", "Foo Points")]
        [TestCase("Foo Pinnacle", "Foo Spire")]
        [TestCase("Foo Tower", "Foo Spire")]
        [TestCase("Foo Needle", "Foo Spire")]
        [TestCase("Foo Spire", "Foo Spire")]
        [TestCase("Foo #3", "Foo 3")]
        [TestCase("Foo Number 3", "Foo 3")]
        [TestCase("N Foo", "North Foo")]
        [TestCase("S Foo", "South Foo")]
        [TestCase("E. Foo", "East Foo")]
        [TestCase("W. Foo", "West Foo")]
        [TestCase("Pt. 13,149", "Point 13149")]
        [TestCase("Pt. 13,149'", "Point 13149")]
        [TestCase("Foo's Pinnacle", "Foos Pinnacle")]
        [TestCase("Mountain of Foos", "Mountain of Foo")]
        public void AddIfPotentialMatchByName_Potential_Match_By_SubformationName_To_SubformationName(string formationSubformationName, string subFormationName)
        {
            List<Formation> formations = new List<Formation>();
            Formation formation1 = new Formation(NAME_FORMATION + "1", 90, 90, NAME_OTHER_FORMATION + "1", 0, formationSubformationName + " 1");
            formations.Add(formation1);
            Formation formation2 = new Formation("NonMatching2", 90, 90, "NonMatching2", 0, "NonMatching2");
            formations.Add(formation2);
            Formation formation = new Formation("Mt Ericsson", 90, 90, "Ericsson Crags", 0, formationSubformationName);
            formations.Add(formation);
            Formation formation3 = new Formation(NAME_FORMATION + "3", 90, 90, NAME_OTHER_FORMATION + "3", 0, formationSubformationName + " 3");
            formations.Add(formation3);

            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, NAME_MATCHER_FORMATION, subFormationName);
            formationMatcher.AddIfPotentialMatchByName(formations);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(3));
        }

        
        [TestCase("foo", "FOO")]
        [TestCase("Mt Foo", "Mount Foo")]
        [TestCase("Foo Mtn", "Foo Mountain")]
        [TestCase("Foo Mtns", "Foo Mountains")]
        [TestCase("Foo PK", "Foo Peak")]
        [TestCase("Foo pks", "Foo Peaks")]
        [TestCase("Pt Foo", "Point Foo")]
        [TestCase("Foo Pts", "Foo Points")]
        [TestCase("Foo Pts.", "Foo Points")]
        [TestCase("Foo Pinnacle", "Foo Spire")]
        [TestCase("Foo Tower", "Foo Spire")]
        [TestCase("Foo Needle", "Foo Spire")]
        [TestCase("Foo Spire", "Foo Spire")]
        [TestCase("Foo #3", "Foo 3")]
        [TestCase("Foo Number 3", "Foo 3")]
        [TestCase("N Foo", "North Foo")]
        [TestCase("S Foo", "South Foo")]
        [TestCase("E. Foo", "East Foo")]
        [TestCase("W. Foo", "West Foo")]
        [TestCase("Pt. 13,149", "Point 13149")]
        [TestCase("Pt. 13,149'", "Point 13149")]
        [TestCase("Foo's Pinnacle", "Foos Pinnacle")]
        [TestCase("Mountain of Foos", "Mountain of Foo")]
        public void CondensePotentialMatches_Multiple_Potential_Matches_Matches_By_FormationName_To_Name(string formationName, string name)
        {
            List<Formation> formations = new List<Formation>();
            Formation formation1 = new Formation(formationName + " 1", 90, 90, "Other" + formationName + "1", 0, "Sub" + formationName + "1");
            formations.Add(formation1);
            Formation formation2 = new Formation("NonMatching2", 90, 90, "NonMatching2", 0, "NonMatching2");
            formations.Add(formation2);
            Formation formation = new Formation(formationName, 90, 90, "Other" + formationName, 0, NAME_SUBFORMATION);
            formations.Add(formation);
            Formation formation3 = new Formation(formationName + " 3", 90, 90, "Other" + formationName + "3", 0, "Sub" + formationName + "3");
            formations.Add(formation3);

            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, name, NAME_MATCHER_SUBFORMATION);
            formationMatcher.AddIfPotentialMatchByName(formations);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(3));

            formationMatcher.CondensePotentialMatches();

            Assert.That(formationMatcher.MatchedFormation, Is.Not.Null);
            Assert.That(formationMatcher.MatchedFormation.Name, Is.EqualTo(formations[2].Name));
            Assert.That(formationMatcher.MatchedFormation.OtherName, Is.EqualTo(formations[2].OtherName));
            Assert.That(formationMatcher.MatchedFormation.SubFormationName, Is.EqualTo(formations[2].SubFormationName));
        }

        [TestCase("foo", "FOO")]
        [TestCase("Mt Foo", "Mount Foo")]
        [TestCase("Foo Mtn", "Foo Mountain")]
        [TestCase("Foo Mtns", "Foo Mountains")]
        [TestCase("Foo PK", "Foo Peak")]
        [TestCase("Foo pks", "Foo Peaks")]
        [TestCase("Pt Foo", "Point Foo")]
        [TestCase("Foo Pts", "Foo Points")]
        [TestCase("Foo Pts.", "Foo Points")]
        [TestCase("Foo Pinnacle", "Foo Spire")]
        [TestCase("Foo Tower", "Foo Spire")]
        [TestCase("Foo Needle", "Foo Spire")]
        [TestCase("Foo Spire", "Foo Spire")]
        [TestCase("Foo #3", "Foo 3")]
        [TestCase("Foo Number 3", "Foo 3")]
        [TestCase("N Foo", "North Foo")]
        [TestCase("S Foo", "South Foo")]
        [TestCase("E. Foo", "East Foo")]
        [TestCase("W. Foo", "West Foo")]
        [TestCase("Pt. 13,149", "Point 13149")]
        [TestCase("Pt. 13,149'", "Point 13149")]
        [TestCase("Foo's Pinnacle", "Foos Pinnacle")]
        [TestCase("Mountain of Foos", "Mountain of Foo")]
        public void CondensePotentialMatches_Multiple_Potential_Matches_Matches_By_FormationName_To_Name_When_SubFormation_Is_Blank(string formationName, string name)
        {
            List<Formation> formations = new List<Formation>();
            Formation formation1 = new Formation(formationName + " 1", 90, 90, "Other" + formationName + "1", 0, "Sub" + formationName + "1");
            formations.Add(formation1);
            Formation formation2 = new Formation("NonMatching2", 90, 90, "NonMatching2", 0, "NonMatching2");
            formations.Add(formation2);
            Formation formation = new Formation(formationName, 90, 90, "Other" + formationName, 0, NAME_SUBFORMATION);
            formations.Add(formation);
            Formation formation3 = new Formation(formationName + " 3", 90, 90, "Other" + formationName + "3", 0, "Sub" + formationName + "3");
            formations.Add(formation3);

            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, name);
            formationMatcher.AddIfPotentialMatchByName(formations);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(3));

            formationMatcher.CondensePotentialMatches();

            Assert.That(formationMatcher.MatchedFormation, Is.Not.Null);
            Assert.That(formationMatcher.MatchedFormation.Name, Is.EqualTo(formations[2].Name));
            Assert.That(formationMatcher.MatchedFormation.OtherName, Is.EqualTo(formations[2].OtherName));
            Assert.That(formationMatcher.MatchedFormation.SubFormationName, Is.EqualTo(formations[2].SubFormationName));
        }

        [TestCase("foo", "FOO")]
        [TestCase("Mt Foo", "Mount Foo")]
        [TestCase("Foo Mtn", "Foo Mountain")]
        [TestCase("Foo Mtns", "Foo Mountains")]
        [TestCase("Foo PK", "Foo Peak")]
        [TestCase("Foo pks", "Foo Peaks")]
        [TestCase("Pt Foo", "Point Foo")]
        [TestCase("Foo Pts", "Foo Points")]
        [TestCase("Foo Pts.", "Foo Points")]
        [TestCase("Foo Pinnacle", "Foo Spire")]
        [TestCase("Foo Tower", "Foo Spire")]
        [TestCase("Foo Needle", "Foo Spire")]
        [TestCase("Foo Spire", "Foo Spire")]
        [TestCase("Foo #3", "Foo 3")]
        [TestCase("Foo Number 3", "Foo 3")]
        [TestCase("N Foo", "North Foo")]
        [TestCase("S Foo", "South Foo")]
        [TestCase("E. Foo", "East Foo")]
        [TestCase("W. Foo", "West Foo")]
        [TestCase("Pt. 13,149", "Point 13149")]
        [TestCase("Pt. 13,149'", "Point 13149")]
        [TestCase("Foo's Pinnacle", "Foos Pinnacle")]
        [TestCase("Mountain of Foos", "Mountain of Foo")]
        public void CondensePotentialMatches_Multiple_Potential_Matches_Matches_By_FormationName_To_SubformationName(string formationName, string subFormationName)
        {
            List<Formation> formations = new List<Formation>();
            Formation formation1 = new Formation(formationName + " 1", 90, 90, NAME_OTHER_FORMATION + "1", 0, NAME_SUBFORMATION + "1");
            formations.Add(formation1);
            Formation formation2 = new Formation("NonMatching2", 90, 90, "NonMatching2", 0, "NonMatching2");
            formations.Add(formation2);
            Formation formation = new Formation(formationName, 90, 90);
            formations.Add(formation);
            Formation formation3 = new Formation(formationName + " 3", 90, 90, NAME_OTHER_FORMATION + "3", 0, NAME_SUBFORMATION + "3");
            formations.Add(formation3);

            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, NAME_MATCHER_FORMATION, subFormationName);
            formationMatcher.AddIfPotentialMatchByName(formations);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(3));

            formationMatcher.CondensePotentialMatches();

            Assert.That(formationMatcher.MatchedFormation, Is.Not.Null);
            Assert.That(formationMatcher.MatchedFormation.Name, Is.EqualTo(formations[2].Name));
            Assert.That(formationMatcher.MatchedFormation.OtherName, Is.EqualTo(formations[2].OtherName));
            Assert.That(formationMatcher.MatchedFormation.SubFormationName, Is.EqualTo(formations[2].SubFormationName));
        }

        [TestCase("foo", "FOO")]
        [TestCase("Mt Foo", "Mount Foo")]
        [TestCase("Foo Mtn", "Foo Mountain")]
        [TestCase("Foo Mtns", "Foo Mountains")]
        [TestCase("Foo PK", "Foo Peak")]
        [TestCase("Foo pks", "Foo Peaks")]
        [TestCase("Pt Foo", "Point Foo")]
        [TestCase("Foo Pts", "Foo Points")]
        [TestCase("Foo Pts.", "Foo Points")]
        [TestCase("Foo Pinnacle", "Foo Spire")]
        [TestCase("Foo Tower", "Foo Spire")]
        [TestCase("Foo Needle", "Foo Spire")]
        [TestCase("Foo Spire", "Foo Spire")]
        [TestCase("Foo #3", "Foo 3")]
        [TestCase("Foo Number 3", "Foo 3")]
        [TestCase("N Foo", "North Foo")]
        [TestCase("S Foo", "South Foo")]
        [TestCase("E. Foo", "East Foo")]
        [TestCase("W. Foo", "West Foo")]
        [TestCase("Pt. 13,149", "Point 13149")]
        [TestCase("Pt. 13,149'", "Point 13149")]
        [TestCase("Foo's Pinnacle", "Foos Pinnacle")]
        [TestCase("Mountain of Foos", "Mountain of Foo")]
        public void CondensePotentialMatches_Multiple_Potential_Matches_Matches_By_OtherName_To_Name(string otherName, string name)
        {
            List<Formation> formations = new List<Formation>();
            Formation formation1 = new Formation(NAME_FORMATION + "1", 90, 90, otherName + " 1", 0, NAME_SUBFORMATION + "1");
            formations.Add(formation1);
            Formation formation2 = new Formation("NonMatching2", 90, 90, "NonMatching2", 0, "NonMatching2");
            formations.Add(formation2);
            Formation formation = new Formation("Mt Ericsson", 90, 90, otherName);
            formations.Add(formation);
            Formation formation3 = new Formation(NAME_FORMATION + "3", 90, 90, otherName + " 3", 0, NAME_SUBFORMATION + "3");
            formations.Add(formation3);

            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, name, NAME_MATCHER_SUBFORMATION);
            formationMatcher.AddIfPotentialMatchByName(formations);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(3));

            formationMatcher.CondensePotentialMatches();

            Assert.That(formationMatcher.MatchedFormation, Is.Not.Null);
            Assert.That(formationMatcher.MatchedFormation.Name, Is.EqualTo(formations[2].Name));
            Assert.That(formationMatcher.MatchedFormation.OtherName, Is.EqualTo(formations[2].OtherName));
            Assert.That(formationMatcher.MatchedFormation.SubFormationName, Is.EqualTo(formations[2].SubFormationName));
        }

        [TestCase("foo", "FOO")]
        [TestCase("Mt Foo", "Mount Foo")]
        [TestCase("Foo Mtn", "Foo Mountain")]
        [TestCase("Foo Mtns", "Foo Mountains")]
        [TestCase("Foo PK", "Foo Peak")]
        [TestCase("Foo pks", "Foo Peaks")]
        [TestCase("Pt Foo", "Point Foo")]
        [TestCase("Foo Pts", "Foo Points")]
        [TestCase("Foo Pts.", "Foo Points")]
        [TestCase("Foo Pinnacle", "Foo Spire")]
        [TestCase("Foo Tower", "Foo Spire")]
        [TestCase("Foo Needle", "Foo Spire")]
        [TestCase("Foo Spire", "Foo Spire")]
        [TestCase("Foo #3", "Foo 3")]
        [TestCase("Foo Number 3", "Foo 3")]
        [TestCase("N Foo", "North Foo")]
        [TestCase("S Foo", "South Foo")]
        [TestCase("E. Foo", "East Foo")]
        [TestCase("W. Foo", "West Foo")]
        [TestCase("Pt. 13,149", "Point 13149")]
        [TestCase("Pt. 13,149'", "Point 13149")]
        [TestCase("Foo's Pinnacle", "Foos Pinnacle")]
        [TestCase("Mountain of Foos", "Mountain of Foo")]
        public void CondensePotentialMatches_Multiple_Potential_Matches_Matches_By_OtherName_To_Name_When_SubFormation_Is_Blank(string otherName, string name)
        {
            List<Formation> formations = new List<Formation>();
            Formation formation1 = new Formation(NAME_FORMATION + "1", 90, 90, otherName + " 1", 0, NAME_SUBFORMATION + "1");
            formations.Add(formation1);
            Formation formation2 = new Formation("NonMatching2", 90, 90, "NonMatching2", 0, "NonMatching2");
            formations.Add(formation2);
            Formation formation = new Formation("Mt Ericsson", 90, 90, otherName);
            formations.Add(formation);
            Formation formation3 = new Formation(NAME_FORMATION + "3", 90, 90, otherName + " 3", 0, NAME_SUBFORMATION + "3");
            formations.Add(formation3);

            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, name);
            formationMatcher.AddIfPotentialMatchByName(formations);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(3));

            formationMatcher.CondensePotentialMatches();

            Assert.That(formationMatcher.MatchedFormation, Is.Not.Null);
            Assert.That(formationMatcher.MatchedFormation.Name, Is.EqualTo(formations[2].Name));
            Assert.That(formationMatcher.MatchedFormation.OtherName, Is.EqualTo(formations[2].OtherName));
            Assert.That(formationMatcher.MatchedFormation.SubFormationName, Is.EqualTo(formations[2].SubFormationName));
        }

        [TestCase("foo", "FOO")]
        [TestCase("Mt Foo", "Mount Foo")]
        [TestCase("Foo Mtn", "Foo Mountain")]
        [TestCase("Foo Mtns", "Foo Mountains")]
        [TestCase("Foo PK", "Foo Peak")]
        [TestCase("Foo pks", "Foo Peaks")]
        [TestCase("Pt Foo", "Point Foo")]
        [TestCase("Foo Pts", "Foo Points")]
        [TestCase("Foo Pts.", "Foo Points")]
        [TestCase("Foo Pinnacle", "Foo Spire")]
        [TestCase("Foo Tower", "Foo Spire")]
        [TestCase("Foo Needle", "Foo Spire")]
        [TestCase("Foo Spire", "Foo Spire")]
        [TestCase("Foo #3", "Foo 3")]
        [TestCase("Foo Number 3", "Foo 3")]
        [TestCase("N Foo", "North Foo")]
        [TestCase("S Foo", "South Foo")]
        [TestCase("E. Foo", "East Foo")]
        [TestCase("W. Foo", "West Foo")]
        [TestCase("Pt. 13,149", "Point 13149")]
        [TestCase("Pt. 13,149'", "Point 13149")]
        [TestCase("Foo's Pinnacle", "Foos Pinnacle")]
        [TestCase("Mountain of Foos", "Mountain of Foo")]
        public void CondensePotentialMatches_Multiple_Potential_Matches_Matches_By_OtherName_To_SubformationName(string otherName, string subFormationName)
        {
            List<Formation> formations = new List<Formation>();
            Formation formation1 = new Formation(NAME_FORMATION + "1", 90, 90, otherName + " 1", 0, NAME_SUBFORMATION + "1");
            formations.Add(formation1);
            Formation formation2 = new Formation("NonMatching2", 90, 90, "NonMatching2", 0, "NonMatching2");
            formations.Add(formation2);
            Formation formation = new Formation("Mt Ericsson", 90, 90, otherName);
            formations.Add(formation);
            Formation formation3 = new Formation(NAME_FORMATION + "3", 90, 90, otherName + " 3", 0, NAME_SUBFORMATION + "3");
            formations.Add(formation3);

            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, NAME_MATCHER_FORMATION, subFormationName);
            formationMatcher.AddIfPotentialMatchByName(formations);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(3));

            formationMatcher.CondensePotentialMatches();

            Assert.That(formationMatcher.MatchedFormation, Is.Not.Null);
            Assert.That(formationMatcher.MatchedFormation.Name, Is.EqualTo(formations[2].Name));
            Assert.That(formationMatcher.MatchedFormation.OtherName, Is.EqualTo(formations[2].OtherName));
            Assert.That(formationMatcher.MatchedFormation.SubFormationName, Is.EqualTo(formations[2].SubFormationName));
        }

        [TestCase("foo", "FOO")]
        [TestCase("Mt Foo", "Mount Foo")]
        [TestCase("Foo Mtn", "Foo Mountain")]
        [TestCase("Foo Mtns", "Foo Mountains")]
        [TestCase("Foo PK", "Foo Peak")]
        [TestCase("Foo pks", "Foo Peaks")]
        [TestCase("Pt Foo", "Point Foo")]
        [TestCase("Foo Pts", "Foo Points")]
        [TestCase("Foo Pts.", "Foo Points")]
        [TestCase("Foo Pinnacle", "Foo Spire")]
        [TestCase("Foo Tower", "Foo Spire")]
        [TestCase("Foo Needle", "Foo Spire")]
        [TestCase("Foo Spire", "Foo Spire")]
        [TestCase("Foo #3", "Foo 3")]
        [TestCase("Foo Number 3", "Foo 3")]
        [TestCase("N Foo", "North Foo")]
        [TestCase("S Foo", "South Foo")]
        [TestCase("E. Foo", "East Foo")]
        [TestCase("W. Foo", "West Foo")]
        [TestCase("Pt. 13,149", "Point 13149")]
        [TestCase("Pt. 13,149'", "Point 13149")]
        [TestCase("Foo's Pinnacle", "Foos Pinnacle")]
        [TestCase("Mountain of Foos", "Mountain of Foo")]
        public void CondensePotentialMatches_Multiple_Potential_Matches_Matches_By_SubformationName_To_Name_When_SubFormation_Is_Blank(string formationSubformationName, string name)
        {
            List<Formation> formations = new List<Formation>();
            Formation formation1 = new Formation(NAME_FORMATION + "1", 90, 90, NAME_OTHER_FORMATION + "1", 0, formationSubformationName + " 1");
            formations.Add(formation1);
            Formation formation2 = new Formation("NonMatching2", 90, 90, "NonMatching2", 0, "NonMatching2");
            formations.Add(formation2);
            Formation formation = new Formation("Mt Ericsson", 90, 90, "Ericsson Crags", 0, formationSubformationName);
            formations.Add(formation);
            Formation formation3 = new Formation(NAME_FORMATION + "3", 90, 90, NAME_OTHER_FORMATION + "3", 0, formationSubformationName + " 3");
            formations.Add(formation3);

            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, name, NAME_MATCHER_SUBFORMATION);
            formationMatcher.AddIfPotentialMatchByName(formations);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(3));

            formationMatcher.CondensePotentialMatches();

            Assert.That(formationMatcher.MatchedFormation, Is.Not.Null);
            Assert.That(formationMatcher.MatchedFormation.Name, Is.EqualTo(formations[2].Name));
            Assert.That(formationMatcher.MatchedFormation.OtherName, Is.EqualTo(formations[2].OtherName));
            Assert.That(formationMatcher.MatchedFormation.SubFormationName, Is.EqualTo(formations[2].SubFormationName));
        }

        [TestCase("foo", "FOO")]
        [TestCase("Mt Foo", "Mount Foo")]
        [TestCase("Foo Mtn", "Foo Mountain")]
        [TestCase("Foo Mtns", "Foo Mountains")]
        [TestCase("Foo PK", "Foo Peak")]
        [TestCase("Foo pks", "Foo Peaks")]
        [TestCase("Pt Foo", "Point Foo")]
        [TestCase("Foo Pts", "Foo Points")]
        [TestCase("Foo Pts.", "Foo Points")]
        [TestCase("Foo Pinnacle", "Foo Spire")]
        [TestCase("Foo Tower", "Foo Spire")]
        [TestCase("Foo Needle", "Foo Spire")]
        [TestCase("Foo Spire", "Foo Spire")]
        [TestCase("Foo #3", "Foo 3")]
        [TestCase("Foo Number 3", "Foo 3")]
        [TestCase("N Foo", "North Foo")]
        [TestCase("S Foo", "South Foo")]
        [TestCase("E. Foo", "East Foo")]
        [TestCase("W. Foo", "West Foo")]
        [TestCase("Pt. 13,149", "Point 13149")]
        [TestCase("Pt. 13,149'", "Point 13149")]
        [TestCase("Foo's Pinnacle", "Foos Pinnacle")]
        [TestCase("Mountain of Foos", "Mountain of Foo")]
        public void CondensePotentialMatches_Multiple_Potential_Matches_Matches_By_SubformationName_To_Name(string formationSubformationName, string name)
        {
            List<Formation> formations = new List<Formation>();
            Formation formation1 = new Formation(NAME_FORMATION + "1", 90, 90, NAME_OTHER_FORMATION + "1", 0, formationSubformationName + " 1");
            formations.Add(formation1);
            Formation formation2 = new Formation("NonMatching2", 90, 90, "NonMatching2", 0, "NonMatching2");
            formations.Add(formation2);
            Formation formation = new Formation("Mt Ericsson", 90, 90, "Ericsson Crags", 0, formationSubformationName);
            formations.Add(formation);
            Formation formation3 = new Formation(NAME_FORMATION + "3", 90, 90, NAME_OTHER_FORMATION + "3", 0, formationSubformationName + " 3");
            formations.Add(formation3);

            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, name);
            formationMatcher.AddIfPotentialMatchByName(formations);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(3));

            formationMatcher.CondensePotentialMatches();

            Assert.That(formationMatcher.MatchedFormation, Is.Not.Null);
            Assert.That(formationMatcher.MatchedFormation.Name, Is.EqualTo(formations[2].Name));
            Assert.That(formationMatcher.MatchedFormation.OtherName, Is.EqualTo(formations[2].OtherName));
            Assert.That(formationMatcher.MatchedFormation.SubFormationName, Is.EqualTo(formations[2].SubFormationName));
        }

        [TestCase("foo", "FOO")]
        [TestCase("Mt Foo", "Mount Foo")]
        [TestCase("Foo Mtn", "Foo Mountain")]
        [TestCase("Foo Mtns", "Foo Mountains")]
        [TestCase("Foo PK", "Foo Peak")]
        [TestCase("Foo pks", "Foo Peaks")]
        [TestCase("Pt Foo", "Point Foo")]
        [TestCase("Foo Pts", "Foo Points")]
        [TestCase("Foo Pts.", "Foo Points")]
        [TestCase("Foo Pinnacle", "Foo Spire")]
        [TestCase("Foo Tower", "Foo Spire")]
        [TestCase("Foo Needle", "Foo Spire")]
        [TestCase("Foo Spire", "Foo Spire")]
        [TestCase("Foo #3", "Foo 3")]
        [TestCase("Foo Number 3", "Foo 3")]
        [TestCase("N Foo", "North Foo")]
        [TestCase("S Foo", "South Foo")]
        [TestCase("E. Foo", "East Foo")]
        [TestCase("W. Foo", "West Foo")]
        [TestCase("Pt. 13,149", "Point 13149")]
        [TestCase("Pt. 13,149'", "Point 13149")]
        [TestCase("Foo's Pinnacle", "Foos Pinnacle")]
        [TestCase("Mountain of Foos", "Mountain of Foo")]
        public void CondensePotentialMatches_Multiple_Potential_Matches_Matches_By_SubformationName_To_SubformationName(string formationSubformationName,  string subFormationName)
        {
            List<Formation> formations = new List<Formation>();
            Formation formation1 = new Formation(NAME_FORMATION + "1", 90, 90, NAME_OTHER_FORMATION + "1", 0, formationSubformationName + " 1");
            formations.Add(formation1);
            Formation formation2 = new Formation("NonMatching2", 90, 90, "NonMatching2", 0, "NonMatching2");
            formations.Add(formation2);
            Formation formation = new Formation("Mt Foo", 90, 90, "Foo Crags", 0, formationSubformationName);
            formations.Add(formation);
            Formation formation3 = new Formation(NAME_FORMATION + "3", 90, 90, NAME_OTHER_FORMATION + "3", 0, formationSubformationName + " 3");
            formations.Add(formation3);

            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, NAME_MATCHER_FORMATION, subFormationName);
            formationMatcher.AddIfPotentialMatchByName(formations);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(3));

            formationMatcher.CondensePotentialMatches();

            Assert.That(formationMatcher.MatchedFormation, Is.Not.Null);
            Assert.That(formationMatcher.MatchedFormation.Name, Is.EqualTo(formations[2].Name));
            Assert.That(formationMatcher.MatchedFormation.OtherName, Is.EqualTo(formations[2].OtherName));
            Assert.That(formationMatcher.MatchedFormation.SubFormationName, Is.EqualTo(formations[2].SubFormationName));
        }
        
        [TestCase("foo", "FOO")]
        [TestCase("Mt Foo", "Mount Foo")]
        [TestCase("Foo Mtn", "Foo Mountain")]
        [TestCase("Foo Mtns", "Foo Mountains")]
        [TestCase("Foo PK", "Foo Peak")]
        [TestCase("Foo pks", "Foo Peaks")]
        [TestCase("Pt Foo", "Point Foo")]
        [TestCase("Foo Pts", "Foo Points")]
        [TestCase("Foo Pts.", "Foo Points")]
        [TestCase("Foo Pinnacle", "Foo Spire")]
        [TestCase("Foo Tower", "Foo Spire")]
        [TestCase("Foo Needle", "Foo Spire")]
        [TestCase("Foo Spire", "Foo Spire")]
        [TestCase("Foo #3", "Foo 3")]
        [TestCase("Foo Number 3", "Foo 3")]
        [TestCase("N Foo", "North Foo")]
        [TestCase("S Foo", "South Foo")]
        [TestCase("E. Foo", "East Foo")]
        [TestCase("W. Foo", "West Foo")]
        [TestCase("Pt. 13,149", "Point 13149")]
        [TestCase("Pt. 13,149'", "Point 13149")]
        [TestCase("Foo's Pinnacle", "Foos Pinnacle")]
        [TestCase("Mountain of Foos", "Mountain of Foo")]
        public void CondensePotentialMatches_Multiple_Potential_Matches_Matches_By_SubformationName_To_SubformationName_And_Name(string formationSubformationName, string subFormationName)
        {
            List<Formation> formations = new List<Formation>();
            Formation formation1 = new Formation(NAME_FORMATION + "1", 90, 90, NAME_OTHER_FORMATION + "1", 0, formationSubformationName + " 1");
            formations.Add(formation1);
            Formation formation2 = new Formation("NonMatching2", 90, 90, "NonMatching2", 0, "NonMatching2");
            formations.Add(formation2);
            Formation formation = new Formation("Mt Foo", 90, 90, "Moo Crags", 0, formationSubformationName);
            formations.Add(formation);
            Formation formationAlmostMatch = new Formation("Mt Moo", 90, 90, "Moo Crags", 0, formationSubformationName);
            formations.Add(formationAlmostMatch);
            Formation formation3 = new Formation(NAME_FORMATION + "3", 90, 90, NAME_OTHER_FORMATION + "3", 0, formationSubformationName + " 3");
            formations.Add(formation3);

            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, "Mt Foo", subFormationName);
            formationMatcher.AddIfPotentialMatchByName(formations);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(4));

            formationMatcher.CondensePotentialMatches();

            Assert.That(formationMatcher.MatchedFormation, Is.Not.Null);
            Assert.That(formationMatcher.MatchedFormation.Name, Is.EqualTo(formations[2].Name));
            Assert.That(formationMatcher.MatchedFormation.OtherName, Is.EqualTo(formations[2].OtherName));
            Assert.That(formationMatcher.MatchedFormation.SubFormationName, Is.EqualTo(formations[2].SubFormationName));
        }
        
        [TestCase("foo", "FOO")]
        [TestCase("Mt Foo", "Mount Foo")]
        [TestCase("Foo Mtn", "Foo Mountain")]
        [TestCase("Foo Mtns", "Foo Mountains")]
        [TestCase("Foo PK", "Foo Peak")]
        [TestCase("Foo pks", "Foo Peaks")]
        [TestCase("Pt Foo", "Point Foo")]
        [TestCase("Foo Pts", "Foo Points")]
        [TestCase("Foo Pts.", "Foo Points")]
        [TestCase("Foo Pinnacle", "Foo Spire")]
        [TestCase("Foo Tower", "Foo Spire")]
        [TestCase("Foo Needle", "Foo Spire")]
        [TestCase("Foo Spire", "Foo Spire")]
        [TestCase("Foo #3", "Foo 3")]
        [TestCase("Foo Number 3", "Foo 3")]
        [TestCase("N Foo", "North Foo")]
        [TestCase("S Foo", "South Foo")]
        [TestCase("E. Foo", "East Foo")]
        [TestCase("W. Foo", "West Foo")]
        [TestCase("Pt. 13,149", "Point 13149")]
        [TestCase("Pt. 13,149'", "Point 13149")]
        [TestCase("Foo's Pinnacle", "Foos Pinnacle")]
        [TestCase("Mountain of Foos", "Mountain of Foo")]
        public void CondensePotentialMatches_Multiple_Potential_Matches_Matches_By_SubformationName_To_SubformationName_And_OtherName(string formationSubformationName, string subFormationName)
        {
            List<Formation> formations = new List<Formation>();
            Formation formation1 = new Formation(NAME_FORMATION + "1", 90, 90, NAME_OTHER_FORMATION + "1", 0, formationSubformationName + " 1");
            formations.Add(formation1);
            Formation formation2 = new Formation("NonMatching2", 90, 90, "NonMatching2", 0, "NonMatching2");
            formations.Add(formation2);
            Formation formation = new Formation("Mt Moo", 90, 90, "Foo Crags", 0, formationSubformationName);
            formations.Add(formation);
            Formation formationAlmostMatch = new Formation("Mt Moo", 90, 90, "Moo Crags", 0, formationSubformationName);
            formations.Add(formationAlmostMatch);
            Formation formation3 = new Formation(NAME_FORMATION + "3", 90, 90, NAME_OTHER_FORMATION + "3", 0, formationSubformationName + " 3");
            formations.Add(formation3);

            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, "Foo Crags", subFormationName);
            formationMatcher.AddIfPotentialMatchByName(formations);

            Assert.That(formationMatcher.PossibleFormations.Count, Is.EqualTo(4));

            formationMatcher.CondensePotentialMatches();

            Assert.That(formationMatcher.MatchedFormation, Is.Not.Null);
            Assert.That(formationMatcher.MatchedFormation.Name, Is.EqualTo(formations[2].Name));
            Assert.That(formationMatcher.MatchedFormation.OtherName, Is.EqualTo(formations[2].OtherName));
            Assert.That(formationMatcher.MatchedFormation.SubFormationName, Is.EqualTo(formations[2].SubFormationName));
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

            List<Formation> formations = new List<Formation>();
            Formation formation1 = new Formation(formationEricssonCrags, 90, 90, subformationName: formationEricssonCrag1);
            formations.Add(formation1);
            Formation formation2 = new Formation(formationEricssonCrags, 90, 90, subformationName: formationEricssonCrag2);
            formations.Add(formation2);
            Formation formation3 = new Formation(formationEricssonCrags, 90, 90, subformationName: formationEricssonCrag3);
            formations.Add(formation3);
            Formation formation4 = new Formation(formationEricssonCrags, 90, 90, subformationName: formationEricssonCrag4);
            formations.Add(formation4);
            Formation formationCrags = new Formation(formationEricssonCrags, 90, 90);
            formations.Add(formationCrags);
            Formation formationMtn = new Formation(formationMtEricsson, 90, 90);
            formations.Add(formationMtn);

            FormationMatcher formationMatcher1 = new FormationMatcher(regionName, formationEricssonCrags, formationEricssonCrag3);
            formationMatcher1.AddIfPotentialMatchByName(formations);

            Assert.That(formationMatcher1.PossibleFormations.Count, Is.EqualTo(5));

            formationMatcher1.CondensePotentialMatches();

            Assert.That(formationMatcher1.MatchedFormation, Is.Not.Null);
            int indexCrag = formations.IndexOf(formation3);
            Assert.That(formationMatcher1.MatchedFormation.Name, Is.EqualTo(formations[indexCrag].Name));
            Assert.That(formationMatcher1.MatchedFormation.OtherName, Is.EqualTo(formations[indexCrag].OtherName));
            Assert.That(formationMatcher1.MatchedFormation.SubFormationName, Is.EqualTo(formations[indexCrag].SubFormationName));

            FormationMatcher formationMatcher2 = new FormationMatcher(regionName, formationMtEricsson);
            formationMatcher2.AddIfPotentialMatchByName(formations);

            Assert.That(formationMatcher2.PossibleFormations.Count, Is.EqualTo(1));

            formationMatcher2.CondensePotentialMatches();

            Assert.That(formationMatcher2.MatchedFormation, Is.Not.Null);
            int indexMtn = formations.IndexOf(formationMtn);
            Assert.That(formationMatcher2.MatchedFormation.Name, Is.EqualTo(formations[indexMtn].Name));
            Assert.That(formationMatcher2.MatchedFormation.OtherName, Is.EqualTo(formations[indexMtn].OtherName));
            Assert.That(formationMatcher2.MatchedFormation.SubFormationName, Is.EqualTo(formations[indexMtn].SubFormationName));
        }

        
        [Test]
        public void CondensePotentialMatches_Cathedral_Peak_And_Eichorns_Pinnacle()
        {
            string regionName = "Cathedral Area";
            string formationCathedralPk = "Cathedral Peak";
            string formationEichornsPinnacle = "Eichorn''s Pinnacle";

            List<Formation> formations = new List<Formation>();
            Formation formation1 = new Formation(formationCathedralPk, 90, 90);
            formations.Add(formation1);
            Formation formation2 = new Formation(formationCathedralPk, 90, 90, subformationName: formationEichornsPinnacle);
            formations.Add(formation2);

            FormationMatcher formationMatcher1 = new FormationMatcher(regionName, formationCathedralPk);
            formationMatcher1.AddIfPotentialMatchByName(formations);

            Assert.That(formationMatcher1.PossibleFormations.Count, Is.EqualTo(2));

            formationMatcher1.CondensePotentialMatches();

            Assert.That(formationMatcher1.MatchedFormation, Is.Not.Null);
            int indexCathedral = formations.LastIndexOf(formation1);
            Assert.That(formationMatcher1.MatchedFormation.Name, Is.EqualTo(formations[indexCathedral].Name));
            Assert.That(formationMatcher1.MatchedFormation.OtherName, Is.EqualTo(formations[indexCathedral].OtherName));
            Assert.That(formationMatcher1.MatchedFormation.SubFormationName, Is.EqualTo(formations[indexCathedral].SubFormationName));

            FormationMatcher formationMatcher2 = new FormationMatcher(regionName, formationEichornsPinnacle);
            formationMatcher2.AddIfPotentialMatchByName(formations);

            Assert.That(formationMatcher2.PossibleFormations.Count, Is.EqualTo(1));

            formationMatcher2.CondensePotentialMatches();

            Assert.That(formationMatcher2.MatchedFormation, Is.Not.Null);
            int indexEichorn = formations.LastIndexOf(formation2);
            Assert.That(formationMatcher2.MatchedFormation.Name, Is.EqualTo(formations[indexEichorn].Name));
            Assert.That(formationMatcher2.MatchedFormation.OtherName, Is.EqualTo(formations[indexEichorn].OtherName));
            Assert.That(formationMatcher2.MatchedFormation.SubFormationName, Is.EqualTo(formations[indexEichorn].SubFormationName));
        }

        [Test]
        public void Override_ToString_Empty_Returns_Default_ToString()
        {
            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, string.Empty);
            Assert.That(formationMatcher.ToString(), Is.EqualTo("MPT.GIS.FormationMatcher"));
        }

        [Test]
        public void Override_ToString_With_Name()
        {
            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, NAME_MATCHER_FORMATION);
            Assert.That(formationMatcher.ToString(), Is.EqualTo($"{NAME_MATCHER_FORMATION}"));
        }

        [Test]
        public void Override_ToString_With_Name_And_SubFormationName()
        {
            FormationMatcher formationMatcher = new FormationMatcher(NAME_REGION, NAME_MATCHER_FORMATION, NAME_MATCHER_SUBFORMATION);
            Assert.That(formationMatcher.ToString(), Is.EqualTo($"{NAME_MATCHER_SUBFORMATION} (of {NAME_MATCHER_FORMATION})"));
        }
    }
}
