// ***********************************************************************
// Assembly         : MPT.GIS
// Author           : Mark Thomas
// Created          : 10-04-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-02-2017
// ***********************************************************************
// <copyright file="FormationMatcher.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MPT.String.Character;
using MPT.String.Word;
using MPT.String.Number;
using sLib = MPT.String.StringLibrary;

namespace MPT.GIS
{

    /// <summary>
    /// Matches formations of known formations to those where only the name and/or region are known.
    /// </summary>
    public class FormationMatcher
    {
        #region Properties
        /// <summary>
        /// The name of the region that the formation lies within.
        /// </summary>
        /// <value>The name of the region.</value>
        public string RegionName { get; }

        /// <summary>
        /// The name of the formation.
        /// </summary>
        /// <value>The name of the formation.</value>
        public string FormationName { get; }

        /// <summary>
        /// The name of the sub-formation.
        /// </summary>
        /// <value>The name of the sub formation.</value>
        public string SubFormationName { get; }

        private Formation _matchedFormation;
        /// <summary>
        /// The matched formation.
        /// </summary>
        /// <value>The matched formation.</value>
        public Formation MatchedFormation => _matchedFormation?.Clone();

        private readonly List<Formation> _possibleFormations = new List<Formation>();
        /// <summary>
        /// The possible formations that match this formation.
        /// </summary>
        /// <value>The possible formations.</value>
        public IList<Formation> PossibleFormations => new ReadOnlyCollection<Formation>(_possibleFormations);
        #endregion

        #region Public Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="FormationMatcher"/> class.
        /// </summary>
        /// <param name="regionName">Name of the region.</param>
        /// <param name="formationName">Name of the formation.</param>
        /// <param name="subformationName">Name of the sub-formation.</param>
        public FormationMatcher(string regionName,
            string formationName,
            string subformationName = "")
        {
            RegionName = regionName;
            FormationName = formationName;
            SubFormationName = subformationName;
        }


        /// <summary>
        /// Adds the formation if it is a potential match based on the various formation names.
        /// </summary>
        /// <param name="formation">The formation.</param>
        public void AddIfPotentialMatchByName(Formation formation)
        {
            if (potentialMatchByName(formation))
            {
                _possibleFormations.Add(formation);
            }
        }

        /// <summary>
        /// Adds each formation if it is a potential match based on the various formation names.
        /// </summary>
        /// <param name="formations">The formations.</param>
        public void AddIfPotentialMatchByName(IEnumerable<Formation> formations)
        {
            foreach (Formation formation in formations)
            {
                AddIfPotentialMatchByName(formation);
            }
        }

        /// <summary>
        /// Selects the matching formation based on the index provided.
        /// </summary>
        /// <param name="index">The index of the formation chosen from <see cref="PossibleFormations"/>.</param>
        public void ChooseMatch(int index)
        {
            if (index > _possibleFormations.Count - 1) throw new System.IndexOutOfRangeException();
            _matchedFormation = _possibleFormations[index];
        }

        /// <summary>
        /// Condenses the potential matches if a singular match can be found.
        /// </summary>
        public void CondensePotentialMatches()
        {
            int indexMatch = uniqueMatchIndex();
            if (indexMatch >= 0) { ChooseMatch(indexMatch); }
        }

        /// <summary>
        /// Normalizes the geographic name, handling irregular cases such as possession, pluralization, elevations, numbering, abbreviations, similar terms, etc.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>System.String.</returns>
        public static string NormalizeGeographicName(string name)
        {
            string modifiedName = name;

            // Handle cases such as Eichorn's Pinnacle vs. Eichorns Pinnacle
            modifiedName = modifiedName.ApplyToIndividualWords(singularNonPossessiveWord);

            // Remove characters used for altitudes, e.g. Point 13,754 = Point 13754, Point 13754' = Point 13754 
            // Remove words relating to numbering, e.g. Crag #3 or Crag Number 3 = Crag 3
            modifiedName = normalizeGeographicNumeric(modifiedName);

            // Remove possible character for abbreviations & acronyms, e.g. Mt. = Mt
            modifiedName = normalizeGeographicAbbreviationName(name,
                            modifiedName,
                            map: new Dictionary<string, string>()
                                {
                                    { "mt", "mount"},
                                    { "mtn", "mountain"},
                                    { "mtns", "mountains"},
                                    { "pk", "peak"},
                                    { "pks", "peaks"},
                                    { "pt", "point"},
                                    { "pts", "points"},
                                    { "n", "north"},
                                    { "s", "south"},
                                    { "e", "east"},
                                    { "w", "west"},
                                },
                            wordsToKeepPlural: new List<string>
                                    { "mountains",
                                      "peaks",
                                      "points"});

            // Substitute a consistent term for terms that are variable, such as spire vs. needle.
            modifiedName = normalizeGeographicConstantName(name,
                            modifiedName,
                            constantWord: "spire",
                            wordsReplace: new List<string>()
                                            {
                                                "pinnacle",
                                                "tower",
                                                "needle",
                                                "spire"
                                            });
            return modifiedName;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(FormationName))
            {
                return base.ToString();
            }
            if (string.IsNullOrEmpty(SubFormationName))
            {
                return FormationName;
            }
            return SubFormationName + " (of " + FormationName + ")";
        }
        #endregion


        #region Private: Helper Wrapper Methods for Delegates (Handles optional parameters)
        private static string RemoveWord(string word, string wordToRemove)
        {
            return sLib.RemoveWord(word, wordToRemove);
        }

        private static string ReplaceWithConstant(string word,
            List<string> wordsToReplace,
            string wordToUse)
        {
            return sLib.ReplaceWithConstant(word, wordsToReplace, wordToUse);
        }

        private static string ReplaceUsingMap(string word,
            Dictionary<string, string> wordMap)
        {
            return sLib.ReplaceUsingMap(word, wordMap);
        }
        #endregion

        #region Private: AddIfPotentialMatch
        /// <summary>
        /// There is a potential match based on the various formation names.
        /// </summary>
        /// <param name="formation">The formation.</param>
        /// <returns><c>true</c> if is a potential match based on the various formation names, <c>false</c> otherwise.</returns>
        private bool potentialMatchByName(Formation formation)
        {
            return potentialMatchByNameUnaltered(formation) ||
                   potentialMatchByNameNormalized(formation) ||
                   potentialMatchByNameNumeric(formation);
        }

        /// <summary>
        /// Check if any combination of one name contains the other.
        /// </summary>
        /// <param name="formation">The formation.</param>
        /// <returns>System.Boolean.</returns>
        private bool potentialMatchByNameUnaltered(Formation formation)
        {
            return (eitherContainsOther(FormationName, formation.Name) ||
                    eitherContainsOther(FormationName, formation.OtherName) ||
                    eitherContainsOther(FormationName, formation.SubFormationName) ||
                    eitherContainsOther(SubFormationName, formation.Name) ||
                    eitherContainsOther(SubFormationName, formation.OtherName) ||
                    eitherContainsOther(SubFormationName, formation.SubFormationName));
        }

        /// <summary>
        /// Check with the names singular and made non-possessive, with other tweaks for geographical name irregularities.
        /// </summary>
        /// <param name="formation">The formation.</param>
        /// <returns><c>true</c> if there is a match among any of the name combinations, <c>false</c> otherwise.</returns>
        private bool potentialMatchByNameNormalized(Formation formation)
        {
            string formationNameNormalized = FormationName.ApplyToIndividualWords(NormalizeGeographicName);
            string subFormationNameNormalized = SubFormationName.ApplyToIndividualWords(NormalizeGeographicName);
            string potentialFormationNameNormalized = formation.Name.ApplyToIndividualWords(NormalizeGeographicName);
            string potentialFormationOtherNameNormalized = formation.OtherName.ApplyToIndividualWords(NormalizeGeographicName);
            string potentialSubFormationOtherNameNormalized = formation.SubFormationName.ApplyToIndividualWords(NormalizeGeographicName);

            return (eitherContainsOther(formationNameNormalized, potentialFormationNameNormalized) ||
                    eitherContainsOther(formationNameNormalized, potentialFormationOtherNameNormalized) ||
                    eitherContainsOther(formationNameNormalized, potentialSubFormationOtherNameNormalized) ||
                    eitherContainsOther(subFormationNameNormalized, potentialFormationNameNormalized) ||
                    eitherContainsOther(subFormationNameNormalized, potentialFormationOtherNameNormalized) ||
                    eitherContainsOther(subFormationNameNormalized, potentialSubFormationOtherNameNormalized));
        }

        /// <summary>
        /// Check with elevations normalized, e.g. no 13,749 or 13,749', just 13749.
        /// </summary>
        /// <param name="formation">The formation.</param>
        /// <returns><c>true</c> if if there is a match among any of the name combinations, <c>false</c> otherwise.</returns>
        private bool potentialMatchByNameNumeric(Formation formation)
        {
            string formationNameNormalizedNumeric = FormationName.GetNumbers();
            string subFormationNameNormalizedNumeric = SubFormationName.GetNumbers();
            string potentialFormationNameNormalizedNumeric = formation.Name.GetNumbers();
            string potentialFormationOtherNameNormalizedNumeric = formation.OtherName.GetNumbers();
            string potentialSubFormationOtherNameNormalizedNumeric = formation.SubFormationName.GetNumbers();

            return (eitherContainsOther(formationNameNormalizedNumeric, potentialFormationNameNormalizedNumeric) ||
                    eitherContainsOther(formationNameNormalizedNumeric, potentialFormationOtherNameNormalizedNumeric) ||
                    eitherContainsOther(formationNameNormalizedNumeric, potentialSubFormationOtherNameNormalizedNumeric) ||
                    eitherContainsOther(subFormationNameNormalizedNumeric, potentialFormationNameNormalizedNumeric) ||
                    eitherContainsOther(subFormationNameNormalizedNumeric, potentialFormationOtherNameNormalizedNumeric) ||
                    eitherContainsOther(subFormationNameNormalizedNumeric, potentialSubFormationOtherNameNormalizedNumeric));
        }

        /// <summary>
        /// Either string contains the other.
        /// </summary>
        /// <param name="one">One string.</param>
        /// <param name="other">The other string.</param>
        /// <returns><c>true</c> if either string contains the other, <c>false</c> otherwise.</returns>
        private bool eitherContainsOther(string one,
            string other)
        {
            if (string.IsNullOrEmpty(one) || string.IsNullOrEmpty(other))
                return false;

            return (one.Contains(other) ||
                    other.Contains(one));
        }
        #endregion

        #region Private: CondensePotentialMatches
        /// <summary>
        /// Determines whether there is a unique possible match where no user input is needed.
        /// If so, a valid index of the formation is returned.
        /// Returns -1 if no unique match was determined.
        /// </summary>
        /// <returns>System.Int32.</returns>
        private int uniqueMatchIndex()
        {
            switch (_possibleFormations.Count)
            {
                case 0:
                    return -1;
                case 1:
                    return 0;
                default: // For now, let's loosely assume that the following potential matches are reliable and don't require user selection
                    Formation formation = probableMatchByName(_possibleFormations);
                    return formation == null ? -1 : _possibleFormations.IndexOf(formation);
            }
        }

        /// <summary>
        /// Determines whether the name has any probable match to the formations listed and returns a formation if it is a very likely match.
        /// This method uses stricter and more specific checks than <see cref="potentialMatchByName"/>.
        /// </summary>
        /// <param name="possibleFormations">The possible formations.</param>
        /// <returns><c>true</c> if the name has any possible match to the formations listed; otherwise, <c>false</c>.</returns>
        private Formation probableMatchByName(IEnumerable<Formation> possibleFormations)
        {
            List<PossibleFormation> possibleFormationsNormalizedNames = possibleFormations.Select(formation => new PossibleFormation(formation)).ToList();

            var possibleSubFormation = MatchingSubFormationName(possibleFormationsNormalizedNames);
            if (possibleSubFormation.Count > 0)
            {
                possibleSubFormation = ConfirmMatchingSubFormationName(possibleSubFormation);
                return possibleSubFormation.Count == 1 ? possibleSubFormation[0].Formation : null;
            }

            possibleFormationsNormalizedNames = MatchingFormationName(possibleFormationsNormalizedNames);
            return possibleFormationsNormalizedNames.Count == 1 ? possibleFormationsNormalizedNames[0].Formation : null;
        }




        private List<PossibleFormation> MatchingSubFormationName(List<PossibleFormation> possibleFormations)
        {
            if (string.IsNullOrEmpty(SubFormationName)) return new List<PossibleFormation>();

            string normalizedSubFormationName = NormalizeGeographicName(SubFormationName);
            List<PossibleFormation> matchingFormations = new List<PossibleFormation>();
            foreach (PossibleFormation formation in possibleFormations)
            {
                if (string.CompareOrdinal(normalizedSubFormationName,
                                          formation.Name) == 0 ||
                    string.CompareOrdinal(normalizedSubFormationName,
                                          formation.OtherName) == 0 ||
                    string.CompareOrdinal(normalizedSubFormationName,
                                          formation.SubFormationName) == 0)
                {
                    matchingFormations.Add(formation);
                }
            }
            return matchingFormations;
        }

        private List<PossibleFormation> ConfirmMatchingSubFormationName(List<PossibleFormation> possibleSubFormations)
        {
            List<PossibleFormation> possibleFormation = possibleSubFormations.ToList();
            if (possibleFormation.Count() <= 1) return possibleFormation;

            string normalizedFormationName = NormalizeGeographicName(FormationName);
            List<PossibleFormation> maybeMatchingFormations = new List<PossibleFormation>();
            List<PossibleFormation> likelyMatchingFormations = new List<PossibleFormation>();
            foreach (PossibleFormation formation in possibleFormation)
            {
                // Check the case of Name/OtherName matching the formation name, where the subformations names of both already match
                if (string.CompareOrdinal(
                        normalizedFormationName,
                        formation.Name) == 0 ||
                    string.CompareOrdinal(
                        normalizedFormationName,
                        formation.OtherName) == 0)
                {
                    likelyMatchingFormations.Add(formation);
                }
                maybeMatchingFormations.Add(formation);
            }

            return likelyMatchingFormations.Count > 0 ? likelyMatchingFormations : maybeMatchingFormations;
        }

        private List<PossibleFormation> MatchingFormationName(List<PossibleFormation> possibleFormations)
        {
            List<PossibleFormation> maybeMatchingFormations = new List<PossibleFormation>();
            List<PossibleFormation> likelyMatchingFormations = new List<PossibleFormation>();

            string normalizedFormationName = NormalizeGeographicName(FormationName);
            foreach (PossibleFormation formation in possibleFormations)
            {
                if (string.CompareOrdinal(normalizedFormationName,
                        formation.Name) != 0 &&
                    string.CompareOrdinal(normalizedFormationName,
                        formation.OtherName) != 0 &&
                    string.CompareOrdinal(normalizedFormationName,
                        formation.SubFormationName) != 0) continue;

                if (string.IsNullOrEmpty(formation.SubFormationName))
                {
                    likelyMatchingFormations.Add(formation);
                }
                else
                {
                    maybeMatchingFormations.Add(formation);
                }
            }

            return likelyMatchingFormations.Count > 0 ? likelyMatchingFormations : maybeMatchingFormations;
        }

        private class PossibleFormation
        {
            public Formation Formation { get; private set; }
            public string Name { get; private set; }
            public string OtherName { get; private set; }
            public string SubFormationName { get; private set; }

            public PossibleFormation(Formation formation)
            {
                Formation = formation;
                Name = NormalizeGeographicName(formation.Name);
                OtherName = NormalizeGeographicName(formation.OtherName);
                SubFormationName = NormalizeGeographicName(formation.SubFormationName);
            }
        }
        #endregion

        #region Private: NormalizeGeographicName
        private static string normalizeGeographicNumeric(string modifiedName)
        {
            // Remove characters used for altitudes, e.g. Point 13,754 = Point 13754, Point 13754' = Point 13754
            modifiedName = modifiedName.Replace(",", string.Empty);
            modifiedName = modifiedName.Replace("'", string.Empty);

            // Remove words relating to numbering, e.g. Crag #3 or Crag Number 3 = Crag 3
            modifiedName = modifiedName.Replace("#", string.Empty);
            modifiedName = modifiedName.ApplyToIndividualWords(RemoveWord, "number");

            return modifiedName;
        }

        private static string normalizeGeographicAbbreviationName(string originalName,
            string modifiedName,
            Dictionary<string, string> map,
            List<string> wordsToKeepPlural)
        {
            modifiedName = modifiedName.Replace(".", string.Empty);
            modifiedName = modifiedName.ApplyToIndividualWords(ReplaceUsingMap, map);

            // Retain pluralization of feature types, e.g. mountains
            return wordsToKeepPlural.Where(
                            wordToKeepPlural => originalName.ToLower().Contains(wordToKeepPlural))
                                .Aggregate(modifiedName, (current, wordToKeepPlural) => current.Replace(wordToKeepPlural.ToSingular(), wordToKeepPlural));
        }


        private static string normalizeGeographicConstantName(string originalName,
            string modifiedName,
            string constantWord,
            List<string> wordsReplace)
        {
            modifiedName = modifiedName.ApplyToIndividualWords(ReplaceWithConstant,
                                wordsReplace,
                                constantWord);

            // Retain pluralization of constant feature types, e.g. mountains
            var constantWordsToKeepPlural = wordsReplace.Select(wordReplace => wordReplace.ToPlural()).ToList();
            return constantWordsToKeepPlural.Where(
                wordToKeepPlural => originalName.ToLower().Contains(wordToKeepPlural))
                                        .Aggregate(modifiedName, (current, wordToKeepPlural) => current.Replace(constantWord, constantWord.ToPlural()));
        }

        /// <summary>
        /// Makes the word singular and non-possessive.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns>System.String.</returns>
        private static string singularNonPossessiveWord(string word)
        {
            string modifiedWord = word.ToLower();
            modifiedWord = modifiedWord.ToSingular();
            modifiedWord = modifiedWord.FromPossessive();

            return modifiedWord;
        }
        #endregion

        
    }
}
